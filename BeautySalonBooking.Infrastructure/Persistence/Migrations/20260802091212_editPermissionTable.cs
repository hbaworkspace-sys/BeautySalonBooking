using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BeautySalonBooking.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class editPermissionTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ROLE_PERMISSIONS_PERMISSIONS_PermissionId",
                schema: "GT",
                table: "ROLE_PERMISSIONS");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PERMISSIONS",
                schema: "GT",
                table: "PERMISSIONS");

            migrationBuilder.DropIndex(
                name: "IX_PERMISSIONS_Code",
                schema: "GT",
                table: "PERMISSIONS");

            migrationBuilder.RenameTable(
                name: "PERMISSIONS",
                schema: "GT",
                newName: "Permissions");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Permissions",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "Permissions",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: false);

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "Permissions",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Permissions",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "Permissions",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AddColumn<int>(
                name: "ParentId",
                table: "Permissions",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Permissions",
                table: "Permissions",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_ParentId",
                table: "Permissions",
                column: "ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Permissions_Permissions_ParentId",
                table: "Permissions",
                column: "ParentId",
                principalTable: "Permissions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ROLE_PERMISSIONS_Permissions_PermissionId",
                schema: "GT",
                table: "ROLE_PERMISSIONS",
                column: "PermissionId",
                principalTable: "Permissions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Permissions_Permissions_ParentId",
                table: "Permissions");

            migrationBuilder.DropForeignKey(
                name: "FK_ROLE_PERMISSIONS_Permissions_PermissionId",
                schema: "GT",
                table: "ROLE_PERMISSIONS");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Permissions",
                table: "Permissions");

            migrationBuilder.DropIndex(
                name: "IX_Permissions_ParentId",
                table: "Permissions");

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "Permissions");

            migrationBuilder.RenameTable(
                name: "Permissions",
                newName: "PERMISSIONS",
                newSchema: "GT");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                schema: "GT",
                table: "PERMISSIONS",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                schema: "GT",
                table: "PERMISSIONS",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                schema: "GT",
                table: "PERMISSIONS",
                type: "bit",
                nullable: false,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                schema: "GT",
                table: "PERMISSIONS",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                schema: "GT",
                table: "PERMISSIONS",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PERMISSIONS",
                schema: "GT",
                table: "PERMISSIONS",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_PERMISSIONS_Code",
                schema: "GT",
                table: "PERMISSIONS",
                column: "Code",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ROLE_PERMISSIONS_PERMISSIONS_PermissionId",
                schema: "GT",
                table: "ROLE_PERMISSIONS",
                column: "PermissionId",
                principalSchema: "GT",
                principalTable: "PERMISSIONS",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
