using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sinodom.ElevatorCloud.Migrations
{
    public partial class Added_EccpMaintenanceTemplateNode_DictMaintenanceItem_Link : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EccpMaintenanceTemplateNode_DictMaintenanceItem_Links",
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
                    MaintenanceTemplateNodeId = table.Column<long>(nullable: false),
                    MaintenanceTemplateNodeId1 = table.Column<int>(nullable: true),
                    DictMaintenanceItemId = table.Column<int>(nullable: false),
                    Sort = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EccpMaintenanceTemplateNode_DictMaintenanceItem_Links", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EccpMaintenanceTemplateNode_DictMaintenanceItem_Links_EccpDictMaintenanceItems_DictMaintenanceItemId",
                        column: x => x.DictMaintenanceItemId,
                        principalTable: "EccpDictMaintenanceItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EccpMaintenanceTemplateNode_DictMaintenanceItem_Links_EccpMaintenanceTemplateNodes_MaintenanceTemplateNodeId1",
                        column: x => x.MaintenanceTemplateNodeId1,
                        principalTable: "EccpMaintenanceTemplateNodes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EccpMaintenanceTemplateNode_DictMaintenanceItem_Links_DictMaintenanceItemId",
                table: "EccpMaintenanceTemplateNode_DictMaintenanceItem_Links",
                column: "DictMaintenanceItemId");

            migrationBuilder.CreateIndex(
                name: "IX_EccpMaintenanceTemplateNode_DictMaintenanceItem_Links_MaintenanceTemplateNodeId1",
                table: "EccpMaintenanceTemplateNode_DictMaintenanceItem_Links",
                column: "MaintenanceTemplateNodeId1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EccpMaintenanceTemplateNode_DictMaintenanceItem_Links");
        }
    }
}
