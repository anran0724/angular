using Microsoft.EntityFrameworkCore.Migrations;

namespace Sinodom.ElevatorCloud.Migrations
{
    public partial class Added_Fileds_TO_WorkOrders : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "IsApproved",
                table: "EccpMaintenanceWorkOrderTransfers",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                table: "EccpMaintenanceTroubledWorkOrders",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsApproved",
                table: "EccpMaintenanceTempWorkOrderTransfers",
                nullable: true,
                oldClrType: typeof(bool));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Remarks",
                table: "EccpMaintenanceTroubledWorkOrders");

            migrationBuilder.AlterColumn<bool>(
                name: "IsApproved",
                table: "EccpMaintenanceWorkOrderTransfers",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsApproved",
                table: "EccpMaintenanceTempWorkOrderTransfers",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);
        }
    }
}
