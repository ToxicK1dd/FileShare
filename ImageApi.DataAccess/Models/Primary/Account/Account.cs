using ImageApi.DataAccess.Base.Model.BaseEntity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ImageApi.DataAccess.Models.Primary.Account
{
    public class Account : BaseEntity
    {
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
        /// Admin navigation property
        /// </summary>
        public Admin.Admin Admin { get; set; }

        /// <summary>
        /// User navigation property
        /// </summary>
        public User.User User { get; set; }

        /// <summary>
        /// Login navigation property
        /// </summary>
        public Login.Login Login { get; set; }

        /// <summary>
        /// Validation code navigation property
        /// </summary>
        public ValidationCode.ValidationCode ValidationCode { get; set; }
        #endregion
    }

    public class AccountEntityTypeConfiguration : BaseEntityTypeConfiguration<Account>
    {
        public override void Configure(EntityTypeBuilder<Account> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.Enabled)
                .IsRequired();
        }
    }
}