// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EccpMaintenanceWorksAppService.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using Sinodom.ElevatorCloud.EccpBaseElevators;
using Sinodom.ElevatorCloud.EccpMaintenanceTempWorkOrders;

namespace Sinodom.ElevatorCloud.EccpMaintenanceWorks
{
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Linq.Dynamic.Core;
    using System.Threading.Tasks;

    using Abp.Application.Services.Dto;
    using Abp.Authorization;
    using Abp.AutoMapper;
    using Abp.Domain.Repositories;
    using Abp.Domain.Uow;
    using Abp.Linq.Extensions;

    using Microsoft.EntityFrameworkCore;

    using Sinodom.ElevatorCloud.Authorization;
    using Sinodom.ElevatorCloud.EccpMaintenancePlans;
    using Sinodom.ElevatorCloud.EccpMaintenanceTemplateNodes;
    using Sinodom.ElevatorCloud.EccpMaintenanceTemplates;
    using Sinodom.ElevatorCloud.EccpMaintenanceWorkOrders;
    using Sinodom.ElevatorCloud.EccpMaintenanceWorks.Dtos;
    using QRCoder;
    using System.Drawing;
    using System.IO;
    using System.Drawing.Imaging;
    using Sinodom.ElevatorCloud.AppReturnModel;

    /// <summary>
    /// The eccp maintenance works app service.
    /// </summary>
    [AbpAuthorize(AppPermissions.Pages_EccpMaintenance_EccpMaintenanceWorks)]
    public class EccpMaintenanceWorksAppService : ElevatorCloudAppServiceBase, IEccpMaintenanceWorksAppService
    {
        /// <summary>
        /// The _eccp maintenance plan_ template_ links repository.
        /// </summary>
        private readonly IRepository<EccpMaintenancePlan_Template_Link, long> _eccpMaintenancePlanTemplateLinksRepository;

        /// <summary>
        /// The _eccp maintenance template node repository.
        /// </summary>
        private readonly IRepository<EccpMaintenanceTemplateNode, int> _eccpMaintenanceTemplateNodeRepository;

        /// <summary>
        /// The _eccp maintenance template repository.
        /// </summary>
        private readonly IRepository<EccpMaintenanceTemplate, int> _eccpMaintenanceTemplateRepository;

        /// <summary>
        /// The _eccp maintenance work order repository.
        /// </summary>
        private readonly IRepository<EccpMaintenanceWorkOrder, int> _eccpMaintenanceWorkOrderRepository;

        /// <summary>
        /// The _eccp maintenance work repository.
        /// </summary>
        private readonly IRepository<EccpMaintenanceWork> _eccpMaintenanceWorkRepository;
        private readonly IRepository<EccpMaintenanceWorkFlow,Guid> _eccpMaintenanceWorkFlowRepository;
        private readonly IRepository<EccpMaintenanceTempWorkOrder, Guid> _eccpMaintenanceTempWorkOrderRepository;
        private readonly IRepository<EccpMaintenancePlan, int> _eccpMaintenancePlanRepository;      
        private readonly IRepository<EccpBaseElevator, Guid> _eccpBaseElevatorRepository;
        private readonly IRepository<EccpMaintenanceWorkFlow_Item_Link, Guid> _eccpMaintenanceWorkFlowItemLinksRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="EccpMaintenanceWorksAppService"/> class.
        /// </summary>
        /// <param name="eccpMaintenanceWorkRepository">
        /// The eccp maintenance work repository.
        /// </param>
        /// <param name="eccpMaintenanceWorkOrderRepository">
        /// The eccp maintenance work order repository.
        /// </param>
        /// <param name="eccpMaintenanceTemplateNodeRepository">
        /// The eccp maintenance template node repository.
        /// </param>
        /// <param name="eccpMaintenancePlanTemplateLinksRepository">
        /// The eccp maintenance plan template links repository.
        /// </param>
        /// <param name="eccpMaintenanceTemplateRepository">
        /// The eccp maintenance template repository.
        /// </param>
        public EccpMaintenanceWorksAppService(
            IRepository<EccpMaintenanceWork> eccpMaintenanceWorkRepository,
            IRepository<EccpMaintenanceWorkOrder, int> eccpMaintenanceWorkOrderRepository,
            IRepository<EccpMaintenanceTemplateNode, int> eccpMaintenanceTemplateNodeRepository,
            IRepository<EccpMaintenancePlan_Template_Link, long> eccpMaintenancePlanTemplateLinksRepository,
            IRepository<EccpMaintenanceTemplate, int> eccpMaintenanceTemplateRepository,
            IRepository<EccpMaintenanceTempWorkOrder, Guid> eccpMaintenanceTempWorkOrderRepository,
            IRepository<EccpMaintenancePlan, int> eccpMaintenancePlanRepository,
            IRepository<EccpBaseElevator, Guid> eccpBaseElevatorRepository,
            IRepository<EccpMaintenanceWorkFlow, Guid> eccpMaintenanceWorkFlowRepository,
            IRepository<EccpMaintenanceWorkFlow_Item_Link, Guid> eccpMaintenanceWorkFlowItemLinksRepository
            )
        {
            this._eccpMaintenanceWorkRepository = eccpMaintenanceWorkRepository;
            this._eccpMaintenanceWorkOrderRepository = eccpMaintenanceWorkOrderRepository;
            this._eccpMaintenanceTemplateNodeRepository = eccpMaintenanceTemplateNodeRepository;
            this._eccpMaintenancePlanTemplateLinksRepository = eccpMaintenancePlanTemplateLinksRepository;
            this._eccpMaintenanceTemplateRepository = eccpMaintenanceTemplateRepository;
            this._eccpMaintenancePlanRepository = eccpMaintenancePlanRepository;
            this._eccpBaseElevatorRepository = eccpBaseElevatorRepository;
            this._eccpMaintenanceTempWorkOrderRepository = eccpMaintenanceTempWorkOrderRepository;
            this._eccpMaintenanceWorkFlowRepository = eccpMaintenanceWorkFlowRepository;
            this._eccpMaintenanceWorkFlowItemLinksRepository = eccpMaintenanceWorkFlowItemLinksRepository;
            
        }

        /// <summary>
        /// The app create.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task AppCreate(CreateOrEditAppEccpMaintenanceWorkDto input)
        {
            
            using (this.UnitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant))
            {               
                var workModel = _eccpMaintenanceWorkRepository.FirstOrDefault(e => e.MaintenanceWorkOrderId == input.MaintenanceWorkOrderId);
                if (workModel == null)
                {
                    var eccpMaintenanceWork = this.ObjectMapper.Map<EccpMaintenanceWork>(input);

                    var wordOrderModel = this._eccpMaintenanceWorkOrderRepository.FirstOrDefault(e => e.Id == input.MaintenanceWorkOrderId);
                    wordOrderModel.MaintenanceTypeId = input.MaintenanceTypeId;
                    wordOrderModel.MaintenanceStatusId = 2;
                    this._eccpMaintenanceWorkOrderRepository.Update(wordOrderModel);

                    var workOrderList = this._eccpMaintenanceWorkOrderRepository.GetAll()
                        .Where(e => e.Id == input.MaintenanceWorkOrderId);

                    var linkTemp = from workOrder in workOrderList
                                   join templateLink in this._eccpMaintenancePlanTemplateLinksRepository.GetAll() on
                                       workOrder.MaintenancePlanId equals templateLink.MaintenancePlanId
                                   join template in this._eccpMaintenanceTemplateRepository.GetAll() on
                                       templateLink.MaintenanceTemplateId equals template.Id
                                   where template.MaintenanceTypeId == input.MaintenanceTypeId
                                   select new { template.TempName, TempId = template.Id };
                    var linkTempModel = linkTemp.FirstOrDefault();
                    eccpMaintenanceWork.TaskName = linkTempModel?.TempName;
                    eccpMaintenanceWork.EccpMaintenanceTemplateId = linkTempModel?.TempId ?? 0;
                    eccpMaintenanceWork.TenantId = this.AbpSession.TenantId ?? 0;
                    eccpMaintenanceWork.CreatorUserId = this.AbpSession.UserId ?? 0;
                    await this._eccpMaintenanceWorkRepository.InsertAsync(eccpMaintenanceWork);
                }
            }
           
        }

        /// <summary>
        /// The create or edit.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task CreateOrEdit(CreateOrEditEccpMaintenanceWorkDto input)
        {
            if (input.Id == null)
            {
                await this.Create(input);
            }
            else
            {
                await this.Update(input);
            }
        }

        /// <summary>
        /// The delete.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [AbpAuthorize(AppPermissions.Pages_EccpMaintenance_EccpMaintenanceWorks_Delete)]
        public async Task Delete(EntityDto input)
        {
            await this._eccpMaintenanceWorkRepository.DeleteAsync(input.Id);
        }

        /// <summary>
        /// The get all.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task<PagedResultDto<GetEccpMaintenanceWorkForView>> GetAll(GetAllEccpMaintenanceWorksInput input)
        {
            var filteredEccpMaintenanceWorks = this._eccpMaintenanceWorkRepository.GetAll().WhereIf(
                !string.IsNullOrWhiteSpace(input.Filter),
                e => e.TaskName.Contains(input.Filter) || e.Remark.Contains(input.Filter));

            var query =
                (from o in filteredEccpMaintenanceWorks
                 join o1 in this._eccpMaintenanceWorkOrderRepository.GetAll() on o.MaintenanceWorkOrderId equals o1.Id
                     into j1
                 from s1 in j1.DefaultIfEmpty()
                 select new
                            {
                                EccpMaintenanceWork = o,
                                EccpMaintenanceWorkOrderPlanCheckDate =
                                    s1 == null ? string.Empty : s1.PlanCheckDate.ToString(CultureInfo.InvariantCulture)
                            }).WhereIf(
                    !string.IsNullOrWhiteSpace(input.EccpMaintenanceWorkOrderPlanCheckDateFilter),
                    e => e.EccpMaintenanceWorkOrderPlanCheckDate.ToLower()
                         == input.EccpMaintenanceWorkOrderPlanCheckDateFilter.ToLower().Trim());

            var totalCount = await query.CountAsync();

            var eccpMaintenanceWorks = new List<GetEccpMaintenanceWorkForView>();
                
            query.OrderBy(input.Sorting ?? "eccpMaintenanceWork.id asc").PageBy(input).MapTo(eccpMaintenanceWorks);

            return new PagedResultDto<GetEccpMaintenanceWorkForView>(totalCount, eccpMaintenanceWorks);
        }

        /// <summary>
        /// The get all eccp maintenance template node for lookup table.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [AbpAuthorize(AppPermissions.Pages_EccpMaintenance_EccpMaintenanceWorks)]
        public async Task<PagedResultDto<EccpMaintenanceTemplateNodeLookupTableDto>> GetAllEccpMaintenanceTemplateNodeForLookupTable(GetAllForLookupTableInput input)
        {
            var query = this._eccpMaintenanceTemplateNodeRepository.GetAll().WhereIf(
                !string.IsNullOrWhiteSpace(input.Filter),
                e => e.TemplateNodeName.ToString().Contains(input.Filter));

            var totalCount = await query.CountAsync();

            var eccpMaintenanceTemplateNodeList = await query.PageBy(input).ToListAsync();

            var lookupTableDtoList = new List<EccpMaintenanceTemplateNodeLookupTableDto>();
            foreach (var eccpMaintenanceTemplateNode in eccpMaintenanceTemplateNodeList)
            {
                lookupTableDtoList.Add(
                    new EccpMaintenanceTemplateNodeLookupTableDto
                        {
                            Id = eccpMaintenanceTemplateNode.Id,
                            DisplayName = eccpMaintenanceTemplateNode.TemplateNodeName
                        });
            }

            return new PagedResultDto<EccpMaintenanceTemplateNodeLookupTableDto>(totalCount, lookupTableDtoList);
        }

        /// <summary>
        /// The get all eccp maintenance work order for lookup table.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [AbpAuthorize(AppPermissions.Pages_EccpMaintenance_EccpMaintenanceWorks)]
        public async Task<PagedResultDto<EccpMaintenanceWorkOrderLookupTableDto>> GetAllEccpMaintenanceWorkOrderForLookupTable(GetAllForLookupTableInput input)
        {
            var query = this._eccpMaintenanceWorkOrderRepository.GetAll().WhereIf(
                !string.IsNullOrWhiteSpace(input.Filter),
                e => e.PlanCheckDate.ToString(CultureInfo.InvariantCulture).Contains(input.Filter));

            var totalCount = await query.CountAsync();

            var eccpMaintenanceWorkOrderList = await query.PageBy(input).ToListAsync();

            var lookupTableDtoList = new List<EccpMaintenanceWorkOrderLookupTableDto>();
            foreach (var eccpMaintenanceWorkOrder in eccpMaintenanceWorkOrderList)
            {
                lookupTableDtoList.Add(
                    new EccpMaintenanceWorkOrderLookupTableDto
                        {
                            Id = eccpMaintenanceWorkOrder.Id,
                            DisplayName = eccpMaintenanceWorkOrder.PlanCheckDate.ToString(CultureInfo.InvariantCulture)
                        });
            }

            return new PagedResultDto<EccpMaintenanceWorkOrderLookupTableDto>(totalCount, lookupTableDtoList);
        }

        /// <summary>
        /// The get app all.
        /// 获取工作用户2维码
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task<PagedResultDto<GetEccpMaintenanceWorkForView>> GetAppAll(
            GetAllEccpMaintenanceWorksInput input)
        {
            var filteredEccpMaintenanceWorks = this._eccpMaintenanceWorkRepository.GetAll()
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => e.TaskName.Contains(input.Filter))
                .Where(e => e.CreatorUserId == this.AbpSession.UserId);

            var query = from o in filteredEccpMaintenanceWorks
                        join o1 in this._eccpMaintenanceWorkOrderRepository.GetAll() on o.MaintenanceWorkOrderId equals
                            o1.Id into j1
                        from s1 in j1.DefaultIfEmpty()
                        select new 
                                   {
                                       EccpMaintenanceWork = o,
                                       EccpMaintenanceWorkOrderPlanCheckDate =
                                           s1 == null ? string.Empty : s1.PlanCheckDate.ToString(CultureInfo.InvariantCulture)
                                   };

            var totalCount = await query.CountAsync();

            var eccpMaintenanceWorks = new List<GetEccpMaintenanceWorkForView>();

            query.OrderBy(input.Sorting ?? "eccpMaintenanceWork.id asc").PageBy(input).MapTo(eccpMaintenanceWorks);

            return new PagedResultDto<GetEccpMaintenanceWorkForView>(totalCount, eccpMaintenanceWorks);
        }

        /// <summary>
        /// The get eccp maintenance work for edit.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [AbpAuthorize(AppPermissions.Pages_EccpMaintenance_EccpMaintenanceWorks_Edit)]
        public async Task<GetEccpMaintenanceWorkForEditOutput> GetEccpMaintenanceWorkForEdit(EntityDto input)
        {
            var eccpMaintenanceWork = await this._eccpMaintenanceWorkRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetEccpMaintenanceWorkForEditOutput
                             {
                                 EccpMaintenanceWork =
                                     this.ObjectMapper.Map<CreateOrEditEccpMaintenanceWorkDto>(eccpMaintenanceWork)
                             };
            var eccpMaintenanceWorkOrder =
                await this._eccpMaintenanceWorkOrderRepository.FirstOrDefaultAsync(
                    output.EccpMaintenanceWork.MaintenanceWorkOrderId);
            output.EccpMaintenanceWorkOrderPlanCheckDate = eccpMaintenanceWorkOrder.PlanCheckDate.ToString(CultureInfo.InvariantCulture);

            return output;
        }

        /// <summary>
        /// The create.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [AbpAuthorize(AppPermissions.Pages_EccpMaintenance_EccpMaintenanceWorks_Create)]
        private async Task Create(CreateOrEditEccpMaintenanceWorkDto input)
        {
            var eccpMaintenanceWork = this.ObjectMapper.Map<EccpMaintenanceWork>(input);

            if (this.AbpSession.TenantId != null)
            {
                eccpMaintenanceWork.TenantId = (int)this.AbpSession.TenantId;
            }

            await this._eccpMaintenanceWorkRepository.InsertAsync(eccpMaintenanceWork);
        }

        /// <summary>
        /// The update.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [AbpAuthorize(AppPermissions.Pages_EccpMaintenance_EccpMaintenanceWorks_Edit)]
        private async Task Update(CreateOrEditEccpMaintenanceWorkDto input)
        {
            if (input.Id != null)
            {
                var eccpMaintenanceWork = await this._eccpMaintenanceWorkRepository.FirstOrDefaultAsync((int)input.Id);
                this.ObjectMapper.Map(input, eccpMaintenanceWork);
            }
        }


        /// <summary>
        /// 工作记录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<GetWorkForRecordOutput>> GetWorkRecords(GetAllEccpMaintenanceWorksInput input)
        {
            using (this.UnitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant))
            {
                var eccpMaintenanceTempWorkOrders = this._eccpMaintenanceTempWorkOrderRepository.GetAll().Where(w => w.UserId == AbpSession.UserId && w.CheckState == 1);            
                var eccpMaintenancePlans = this._eccpMaintenancePlanRepository.GetAll();
                var eccpBaseElevators = this._eccpBaseElevatorRepository.GetAll();
                var maintenanceWorkIds = this._eccpMaintenanceWorkFlowRepository.GetAll()
                    .Where(w => w.CreatorUserId == this.AbpSession.UserId).Select(s => s.MaintenanceWorkId).Distinct();
                var eccpEccpMaintenanceWorkOrderIds = this._eccpMaintenanceWorkRepository.GetAll()
                    .Where(w => maintenanceWorkIds.Contains(w.Id))
                    .Select(s => s.MaintenanceWorkOrderId).ToList();

                var eccpMaintenanceWorkOrders = _eccpMaintenanceWorkOrderRepository.GetAll().Where(w => eccpEccpMaintenanceWorkOrderIds.Contains(w.Id)); 
                var maintenanceWorks = from eccpMaintenanceWorkOrder in eccpMaintenanceWorkOrders                                          
                                       join eccpMaintenancePlan in eccpMaintenancePlans
                                           on eccpMaintenanceWorkOrder.MaintenancePlanId equals eccpMaintenancePlan.Id
                                       join eccpBaseElevator in eccpBaseElevators 
                                           on eccpMaintenancePlan.ElevatorId equals eccpBaseElevator.Id                                      
                                       select new GetWorkForRecordOutput
                                       {
                                           Id = eccpMaintenanceWorkOrder.Id,
                                           CategoryId = "1",
                                           CategoryName = "维保工单",
                                           TypeName = eccpMaintenanceWorkOrder.MaintenanceType.Name,
                                           Describe = eccpBaseElevator.District.Name + eccpBaseElevator.InstallationAddress,
                                           CompletionTime = eccpMaintenanceWorkOrder.ComplateDate,
                                       };      
                var tempWorkOrders = eccpMaintenanceTempWorkOrders.Select(s => new GetWorkForRecordOutput
                {
                    Id = s.Id,
                    CategoryId = "2",
                    CategoryName = "临时工单",
                    TypeName = s.TempWorkOrderType.Name,
                    Describe = s.Describe,
                    CompletionTime = s.CompletionTime
                });
                var query = maintenanceWorks.Concat(tempWorkOrders);

                var totalCount = await query.CountAsync();

                var workOrders =
                    await query.OrderBy(input.Sorting ?? "CompletionTime desc").PageBy(input).ToListAsync();

                return new PagedResultDto<GetWorkForRecordOutput>(totalCount, workOrders);
            }
            
            //todo 重写单元测试
        }

        /// <summary>
        /// The get eccp maintenance work flow details.
        /// 工作记录详情
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task<List<GetEccpMaintenanceWorkFlowDetails>> GetEccpMaintenanceWorkFlowDetails(int id)
        {
            using (this.UnitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant))
            {
                var eccpMaintenanceWorkOrder = await this._eccpMaintenanceWorkOrderRepository.FirstOrDefaultAsync(id);
                if (eccpMaintenanceWorkOrder == null)
                {
                    return null;
                }

                var eccpMaintenanceWork = await this._eccpMaintenanceWorkRepository.GetAll().FirstOrDefaultAsync(w => w.MaintenanceWorkOrderId == id);
                if (eccpMaintenanceWork == null)
                {
                    return null;
                }

                var eccpMaintenanceWorkFlows = this._eccpMaintenanceWorkFlowRepository.GetAll().Where(w => w.MaintenanceWorkId == eccpMaintenanceWork.Id);
                var eccpMaintenanceTemplateNodes = this._eccpMaintenanceTemplateNodeRepository.GetAll();
                var eccpMaintenanceWorkFlowDetails = from eccpMaintenanceWorkFlow in eccpMaintenanceWorkFlows
                                                     join eccpMaintenanceTemplateNode in eccpMaintenanceTemplateNodes on eccpMaintenanceWorkFlow.MaintenanceTemplateNodeId
                                                     equals eccpMaintenanceTemplateNode.Id
                                                     select new GetEccpMaintenanceWorkFlowDetails
                                                     {
                                                         Id = eccpMaintenanceWorkFlow.Id,
                                                         ActionCode = eccpMaintenanceTemplateNode.ActionCode,
                                                         TemplateNodeName = eccpMaintenanceTemplateNode.TemplateNodeName,
                                                         NodeDesc = eccpMaintenanceTemplateNode.NodeDesc,
                                                         NodeTypeName = eccpMaintenanceTemplateNode.DictNodeType.Name,
                                                         ActionCodeValue = eccpMaintenanceWorkFlow.ActionCodeValue,
                                                         WorkFlowStatusName = eccpMaintenanceWorkFlow.DictMaintenanceWorkFlowStatus.Name,
                                                         WorkFlowRemark = eccpMaintenanceWorkFlow.Remark,
                                                         LastModificationTime = eccpMaintenanceWorkFlow.LastModificationTime
                                                     };

                var workFlowDetailList = await eccpMaintenanceWorkFlowDetails.ToListAsync();
                var workFlowIds = workFlowDetailList.Select(s => s.Id);
                var maintenanceItems = this._eccpMaintenanceWorkFlowItemLinksRepository.GetAll().Include(s => s.DictMaintenanceItem)
                    .Where(w => workFlowIds.Contains(w.MaintenanceWorkFlowId)).Select(s => new MaintenanceItem
                    {
                        Name = s.DictMaintenanceItem.Name,
                        TermCode = s.DictMaintenanceItem.TermCode,
                        DisOrder = s.DictMaintenanceItem.DisOrder,
                        MaintenanceWorkFlowId = s.MaintenanceWorkFlowId
                    }).ToList();

                foreach (var workFlowDetail in workFlowDetailList)
                {
                    var workFlowDetailItems = maintenanceItems.Where(w => w.MaintenanceWorkFlowId == workFlowDetail.Id);
                    workFlowDetail.MaintenanceItems = workFlowDetailItems.ToList();
                }
                return workFlowDetailList;
               
            }
            //todo 重写单元测试
        }
        /// <summary>
        /// 获取工作用户2维码
        /// </summary>
        /// <param name="key"></param>
        /// <param name="userId"></param>
        /// <param name="workId"></param>
        /// <param name="longitude">经度</param>
        /// <param name="latitude">纬度</param>
        /// <returns></returns>
        public string GetWorkQrCode(string key, int userId, int workId,string longitude,string latitude)
        {
            Config.Width = 614;
            Config.Height = 874;
            //Config.Width2 = 543;
            //Config.Height2 = 543;
            //Config.TotalCount = 1;
            Config.Level = QRCodeGenerator.ECCLevel.Q;
            Config.ImageType = "png";
            Config.BottomSize = 48;
            string dir = DateTime.Now.Ticks.ToString();
            string file = string.Empty;
            int width = Config.Width;
            int height = Config.Height;
            //int width2 = Config.Width2;
           // int height2 = Config.Height2;
            //string bottom = Config.Bottom;
            //Font fontCompany = new Font("微软雅黑", 70);
            //Font fontCompanyCode = new Font("华文宋体", 120);
            using (Bitmap bmpPNG = new Bitmap(width, height))
            {
                Byte[] byteArray;
                using (Graphics g = Graphics.FromImage(bmpPNG))
                {
                    string content = "1<=>{" + string.Format("key:'{0}',userId:'{1}',workId:'{2}',longitude:'{3}',latitude:'{4}',createTime:'{5}'", key, userId, workId,longitude,latitude,DateTime.Now.ToString()) + "}";
                    g.Clear(Color.White);
                    var qrGenerator = new QRCodeGenerator();
                    var qrCodeData = qrGenerator.CreateQrCode(content, Config.Level);
                    var qrCode = new QRCode(qrCodeData);
                    Bitmap bmpCode = qrCode.GetGraphic(20);
                    //code.QRCodeErrorCorrect = Config.Level;
                    //code.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
                    //code.QRCodeScale = 4;
                    //code.QRCodeVersion = 8;                  
                    //Bitmap bmpCode = code.Encode(content);
                    MemoryStream ms = new MemoryStream();
                    bmpCode.Save(ms, ImageFormat.Png);
                    byteArray = ms.ToArray();
                    ms.Close();
                }
                return Convert.ToBase64String(byteArray);
            }
        }
    }
    public class Config
    {
        public static int Width { get; set; }
        public static int Height { get; set; }
        public static int Width2 { get; set; }
        public static int Height2 { get; set; }

        public static int TotalCount { get; set; }

        public static QRCodeGenerator.ECCLevel Level { get; set; }

        public static string ImageType { get; set; }

        public static string Bottom { get; set; }

        public static float BottomSize { get; set; }
    }
}