using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sinodom.ElevatorCloud.Migrations
{
    public partial class Added_LanFlowInstance_WorkOrder_Link : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LanFlowInstance_WorkOrder_Links",
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
                    InstanceId = table.Column<int>(nullable: false),
                    MaintenanceWorkOrderId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LanFlowInstance_WorkOrder_Links", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LanFlowInstance_WorkOrder_Links_LanFlowInstances_InstanceId",
                        column: x => x.InstanceId,
                        principalTable: "LanFlowInstances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LanFlowInstance_WorkOrder_Links_EccpMaintenanceWorkOrders_MaintenanceWorkOrderId",
                        column: x => x.MaintenanceWorkOrderId,
                        principalTable: "EccpMaintenanceWorkOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LanFlowInstance_WorkOrder_Links_InstanceId",
                table: "LanFlowInstance_WorkOrder_Links",
                column: "InstanceId");

            migrationBuilder.CreateIndex(
                name: "IX_LanFlowInstance_WorkOrder_Links_MaintenanceWorkOrderId",
                table: "LanFlowInstance_WorkOrder_Links",
                column: "MaintenanceWorkOrderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LanFlowInstance_WorkOrder_Links");
        }
    }
}
