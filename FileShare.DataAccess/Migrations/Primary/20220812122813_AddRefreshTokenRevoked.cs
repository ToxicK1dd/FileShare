using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FileShare.DataAccess.Migrations.Primary
{
    public partial class AddRefreshTokenRevoked : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsRevoked",
                schema: "Identity",
                table: "RefreshTokens",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "Revoked",
                schema: "Identity",
                table: "RefreshTokens",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsRevoked",
                schema: "Identity",
                table: "RefreshTokens");

            migrationBuilder.DropColumn(
                name: "Revoked",
                schema: "Identity",
                table: "RefreshTokens");
        }
    }
}
