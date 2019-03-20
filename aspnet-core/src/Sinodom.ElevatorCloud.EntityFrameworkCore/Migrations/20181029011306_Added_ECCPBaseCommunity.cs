using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sinodom.ElevatorCloud.Migrations
{
    public partial class Added_ECCPBaseCommunity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ECCPBaseCommunities",
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
                    Name = table.Column<string>(maxLength: 128, nullable: false),
                    Address = table.Column<string>(maxLength: 128, nullable: false),
                    Longitude = table.Column<string>(maxLength: 64, nullable: false),
                    Latitude = table.Column<string>(maxLength: 128, nullable: false),
                    ProvinceId = table.Column<int>(nullable: true),
                    CityId = table.Column<int>(nullable: true),
                    DistrictId = table.Column<int>(nullable: true),
                    StreetId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ECCPBaseCommunities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ECCPBaseCommunities_ECCPBaseAreas_CityId",
                        column: x => x.CityId,
                        principalTable: "ECCPBaseAreas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ECCPBaseCommunities_ECCPBaseAreas_DistrictId",
                        column: x => x.DistrictId,
                        principalTable: "ECCPBaseAreas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ECCPBaseCommunities_ECCPBaseAreas_ProvinceId",
                        column: x => x.ProvinceId,
                        principalTable: "ECCPBaseAreas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ECCPBaseCommunities_ECCPBaseAreas_StreetId",
                        column: x => x.StreetId,
                        principalTable: "ECCPBaseAreas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ECCPBaseCommunities_CityId",
                table: "ECCPBaseCommunities",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_ECCPBaseCommunities_DistrictId",
                table: "ECCPBaseCommunities",
                column: "DistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_ECCPBaseCommunities_ProvinceId",
                table: "ECCPBaseCommunities",
                column: "ProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_ECCPBaseCommunities_StreetId",
                table: "ECCPBaseCommunities",
                column: "StreetId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ECCPBaseCommunities");
        }
    }
}
