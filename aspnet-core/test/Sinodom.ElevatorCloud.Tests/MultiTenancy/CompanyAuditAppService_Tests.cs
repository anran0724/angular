namespace Sinodom.ElevatorCloud.Tests.MultiTenancy
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Abp.Application.Editions;
    using Abp.Application.Services.Dto;

    using Shouldly;

    using Sinodom.ElevatorCloud.Editions;
    using Sinodom.ElevatorCloud.MultiTenancy;
    using Sinodom.ElevatorCloud.MultiTenancy.CompanyAudits;
    using Sinodom.ElevatorCloud.MultiTenancy.CompanyAudits.Dtos;
    using Sinodom.ElevatorCloud.MultiTenancy.Dto;
    using Sinodom.ElevatorCloud.MultiTenancy.Payments.Dto;

    public class CompanyAuditAppService_Tests : AppTestBase
    {

        private readonly ITenantRegistrationAppService _tenantRegistrationAppService;
        private readonly IEccpCompanyAuditsAppService _eccpCompanyAuditsAppService;
        private readonly TenantManager _tenantManager;


        public CompanyAuditAppService_Tests()
        {
            LoginAsHostAdmin();
            _tenantRegistrationAppService = Resolve<ITenantRegistrationAppService>();
            _eccpCompanyAuditsAppService = Resolve<IEccpCompanyAuditsAppService>();
            _tenantManager = Resolve<TenantManager>();
        }

        [MultiTenantFact]
        public async Task GetCompanyAudits_Test()
        {
            // Arrange
            var maintenanceTenant = await this.GetTenantAsync(Tenant.DefaultMaintenanceTenantName);
            maintenanceTenant.ShouldNotBeNull();

            maintenanceTenant.IsActive = false;
            await this._tenantManager.UpdateAsync(maintenanceTenant);
            

            //Act
            var output = await this._eccpCompanyAuditsAppService.GetAll(new GetAllEccpCompanyAuditInput());

            //Assert
            output.TotalCount.ShouldBe(1);
            output.Items.Count.ShouldBe(1);
            output.Items[0].EditionTypeName.ShouldBe("维保公司");
            output.Items[0].EccpCompanyInfo.Name.ShouldBe("默认维保公司");
            output.Items[0].EccpCompanyInfoExtension.LegalPerson.ShouldBe("维保单位企业法人");
        }

        [MultiTenantFact]
        public async Task GetCompanyAudit_Test()
        {
            //Act
            var output = await _eccpCompanyAuditsAppService.GetCompanyAuditForEdit(new EntityDto(2));

            //Assert
            output.ShouldNotBe(null);
            output.EditionTypeName.ShouldBe("维保公司");
            output.EccpCompanyInfo.Name.ShouldBe("默认维保公司");
            output.EccpCompanyInfoExtension.LegalPerson.ShouldBe("维保单位企业法人");
        }

        [MultiTenantFact]
        public async Task EditCompanyAudit_Test()
        {
            // Act
            await this._eccpCompanyAuditsAppService.EditCompanyAudit(new EditCompanyAuditDto { Id = 3, CheckState = false, Remarks = "照片不合格" });
            await this._eccpCompanyAuditsAppService.EditCompanyAudit(new EditCompanyAuditDto { Id = 3, CheckState = true, Remarks = "照片合格" });
            var output = await this._eccpCompanyAuditsAppService.GetCompanyAuditLogs(new GetAllEccpCompanyAuditLogInput { TenantId = 3 });

            // Assert
            output.TotalCount.ShouldBe(2);
            output.Items.Count.ShouldBe(2);
            output.Items[0].CheckStateName.ShouldBe("未通过");
            output.Items[0].Remarks.ShouldBe("照片不合格");
            output.Items[1].CheckStateName.ShouldBe("已通过");
            output.Items[1].Remarks.ShouldBe("照片合格");
        }

        private Edition GetEditionEntity(string name)
        {
            var editionEntity = this.UsingDbContext(context => context.Editions.FirstOrDefault(e => e.Name == name));
            editionEntity.ShouldNotBeNull();
            
            return editionEntity;
        }
    }
}