using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BeautySalonBooking.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddMediaTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Glyph",
                schema: "GT",
                table: "SERVICES",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Glyph",
                schema: "GT",
                table: "ORGANIZATIONS",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ContentType",
                schema: "GT",
                table: "ORGANIZATION_MEDIAS",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(5)",
                oldMaxLength: 5);

            migrationBuilder.AddColumn<string>(
                name: "Glyph",
                schema: "BT",
                table: "CATEGORIES",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Glyph",
                schema: "GT",
                table: "BRANCHES",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BRANCH_MEDIAS",
                schema: "GT",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BranchId = table.Column<long>(type: "bigint", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ContentType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FileSize = table.Column<long>(type: "bigint", nullable: false),
                    Content = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    Type = table.Column<byte>(type: "tinyint", nullable: false),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BRANCH_MEDIAS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BRANCH_MEDIAS_BRANCHES_BranchId",
                        column: x => x.BranchId,
                        principalSchema: "GT",
                        principalTable: "BRANCHES",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CATEGORY_MEDIAS",
                schema: "BT",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ContentType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FileSize = table.Column<long>(type: "bigint", nullable: false),
                    Content = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    Type = table.Column<byte>(type: "tinyint", nullable: false),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CATEGORY_MEDIAS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CATEGORY_MEDIAS_CATEGORIES_CategoryId",
                        column: x => x.CategoryId,
                        principalSchema: "BT",
                        principalTable: "CATEGORIES",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SERVICE_MEDIAS",
                schema: "GT",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServiceId = table.Column<long>(type: "bigint", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ContentType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FileSize = table.Column<long>(type: "bigint", nullable: false),
                    Content = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    Type = table.Column<byte>(type: "tinyint", nullable: false),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SERVICE_MEDIAS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SERVICE_MEDIAS_SERVICES_ServiceId",
                        column: x => x.ServiceId,
                        principalSchema: "GT",
                        principalTable: "SERVICES",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "USER_MEDIAS",
                schema: "GT",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ContentType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FileSize = table.Column<long>(type: "bigint", nullable: false),
                    Content = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    Type = table.Column<byte>(type: "tinyint", nullable: false),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USER_MEDIAS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_USER_MEDIAS_USERS_UserId",
                        column: x => x.UserId,
                        principalSchema: "GT",
                        principalTable: "USERS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BRANCH_MEDIAS_BranchId",
                schema: "GT",
                table: "BRANCH_MEDIAS",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_CATEGORY_MEDIAS_CategoryId",
                schema: "BT",
                table: "CATEGORY_MEDIAS",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_SERVICE_MEDIAS_ServiceId",
                schema: "GT",
                table: "SERVICE_MEDIAS",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_USER_MEDIAS_UserId",
                schema: "GT",
                table: "USER_MEDIAS",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BRANCH_MEDIAS",
                schema: "GT");

            migrationBuilder.DropTable(
                name: "CATEGORY_MEDIAS",
                schema: "BT");

            migrationBuilder.DropTable(
                name: "SERVICE_MEDIAS",
                schema: "GT");

            migrationBuilder.DropTable(
                name: "USER_MEDIAS",
                schema: "GT");

            migrationBuilder.DropColumn(
                name: "Glyph",
                schema: "GT",
                table: "SERVICES");

            migrationBuilder.DropColumn(
                name: "Glyph",
                schema: "GT",
                table: "ORGANIZATIONS");

            migrationBuilder.DropColumn(
                name: "Glyph",
                schema: "BT",
                table: "CATEGORIES");

            migrationBuilder.DropColumn(
                name: "Glyph",
                schema: "GT",
                table: "BRANCHES");

            migrationBuilder.AlterColumn<string>(
                name: "ContentType",
                schema: "GT",
                table: "ORGANIZATION_MEDIAS",
                type: "nvarchar(5)",
                maxLength: 5,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);
        }
    }
}
