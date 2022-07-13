using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ImageApi.DataAccess.Models.Primary.Account
{
    public class Account : BaseEntity
    {
        /// <summary>
        /// Toggles the ability to authenticate
        /// </summary>
        public bool Enabled { get; set; }


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