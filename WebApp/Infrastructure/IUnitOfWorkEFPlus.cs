using System;
using System.Threading.Tasks;
using WebApp.Model;
using WebApp.Model.Entity;

namespace WebApp.Infrastructure
{
    public interface IUnitOfWorkEFPlus : IDisposable
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
