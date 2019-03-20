using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sinodom.ElevatorCloud.Migrations
{
    public partial class Added_EccpMaintenanceTemplate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EccpMaintenanceTemplates",
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
                    TenantId = table.Column<int>(nullable: true),
                    TempName = table.Column<string>(maxLength: 50, nullable: false),
                    TempDesc = table.Column<string>(maxLength: 250, nullable: true),
                    TempAllow = table.Column<string>(maxLength: 20, nullable: true),
                    TempDeny = table.Column<string>(maxLength: 20, nullable: true),
                    TempCondition = table.Column<string>(maxLength: 30, nullable: true),
                    TempNodeCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EccpMaintenanceTemplates", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EccpMaintenanceTemplates_TenantId",
                table: "EccpMaintenanceTemplates",
                column: "TenantId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EccpMaintenanceTemplates");
        }
    }
}
