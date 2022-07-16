using ImageApi.DataAccess.Base.Model.BaseEntity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ImageApi.DataAccess.Models.Primary.VerificationCode
{
    public class VerificationCode : BaseEntity
    {
        /// <summary>
        /// The code used for validating an account
        /// </summary>
        public string Code { get; set; }


        #region Navigation Properties

        public Guid AccountId { get; set; }

        public Account.Account Account { get; set; }

        #endregion
    }

    public class VerificationCodeEntityTypeConfiguration : BaseEntityTypeConfiguration<VerificationCode>
    {
        public override void Configure(EntityTypeBuilder<VerificationCode> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.Code)
                .HasMaxLength(16)
                .IsRequired();

            builder.HasOne(x => x.Account)
                .WithOne(x => x.VerificationCode)
                .HasForeignKey<VerificationCode>(x => x.AccountId);
        }
    }
}