using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata;

namespace FileShare.DataAccess.Base.Model.Entity
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

            // Prevent property from being changed once set
            builder.Property(x => x.Created)
                .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);

            // Soft delete query filter
            builder.HasQueryFilter(x => !x.IsDeleted);
        }
    }
}