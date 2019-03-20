using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sinodom.ElevatorCloud.Migrations
{
    public partial class Added_LanFlowInstance : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LanFlowInstances",
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
                    StatusValue = table.Column<string>(maxLength: 50, nullable: false),
                    StatusName = table.Column<string>(maxLength: 50, nullable: false),
                    SchemeContent = table.Column<string>(nullable: false),
                    LanFlowSchemeId = table.Column<int>(nullable: false),
                    ElevatorId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LanFlowInstances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LanFlowInstances_EccpBaseElevators_ElevatorId",
                        column: x => x.ElevatorId,
                        principalTable: "EccpBaseElevators",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LanFlowInstances_LanFlowSchemes_LanFlowSchemeId",
                        column: x => x.LanFlowSchemeId,
                        principalTable: "LanFlowSchemes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LanFlowInstances_ElevatorId",
                table: "LanFlowInstances",
                column: "ElevatorId");

            migrationBuilder.CreateIndex(
                name: "IX_LanFlowInstances_LanFlowSchemeId",
                table: "LanFlowInstances",
                column: "LanFlowSchemeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LanFlowInstances");
        }
    }
}
