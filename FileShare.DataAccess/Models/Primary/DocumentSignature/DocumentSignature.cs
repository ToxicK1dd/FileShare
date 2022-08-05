using FileShare.DataAccess.Base.Model.BaseEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FileShare.DataAccess.Models.Primary.DocumentSignature
{
    public class DocumentSignature : BaseEntity
    {
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

    public class DocumentSignatureEntityTypeConfiguration : BaseEntityTypeConfiguration<DocumentSignature>
    {
        public override void Configure(EntityTypeBuilder<DocumentSignature> builder)
        {
            base.Configure(builder);

            builder.ToTable("DocumentSignatures");

            builder.HasOne(x => x.Document)
                .WithMany(x => x.Signatures)
                .HasForeignKey(x => x.DocumentId);
        }
    }
}