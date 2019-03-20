using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sinodom.ElevatorCloud.Migrations
{
    public partial class Added_EccpMaintenanceTroubledWorkOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EccpMaintenanceTroubledWorkOrders",
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
                    WorkOrderStatusName = table.Column<string>(maxLength: 20, nullable: true),
                    TroubledDesc = table.Column<string>(maxLength: 500, nullable: true),
                    IsAudit = table.Column<int>(nullable: true),
                    MaintenanceWorkOrderId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EccpMaintenanceTroubledWorkOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EccpMaintenanceTroubledWorkOrders_EccpMaintenanceWorkOrders_MaintenanceWorkOrderId",
                        column: x => x.MaintenanceWorkOrderId,
                        principalTable: "EccpMaintenanceWorkOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EccpMaintenanceTroubledWorkOrders_MaintenanceWorkOrderId",
                table: "EccpMaintenanceTroubledWorkOrders",
                column: "MaintenanceWorkOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_EccpMaintenanceTroubledWorkOrders_TenantId",
                table: "EccpMaintenanceTroubledWorkOrders",
                column: "TenantId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EccpMaintenanceTroubledWorkOrders");
        }
    }
}
