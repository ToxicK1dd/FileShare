using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ImageApi.DataAccess.Models.Primary.Document
{
    public class Document : BaseEntity
    {
        /// <summary>
        /// Document bytes
        /// </summary>
        public byte[] Blob { get; set; }

        /// <summary>
        /// Document format, eg .png
        /// </summary>
        public string Format { get; set; }


        #region Navigation Properties
        /// <summary>
        /// User foreign key
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// User navigation property
        /// </summary>
        public User.User User { get; set; }

        /// <summary>
        /// Document details navigation property
        /// </summary>
        public ICollection<DocumentDetail.DocumentDetail> Details { get; set; }
        #endregion
    }

    public class DocumentEntityTypeConfiguration : BaseEntityTypeConfiguration<Document>
    {
        public override void Configure(EntityTypeBuilder<Document> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.Blob)
                .HasColumnType("LONGBLOB")
                .IsRequired();

            builder.Property(x => x.Format)
                .HasMaxLength(64)
                .IsRequired();

            builder.HasOne(x => x.User)
                .WithMany(x => x.Documents)
                .HasForeignKey(x => x.UserId);
        }
    }
}