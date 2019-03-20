using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sinodom.ElevatorCloud.Migrations
{
    public partial class Added_EccpBaseElevatorSubsidiaryInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EccpBaseElevatorSubsidiaryInfos",
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
                    TenantId = table.Column<int>(nullable: true),
                    CustomNum = table.Column<string>(maxLength: 50, nullable: true),
                    ManufacturingLicenseNumber = table.Column<string>(maxLength: 50, nullable: true),
                    FloorNumber = table.Column<int>(nullable: true),
                    GateNumber = table.Column<int>(nullable: true),
                    RatedSpeed = table.Column<double>(nullable: true),
                    Deadweight = table.Column<double>(nullable: true),
                    ElevatorId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EccpBaseElevatorSubsidiaryInfos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EccpBaseElevatorSubsidiaryInfos_EccpBaseElevators_ElevatorId",
                        column: x => x.ElevatorId,
                        principalTable: "EccpBaseElevators",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EccpBaseElevatorSubsidiaryInfos_ElevatorId",
                table: "EccpBaseElevatorSubsidiaryInfos",
                column: "ElevatorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EccpBaseElevatorSubsidiaryInfos");
        }
    }
}
