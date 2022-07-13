using ImageApi.DataAccess.Base.Model.BaseEntity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ImageApi.DataAccess.Models.Primary.ContactInfo
{
    public class ContactInfo : BaseEntity
    {
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
        /// Account info foreign key
        /// </summary>
        public Guid AccountInfoId { get; set; }

        /// <summary>
        /// Account info navigation property
        /// </summary>
        public AccountInfo.AccountInfo AccountInfo { get; set; }
        #endregion
    }

    public class ContactInfoEntityTypeConfiguration : BaseEntityTypeConfiguration<ContactInfo>
    {
        public override void Configure(EntityTypeBuilder<ContactInfo> builder)
        {
            base.Configure(builder);

            builder.HasIndex(x => x.Email)
                .IsUnique();
            builder.Property(x => x.Email)
                .HasMaxLength(128)
                .IsRequired();

            builder.HasIndex(x => x.PhoneNumber)
                .IsUnique();
            builder.Property(x => x.PhoneNumber)
                .HasMaxLength(32)
                .IsRequired(false);

            builder.HasOne(x => x.AccountInfo)
                .WithOne(x => x.ContactInfo)
                .HasForeignKey<ContactInfo>(x => x.AccountInfoId);
        }
    }
}