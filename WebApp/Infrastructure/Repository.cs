using WebApp.Model;

namespace WebApp.Infrastructure
{
    public class Repository<T> : RepositoryBase<T, AuditTrailDbContext> where T : class
    {

        public Repository(AuditTrailDbContext dataContext) : base(dataContext)
        {
        }
    }
}
