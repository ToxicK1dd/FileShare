using ImageApi.DataAccess.Base.Model.BaseEntity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ImageApi.DataAccess.Models.Primary.ValidationCode
{
    public class ValidationCode : BaseEntity
    {
        /// <summary>
        /// The code used for validating an account
        /// </summary>
        public string Code { get; set; }


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

    public class ValidationCodeEntityTypeConfiguration : BaseEntityTypeConfiguration<ValidationCode>
    {
        public override void Configure(EntityTypeBuilder<ValidationCode> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.Code)
                .HasMaxLength(16)
                .IsRequired();

            builder.HasOne(x => x.Account)
                .WithOne(x => x.ValidationCode)
                .HasForeignKey<ValidationCode>(x => x.AccountId);
        }
    }
}