using Microsoft.EntityFrameworkCore.Migrations;

namespace Sinodom.ElevatorCloud.Migrations
{
    public partial class Added_ECCPEdition_Entity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ECCPEditionsTypeId",
                table: "AbpEditions",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AbpEditions_ECCPEditionsTypeId",
                table: "AbpEditions",
                column: "ECCPEditionsTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_AbpEditions_ECCPEditionsTypes_ECCPEditionsTypeId",
                table: "AbpEditions",
                column: "ECCPEditionsTypeId",
                principalTable: "ECCPEditionsTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AbpEditions_ECCPEditionsTypes_ECCPEditionsTypeId",
                table: "AbpEditions");

            migrationBuilder.DropIndex(
                name: "IX_AbpEditions_ECCPEditionsTypeId",
                table: "AbpEditions");

            migrationBuilder.DropColumn(
                name: "ECCPEditionsTypeId",
                table: "AbpEditions");
        }
    }
}
