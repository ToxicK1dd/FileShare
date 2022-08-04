using FileShare.DataAccess.Base.Model.BaseEntity;
using Microsoft.EntityFrameworkCore;

namespace FileShare.DataAccess.Models.Primary.VerificationCode
{
    public class VerificationCode : BaseEntity
    {
        /// <summary>
        /// The code used to verify various things
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// The type of verification code
        /// </summary>
        public VerificationCodeType Type { get; set; }


        #region Navigation Properties

        public Guid UserId { get; set; }

        public User.User User { get; set; }

        #endregion
    }

    public class VerificationCodeEntitityTypeConfiguration : BaseEntityTypeConfiguration<VerificationCode>
    {
        public override void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<VerificationCode> builder)
        {
            base.Configure(builder);

            builder.ToTable("VerificationCodes");

            builder.Property(x => x.Code)
                .HasMaxLength(128)
                .IsRequired();

            builder.HasOne(x => x.User)
                .WithMany(x => x.VerificationCodes);
        }
    }

    public enum VerificationCodeType : byte
    {
        /// <summary>
        /// The code is used to verify the account
        /// </summary>
        User = 1,
        /// <summary>
        /// The code is used to verify the phone number
        /// </summary>
        PhoneNumber = 2,
        /// <summary>
        /// The code is used to verify the email
        /// </summary>
        Email = 3
    }
}