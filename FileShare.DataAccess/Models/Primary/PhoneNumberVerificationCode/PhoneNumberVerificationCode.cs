using FileShare.DataAccess.Base.Model.BaseEntity;

namespace FileShare.DataAccess.Models.Primary.PhoneNumberVerificationCode
{
    public class PhoneNumberVerificationCode : BaseEntity
    {
        /// <summary>
        /// The code used to verify a phone number
        /// </summary>
        public string Code { get; set; }


        #region Navigation Properties

        public Guid PhoneNumberId { get; set; }

        public PhoneNumber.PhoneNumber PhoneNumber { get; set; } 


        public Guid AccountId { get; set; }

        public Account.Account Account { get; set; }

        #endregion
    }

    public class PhoneNumberVerificationCodeEntitityTypeConfiguration : BaseEntityTypeConfiguration<PhoneNumberVerificationCode>
    {
        public override void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<PhoneNumberVerificationCode> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.Code)
                .HasMaxLength(128)
                .IsRequired();

            builder.HasOne(x => x.PhoneNumber)
                .WithOne(x => x.PhoneNumberVerificationCode)
                .HasForeignKey<PhoneNumberVerificationCode>(x => x.PhoneNumberId);

            builder.HasOne(x => x.Account)
                .WithOne(x => x.PhoneNumberVerificationCode)
                .HasForeignKey<PhoneNumberVerificationCode>(x => x.AccountId);
        }
    }
}