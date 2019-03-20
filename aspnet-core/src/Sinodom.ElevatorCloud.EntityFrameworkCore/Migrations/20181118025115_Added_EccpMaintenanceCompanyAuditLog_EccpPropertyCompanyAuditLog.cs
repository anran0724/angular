using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sinodom.ElevatorCloud.Migrations
{
    public partial class Added_EccpMaintenanceCompanyAuditLog_EccpPropertyCompanyAuditLog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EccpMaintenanceCompanyAuditLogs",
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
                    CheckState = table.Column<bool>(nullable: false),
                    Remarks = table.Column<string>(maxLength: 500, nullable: false),
                    MaintenanceCompanyId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EccpMaintenanceCompanyAuditLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EccpMaintenanceCompanyAuditLogs_ECCPBaseMaintenanceCompanies_MaintenanceCompanyId",
                        column: x => x.MaintenanceCompanyId,
                        principalTable: "ECCPBaseMaintenanceCompanies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EccpPropertyCompanyAuditLogs",
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
                    CheckState = table.Column<bool>(nullable: false),
                    Remarks = table.Column<string>(maxLength: 500, nullable: false),
                    PropertyCompanyId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EccpPropertyCompanyAuditLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EccpPropertyCompanyAuditLogs_ECCPBasePropertyCompanies_PropertyCompanyId",
                        column: x => x.PropertyCompanyId,
                        principalTable: "ECCPBasePropertyCompanies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EccpMaintenanceCompanyAuditLogs_MaintenanceCompanyId",
                table: "EccpMaintenanceCompanyAuditLogs",
                column: "MaintenanceCompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_EccpPropertyCompanyAuditLogs_PropertyCompanyId",
                table: "EccpPropertyCompanyAuditLogs",
                column: "PropertyCompanyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EccpMaintenanceCompanyAuditLogs");

            migrationBuilder.DropTable(
                name: "EccpPropertyCompanyAuditLogs");
        }
    }
}
