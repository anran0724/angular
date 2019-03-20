using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sinodom.ElevatorCloud.Migrations
{
    public partial class Added_EccpBaseElevator : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EccpBaseElevators",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeleterUserId = table.Column<long>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    TenantId = table.Column<int>(nullable: true),
                    Name = table.Column<string>(maxLength: 20, nullable: true),
                    CertificateNum = table.Column<string>(maxLength: 50, nullable: false),
                    MachineNum = table.Column<string>(maxLength: 50, nullable: true),
                    InstallationAddress = table.Column<string>(maxLength: 255, nullable: true),
                    InstallationDatetime = table.Column<DateTime>(nullable: true),
                    Longitude = table.Column<string>(maxLength: 64, nullable: false),
                    Latitude = table.Column<string>(maxLength: 64, nullable: false),
                    EccpDictPlaceTypeId = table.Column<int>(nullable: true),
                    EccpDictElevatorTypeId = table.Column<int>(nullable: true),
                    ECCPDictElevatorStatusId = table.Column<int>(nullable: true),
                    ECCPBaseCommunityId = table.Column<long>(nullable: true),
                    ECCPBaseMaintenanceCompanyId = table.Column<int>(nullable: true),
                    ECCPBaseAnnualInspectionUnitId = table.Column<long>(nullable: true),
                    ECCPBaseRegisterCompanyId = table.Column<long>(nullable: true),
                    ECCPBaseProductionCompanyId = table.Column<long>(nullable: true),
                    EccpBaseElevatorBrandId = table.Column<int>(nullable: true),
                    EccpBaseElevatorModelId = table.Column<int>(nullable: true),
                    ProvinceId = table.Column<int>(nullable: true),
                    CityId = table.Column<int>(nullable: true),
                    DistrictId = table.Column<int>(nullable: true),
                    StreetId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EccpBaseElevators", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EccpBaseElevators_ECCPBaseAreas_CityId",
                        column: x => x.CityId,
                        principalTable: "ECCPBaseAreas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EccpBaseElevators_ECCPBaseAreas_DistrictId",
                        column: x => x.DistrictId,
                        principalTable: "ECCPBaseAreas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EccpBaseElevators_ECCPBaseAnnualInspectionUnits_ECCPBaseAnnualInspectionUnitId",
                        column: x => x.ECCPBaseAnnualInspectionUnitId,
                        principalTable: "ECCPBaseAnnualInspectionUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EccpBaseElevators_ECCPBaseCommunities_ECCPBaseCommunityId",
                        column: x => x.ECCPBaseCommunityId,
                        principalTable: "ECCPBaseCommunities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EccpBaseElevators_ECCPBaseMaintenanceCompanies_ECCPBaseMaintenanceCompanyId",
                        column: x => x.ECCPBaseMaintenanceCompanyId,
                        principalTable: "ECCPBaseMaintenanceCompanies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EccpBaseElevators_ECCPBaseProductionCompanies_ECCPBaseProductionCompanyId",
                        column: x => x.ECCPBaseProductionCompanyId,
                        principalTable: "ECCPBaseProductionCompanies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EccpBaseElevators_ECCPBaseRegisterCompanies_ECCPBaseRegisterCompanyId",
                        column: x => x.ECCPBaseRegisterCompanyId,
                        principalTable: "ECCPBaseRegisterCompanies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EccpBaseElevators_ECCPDictElevatorStatuses_ECCPDictElevatorStatusId",
                        column: x => x.ECCPDictElevatorStatusId,
                        principalTable: "ECCPDictElevatorStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EccpBaseElevators_EccpBaseElevatorBrands_EccpBaseElevatorBrandId",
                        column: x => x.EccpBaseElevatorBrandId,
                        principalTable: "EccpBaseElevatorBrands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EccpBaseElevators_EccpBaseElevatorModels_EccpBaseElevatorModelId",
                        column: x => x.EccpBaseElevatorModelId,
                        principalTable: "EccpBaseElevatorModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EccpBaseElevators_EccpDictElevatorTypes_EccpDictElevatorTypeId",
                        column: x => x.EccpDictElevatorTypeId,
                        principalTable: "EccpDictElevatorTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EccpBaseElevators_EccpDictPlaceTypes_EccpDictPlaceTypeId",
                        column: x => x.EccpDictPlaceTypeId,
                        principalTable: "EccpDictPlaceTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EccpBaseElevators_ECCPBaseAreas_ProvinceId",
                        column: x => x.ProvinceId,
                        principalTable: "ECCPBaseAreas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EccpBaseElevators_ECCPBaseAreas_StreetId",
                        column: x => x.StreetId,
                        principalTable: "ECCPBaseAreas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EccpBaseElevators_CityId",
                table: "EccpBaseElevators",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_EccpBaseElevators_DistrictId",
                table: "EccpBaseElevators",
                column: "DistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_EccpBaseElevators_ECCPBaseAnnualInspectionUnitId",
                table: "EccpBaseElevators",
                column: "ECCPBaseAnnualInspectionUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_EccpBaseElevators_ECCPBaseCommunityId",
                table: "EccpBaseElevators",
                column: "ECCPBaseCommunityId");

            migrationBuilder.CreateIndex(
                name: "IX_EccpBaseElevators_ECCPBaseMaintenanceCompanyId",
                table: "EccpBaseElevators",
                column: "ECCPBaseMaintenanceCompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_EccpBaseElevators_ECCPBaseProductionCompanyId",
                table: "EccpBaseElevators",
                column: "ECCPBaseProductionCompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_EccpBaseElevators_ECCPBaseRegisterCompanyId",
                table: "EccpBaseElevators",
                column: "ECCPBaseRegisterCompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_EccpBaseElevators_ECCPDictElevatorStatusId",
                table: "EccpBaseElevators",
                column: "ECCPDictElevatorStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_EccpBaseElevators_EccpBaseElevatorBrandId",
                table: "EccpBaseElevators",
                column: "EccpBaseElevatorBrandId");

            migrationBuilder.CreateIndex(
                name: "IX_EccpBaseElevators_EccpBaseElevatorModelId",
                table: "EccpBaseElevators",
                column: "EccpBaseElevatorModelId");

            migrationBuilder.CreateIndex(
                name: "IX_EccpBaseElevators_EccpDictElevatorTypeId",
                table: "EccpBaseElevators",
                column: "EccpDictElevatorTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_EccpBaseElevators_EccpDictPlaceTypeId",
                table: "EccpBaseElevators",
                column: "EccpDictPlaceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_EccpBaseElevators_ProvinceId",
                table: "EccpBaseElevators",
                column: "ProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_EccpBaseElevators_StreetId",
                table: "EccpBaseElevators",
                column: "StreetId");

            migrationBuilder.CreateIndex(
                name: "IX_EccpBaseElevators_TenantId",
                table: "EccpBaseElevators",
                column: "TenantId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EccpBaseElevators");
        }
    }
}
