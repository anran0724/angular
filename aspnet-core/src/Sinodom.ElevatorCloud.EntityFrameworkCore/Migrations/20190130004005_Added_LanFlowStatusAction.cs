using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sinodom.ElevatorCloud.Migrations
{
    public partial class Added_LanFlowStatusAction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LanFlowStatusActions",
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
                    StatusName = table.Column<string>(maxLength: 50, nullable: false),
                    ActionName = table.Column<string>(maxLength: 50, nullable: false),
                    ActionDesc = table.Column<string>(maxLength: 50, nullable: true),
                    ActionCode = table.Column<string>(maxLength: 50, nullable: false),
                    UserRoleCode = table.Column<string>(maxLength: 50, nullable: true),
                    ArgumentValue = table.Column<string>(maxLength: 50, nullable: false),
                    IsStartProcess = table.Column<bool>(nullable: false),
                    IsEndProcess = table.Column<bool>(nullable: false),
                    IsAdopt = table.Column<bool>(nullable: false),
                    ApiAction = table.Column<string>(maxLength: 150, nullable: true),
                    SortCode = table.Column<int>(nullable: false),
                    SchemeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LanFlowStatusActions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LanFlowStatusActions_LanFlowSchemes_SchemeId",
                        column: x => x.SchemeId,
                        principalTable: "LanFlowSchemes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LanFlowStatusActions_SchemeId",
                table: "LanFlowStatusActions",
                column: "SchemeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LanFlowStatusActions");
        }
    }
}
