using Microsoft.EntityFrameworkCore.Migrations;

namespace Sinodom.ElevatorCloud.Migrations
{
    public partial class Removed_MaintenanceTemplateNodeId_FROM_EccpMaintenanceWork : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EccpMaintenanceWorks_EccpMaintenanceTemplateNodes_MaintenanceTemplateNodeId",
                table: "EccpMaintenanceWorks");

            migrationBuilder.DropIndex(
                name: "IX_EccpMaintenanceWorks_MaintenanceTemplateNodeId",
                table: "EccpMaintenanceWorks");

            migrationBuilder.DropColumn(
                name: "MaintenanceTemplateNodeId",
                table: "EccpMaintenanceWorks");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MaintenanceTemplateNodeId",
                table: "EccpMaintenanceWorks",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EccpMaintenanceWorks_MaintenanceTemplateNodeId",
                table: "EccpMaintenanceWorks",
                column: "MaintenanceTemplateNodeId");

            migrationBuilder.AddForeignKey(
                name: "FK_EccpMaintenanceWorks_EccpMaintenanceTemplateNodes_MaintenanceTemplateNodeId",
                table: "EccpMaintenanceWorks",
                column: "MaintenanceTemplateNodeId",
                principalTable: "EccpMaintenanceTemplateNodes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
