using ImageApi.DataAccess.Base.Model.BaseEntity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ImageApi.DataAccess.Models.Primary.Account
{
    public class Account : BaseEntity
    {
        /// <summary>
        /// The type of account
        /// </summary>
        public AccountType Type { get; set; }

        /// <summary>
        /// Toggles the ability to authenticate
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// Whether or not the account has been verified to be a real person
        /// </summary>
        public bool Verified { get; set; }


        #region Navigation Properties

        public ICollection<Document.Document> Documents { get; set; }

        public ICollection<Share.Share> Shares { get; set; }

        public ICollection<SocialSecurityNumber.SocialSecurityNumber> SocialSecurityNumbers { get; set; }


        public Address.Address Address { get; set; }

        public Email.Email Email { get; set; }

        public EmailVerificationCode.EmailVerificationCode EmailVerificationCode { get; set; }

        public Login.Login Login { get; set; }

        public PhoneNumber.PhoneNumber PhoneNumber { get; set; }

        public PhoneNumberVerificationCode.PhoneNumberVerificationCode PhoneNumberVerificationCode { get; set; }

        public User.User User { get; set; }

        #endregion
    }

    public class AccountEntityTypeConfiguration : BaseEntityTypeConfiguration<Account>
    {
        public override void Configure(EntityTypeBuilder<Account> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.Type)
                .IsRequired();

            builder.Property(x => x.Enabled)
                .IsRequired();

            builder.Property(x => x.Verified)
                .IsRequired();
        }
    }

    public enum AccountType
    {
        User = 1
    }
}