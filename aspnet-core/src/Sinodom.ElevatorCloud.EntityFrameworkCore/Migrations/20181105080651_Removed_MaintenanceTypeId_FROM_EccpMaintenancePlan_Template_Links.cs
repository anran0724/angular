using Microsoft.EntityFrameworkCore.Migrations;

namespace Sinodom.ElevatorCloud.Migrations
{
    public partial class Removed_MaintenanceTypeId_FROM_EccpMaintenancePlan_Template_Links : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EccpMaintenancePlan_Template_Links_EccpDictMaintenanceTypes_MaintenanceTypeId",
                table: "EccpMaintenancePlan_Template_Links");

            migrationBuilder.DropIndex(
                name: "IX_EccpMaintenancePlan_Template_Links_MaintenanceTypeId",
                table: "EccpMaintenancePlan_Template_Links");

            migrationBuilder.DropColumn(
                name: "MaintenanceTypeId",
                table: "EccpMaintenancePlan_Template_Links");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MaintenanceTypeId",
                table: "EccpMaintenancePlan_Template_Links",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_EccpMaintenancePlan_Template_Links_MaintenanceTypeId",
                table: "EccpMaintenancePlan_Template_Links",
                column: "MaintenanceTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_EccpMaintenancePlan_Template_Links_EccpDictMaintenanceTypes_MaintenanceTypeId",
                table: "EccpMaintenancePlan_Template_Links",
                column: "MaintenanceTypeId",
                principalTable: "EccpDictMaintenanceTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
