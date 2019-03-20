using Microsoft.EntityFrameworkCore.Migrations;

namespace Sinodom.ElevatorCloud.Migrations
{
    public partial class Added_FIELD_MustDo_SpareNodeId_TO_EccpMaintenanceTemplateNode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "MustDo",
                table: "EccpMaintenanceTemplateNodes",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "SpareNodeId",
                table: "EccpMaintenanceTemplateNodes",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MustDo",
                table: "EccpMaintenanceTemplateNodes");

            migrationBuilder.DropColumn(
                name: "SpareNodeId",
                table: "EccpMaintenanceTemplateNodes");
        }
    }
}
