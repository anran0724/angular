using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sinodom.ElevatorCloud.Migrations
{
    public partial class TypeChange_ECCPEditionsTypeId_Int_IntNull : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AbpEditions_ECCPEditionsTypes_ECCPEditionsTypeId",
                table: "AbpEditions");

            migrationBuilder.CreateTable(
                name: "ECCPEditionPermissions",
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
                    Name = table.Column<string>(maxLength: 128, nullable: false),
                    IsGranted = table.Column<bool>(nullable: false),
                    EditionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ECCPEditionPermissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ECCPEditionPermissions_AbpEditions_EditionId",
                        column: x => x.EditionId,
                        principalTable: "AbpEditions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ECCPEditionPermissions_EditionId",
                table: "ECCPEditionPermissions",
                column: "EditionId");

            migrationBuilder.AddForeignKey(
                name: "FK_AbpEditions_ECCPEditionsTypes_ECCPEditionsTypeId",
                table: "AbpEditions",
                column: "ECCPEditionsTypeId",
                principalTable: "ECCPEditionsTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AbpEditions_ECCPEditionsTypes_ECCPEditionsTypeId",
                table: "AbpEditions");

            migrationBuilder.DropTable(
                name: "ECCPEditionPermissions");

            migrationBuilder.AddForeignKey(
                name: "FK_AbpEditions_ECCPEditionsTypes_ECCPEditionsTypeId",
                table: "AbpEditions",
                column: "ECCPEditionsTypeId",
                principalTable: "ECCPEditionsTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
