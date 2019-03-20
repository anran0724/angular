using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sinodom.ElevatorCloud.Migrations
{
    public partial class Changed_LanFlows_TABLES : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LanFlowInstanceOperationHistories_LanFlowInstances_InstanceId",
                table: "LanFlowInstanceOperationHistories");

            migrationBuilder.DropTable(
                name: "LanFlowInstance_WorkOrder_Links");

            migrationBuilder.DropTable(
                name: "LanFlowInstances");

            migrationBuilder.RenameColumn(
                name: "InstanceId",
                table: "LanFlowInstanceOperationHistories",
                newName: "FlowStatusActionId");

            migrationBuilder.RenameIndex(
                name: "IX_LanFlowInstanceOperationHistories_InstanceId",
                table: "LanFlowInstanceOperationHistories",
                newName: "IX_LanFlowInstanceOperationHistories_FlowStatusActionId");

            migrationBuilder.AddColumn<string>(
                name: "Field",
                table: "LanFlowInstanceOperationHistories",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ObjectId",
                table: "LanFlowInstanceOperationHistories",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TaskDescription",
                table: "LanFlowInstanceOperationHistories",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_LanFlowInstanceOperationHistories_LanFlowStatusActions_FlowStatusActionId",
                table: "LanFlowInstanceOperationHistories",
                column: "FlowStatusActionId",
                principalTable: "LanFlowStatusActions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LanFlowInstanceOperationHistories_LanFlowStatusActions_FlowStatusActionId",
                table: "LanFlowInstanceOperationHistories");

            migrationBuilder.DropColumn(
                name: "Field",
                table: "LanFlowInstanceOperationHistories");

            migrationBuilder.DropColumn(
                name: "ObjectId",
                table: "LanFlowInstanceOperationHistories");

            migrationBuilder.DropColumn(
                name: "TaskDescription",
                table: "LanFlowInstanceOperationHistories");

            migrationBuilder.RenameColumn(
                name: "FlowStatusActionId",
                table: "LanFlowInstanceOperationHistories",
                newName: "InstanceId");

            migrationBuilder.RenameIndex(
                name: "IX_LanFlowInstanceOperationHistories_FlowStatusActionId",
                table: "LanFlowInstanceOperationHistories",
                newName: "IX_LanFlowInstanceOperationHistories_InstanceId");

            migrationBuilder.CreateTable(
                name: "LanFlowInstances",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    DeleterUserId = table.Column<long>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    ElevatorId = table.Column<Guid>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LanFlowSchemeId = table.Column<int>(nullable: false),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    SchemeContent = table.Column<string>(nullable: false),
                    StatusName = table.Column<string>(maxLength: 50, nullable: false),
                    StatusValue = table.Column<int>(nullable: false),
                    TaskDescription = table.Column<string>(maxLength: 500, nullable: true),
                    TenantId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LanFlowInstances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LanFlowInstances_EccpBaseElevators_ElevatorId",
                        column: x => x.ElevatorId,
                        principalTable: "EccpBaseElevators",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LanFlowInstances_LanFlowSchemes_LanFlowSchemeId",
                        column: x => x.LanFlowSchemeId,
                        principalTable: "LanFlowSchemes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LanFlowInstance_WorkOrder_Links",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    DeleterUserId = table.Column<long>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    InstanceId = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    MaintenanceWorkOrderId = table.Column<int>(nullable: false),
                    TenantId = table.Column<int>(nullable: false)
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

            migrationBuilder.CreateIndex(
                name: "IX_LanFlowInstances_ElevatorId",
                table: "LanFlowInstances",
                column: "ElevatorId");

            migrationBuilder.CreateIndex(
                name: "IX_LanFlowInstances_LanFlowSchemeId",
                table: "LanFlowInstances",
                column: "LanFlowSchemeId");

            migrationBuilder.AddForeignKey(
                name: "FK_LanFlowInstanceOperationHistories_LanFlowInstances_InstanceId",
                table: "LanFlowInstanceOperationHistories",
                column: "InstanceId",
                principalTable: "LanFlowInstances",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
