using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuditTrail_Console.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AuditTrailDbContext _dbContext;

        public UnitOfWork()
        {
            _dbContext = new AuditTrailDbContext();
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
                    _personRepo = new Repository<Person>(_dbContext);
                return _personRepo;
            }
        }

        private IRepository<AuditEntry> _auditEntryRepo;
        public IRepository<AuditEntry> AuditEntryRepository
        {
            get
            {
                if (_auditEntryRepo == null)
                    _auditEntryRepo = new Repository<AuditEntry>(_dbContext);
                return _auditEntryRepo;
            }
        }

        private IRepository<AuditEntryProperty> _auditEntryPropertyRepo;
        public IRepository<AuditEntryProperty> AuditEntryPropertyRepository
        {
            get
            {
                if (_auditEntryPropertyRepo == null)
                    _auditEntryPropertyRepo = new Repository<AuditEntryProperty>(_dbContext);
                return _auditEntryPropertyRepo;
            }
        }

        private IRepository<HistoryTrackingAudit> _historyTrackingAuditRepo;
        public IRepository<HistoryTrackingAudit> HistoryTrackingAuditRepository
        {
            get
            {
                if (_historyTrackingAuditRepo == null)
                    _historyTrackingAuditRepo = new Repository<HistoryTrackingAudit>(_dbContext);
                return _historyTrackingAuditRepo;
            }
        }

        private IRepository<HistoryTrackingValueAudit> _historyTrackingValueAuditRepo;
        public IRepository<HistoryTrackingValueAudit> HistoryTrackingValueAuditRepository
        {
            get
            {
                if (_historyTrackingValueAuditRepo == null)
                    _historyTrackingValueAuditRepo = new Repository<HistoryTrackingValueAudit>(_dbContext);
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
                var entries = _dbContext.ChangeTracker.Entries();
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
                var entries = _dbContext.ChangeTracker.Entries();
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
    }
}
