using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AuditTrail_Console.Infrastructure
{
    public interface IRepositoryEFPlus<T>
    {
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity, bool isHardDelete = false);
        void Delete(Expression<Func<T, bool>> where, bool isHardDelete = false);

        T GetById(Guid id);
        Task<T> GetByIdAsync(Guid id);
        IQueryable<T> GetQueryById(Guid id);
        Task<TResult> GetPropertyById<TResult>(Guid id, Expression<Func<T, TResult>> selector);

        IQueryable<T> GetQuery();
        IQueryable<T> GetQuery(Expression<Func<T, bool>> where);
        //IQueryable<T> GetQueryWithDeleted();

        IQueryable<T> SqlQuery(string query, string jobName);
        T Refresh(T entity);
    }
}
