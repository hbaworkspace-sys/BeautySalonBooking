using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BeautySalonBooking.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class addrelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BRANCHES_ORGANIZATIONS_OrganizationId",
                schema: "GT",
                table: "BRANCHES");

            migrationBuilder.AddColumn<long>(
                name: "OrganizationId1",
                schema: "GT",
                table: "ORGANIZATION_DETAILS",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ORGANIZATION_DETAILS_OrganizationId1",
                schema: "GT",
                table: "ORGANIZATION_DETAILS",
                column: "OrganizationId1",
                unique: true,
                filter: "[OrganizationId1] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_BRANCHES_ORGANIZATIONS_OrganizationId",
                schema: "GT",
                table: "BRANCHES",
                column: "OrganizationId",
                principalSchema: "GT",
                principalTable: "ORGANIZATIONS",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ORGANIZATION_DETAILS_ORGANIZATIONS_OrganizationId1",
                schema: "GT",
                table: "ORGANIZATION_DETAILS",
                column: "OrganizationId1",
                principalSchema: "GT",
                principalTable: "ORGANIZATIONS",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BRANCHES_ORGANIZATIONS_OrganizationId",
                schema: "GT",
                table: "BRANCHES");

            migrationBuilder.DropForeignKey(
                name: "FK_ORGANIZATION_DETAILS_ORGANIZATIONS_OrganizationId1",
                schema: "GT",
                table: "ORGANIZATION_DETAILS");

            migrationBuilder.DropIndex(
                name: "IX_ORGANIZATION_DETAILS_OrganizationId1",
                schema: "GT",
                table: "ORGANIZATION_DETAILS");

            migrationBuilder.DropColumn(
                name: "OrganizationId1",
                schema: "GT",
                table: "ORGANIZATION_DETAILS");

            migrationBuilder.AddForeignKey(
                name: "FK_BRANCHES_ORGANIZATIONS_OrganizationId",
                schema: "GT",
                table: "BRANCHES",
                column: "OrganizationId",
                principalSchema: "GT",
                principalTable: "ORGANIZATIONS",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
