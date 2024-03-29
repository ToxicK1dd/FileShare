﻿using FileShare.DataAccess.Base.Model.IdentityUser;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FileShare.DataAccess.Models.Primary.User
{
    public class User : BaseIdentityUser
    {
        public bool IsEnabled { get; set; }

        public bool IsVerified { get; set; }


        #region Navigation Properties

        public ICollection<Document.Document> Documents { get; set; }

        public ICollection<Share.Share> Shares { get; set; }

        public ICollection<VerificationCode.VerificationCode> VerificationCodes { get; set; }

        public ICollection<RefreshToken.RefreshToken> RefreshTokens { get; set; }

        public ICollection<LoginAttempt.LoginAttempt> LoginAttempts { get; set; }


        public UserInformation.UserInformation UserInformation { get; set; }

        #endregion
    }

    public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable(name: "User");

            builder.HasIndex(x => x.UserName).IsUnique();
            builder.HasIndex(x => x.NormalizedUserName).IsUnique();
            builder.HasIndex(x => x.Email).IsUnique();
            builder.HasIndex(x => x.NormalizedEmail).IsUnique();
            builder.HasIndex(x => x.PhoneNumber).IsUnique();
        }
    }
}