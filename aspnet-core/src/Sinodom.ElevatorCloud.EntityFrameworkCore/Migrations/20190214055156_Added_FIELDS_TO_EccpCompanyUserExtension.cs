using Microsoft.EntityFrameworkCore.Migrations;

namespace Sinodom.ElevatorCloud.Migrations
{
    public partial class Added_FIELDS_TO_EccpCompanyUserExtension : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PositionCityId",
                table: "EccpCompanyUserExtensions",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "PositionCommunityId",
                table: "EccpCompanyUserExtensions",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PositionDistrictId",
                table: "EccpCompanyUserExtensions",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PositionProvinceId",
                table: "EccpCompanyUserExtensions",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PositionStreetId",
                table: "EccpCompanyUserExtensions",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EccpCompanyUserExtensions_PositionCityId",
                table: "EccpCompanyUserExtensions",
                column: "PositionCityId");

            migrationBuilder.CreateIndex(
                name: "IX_EccpCompanyUserExtensions_PositionCommunityId",
                table: "EccpCompanyUserExtensions",
                column: "PositionCommunityId");

            migrationBuilder.CreateIndex(
                name: "IX_EccpCompanyUserExtensions_PositionDistrictId",
                table: "EccpCompanyUserExtensions",
                column: "PositionDistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_EccpCompanyUserExtensions_PositionProvinceId",
                table: "EccpCompanyUserExtensions",
                column: "PositionProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_EccpCompanyUserExtensions_PositionStreetId",
                table: "EccpCompanyUserExtensions",
                column: "PositionStreetId");

            migrationBuilder.AddForeignKey(
                name: "FK_EccpCompanyUserExtensions_ECCPBaseAreas_PositionCityId",
                table: "EccpCompanyUserExtensions",
                column: "PositionCityId",
                principalTable: "ECCPBaseAreas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EccpCompanyUserExtensions_ECCPBaseCommunities_PositionCommunityId",
                table: "EccpCompanyUserExtensions",
                column: "PositionCommunityId",
                principalTable: "ECCPBaseCommunities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EccpCompanyUserExtensions_ECCPBaseAreas_PositionDistrictId",
                table: "EccpCompanyUserExtensions",
                column: "PositionDistrictId",
                principalTable: "ECCPBaseAreas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EccpCompanyUserExtensions_ECCPBaseAreas_PositionProvinceId",
                table: "EccpCompanyUserExtensions",
                column: "PositionProvinceId",
                principalTable: "ECCPBaseAreas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EccpCompanyUserExtensions_ECCPBaseAreas_PositionStreetId",
                table: "EccpCompanyUserExtensions",
                column: "PositionStreetId",
                principalTable: "ECCPBaseAreas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EccpCompanyUserExtensions_ECCPBaseAreas_PositionCityId",
                table: "EccpCompanyUserExtensions");

            migrationBuilder.DropForeignKey(
                name: "FK_EccpCompanyUserExtensions_ECCPBaseCommunities_PositionCommunityId",
                table: "EccpCompanyUserExtensions");

            migrationBuilder.DropForeignKey(
                name: "FK_EccpCompanyUserExtensions_ECCPBaseAreas_PositionDistrictId",
                table: "EccpCompanyUserExtensions");

            migrationBuilder.DropForeignKey(
                name: "FK_EccpCompanyUserExtensions_ECCPBaseAreas_PositionProvinceId",
                table: "EccpCompanyUserExtensions");

            migrationBuilder.DropForeignKey(
                name: "FK_EccpCompanyUserExtensions_ECCPBaseAreas_PositionStreetId",
                table: "EccpCompanyUserExtensions");

            migrationBuilder.DropIndex(
                name: "IX_EccpCompanyUserExtensions_PositionCityId",
                table: "EccpCompanyUserExtensions");

            migrationBuilder.DropIndex(
                name: "IX_EccpCompanyUserExtensions_PositionCommunityId",
                table: "EccpCompanyUserExtensions");

            migrationBuilder.DropIndex(
                name: "IX_EccpCompanyUserExtensions_PositionDistrictId",
                table: "EccpCompanyUserExtensions");

            migrationBuilder.DropIndex(
                name: "IX_EccpCompanyUserExtensions_PositionProvinceId",
                table: "EccpCompanyUserExtensions");

            migrationBuilder.DropIndex(
                name: "IX_EccpCompanyUserExtensions_PositionStreetId",
                table: "EccpCompanyUserExtensions");

            migrationBuilder.DropColumn(
                name: "PositionCityId",
                table: "EccpCompanyUserExtensions");

            migrationBuilder.DropColumn(
                name: "PositionCommunityId",
                table: "EccpCompanyUserExtensions");

            migrationBuilder.DropColumn(
                name: "PositionDistrictId",
                table: "EccpCompanyUserExtensions");

            migrationBuilder.DropColumn(
                name: "PositionProvinceId",
                table: "EccpCompanyUserExtensions");

            migrationBuilder.DropColumn(
                name: "PositionStreetId",
                table: "EccpCompanyUserExtensions");
        }
    }
}
