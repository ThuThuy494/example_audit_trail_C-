using AuditTrail_Console.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuditTrail_Console.Infrastructure
{
    public interface IUnitOfWork : IDisposable
    {
        void SetDetachChanges(bool value);
        int SaveChanges(); Task<int> SaveChangesAsync();
        //IRepository<T> Repository<T>();
        IAuditTrailDbContext DataContext { get; }
        IRepository<Person> PersonRepository { get; }
        IRepository<PersonDetail> PersonRoleRepository { get; }
        IRepository<AuditEntry> AuditEntryRepository { get; }
        IRepository<AuditEntryProperty> AuditEntryPropertyRepository { get; }
        IRepository<HistoryTrackingAudit> HistoryTrackingAuditRepository { get; }
        IRepository<HistoryTrackingValueAudit> HistoryTrackingValueAuditRepository { get; }
    }
}
