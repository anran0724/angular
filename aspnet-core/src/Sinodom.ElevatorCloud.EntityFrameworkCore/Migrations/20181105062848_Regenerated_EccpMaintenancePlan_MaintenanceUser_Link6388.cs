using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sinodom.ElevatorCloud.Migrations
{
    public partial class Regenerated_EccpMaintenancePlan_MaintenanceUser_Link6388 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EccpMaintenancePlan_MaintenanceUser_Links",
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
                    UserId = table.Column<long>(nullable: false),
                    MaintenancePlanId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EccpMaintenancePlan_MaintenanceUser_Links", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EccpMaintenancePlan_MaintenanceUser_Links_EccpMaintenancePlans_MaintenancePlanId",
                        column: x => x.MaintenancePlanId,
                        principalTable: "EccpMaintenancePlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EccpMaintenancePlan_MaintenanceUser_Links_AbpUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EccpMaintenancePlan_MaintenanceUser_Links_MaintenancePlanId",
                table: "EccpMaintenancePlan_MaintenanceUser_Links",
                column: "MaintenancePlanId");

            migrationBuilder.CreateIndex(
                name: "IX_EccpMaintenancePlan_MaintenanceUser_Links_TenantId",
                table: "EccpMaintenancePlan_MaintenanceUser_Links",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_EccpMaintenancePlan_MaintenanceUser_Links_UserId",
                table: "EccpMaintenancePlan_MaintenanceUser_Links",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EccpMaintenancePlan_MaintenanceUser_Links");
        }
    }
}
