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
        /// <summary>
        /// Document navigation property
        /// </summary>
        public ICollection<Document.Document> Documents { get; set; }

        /// <summary>
        /// Account foreign key
        /// </summary>
        public Guid AccountId { get; set; }

        /// <summary>
        /// Account navigation property
        /// </summary>
        public Account.Account Account { get; set; }

        /// <summary>
        /// Account foreign key
        /// </summary>
        public Guid SharedWithAccountId { get; set; }

        /// <summary>
        /// Account navigation property
        /// </summary>
        public Account.Account SharedWithAccount { get; set; }

        /// <summary>
        /// Share detail navigation property
        /// </summary>
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

            builder.HasOne(x => x.SharedWithAccount)
                .WithMany(x => x.Shares)
                .HasForeignKey(x => x.SharedWithAccountId);
        }
    }
}