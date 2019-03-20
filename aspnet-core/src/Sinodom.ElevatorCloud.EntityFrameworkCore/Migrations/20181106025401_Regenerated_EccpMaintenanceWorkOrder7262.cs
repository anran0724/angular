using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sinodom.ElevatorCloud.Migrations
{
    public partial class Regenerated_EccpMaintenanceWorkOrder7262 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EccpMaintenanceWorkOrders",
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
                    IsPassed = table.Column<bool>(nullable: false),
                    Remark = table.Column<string>(maxLength: 500, nullable: true),
                    Longitude = table.Column<double>(nullable: true),
                    Latitude = table.Column<double>(nullable: true),
                    PlanCheckDate = table.Column<DateTime>(nullable: false),
                    MaintenancePlanId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EccpMaintenanceWorkOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EccpMaintenanceWorkOrders_EccpMaintenancePlans_MaintenancePlanId",
                        column: x => x.MaintenancePlanId,
                        principalTable: "EccpMaintenancePlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EccpMaintenanceWorkOrders_MaintenancePlanId",
                table: "EccpMaintenanceWorkOrders",
                column: "MaintenancePlanId");

            migrationBuilder.CreateIndex(
                name: "IX_EccpMaintenanceWorkOrders_TenantId",
                table: "EccpMaintenanceWorkOrders",
                column: "TenantId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EccpMaintenanceWorkOrders");
        }
    }
}
