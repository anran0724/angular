using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sinodom.ElevatorCloud.Migrations
{
    public partial class Added_EccpMaintenanceTempWorkOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EccpMaintenanceTempWorkOrders",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    TenantId = table.Column<int>(nullable: false),
                    Title = table.Column<string>(maxLength: 50, nullable: false),
                    Describe = table.Column<string>(maxLength: 500, nullable: true),
                    CheckState = table.Column<int>(nullable: false),
                    CompletionTime = table.Column<DateTime>(nullable: true),
                    MaintenanceCompanyId = table.Column<int>(nullable: false),
                    UserId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EccpMaintenanceTempWorkOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EccpMaintenanceTempWorkOrders_ECCPBaseMaintenanceCompanies_MaintenanceCompanyId",
                        column: x => x.MaintenanceCompanyId,
                        principalTable: "ECCPBaseMaintenanceCompanies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EccpMaintenanceTempWorkOrders_AbpUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EccpMaintenanceTempWorkOrders_MaintenanceCompanyId",
                table: "EccpMaintenanceTempWorkOrders",
                column: "MaintenanceCompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_EccpMaintenanceTempWorkOrders_TenantId",
                table: "EccpMaintenanceTempWorkOrders",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_EccpMaintenanceTempWorkOrders_UserId",
                table: "EccpMaintenanceTempWorkOrders",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EccpMaintenanceTempWorkOrders");
        }
    }
}
