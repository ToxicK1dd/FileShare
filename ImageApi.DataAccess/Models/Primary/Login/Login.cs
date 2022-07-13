using ImageApi.DataAccess.Base.Model.BaseEntity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ImageApi.DataAccess.Models.Primary.Login
{
    public class Login : BaseEntity
    {
        /// <summary>
        /// Name used for authentication
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Password used for authentication
        /// </summary>
        public string Password { get; set; }


        #region Navigation Properties
        /// <summary>
        /// Account foreign key
        /// </summary>
        public Guid AccountId { get; set; }

        /// <summary>
        /// Account navigation property
        /// </summary>
        public Account.Account Account { get; set; }

        /// <summary>
        /// Login details navigation property
        /// </summary>
        public ICollection<LoginDetail.LoginDetail> LoginDetails { get; set; } 

        /// <summary>
        /// Refresh token navigation properties
        /// </summary>
        public ICollection<RefreshToken.RefreshToken> RefreshTokens { get; set; }
        #endregion
    }

    public class LoginEntityTypeConfiguration : BaseEntityTypeConfiguration<Login>
    {
        public override void Configure(EntityTypeBuilder<Login> builder)
        {
            base.Configure(builder);

            builder.HasIndex(x => x.Username)
                .IsUnique();
            builder.Property(x => x.Username)
                .HasMaxLength(64)
                .IsRequired();

            builder.Property(x => x.Password)
                .HasMaxLength(128)
                .IsRequired();

            builder.HasOne(x => x.Account)
                .WithOne(x => x.Login)
                .HasForeignKey<Login>(x => x.AccountId);
        }
    }
}