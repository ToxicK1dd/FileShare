using ImageApi.DataAccess.Base.Model.BaseEntity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ImageApi.DataAccess.Models.Primary.DocumentDetail
{
    public class DocumentDetail : BaseEntity
    {
        /// <summary>
        /// The time of when the document was accessed
        /// </summary>
        public DateTimeOffset Accessed { get; set; }

        /// <summary>
        /// The user who accessed the document
        /// </summary>
        public Guid AccessedBy { get; set; }


        #region Navigation Properties
        /// <summary>
        /// Document foreign key
        /// </summary>
        public Guid DocumentId { get; set; }

        /// <summary>
        /// Document navigation property
        /// </summary>
        public Document.Document Document { get; set; } 
        #endregion
    }

    public class DocumentDetailEntityTypeConfiguration : BaseEntityTypeConfiguration<DocumentDetail>
    {
        public override void Configure(EntityTypeBuilder<DocumentDetail> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.Accessed)
                .IsRequired();

            builder.Property(x => x.AccessedBy)
                .IsRequired();

            builder.HasOne(x => x.Document)
                .WithMany(x => x.Details)
                .HasForeignKey(x => x.DocumentId);
        }
    }
}