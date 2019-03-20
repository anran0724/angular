using Microsoft.EntityFrameworkCore.Migrations;

namespace Sinodom.ElevatorCloud.Migrations
{
    public partial class Added_FILEDS_TO_LanFlows : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TenantId",
                table: "LanFlowStatusActions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TenantId",
                table: "LanFlowSchemes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "TaskDescription",
                table: "LanFlowInstances",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TenantId",
                table: "LanFlowInstances",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TenantId",
                table: "LanFlowInstanceOperationHistories",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TenantId",
                table: "LanFlowInstance_WorkOrder_Links",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "LanFlowStatusActions");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "LanFlowSchemes");

            migrationBuilder.DropColumn(
                name: "TaskDescription",
                table: "LanFlowInstances");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "LanFlowInstances");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "LanFlowInstanceOperationHistories");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "LanFlowInstance_WorkOrder_Links");
        }
    }
}
