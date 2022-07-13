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
        /// Whether or not the account has been validated using email
        /// </summary>
        public bool Validated { get; set; }


        #region Navigation Properties
        /// <summary>
        /// Account info navigation property
        /// </summary>
        public AccountInfo.AccountInfo AccountInfo { get; set; }

        /// <summary>
        /// Login navigation property
        /// </summary>
        public Login.Login Login { get; set; }

        /// <summary>
        /// Validation code navigation property
        /// </summary>
        public ValidationCode.ValidationCode ValidationCode { get; set; }

        /// <summary>
        /// Document navigation property
        /// </summary>
        public ICollection<Document.Document> Documents { get; set; }

        /// <summary>
        /// Account role navigation property
        /// </summary>
        public ICollection<AccountRole.AccountRole> Roles { get; set; }

        /// <summary>
        /// Share navigation property
        /// </summary>
        public ICollection<Share.Share> Shares { get; set; }
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

            builder.Property(x => x.Validated)
                .IsRequired();
        }
    }

    public enum AccountType
    {
        User = 1
    }
}