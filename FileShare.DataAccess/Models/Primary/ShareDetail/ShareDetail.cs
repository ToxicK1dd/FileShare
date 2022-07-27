using FileShare.DataAccess.Base.Model.BaseEntity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FileShare.DataAccess.Models.Primary.ShareDetail
{
    public class ShareDetail : BaseEntity
    {
        /// <summary>
        /// Which type of detail
        /// </summary>
        public DetailType Type { get; set; }

        /// <summary>
        /// Short precise description of the event
        /// </summary>
        public string Description { get; set; }


        #region Navigation Properties

        public Guid ShareId { get; set; }

        public Share.Share Share { get; set; }

        #endregion
    }

    public class ShareDetailEntityTypeConfiguration : BaseEntityTypeConfiguration<ShareDetail>
    {
        public override void Configure(EntityTypeBuilder<ShareDetail> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.Type)
                .IsRequired();

            builder.Property(x => x.Description)
                .HasMaxLength(256)
                .IsRequired();

            builder.HasOne(x => x.Share)
                .WithMany(x => x.ShareDetails)
                .HasForeignKey(x => x.ShareId);
        }
    }

    public enum DetailType
    {
        Opened = 1
    }
}