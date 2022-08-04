using FileShare.DataAccess.Base.Model.BaseEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FileShare.DataAccess.Models.Primary.LoginDetail
{
    public class LoginDetail : BaseEntity
    {
        /// <summary>
        /// Indicates if the login attempt was successful
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Indicates what type of device attempted to login
        /// </summary>
        public DeviceType DeviceType { get; set; }

        /// <summary>
        /// The time of the login attemt
        /// </summary>
        public DateTimeOffset Time { get; set; }


        #region Navigation Properties

        public Guid LoginId { get; set; }

        public Login.Login Login { get; set; } 

        #endregion
    }

    public class LoginDetailEntityTypeConfiguration : BaseEntityTypeConfiguration<LoginDetail>
    {
        public override void Configure(EntityTypeBuilder<LoginDetail> builder)
        {
            base.Configure(builder);

            builder.ToTable("LoginDetails");

            builder.Property(x => x.Success)
                .IsRequired();

            builder.Property(x => x.DeviceType)
                .IsRequired();

            builder.Property(x => x.Time)
                .IsRequired();

            builder.HasOne(x => x.Login)
                .WithMany(x => x.LoginDetails)
                .HasForeignKey(x => x.LoginId);
        }
    }

    public enum DeviceType
    {
        Desktop = 1, // Web browser
        Mobile = 2, // Web browser
        MobileApp = 3,
    }
}