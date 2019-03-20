using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sinodom.ElevatorCloud.Migrations
{
    public partial class Regenerated_EccpMaintenanceCompanyChangeLog3920 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EccpMaintenanceCompanyChangeLogs",
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
                    FieldName = table.Column<string>(maxLength: 20, nullable: false),
                    OldValue = table.Column<string>(maxLength: 500, nullable: false),
                    NewValue = table.Column<string>(maxLength: 500, nullable: false),
                    MaintenanceCompanyId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EccpMaintenanceCompanyChangeLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EccpMaintenanceCompanyChangeLogs_ECCPBaseMaintenanceCompanies_MaintenanceCompanyId",
                        column: x => x.MaintenanceCompanyId,
                        principalTable: "ECCPBaseMaintenanceCompanies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EccpMaintenanceCompanyChangeLogs_MaintenanceCompanyId",
                table: "EccpMaintenanceCompanyChangeLogs",
                column: "MaintenanceCompanyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EccpMaintenanceCompanyChangeLogs");
        }
    }
}
