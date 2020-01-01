using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Z.EntityFramework.Plus;

namespace AuditTrail_Console.Infrastructure
{
    public class UnitOfWorkEFPlus : IUnitOfWorkEFPlus
    {
        private readonly AuditTraiEFPlusDbContext _dbContext;

        public UnitOfWorkEFPlus()
        {
            _dbContext = new AuditTraiEFPlusDbContext();
        }
        public IAuditTrailDbContext DataContext
        {
            get { return _dbContext; }
        }

        private IRepositoryEFPlus<Person> _personRepo;
        public IRepositoryEFPlus<Person> PersonRepository
        {
            get
            {
                if (_personRepo == null)
                    _personRepo = new RepositoryEFPlus<Person>(_dbContext);
                return _personRepo;
            }
        }

        private IRepositoryEFPlus<AuditEntry> _auditEntryRepo; 
        public IRepositoryEFPlus<AuditEntry> AuditEntryRepository
        {
            get
            {
                if (_auditEntryRepo == null)
                    _auditEntryRepo = new RepositoryEFPlus<AuditEntry>(_dbContext);
                return _auditEntryRepo;
            }
        }

        private IRepositoryEFPlus<AuditEntryProperty> _auditEntryPropertyRepo;
        public IRepositoryEFPlus<AuditEntryProperty> AuditEntryPropertyRepository
        {
            get
            {
                if (_auditEntryPropertyRepo == null)
                    _auditEntryPropertyRepo = new RepositoryEFPlus<AuditEntryProperty>(_dbContext);
                return _auditEntryPropertyRepo;
            }
        }

        private IRepositoryEFPlus<HistoryTrackingAudit> _historyTrackingAuditRepo;
        public IRepositoryEFPlus<HistoryTrackingAudit> HistoryTrackingAuditRepository
        {
            get
            {
                if (_historyTrackingAuditRepo == null)
                    _historyTrackingAuditRepo = new RepositoryEFPlus<HistoryTrackingAudit>(_dbContext);
                return _historyTrackingAuditRepo;
            }
        }

        private IRepositoryEFPlus<HistoryTrackingValueAudit> _historyTrackingValueAuditRepo;
        public IRepositoryEFPlus<HistoryTrackingValueAudit> HistoryTrackingValueAuditRepository
        {
            get
            {
                if (_historyTrackingValueAuditRepo == null)
                    _historyTrackingValueAuditRepo = new RepositoryEFPlus<HistoryTrackingValueAudit>(_dbContext);
                return _historyTrackingValueAuditRepo;
            }
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }

        public int SaveChanges()
        {
            try
            {
                //var entries = _dbContext.ChangeTracker.Entries();
                //var audit = new Audit();

                ////audit.PreSaveChanges(_dbContext);

                //var rNo = _dbContext.SaveChanges(audit);

                ////audit.PostSaveChanges();

                //// Access to all auditing information
                //var entries = audit.Entries;
                //foreach (var e in entries)
                //{
                //    var entity = e.Entity;
                //    //foreach (var property in e.Properties)
                //    //{
                //    //}
                //    switch (e.State)
                //    {
                //        case AuditEntryState.EntityAdded:
                //            Console.WriteLine("==== State Add EF Plus ====");
                //            break;
                //        case AuditEntryState.EntityModified:
                //            Console.WriteLine("==== State Modified EF Plus ====");
                //            break;
                //    }
                //}
                //return rNo;
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

                return _dbContext.SaveChanges(); ;
            }
            catch (DbEntityValidationException e)
            {
                throw new Exception(e.Message);
            }
        }

        public Task<int> SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

        public void SetDetachChanges(bool value)
        {
            _dbContext.Configuration.AutoDetectChangesEnabled = value;
        }
    }
}
