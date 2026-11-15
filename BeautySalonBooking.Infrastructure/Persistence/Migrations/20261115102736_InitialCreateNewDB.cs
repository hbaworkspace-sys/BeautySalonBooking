using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BeautySalonBooking.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreateNewDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ContentItem",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrganizationId = table.Column<long>(type: "bigint", nullable: true),
                    BranchId = table.Column<long>(type: "bigint", nullable: true),
                    PublisherUserId = table.Column<long>(type: "bigint", nullable: true),
                    Type = table.Column<byte>(type: "tinyint", nullable: false),
                    Status = table.Column<byte>(type: "tinyint", nullable: false),
                    Caption = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PublishedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ExpiresAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContentItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContentItem_BRANCHES_BranchId",
                        column: x => x.BranchId,
                        principalSchema: "GT",
                        principalTable: "BRANCHES",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ContentItem_ORGANIZATIONS_OrganizationId",
                        column: x => x.OrganizationId,
                        principalSchema: "GT",
                        principalTable: "ORGANIZATIONS",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ContentItem_USERS_PublisherUserId",
                        column: x => x.PublisherUserId,
                        principalSchema: "GT",
                        principalTable: "USERS",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ContentMedia",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContentItemId = table.Column<long>(type: "bigint", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContentType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileSize = table.Column<long>(type: "bigint", nullable: false),
                    Content = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    Type = table.Column<byte>(type: "tinyint", nullable: false),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContentMedia", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContentMedia_ContentItem_ContentItemId",
                        column: x => x.ContentItemId,
                        principalTable: "ContentItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContentItem_BranchId",
                table: "ContentItem",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_ContentItem_OrganizationId",
                table: "ContentItem",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_ContentItem_PublisherUserId",
                table: "ContentItem",
                column: "PublisherUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ContentMedia_ContentItemId",
                table: "ContentMedia",
                column: "ContentItemId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContentMedia");

            migrationBuilder.DropTable(
                name: "ContentItem");
        }
    }
}
