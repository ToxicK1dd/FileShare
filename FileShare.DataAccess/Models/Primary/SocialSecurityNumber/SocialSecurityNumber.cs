using FileShare.DataAccess.Base.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FileShare.DataAccess.Models.Primary.SocialSecurityNumber
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

        public Guid UserId { get; set; }

        public User.User User { get; set; }

        #endregion
    }

    public class SocialSecurityNumberEntityTypeConfiguration : BaseEntityTypeConfiguration<SocialSecurityNumber>
    {
        public override void Configure(EntityTypeBuilder<SocialSecurityNumber> builder)
        {
            base.Configure(builder);

            builder.ToTable("SocialSecurityNumbers");

            builder.Property(x => x.Type)
                .IsRequired();

            builder.Property(x => x.Number)
                .HasMaxLength(128)
                .IsRequired();

            builder.HasOne(x => x.User)
                .WithMany(x => x.SocialSecurityNumbers)
                .HasForeignKey(x => x.UserId);
        }
    }

    public enum SsnType
    {
        CPR = 1 // Denmark
    }
}