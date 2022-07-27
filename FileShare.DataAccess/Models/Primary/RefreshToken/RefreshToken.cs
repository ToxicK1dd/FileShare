using FileShare.DataAccess.Base.Model.BaseEntity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FileShare.DataAccess.Models.Primary.RefreshToken
{
    public class RefreshToken : BaseEntity
    {
        /// <summary>
        /// Token used for optaining a new JWT
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// When the token expires
        /// </summary>
        public DateTimeOffset Expiration { get; set; }


        #region Navigation Properties

        public Guid LoginId { get; set; }

        public Login.Login Login { get; set; } 

        #endregion
    }

    public class RefreshTokenEntityTypeConfiguration : BaseEntityTypeConfiguration<RefreshToken>
    {
        public override void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.Token)
                .HasMaxLength(512)
                .IsRequired();

            builder.Property(x => x.Expiration)
                .IsRequired();

            builder.HasOne(x => x.Login)
                .WithMany(x => x.RefreshTokens)
                .HasForeignKey(x => x.LoginId);
        }
    }
}