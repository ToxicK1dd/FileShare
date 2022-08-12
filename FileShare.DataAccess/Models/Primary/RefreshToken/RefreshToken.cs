using FileShare.DataAccess.Base.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FileShare.DataAccess.Models.Primary.RefreshToken
{
    public class RefreshToken : BaseEntity
    {
        /// <summary>
        /// Token used for optaining a new access token
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// When the token expires
        /// </summary>
        public DateTimeOffset Expires { get; set; }

        /// <summary>
        /// Is the token expired
        /// </summary>
        public bool IsExpired { get => Expires < DateTimeOffset.UtcNow; }

        /// <summary>
        /// When the token was revoked
        /// </summary>
        public DateTimeOffset Revoked { get; set; }

        /// <summary>
        /// Has the token been revoked
        /// </summary>
        public bool IsRevoked { get; set; }

        #region Navigation Properties

        public Guid UserId { get; set; }

        public User.User User { get; set; }

        #endregion
    }

    public class RefreshTokenEntityTypeConfiguration : BaseEntityTypeConfiguration<RefreshToken>
    {
        public override void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            base.Configure(builder);

            builder.ToTable("RefreshTokens");

            builder.Property(x => x.Token)
                .HasMaxLength(512)
                .IsRequired();

            builder.Property(x => x.Expires)
                .IsRequired();

            builder.Ignore(x => x.IsExpired);

            builder.HasOne(x => x.User)
                .WithMany(x => x.RefreshTokens)
                .HasForeignKey(x => x.UserId);
        }
    }
}