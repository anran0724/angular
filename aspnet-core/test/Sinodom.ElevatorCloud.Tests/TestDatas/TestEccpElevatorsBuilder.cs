// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TestEccpElevatorsBuilder.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.Tests.TestDatas
{
    using System;
    using System.Linq;

    using Sinodom.ElevatorCloud.EccpBaseElevators;
    using Sinodom.ElevatorCloud.EntityFrameworkCore;

    /// <summary>
    /// The test eccp elevators builder.
    /// </summary>
    public class TestEccpElevatorsBuilder
    {
        /// <summary>
        /// The _context.
        /// </summary>
        private readonly ElevatorCloudDbContext _context;

        /// <summary>
        /// The _maintenance tenant id.
        /// </summary>
        private readonly int _maintenanceTenantId;

        /// <summary>
        /// The _property tenant id.
        /// </summary>
        private readonly int _propertyTenantId;

        /// <summary>
        /// Initializes a new instance of the <see cref="TestEccpElevatorsBuilder"/> class.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        /// <param name="maintenanceTenantId">
        /// The maintenance tenant id.
        /// </param>
        /// <param name="propertyTenantId">
        /// The property tenant id.
        /// </param>
        public TestEccpElevatorsBuilder(ElevatorCloudDbContext context, int maintenanceTenantId, int propertyTenantId)
        {
            this._context = context;
            this._maintenanceTenantId = maintenanceTenantId;
            this._propertyTenantId = propertyTenantId;
        }

        /// <summary>
        /// The create.
        /// </summary>
        public void Create()
        {
            this.CreateElevator(
                "测试电梯1",
                "a1111111111",
                "b1111111111",
                this._maintenanceTenantId,
                this._propertyTenantId);
            this.CreateElevator("测试电梯2", "a2222222222", "b2222222222", this._maintenanceTenantId);
            this.CreateElevator("测试电梯3", "a3333333333", "b3333333333", this._propertyTenantId);
            this.CreateElevator(
                "测试电梯4",
                "a4444444444",
                "b4444444444",
                this._maintenanceTenantId,
                this._propertyTenantId);
            this.CreateElevator("测试电梯5", "a2222222222", "b2222222222");
            this.CreateElevator("测试电梯6", "a666", "b666");
        }

        /// <summary>
        /// The create elevator.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="certificateNum">
        /// The certificate num.
        /// </param>
        /// <param name="machineNum">
        /// The machine num.
        /// </param>
        /// <param name="maintenanceTenantId">
        /// The maintenance tenant id.
        /// </param>
        /// <param name="propertyTenantId">
        /// The property tenant id.
        /// </param>
        private void CreateElevator(
            string name,
            string certificateNum,
            string machineNum,
            int? maintenanceTenantId = null,
            int? propertyTenantId = null)
        {
            int? maintenanceCompanyId = null;
            int? propertyCompanyId = null;

            if (maintenanceTenantId.HasValue)
                maintenanceCompanyId = this._context.ECCPBaseMaintenanceCompanies
                    .FirstOrDefault(e => e.TenantId == maintenanceTenantId)?.Id;

            if (propertyTenantId.HasValue)
                propertyCompanyId = this._context.ECCPBasePropertyCompanies
                    .FirstOrDefault(e => e.TenantId == propertyTenantId)?.Id;

            this._context.EccpBaseElevators.Add(
                new EccpBaseElevator
                {
                    Name = name,
                    CertificateNum = certificateNum,
                    MachineNum = machineNum,
                    InstallationAddress = "asdsadasd",
                    InstallationDatetime = new DateTime().Date,
                    Latitude = "123",
                    Longitude = "123213",
                    EccpDictPlaceTypeId = 1,
                    EccpDictElevatorTypeId = 12,
                    ECCPDictElevatorStatusId = 4,
                    ECCPBaseMaintenanceCompanyId = maintenanceCompanyId,
                    ECCPBasePropertyCompanyId = propertyCompanyId
                });
            this._context.SaveChanges();
        }
    }
}