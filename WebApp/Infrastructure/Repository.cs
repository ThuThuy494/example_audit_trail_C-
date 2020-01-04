using Autofac;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using WebApp.HistoryTracking;
using WebApp.Model;
using WebApp.Model.Entity;

namespace WebApp.Infrastructure
{
    public class Repository<T> : IRepository<T>
        where T : class
    {
        //public DbContext DataContext;
        public readonly AuditTrailDbContext DataContext;
        public readonly DbSet<T> Dbset;
        public readonly IComponentContext componentContext;

        private const string RepositoryCustomImplementation = "AuditTrail_Console.HistoryTracking";

        public Repository(AuditTrailDbContext context, IComponentContext _componentContext)
        {
            DataContext = context;
            this.componentContext = _componentContext;
            Dbset = context.Set<T>();
            context.LogHistoryTrackerEvent = LogHistoryTrackerEvent;
            DataContext.Container = componentContext;
        }
        public void Add(T entity)
        {
            Dbset.Add(entity);
        }

        public void Delete(T entity, bool isHardDelete = false)
        {
            if (isHardDelete)
            {
                Dbset.Remove(entity);
                DataContext.Entry(entity).State = EntityState.Detached;
            }
            else
            {
                //entity.IsDeleted = true;
                UpdateEntityObject(entity);
            }
        }

        private void UpdateEntityObject(T entity)
        {
            Dbset.Attach(entity);
            DataContext.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(System.Linq.Expressions.Expression<Func<T, bool>> where, bool isHardDelete = false)
        {
            var entities = GetQuery(where).AsEnumerable();
            foreach (var entity in entities)
            {
                Delete(entity, isHardDelete);
            }
        }

        public T GetById(Guid id)
        {
            return Dbset.Find(id);
        }

        public Task<T> GetByIdAsync(Guid id)
        {
            return Dbset.FindAsync(id);
        }

        public IQueryable<T> GetQuery()
        {
            return Dbset.AsQueryable();
        }

        public IQueryable<T> GetQuery(System.Linq.Expressions.Expression<Func<T, bool>> where)
        {
            return GetQuery().Where(where);
        }

        public IQueryable<T> SqlQuery(string query, string jobName)
        {
            var latParam = new ObjectParameter("@job_name", jobName);

            object[] parameters = { latParam };

            return Dbset.SqlQuery(query, parameters).AsQueryable();
        }

        public void Update(T entity)
        {
            UpdateEntityObject(entity);
        }

        public IQueryable<T> GetQueryById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<TResult> GetPropertyById<TResult>(Guid id, System.Linq.Expressions.Expression<Func<T, TResult>> selector)
        {
            throw new NotImplementedException();
        }

        public T Refresh(T entity)
        {
            throw new NotImplementedException();
        }

        private void LogHistoryTrackerEvent(IEnumerable<DbEntityEntry> changedEntities)
        {
            var baseImplementation = ResolveRepositoryBaseImplementation();
            if (baseImplementation == null) return;
            var a = componentContext.Resolve<RepositoryBaseImplementation>();
            var entities = changedEntities.Where(w => ObjectContext.GetObjectType(w.Entity.GetType()).GetInterfaces().Contains(typeof(IHistoryTracker)));

            foreach (var entity in entities)
            {
                baseImplementation.LogHistoryTracking(entity);
            }

            var dynamicEntites = changedEntities.Where(w => ObjectContext.GetObjectType(w.Entity.GetType()).GetInterfaces().Contains(typeof(IDynamicHistoryTracker)));

            foreach (var entity in dynamicEntites)
            {
                baseImplementation.LogDynamicHistoryTracking(entity);
            }
        }


        /// <summary>
        /// Resolves the repository base implementation.
        /// </summary>
        /// <returns></returns>
        private RepositoryBaseImplementation ResolveRepositoryBaseImplementation()
        {
            if (DataContext.Container == null || !DataContext.Container.IsRegisteredWithKey<RepositoryBaseImplementation>("WebApp.HistoryTracking"))
                return null;
            return DataContext.Container.ResolveNamed<RepositoryBaseImplementation>("WebApp.HistoryTracking");
        }
    }
}
