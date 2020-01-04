using System;
using System.Data.Entity.Infrastructure;

namespace WebApp.Model
{
    public class AuditTrailDbContextFactory : IDbContextFactory<AuditTrailDbContext>
    {
        public AuditTrailDbContext Create()
        {
            return new AuditTrailDbContext();
        }
    }
}
// </auto-generated>


