using AuditTrail_Console.Entity;
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
        IRepositoryEFPlus<Person> PersonRepository { get; }
        IRepositoryEFPlus<PersonDetail> PersonDetailsRepository { get; }
        IRepositoryEFPlus<AuditEntry> AuditEntryRepository { get; }
        IRepositoryEFPlus<AuditEntryProperty> AuditEntryPropertyRepository { get; }
        IRepositoryEFPlus<HistoryTrackingAudit> HistoryTrackingAuditRepository { get; }
        IRepositoryEFPlus<HistoryTrackingValueAudit> HistoryTrackingValueAuditRepository { get; }
    }
}
