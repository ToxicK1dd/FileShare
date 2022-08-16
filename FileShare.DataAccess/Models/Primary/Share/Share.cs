using FileShare.DataAccess.Base.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FileShare.DataAccess.Models.Primary.Share
{
    public class Share : BaseEntity
    {
        /// <summary>
        /// Date of which the access to the documents expires
        /// </summary>
        public DateTimeOffset Expires { get; set; }

        /// <summary>
        /// Is the token expired
        /// </summary>
        public bool IsExpired { get => Expires < DateTimeOffset.UtcNow; }

        
        #region Navigation Properties

        public Guid UserId { get; set; }

        public User.User User { get; set; }


        public ICollection<Document.Document> Documents { get; set; }

        public ICollection<ShareDetail.ShareDetail> ShareDetails { get; set; }

        #endregion
    }

    public class ShareEntityTypeConfiguration : BaseEntityTypeConfiguration<Share>
    {
        public override void Configure(EntityTypeBuilder<Share> builder)
        {
            base.Configure(builder);

            builder.ToTable("Shares");

            builder.Property(x => x.Expires)
                .IsRequired();

            builder.Ignore(x => x.IsExpired);

            builder.HasMany(x => x.Documents)
                .WithMany(x => x.Shares);

            builder.HasOne(x => x.User)
                .WithMany(x => x.Shares)
                .HasForeignKey(x => x.UserId);
        }
    }
}