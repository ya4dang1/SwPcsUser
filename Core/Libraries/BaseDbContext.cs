using Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Libraries
{
    public abstract class BaseDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<AuditLog> AuditLogs { get; set; }

        public DbSet<FileLibrary> FileLibraries { get; set; }

        private readonly IHttpContextAccessor httpContext;

        public BaseDbContext(DbContextOptions options) : base(options)
        {
        }

        public BaseDbContext(DbContextOptions options, IHttpContextAccessor httpContext) : base(options)
        {
            this.httpContext = httpContext;
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            TrackChanges();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            TrackChanges();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private void TrackChanges()
        {
            var entities = ChangeTracker.Entries().Where(x => x.Entity is BaseModel && (x.State == EntityState.Added || x.State == EntityState.Modified || x.State == EntityState.Deleted));
            string actionBy = GetActionBy();
            DateTime actionOn = DateTime.UtcNow;

            foreach (var entity in entities)
            {
                if (entity.State != EntityState.Deleted)
                {
                    ((BaseModel)entity.Entity).ActionBy = actionBy;
                    ((BaseModel)entity.Entity).ActionOn = actionOn;
                }

                this.Add<AuditLog>(new AuditLog
                {
                    EntityName = entity.Entity.GetType().Name,
                    EntityId = ((BaseModel)entity.Entity).Id,
                    Action = entity.State,
                    ActionBy = actionBy,
                    ActionOn = actionOn,
                    OldValue = JsonConvert.SerializeObject(entity.OriginalValues),
                    NewValue = JsonConvert.SerializeObject(entity.CurrentValues),
                });
            }
        }

        private string GetActionBy()
        {
            string name = httpContext?.HttpContext?.User?.Identity?.Name;

            if (name == null)
                return "System";

            return name;
        }
    }
}
