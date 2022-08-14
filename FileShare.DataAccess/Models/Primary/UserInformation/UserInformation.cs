using FileShare.DataAccess.Base.Model.Entity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FileShare.DataAccess.Models.Primary.UserInformation
{
    public class UserInformation : BaseEntity
    {
        /// <summary>
        /// The firstname of the user
        /// </summary>
        public string Firstname { get; set; }

        /// <summary>
        /// All middle names of the user
        /// </summary>
        public string Middlenames { get; set; }

        /// <summary>
        /// The lastname of the user
        /// </summary>
        public string Lastname { get; set; }

        /// <summary>
        /// The date of which the user was born
        /// </summary>
        public DateTimeOffset DateOfBirth { get; set; }

        /// <summary>
        /// The country of which the user resides
        /// </summary>
        public string Nationality { get; set; }

        /// <summary>
        /// The sex of the user
        /// </summary>
        public SexType Sex { get; set; }


        #region Navigation Properties

        public Guid UserId { get; set; }

        public User.User User { get; set; }

        #endregion
    }

    public class UserInformationEntityTypeConfiguration : BaseEntityTypeConfiguration<UserInformation>
    {
        public override void Configure(EntityTypeBuilder<UserInformation> builder)
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

            builder.HasOne(x => x.User)
                .WithOne(x => x.UserInformation)
                .HasForeignKey<UserInformation>(x => x.UserId);
        }
    }

    public enum SexType
    {
        Male = 1,
        Female = 2,
        Other = 3
    }
}