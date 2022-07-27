using FileShare.DataAccess.Base.Model.BaseEntity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FileShare.DataAccess.Models.Primary.EmailVerificationCode
{
    public class EmailVerificationCode : BaseEntity
    {
        /// <summary>
        /// The code used to verify an email address
        /// </summary>
        public string Code { get; set; }


        #region Navigation Properties

        public Guid EmailId { get; set; }

        public Email.Email Email { get; set; }


        public Guid AccountId { get; set; }

        public Account.Account Account { get; set; }

        #endregion
    }

    public class EmailVerificationCodeEntityTypeConfiguration : BaseEntityTypeConfiguration<EmailVerificationCode>
    {
        public override void Configure(EntityTypeBuilder<EmailVerificationCode> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.Code)
                .HasMaxLength(128)
                .IsRequired();

            builder.HasOne(x => x.Email)
                .WithOne(x => x.EmailVerificationCode)
                .HasForeignKey<EmailVerificationCode>(x => x.EmailId);

            builder.HasOne(x => x.Account)
                .WithOne(x => x.EmailVerificationCode)
                .HasForeignKey<EmailVerificationCode>(x => x.AccountId);
        }
    }
}