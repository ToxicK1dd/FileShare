using FileShare.DataAccess.Base.Model.BaseEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FileShare.DataAccess.Models.Primary.DeviceToken
{
    public class DeviceToken : BaseEntity
    {
        /// <summary>
        /// The device/platform type of the token
        /// </summary>
        public DeviceType DeviceType { get; set; }

        /// <summary>
        /// Device token for sending notifications
        /// </summary>
        public string Token { get; set; }


        #region Navigation Properties

        public Guid UserId { get; set; }

        public User.User User { get; set; }

        #endregion
    }

    public class DeviceTokenEntityTypeConfigration : BaseEntityTypeConfiguration<DeviceToken>
    {
        public override void Configure(EntityTypeBuilder<DeviceToken> builder)
        {
            base.Configure(builder);

            builder.ToTable("DeviceTokens");

            builder.Property(x => x.DeviceType)
                .IsRequired();

            builder.Property(x => x.Token)
                .HasMaxLength(256)
                .IsRequired();

            builder.HasOne(x => x.User)
                .WithMany(x => x.DeviceTokens)
                .HasForeignKey(x => x.UserId);
        }
    }

    public enum DeviceType
    {
        Android = 1,
        IOS = 2,
    }
}