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
using Z.EntityFramework.Plus;
using AuditEntry = WebApp.Model.Entity.AuditEntry;
using AuditEntryProperty = WebApp.Model.Entity.AuditEntryProperty;

namespace WebApp.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AuditTrailDbContext _dbContext;

        public UnitOfWork(IComponentContext container, AuditTrailDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbContext.Container = container;
        }
        public IAuditTrailDbContext DataContext
        {
            get { return _dbContext; }
        }

        private Repository<Person> _personRepo;
        public IRepository<Person> PersonRepository
        {
            get
            {
                return _personRepo ??
                         (_personRepo = new Repository<Person>(_dbContext)); ;
            }
        }

        private Repository<PersonDetail> _personRoleRepo;
        public IRepository<PersonDetail> PersonRoleRepository
        {
            get
            {
                return _personRoleRepo ??
                        (_personRoleRepo = new Repository<PersonDetail>(_dbContext));
            }
        }

        private Repository<AuditEntry> _auditEntryRepo;
        public IRepository<AuditEntry> AuditEntryRepository
        {
            get
            {
                return _auditEntryRepo ??
                        (_auditEntryRepo = new Repository<AuditEntry>(_dbContext));
            }
        }

        private Repository<AuditEntryProperty> _auditEntryPropertyRepo;
        public IRepository<AuditEntryProperty> AuditEntryPropertyRepository
        {
            get
            {
                return _auditEntryPropertyRepo ??
                         (_auditEntryPropertyRepo = new Repository<AuditEntryProperty>(_dbContext));
            }
        }

        private Repository<HistoryTrackingAudit> _historyTrackingAuditRepo;
        public IRepository<HistoryTrackingAudit> HistoryTrackingAuditRepository
        {
            get
            {
                return _historyTrackingAuditRepo ??
                          (_historyTrackingAuditRepo = new Repository<HistoryTrackingAudit>(_dbContext));
            }
        }

        private Repository<HistoryTrackingValueAudit> _historyTrackingValueAuditRepo;
        public IRepository<HistoryTrackingValueAudit> HistoryTrackingValueAuditRepository
        {
            get
            {
                return _historyTrackingValueAuditRepo ??
                         (_historyTrackingValueAuditRepo = new Repository<HistoryTrackingValueAudit>(_dbContext));
            }
        }

        private Repository<Category> _categoryRepo;
        public IRepository<Category> CategoryRepository
        {
            get
            {
                return _categoryRepo ??
                         (_categoryRepo = new Repository<Category>(_dbContext));
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
                SaveChangeDetail();
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
                SaveChangeDetail();
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

        private void SaveChangeDetail()
        {
            var audit = new Audit();
            audit.PreSaveChanges(_dbContext);
            // Access to all auditing information
            var entries = audit.Entries;
            foreach (var e in entries)
            {
                var entity = e.Entity;
                switch (e.State)
                {
                    case AuditEntryState.EntityAdded:
                        Console.WriteLine("==== State Add EF Plus ====");
                        break;
                    case AuditEntryState.EntityModified:
                        Console.WriteLine("==== State Modified EF Plus ====");
                        break;
                }
            }

            //var rowAffecteds = _dbContext.SaveChanges();
            audit.PostSaveChanges();

            if (audit.Configuration.AutoSavePreAction != null)
            {
                audit.Configuration.AutoSavePreAction(_dbContext, audit);
            }
        }

        //public IRepository<T> Repository<T>()
        //{
        //    return new Repository<T>(_dbContext);
        //}
    }
}
