using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sinodom.ElevatorCloud.Migrations
{
    public partial class Added_EccpMaintenanceWorkLog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Mobile",
                table: "EccpCompanyUserExtensions",
                maxLength: 11,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "IdCard",
                table: "EccpCompanyUserExtensions",
                maxLength: 18,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "EccpMaintenanceWorkLogs",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeleterUserId = table.Column<long>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    TenantId = table.Column<int>(nullable: false),
                    MaintenanceItemsName = table.Column<string>(maxLength: 50, nullable: true),
                    Remark = table.Column<string>(maxLength: 500, nullable: true),
                    MaintenanceWorkFlowId = table.Column<Guid>(nullable: false),
                    MaintenanceWorkFlowName = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EccpMaintenanceWorkLogs", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EccpMaintenanceWorkLogs_TenantId",
                table: "EccpMaintenanceWorkLogs",
                column: "TenantId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EccpMaintenanceWorkLogs");

            migrationBuilder.AlterColumn<string>(
                name: "Mobile",
                table: "EccpCompanyUserExtensions",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 11);

            migrationBuilder.AlterColumn<string>(
                name: "IdCard",
                table: "EccpCompanyUserExtensions",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 18);
        }
    }
}
