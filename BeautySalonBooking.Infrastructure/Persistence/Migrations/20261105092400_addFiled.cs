using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BeautySalonBooking.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class addFiled : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                schema: "REQ",
                table: "PERSON_REQUEST_DETAILS",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CodePosti",
                schema: "REQ",
                table: "PERSON_REQUEST_DETAILS",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LicenseNo",
                schema: "REQ",
                table: "PERSON_REQUEST_DETAILS",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                schema: "REQ",
                table: "PERSON_REQUEST_DETAILS");

            migrationBuilder.DropColumn(
                name: "CodePosti",
                schema: "REQ",
                table: "PERSON_REQUEST_DETAILS");

            migrationBuilder.DropColumn(
                name: "LicenseNo",
                schema: "REQ",
                table: "PERSON_REQUEST_DETAILS");
        }
    }
}
