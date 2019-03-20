using Microsoft.EntityFrameworkCore.Migrations;

namespace Sinodom.ElevatorCloud.Migrations
{
    public partial class Added_IsClosed_TO_EccpMaintenanceWorkOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsClosed",
                table: "EccpMaintenanceWorkOrders",
                nullable: false,
                defaultValue: false);

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsClosed",
                table: "EccpMaintenanceWorkOrders");

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
    }
}
