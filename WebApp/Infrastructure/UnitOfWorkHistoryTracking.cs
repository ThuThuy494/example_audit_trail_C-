﻿using Autofac;
using System;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Threading.Tasks;
using WebApp.Model;
using WebApp.Model.Entity;
using Z.EntityFramework.Plus;
using AuditEntry = WebApp.Model.Entity.AuditEntry;
using AuditEntryProperty = WebApp.Model.Entity.AuditEntryProperty;

namespace WebApp.Infrastructure
{
    public class UnitOfWorkHistoryTracking : IUnitOfWorkHistoryTracking
    {
        private readonly AuditTrailDbContext _dbContext;

        public UnitOfWorkHistoryTracking(IComponentContext container, AuditTrailDbContext dbContext)
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
        }
    }
}
