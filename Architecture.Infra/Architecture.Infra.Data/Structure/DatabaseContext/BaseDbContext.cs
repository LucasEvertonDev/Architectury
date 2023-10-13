using Architecture.Application.Core.Structure.Extensions;
using Architecture.Application.Domain.Constants;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Architecture.Infra.Data.Structure.DatabaseContext;

public class BaseDbContext<TContext> : DbContext where TContext : DbContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public BaseDbContext(DbContextOptions<TContext> options, IHttpContextAccessor httpContextAccessor)
       : base(options)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("CreateDate") != null))
        {
            if (entry.State == EntityState.Modified)
            {
                if (entry.Property("UpdateDate").CurrentValue == null)
                    entry.Property("UpdateDate").CurrentValue = DateTime.Now;
            }
            else if (entry.State == EntityState.Added)
            {
                if (entry.Property("CreateDate").CurrentValue == null)
                    entry.Property("CreateDate").CurrentValue = DateTime.Now;
            }
        }

        foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("LastUpdateBy") != null))
        {
            if (entry.State == EntityState.Modified)
            {
                var userid = _httpContextAccessor.HttpContext?.User?.Identity?.GetUserClaim(JWTUserClaims.UserId);

                if (entry.Property("LastUpdateBy").CurrentValue == null && !string.IsNullOrEmpty(userid))
                    entry.Property("LastUpdateBy").CurrentValue = userid;
            }

        }

        return base.SaveChangesAsync(cancellationToken);
    }
}
