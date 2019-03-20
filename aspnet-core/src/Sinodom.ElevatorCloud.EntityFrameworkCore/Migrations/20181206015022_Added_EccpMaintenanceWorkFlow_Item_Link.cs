using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sinodom.ElevatorCloud.Migrations
{
    public partial class Added_EccpMaintenanceWorkFlow_Item_Link : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EccpMaintenanceWorkFlow_Item_Links",
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
                    Remark = table.Column<string>(maxLength: 500, nullable: true),
                    MaintenanceWorkFlowId = table.Column<Guid>(nullable: true),
                    DictMaintenanceItemId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EccpMaintenanceWorkFlow_Item_Links", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EccpMaintenanceWorkFlow_Item_Links_EccpDictMaintenanceItems_DictMaintenanceItemId",
                        column: x => x.DictMaintenanceItemId,
                        principalTable: "EccpDictMaintenanceItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EccpMaintenanceWorkFlow_Item_Links_EccpMaintenanceWorkFlows_MaintenanceWorkFlowId",
                        column: x => x.MaintenanceWorkFlowId,
                        principalTable: "EccpMaintenanceWorkFlows",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EccpMaintenanceWorkFlow_Item_Links_DictMaintenanceItemId",
                table: "EccpMaintenanceWorkFlow_Item_Links",
                column: "DictMaintenanceItemId");

            migrationBuilder.CreateIndex(
                name: "IX_EccpMaintenanceWorkFlow_Item_Links_MaintenanceWorkFlowId",
                table: "EccpMaintenanceWorkFlow_Item_Links",
                column: "MaintenanceWorkFlowId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EccpMaintenanceWorkFlow_Item_Links");
        }
    }
}
