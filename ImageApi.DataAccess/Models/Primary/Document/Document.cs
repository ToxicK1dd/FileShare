using ImageApi.DataAccess.Base.Model.BaseEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.Json.Nodes;

namespace ImageApi.DataAccess.Models.Primary.Document
{
    public class Document : BaseEntity
    {
        /// <summary>
        /// Document bytes
        /// </summary>
        public byte[] Blob { get; set; }

        /// <summary>
        /// Document contents in raw json format
        /// </summary>
        public JsonObject Content { get; set; }


        #region Navigation Properties
        /// <summary>
        /// Account foreign key
        /// </summary>
        public Guid AccountId { get; set; }

        /// <summary>
        /// Account navigation property
        /// </summary>
        public Account.Account Account { get; set; }

        /// <summary>
        /// Document detail navigation property
        /// </summary>
        public DocumentDetail.DocumentDetail Detail { get; set; }

        /// <summary>
        /// Document signature navigation property
        /// </summary>
        public ICollection<DocumentSignature.DocumentSignature> Signatures { get; set; }

        /// <summary>
        /// Share navigation property
        /// </summary>
        public ICollection<Share.Share> Shares { get; set; }
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

            builder.Property(x => x.Content)
                .IsRequired(false);

            builder.HasOne(x => x.Account)
                .WithMany(x => x.Documents)
                .HasForeignKey(x => x.AccountId);
        }
    }
}