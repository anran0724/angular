using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sinodom.ElevatorCloud.Migrations
{
    public partial class Added_EccpMaintenanceWorkOrderTransfer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EccpMaintenanceWorkOrderTransfers",
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
                    TenantId = table.Column<int>(nullable: false),
                    MaintenanceWorkOrderId = table.Column<int>(nullable: false),
                    IsApproved = table.Column<bool>(nullable: false),
                    Remark = table.Column<string>(nullable: true),
                    TransferUserId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EccpMaintenanceWorkOrderTransfers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EccpMaintenanceWorkOrderTransfers_EccpMaintenanceWorkOrders_MaintenanceWorkOrderId",
                        column: x => x.MaintenanceWorkOrderId,
                        principalTable: "EccpMaintenanceWorkOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EccpMaintenanceWorkOrderTransfers_AbpUsers_TransferUserId",
                        column: x => x.TransferUserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EccpMaintenanceWorkOrderTransfers_MaintenanceWorkOrderId",
                table: "EccpMaintenanceWorkOrderTransfers",
                column: "MaintenanceWorkOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_EccpMaintenanceWorkOrderTransfers_TransferUserId",
                table: "EccpMaintenanceWorkOrderTransfers",
                column: "TransferUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EccpMaintenanceWorkOrderTransfers");
        }
    }
}
