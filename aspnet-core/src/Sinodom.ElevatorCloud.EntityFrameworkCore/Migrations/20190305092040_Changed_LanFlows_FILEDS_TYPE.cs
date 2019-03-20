using Microsoft.EntityFrameworkCore.Migrations;

namespace Sinodom.ElevatorCloud.Migrations
{
    public partial class Changed_LanFlows_FILEDS_TYPE : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ArgumentValue",
                table: "LanFlowStatusActions",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "ActionValue",
                table: "LanFlowInstanceOperationHistories",
                nullable: false,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ArgumentValue",
                table: "LanFlowStatusActions",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(int),
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<int>(
                name: "ActionValue",
                table: "LanFlowInstanceOperationHistories",
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
