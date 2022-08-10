﻿// <auto-generated />
using System;
using FileShare.DataAccess.Models.Primary;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FileShare.DataAccess.Migrations.Primary
{
    [DbContext(typeof(PrimaryContext))]
    partial class PrimaryContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("Identity")
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("DocumentShare", b =>
                {
                    b.Property<Guid>("DocumentsId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("SharesId")
                        .HasColumnType("char(36)");

                    b.HasKey("DocumentsId", "SharesId");

                    b.HasIndex("SharesId");

                    b.ToTable("DocumentShare", "Identity");
                });

            modelBuilder.Entity("FileShare.DataAccess.Models.Primary.Address.Address", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("AdditionalStreet")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<DateTimeOffset>("Changed")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("Country")
                        .HasColumnType("longtext");

                    b.Property<DateTimeOffset>("Created")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTimeOffset>("Deleted")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("PostalCode")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("Region")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<DateTimeOffset>("Retrieved")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Address", "Identity");
                });

            modelBuilder.Entity("FileShare.DataAccess.Models.Primary.DeviceToken.DeviceToken", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTimeOffset>("Changed")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTimeOffset>("Created")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTimeOffset>("Deleted")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("DeviceType")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTimeOffset>("Retrieved")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.HasIndex("UserId");

                    b.ToTable("DeviceTokens", "Identity");
                });

            modelBuilder.Entity("FileShare.DataAccess.Models.Primary.Document.Document", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTimeOffset>("Changed")
                        .HasColumnType("datetime(6)");

                    b.Property<byte[]>("Contents")
                        .IsRequired()
                        .HasColumnType("LONGBLOB");

                    b.Property<DateTimeOffset>("Created")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTimeOffset>("Deleted")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Json")
                        .HasColumnType("longtext");

                    b.Property<DateTimeOffset>("Retrieved")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.HasIndex("UserId");

                    b.ToTable("Documents", "Identity");
                });

            modelBuilder.Entity("FileShare.DataAccess.Models.Primary.DocumentDetail.DocumentDetail", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTimeOffset>("Changed")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("ContentType")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<DateTimeOffset>("Created")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTimeOffset>("Deleted")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("DocumentId")
                        .HasColumnType("char(36)");

                    b.Property<string>("Extention")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<long>("Length")
                        .HasColumnType("bigint");

                    b.Property<DateTimeOffset>("Retrieved")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("DocumentId")
                        .IsUnique();

                    b.HasIndex("Id")
                        .IsUnique();

                    b.ToTable("DocumentDetails", "Identity");
                });

            modelBuilder.Entity("FileShare.DataAccess.Models.Primary.DocumentSignature.DocumentSignature", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTimeOffset>("Changed")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTimeOffset>("Created")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTimeOffset>("Deleted")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("DocumentId")
                        .HasColumnType("char(36)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTimeOffset>("Retrieved")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("DocumentId");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.ToTable("DocumentSignatures", "Identity");
                });

            modelBuilder.Entity("FileShare.DataAccess.Models.Primary.LoginAttempt.LoginAttempt", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTimeOffset>("Changed")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTimeOffset>("Created")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTimeOffset>("Deleted")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("DeviceType")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsSuccessful")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTimeOffset>("Retrieved")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTimeOffset>("Time")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.HasIndex("UserId");

                    b.ToTable("LoginAttempts", "Identity");
                });

            modelBuilder.Entity("FileShare.DataAccess.Models.Primary.RefreshToken.RefreshToken", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTimeOffset>("Changed")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTimeOffset>("Created")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTimeOffset>("Deleted")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTimeOffset>("Expires")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTimeOffset>("Retrieved")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasMaxLength(512)
                        .HasColumnType("varchar(512)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.HasIndex("UserId");

                    b.ToTable("RefreshTokens", "Identity");
                });

            modelBuilder.Entity("FileShare.DataAccess.Models.Primary.Share.Share", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTimeOffset>("Changed")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTimeOffset>("Created")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTimeOffset>("Deleted")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTimeOffset>("Expires")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTimeOffset>("Retrieved")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.HasIndex("UserId");

                    b.ToTable("Shares", "Identity");
                });

            modelBuilder.Entity("FileShare.DataAccess.Models.Primary.ShareDetail.ShareDetail", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTimeOffset>("Changed")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTimeOffset>("Created")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTimeOffset>("Deleted")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTimeOffset>("Retrieved")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("ShareId")
                        .HasColumnType("char(36)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.HasIndex("ShareId");

                    b.ToTable("ShareDetails", "Identity");
                });

            modelBuilder.Entity("FileShare.DataAccess.Models.Primary.SocialSecurityNumber.SocialSecurityNumber", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTimeOffset>("Changed")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTimeOffset>("Created")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTimeOffset>("Deleted")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<DateTimeOffset>("Retrieved")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.HasIndex("UserId");

                    b.ToTable("SocialSecurityNumbers", "Identity");
                });

            modelBuilder.Entity("FileShare.DataAccess.Models.Primary.User.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset>("Changed")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<DateTimeOffset>("Created")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTimeOffset>("Deleted")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsVerified")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("longtext");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("varchar(255)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTimeOffset>("Retrieved")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("longtext");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("NormalizedEmail")
                        .IsUnique()
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.HasIndex("PhoneNumber")
                        .IsUnique();

                    b.HasIndex("UserName")
                        .IsUnique();

                    b.ToTable("User", "Identity");
                });

            modelBuilder.Entity("FileShare.DataAccess.Models.Primary.UserInformation.UserInformation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTimeOffset>("Changed")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTimeOffset>("Created")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTimeOffset>("DateOfBirth")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTimeOffset>("Deleted")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Firstname")
                        .HasMaxLength(64)
                        .HasColumnType("varchar(64)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Lastname")
                        .HasMaxLength(64)
                        .HasColumnType("varchar(64)");

                    b.Property<string>("Middlenames")
                        .HasMaxLength(768)
                        .HasColumnType("varchar(768)");

                    b.Property<string>("Nationality")
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<DateTimeOffset>("Retrieved")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("Sex")
                        .HasColumnType("int");

                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("UserInformation", "Identity");
                });

            modelBuilder.Entity("FileShare.DataAccess.Models.Primary.VerificationCode.VerificationCode", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTimeOffset>("Changed")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<DateTimeOffset>("Created")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTimeOffset>("Deleted")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTimeOffset>("Retrieved")
                        .HasColumnType("datetime(6)");

                    b.Property<byte>("Type")
                        .HasColumnType("tinyint unsigned");

                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.HasIndex("UserId");

                    b.ToTable("VerificationCodes", "Identity");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole<System.Guid>", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("Role", "Identity");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("RoleClaims", "Identity");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext");

                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Claims", "Identity");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("longtext");

                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("Logins", "Identity");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("char(36)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("UserRole", "Identity");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Value")
                        .HasColumnType("longtext");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("Tokens", "Identity");
                });

            modelBuilder.Entity("DocumentShare", b =>
                {
                    b.HasOne("FileShare.DataAccess.Models.Primary.Document.Document", null)
                        .WithMany()
                        .HasForeignKey("DocumentsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FileShare.DataAccess.Models.Primary.Share.Share", null)
                        .WithMany()
                        .HasForeignKey("SharesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FileShare.DataAccess.Models.Primary.Address.Address", b =>
                {
                    b.HasOne("FileShare.DataAccess.Models.Primary.User.User", "User")
                        .WithOne("Address")
                        .HasForeignKey("FileShare.DataAccess.Models.Primary.Address.Address", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("FileShare.DataAccess.Models.Primary.DeviceToken.DeviceToken", b =>
                {
                    b.HasOne("FileShare.DataAccess.Models.Primary.User.User", "User")
                        .WithMany("DeviceTokens")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("FileShare.DataAccess.Models.Primary.Document.Document", b =>
                {
                    b.HasOne("FileShare.DataAccess.Models.Primary.User.User", "User")
                        .WithMany("Documents")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("FileShare.DataAccess.Models.Primary.DocumentDetail.DocumentDetail", b =>
                {
                    b.HasOne("FileShare.DataAccess.Models.Primary.Document.Document", "Document")
                        .WithOne("Detail")
                        .HasForeignKey("FileShare.DataAccess.Models.Primary.DocumentDetail.DocumentDetail", "DocumentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Document");
                });

            modelBuilder.Entity("FileShare.DataAccess.Models.Primary.DocumentSignature.DocumentSignature", b =>
                {
                    b.HasOne("FileShare.DataAccess.Models.Primary.Document.Document", "Document")
                        .WithMany("Signatures")
                        .HasForeignKey("DocumentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Document");
                });

            modelBuilder.Entity("FileShare.DataAccess.Models.Primary.LoginAttempt.LoginAttempt", b =>
                {
                    b.HasOne("FileShare.DataAccess.Models.Primary.User.User", "User")
                        .WithMany("LoginAttempts")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("FileShare.DataAccess.Models.Primary.RefreshToken.RefreshToken", b =>
                {
                    b.HasOne("FileShare.DataAccess.Models.Primary.User.User", "User")
                        .WithMany("RefreshTokens")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("FileShare.DataAccess.Models.Primary.Share.Share", b =>
                {
                    b.HasOne("FileShare.DataAccess.Models.Primary.User.User", "User")
                        .WithMany("Shares")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("FileShare.DataAccess.Models.Primary.ShareDetail.ShareDetail", b =>
                {
                    b.HasOne("FileShare.DataAccess.Models.Primary.Share.Share", "Share")
                        .WithMany("ShareDetails")
                        .HasForeignKey("ShareId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Share");
                });

            modelBuilder.Entity("FileShare.DataAccess.Models.Primary.SocialSecurityNumber.SocialSecurityNumber", b =>
                {
                    b.HasOne("FileShare.DataAccess.Models.Primary.User.User", "User")
                        .WithMany("SocialSecurityNumbers")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("FileShare.DataAccess.Models.Primary.UserInformation.UserInformation", b =>
                {
                    b.HasOne("FileShare.DataAccess.Models.Primary.User.User", "User")
                        .WithOne("UserInformation")
                        .HasForeignKey("FileShare.DataAccess.Models.Primary.UserInformation.UserInformation", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("FileShare.DataAccess.Models.Primary.VerificationCode.VerificationCode", b =>
                {
                    b.HasOne("FileShare.DataAccess.Models.Primary.User.User", "User")
                        .WithMany("VerificationCodes")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<System.Guid>", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.HasOne("FileShare.DataAccess.Models.Primary.User.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.HasOne("FileShare.DataAccess.Models.Primary.User.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<System.Guid>", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FileShare.DataAccess.Models.Primary.User.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.HasOne("FileShare.DataAccess.Models.Primary.User.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FileShare.DataAccess.Models.Primary.Document.Document", b =>
                {
                    b.Navigation("Detail");

                    b.Navigation("Signatures");
                });

            modelBuilder.Entity("FileShare.DataAccess.Models.Primary.Share.Share", b =>
                {
                    b.Navigation("ShareDetails");
                });

            modelBuilder.Entity("FileShare.DataAccess.Models.Primary.User.User", b =>
                {
                    b.Navigation("Address");

                    b.Navigation("DeviceTokens");

                    b.Navigation("Documents");

                    b.Navigation("LoginAttempts");

                    b.Navigation("RefreshTokens");

                    b.Navigation("Shares");

                    b.Navigation("SocialSecurityNumbers");

                    b.Navigation("UserInformation");

                    b.Navigation("VerificationCodes");
                });
#pragma warning restore 612, 618
        }
    }
}
