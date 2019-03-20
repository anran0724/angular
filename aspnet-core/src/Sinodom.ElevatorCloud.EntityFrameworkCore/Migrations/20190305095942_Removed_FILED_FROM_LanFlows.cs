using Microsoft.EntityFrameworkCore.Migrations;

namespace Sinodom.ElevatorCloud.Migrations
{
    public partial class Removed_FILED_FROM_LanFlows : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "LanFlowStatusActions");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "LanFlowSchemes");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
