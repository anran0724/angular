using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sinodom.ElevatorCloud.Migrations
{
    public partial class Added_EccpElevatorQrCode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EccpElevatorQrCodes",
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
                    AreaName = table.Column<string>(maxLength: 1, nullable: false),
                    ElevatorNum = table.Column<string>(maxLength: 20, nullable: false),
                    ImgPicture = table.Column<string>(maxLength: 50, nullable: true),
                    IsInstall = table.Column<bool>(nullable: false),
                    IsGrant = table.Column<bool>(nullable: false),
                    InstallDateTime = table.Column<DateTime>(nullable: true),
                    GrantDateTime = table.Column<DateTime>(nullable: true),
                    ElevatorId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EccpElevatorQrCodes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EccpElevatorQrCodes_EccpBaseElevators_ElevatorId",
                        column: x => x.ElevatorId,
                        principalTable: "EccpBaseElevators",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EccpElevatorQrCodes_ElevatorId",
                table: "EccpElevatorQrCodes",
                column: "ElevatorId");

            migrationBuilder.CreateIndex(
                name: "IX_EccpElevatorQrCodes_TenantId",
                table: "EccpElevatorQrCodes",
                column: "TenantId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EccpElevatorQrCodes");
        }
    }
}
