using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sinodom.ElevatorCloud.Migrations
{
    public partial class Regenerated_EccpMaintenancePlan_Template_Link1377 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EccpMaintenancePlan_Template_Links",
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
                    MaintenancePlanId = table.Column<int>(nullable: false),
                    MaintenanceTemplateId = table.Column<int>(nullable: false),
                    MaintenanceTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EccpMaintenancePlan_Template_Links", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EccpMaintenancePlan_Template_Links_EccpMaintenancePlans_MaintenancePlanId",
                        column: x => x.MaintenancePlanId,
                        principalTable: "EccpMaintenancePlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EccpMaintenancePlan_Template_Links_EccpMaintenanceTemplates_MaintenanceTemplateId",
                        column: x => x.MaintenanceTemplateId,
                        principalTable: "EccpMaintenanceTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EccpMaintenancePlan_Template_Links_EccpDictMaintenanceTypes_MaintenanceTypeId",
                        column: x => x.MaintenanceTypeId,
                        principalTable: "EccpDictMaintenanceTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EccpMaintenancePlan_Template_Links_MaintenancePlanId",
                table: "EccpMaintenancePlan_Template_Links",
                column: "MaintenancePlanId");

            migrationBuilder.CreateIndex(
                name: "IX_EccpMaintenancePlan_Template_Links_MaintenanceTemplateId",
                table: "EccpMaintenancePlan_Template_Links",
                column: "MaintenanceTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_EccpMaintenancePlan_Template_Links_MaintenanceTypeId",
                table: "EccpMaintenancePlan_Template_Links",
                column: "MaintenanceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_EccpMaintenancePlan_Template_Links_TenantId",
                table: "EccpMaintenancePlan_Template_Links",
                column: "TenantId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EccpMaintenancePlan_Template_Links");
        }
    }
}
