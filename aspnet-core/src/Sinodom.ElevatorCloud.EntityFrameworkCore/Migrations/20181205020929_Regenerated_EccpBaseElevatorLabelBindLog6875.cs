using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sinodom.ElevatorCloud.Migrations
{
    public partial class Regenerated_EccpBaseElevatorLabelBindLog6875 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EccpBaseElevatorLabelBindLogs",
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
                    LabelName = table.Column<string>(maxLength: 50, nullable: false),
                    LocalInformation = table.Column<string>(maxLength: 50, nullable: true),
                    BindingTime = table.Column<DateTime>(nullable: true),
                    BinaryObjectsId = table.Column<Guid>(nullable: false),
                    Remark = table.Column<string>(maxLength: 500, nullable: true),
                    ElevatorLabelId = table.Column<long>(nullable: false),
                    ElevatorId = table.Column<Guid>(nullable: true),
                    LabelStatusId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EccpBaseElevatorLabelBindLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EccpBaseElevatorLabelBindLogs_EccpBaseElevators_ElevatorId",
                        column: x => x.ElevatorId,
                        principalTable: "EccpBaseElevators",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EccpBaseElevatorLabelBindLogs_EccpDictLabelStatuses_LabelStatusId",
                        column: x => x.LabelStatusId,
                        principalTable: "EccpDictLabelStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EccpBaseElevatorLabelBindLogs_ElevatorId",
                table: "EccpBaseElevatorLabelBindLogs",
                column: "ElevatorId");

            migrationBuilder.CreateIndex(
                name: "IX_EccpBaseElevatorLabelBindLogs_LabelStatusId",
                table: "EccpBaseElevatorLabelBindLogs",
                column: "LabelStatusId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EccpBaseElevatorLabelBindLogs");
        }
    }
}
