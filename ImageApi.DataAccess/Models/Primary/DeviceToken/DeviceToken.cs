using ImageApi.DataAccess.Base.Model.BaseEntity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ImageApi.DataAccess.Models.Primary.DeviceToken
{
    public class DeviceToken : BaseEntity
    {
        /// <summary>
        /// The device/platform type of the token
        /// </summary>
        public DeviceType Type { get; set; }

        /// <summary>
        /// Device token for sending notifications
        /// </summary>
        public string Token { get; set; }


        #region Navigation Properties

        public Guid LoginId { get; set; }

        public Login.Login Login { get; set; }
    
        #endregion
    }

    public class DeviceTokenEntityTypeConfigration : BaseEntityTypeConfiguration<DeviceToken>
    {
        public override void Configure(EntityTypeBuilder<DeviceToken> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.Type)
                .IsRequired();

            builder.Property(x => x.Token)
                .HasMaxLength(256)
                .IsRequired();

            builder.HasOne(x => x.Login)
                .WithMany(x => x.DeviceTokens)
                .HasForeignKey(x => x.LoginId);
        }
    }

    public enum DeviceType
    {
        Android = 1,
        IOS = 2,
    }
}