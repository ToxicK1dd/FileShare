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
        /// <summary>
        /// Account info foreign key
        /// </summary>
        public Guid AccountInfoId { get; set; }

        /// <summary>
        /// Account info navigation propertu
        /// </summary>
        public AccountInfo.AccountInfo AccountInfo { get; set; }
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

            builder.HasOne(x => x.AccountInfo)
                .WithMany(x => x.SocialSecurityNumbers)
                .HasForeignKey(x => x.AccountInfoId);
        }
    }

    public enum SsnType
    {
        CPR = 1 // Denmark
    }
}