using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sinodom.ElevatorCloud.Migrations
{
    public partial class Added_EccpElevatorQrCodeBindLogs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EccpElevatorQrCodeBindLogs",
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
                    OldElevatorId = table.Column<Guid>(nullable: true),
                    NewElevatorId = table.Column<Guid>(nullable: true),
                    OldQrCodeId = table.Column<Guid>(nullable: true),
                    NewQrCodeId = table.Column<Guid>(nullable: true),
                    Remark = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EccpElevatorQrCodeBindLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EccpElevatorQrCodeBindLogs_EccpBaseElevators_NewElevatorId",
                        column: x => x.NewElevatorId,
                        principalTable: "EccpBaseElevators",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EccpElevatorQrCodeBindLogs_EccpElevatorQrCodes_NewQrCodeId",
                        column: x => x.NewQrCodeId,
                        principalTable: "EccpElevatorQrCodes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EccpElevatorQrCodeBindLogs_EccpBaseElevators_OldElevatorId",
                        column: x => x.OldElevatorId,
                        principalTable: "EccpBaseElevators",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EccpElevatorQrCodeBindLogs_EccpElevatorQrCodes_OldQrCodeId",
                        column: x => x.OldQrCodeId,
                        principalTable: "EccpElevatorQrCodes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EccpElevatorQrCodeBindLogs_NewElevatorId",
                table: "EccpElevatorQrCodeBindLogs",
                column: "NewElevatorId");

            migrationBuilder.CreateIndex(
                name: "IX_EccpElevatorQrCodeBindLogs_NewQrCodeId",
                table: "EccpElevatorQrCodeBindLogs",
                column: "NewQrCodeId");

            migrationBuilder.CreateIndex(
                name: "IX_EccpElevatorQrCodeBindLogs_OldElevatorId",
                table: "EccpElevatorQrCodeBindLogs",
                column: "OldElevatorId");

            migrationBuilder.CreateIndex(
                name: "IX_EccpElevatorQrCodeBindLogs_OldQrCodeId",
                table: "EccpElevatorQrCodeBindLogs",
                column: "OldQrCodeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EccpElevatorQrCodeBindLogs");
        }
    }
}
