using Autofac;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Threading.Tasks;
using WebApp.HistoryTracking;
using WebApp.Model;
using WebApp.Model.Entity;

namespace WebApp.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AuditTrailDbContext _dbContext;

        public UnitOfWork(IComponentContext container)
        {
            _dbContext = new AuditTrailDbContext();
            _dbContext.Container = container;
        }
        public IAuditTrailDbContext DataContext
        {
            get { return _dbContext; }
        }

        private IRepository<Person> _personRepo;
        public IRepository<Person> PersonRepository
        {
            get
            {
                if (_personRepo == null)
                    _personRepo = new Repository<Person>(_dbContext, _dbContext.Container);
                return _personRepo;
            }
        }

        private IRepository<PersonDetail> _personRoleRepo;
        public IRepository<PersonDetail> PersonRoleRepository
        {
            get
            {
                if (_personRoleRepo == null)
                    _personRoleRepo = new Repository<PersonDetail>(_dbContext, _dbContext.Container);
                return _personRoleRepo;
            }
        }

        private IRepository<AuditEntry> _auditEntryRepo;
        public IRepository<AuditEntry> AuditEntryRepository
        {
            get
            {
                if (_auditEntryRepo == null)
                    _auditEntryRepo = new Repository<AuditEntry>(_dbContext, _dbContext.Container);
                return _auditEntryRepo;
            }
        }

        private IRepository<AuditEntryProperty> _auditEntryPropertyRepo;
        public IRepository<AuditEntryProperty> AuditEntryPropertyRepository
        {
            get
            {
                if (_auditEntryPropertyRepo == null)
                    _auditEntryPropertyRepo = new Repository<AuditEntryProperty>(_dbContext, _dbContext.Container);
                return _auditEntryPropertyRepo;
            }
        }

        private IRepository<HistoryTrackingAudit> _historyTrackingAuditRepo;
        public IRepository<HistoryTrackingAudit> HistoryTrackingAuditRepository
        {
            get
            {
                if (_historyTrackingAuditRepo == null)
                    _historyTrackingAuditRepo = new Repository<HistoryTrackingAudit>(_dbContext, _dbContext.Container);
                return _historyTrackingAuditRepo;
            }
        }

        private IRepository<HistoryTrackingValueAudit> _historyTrackingValueAuditRepo;
        public IRepository<HistoryTrackingValueAudit> HistoryTrackingValueAuditRepository
        {
            get
            {
                if (_historyTrackingValueAuditRepo == null)
                    _historyTrackingValueAuditRepo = new Repository<HistoryTrackingValueAudit>(_dbContext, _dbContext.Container);
                return _historyTrackingValueAuditRepo;
            }
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }

        //public IRepository<T> Repository<T>()
        //{
        //    return new Repository<T>(_dbContext, _currentUser);
        //}

        public int SaveChanges()
        {
            try
            {
                //var entries = SaveChangeDetail();
                //foreach (var e in entries)
                //{
                //    var entity = e.Entity;
                //    switch (e.State)
                //    {
                //        case EntityState.Added:
                //            Console.WriteLine("==== State Add ====");
                //            break;
                //        case EntityState.Modified:
                //            Console.WriteLine("==== State Modified ====");
                //            break;
                //    }
                //}
                return _dbContext.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                throw new Exception(e.Message);
            }
        }

        public Task<int> SaveChangesAsync()
        {
            try
            {
                var entries = SaveChangeDetail();
                foreach (var e in entries)
                {
                    var entity = e.Entity;
                    switch (e.State)
                    {
                        case EntityState.Added:
                            Console.WriteLine("==== State Add ====");
                            break;
                        case EntityState.Modified:
                            Console.WriteLine("==== State Modified ====");
                            break;
                    }
                }
                return _dbContext.SaveChangesAsync();
            }
            catch (DbEntityValidationException e)
            {
                throw new Exception(e.Message);
            }
        }

        public void SetDetachChanges(bool value)
        {
            _dbContext.Configuration.AutoDetectChangesEnabled = value;
        }

        private List<DbEntityEntry> SaveChangeDetail()
        {
            var changedEntities = _dbContext.ChangeTracker.Entries().ToList();
            // Maybe clear cache for unchanged entites I don't filter unchange
            //if (TableUpdatedEvent != null) TableUpdatedEvent(changedEntities);
            // NO need log history, delete workflow for unchange entities
            changedEntities = changedEntities.Where(t => t.State != EntityState.Unchanged).ToList();

            LogHistoryTrackerEvent(changedEntities);
            return changedEntities;
        }

        private void LogHistoryTrackerEvent(IEnumerable<DbEntityEntry> changedEntities)
        {
            var baseImplementation = ResolveRepositoryBaseImplementation();
            if (baseImplementation == null) return;

            var entities = changedEntities.Where(w => ObjectContext.GetObjectType(w.Entity.GetType()).GetInterfaces().Contains(typeof(IHistoryTracker)));

            foreach (var entity in entities)
            {
                baseImplementation.LogHistoryTracking(entity);
            }

            var dynamicEntites = changedEntities.Where(w => ObjectContext.GetObjectType(w.Entity.GetType()).GetInterfaces().Contains(typeof(IDynamicHistoryTracker)));

            foreach (var entity in dynamicEntites)
            {
                baseImplementation.LogDynamicHistoryTracking(entity);
            }
        }
        private RepositoryBaseImplementation ResolveRepositoryBaseImplementation()
        {
            if (_dbContext.Container == null || !_dbContext.Container.IsRegisteredWithKey<RepositoryBaseImplementation>("WebApp.HistoryTracking"))
                return null;
            return _dbContext.Container.ResolveNamed<RepositoryBaseImplementation>("WebApp.HistoryTracking");
        }

    }
}
