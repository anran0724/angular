using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sinodom.ElevatorCloud.Migrations
{
    public partial class Added_EccpBaseElevatorLabel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EccpBaseElevatorLabels",
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
                    TenantId = table.Column<int>(nullable: true),
                    LabelName = table.Column<string>(maxLength: 50, nullable: false),
                    UniqueId = table.Column<string>(maxLength: 50, nullable: false),
                    LocalInformation = table.Column<string>(maxLength: 50, nullable: true),
                    BindingTime = table.Column<DateTime>(nullable: true),
                    BinaryObjectsId = table.Column<Guid>(nullable: false),
                    ElevatorId = table.Column<Guid>(nullable: true),
                    LabelStatusId = table.Column<int>(nullable: false),
                    UserId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EccpBaseElevatorLabels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EccpBaseElevatorLabels_EccpBaseElevators_ElevatorId",
                        column: x => x.ElevatorId,
                        principalTable: "EccpBaseElevators",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EccpBaseElevatorLabels_EccpDictLabelStatuses_LabelStatusId",
                        column: x => x.LabelStatusId,
                        principalTable: "EccpDictLabelStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EccpBaseElevatorLabels_AbpUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EccpBaseElevatorLabels_ElevatorId",
                table: "EccpBaseElevatorLabels",
                column: "ElevatorId");

            migrationBuilder.CreateIndex(
                name: "IX_EccpBaseElevatorLabels_LabelStatusId",
                table: "EccpBaseElevatorLabels",
                column: "LabelStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_EccpBaseElevatorLabels_TenantId",
                table: "EccpBaseElevatorLabels",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_EccpBaseElevatorLabels_UserId",
                table: "EccpBaseElevatorLabels",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EccpBaseElevatorLabels");
        }
    }
}
