using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApp.HistoryTracking
{
    public abstract class RepositoryBaseImplementation
    {
        public virtual void LogHistoryTracking(DbEntityEntry dbEntityEntry) { }

        public virtual void LogDynamicHistoryTracking(DbEntityEntry dbEntityEntry) { }
    }
}
