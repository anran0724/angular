using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sinodom.ElevatorCloud.Migrations
{
    public partial class Added_EccpMaintenanceWorkFlow : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EccpMaintenanceWorkFlows",
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
                    ActionCodeValue = table.Column<string>(maxLength: 25, nullable: true),
                    Remark = table.Column<string>(maxLength: 500, nullable: true),
                    MaintenanceTemplateNodeId = table.Column<int>(nullable: false),
                    DictMaintenanceWorkFlowStatusId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EccpMaintenanceWorkFlows", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EccpMaintenanceWorkFlows_EccpDictMaintenanceWorkFlowStatuses_DictMaintenanceWorkFlowStatusId",
                        column: x => x.DictMaintenanceWorkFlowStatusId,
                        principalTable: "EccpDictMaintenanceWorkFlowStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EccpMaintenanceWorkFlows_EccpMaintenanceTemplateNodes_MaintenanceTemplateNodeId",
                        column: x => x.MaintenanceTemplateNodeId,
                        principalTable: "EccpMaintenanceTemplateNodes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EccpMaintenanceWorkFlows_DictMaintenanceWorkFlowStatusId",
                table: "EccpMaintenanceWorkFlows",
                column: "DictMaintenanceWorkFlowStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_EccpMaintenanceWorkFlows_MaintenanceTemplateNodeId",
                table: "EccpMaintenanceWorkFlows",
                column: "MaintenanceTemplateNodeId");

            migrationBuilder.CreateIndex(
                name: "IX_EccpMaintenanceWorkFlows_TenantId",
                table: "EccpMaintenanceWorkFlows",
                column: "TenantId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EccpMaintenanceWorkFlows");
        }
    }
}
