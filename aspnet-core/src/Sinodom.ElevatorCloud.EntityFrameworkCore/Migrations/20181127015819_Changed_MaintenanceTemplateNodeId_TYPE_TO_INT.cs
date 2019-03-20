using Microsoft.EntityFrameworkCore.Migrations;

namespace Sinodom.ElevatorCloud.Migrations
{
    public partial class Changed_MaintenanceTemplateNodeId_TYPE_TO_INT : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EccpMaintenanceTemplateNode_DictMaintenanceItem_Links_EccpMaintenanceTemplateNodes_MaintenanceTemplateNodeId1",
                table: "EccpMaintenanceTemplateNode_DictMaintenanceItem_Links");

            migrationBuilder.DropIndex(
                name: "IX_EccpMaintenanceTemplateNode_DictMaintenanceItem_Links_MaintenanceTemplateNodeId1",
                table: "EccpMaintenanceTemplateNode_DictMaintenanceItem_Links");

            migrationBuilder.DropColumn(
                name: "MaintenanceTemplateNodeId1",
                table: "EccpMaintenanceTemplateNode_DictMaintenanceItem_Links");

            migrationBuilder.AlterColumn<int>(
                name: "MaintenanceTemplateNodeId",
                table: "EccpMaintenanceTemplateNode_DictMaintenanceItem_Links",
                nullable: false,
                oldClrType: typeof(long));

            migrationBuilder.CreateIndex(
                name: "IX_EccpMaintenanceTemplateNode_DictMaintenanceItem_Links_MaintenanceTemplateNodeId",
                table: "EccpMaintenanceTemplateNode_DictMaintenanceItem_Links",
                column: "MaintenanceTemplateNodeId");

            migrationBuilder.AddForeignKey(
                name: "FK_EccpMaintenanceTemplateNode_DictMaintenanceItem_Links_EccpMaintenanceTemplateNodes_MaintenanceTemplateNodeId",
                table: "EccpMaintenanceTemplateNode_DictMaintenanceItem_Links",
                column: "MaintenanceTemplateNodeId",
                principalTable: "EccpMaintenanceTemplateNodes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EccpMaintenanceTemplateNode_DictMaintenanceItem_Links_EccpMaintenanceTemplateNodes_MaintenanceTemplateNodeId",
                table: "EccpMaintenanceTemplateNode_DictMaintenanceItem_Links");

            migrationBuilder.DropIndex(
                name: "IX_EccpMaintenanceTemplateNode_DictMaintenanceItem_Links_MaintenanceTemplateNodeId",
                table: "EccpMaintenanceTemplateNode_DictMaintenanceItem_Links");

            migrationBuilder.AlterColumn<long>(
                name: "MaintenanceTemplateNodeId",
                table: "EccpMaintenanceTemplateNode_DictMaintenanceItem_Links",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "MaintenanceTemplateNodeId1",
                table: "EccpMaintenanceTemplateNode_DictMaintenanceItem_Links",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EccpMaintenanceTemplateNode_DictMaintenanceItem_Links_MaintenanceTemplateNodeId1",
                table: "EccpMaintenanceTemplateNode_DictMaintenanceItem_Links",
                column: "MaintenanceTemplateNodeId1");

            migrationBuilder.AddForeignKey(
                name: "FK_EccpMaintenanceTemplateNode_DictMaintenanceItem_Links_EccpMaintenanceTemplateNodes_MaintenanceTemplateNodeId1",
                table: "EccpMaintenanceTemplateNode_DictMaintenanceItem_Links",
                column: "MaintenanceTemplateNodeId1",
                principalTable: "EccpMaintenanceTemplateNodes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
