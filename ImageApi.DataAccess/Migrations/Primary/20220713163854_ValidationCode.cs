using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ImageApi.DataAccess.Migrations.Primary
{
    public partial class ValidationCode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Validated",
                table: "Account",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

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
                name: "ValidationCode");

            migrationBuilder.DropColumn(
                name: "Validated",
                table: "Account");
        }
    }
}
