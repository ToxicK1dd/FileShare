using ImageApi.DataAccess.Base.Model.BaseEntity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ImageApi.DataAccess.Models.Primary.User
{
    public class User : BaseEntity
    {
        /// <summary>
        /// The firstname of the account
        /// </summary>
        public string Firstname { get; set; }

        /// <summary>
        /// All middle names of the account
        /// </summary>
        public string Middlenames { get; set; }

        /// <summary>
        /// The lastname of the account
        /// </summary>
        public string Lastname { get; set; }

        /// <summary>
        /// The date of which the account was born
        /// </summary>
        public DateTimeOffset DateOfBirth { get; set; }

        /// <summary>
        /// The country of which the account resides
        /// </summary>
        public string Nationality { get; set; }

        /// <summary>
        /// The sex of the account
        /// </summary>
        public SexType Sex { get; set; }


        #region Navigation Properties

        public Guid AccountId { get; set; }

        public Account.Account Account { get; set; }

        #endregion
    }

    public class UserEntityTypeConfiguration : BaseEntityTypeConfiguration<User>
    {
        public override void Configure(EntityTypeBuilder<User> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.Firstname)
                .HasMaxLength(64)
                .IsRequired(false);

            builder.Property(x => x.Middlenames)
                .HasMaxLength(768)
                .IsRequired(false);

            builder.Property(x => x.Lastname)
                .HasMaxLength(64)
                .IsRequired(false);

            builder.Property(x => x.DateOfBirth)
                .IsRequired();

            builder.Property(x => x.Nationality)
                .HasMaxLength(128)
                .IsRequired(false);

            builder.Property(x => x.Sex)
                .IsRequired();

            builder.HasOne(x => x.Account)
                .WithOne(x => x.User)
                .HasForeignKey<User>(x => x.AccountId);
        }
    }

    public enum SexType
    {
        Male = 1,
        Female = 2,
        Other = 3
    }
}