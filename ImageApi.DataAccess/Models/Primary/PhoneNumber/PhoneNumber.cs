using ImageApi.DataAccess.Base.Model.BaseEntity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ImageApi.DataAccess.Models.Primary.PhoneNumber
{
    public class PhoneNumber : BaseEntity
    {
        /// <summary>
        /// The number of the phone 
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// Has the number been verified by the user
        /// </summary>
        public bool Verified { get; set; }


        #region Navigation Properties
        
        public Guid AccountId { get; set; }

        public Account.Account Account { get; set; }


        public PhoneNumberVerificationCode.PhoneNumberVerificationCode PhoneNumberVerificationCode { get; set; }

        #endregion
    }

    public class PhoneNumberEntityTypeConfiguration : BaseEntityTypeConfiguration<PhoneNumber>
    {
        public override void Configure(EntityTypeBuilder<PhoneNumber> builder)
        {
            base.Configure(builder);

            builder.HasIndex(x => x.Number)
                .IsUnique();
            builder.Property(x => x.Number)
                .HasMaxLength(256)
                .IsRequired();

            builder.Property(x => x.Verified)
                .IsRequired();

            builder.HasOne(x => x.Account)
                .WithOne(x => x.PhoneNumber)
                .HasForeignKey<PhoneNumber>(x => x.AccountId);
        }
    }
}