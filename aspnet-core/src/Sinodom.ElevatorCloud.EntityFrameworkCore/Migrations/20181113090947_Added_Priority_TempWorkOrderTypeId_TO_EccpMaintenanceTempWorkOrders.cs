using Microsoft.EntityFrameworkCore.Migrations;

namespace Sinodom.ElevatorCloud.Migrations
{
    public partial class Added_Priority_TempWorkOrderTypeId_TO_EccpMaintenanceTempWorkOrders : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Priority",
                table: "EccpMaintenanceTempWorkOrders",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TempWorkOrderTypeId",
                table: "EccpMaintenanceTempWorkOrders",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_EccpMaintenanceTempWorkOrders_TempWorkOrderTypeId",
                table: "EccpMaintenanceTempWorkOrders",
                column: "TempWorkOrderTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_EccpMaintenanceTempWorkOrders_EccpDictTempWorkOrderTypes_TempWorkOrderTypeId",
                table: "EccpMaintenanceTempWorkOrders",
                column: "TempWorkOrderTypeId",
                principalTable: "EccpDictTempWorkOrderTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EccpMaintenanceTempWorkOrders_EccpDictTempWorkOrderTypes_TempWorkOrderTypeId",
                table: "EccpMaintenanceTempWorkOrders");

            migrationBuilder.DropIndex(
                name: "IX_EccpMaintenanceTempWorkOrders_TempWorkOrderTypeId",
                table: "EccpMaintenanceTempWorkOrders");

            migrationBuilder.DropColumn(
                name: "Priority",
                table: "EccpMaintenanceTempWorkOrders");

            migrationBuilder.DropColumn(
                name: "TempWorkOrderTypeId",
                table: "EccpMaintenanceTempWorkOrders");
        }
    }
}
