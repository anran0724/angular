using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sinodom.ElevatorCloud.Migrations
{
    public partial class Added_LanFlowInstanceOperationHistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LanFlowInstanceOperationHistories",
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
                    StatusValue = table.Column<int>(nullable: false),
                    StatusName = table.Column<string>(maxLength: 200, nullable: true),
                    ActionDesc = table.Column<string>(maxLength: 200, nullable: true),
                    ActionCode = table.Column<string>(maxLength: 20, nullable: false),
                    ActionValue = table.Column<string>(maxLength: 20, nullable: false),
                    InstanceId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LanFlowInstanceOperationHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LanFlowInstanceOperationHistories_LanFlowInstances_InstanceId",
                        column: x => x.InstanceId,
                        principalTable: "LanFlowInstances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LanFlowInstanceOperationHistories_InstanceId",
                table: "LanFlowInstanceOperationHistories",
                column: "InstanceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LanFlowInstanceOperationHistories");
        }
    }
}
