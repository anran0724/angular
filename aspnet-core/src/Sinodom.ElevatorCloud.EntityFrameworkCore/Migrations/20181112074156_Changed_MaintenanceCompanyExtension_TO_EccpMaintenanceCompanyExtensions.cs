using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sinodom.ElevatorCloud.Migrations
{
    public partial class Changed_MaintenanceCompanyExtension_TO_EccpMaintenanceCompanyExtensions_PropertyCompanyExtensions_TO_EccpPropertyCompanyExtensions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MaintenanceCompanyExtensions");

            migrationBuilder.DropTable(
                name: "PropertyCompanyExtensions");

            migrationBuilder.CreateTable(
                name: "EccpMaintenanceCompanyExtensions",
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
                    table.PrimaryKey("PK_EccpMaintenanceCompanyExtensions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EccpMaintenanceCompanyExtensions_ECCPBaseMaintenanceCompanies_MaintenanceCompanyId",
                        column: x => x.MaintenanceCompanyId,
                        principalTable: "ECCPBaseMaintenanceCompanies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EccpPropertyCompanyExtensions",
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
                    table.PrimaryKey("PK_EccpPropertyCompanyExtensions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EccpPropertyCompanyExtensions_ECCPBasePropertyCompanies_PropertyCompanyId",
                        column: x => x.PropertyCompanyId,
                        principalTable: "ECCPBasePropertyCompanies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EccpMaintenanceCompanyExtensions_MaintenanceCompanyId",
                table: "EccpMaintenanceCompanyExtensions",
                column: "MaintenanceCompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_EccpPropertyCompanyExtensions_PropertyCompanyId",
                table: "EccpPropertyCompanyExtensions",
                column: "PropertyCompanyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EccpMaintenanceCompanyExtensions");

            migrationBuilder.DropTable(
                name: "EccpPropertyCompanyExtensions");

            migrationBuilder.CreateTable(
                name: "MaintenanceCompanyExtensions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AptitudePhotoId = table.Column<Guid>(nullable: true),
                    BusinessLicenseId = table.Column<Guid>(nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    DeleterUserId = table.Column<long>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    LegalPerson = table.Column<string>(maxLength: 10, nullable: false),
                    MaintenanceCompanyId = table.Column<int>(nullable: false),
                    Mobile = table.Column<string>(maxLength: 11, nullable: false)
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
                    AptitudePhotoId = table.Column<Guid>(nullable: true),
                    BusinessLicenseId = table.Column<Guid>(nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    DeleterUserId = table.Column<long>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    LegalPerson = table.Column<string>(maxLength: 10, nullable: false),
                    Mobile = table.Column<string>(maxLength: 11, nullable: false),
                    PropertyCompanyId = table.Column<int>(nullable: false)
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
    }
}
