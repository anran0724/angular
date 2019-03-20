using Microsoft.EntityFrameworkCore.Migrations;

namespace Sinodom.ElevatorCloud.Migrations
{
    public partial class Added_MaintenanceTemplateNodeId_To_EccpMaintenanceWorkFlow : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MaintenanceWorkId",
                table: "EccpMaintenanceWorkFlows",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_EccpMaintenanceWorkFlows_MaintenanceWorkId",
                table: "EccpMaintenanceWorkFlows",
                column: "MaintenanceWorkId");

            migrationBuilder.AddForeignKey(
                name: "FK_EccpMaintenanceWorkFlows_EccpMaintenanceWorks_MaintenanceWorkId",
                table: "EccpMaintenanceWorkFlows",
                column: "MaintenanceWorkId",
                principalTable: "EccpMaintenanceWorks",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EccpMaintenanceWorkFlows_EccpMaintenanceWorks_MaintenanceWorkId",
                table: "EccpMaintenanceWorkFlows");

            migrationBuilder.DropIndex(
                name: "IX_EccpMaintenanceWorkFlows_MaintenanceWorkId",
                table: "EccpMaintenanceWorkFlows");

            migrationBuilder.DropColumn(
                name: "MaintenanceWorkId",
                table: "EccpMaintenanceWorkFlows");
        }
    }
}
