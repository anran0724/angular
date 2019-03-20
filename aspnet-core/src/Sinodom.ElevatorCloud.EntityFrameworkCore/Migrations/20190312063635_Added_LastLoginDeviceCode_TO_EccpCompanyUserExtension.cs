using Microsoft.EntityFrameworkCore.Migrations;

namespace Sinodom.ElevatorCloud.Migrations
{
    public partial class Added_LastLoginDeviceCode_TO_EccpCompanyUserExtension : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LastLoginDeviceCode",
                table: "EccpCompanyUserExtensions",
                maxLength: 50,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastLoginDeviceCode",
                table: "EccpCompanyUserExtensions");
        }
    }
}
