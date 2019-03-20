using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sinodom.ElevatorCloud.Migrations
{
    public partial class Added_PropertyCompany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PropertyCompanies",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 32, nullable: false),
                    Addresse = table.Column<string>(maxLength: 128, nullable: false),
                    Longitude = table.Column<string>(maxLength: 64, nullable: false),
                    Latitude = table.Column<string>(maxLength: 64, nullable: false),
                    Telephone = table.Column<string>(maxLength: 36, nullable: false),
                    Summary = table.Column<string>(nullable: true),
                    ProvinceId = table.Column<int>(nullable: true),
                    CityId = table.Column<int>(nullable: true),
                    DistrictId = table.Column<int>(nullable: true),
                    StreetId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyCompanies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PropertyCompanies_ECCPBaseAreas_CityId",
                        column: x => x.CityId,
                        principalTable: "ECCPBaseAreas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PropertyCompanies_ECCPBaseAreas_DistrictId",
                        column: x => x.DistrictId,
                        principalTable: "ECCPBaseAreas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PropertyCompanies_ECCPBaseAreas_ProvinceId",
                        column: x => x.ProvinceId,
                        principalTable: "ECCPBaseAreas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PropertyCompanies_ECCPBaseAreas_StreetId",
                        column: x => x.StreetId,
                        principalTable: "ECCPBaseAreas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PropertyCompanies_CityId",
                table: "PropertyCompanies",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyCompanies_DistrictId",
                table: "PropertyCompanies",
                column: "DistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyCompanies_ProvinceId",
                table: "PropertyCompanies",
                column: "ProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyCompanies_StreetId",
                table: "PropertyCompanies",
                column: "StreetId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PropertyCompanies");
        }
    }
}
