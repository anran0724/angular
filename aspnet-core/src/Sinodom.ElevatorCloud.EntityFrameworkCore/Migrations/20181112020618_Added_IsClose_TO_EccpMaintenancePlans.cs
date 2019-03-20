using Microsoft.EntityFrameworkCore.Migrations;

namespace Sinodom.ElevatorCloud.Migrations
{
    public partial class Added_IsClose_TO_EccpMaintenancePlans : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsClose",
                table: "EccpMaintenancePlans",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsClose",
                table: "EccpMaintenancePlans");
        }
    }
}
