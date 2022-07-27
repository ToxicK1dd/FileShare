using FileShare.DataAccess.Base.Model.BaseEntity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FileShare.DataAccess.Models.Primary.Address
{
    public class Address : BaseEntity
    {
        /// <summary>
        /// The street where the address is located
        /// </summary>
        public string Street { get; set; }

        /// <summary>
        /// Additional street for different formatting
        /// </summary>
        public string AdditionalStreet { get; set; }

        /// <summary>
        /// The area postal code
        /// </summary>
        public string PostalCode { get; set; }

        /// <summary>
        /// The city of the region
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// The region of the country
        /// </summary>
        public string Region { get; set; }

        /// <summary>
        /// The country where the address is located
        /// </summary>
        public string Country { get; set; }


        #region Navigation Properties

        public Guid AccountId { get; set; }

        public Account.Account Account { get; set; }

        #endregion
    }

    public class AddressEntityTypeConfiguration : BaseEntityTypeConfiguration<Address>
    {
        public override void Configure(EntityTypeBuilder<Address> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.Street)
                .HasMaxLength(256)
                .IsRequired();

            builder.Property(x => x.AdditionalStreet)
                .HasMaxLength(256)
                .IsRequired();

            builder.Property(x => x.PostalCode)
                .HasMaxLength(256)
                .IsRequired();

            builder.Property(x => x.City)
                .HasMaxLength(256)
                .IsRequired();

            builder.Property(x => x.Region)
                .HasMaxLength(256)
                .IsRequired();

            builder.Property(x => x.City)
                .HasMaxLength(256)
                .IsRequired();

            builder.HasOne(x => x.Account)
                .WithOne(x => x.Address)
                .HasForeignKey<Address>(x => x.AccountId);
        }
    }
}