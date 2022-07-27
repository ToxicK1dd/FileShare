using FileShare.DataAccess.Base.Model.BaseEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;
using System.Text.Json.Nodes;

namespace FileShare.DataAccess.Models.Primary.Document
{
    public class Document : BaseEntity
    {
        /// <summary>
        /// Document in bytes
        /// </summary>
        public byte[] Contents { get; set; }

        /// <summary>
        /// Contents in json format
        /// </summary>
        public JsonObject Json { get; set; }


        #region Navigation Properties

        public Guid AccountId { get; set; }

        public Account.Account Account { get; set; }


        public DocumentDetail.DocumentDetail Detail { get; set; }

        public ICollection<DocumentSignature.DocumentSignature> Signatures { get; set; }

        public ICollection<Share.Share> Shares { get; set; }
        #endregion
    }

    public class DocumentEntityTypeConfiguration : BaseEntityTypeConfiguration<Document>
    {
        public override void Configure(EntityTypeBuilder<Document> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.Contents)
                .HasColumnType("LONGBLOB")
                .IsRequired();

            builder.Property(e => e.Json)
                .HasConversion(
                    value => value == null ? "" : JsonConvert.SerializeObject(value),
                    value => JsonConvert.DeserializeObject<JsonObject>(value))
                .IsRequired(false);

            builder.HasOne(x => x.Account)
                .WithMany(x => x.Documents)
                .HasForeignKey(x => x.AccountId);
        }
    }
}