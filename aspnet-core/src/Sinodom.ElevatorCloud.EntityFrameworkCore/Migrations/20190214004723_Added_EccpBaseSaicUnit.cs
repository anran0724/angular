using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sinodom.ElevatorCloud.Migrations
{
    public partial class Added_EccpBaseSaicUnit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EccpBaseSaicUnits",
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
                    Name = table.Column<string>(maxLength: 32, nullable: false),
                    Address = table.Column<string>(maxLength: 128, nullable: false),
                    Telephone = table.Column<string>(maxLength: 36, nullable: false),
                    Summary = table.Column<string>(maxLength: 2000, nullable: true),
                    Longitude = table.Column<double>(nullable: true),
                    Latitude = table.Column<double>(nullable: true),
                    ProvinceId = table.Column<int>(nullable: true),
                    CityId = table.Column<int>(nullable: true),
                    DistrictId = table.Column<int>(nullable: true),
                    StreetId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EccpBaseSaicUnits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EccpBaseSaicUnits_ECCPBaseAreas_CityId",
                        column: x => x.CityId,
                        principalTable: "ECCPBaseAreas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EccpBaseSaicUnits_ECCPBaseAreas_DistrictId",
                        column: x => x.DistrictId,
                        principalTable: "ECCPBaseAreas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EccpBaseSaicUnits_ECCPBaseAreas_ProvinceId",
                        column: x => x.ProvinceId,
                        principalTable: "ECCPBaseAreas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EccpBaseSaicUnits_ECCPBaseAreas_StreetId",
                        column: x => x.StreetId,
                        principalTable: "ECCPBaseAreas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EccpBaseSaicUnits_CityId",
                table: "EccpBaseSaicUnits",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_EccpBaseSaicUnits_DistrictId",
                table: "EccpBaseSaicUnits",
                column: "DistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_EccpBaseSaicUnits_ProvinceId",
                table: "EccpBaseSaicUnits",
                column: "ProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_EccpBaseSaicUnits_StreetId",
                table: "EccpBaseSaicUnits",
                column: "StreetId");

            migrationBuilder.CreateIndex(
                name: "IX_EccpBaseSaicUnits_TenantId",
                table: "EccpBaseSaicUnits",
                column: "TenantId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EccpBaseSaicUnits");
        }
    }
}
