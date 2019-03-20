using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sinodom.ElevatorCloud.Migrations
{
    public partial class Added_EccpMaintenanceContract_Elevator_Links : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EccpMaintenanceContract_Elevator_Links",
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
                    MaintenanceContractId = table.Column<long>(nullable: false),
                    ElevatorId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EccpMaintenanceContract_Elevator_Links", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EccpMaintenanceContract_Elevator_Links_EccpBaseElevators_ElevatorId",
                        column: x => x.ElevatorId,
                        principalTable: "EccpBaseElevators",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EccpMaintenanceContract_Elevator_Links_EccpMaintenanceContracts_MaintenanceContractId",
                        column: x => x.MaintenanceContractId,
                        principalTable: "EccpMaintenanceContracts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EccpMaintenanceContract_Elevator_Links_ElevatorId",
                table: "EccpMaintenanceContract_Elevator_Links",
                column: "ElevatorId");

            migrationBuilder.CreateIndex(
                name: "IX_EccpMaintenanceContract_Elevator_Links_MaintenanceContractId",
                table: "EccpMaintenanceContract_Elevator_Links",
                column: "MaintenanceContractId");

            migrationBuilder.CreateIndex(
                name: "IX_EccpMaintenanceContract_Elevator_Links_TenantId",
                table: "EccpMaintenanceContract_Elevator_Links",
                column: "TenantId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EccpMaintenanceContract_Elevator_Links");
        }
    }
}
