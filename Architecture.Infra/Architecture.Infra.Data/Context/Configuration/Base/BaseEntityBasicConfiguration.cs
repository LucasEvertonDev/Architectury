using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Architecture.Application.Domain.DbContexts.Domains.Base;

namespace Architecture.Infra.Data.Context.Configuration.Base;

public class BaseEntityWithDatesConfiguration<TEntity> : BaseEntityConfiguration<TEntity> where TEntity : BaseEntityWithDates<TEntity>
{
    public override void Configure(EntityTypeBuilder<TEntity> builder)  
    {
        builder.Property(u => u.CreateDate);
    }
}

