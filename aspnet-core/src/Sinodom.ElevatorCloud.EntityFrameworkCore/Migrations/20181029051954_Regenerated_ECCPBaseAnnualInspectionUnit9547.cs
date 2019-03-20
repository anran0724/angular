using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sinodom.ElevatorCloud.Migrations
{
    public partial class Regenerated_ECCPBaseAnnualInspectionUnit9547 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ECCPBaseAnnualInspectionUnits",
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
                    Name = table.Column<string>(maxLength: 32, nullable: false),
                    Addresse = table.Column<string>(maxLength: 128, nullable: false),
                    Telephone = table.Column<string>(maxLength: 36, nullable: false),
                    Summary = table.Column<string>(nullable: true),
                    ProvinceId = table.Column<int>(nullable: true),
                    CityId = table.Column<int>(nullable: true),
                    DistrictId = table.Column<int>(nullable: true),
                    StreetId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ECCPBaseAnnualInspectionUnits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ECCPBaseAnnualInspectionUnits_ECCPBaseAreas_CityId",
                        column: x => x.CityId,
                        principalTable: "ECCPBaseAreas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ECCPBaseAnnualInspectionUnits_ECCPBaseAreas_DistrictId",
                        column: x => x.DistrictId,
                        principalTable: "ECCPBaseAreas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ECCPBaseAnnualInspectionUnits_ECCPBaseAreas_ProvinceId",
                        column: x => x.ProvinceId,
                        principalTable: "ECCPBaseAreas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ECCPBaseAnnualInspectionUnits_ECCPBaseAreas_StreetId",
                        column: x => x.StreetId,
                        principalTable: "ECCPBaseAreas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ECCPBaseAnnualInspectionUnits_CityId",
                table: "ECCPBaseAnnualInspectionUnits",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_ECCPBaseAnnualInspectionUnits_DistrictId",
                table: "ECCPBaseAnnualInspectionUnits",
                column: "DistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_ECCPBaseAnnualInspectionUnits_ProvinceId",
                table: "ECCPBaseAnnualInspectionUnits",
                column: "ProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_ECCPBaseAnnualInspectionUnits_StreetId",
                table: "ECCPBaseAnnualInspectionUnits",
                column: "StreetId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ECCPBaseAnnualInspectionUnits");
        }
    }
}
