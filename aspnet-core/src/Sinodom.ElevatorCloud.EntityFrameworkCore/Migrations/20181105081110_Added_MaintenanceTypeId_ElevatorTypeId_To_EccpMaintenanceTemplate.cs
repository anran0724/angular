using Microsoft.EntityFrameworkCore.Migrations;

namespace Sinodom.ElevatorCloud.Migrations
{
    public partial class Added_MaintenanceTypeId_ElevatorTypeId_To_EccpMaintenanceTemplate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ElevatorTypeId",
                table: "EccpMaintenanceTemplates",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MaintenanceTypeId",
                table: "EccpMaintenanceTemplates",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_EccpMaintenanceTemplates_ElevatorTypeId",
                table: "EccpMaintenanceTemplates",
                column: "ElevatorTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_EccpMaintenanceTemplates_MaintenanceTypeId",
                table: "EccpMaintenanceTemplates",
                column: "MaintenanceTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_EccpMaintenanceTemplates_EccpDictElevatorTypes_ElevatorTypeId",
                table: "EccpMaintenanceTemplates",
                column: "ElevatorTypeId",
                principalTable: "EccpDictElevatorTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EccpMaintenanceTemplates_EccpDictMaintenanceTypes_MaintenanceTypeId",
                table: "EccpMaintenanceTemplates",
                column: "MaintenanceTypeId",
                principalTable: "EccpDictMaintenanceTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EccpMaintenanceTemplates_EccpDictElevatorTypes_ElevatorTypeId",
                table: "EccpMaintenanceTemplates");

            migrationBuilder.DropForeignKey(
                name: "FK_EccpMaintenanceTemplates_EccpDictMaintenanceTypes_MaintenanceTypeId",
                table: "EccpMaintenanceTemplates");

            migrationBuilder.DropIndex(
                name: "IX_EccpMaintenanceTemplates_ElevatorTypeId",
                table: "EccpMaintenanceTemplates");

            migrationBuilder.DropIndex(
                name: "IX_EccpMaintenanceTemplates_MaintenanceTypeId",
                table: "EccpMaintenanceTemplates");

            migrationBuilder.DropColumn(
                name: "ElevatorTypeId",
                table: "EccpMaintenanceTemplates");

            migrationBuilder.DropColumn(
                name: "MaintenanceTypeId",
                table: "EccpMaintenanceTemplates");
        }
    }
}
