using Microsoft.EntityFrameworkCore.Migrations;

namespace Sinodom.ElevatorCloud.Migrations
{
    public partial class Changed_LanFlows_FILEDS_TO_INT : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "StatusValue",
                table: "LanFlowStatusActions",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "StatusValue",
                table: "LanFlowInstances",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<int>(
                name: "StatusValue",
                table: "LanFlowInstanceOperationHistories",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ActionValue",
                table: "LanFlowInstanceOperationHistories",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 20);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "StatusValue",
                table: "LanFlowStatusActions",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "StatusValue",
                table: "LanFlowInstances",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "StatusValue",
                table: "LanFlowInstanceOperationHistories",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "ActionValue",
                table: "LanFlowInstanceOperationHistories",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(int));
        }
    }
}
