using FileShare.DataAccess.Base.Model.BaseEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FileShare.DataAccess.Models.Primary.LoginAttempt
{
    public class LoginAttempt : BaseEntity
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

        public Guid UserId { get; set; }

        public User.User User { get; set; }

        #endregion
    }

    public class LoginAttemptEntityTypeConfiguration : BaseEntityTypeConfiguration<LoginAttempt>
    {
        public override void Configure(EntityTypeBuilder<LoginAttempt> builder)
        {
            base.Configure(builder);

            builder.ToTable("LoginAttempts");

            builder.Property(x => x.Success)
                .IsRequired();

            builder.Property(x => x.DeviceType)
                .IsRequired();

            builder.Property(x => x.Time)
                .IsRequired();

            builder.HasOne(x => x.User)
                .WithMany(x => x.LoginAttempts)
                .HasForeignKey(x => x.UserId);
        }
    }

    public enum DeviceType
    {
        Desktop = 1, // Web browser
        Mobile = 2, // Web browser
        MobileApp = 3,
    }
}