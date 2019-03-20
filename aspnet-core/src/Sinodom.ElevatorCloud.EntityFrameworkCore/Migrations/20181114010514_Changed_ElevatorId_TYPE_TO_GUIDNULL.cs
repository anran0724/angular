using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sinodom.ElevatorCloud.Migrations
{
    public partial class Changed_ElevatorId_TYPE_TO_GUIDNULL : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EccpElevatorQrCodes_EccpBaseElevators_ElevatorId",
                table: "EccpElevatorQrCodes");

            migrationBuilder.AlterColumn<Guid>(
                name: "ElevatorId",
                table: "EccpElevatorQrCodes",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddForeignKey(
                name: "FK_EccpElevatorQrCodes_EccpBaseElevators_ElevatorId",
                table: "EccpElevatorQrCodes",
                column: "ElevatorId",
                principalTable: "EccpBaseElevators",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EccpElevatorQrCodes_EccpBaseElevators_ElevatorId",
                table: "EccpElevatorQrCodes");

            migrationBuilder.AlterColumn<Guid>(
                name: "ElevatorId",
                table: "EccpElevatorQrCodes",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_EccpElevatorQrCodes_EccpBaseElevators_ElevatorId",
                table: "EccpElevatorQrCodes",
                column: "ElevatorId",
                principalTable: "EccpBaseElevators",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
