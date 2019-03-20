using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sinodom.ElevatorCloud.Migrations
{
    public partial class Added_EccpMaintenanceContract : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EccpMaintenanceContracts",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeleterUserId = table.Column<long>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    TenantId = table.Column<int>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    MaintenanceCompanyId = table.Column<int>(nullable: false),
                    PropertyCompanyId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EccpMaintenanceContracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EccpMaintenanceContracts_ECCPBaseMaintenanceCompanies_MaintenanceCompanyId",
                        column: x => x.MaintenanceCompanyId,
                        principalTable: "ECCPBaseMaintenanceCompanies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EccpMaintenanceContracts_ECCPBasePropertyCompanies_PropertyCompanyId",
                        column: x => x.PropertyCompanyId,
                        principalTable: "ECCPBasePropertyCompanies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EccpMaintenanceContracts_MaintenanceCompanyId",
                table: "EccpMaintenanceContracts",
                column: "MaintenanceCompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_EccpMaintenanceContracts_PropertyCompanyId",
                table: "EccpMaintenanceContracts",
                column: "PropertyCompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_EccpMaintenanceContracts_TenantId",
                table: "EccpMaintenanceContracts",
                column: "TenantId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EccpMaintenanceContracts");
        }
    }
}
