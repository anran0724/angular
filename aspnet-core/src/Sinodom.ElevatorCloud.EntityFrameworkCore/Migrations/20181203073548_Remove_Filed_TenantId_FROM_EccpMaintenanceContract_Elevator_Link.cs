using Microsoft.EntityFrameworkCore.Migrations;

namespace Sinodom.ElevatorCloud.Migrations
{
    public partial class Remove_Filed_TenantId_FROM_EccpMaintenanceContract_Elevator_Link : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_EccpMaintenanceContract_Elevator_Links_TenantId",
                table: "EccpMaintenanceContract_Elevator_Links");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "EccpMaintenanceContract_Elevator_Links");

            migrationBuilder.AlterColumn<string>(
                name: "Mobile",
                table: "EccpCompanyUserExtensions",
                maxLength: 11,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "IdCard",
                table: "EccpCompanyUserExtensions",
                maxLength: 18,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TenantId",
                table: "EccpMaintenanceContract_Elevator_Links",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Mobile",
                table: "EccpCompanyUserExtensions",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 11);

            migrationBuilder.AlterColumn<string>(
                name: "IdCard",
                table: "EccpCompanyUserExtensions",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 18);

            migrationBuilder.CreateIndex(
                name: "IX_EccpMaintenanceContract_Elevator_Links_TenantId",
                table: "EccpMaintenanceContract_Elevator_Links",
                column: "TenantId");
        }
    }
}
