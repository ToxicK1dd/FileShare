﻿// <auto-generated />
using System;
using ImageApi.DataAccess.Models.Primary;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ImageApi.DataAccess.Migrations.Primary
{
    [DbContext(typeof(PrimaryContext))]
    partial class PrimaryContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("ImageApi.DataAccess.Models.Primary.Account.Account", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<bool>("Enabled")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.ToTable("Account");
                });

            modelBuilder.Entity("ImageApi.DataAccess.Models.Primary.AccountInfo.AccountInfo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("AccountId")
                        .HasColumnType("char(36)");

                    b.Property<string>("Email")
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<string>("FullName")
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(32)
                        .HasColumnType("varchar(32)");

                    b.HasKey("Id");

                    b.HasIndex("AccountId")
                        .IsUnique();

                    b.HasIndex("Id")
                        .IsUnique();

                    b.ToTable("AccountInfo");
                });

            modelBuilder.Entity("ImageApi.DataAccess.Models.Primary.Admin.Admin", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid?>("AccountId")
                        .HasColumnType("char(36)");

                    b.Property<int>("RoleType")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AccountId")
                        .IsUnique();

                    b.HasIndex("Id")
                        .IsUnique();

                    b.ToTable("Admin");
                });

            modelBuilder.Entity("ImageApi.DataAccess.Models.Primary.Document.Document", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<byte[]>("Blob")
                        .IsRequired()
                        .HasColumnType("LONGBLOB");

                    b.Property<string>("Format")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("varchar(64)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.HasIndex("UserId");

                    b.ToTable("Document");
                });

            modelBuilder.Entity("ImageApi.DataAccess.Models.Primary.DocumentDetail.DocumentDetail", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTimeOffset>("Accessed")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("AccessedBy")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("DocumentId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("DocumentId");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.ToTable("DocumentDetail");
                });

            modelBuilder.Entity("ImageApi.DataAccess.Models.Primary.Login.Login", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("AccountId")
                        .HasColumnType("char(36)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("varchar(64)");

                    b.HasKey("Id");

                    b.HasIndex("AccountId")
                        .IsUnique();

                    b.HasIndex("Id")
                        .IsUnique();

                    b.ToTable("Login");
                });

            modelBuilder.Entity("ImageApi.DataAccess.Models.Primary.LoginDetail.LoginDetail", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<int>("DeviceType")
                        .HasColumnType("int");

                    b.Property<Guid>("LoginId")
                        .HasColumnType("char(36)");

                    b.Property<bool>("Success")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTimeOffset>("Time")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.HasIndex("LoginId");

                    b.ToTable("LoginDetail");
                });

            modelBuilder.Entity("ImageApi.DataAccess.Models.Primary.RefreshToken.RefreshToken", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("LoginId")
                        .HasColumnType("char(36)");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.HasIndex("LoginId");

                    b.ToTable("RefreshToken");
                });

            modelBuilder.Entity("ImageApi.DataAccess.Models.Primary.User.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid?>("AccountId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("AccountId")
                        .IsUnique();

                    b.HasIndex("Id")
                        .IsUnique();

                    b.ToTable("User");
                });

            modelBuilder.Entity("ImageApi.DataAccess.Models.Primary.AccountInfo.AccountInfo", b =>
                {
                    b.HasOne("ImageApi.DataAccess.Models.Primary.Account.Account", "Account")
                        .WithOne("AccountInfo")
                        .HasForeignKey("ImageApi.DataAccess.Models.Primary.AccountInfo.AccountInfo", "AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("ImageApi.DataAccess.Models.Primary.Admin.Admin", b =>
                {
                    b.HasOne("ImageApi.DataAccess.Models.Primary.Account.Account", "Account")
                        .WithOne("Admin")
                        .HasForeignKey("ImageApi.DataAccess.Models.Primary.Admin.Admin", "AccountId");

                    b.Navigation("Account");
                });

            modelBuilder.Entity("ImageApi.DataAccess.Models.Primary.Document.Document", b =>
                {
                    b.HasOne("ImageApi.DataAccess.Models.Primary.User.User", "User")
                        .WithMany("Documents")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("ImageApi.DataAccess.Models.Primary.DocumentDetail.DocumentDetail", b =>
                {
                    b.HasOne("ImageApi.DataAccess.Models.Primary.Document.Document", "Document")
                        .WithMany("Details")
                        .HasForeignKey("DocumentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Document");
                });

            modelBuilder.Entity("ImageApi.DataAccess.Models.Primary.Login.Login", b =>
                {
                    b.HasOne("ImageApi.DataAccess.Models.Primary.Account.Account", "Account")
                        .WithOne("Login")
                        .HasForeignKey("ImageApi.DataAccess.Models.Primary.Login.Login", "AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("ImageApi.DataAccess.Models.Primary.LoginDetail.LoginDetail", b =>
                {
                    b.HasOne("ImageApi.DataAccess.Models.Primary.Login.Login", "Login")
                        .WithMany("LoginDetails")
                        .HasForeignKey("LoginId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Login");
                });

            modelBuilder.Entity("ImageApi.DataAccess.Models.Primary.RefreshToken.RefreshToken", b =>
                {
                    b.HasOne("ImageApi.DataAccess.Models.Primary.Login.Login", "Login")
                        .WithMany("RefreshTokens")
                        .HasForeignKey("LoginId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Login");
                });

            modelBuilder.Entity("ImageApi.DataAccess.Models.Primary.User.User", b =>
                {
                    b.HasOne("ImageApi.DataAccess.Models.Primary.Account.Account", "Account")
                        .WithOne("User")
                        .HasForeignKey("ImageApi.DataAccess.Models.Primary.User.User", "AccountId");

                    b.Navigation("Account");
                });

            modelBuilder.Entity("ImageApi.DataAccess.Models.Primary.Account.Account", b =>
                {
                    b.Navigation("AccountInfo");

                    b.Navigation("Admin");

                    b.Navigation("Login");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ImageApi.DataAccess.Models.Primary.Document.Document", b =>
                {
                    b.Navigation("Details");
                });

            modelBuilder.Entity("ImageApi.DataAccess.Models.Primary.Login.Login", b =>
                {
                    b.Navigation("LoginDetails");

                    b.Navigation("RefreshTokens");
                });

            modelBuilder.Entity("ImageApi.DataAccess.Models.Primary.User.User", b =>
                {
                    b.Navigation("Documents");
                });
#pragma warning restore 612, 618
        }
    }
}
