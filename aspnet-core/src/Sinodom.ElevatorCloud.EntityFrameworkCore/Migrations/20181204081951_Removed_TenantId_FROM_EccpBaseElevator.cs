using Microsoft.EntityFrameworkCore.Migrations;

namespace Sinodom.ElevatorCloud.Migrations
{
    public partial class Removed_TenantId_FROM_EccpBaseElevator : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "EccpBaseElevatorSubsidiaryInfos");

            migrationBuilder.RenameColumn(
                name: "TenantId",
                table: "EccpBaseElevators",
                newName: "ECCPBasePropertyCompanyId");

            migrationBuilder.RenameIndex(
                name: "IX_EccpBaseElevators_TenantId",
                table: "EccpBaseElevators",
                newName: "IX_EccpBaseElevators_ECCPBasePropertyCompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_EccpBaseElevators_ECCPBasePropertyCompanies_ECCPBasePropertyCompanyId",
                table: "EccpBaseElevators",
                column: "ECCPBasePropertyCompanyId",
                principalTable: "ECCPBasePropertyCompanies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EccpBaseElevators_ECCPBasePropertyCompanies_ECCPBasePropertyCompanyId",
                table: "EccpBaseElevators");

            migrationBuilder.RenameColumn(
                name: "ECCPBasePropertyCompanyId",
                table: "EccpBaseElevators",
                newName: "TenantId");

            migrationBuilder.RenameIndex(
                name: "IX_EccpBaseElevators_ECCPBasePropertyCompanyId",
                table: "EccpBaseElevators",
                newName: "IX_EccpBaseElevators_TenantId");

            migrationBuilder.AddColumn<int>(
                name: "TenantId",
                table: "EccpBaseElevatorSubsidiaryInfos",
                nullable: true);
        }
    }
}
