using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sinodom.ElevatorCloud.Migrations
{
    public partial class Added_EccpMaintenanceTempWorkOrderActionLog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EccpMaintenanceTempWorkOrderActionLogs",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeleterUserId = table.Column<long>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    TenantId = table.Column<int>(nullable: false),
                    Remarks = table.Column<string>(maxLength: 500, nullable: true),
                    CheckState = table.Column<int>(nullable: false),
                    TempWorkOrderId = table.Column<Guid>(nullable: false),
                    UserId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EccpMaintenanceTempWorkOrderActionLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EccpMaintenanceTempWorkOrderActionLogs_EccpMaintenanceTempWorkOrders_TempWorkOrderId",
                        column: x => x.TempWorkOrderId,
                        principalTable: "EccpMaintenanceTempWorkOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EccpMaintenanceTempWorkOrderActionLogs_AbpUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EccpMaintenanceTempWorkOrderActionLogs_TempWorkOrderId",
                table: "EccpMaintenanceTempWorkOrderActionLogs",
                column: "TempWorkOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_EccpMaintenanceTempWorkOrderActionLogs_TenantId",
                table: "EccpMaintenanceTempWorkOrderActionLogs",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_EccpMaintenanceTempWorkOrderActionLogs_UserId",
                table: "EccpMaintenanceTempWorkOrderActionLogs",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EccpMaintenanceTempWorkOrderActionLogs");
        }
    }
}
