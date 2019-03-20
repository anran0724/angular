using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sinodom.ElevatorCloud.Migrations
{
    public partial class CertificateFrontPictureId_CertificateBackPictureId_ExpirationDate_TO_EccpCompanyUserExtensions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CertificateBackPictureId",
                table: "EccpCompanyUserExtensions",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CertificateFrontPictureId",
                table: "EccpCompanyUserExtensions",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpirationDate",
                table: "EccpCompanyUserExtensions",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CertificateBackPictureId",
                table: "EccpCompanyUserExtensions");

            migrationBuilder.DropColumn(
                name: "CertificateFrontPictureId",
                table: "EccpCompanyUserExtensions");

            migrationBuilder.DropColumn(
                name: "ExpirationDate",
                table: "EccpCompanyUserExtensions");
        }
    }
}
