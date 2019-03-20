using Microsoft.EntityFrameworkCore.Migrations;

namespace Sinodom.ElevatorCloud.Migrations
{
    public partial class Added_FIELD_IsAudit_TO_Company : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAudit",
                table: "ECCPBasePropertyCompanies",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsAudit",
                table: "ECCPBaseMaintenanceCompanies",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAudit",
                table: "ECCPBasePropertyCompanies");

            migrationBuilder.DropColumn(
                name: "IsAudit",
                table: "ECCPBaseMaintenanceCompanies");
        }
    }
}
