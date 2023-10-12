using Architecture.Application.Domain.DbContexts.Domains.Base;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Architecture.Infra.Data.Context.Configuration.Base;

public class BaseEntitLastUpdateByConfiguration<TEntity> : BaseEntityConfiguration<TEntity> where TEntity : BaseEntityLastUpdateBy<TEntity>
{
    public override void Configure(EntityTypeBuilder<TEntity> builder)
    {
        base.Configure(builder);
        builder.Property(u => u.LastUpdateBy).HasMaxLength(50);
    }
}
