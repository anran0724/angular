// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ECCPMaintenanceTempWorkOrdersAppService_Tests.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.Tests.EccpMaintenanceTempWorkOrders
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Abp.Application.Services.Dto;
    using Abp.Authorization.Users;

    using Shouldly;

    using Sinodom.ElevatorCloud.EccpBaseElevators.Dtos;
    using Sinodom.ElevatorCloud.EccpMaintenanceTempWorkOrders;
    using Sinodom.ElevatorCloud.EccpMaintenanceTempWorkOrders.Dtos;
    using Sinodom.ElevatorCloud.MultiTenancy;

    /// <summary>
    /// The eccp maintenance temp work orders app service_ tests.
    /// </summary>
    public class ECCPMaintenanceTempWorkOrdersAppService_Tests : AppTestBase
    {
        /// <summary>
        /// The _eccp maintenance temp work orders app service.
        /// </summary>
        private readonly IEccpMaintenanceTempWorkOrdersAppService _eccpMaintenanceTempWorkOrdersAppService;

        /// <summary>
        /// The _maintenance temp work orders app service.
        /// </summary>
        private readonly IEccpMaintenanceTempWorkOrdersAppService _maintenanceTempWorkOrdersAppService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ECCPMaintenanceTempWorkOrdersAppService_Tests"/> class.
        /// </summary>
        public ECCPMaintenanceTempWorkOrdersAppService_Tests()
        {
            this.LoginAsTenant(Tenant.DefaultMaintenanceTenantName, AbpUserBase.AdminUserName);
            this._maintenanceTempWorkOrdersAppService = this.Resolve<EccpMaintenanceTempWorkOrdersAppService>();
            this._eccpMaintenanceTempWorkOrdersAppService = this.Resolve<EccpMaintenanceTempWorkOrdersAppService>();
        }

        /// <summary>
        /// 临时维保工单单元测试添加
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [MultiTenantFact]
        public async Task AddEeepMaintenanceTempWorkOrders()
        {
            var eccpMaintenanceTempWork = new CreateOrEditEccpMaintenanceTempWorkOrderDto
                                              {
                                                  UserId = this.AbpSession.UserId,
                                                  TempWorkOrderTypeId = 1,
                                                  MaintenanceCompanyId = 1,
                                                  Title = "临时维保工单单元测试添加",
                                                  Describe = "临时维保工单",
                                                  Priority = 0
                                              };
            await this._maintenanceTempWorkOrdersAppService.CreateOrEdit(eccpMaintenanceTempWork);
            var getAllEccpMaintenanceTempWorkOrdersInput =
                new GetAllEccpMaintenanceTempWorkOrdersInput { Filter = "临时维保工单单元测试添加" };
            var eccpMaintenanceTempWorkList =
                await this._maintenanceTempWorkOrdersAppService.GetAll(getAllEccpMaintenanceTempWorkOrdersInput);
            eccpMaintenanceTempWorkList.Items.Count.ShouldBe(1);
        }

        /// <summary>
        /// 临时维保工单单元测试删除
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [MultiTenantFact]
        public async Task DeleteEeepMaintenanceTempWorkOrders()
        {
            var eccpMaintenanceTempWork = this._maintenanceTempWorkOrdersAppService
                .GetAll(new GetAllEccpMaintenanceTempWorkOrdersInput()).Result.Items.FirstOrDefault();
            var title = eccpMaintenanceTempWork.EccpMaintenanceTempWorkOrder.Title;
            EntityDto<Guid> input = new EccpBaseElevatorDto();
            input.Id = eccpMaintenanceTempWork.EccpMaintenanceTempWorkOrder.Id;
            await this._maintenanceTempWorkOrdersAppService.Delete(input);

            var getAllEccpMaintenanceTempWorkOrdersInput =
                new GetAllEccpMaintenanceTempWorkOrdersInput { Filter = title };
            var eccpMaintenanceTempWorkList =
                await this._maintenanceTempWorkOrdersAppService.GetAll(getAllEccpMaintenanceTempWorkOrdersInput);
            eccpMaintenanceTempWorkList.Items.Count.ShouldBe(0);
        }

        /// <summary>
        /// 临时维保工单单元测试修改
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [MultiTenantFact]
        public async Task EditEeepMaintenanceTempWorkOrders()
        {
            var eccpMaintenanceTempWork = this._maintenanceTempWorkOrdersAppService
                .GetAll(new GetAllEccpMaintenanceTempWorkOrdersInput()).Result.Items.FirstOrDefault();
            var editInput = new CreateOrEditEccpMaintenanceTempWorkOrderDto
                                {
                                    TempWorkOrderTypeId = 1,
                                    MaintenanceCompanyId = 1,
                                    UserId = this.AbpSession.UserId,
                                    Priority = 1,
                                    Describe = "临时维保工单单元测试修改",
                                    Title = "临时维保工单单元测试修改",
                                    Id = eccpMaintenanceTempWork.EccpMaintenanceTempWorkOrder.Id
                                };
            await this._maintenanceTempWorkOrdersAppService.CreateOrEdit(editInput);
            var getAllEccpMaintenanceTempWorkOrdersInput = new GetAllEccpMaintenanceTempWorkOrdersInput();
            getAllEccpMaintenanceTempWorkOrdersInput.Filter = "临时维保工单单元测试修改";

            var eccpMaintenanceTempWorkList =
                await this._maintenanceTempWorkOrdersAppService.GetAll(getAllEccpMaintenanceTempWorkOrdersInput);
            eccpMaintenanceTempWorkList.Items.Count.ShouldBe(1);
        }

        /// <summary>
        /// 手机端查询临时工单测试
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [MultiTenantFact]
        public async Task GetEccpMaintenanceTempWorkOrder()
        {
         
            var getAllEccpMaintenanceTempWorkOrdersInput = new GetAllEccpMaintenanceTempWorkOrdersInput();
            getAllEccpMaintenanceTempWorkOrdersInput.UserId = this.AbpSession.UserId.Value;
            var entities =
                await this._eccpMaintenanceTempWorkOrdersAppService.GetEccpMaintenanceTempWorkOrder(
                    getAllEccpMaintenanceTempWorkOrdersInput);
            entities.TotalCount.ShouldBe(2);
        }

        /// <summary>
        /// 手机端临时维保工单详情单元测试
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [MultiTenantFact]
        public async Task GetEccpMaintenanceTempWorkOrderForDetails()
        {
            var eccpMaintenanceTempWork = this._maintenanceTempWorkOrdersAppService
                .GetAll(new GetAllEccpMaintenanceTempWorkOrdersInput()).Result.Items.FirstOrDefault();
            var title = eccpMaintenanceTempWork.EccpMaintenanceTempWorkOrder.Title;
            EntityDto<Guid> input = new EccpBaseElevatorDto();
            input.Id = eccpMaintenanceTempWork.EccpMaintenanceTempWorkOrder.Id;
            var eccpMaintenanceTempWorkDetails =
                await this._maintenanceTempWorkOrdersAppService.GetEccpMaintenanceTempWorkOrderForDetails(input);
            eccpMaintenanceTempWorkDetails.Title.ShouldBe(title);
        }

        /// <summary>
        /// 手机端临时维保工单处置单元测试
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [MultiTenantFact]
        public async Task ManagementEccpMaintenanceTempWorkOrder()
        {
            var eccpMaintenanceTempWork = this._maintenanceTempWorkOrdersAppService
                .GetAll(new GetAllEccpMaintenanceTempWorkOrdersInput()).Result.Items.FirstOrDefault();
            var input = new HandleEccpMaintenanceTempWorkOrderDto
                            {
                                Id = eccpMaintenanceTempWork.EccpMaintenanceTempWorkOrder.Id,
                                CheckState = 2,
                                Remarks = "1"
                            };
            await this._maintenanceTempWorkOrdersAppService.ManagementEccpMaintenanceTempWorkOrder(input);

            EntityDto<Guid> detailsInput = new EccpBaseElevatorDto { Id = input.Id };
            var eccpMaintenanceTempWorkDetails =
                await this._maintenanceTempWorkOrdersAppService.GetEccpMaintenanceTempWorkOrderForDetails(detailsInput);
            eccpMaintenanceTempWorkDetails.CheckState.ShouldBe(2);
            eccpMaintenanceTempWorkDetails.Remarks.ShouldBe(input.Remarks);

            var maintenanceTempWorkOrderDetail =
                await this._maintenanceTempWorkOrdersAppService.GetEccpMaintenanceTempWorkOrderDetails(detailsInput.Id);
            maintenanceTempWorkOrderDetail.MaintenanceTempWorkOrderActionLogs.Count.ShouldBe(1) ;


        }
       
    }
}