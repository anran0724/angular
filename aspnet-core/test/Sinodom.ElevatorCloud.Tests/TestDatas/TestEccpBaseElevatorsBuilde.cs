using Sinodom.ElevatorCloud.ECCPBaseAnnualInspectionUnits;
using Sinodom.ElevatorCloud.ECCPBaseAreas;
using Sinodom.ElevatorCloud.ECCPBaseCommunities;
using Sinodom.ElevatorCloud.EccpBaseElevators;
using Sinodom.ElevatorCloud.ECCPBaseMaintenanceCompanies;
using Sinodom.ElevatorCloud.ECCPBaseProductionCompanies;
using Sinodom.ElevatorCloud.ECCPBaseRegisterCompanies;
using Sinodom.ElevatorCloud.EccpDict;
using Sinodom.ElevatorCloud.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sinodom.ElevatorCloud.Tests.TestDatas
{
    public class TestEccpBaseElevatorsBuilde
    {
        private readonly ElevatorCloudDbContext _context;

        private readonly int _tenantId;

        public TestEccpBaseElevatorsBuilde(ElevatorCloudDbContext context, int tenantId)
        {
            _context = context;
            this._tenantId = tenantId;
        }
        public void Create()
        {
            this.CreateEccpBaseElevators();
        }
        private void CreateEccpBaseElevators()
        {
            ECCPBaseArea areaEntity = new ECCPBaseArea();
            EccpDictPlaceType PlaceTypeEntity = new EccpDictPlaceType();
            EccpDictElevatorType ElevatorTypeEntity = new EccpDictElevatorType();
            ECCPDictElevatorStatus ElevatorStatusEntity = new ECCPDictElevatorStatus();
            ECCPBaseCommunity CommunityEntity = new ECCPBaseCommunity();
            ECCPBaseMaintenanceCompany MaintenanceCompanyEntity = new ECCPBaseMaintenanceCompany();
            ECCPBaseAnnualInspectionUnit AnnualInspectionUnitEntity = new ECCPBaseAnnualInspectionUnit();
            ECCPBaseRegisterCompany RegisterCompanyEntity = new ECCPBaseRegisterCompany();
            ECCPBaseProductionCompany ProductionCompanyEntity = new ECCPBaseProductionCompany();

            areaEntity = _context.ECCPBaseAreas.FirstOrDefault(e => e.Name == "海南省");
            PlaceTypeEntity = _context.EccpDictPlaceTypes.FirstOrDefault(e => e.Name == "住宅");
            ElevatorTypeEntity = _context.EccpDictElevatorTypes.FirstOrDefault(e => e.Name == "载货电梯");
            ElevatorStatusEntity = _context.ECCPDictElevatorStatuses.FirstOrDefault(e => e.Name == "测试");
            CommunityEntity = _context.ECCPBaseCommunities.FirstOrDefault(e => e.Name == "测试园区");
            MaintenanceCompanyEntity = _context.ECCPBaseMaintenanceCompanies.FirstOrDefault(e => e.Name == "默认维保公司");
            AnnualInspectionUnitEntity = _context.ECCPBaseAnnualInspectionUnits.FirstOrDefault(e => e.Name == "测试年检单位");
            RegisterCompanyEntity = _context.ECCPBaseRegisterCompanies.FirstOrDefault(e => e.Name == "测试注册单位");
            ProductionCompanyEntity = _context.ECCPBaseProductionCompanies.FirstOrDefault(e => e.Name == "测试生产企业");

            _context.EccpBaseElevators.Add(
                        new EccpBaseElevator
                        {
                            Name = "测试电梯",
                            CertificateNum = "100000001",
                            MachineNum = "123123123213",
                            InstallationAddress = "asdsadasd",
                            InstallationDatetime = new DateTime().Date,
                            Latitude = "123",
                            Longitude = "123213",
                            EccpDictPlaceTypeId = PlaceTypeEntity.Id,
                            EccpDictElevatorTypeId = ElevatorTypeEntity.Id,
                            ECCPDictElevatorStatusId = ElevatorStatusEntity.Id,
                            ECCPBaseCommunityId = CommunityEntity.Id,
                            ECCPBaseMaintenanceCompanyId = MaintenanceCompanyEntity.Id,
                            ECCPBaseAnnualInspectionUnitId = AnnualInspectionUnitEntity.Id,
                            ECCPBaseRegisterCompanyId = RegisterCompanyEntity.Id,
                            ECCPBaseProductionCompanyId = ProductionCompanyEntity.Id,
                            ProvinceId = areaEntity.Id
                        });
            _context.SaveChanges();
            var entity = _context.EccpBaseElevators.FirstOrDefault(e => e.Name == "测试电梯");
            _context.EccpBaseElevatorSubsidiaryInfos.Add(
                     new EccpBaseElevatorSubsidiaryInfo
                     {
                         CustomNum = "测试电梯",
                         ManufacturingLicenseNumber = "100000001",
                         FloorNumber = 2,
                         GateNumber = 2,
                         RatedSpeed = 2,
                         Deadweight = 2,
                         ElevatorId = entity.Id
                     });
            _context.SaveChanges();

        }

    }
}
