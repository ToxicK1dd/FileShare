using ImageApi.DataAccess.Base.Model.BaseEntity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ImageApi.DataAccess.Models.Primary.AccountInfo
{
    public class AccountInfo : BaseEntity
    {
        /// <summary>
        /// Full name of the user
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Mail for which to send emails to
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Number for which to send sms messages to
        /// </summary>
        public string PhoneNumber { get; set; }


        #region Navigation Properties
        /// <summary>
        /// Account foreign key
        /// </summary>
        public Guid AccountId { get; set; }

        /// <summary>
        /// Account navigation property
        /// </summary>
        public Account.Account Account { get; set; }
        #endregion
    }

    public class AccountInfoEntityTypeConfiguration : BaseEntityTypeConfiguration<AccountInfo>
    {
        public override void Configure(EntityTypeBuilder<AccountInfo> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.FullName)
                .HasMaxLength(128);

            builder.Property(x => x.Email)
                .HasMaxLength(128);

            builder.Property(x => x.PhoneNumber)
                .HasMaxLength(32);

            builder.HasOne(x => x.Account)
                .WithOne(x => x.AccountInfo)
                .HasForeignKey<AccountInfo>(x => x.AccountId);
        }
    }
}