using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AuditTrail_Console.Infrastructure
{
    public interface IUnitOfWorkEFPlus: IDisposable
    {
        void SetDetachChanges(bool value);
        int SaveChanges(); Task<int> SaveChangesAsync();
        //IRepository<T> Repository<T>();
        IAuditTrailDbContext DataContext { get; }
        IRepository<Person> PersonRepository { get; }
        IRepository<AuditEntry> AuditEntryRepository { get; }
        IRepository<AuditEntryProperty> AuditEntryPropertyRepository { get; }
        IRepository<HistoryTrackingAudit> HistoryTrackingAuditRepository { get; }
        IRepository<HistoryTrackingValueAudit> HistoryTrackingValueAuditRepository { get; }
    }
}
