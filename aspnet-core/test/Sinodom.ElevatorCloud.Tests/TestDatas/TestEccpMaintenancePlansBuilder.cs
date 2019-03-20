namespace Sinodom.ElevatorCloud.Tests.TestDatas
{
    using System;
    using System.Linq;

    using Sinodom.ElevatorCloud.EccpMaintenanceContracts;
    using Sinodom.ElevatorCloud.EccpMaintenancePlans;
    using Sinodom.ElevatorCloud.EccpMaintenanceTemplates;
    using Sinodom.ElevatorCloud.EntityFrameworkCore;

    public class TestEccpMaintenancePlansBuilder
    {
        private readonly ElevatorCloudDbContext _context;

        private readonly int _maintenanceTenantId;

        public TestEccpMaintenancePlansBuilder(ElevatorCloudDbContext context, int maintenanceTenantId)
        {
            this._context = context;
            this._maintenanceTenantId = maintenanceTenantId;
        }

        public void Create()
        {
            this.CreateMaintenancePlan(this._maintenanceTenantId, "a1111111111");
            this.CreateMaintenancePlan(this._maintenanceTenantId, "a4444444444");
        }

        private void CreateMaintenancePlan(int maintenanceTenantId, string certificateNum)
        {
            var elevatorId = this.GetElevatorId(certificateNum);
            var maintenanceUser = this._context.Users.FirstOrDefault(e => e.UserName == "TestMaintenanceUser1");
            var propertyUser = this._context.Users.FirstOrDefault(e => e.UserName == "TestPropertyUser1");
            EccpMaintenanceTemplate maintenanceTemplate = this._context.EccpMaintenanceTemplates.FirstOrDefault();

            var eccpMaintenancePlan = new EccpMaintenancePlan
                                          {
                                              TenantId = maintenanceTenantId,
                                              RemindHour = 30 * 24,
                                              PollingPeriod = 30,
                                              ElevatorId = elevatorId,
                                              PlanGroupGuid = Guid.NewGuid()
                                          };
            //this._context.EccpMaintenancePlans.Add(new EccpMaintenancePlan
            //{
            //    TenantId = maintenanceTenantId,
            //    RemindHour = 30 * 24,
            //    PollingPeriod = 30,
            //    ElevatorId = elevatorId,
            //    PlanGroupGuid = Guid.NewGuid()
            //});


          this._context.EccpMaintenancePlan_MaintenanceUser_Links.Add(
                new EccpMaintenancePlan_MaintenanceUser_Link
                    {
                        MaintenancePlan = eccpMaintenancePlan, User = maintenanceUser, TenantId = maintenanceTenantId
                    });

            this._context.EccpMaintenancePlan_PropertyUser_Links.Add(
                new EccpMaintenancePlan_PropertyUser_Link
                    {
                        MaintenancePlan = eccpMaintenancePlan, User = propertyUser, TenantId = maintenanceTenantId
                    });

            this._context.EccpMaintenancePlan_Template_Links.Add(
                new EccpMaintenancePlan_Template_Link
                    {
                        MaintenancePlan = eccpMaintenancePlan, MaintenanceTemplate = maintenanceTemplate, TenantId = maintenanceTenantId
                    });

            this._context.EccpMaintenancePlanCustomRules.Add(
                new EccpMaintenancePlanCustomRule
                    {
                        QuarterPollingPeriod = 3,
                        HalfYearPollingPeriod = 6,
                        YearPollingPeriod = 12,
                        TenantId = maintenanceTenantId,
                        PlanGroupGuid = eccpMaintenancePlan.PlanGroupGuid.Value
                    });

            //this._context.EccpMaintenancePlans.Add(eccpMaintenancePlan);
            this._context.SaveChanges();
        }

        private Guid GetElevatorId(string certificateNum)
        {
            return this._context.EccpBaseElevators.FirstOrDefault(e => e.CertificateNum == certificateNum).Id;
        }
    }
}