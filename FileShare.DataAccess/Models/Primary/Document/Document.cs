using FileShare.DataAccess.Base.Model.Entity;
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

        public Guid UserId { get; set; }

        public User.User User { get; set; }


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

            builder.ToTable("Documents");

            builder.Property(x => x.Contents)
                .HasColumnType("LONGBLOB")
                .IsRequired();

            builder.Property(e => e.Json)
                .HasConversion(
                    value => value == null ? "" : JsonConvert.SerializeObject(value),
                    value => JsonConvert.DeserializeObject<JsonObject>(value))
                .IsRequired(false);

            builder.HasOne(x => x.User)
                .WithMany(x => x.Documents)
                .HasForeignKey(x => x.UserId);
        }
    }
}