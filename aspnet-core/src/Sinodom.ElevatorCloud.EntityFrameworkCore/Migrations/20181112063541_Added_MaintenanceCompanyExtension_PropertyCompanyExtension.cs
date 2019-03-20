using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sinodom.ElevatorCloud.Migrations
{
    public partial class Added_MaintenanceCompanyExtension_PropertyCompanyExtension : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MaintenanceCompanyExtensions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeleterUserId = table.Column<long>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    LegalPerson = table.Column<string>(maxLength: 10, nullable: false),
                    Mobile = table.Column<string>(maxLength: 11, nullable: false),
                    MaintenanceCompanyId = table.Column<int>(nullable: false),
                    BusinessLicenseId = table.Column<Guid>(nullable: true),
                    AptitudePhotoId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaintenanceCompanyExtensions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MaintenanceCompanyExtensions_ECCPBaseMaintenanceCompanies_MaintenanceCompanyId",
                        column: x => x.MaintenanceCompanyId,
                        principalTable: "ECCPBaseMaintenanceCompanies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PropertyCompanyExtensions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeleterUserId = table.Column<long>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    LegalPerson = table.Column<string>(maxLength: 10, nullable: false),
                    Mobile = table.Column<string>(maxLength: 11, nullable: false),
                    PropertyCompanyId = table.Column<int>(nullable: false),
                    BusinessLicenseId = table.Column<Guid>(nullable: true),
                    AptitudePhotoId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyCompanyExtensions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PropertyCompanyExtensions_ECCPBasePropertyCompanies_PropertyCompanyId",
                        column: x => x.PropertyCompanyId,
                        principalTable: "ECCPBasePropertyCompanies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MaintenanceCompanyExtensions_MaintenanceCompanyId",
                table: "MaintenanceCompanyExtensions",
                column: "MaintenanceCompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyCompanyExtensions_PropertyCompanyId",
                table: "PropertyCompanyExtensions",
                column: "PropertyCompanyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MaintenanceCompanyExtensions");

            migrationBuilder.DropTable(
                name: "PropertyCompanyExtensions");
        }
    }
}
