using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sinodom.ElevatorCloud.Migrations
{
    public partial class Added_Fields_TO_EccpMaintenanceContract : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsStop",
                table: "EccpMaintenanceContracts",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "StopDate",
                table: "EccpMaintenanceContracts",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsStop",
                table: "EccpMaintenanceContracts");

            migrationBuilder.DropColumn(
                name: "StopDate",
                table: "EccpMaintenanceContracts");
        }
    }
}
