using ImageApi.DataAccess.Base.Model.BaseEntity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ImageApi.DataAccess.Models.Primary.ContactDetail
{
    public class ContactDetail : BaseEntity
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

        public Guid AccountId { get; set; }

        public Account.Account Account { get; set; }

        #endregion
    }

    public class ContactDetailEntityTypeConfiguration : BaseEntityTypeConfiguration<ContactDetail>
    {
        public override void Configure(EntityTypeBuilder<ContactDetail> builder)
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

            builder.HasOne(x => x.Account)
                .WithOne(x => x.ContactDetail)
                .HasForeignKey<ContactDetail>(x => x.AccountId);
        }
    }
}