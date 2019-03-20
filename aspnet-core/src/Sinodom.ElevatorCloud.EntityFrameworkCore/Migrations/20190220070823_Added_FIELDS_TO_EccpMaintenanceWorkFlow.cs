using Microsoft.EntityFrameworkCore.Migrations;

namespace Sinodom.ElevatorCloud.Migrations
{
    public partial class Added_FIELDS_TO_EccpMaintenanceWorkFlow : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Latitude",
                table: "EccpMaintenanceWorkFlows",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Longitude",
                table: "EccpMaintenanceWorkFlows",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "EccpMaintenanceWorkFlows");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "EccpMaintenanceWorkFlows");
        }
    }
}
