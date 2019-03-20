using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sinodom.ElevatorCloud.Migrations
{
    public partial class Added_EccpMaintenanceTempWorkOrderTransferAuditLog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EccpMaintenanceTempWorkOrderTransferAuditLogs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeleterUserId = table.Column<long>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    MaintenanceTempWorkOrderTransferId = table.Column<int>(nullable: false),
                    IsApproved = table.Column<bool>(nullable: false),
                    Remark = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EccpMaintenanceTempWorkOrderTransferAuditLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EccpMaintenanceTempWorkOrderTransferAuditLogs_EccpMaintenanceTempWorkOrderTransfers_MaintenanceTempWorkOrderTransferId",
                        column: x => x.MaintenanceTempWorkOrderTransferId,
                        principalTable: "EccpMaintenanceTempWorkOrderTransfers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EccpMaintenanceTempWorkOrderTransferAuditLogs_MaintenanceTempWorkOrderTransferId",
                table: "EccpMaintenanceTempWorkOrderTransferAuditLogs",
                column: "MaintenanceTempWorkOrderTransferId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EccpMaintenanceTempWorkOrderTransferAuditLogs");
        }
    }
}
