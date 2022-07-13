using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ImageApi.DataAccess.Models.Primary.Account
{
    public class Account : BaseEntity
    {
        /// <summary>
        /// What type of account this is
        /// </summary>
        public AccountType Type { get; set; }

        /// <summary>
        /// Toggles the ability to authenticate
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// Mail used for sending messages
        /// </summary>
        public string Email { get; set; }


        #region Navigation Properties
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

            builder.Property(x => x.Type)
                .IsRequired();

            builder.Property(x => x.Enabled)
                .IsRequired();

            builder.Property(x => x.Email)
                .HasMaxLength(256)
                .IsRequired();
        }
    }

    public enum AccountType
    {
        Admin = 1,
        User = 2,
    }
}