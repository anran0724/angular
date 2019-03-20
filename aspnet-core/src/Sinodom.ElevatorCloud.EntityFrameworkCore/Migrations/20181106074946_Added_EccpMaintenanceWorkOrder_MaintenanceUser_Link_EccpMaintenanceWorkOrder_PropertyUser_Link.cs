using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sinodom.ElevatorCloud.Migrations
{
    public partial class Added_EccpMaintenanceWorkOrder_MaintenanceUser_Link_EccpMaintenanceWorkOrder_PropertyUser_Link : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TenantId",
                table: "ECCPBasePropertyCompanies",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TenantId",
                table: "ECCPBaseMaintenanceCompanies",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "EccpMaintenanceWorkOrder_MaintenanceUser_Links",
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
                    TenantId = table.Column<int>(nullable: false),
                    UserId = table.Column<long>(nullable: false),
                    MaintenancePlanId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EccpMaintenanceWorkOrder_MaintenanceUser_Links", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EccpMaintenanceWorkOrder_PropertyUser_Links",
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
                    TenantId = table.Column<int>(nullable: false),
                    UserId = table.Column<long>(nullable: false),
                    MaintenancePlanId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EccpMaintenanceWorkOrder_PropertyUser_Links", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EccpMaintenanceWorkOrder_MaintenanceUser_Links");

            migrationBuilder.DropTable(
                name: "EccpMaintenanceWorkOrder_PropertyUser_Links");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "ECCPBasePropertyCompanies");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "ECCPBaseMaintenanceCompanies");
        }
    }
}
