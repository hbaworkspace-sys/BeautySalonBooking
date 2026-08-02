using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BeautySalonBooking.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class editRefreshTokens : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Token",
                schema: "GT",
                table: "REFRESH_TOKEN",
                newName: "TokenHash");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAtUtc",
                schema: "GT",
                table: "REFRESH_TOKEN",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "DeviceId",
                schema: "GT",
                table: "REFRESH_TOKEN",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "DeviceName",
                schema: "GT",
                table: "REFRESH_TOKEN",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "IpAddress",
                schema: "GT",
                table: "REFRESH_TOKEN",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ReplacedByTokenHash",
                schema: "GT",
                table: "REFRESH_TOKEN",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RevokedAt",
                schema: "GT",
                table: "REFRESH_TOKEN",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RevokedAtUtc",
                schema: "GT",
                table: "REFRESH_TOKEN",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAtUtc",
                schema: "GT",
                table: "REFRESH_TOKEN");

            migrationBuilder.DropColumn(
                name: "DeviceId",
                schema: "GT",
                table: "REFRESH_TOKEN");

            migrationBuilder.DropColumn(
                name: "DeviceName",
                schema: "GT",
                table: "REFRESH_TOKEN");

            migrationBuilder.DropColumn(
                name: "IpAddress",
                schema: "GT",
                table: "REFRESH_TOKEN");

            migrationBuilder.DropColumn(
                name: "ReplacedByTokenHash",
                schema: "GT",
                table: "REFRESH_TOKEN");

            migrationBuilder.DropColumn(
                name: "RevokedAt",
                schema: "GT",
                table: "REFRESH_TOKEN");

            migrationBuilder.DropColumn(
                name: "RevokedAtUtc",
                schema: "GT",
                table: "REFRESH_TOKEN");

            migrationBuilder.RenameColumn(
                name: "TokenHash",
                schema: "GT",
                table: "REFRESH_TOKEN",
                newName: "Token");
        }
    }
}
