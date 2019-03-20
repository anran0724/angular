using Microsoft.EntityFrameworkCore.Migrations;

namespace Sinodom.ElevatorCloud.Migrations
{
    public partial class Regenerated_EccpMaintenanceWorkOrder7242 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MaintenanceStatusId",
                table: "EccpMaintenanceWorkOrders",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MaintenanceTypeId",
                table: "EccpMaintenanceWorkOrders",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_EccpMaintenanceWorkOrders_MaintenanceStatusId",
                table: "EccpMaintenanceWorkOrders",
                column: "MaintenanceStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_EccpMaintenanceWorkOrders_MaintenanceTypeId",
                table: "EccpMaintenanceWorkOrders",
                column: "MaintenanceTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_EccpMaintenanceWorkOrders_EccpDictMaintenanceStatuses_MaintenanceStatusId",
                table: "EccpMaintenanceWorkOrders",
                column: "MaintenanceStatusId",
                principalTable: "EccpDictMaintenanceStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EccpMaintenanceWorkOrders_EccpDictMaintenanceTypes_MaintenanceTypeId",
                table: "EccpMaintenanceWorkOrders",
                column: "MaintenanceTypeId",
                principalTable: "EccpDictMaintenanceTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EccpMaintenanceWorkOrders_EccpDictMaintenanceStatuses_MaintenanceStatusId",
                table: "EccpMaintenanceWorkOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_EccpMaintenanceWorkOrders_EccpDictMaintenanceTypes_MaintenanceTypeId",
                table: "EccpMaintenanceWorkOrders");

            migrationBuilder.DropIndex(
                name: "IX_EccpMaintenanceWorkOrders_MaintenanceStatusId",
                table: "EccpMaintenanceWorkOrders");

            migrationBuilder.DropIndex(
                name: "IX_EccpMaintenanceWorkOrders_MaintenanceTypeId",
                table: "EccpMaintenanceWorkOrders");

            migrationBuilder.DropColumn(
                name: "MaintenanceStatusId",
                table: "EccpMaintenanceWorkOrders");

            migrationBuilder.DropColumn(
                name: "MaintenanceTypeId",
                table: "EccpMaintenanceWorkOrders");
        }
    }
}
