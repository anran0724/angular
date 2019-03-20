using Microsoft.EntityFrameworkCore.Migrations;

namespace Sinodom.ElevatorCloud.Migrations
{
    public partial class Changed_Links_TYPE_TO_INTNULL : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "TenantId",
                table: "EccpMaintenanceTemplateNode_DictMaintenanceItem_Links",
                nullable: true,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "TenantId",
                table: "EccpMaintenanceTemplateNode_DictMaintenanceItem_Links",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}
