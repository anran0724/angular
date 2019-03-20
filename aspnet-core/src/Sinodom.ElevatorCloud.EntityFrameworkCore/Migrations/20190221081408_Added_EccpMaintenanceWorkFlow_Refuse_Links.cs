using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sinodom.ElevatorCloud.Migrations
{
    public partial class Added_EccpMaintenanceWorkFlow_Refuse_Links : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EccpMaintenanceWorkFlow_Refuse_Links",
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
                    MaintenanceWorkFlowId = table.Column<Guid>(nullable: false),
                    RefusePictureId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EccpMaintenanceWorkFlow_Refuse_Links", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EccpMaintenanceWorkFlow_Refuse_Links_EccpMaintenanceWorkFlows_MaintenanceWorkFlowId",
                        column: x => x.MaintenanceWorkFlowId,
                        principalTable: "EccpMaintenanceWorkFlows",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EccpMaintenanceWorkFlow_Refuse_Links_MaintenanceWorkFlowId",
                table: "EccpMaintenanceWorkFlow_Refuse_Links",
                column: "MaintenanceWorkFlowId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EccpMaintenanceWorkFlow_Refuse_Links");
        }
    }
}
