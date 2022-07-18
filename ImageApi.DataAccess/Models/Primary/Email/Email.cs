using ImageApi.DataAccess.Base.Model.BaseEntity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ImageApi.DataAccess.Models.Primary.Email
{
    public class Email : BaseEntity
    {
        /// <summary>
        /// The address of the email
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Has the email been verified by the user
        /// </summary>
        public bool Verified { get; set; }


        #region Navigation Properties

        public Guid AccountId { get; set; }

        public Account.Account Account { get; set; }


        public EmailVerificationCode.EmailVerificationCode EmailVerificationCode { get; set; }

        #endregion
    }

    public class EmailEntityTypeConfiguration : BaseEntityTypeConfiguration<Email>
    {
        public override void Configure(EntityTypeBuilder<Email> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.Address)
                .HasMaxLength(256)
                .IsRequired();

            builder.Property(x => x.Verified)
                .IsRequired();

            builder.HasOne(x => x.Account)
                .WithOne(x => x.Email)
                .HasForeignKey<Email>(x => x.AccountId);
        }
    }
}