using Microsoft.EntityFrameworkCore.Migrations;

namespace Sinodom.ElevatorCloud.Migrations
{
    public partial class Added_SyncId_TO_Elevator_User_Company : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SyncCompanyId",
                table: "EccpPropertyCompanyExtensions",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SyncCompanyId",
                table: "EccpMaintenanceCompanyExtensions",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "IdCard",
                table: "EccpCompanyUserExtensions",
                maxLength: 18,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 10);

            migrationBuilder.AddColumn<int>(
                name: "SyncUserId",
                table: "EccpCompanyUserExtensions",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SyncElevatorId",
                table: "EccpBaseElevators",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SyncCompanyId",
                table: "EccpPropertyCompanyExtensions");

            migrationBuilder.DropColumn(
                name: "SyncCompanyId",
                table: "EccpMaintenanceCompanyExtensions");

            migrationBuilder.DropColumn(
                name: "SyncUserId",
                table: "EccpCompanyUserExtensions");

            migrationBuilder.DropColumn(
                name: "SyncElevatorId",
                table: "EccpBaseElevators");

            migrationBuilder.AlterColumn<string>(
                name: "IdCard",
                table: "EccpCompanyUserExtensions",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 18);
        }
    }
}
