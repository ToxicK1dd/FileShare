using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ImageApi.DataAccess.Migrations.Primary
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Account",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Enabled = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Validated = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Account", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AccountRole",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Type = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountRole", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AccountInfo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Firstname = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MiddleNames = table.Column<string>(type: "varchar(768)", maxLength: 768, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Lastname = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DateOfBirth = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false),
                    Nationality = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AccountId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccountInfo_Account_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Document",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Blob = table.Column<byte[]>(type: "LONGBLOB", nullable: false),
                    Content = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AccountId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Document", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Document_Account_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Login",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Username = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Password = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AccountId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Login", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Login_Account_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Share",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Expiration = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false),
                    AccountId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    SharedWithAccountId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Share", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Share_Account_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Share_Account_SharedWithAccountId",
                        column: x => x.SharedWithAccountId,
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ValidationCode",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Code = table.Column<string>(type: "varchar(16)", maxLength: 16, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AccountId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ValidationCode", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ValidationCode_Account_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AccountAccountRole",
                columns: table => new
                {
                    AccountsId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    RolesId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountAccountRole", x => new { x.AccountsId, x.RolesId });
                    table.ForeignKey(
                        name: "FK_AccountAccountRole_Account_AccountsId",
                        column: x => x.AccountsId,
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccountAccountRole_AccountRole_RolesId",
                        column: x => x.RolesId,
                        principalTable: "AccountRole",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Street = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AdditionalStreet = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PostalCode = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    City = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Region = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Country = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AccountInfoId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Address_AccountInfo_AccountInfoId",
                        column: x => x.AccountInfoId,
                        principalTable: "AccountInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ContactInfo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Email = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PhoneNumber = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AccountInfoId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContactInfo_AccountInfo_AccountInfoId",
                        column: x => x.AccountInfoId,
                        principalTable: "AccountInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "SocialSecurityNumber",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Number = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AccountInfoId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SocialSecurityNumber", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SocialSecurityNumber_AccountInfo_AccountInfoId",
                        column: x => x.AccountInfoId,
                        principalTable: "AccountInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "DocumentDetail",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Format = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ByteSize = table.Column<int>(type: "int", nullable: false),
                    DocumentId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DocumentDetail_Document_DocumentId",
                        column: x => x.DocumentId,
                        principalTable: "Document",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "DocumentSignature",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    DocumentId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentSignature", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DocumentSignature_Document_DocumentId",
                        column: x => x.DocumentId,
                        principalTable: "Document",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "LoginDetail",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Success = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    DeviceType = table.Column<int>(type: "int", nullable: false),
                    Time = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false),
                    LoginId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoginDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LoginDetail_Login_LoginId",
                        column: x => x.LoginId,
                        principalTable: "Login",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "RefreshToken",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Token = table.Column<string>(type: "varchar(512)", maxLength: 512, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Expiration = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false),
                    LoginId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshToken", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefreshToken_Login_LoginId",
                        column: x => x.LoginId,
                        principalTable: "Login",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "DocumentShare",
                columns: table => new
                {
                    DocumentsId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    SharesId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentShare", x => new { x.DocumentsId, x.SharesId });
                    table.ForeignKey(
                        name: "FK_DocumentShare_Document_DocumentsId",
                        column: x => x.DocumentsId,
                        principalTable: "Document",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DocumentShare_Share_SharesId",
                        column: x => x.SharesId,
                        principalTable: "Share",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ShareDetail",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ShareId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShareDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShareDetail_Share_ShareId",
                        column: x => x.ShareId,
                        principalTable: "Share",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Account_Id",
                table: "Account",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AccountAccountRole_RolesId",
                table: "AccountAccountRole",
                column: "RolesId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountInfo_AccountId",
                table: "AccountInfo",
                column: "AccountId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AccountInfo_Id",
                table: "AccountInfo",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AccountRole_Id",
                table: "AccountRole",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Address_AccountInfoId",
                table: "Address",
                column: "AccountInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_Address_Id",
                table: "Address",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ContactInfo_AccountInfoId",
                table: "ContactInfo",
                column: "AccountInfoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ContactInfo_Email",
                table: "ContactInfo",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ContactInfo_Id",
                table: "ContactInfo",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ContactInfo_PhoneNumber",
                table: "ContactInfo",
                column: "PhoneNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Document_AccountId",
                table: "Document",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Document_Id",
                table: "Document",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DocumentDetail_DocumentId",
                table: "DocumentDetail",
                column: "DocumentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DocumentDetail_Id",
                table: "DocumentDetail",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DocumentShare_SharesId",
                table: "DocumentShare",
                column: "SharesId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentSignature_DocumentId",
                table: "DocumentSignature",
                column: "DocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentSignature_Id",
                table: "DocumentSignature",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Login_AccountId",
                table: "Login",
                column: "AccountId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Login_Id",
                table: "Login",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Login_Username",
                table: "Login",
                column: "Username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LoginDetail_Id",
                table: "LoginDetail",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LoginDetail_LoginId",
                table: "LoginDetail",
                column: "LoginId");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshToken_Id",
                table: "RefreshToken",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RefreshToken_LoginId",
                table: "RefreshToken",
                column: "LoginId");

            migrationBuilder.CreateIndex(
                name: "IX_Share_AccountId",
                table: "Share",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Share_Id",
                table: "Share",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Share_SharedWithAccountId",
                table: "Share",
                column: "SharedWithAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_ShareDetail_Id",
                table: "ShareDetail",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ShareDetail_ShareId",
                table: "ShareDetail",
                column: "ShareId");

            migrationBuilder.CreateIndex(
                name: "IX_SocialSecurityNumber_AccountInfoId",
                table: "SocialSecurityNumber",
                column: "AccountInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_SocialSecurityNumber_Id",
                table: "SocialSecurityNumber",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ValidationCode_AccountId",
                table: "ValidationCode",
                column: "AccountId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ValidationCode_Id",
                table: "ValidationCode",
                column: "Id",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountAccountRole");

            migrationBuilder.DropTable(
                name: "Address");

            migrationBuilder.DropTable(
                name: "ContactInfo");

            migrationBuilder.DropTable(
                name: "DocumentDetail");

            migrationBuilder.DropTable(
                name: "DocumentShare");

            migrationBuilder.DropTable(
                name: "DocumentSignature");

            migrationBuilder.DropTable(
                name: "LoginDetail");

            migrationBuilder.DropTable(
                name: "RefreshToken");

            migrationBuilder.DropTable(
                name: "ShareDetail");

            migrationBuilder.DropTable(
                name: "SocialSecurityNumber");

            migrationBuilder.DropTable(
                name: "ValidationCode");

            migrationBuilder.DropTable(
                name: "AccountRole");

            migrationBuilder.DropTable(
                name: "Document");

            migrationBuilder.DropTable(
                name: "Login");

            migrationBuilder.DropTable(
                name: "Share");

            migrationBuilder.DropTable(
                name: "AccountInfo");

            migrationBuilder.DropTable(
                name: "Account");
        }
    }
}
