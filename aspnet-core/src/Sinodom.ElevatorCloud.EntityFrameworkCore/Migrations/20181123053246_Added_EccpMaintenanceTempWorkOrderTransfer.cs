using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sinodom.ElevatorCloud.Migrations
{
    public partial class Added_EccpMaintenanceTempWorkOrderTransfer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EccpMaintenanceTempWorkOrderTransfers",
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
                    MaintenanceTempWorkOrderId = table.Column<Guid>(nullable: true),
                    IsApproved = table.Column<bool>(nullable: false),
                    Remark = table.Column<string>(nullable: true),
                    TransferUserId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EccpMaintenanceTempWorkOrderTransfers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EccpMaintenanceTempWorkOrderTransfers_EccpMaintenanceTempWorkOrders_MaintenanceTempWorkOrderId",
                        column: x => x.MaintenanceTempWorkOrderId,
                        principalTable: "EccpMaintenanceTempWorkOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EccpMaintenanceTempWorkOrderTransfers_AbpUsers_TransferUserId",
                        column: x => x.TransferUserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EccpMaintenanceTempWorkOrderTransfers_MaintenanceTempWorkOrderId",
                table: "EccpMaintenanceTempWorkOrderTransfers",
                column: "MaintenanceTempWorkOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_EccpMaintenanceTempWorkOrderTransfers_TransferUserId",
                table: "EccpMaintenanceTempWorkOrderTransfers",
                column: "TransferUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EccpMaintenanceTempWorkOrderTransfers");
        }
    }
}
