using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BeautySalonBooking.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "GT");

            migrationBuilder.EnsureSchema(
                name: "BT");

            migrationBuilder.EnsureSchema(
                name: "REQ");

            migrationBuilder.EnsureSchema(
                name: "TBL");

            migrationBuilder.CreateTable(
                name: "BRANCH_ROLES",
                schema: "BT",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BRANCH_ROLES", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CATEGORIES",
                schema: "BT",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParentId = table.Column<int>(type: "int", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Level = table.Column<byte>(type: "tinyint", nullable: false),
                    Type = table.Column<byte>(type: "tinyint", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CATEGORIES", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CATEGORIES_CATEGORIES_ParentId",
                        column: x => x.ParentId,
                        principalSchema: "BT",
                        principalTable: "CATEGORIES",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "COLORS",
                schema: "BT",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    HexCode = table.Column<string>(type: "nvarchar(7)", maxLength: 7, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_COLORS", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DOCUMENT_TYPES",
                schema: "REQ",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DOCUMENT_TYPES", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ORGANIZATIONS",
                schema: "GT",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    LicenseNumber = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Type = table.Column<byte>(type: "tinyint", nullable: false),
                    Signature = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Slogan = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ORGANIZATIONS", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PERMISSIONS",
                schema: "GT",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Type = table.Column<byte>(type: "tinyint", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PERMISSIONS", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PHONENUMBERS",
                schema: "GT",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RelatedId = table.Column<long>(type: "bigint", nullable: false),
                    RelatedType = table.Column<byte>(type: "tinyint", nullable: false),
                    Type = table.Column<byte>(type: "tinyint", nullable: false),
                    Number = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false),
                    IsVerified = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PHONENUMBERS", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "REGIONS",
                schema: "BT",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParentId = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    Type = table.Column<byte>(type: "tinyint", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_REGIONS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_REGIONS_REGIONS_ParentId",
                        column: x => x.ParentId,
                        principalSchema: "BT",
                        principalTable: "REGIONS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "REQUEST_FIELD_LOOKUPS",
                schema: "REQ",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Schema = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: false),
                    TableName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ValueColumn = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DisplayColumn = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_REQUEST_FIELD_LOOKUPS", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "REQUEST_TYPES",
                schema: "REQ",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    IsPaymentRequired = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_REQUEST_TYPES", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ROLES",
                schema: "BT",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ROLES", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SERVICES",
                schema: "GT",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    BasePrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BaseDuration = table.Column<TimeSpan>(type: "time", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SERVICES", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SERVICES_CATEGORIES_CategoryId",
                        column: x => x.CategoryId,
                        principalSchema: "BT",
                        principalTable: "CATEGORIES",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BRANCHES",
                schema: "GT",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrganizationId = table.Column<long>(type: "bigint", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BRANCHES", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BRANCHES_ORGANIZATIONS_OrganizationId",
                        column: x => x.OrganizationId,
                        principalSchema: "GT",
                        principalTable: "ORGANIZATIONS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ORGANIZATION_DETAILS",
                schema: "GT",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrganizationId = table.Column<long>(type: "bigint", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    Website = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Instagram = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Whatsapp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FromDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ToDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ORGANIZATION_DETAILS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ORGANIZATION_DETAILS_ORGANIZATIONS_OrganizationId",
                        column: x => x.OrganizationId,
                        principalSchema: "GT",
                        principalTable: "ORGANIZATIONS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ORGANIZATION_MEDIAS",
                schema: "GT",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrganizationId = table.Column<long>(type: "bigint", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ContentType = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
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
                    table.PrimaryKey("PK_ORGANIZATION_MEDIAS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ORGANIZATION_MEDIAS_ORGANIZATIONS_OrganizationId",
                        column: x => x.OrganizationId,
                        principalSchema: "GT",
                        principalTable: "ORGANIZATIONS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ADDRESSES",
                schema: "GT",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RelatedId = table.Column<long>(type: "bigint", nullable: false),
                    RelatedType = table.Column<byte>(type: "tinyint", nullable: false),
                    RegionId = table.Column<int>(type: "int", nullable: false),
                    FullAddress = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    PostalCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ADDRESSES", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ADDRESSES_REGIONS_RegionId",
                        column: x => x.RegionId,
                        principalSchema: "BT",
                        principalTable: "REGIONS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PERSONS",
                schema: "GT",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FatherName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    NationalCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Gender = table.Column<byte>(type: "tinyint", nullable: false),
                    BirthDate = table.Column<DateOnly>(type: "date", nullable: true),
                    BirthRegionId = table.Column<int>(type: "int", nullable: true),
                    BirthCertificateNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    BirthCertificateSerial = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PERSONS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PERSONS_REGIONS_BirthRegionId",
                        column: x => x.BirthRegionId,
                        principalSchema: "BT",
                        principalTable: "REGIONS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "REQUEST_FIELDS",
                schema: "REQ",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestTypeId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Key = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FieldType = table.Column<byte>(type: "tinyint", nullable: false),
                    IsRequired = table.Column<bool>(type: "bit", nullable: false),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false),
                    MaxLength = table.Column<int>(type: "int", nullable: true),
                    DefaultValue = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Placeholder = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Group = table.Column<byte>(type: "tinyint", nullable: false),
                    LookupId = table.Column<int>(type: "int", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_REQUEST_FIELDS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_REQUEST_FIELDS_REQUEST_FIELD_LOOKUPS_LookupId",
                        column: x => x.LookupId,
                        principalSchema: "REQ",
                        principalTable: "REQUEST_FIELD_LOOKUPS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_REQUEST_FIELDS_REQUEST_TYPES_RequestTypeId",
                        column: x => x.RequestTypeId,
                        principalSchema: "REQ",
                        principalTable: "REQUEST_TYPES",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "REQUEST_REQUIRED_DOCUMENTS",
                schema: "REQ",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestTypeId = table.Column<int>(type: "int", nullable: false),
                    DocumentTypeId = table.Column<int>(type: "int", nullable: false),
                    IsRequired = table.Column<bool>(type: "bit", nullable: false),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_REQUEST_REQUIRED_DOCUMENTS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_REQUEST_REQUIRED_DOCUMENTS_DOCUMENT_TYPES_DocumentTypeId",
                        column: x => x.DocumentTypeId,
                        principalSchema: "REQ",
                        principalTable: "DOCUMENT_TYPES",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_REQUEST_REQUIRED_DOCUMENTS_REQUEST_TYPES_RequestTypeId",
                        column: x => x.RequestTypeId,
                        principalSchema: "REQ",
                        principalTable: "REQUEST_TYPES",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ROLE_PERMISSIONS",
                schema: "GT",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    PermissionId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ROLE_PERMISSIONS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ROLE_PERMISSIONS_PERMISSIONS_PermissionId",
                        column: x => x.PermissionId,
                        principalSchema: "GT",
                        principalTable: "PERMISSIONS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ROLE_PERMISSIONS_ROLES_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "BT",
                        principalTable: "ROLES",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BRANCH_HOLIDAYS",
                schema: "GT",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BranchId = table.Column<long>(type: "bigint", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    IsNationalHoliday = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BRANCH_HOLIDAYS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BRANCH_HOLIDAYS_BRANCHES_BranchId",
                        column: x => x.BranchId,
                        principalSchema: "GT",
                        principalTable: "BRANCHES",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BRANCH_SCHEDULES",
                schema: "GT",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BranchId = table.Column<long>(type: "bigint", nullable: false),
                    DayOfWeek = table.Column<int>(type: "int", nullable: false),
                    IsWorkingDay = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BRANCH_SCHEDULES", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BRANCH_SCHEDULES_BRANCHES_BranchId",
                        column: x => x.BranchId,
                        principalSchema: "GT",
                        principalTable: "BRANCHES",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BRANCH_SERVICES",
                schema: "GT",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BranchId = table.Column<long>(type: "bigint", nullable: false),
                    ServiceId = table.Column<long>(type: "bigint", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Duration = table.Column<TimeSpan>(type: "time", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false),
                    FromDate = table.Column<DateOnly>(type: "date", nullable: false),
                    ToDate = table.Column<DateOnly>(type: "date", nullable: false),
                    ServiceId1 = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BRANCH_SERVICES", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BRANCH_SERVICES_BRANCHES_BranchId",
                        column: x => x.BranchId,
                        principalSchema: "GT",
                        principalTable: "BRANCHES",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BRANCH_SERVICES_SERVICES_ServiceId",
                        column: x => x.ServiceId,
                        principalSchema: "GT",
                        principalTable: "SERVICES",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BRANCH_SERVICES_SERVICES_ServiceId1",
                        column: x => x.ServiceId1,
                        principalSchema: "GT",
                        principalTable: "SERVICES",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BRANCH_MEMBERS",
                schema: "GT",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BranchId = table.Column<long>(type: "bigint", nullable: false),
                    PersonId = table.Column<long>(type: "bigint", nullable: false),
                    BranchRoleId = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: false),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BRANCH_MEMBERS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BRANCH_MEMBERS_BRANCHES_BranchId",
                        column: x => x.BranchId,
                        principalSchema: "GT",
                        principalTable: "BRANCHES",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BRANCH_MEMBERS_BRANCH_ROLES_BranchRoleId",
                        column: x => x.BranchRoleId,
                        principalSchema: "BT",
                        principalTable: "BRANCH_ROLES",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BRANCH_MEMBERS_PERSONS_PersonId",
                        column: x => x.PersonId,
                        principalSchema: "GT",
                        principalTable: "PERSONS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "USERS",
                schema: "GT",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonId = table.Column<long>(type: "bigint", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    AuthenticationMode = table.Column<byte>(type: "tinyint", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false),
                    LockoutEnd = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastLoginAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastPasswordChangedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LastFailedLoginAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastLogoutAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Tag1 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Tag2 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Tag3 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USERS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_USERS_PERSONS_PersonId",
                        column: x => x.PersonId,
                        principalSchema: "GT",
                        principalTable: "PERSONS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BRANCH_WORKING_HOURS",
                schema: "GT",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BranchScheduleId = table.Column<long>(type: "bigint", nullable: false),
                    StartTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    EndTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BRANCH_WORKING_HOURS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BRANCH_WORKING_HOURS_BRANCH_SCHEDULES_BranchScheduleId",
                        column: x => x.BranchScheduleId,
                        principalSchema: "GT",
                        principalTable: "BRANCH_SCHEDULES",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BRANCH_MEMBER_SCHEDULES",
                schema: "GT",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BranchMemberId = table.Column<long>(type: "bigint", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BRANCH_MEMBER_SCHEDULES", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BRANCH_MEMBER_SCHEDULES_BRANCH_MEMBERS_BranchMemberId",
                        column: x => x.BranchMemberId,
                        principalSchema: "GT",
                        principalTable: "BRANCH_MEMBERS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BRANCH_MEMBER_SERVICES",
                schema: "GT",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BranchMemberId = table.Column<long>(type: "bigint", nullable: false),
                    BranchServiceId = table.Column<long>(type: "bigint", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Duration = table.Column<TimeSpan>(type: "time", nullable: true),
                    BranchServiceId1 = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BRANCH_MEMBER_SERVICES", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BRANCH_MEMBER_SERVICES_BRANCH_MEMBERS_BranchMemberId",
                        column: x => x.BranchMemberId,
                        principalSchema: "GT",
                        principalTable: "BRANCH_MEMBERS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BRANCH_MEMBER_SERVICES_BRANCH_SERVICES_BranchServiceId",
                        column: x => x.BranchServiceId,
                        principalSchema: "GT",
                        principalTable: "BRANCH_SERVICES",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BRANCH_MEMBER_SERVICES_BRANCH_SERVICES_BranchServiceId1",
                        column: x => x.BranchServiceId1,
                        principalSchema: "GT",
                        principalTable: "BRANCH_SERVICES",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ORGANIZATION_OWNERS",
                schema: "GT",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrganizationId = table.Column<long>(type: "bigint", nullable: false),
                    OwnerUserId = table.Column<long>(type: "bigint", nullable: false),
                    IsPrimary = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ORGANIZATION_OWNERS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ORGANIZATION_OWNERS_ORGANIZATIONS_OrganizationId",
                        column: x => x.OrganizationId,
                        principalSchema: "GT",
                        principalTable: "ORGANIZATIONS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ORGANIZATION_OWNERS_USERS_OwnerUserId",
                        column: x => x.OwnerUserId,
                        principalSchema: "GT",
                        principalTable: "USERS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OTP_CODES",
                schema: "TBL",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MobileNumber = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    CodeHash = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false),
                    Purpose = table.Column<byte>(type: "tinyint", nullable: false),
                    UsedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ExpiresAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AttemptCount = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: true),
                    LastAttemptAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OTP_CODES", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OTP_CODES_USERS_UserId",
                        column: x => x.UserId,
                        principalSchema: "GT",
                        principalTable: "USERS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "REQUESTS",
                schema: "REQ",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    RequestTypeId = table.Column<int>(type: "int", nullable: false),
                    TrackingCode = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    Status = table.Column<byte>(type: "tinyint", nullable: false),
                    CurrentStep = table.Column<byte>(type: "tinyint", nullable: false),
                    SubmittedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReviewedBy = table.Column<long>(type: "bigint", nullable: true),
                    ReviewDescription = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_REQUESTS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_REQUESTS_REQUEST_TYPES_RequestTypeId",
                        column: x => x.RequestTypeId,
                        principalSchema: "REQ",
                        principalTable: "REQUEST_TYPES",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_REQUESTS_USERS_ReviewedBy",
                        column: x => x.ReviewedBy,
                        principalSchema: "GT",
                        principalTable: "USERS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_REQUESTS_USERS_UserId",
                        column: x => x.UserId,
                        principalSchema: "GT",
                        principalTable: "USERS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "USER_ROLES",
                schema: "GT",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    IsPrimary = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USER_ROLES", x => x.Id);
                    table.ForeignKey(
                        name: "FK_USER_ROLES_ROLES_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "BT",
                        principalTable: "ROLES",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_USER_ROLES_USERS_UserId",
                        column: x => x.UserId,
                        principalSchema: "GT",
                        principalTable: "USERS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SCHEDULE_EXCEPTIONS",
                schema: "GT",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BranchMemberScheduleId = table.Column<long>(type: "bigint", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    StartTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    EndTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    IsWorkingDay = table.Column<bool>(type: "bit", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SCHEDULE_EXCEPTIONS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SCHEDULE_EXCEPTIONS_BRANCH_MEMBER_SCHEDULES_BranchMemberScheduleId",
                        column: x => x.BranchMemberScheduleId,
                        principalSchema: "GT",
                        principalTable: "BRANCH_MEMBER_SCHEDULES",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TIMEOFFS",
                schema: "GT",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BranchMemberScheduleId = table.Column<long>(type: "bigint", nullable: false),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: false),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: false),
                    StartTime = table.Column<TimeOnly>(type: "time", nullable: true),
                    EndTime = table.Column<TimeOnly>(type: "time", nullable: true),
                    Reason = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TIMEOFFS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TIMEOFFS_BRANCH_MEMBER_SCHEDULES_BranchMemberScheduleId",
                        column: x => x.BranchMemberScheduleId,
                        principalSchema: "GT",
                        principalTable: "BRANCH_MEMBER_SCHEDULES",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WORKING_SHIFTS",
                schema: "GT",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BranchMemberScheduleId = table.Column<long>(type: "bigint", nullable: false),
                    DayOfWeek = table.Column<int>(type: "int", nullable: false),
                    StartTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    EndTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WORKING_SHIFTS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WORKING_SHIFTS_BRANCH_MEMBER_SCHEDULES_BranchMemberScheduleId",
                        column: x => x.BranchMemberScheduleId,
                        principalSchema: "GT",
                        principalTable: "BRANCH_MEMBER_SCHEDULES",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "APPOINTMENTS",
                schema: "GT",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerUserId = table.Column<long>(type: "bigint", nullable: false),
                    BranchMemberServiceId = table.Column<long>(type: "bigint", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    StartTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    EndTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Status = table.Column<byte>(type: "tinyint", nullable: false),
                    PaymentStatus = table.Column<byte>(type: "tinyint", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CancelledByUserId = table.Column<long>(type: "bigint", nullable: true),
                    CancelledAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CancelReason = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_APPOINTMENTS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_APPOINTMENTS_BRANCH_MEMBER_SERVICES_BranchMemberServiceId",
                        column: x => x.BranchMemberServiceId,
                        principalSchema: "GT",
                        principalTable: "BRANCH_MEMBER_SERVICES",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_APPOINTMENTS_USERS_CancelledByUserId",
                        column: x => x.CancelledByUserId,
                        principalSchema: "GT",
                        principalTable: "USERS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_APPOINTMENTS_USERS_CustomerUserId",
                        column: x => x.CustomerUserId,
                        principalSchema: "GT",
                        principalTable: "USERS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ORGANIZATION_REQUEST_DETAILS",
                schema: "REQ",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestId = table.Column<long>(type: "bigint", nullable: false),
                    OrganizationName = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true),
                    OrganizationDescription = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    MobileNumber = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Website = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Instagram = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Whatsapp = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    BranchName = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true),
                    BranchDescription = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    RegionId = table.Column<int>(type: "int", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    PostalCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    LicenseNumber = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    LicenseIssuedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LicenseExpireDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Slogan = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    AdditionalInfo = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ORGANIZATION_REQUEST_DETAILS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ORGANIZATION_REQUEST_DETAILS_REGIONS_RegionId",
                        column: x => x.RegionId,
                        principalSchema: "BT",
                        principalTable: "REGIONS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ORGANIZATION_REQUEST_DETAILS_REQUESTS_RequestId",
                        column: x => x.RequestId,
                        principalSchema: "REQ",
                        principalTable: "REQUESTS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PERSON_REQUEST_DETAILS",
                schema: "REQ",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestId = table.Column<long>(type: "bigint", nullable: false),
                    Biography = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    ExperienceYears = table.Column<int>(type: "int", nullable: true),
                    Skills = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Certifications = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Instagram = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Website = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    AdditionalInfo = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PERSON_REQUEST_DETAILS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PERSON_REQUEST_DETAILS_REQUESTS_RequestId",
                        column: x => x.RequestId,
                        principalSchema: "REQ",
                        principalTable: "REQUESTS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "REQUEST_DETAILS",
                schema: "REQ",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestId = table.Column<long>(type: "bigint", nullable: false),
                    RequestFieldId = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_REQUEST_DETAILS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_REQUEST_DETAILS_REQUESTS_RequestId",
                        column: x => x.RequestId,
                        principalSchema: "REQ",
                        principalTable: "REQUESTS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_REQUEST_DETAILS_REQUEST_FIELDS_RequestFieldId",
                        column: x => x.RequestFieldId,
                        principalSchema: "REQ",
                        principalTable: "REQUEST_FIELDS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "REQUEST_DOCUMENTS",
                schema: "REQ",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestId = table.Column<long>(type: "bigint", nullable: false),
                    DocumentTypeId = table.Column<int>(type: "int", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ContentType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FileSize = table.Column<long>(type: "bigint", nullable: false),
                    Content = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    Status = table.Column<byte>(type: "tinyint", nullable: false),
                    ReviewedByUserId = table.Column<long>(type: "bigint", nullable: true),
                    ReviewedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ReviewDescription = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_REQUEST_DOCUMENTS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_REQUEST_DOCUMENTS_DOCUMENT_TYPES_DocumentTypeId",
                        column: x => x.DocumentTypeId,
                        principalSchema: "REQ",
                        principalTable: "DOCUMENT_TYPES",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_REQUEST_DOCUMENTS_REQUESTS_RequestId",
                        column: x => x.RequestId,
                        principalSchema: "REQ",
                        principalTable: "REQUESTS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_REQUEST_DOCUMENTS_USERS_ReviewedByUserId",
                        column: x => x.ReviewedByUserId,
                        principalSchema: "GT",
                        principalTable: "USERS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "REQUEST_HISTORIES",
                schema: "REQ",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestId = table.Column<long>(type: "bigint", nullable: false),
                    Status = table.Column<byte>(type: "tinyint", nullable: false),
                    Action = table.Column<byte>(type: "tinyint", nullable: false),
                    ChangedByUserId = table.Column<long>(type: "bigint", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_REQUEST_HISTORIES", x => x.Id);
                    table.ForeignKey(
                        name: "FK_REQUEST_HISTORIES_REQUESTS_RequestId",
                        column: x => x.RequestId,
                        principalSchema: "REQ",
                        principalTable: "REQUESTS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_REQUEST_HISTORIES_USERS_ChangedByUserId",
                        column: x => x.ChangedByUserId,
                        principalSchema: "GT",
                        principalTable: "USERS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ADDRESSES_RegionId",
                schema: "GT",
                table: "ADDRESSES",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_ADDRESSES_RelatedId_RelatedType",
                schema: "GT",
                table: "ADDRESSES",
                columns: new[] { "RelatedId", "RelatedType" });

            migrationBuilder.CreateIndex(
                name: "IX_APPOINTMENTS_BranchMemberServiceId_Date_StartTime",
                schema: "GT",
                table: "APPOINTMENTS",
                columns: new[] { "BranchMemberServiceId", "Date", "StartTime" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_APPOINTMENTS_CancelledByUserId",
                schema: "GT",
                table: "APPOINTMENTS",
                column: "CancelledByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_APPOINTMENTS_CustomerUserId",
                schema: "GT",
                table: "APPOINTMENTS",
                column: "CustomerUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BRANCH_HOLIDAYS_BranchId_Date",
                schema: "GT",
                table: "BRANCH_HOLIDAYS",
                columns: new[] { "BranchId", "Date" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BRANCH_MEMBER_SCHEDULES_BranchMemberId",
                schema: "GT",
                table: "BRANCH_MEMBER_SCHEDULES",
                column: "BranchMemberId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BRANCH_MEMBER_SERVICES_BranchMemberId_BranchServiceId",
                schema: "GT",
                table: "BRANCH_MEMBER_SERVICES",
                columns: new[] { "BranchMemberId", "BranchServiceId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BRANCH_MEMBER_SERVICES_BranchServiceId",
                schema: "GT",
                table: "BRANCH_MEMBER_SERVICES",
                column: "BranchServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_BRANCH_MEMBER_SERVICES_BranchServiceId1",
                schema: "GT",
                table: "BRANCH_MEMBER_SERVICES",
                column: "BranchServiceId1");

            migrationBuilder.CreateIndex(
                name: "IX_BRANCH_MEMBERS_BranchId",
                schema: "GT",
                table: "BRANCH_MEMBERS",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_BRANCH_MEMBERS_BranchRoleId",
                schema: "GT",
                table: "BRANCH_MEMBERS",
                column: "BranchRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_BRANCH_MEMBERS_PersonId",
                schema: "GT",
                table: "BRANCH_MEMBERS",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_BRANCH_ROLES_Code",
                schema: "BT",
                table: "BRANCH_ROLES",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BRANCH_SCHEDULES_BranchId_DayOfWeek",
                schema: "GT",
                table: "BRANCH_SCHEDULES",
                columns: new[] { "BranchId", "DayOfWeek" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BRANCH_SERVICES_BranchId_ServiceId",
                schema: "GT",
                table: "BRANCH_SERVICES",
                columns: new[] { "BranchId", "ServiceId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BRANCH_SERVICES_ServiceId",
                schema: "GT",
                table: "BRANCH_SERVICES",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_BRANCH_SERVICES_ServiceId1",
                schema: "GT",
                table: "BRANCH_SERVICES",
                column: "ServiceId1");

            migrationBuilder.CreateIndex(
                name: "IX_BRANCH_WORKING_HOURS_BranchScheduleId",
                schema: "GT",
                table: "BRANCH_WORKING_HOURS",
                column: "BranchScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_BRANCHES_OrganizationId",
                schema: "GT",
                table: "BRANCHES",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_CATEGORIES_Code",
                schema: "BT",
                table: "CATEGORIES",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CATEGORIES_ParentId",
                schema: "BT",
                table: "CATEGORIES",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_COLORS_Code",
                schema: "BT",
                table: "COLORS",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_COLORS_Name",
                schema: "BT",
                table: "COLORS",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DOCUMENT_TYPES_Code",
                schema: "REQ",
                table: "DOCUMENT_TYPES",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ORGANIZATION_DETAILS_OrganizationId",
                schema: "GT",
                table: "ORGANIZATION_DETAILS",
                column: "OrganizationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ORGANIZATION_MEDIAS_OrganizationId",
                schema: "GT",
                table: "ORGANIZATION_MEDIAS",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_ORGANIZATION_OWNERS_OrganizationId_IsPrimary",
                schema: "GT",
                table: "ORGANIZATION_OWNERS",
                columns: new[] { "OrganizationId", "IsPrimary" },
                unique: true,
                filter: "[IsPrimary] = 1");

            migrationBuilder.CreateIndex(
                name: "IX_ORGANIZATION_OWNERS_OrganizationId_OwnerUserId",
                schema: "GT",
                table: "ORGANIZATION_OWNERS",
                columns: new[] { "OrganizationId", "OwnerUserId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ORGANIZATION_OWNERS_OwnerUserId",
                schema: "GT",
                table: "ORGANIZATION_OWNERS",
                column: "OwnerUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ORGANIZATION_REQUEST_DETAILS_RegionId",
                schema: "REQ",
                table: "ORGANIZATION_REQUEST_DETAILS",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_ORGANIZATION_REQUEST_DETAILS_RequestId",
                schema: "REQ",
                table: "ORGANIZATION_REQUEST_DETAILS",
                column: "RequestId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OTP_CODES_MobileNumber_Purpose_ExpiresAt",
                schema: "TBL",
                table: "OTP_CODES",
                columns: new[] { "MobileNumber", "Purpose", "ExpiresAt" });

            migrationBuilder.CreateIndex(
                name: "IX_OTP_CODES_UserId",
                schema: "TBL",
                table: "OTP_CODES",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PERMISSIONS_Code",
                schema: "GT",
                table: "PERMISSIONS",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PERSON_REQUEST_DETAILS_RequestId",
                schema: "REQ",
                table: "PERSON_REQUEST_DETAILS",
                column: "RequestId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PERSONS_BirthRegionId",
                schema: "GT",
                table: "PERSONS",
                column: "BirthRegionId");

            migrationBuilder.CreateIndex(
                name: "IX_PERSONS_NationalCode",
                schema: "GT",
                table: "PERSONS",
                column: "NationalCode",
                unique: true,
                filter: "[NationalCode] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_PHONENUMBERS_RelatedId_RelatedType_IsDefault",
                schema: "GT",
                table: "PHONENUMBERS",
                columns: new[] { "RelatedId", "RelatedType", "IsDefault" },
                unique: true,
                filter: "[IsDefault] = 1");

            migrationBuilder.CreateIndex(
                name: "IX_PHONENUMBERS_RelatedId_RelatedType_Number",
                schema: "GT",
                table: "PHONENUMBERS",
                columns: new[] { "RelatedId", "RelatedType", "Number" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_REGIONS_Code",
                schema: "BT",
                table: "REGIONS",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_REGIONS_ParentId",
                schema: "BT",
                table: "REGIONS",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_REQUEST_DETAILS_RequestFieldId",
                schema: "REQ",
                table: "REQUEST_DETAILS",
                column: "RequestFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_REQUEST_DETAILS_RequestId",
                schema: "REQ",
                table: "REQUEST_DETAILS",
                column: "RequestId");

            migrationBuilder.CreateIndex(
                name: "IX_REQUEST_DOCUMENTS_DocumentTypeId",
                schema: "REQ",
                table: "REQUEST_DOCUMENTS",
                column: "DocumentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_REQUEST_DOCUMENTS_RequestId",
                schema: "REQ",
                table: "REQUEST_DOCUMENTS",
                column: "RequestId");

            migrationBuilder.CreateIndex(
                name: "IX_REQUEST_DOCUMENTS_ReviewedByUserId",
                schema: "REQ",
                table: "REQUEST_DOCUMENTS",
                column: "ReviewedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_REQUEST_FIELD_LOOKUPS_Code",
                schema: "REQ",
                table: "REQUEST_FIELD_LOOKUPS",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_REQUEST_FIELDS_LookupId",
                schema: "REQ",
                table: "REQUEST_FIELDS",
                column: "LookupId");

            migrationBuilder.CreateIndex(
                name: "IX_REQUEST_FIELDS_RequestTypeId_Key",
                schema: "REQ",
                table: "REQUEST_FIELDS",
                columns: new[] { "RequestTypeId", "Key" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_REQUEST_HISTORIES_ChangedByUserId",
                schema: "REQ",
                table: "REQUEST_HISTORIES",
                column: "ChangedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_REQUEST_HISTORIES_RequestId",
                schema: "REQ",
                table: "REQUEST_HISTORIES",
                column: "RequestId");

            migrationBuilder.CreateIndex(
                name: "IX_REQUEST_REQUIRED_DOCUMENTS_DocumentTypeId",
                schema: "REQ",
                table: "REQUEST_REQUIRED_DOCUMENTS",
                column: "DocumentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_REQUEST_REQUIRED_DOCUMENTS_RequestTypeId_DocumentTypeId",
                schema: "REQ",
                table: "REQUEST_REQUIRED_DOCUMENTS",
                columns: new[] { "RequestTypeId", "DocumentTypeId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_REQUEST_TYPES_Code",
                schema: "REQ",
                table: "REQUEST_TYPES",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_REQUESTS_RequestTypeId",
                schema: "REQ",
                table: "REQUESTS",
                column: "RequestTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_REQUESTS_ReviewedBy",
                schema: "REQ",
                table: "REQUESTS",
                column: "ReviewedBy");

            migrationBuilder.CreateIndex(
                name: "IX_REQUESTS_TrackingCode",
                schema: "REQ",
                table: "REQUESTS",
                column: "TrackingCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_REQUESTS_UserId",
                schema: "REQ",
                table: "REQUESTS",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ROLE_PERMISSIONS_PermissionId",
                schema: "GT",
                table: "ROLE_PERMISSIONS",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_ROLE_PERMISSIONS_RoleId_PermissionId",
                schema: "GT",
                table: "ROLE_PERMISSIONS",
                columns: new[] { "RoleId", "PermissionId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ROLES_Code",
                schema: "BT",
                table: "ROLES",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SCHEDULE_EXCEPTIONS_BranchMemberScheduleId_Date",
                schema: "GT",
                table: "SCHEDULE_EXCEPTIONS",
                columns: new[] { "BranchMemberScheduleId", "Date" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SERVICES_CategoryId",
                schema: "GT",
                table: "SERVICES",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_SERVICES_Code",
                schema: "GT",
                table: "SERVICES",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TIMEOFFS_BranchMemberScheduleId",
                schema: "GT",
                table: "TIMEOFFS",
                column: "BranchMemberScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_USER_ROLES_RoleId",
                schema: "GT",
                table: "USER_ROLES",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_USER_ROLES_UserId_IsPrimary",
                schema: "GT",
                table: "USER_ROLES",
                columns: new[] { "UserId", "IsPrimary" },
                unique: true,
                filter: "[IsPrimary] = 1");

            migrationBuilder.CreateIndex(
                name: "IX_USER_ROLES_UserId_RoleId",
                schema: "GT",
                table: "USER_ROLES",
                columns: new[] { "UserId", "RoleId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_USERS_PersonId",
                schema: "GT",
                table: "USERS",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_USERS_UserName",
                schema: "GT",
                table: "USERS",
                column: "UserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WORKING_SHIFTS_BranchMemberScheduleId_DayOfWeek_StartTime_EndTime",
                schema: "GT",
                table: "WORKING_SHIFTS",
                columns: new[] { "BranchMemberScheduleId", "DayOfWeek", "StartTime", "EndTime" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ADDRESSES",
                schema: "GT");

            migrationBuilder.DropTable(
                name: "APPOINTMENTS",
                schema: "GT");

            migrationBuilder.DropTable(
                name: "BRANCH_HOLIDAYS",
                schema: "GT");

            migrationBuilder.DropTable(
                name: "BRANCH_WORKING_HOURS",
                schema: "GT");

            migrationBuilder.DropTable(
                name: "COLORS",
                schema: "BT");

            migrationBuilder.DropTable(
                name: "ORGANIZATION_DETAILS",
                schema: "GT");

            migrationBuilder.DropTable(
                name: "ORGANIZATION_MEDIAS",
                schema: "GT");

            migrationBuilder.DropTable(
                name: "ORGANIZATION_OWNERS",
                schema: "GT");

            migrationBuilder.DropTable(
                name: "ORGANIZATION_REQUEST_DETAILS",
                schema: "REQ");

            migrationBuilder.DropTable(
                name: "OTP_CODES",
                schema: "TBL");

            migrationBuilder.DropTable(
                name: "PERSON_REQUEST_DETAILS",
                schema: "REQ");

            migrationBuilder.DropTable(
                name: "PHONENUMBERS",
                schema: "GT");

            migrationBuilder.DropTable(
                name: "REQUEST_DETAILS",
                schema: "REQ");

            migrationBuilder.DropTable(
                name: "REQUEST_DOCUMENTS",
                schema: "REQ");

            migrationBuilder.DropTable(
                name: "REQUEST_HISTORIES",
                schema: "REQ");

            migrationBuilder.DropTable(
                name: "REQUEST_REQUIRED_DOCUMENTS",
                schema: "REQ");

            migrationBuilder.DropTable(
                name: "ROLE_PERMISSIONS",
                schema: "GT");

            migrationBuilder.DropTable(
                name: "SCHEDULE_EXCEPTIONS",
                schema: "GT");

            migrationBuilder.DropTable(
                name: "TIMEOFFS",
                schema: "GT");

            migrationBuilder.DropTable(
                name: "USER_ROLES",
                schema: "GT");

            migrationBuilder.DropTable(
                name: "WORKING_SHIFTS",
                schema: "GT");

            migrationBuilder.DropTable(
                name: "BRANCH_MEMBER_SERVICES",
                schema: "GT");

            migrationBuilder.DropTable(
                name: "BRANCH_SCHEDULES",
                schema: "GT");

            migrationBuilder.DropTable(
                name: "REQUEST_FIELDS",
                schema: "REQ");

            migrationBuilder.DropTable(
                name: "REQUESTS",
                schema: "REQ");

            migrationBuilder.DropTable(
                name: "DOCUMENT_TYPES",
                schema: "REQ");

            migrationBuilder.DropTable(
                name: "PERMISSIONS",
                schema: "GT");

            migrationBuilder.DropTable(
                name: "ROLES",
                schema: "BT");

            migrationBuilder.DropTable(
                name: "BRANCH_MEMBER_SCHEDULES",
                schema: "GT");

            migrationBuilder.DropTable(
                name: "BRANCH_SERVICES",
                schema: "GT");

            migrationBuilder.DropTable(
                name: "REQUEST_FIELD_LOOKUPS",
                schema: "REQ");

            migrationBuilder.DropTable(
                name: "REQUEST_TYPES",
                schema: "REQ");

            migrationBuilder.DropTable(
                name: "USERS",
                schema: "GT");

            migrationBuilder.DropTable(
                name: "BRANCH_MEMBERS",
                schema: "GT");

            migrationBuilder.DropTable(
                name: "SERVICES",
                schema: "GT");

            migrationBuilder.DropTable(
                name: "BRANCHES",
                schema: "GT");

            migrationBuilder.DropTable(
                name: "BRANCH_ROLES",
                schema: "BT");

            migrationBuilder.DropTable(
                name: "PERSONS",
                schema: "GT");

            migrationBuilder.DropTable(
                name: "CATEGORIES",
                schema: "BT");

            migrationBuilder.DropTable(
                name: "ORGANIZATIONS",
                schema: "GT");

            migrationBuilder.DropTable(
                name: "REGIONS",
                schema: "BT");
        }
    }
}
