using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace ImageApi.DataAccess.Models
{
    /// <summary>
    /// Base entity type configuration for database models.
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    public abstract class BaseEntityTypeConfiguration<TModel> : IEntityTypeConfiguration<TModel>
        where TModel : BaseEntity
    {
        public virtual void Configure(EntityTypeBuilder<TModel> builder)
        {
            // Configure primary key, and index
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.Id).IsUnique();
        }
    }
}