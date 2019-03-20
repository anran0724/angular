using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sinodom.ElevatorCloud.Migrations
{
    public partial class Added_EccpMaintenanceTemplateNode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EccpMaintenanceTemplateNodes",
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
                    TenantId = table.Column<int>(nullable: true),
                    ParentNodeId = table.Column<int>(nullable: true),
                    NodeName = table.Column<string>(maxLength: 50, nullable: false),
                    NodeDesc = table.Column<string>(maxLength: 250, nullable: true),
                    NodeIndex = table.Column<int>(nullable: false),
                    ActionCode = table.Column<string>(maxLength: 50, nullable: true),
                    MaintenanceTemplateId = table.Column<int>(nullable: false),
                    DictNodeTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EccpMaintenanceTemplateNodes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EccpMaintenanceTemplateNodes_EccpDictNodeTypes_DictNodeTypeId",
                        column: x => x.DictNodeTypeId,
                        principalTable: "EccpDictNodeTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EccpMaintenanceTemplateNodes_EccpMaintenanceTemplates_MaintenanceTemplateId",
                        column: x => x.MaintenanceTemplateId,
                        principalTable: "EccpMaintenanceTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EccpMaintenanceTemplateNodes_DictNodeTypeId",
                table: "EccpMaintenanceTemplateNodes",
                column: "DictNodeTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_EccpMaintenanceTemplateNodes_MaintenanceTemplateId",
                table: "EccpMaintenanceTemplateNodes",
                column: "MaintenanceTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_EccpMaintenanceTemplateNodes_TenantId",
                table: "EccpMaintenanceTemplateNodes",
                column: "TenantId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EccpMaintenanceTemplateNodes");
        }
    }
}
