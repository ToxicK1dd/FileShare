using ImageApi.DataAccess.Base.Model.BaseEntity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ImageApi.DataAccess.Models.Primary.DocumentDetail
{
    public class DocumentDetail : BaseEntity
    {
        /// <summary>
        /// Document format, eg .png
        /// </summary>
        public string Format { get; set; }

        /// <summary>
        /// Document size in bytes
        /// </summary>
        public int ByteSize { get; set; }


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

            builder.Property(x => x.Format)
                .HasMaxLength(128)
                .IsRequired();

            builder.Property(x => x.ByteSize)
                .IsRequired();

            builder.HasOne(x => x.Document)
                .WithOne(x => x.Detail)
                .HasForeignKey<DocumentDetail>(x => x.DocumentId);
        }
    }
}