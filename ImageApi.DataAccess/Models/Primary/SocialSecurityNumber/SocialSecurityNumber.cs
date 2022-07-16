using ImageApi.DataAccess.Base.Model.BaseEntity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ImageApi.DataAccess.Models.Primary.SocialSecurityNumber
{
    public class SocialSecurityNumber : BaseEntity
    {
        /// <summary>
        /// The type of ssn, depends on country
        /// </summary>
        public SsnType Type { get; set; }

        /// <summary>
        /// The ssn itself
        /// </summary>
        public string Number { get; set; }


        #region Navigation Properties

        public Guid AccountId { get; set; }

        public Account.Account Account { get; set; }

        #endregion
    }

    public class SocialSecurityNumberEntityTypeConfiguration : BaseEntityTypeConfiguration<SocialSecurityNumber>
    {
        public override void Configure(EntityTypeBuilder<SocialSecurityNumber> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.Type)
                .IsRequired();

            builder.Property(x => x.Number)
                .HasMaxLength(128)
                .IsRequired();

            builder.HasOne(x => x.Account)
                .WithMany(x => x.SocialSecurityNumbers)
                .HasForeignKey(x => x.AccountId);
        }
    }

    public enum SsnType
    {
        CPR = 1 // Denmark
    }
}