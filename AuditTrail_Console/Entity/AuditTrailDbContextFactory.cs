using System;
using System.Data.Entity.Infrastructure;

namespace AuditTrail_Console.Entity
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


