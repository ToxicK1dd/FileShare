using FileShare.DataAccess.Base.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FileShare.DataAccess.Models.Primary.DocumentDetail
{
    public class DocumentDetail : BaseEntity
    {
        /// <summary>
        /// Document name, eg image.png
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Document format, eg .png
        /// </summary>
        public string Extention { get; set; }

        /// <summary>
        /// Document MIME type, eg image/png
        /// </summary>
        public string ContentType { get; set; }

        /// <summary>
        /// Document size in bytes
        /// </summary>
        public long Length { get; set; }


        #region Navigation Properties

        public Guid DocumentId { get; set; }

        public Document.Document Document { get; set; }

        #endregion
    }

    public class DocumentDetailEntityTypeConfiguration : BaseEntityTypeConfiguration<DocumentDetail>
    {
        public override void Configure(EntityTypeBuilder<DocumentDetail> builder)
        {
            base.Configure(builder);

            builder.ToTable("DocumentDetails");

            builder.Property(x => x.FileName)
                .HasMaxLength(256)
                .IsRequired();

            builder.Property(x => x.Extention)
                .HasMaxLength(128)
                .IsRequired();

            builder.Property(x => x.ContentType)
                .HasMaxLength(128)
                .IsRequired();

            builder.Property(x => x.Length)
                .IsRequired();

            builder.HasOne(x => x.Document)
                .WithOne(x => x.Detail)
                .HasForeignKey<DocumentDetail>(x => x.DocumentId);
        }
    }
}