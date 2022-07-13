using ImageApi.DataAccess.Base.Model.BaseEntity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ImageApi.DataAccess.Models.Primary.AccountInfo
{
    public class AccountInfo : BaseEntity
    {
        /// <summary>
        /// The firstname of the account
        /// </summary>
        public string Firstname { get; set; }

        /// <summary>
        /// All middle names of the account
        /// </summary>
        public string MiddleNames { get; set; }

        /// <summary>
        /// The lastname of the account
        /// </summary>
        public string Lastname { get; set; }

        /// <summary>
        /// The date of which the account was born
        /// </summary>
        public DateTimeOffset DateOfBirth { get; set; }

        /// <summary>
        /// The country of which the account resides
        /// </summary>
        public string Nationality { get; set; }


        #region Navigation Properties
        /// <summary>
        /// Account foreign key
        /// </summary>
        public Guid AccountId { get; set; }

        /// <summary>
        /// Account navigation property
        /// </summary>
        public Account.Account Account { get; set; }

        /// <summary>
        /// Contact info navigation property
        /// </summary>
        public ContactInfo.ContactInfo ContactInfo { get; set; }

        /// <summary>
        /// Social security number navigation property
        /// </summary>
        public ICollection<SocialSecurityNumber.SocialSecurityNumber> SocialSecurityNumbers { get; set; } 

        /// <summary>
        /// Address navigation property
        /// </summary>
        public ICollection<Address.Address> Addresses { get; set; }
        #endregion
    }

    public class AccountInfoEntityTypeConfiguration : BaseEntityTypeConfiguration<AccountInfo>
    {
        public override void Configure(EntityTypeBuilder<AccountInfo> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.Firstname)
                .HasMaxLength(64)
                .IsRequired(false);

            builder.Property(x => x.MiddleNames)
                .HasMaxLength(768)
                .IsRequired(false);

            builder.Property(x => x.Lastname)
                .HasMaxLength(64)
                .IsRequired(false);

            builder.Property(x => x.DateOfBirth)
                .IsRequired();

            builder.Property(x => x.Nationality)
                .HasMaxLength(128)
                .IsRequired(false);

            builder.HasOne(x => x.Account)
                .WithOne(x => x.AccountInfo)
                .HasForeignKey<AccountInfo>(x => x.AccountId);
        }
    }
}