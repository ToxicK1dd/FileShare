using ImageApi.DataAccess.Base.Model.BaseEntity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ImageApi.DataAccess.Models.Primary.Share
{
    public class Share : BaseEntity
    {
        /// <summary>
        /// Date of which the access to the documents expires
        /// </summary>
        public DateTimeOffset Expiration { get; set; }


        #region Navigation Properties

        public Guid AccountId { get; set; }

        public Account.Account Account { get; set; }


        public ICollection<Document.Document> Documents { get; set; }

        public ICollection<ShareDetail.ShareDetail> ShareDetails { get; set; }

        #endregion
    }

    public class ShareEntityTypeConfiguration : BaseEntityTypeConfiguration<Share>
    {
        public override void Configure(EntityTypeBuilder<Share> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.Expiration)
                .IsRequired();

            builder.HasMany(x => x.Documents)
                .WithMany(x => x.Shares);

            builder.HasOne(x => x.Account)
                .WithMany(x => x.Shares)
                .HasForeignKey(x => x.AccountId);
        }
    }
}