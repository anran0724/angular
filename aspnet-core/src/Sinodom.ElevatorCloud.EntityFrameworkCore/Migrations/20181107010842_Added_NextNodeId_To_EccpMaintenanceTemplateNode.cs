using Microsoft.EntityFrameworkCore.Migrations;

namespace Sinodom.ElevatorCloud.Migrations
{
    public partial class Added_NextNodeId_To_EccpMaintenanceTemplateNode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NextNodeId",
                table: "EccpMaintenanceTemplateNodes",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NextNodeId",
                table: "EccpMaintenanceTemplateNodes");
        }
    }
}
