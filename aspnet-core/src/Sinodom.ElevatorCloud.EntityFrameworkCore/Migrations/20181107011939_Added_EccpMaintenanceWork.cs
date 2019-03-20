using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sinodom.ElevatorCloud.Migrations
{
    public partial class Added_EccpMaintenanceWork : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EccpMaintenanceWorks",
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
                    TaskName = table.Column<string>(maxLength: 50, nullable: false),
                    Remark = table.Column<string>(maxLength: 250, nullable: true),
                    MaintenanceWorkOrderId = table.Column<int>(nullable: false),
                    MaintenanceTemplateNodeId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EccpMaintenanceWorks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EccpMaintenanceWorks_EccpMaintenanceTemplateNodes_MaintenanceTemplateNodeId",
                        column: x => x.MaintenanceTemplateNodeId,
                        principalTable: "EccpMaintenanceTemplateNodes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EccpMaintenanceWorks_EccpMaintenanceWorkOrders_MaintenanceWorkOrderId",
                        column: x => x.MaintenanceWorkOrderId,
                        principalTable: "EccpMaintenanceWorkOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EccpMaintenanceWorks_MaintenanceTemplateNodeId",
                table: "EccpMaintenanceWorks",
                column: "MaintenanceTemplateNodeId");

            migrationBuilder.CreateIndex(
                name: "IX_EccpMaintenanceWorks_MaintenanceWorkOrderId",
                table: "EccpMaintenanceWorks",
                column: "MaintenanceWorkOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_EccpMaintenanceWorks_TenantId",
                table: "EccpMaintenanceWorks",
                column: "TenantId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EccpMaintenanceWorks");
        }
    }
}
