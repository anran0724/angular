using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sinodom.ElevatorCloud.Migrations
{
    public partial class Added_FILEDS_TO_EccpCompanyUserExtension : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Heartbeat",
                table: "EccpCompanyUserExtensions",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsOnline",
                table: "EccpCompanyUserExtensions",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<float>(
                name: "Latitude",
                table: "EccpCompanyUserExtensions",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "Longitude",
                table: "EccpCompanyUserExtensions",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Heartbeat",
                table: "EccpCompanyUserExtensions");

            migrationBuilder.DropColumn(
                name: "IsOnline",
                table: "EccpCompanyUserExtensions");

            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "EccpCompanyUserExtensions");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "EccpCompanyUserExtensions");
        }
    }
}
