using Microsoft.EntityFrameworkCore.Migrations;

namespace Sinodom.ElevatorCloud.Migrations
{
    public partial class Added_IsMember_TO_EccpPropertyCompanyExtensions_EccpMaintenanceCompanyExtensions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsMember",
                table: "EccpPropertyCompanyExtensions",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsMember",
                table: "EccpMaintenanceCompanyExtensions",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsMember",
                table: "EccpPropertyCompanyExtensions");

            migrationBuilder.DropColumn(
                name: "IsMember",
                table: "EccpMaintenanceCompanyExtensions");
        }
    }
}
