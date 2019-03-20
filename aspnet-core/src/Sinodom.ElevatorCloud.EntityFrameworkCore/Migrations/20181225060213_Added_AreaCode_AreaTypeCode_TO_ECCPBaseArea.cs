using Microsoft.EntityFrameworkCore.Migrations;

namespace Sinodom.ElevatorCloud.Migrations
{
    public partial class Added_AreaCode_AreaTypeCode_TO_ECCPBaseArea : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AreaCode",
                table: "ECCPBaseAreas",
                maxLength: 12,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AreaTypeCode",
                table: "ECCPBaseAreas",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AreaCode",
                table: "ECCPBaseAreas");

            migrationBuilder.DropColumn(
                name: "AreaTypeCode",
                table: "ECCPBaseAreas");
        }
    }
}
