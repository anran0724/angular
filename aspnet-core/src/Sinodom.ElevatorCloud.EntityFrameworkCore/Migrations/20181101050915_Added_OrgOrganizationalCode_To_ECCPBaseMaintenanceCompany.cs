using Microsoft.EntityFrameworkCore.Migrations;

namespace Sinodom.ElevatorCloud.Migrations
{
    public partial class Added_OrgOrganizationalCode_To_ECCPBaseMaintenanceCompany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OrgOrganizationalCode",
                table: "ECCPBaseMaintenanceCompanies",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrgOrganizationalCode",
                table: "ECCPBaseMaintenanceCompanies");
        }
    }
}
