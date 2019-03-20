// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EccpBaseElevatorLabelsAppService.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpBaseElevatorLabels
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Linq.Dynamic.Core;
    using System.Net;
    using System.Text;
    using System.Threading.Tasks;

    using Abp.Application.Services.Dto;
    using Abp.Authorization;
    using Abp.AutoMapper;
    using Abp.Domain.Repositories;
    using Abp.Linq.Extensions;

    using Microsoft.EntityFrameworkCore;

    using Sinodom.ElevatorCloud.Authorization;
    using Sinodom.ElevatorCloud.Authorization.Users;
    using Sinodom.ElevatorCloud.EccpBaseElevatorLabels.Dtos;
    using Sinodom.ElevatorCloud.EccpBaseElevators;
    using Sinodom.ElevatorCloud.EccpDict;
    using Newtonsoft.Json;
    /// <summary>
    ///     The eccp base elevator labels app service.
    /// </summary>
    [AbpAuthorize(AppPermissions.Pages_EccpElevator_EccpBaseElevatorLabels)]
    public class EccpBaseElevatorLabelsAppService : ElevatorCloudAppServiceBase, IEccpBaseElevatorLabelsAppService
    {
        /// <summary>
        ///     The eccp base elevator label bind log repository.
        /// </summary>
        private readonly IRepository<EccpBaseElevatorLabelBindLog, long> _eccpBaseElevatorLabelBindLogRepository;

        /// <summary>
        ///     The eccp base elevator label repository.
        /// </summary>
        private readonly IRepository<EccpBaseElevatorLabel, long> _eccpBaseElevatorLabelRepository;

        /// <summary>
        ///     The eccp base elevator repository.
        /// </summary>
        private readonly IRepository<EccpBaseElevator, Guid> _eccpBaseElevatorRepository;

        /// <summary>
        ///     The eccp dict label status repository.
        /// </summary>
        private readonly IRepository<EccpDictLabelStatus, int> _eccpDictLabelStatusRepository;

        /// <summary>
        ///     The user repository.
        /// </summary>
        private readonly IRepository<User, long> _userRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="EccpBaseElevatorLabelsAppService"/> class.
        /// </summary>
        /// <param name="eccpBaseElevatorLabelRepository">
        /// The eccp base elevator label repository.
        /// </param>
        /// <param name="eccpBaseElevatorRepository">
        /// The eccp base elevator repository.
        /// </param>
        /// <param name="eccpDictLabelStatusRepository">
        /// The eccp dict label status repository.
        /// </param>
        /// <param name="userRepository">
        /// The user repository.
        /// </param>
        /// <param name="eccpBaseElevatorLabelBindLogRepository">
        /// The eccp base elevator label bind log repository.
        /// </param>
        public EccpBaseElevatorLabelsAppService(
            IRepository<EccpBaseElevatorLabel, long> eccpBaseElevatorLabelRepository,
            IRepository<EccpBaseElevator, Guid> eccpBaseElevatorRepository,
            IRepository<EccpDictLabelStatus, int> eccpDictLabelStatusRepository,
            IRepository<User, long> userRepository,
            IRepository<EccpBaseElevatorLabelBindLog, long> eccpBaseElevatorLabelBindLogRepository)
        {
            this._eccpBaseElevatorLabelRepository = eccpBaseElevatorLabelRepository;
            this._eccpBaseElevatorRepository = eccpBaseElevatorRepository;
            this._eccpDictLabelStatusRepository = eccpDictLabelStatusRepository;
            this._userRepository = userRepository;
            this._eccpBaseElevatorLabelBindLogRepository = eccpBaseElevatorLabelBindLogRepository;
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
        public async Task CreateOrEdit(CreateOrEditEccpBaseElevatorLabelDto input)
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
        [AbpAuthorize(AppPermissions.Pages_EccpElevator_EccpBaseElevatorLabels_Delete)]
        public async Task Delete(EntityDto<long> input)
        {
            await this._eccpBaseElevatorLabelRepository.DeleteAsync(input.Id);
        }

        /// <summary>
        /// The discontinue use.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [AbpAuthorize(AppPermissions.Pages_EccpElevator_EccpBaseElevatorLabels_DiscontinueUse)]
        public async Task DiscontinueUse(EntityDto<long> input)
        {
            var eccpBaseElevatorLabel = await this._eccpBaseElevatorLabelRepository.FirstOrDefaultAsync(input.Id);
            var eccpDictLabelStatus =
                await this._eccpDictLabelStatusRepository.FirstOrDefaultAsync(e => e.Name == "已失效");
            if (eccpDictLabelStatus != null)
            {
                // 添加日志
                var eccpBaseElevatorLabelBindLog = new EccpBaseElevatorLabelBindLog
                {
                    LabelName = eccpBaseElevatorLabel.LabelName,
                    LocalInformation = eccpBaseElevatorLabel.LocalInformation,
                    BindingTime = eccpBaseElevatorLabel.BindingTime,
                    BinaryObjectsId = eccpBaseElevatorLabel.BinaryObjectsId,
                    Remark = "标签停用",
                    ElevatorLabelId = eccpBaseElevatorLabel.Id,
                    ElevatorId = eccpBaseElevatorLabel.ElevatorId,
                    LabelStatusId = eccpBaseElevatorLabel.LabelStatusId
                };
                await this._eccpBaseElevatorLabelBindLogRepository.InsertAsync(eccpBaseElevatorLabelBindLog);

                eccpBaseElevatorLabel.LabelStatusId = eccpDictLabelStatus.Id;
                await this._eccpBaseElevatorLabelRepository.UpdateAsync(eccpBaseElevatorLabel);
            }
        }

        /// <summary>
        /// The elevator label bind.
        /// 标签绑定
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task<int> ElevatorLabelBind(CreateOrEditEccpBaseElevatorLabelDto input)
        {
            int result;
            var baseElevatorLabel = await this._eccpBaseElevatorLabelRepository.GetAll().Include(e => e.LabelStatus)
                                        .FirstOrDefaultAsync(
                                            e => e.UniqueId == input.UniqueId && e.LabelStatus.Name != "已失效");
            if (baseElevatorLabel != null)
            {
                input.Id = baseElevatorLabel.Id;
                input.UniqueId = baseElevatorLabel.UniqueId;
                input.UserId = baseElevatorLabel.UserId;
                var eccpDictLabelStatus =
                    await this._eccpDictLabelStatusRepository.FirstOrDefaultAsync(e => e.Name == "已绑定");
                input.LabelStatusId = eccpDictLabelStatus.Id;
                this.ObjectMapper.Map(input, baseElevatorLabel);

                // 添加日志
                var eccpBaseElevatorLabelBindLog = new EccpBaseElevatorLabelBindLog
                {
                    LabelName = baseElevatorLabel.LabelName,
                    LocalInformation = baseElevatorLabel.LocalInformation,
                    BindingTime = baseElevatorLabel.BindingTime,
                    BinaryObjectsId = baseElevatorLabel.BinaryObjectsId,
                    Remark =
                                                               "将标签" + baseElevatorLabel.UniqueId + "绑定到电梯"
                                                               + input.ElevatorId + "，LabelName：" + input.LabelName,
                    ElevatorLabelId = baseElevatorLabel.Id,
                    ElevatorId = baseElevatorLabel.ElevatorId,
                    LabelStatusId = baseElevatorLabel.LabelStatusId
                };
                Dictionary<string, string> parameters = new Dictionary<string, string>();
                parameters.Add("NFCNum", input.UniqueId);
                parameters.Add("CheckTermName", input.LabelName);
                var elevatorModel = this._eccpBaseElevatorRepository.FirstOrDefault(e => e.Id == baseElevatorLabel.ElevatorId.Value);
                if (elevatorModel != null)
                {
                    parameters.Add("LiftId", elevatorModel.SyncElevatorId.ToString());
                }
                JsonMessage jsonMessage = HttpsPost("https://www.dianti119.com/API/NFC/BindCheckNFC", parameters);
                if (jsonMessage.Code.Equals("1"))
                {
                    await this._eccpBaseElevatorLabelBindLogRepository.InsertAsync(eccpBaseElevatorLabelBindLog);
                    result = 1;
                }
                else
                {
                    result = -2;//远程接口错误，操作失败
                }
            }
            else
            {
                result = -1; // 标签未初始化
            }

            return result;
        }




        /// <summary>
        /// 标签绑定
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public JsonMessage BindCheckNFC(CreateOrEditEccpBaseElevatorNFCDto input)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("NFCNum", input.UniqueId);
            parameters.Add("CheckTermName", input.LabelName);
            parameters.Add("LiftId", input.ElevatorId);
            JsonMessage jsonMessage = HttpsPost("http://192.168.1.125:8080/API/NFC/BindCheckNFC", parameters);
            return jsonMessage;
        }
        /// <summary>
        /// The elevator label initialization.
        /// 标签初始化
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public JsonMessage SaveCheckNfc(CreateOrEditEccpBaseElevatorNFCDto input)
        {         
                Dictionary<string, string> parameters = new Dictionary<string, string>();
                parameters.Add("NFCNum", input.UniqueId);                
                JsonMessage jsonMessage = HttpsPost("http://192.168.1.125:8080/API/NFC/SaveCheckNfc", parameters);
                return jsonMessage;
        }
        /// <summary>
        /// The elevator label initialization.
        /// 标签初始化
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task<int> ElevatorLabelInitialization(CreateOrEditEccpBaseElevatorLabelDto input)
        {
            int result;
            var baseElevatorLabel = await this._eccpBaseElevatorLabelRepository.GetAll().Include(e => e.LabelStatus)
                                        .FirstOrDefaultAsync(
                                            e => e.UniqueId == input.UniqueId && e.LabelStatus.Name != "已失效");
            if (baseElevatorLabel == null)
            {
                var eccpBaseElevatorLabel = this.ObjectMapper.Map<EccpBaseElevatorLabel>(input);
                eccpBaseElevatorLabel.LabelName = "未命名";
                var eccpDictLabelStatus =
                    await this._eccpDictLabelStatusRepository.FirstOrDefaultAsync(e => e.Name == "未绑定");
                eccpBaseElevatorLabel.LabelStatusId = eccpDictLabelStatus.Id;
                eccpBaseElevatorLabel.BinaryObjectsId = Guid.NewGuid();

                if (this.AbpSession.TenantId != null)
                {
                    eccpBaseElevatorLabel.TenantId = this.AbpSession.TenantId;
                    eccpBaseElevatorLabel.UserId = this.AbpSession.UserId;
                }
                Dictionary<string, string> parameters = new Dictionary<string, string>();
                parameters.Add("NFCNum", input.UniqueId);
                parameters.Add("CheckTermName", input.LabelName);
                JsonMessage jsonMessage = HttpsPost("https://www.dianti119.com/API/NFC/SaveCheckNfc", parameters);
                if (jsonMessage.Code.Equals("1"))
                {
                    await this._eccpBaseElevatorLabelRepository.InsertAsync(eccpBaseElevatorLabel);
                    result = 1;
                }
                else
                {
                    result = -1;//远程接口错误，操作失败
                }
            }
            else
            {
                result = -1; // 标签已存在
            }

            return result;
        }

        /// <summary>
        /// The elevator label initialization.
        /// 扫码获取电梯信息
        /// </summary>
        /// <param name="LiftNum">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public JsonMessage GetElevator(string LiftNum)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("LiftNum", LiftNum);
            JsonMessage jsonMessage = HttpsPost("https://www.dianti119.com/API/LiftParts/GetLift", parameters);
            return jsonMessage;
        }

        /// <summary>
        /// The elevator label initialization.
        /// 取消绑定
        /// </summary>
        /// <param name="PartsId">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public JsonMessage DeleteTL_Parts(int PartsId)
        {
            JsonMessage jsonMessage = HttpsGet(string.Format("https://www.dianti119.com/API/LiftParts/DeleteTL_Parts?PartsId={0}", PartsId));
            return jsonMessage;
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
        public async Task<PagedResultDto<GetEccpBaseElevatorLabelForView>> GetAll(
            GetAllEccpBaseElevatorLabelsInput input)
        {
            var filteredEccpBaseElevatorLabels = this._eccpBaseElevatorLabelRepository.GetAll()
                .WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                    e => e.LabelName.Contains(input.Filter) || e.UniqueId.Contains(input.Filter)
                                                            || e.LocalInformation.Contains(input.Filter))
                .WhereIf(input.MinBindingTimeFilter != null, e => e.BindingTime >= input.MinBindingTimeFilter).WhereIf(
                    input.MaxBindingTimeFilter != null,
                    e => e.BindingTime <= input.MaxBindingTimeFilter);

            var query =
                (from o in filteredEccpBaseElevatorLabels
                 join o1 in this._eccpBaseElevatorRepository.GetAll() on o.ElevatorId equals o1.Id into j1
                 from s1 in j1.DefaultIfEmpty()
                 join o2 in this._eccpDictLabelStatusRepository.GetAll() on o.LabelStatusId equals o2.Id into j2
                 from s2 in j2.DefaultIfEmpty()
                 join o3 in this._userRepository.GetAll() on o.UserId equals o3.Id into j3
                 from s3 in j3.DefaultIfEmpty()
                 select new
                 {
                     EccpBaseElevatorLabel = o,
                     EccpBaseElevatorName = s1 == null ? string.Empty : s1.Name,
                     EccpDictLabelStatusName = s2 == null ? string.Empty : s2.Name,
                     UserName = s3 == null ? string.Empty : s3.Name
                 }).WhereIf(
                    !string.IsNullOrWhiteSpace(input.EccpBaseElevatorNameFilter),
                    e => e.EccpBaseElevatorName.ToLower() == input.EccpBaseElevatorNameFilter.ToLower().Trim()).WhereIf(
                    !string.IsNullOrWhiteSpace(input.EccpDictLabelStatusNameFilter),
                    e => e.EccpDictLabelStatusName.ToLower() == input.EccpDictLabelStatusNameFilter.ToLower().Trim())
                .WhereIf(
                    !string.IsNullOrWhiteSpace(input.UserNameFilter),
                    e => e.UserName.ToLower() == input.UserNameFilter.ToLower().Trim());

            var totalCount = await query.CountAsync();

            var eccpBaseElevatorLabels = new List<GetEccpBaseElevatorLabelForView>();

            query.OrderBy(input.Sorting ?? "eccpBaseElevatorLabel.id asc").PageBy(input).MapTo(eccpBaseElevatorLabels);

            return new PagedResultDto<GetEccpBaseElevatorLabelForView>(totalCount, eccpBaseElevatorLabels);
        }

        /// <summary>
        /// The get all eccp base elevator for lookup table.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [AbpAuthorize(AppPermissions.Pages_EccpElevator_EccpBaseElevatorLabels)]
        public async Task<PagedResultDto<EccpBaseElevatorLookupTableDto>> GetAllEccpBaseElevatorForLookupTable(
            GetAllForLookupTableInput input)
        {
            var query = this._eccpBaseElevatorRepository.GetAll().WhereIf(
                !string.IsNullOrWhiteSpace(input.Filter),
                e => e.Name.ToString().Contains(input.Filter));

            var totalCount = await query.CountAsync();

            var eccpBaseElevatorList = await query.PageBy(input).ToListAsync();

            var lookupTableDtoList = new List<EccpBaseElevatorLookupTableDto>();
            foreach (var eccpBaseElevator in eccpBaseElevatorList)
            {
                lookupTableDtoList.Add(
                    new EccpBaseElevatorLookupTableDto
                    {
                        Id = eccpBaseElevator.Id.ToString(),
                        DisplayName = eccpBaseElevator.Name
                    });
            }

            return new PagedResultDto<EccpBaseElevatorLookupTableDto>(totalCount, lookupTableDtoList);
        }

        /// <summary>
        /// The get all eccp dict label status for lookup table.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [AbpAuthorize(AppPermissions.Pages_EccpElevator_EccpBaseElevatorLabels)]
        public async Task<PagedResultDto<EccpDictLabelStatusLookupTableDto>> GetAllEccpDictLabelStatusForLookupTable(
            GetAllForLookupTableInput input)
        {
            var query = this._eccpDictLabelStatusRepository.GetAll().WhereIf(
                !string.IsNullOrWhiteSpace(input.Filter),
                e => e.Name.ToString().Contains(input.Filter));

            var totalCount = await query.CountAsync();

            var eccpDictLabelStatusList = await query.PageBy(input).ToListAsync();

            var lookupTableDtoList = new List<EccpDictLabelStatusLookupTableDto>();
            foreach (var eccpDictLabelStatus in eccpDictLabelStatusList)
            {
                lookupTableDtoList.Add(
                    new EccpDictLabelStatusLookupTableDto
                    {
                        Id = eccpDictLabelStatus.Id,
                        DisplayName = eccpDictLabelStatus.Name
                    });
            }

            return new PagedResultDto<EccpDictLabelStatusLookupTableDto>(totalCount, lookupTableDtoList);
        }

        /// <summary>
        /// The get all user for lookup table.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [AbpAuthorize(AppPermissions.Pages_EccpElevator_EccpBaseElevatorLabels)]
        public async Task<PagedResultDto<UserLookupTableDto>> GetAllUserForLookupTable(GetAllForLookupTableInput input)
        {
            var query = this._userRepository.GetAll().WhereIf(
                !string.IsNullOrWhiteSpace(input.Filter),
                e => e.Name.ToString().Contains(input.Filter));

            var totalCount = await query.CountAsync();

            var userList = await query.PageBy(input).ToListAsync();

            var lookupTableDtoList = new List<UserLookupTableDto>();
            foreach (var user in userList)
            {
                lookupTableDtoList.Add(new UserLookupTableDto { Id = user.Id, DisplayName = user.Name });
            }

            return new PagedResultDto<UserLookupTableDto>(totalCount, lookupTableDtoList);
        }

        /// <summary>
        /// The get eccp base elevator label for edit.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [AbpAuthorize(AppPermissions.Pages_EccpElevator_EccpBaseElevatorLabels_Edit)]
        public async Task<GetEccpBaseElevatorLabelForEditOutput> GetEccpBaseElevatorLabelForEdit(EntityDto<long> input)
        {
            var eccpBaseElevatorLabel = await this._eccpBaseElevatorLabelRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetEccpBaseElevatorLabelForEditOutput
            {
                EccpBaseElevatorLabel =
                                     this.ObjectMapper.Map<CreateOrEditEccpBaseElevatorLabelDto>(eccpBaseElevatorLabel)
            };

            if (output.EccpBaseElevatorLabel.ElevatorId != null)
            {
                var eccpBaseElevator =
                    await this._eccpBaseElevatorRepository.FirstOrDefaultAsync(
                        (Guid)output.EccpBaseElevatorLabel.ElevatorId);
                output.EccpBaseElevatorName = eccpBaseElevator.Name;
            }

            var eccpDictLabelStatus =
                await this._eccpDictLabelStatusRepository.FirstOrDefaultAsync(
                    output.EccpBaseElevatorLabel.LabelStatusId);
            output.EccpDictLabelStatusName = eccpDictLabelStatus.Name;

            if (output.EccpBaseElevatorLabel.UserId != null)
            {
                var user = await this._userRepository.FirstOrDefaultAsync((long)output.EccpBaseElevatorLabel.UserId);
                output.UserName = user.Name;
            }

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
        [AbpAuthorize(AppPermissions.Pages_EccpElevator_EccpBaseElevatorLabels_Create)]
        private async Task Create(CreateOrEditEccpBaseElevatorLabelDto input)
        {
            var eccpBaseElevatorLabel = this.ObjectMapper.Map<EccpBaseElevatorLabel>(input);

            if (this.AbpSession.TenantId != null)
            {
                eccpBaseElevatorLabel.TenantId = this.AbpSession.TenantId;
            }

            await this._eccpBaseElevatorLabelRepository.InsertAsync(eccpBaseElevatorLabel);
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
        [AbpAuthorize(AppPermissions.Pages_EccpElevator_EccpBaseElevatorLabels_Edit)]
        private async Task Update(CreateOrEditEccpBaseElevatorLabelDto input)
        {
            if (input.Id != null)
            {
                var eccpBaseElevatorLabel =
                    await this._eccpBaseElevatorLabelRepository.FirstOrDefaultAsync((long)input.Id);
                this.ObjectMapper.Map(input, eccpBaseElevatorLabel);
            }
        }
        /// <summary>
        /// 调用POST请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private JsonMessage HttpsPost(string url, IDictionary<string, string> parameters)
        {
            HttpWebRequest https = (HttpWebRequest)HttpWebRequest.Create(url);
            //utf-8编码                  
            https.ContentType = "application/x-www-form-urlencoded;charset=utf-8";
            https.Method = "POST";
            https.Accept = "text/plain;charset=utf-8";
            Encoding requestEncoding = Encoding.GetEncoding("UTF-8");
            if (!(parameters == null || parameters.Count == 0))
            {
                StringBuilder buffer = new StringBuilder();
                int i = 0;
                foreach (string key in parameters.Keys)
                {
                    if (i > 0)
                    {
                        buffer.AppendFormat("&{0}={1}", key, parameters[key]);
                    }
                    else
                    {
                        buffer.AppendFormat("{0}={1}", key, parameters[key]);
                    }
                    i++;
                }
                byte[] data = requestEncoding.GetBytes(buffer.ToString());
                using (Stream stream = https.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }
            }

            HttpWebResponse myResponse;
            //4读取服务器的返回信息
            try
            {
                myResponse = (HttpWebResponse)https.GetResponse();
            }
            catch (WebException ex)
            {
                myResponse = (HttpWebResponse)ex.Response;
            }

            StreamReader reader = new StreamReader(myResponse.GetResponseStream(), Encoding.UTF8);
            string ReqResult = reader.ReadToEnd();
            reader.Close();
            myResponse.Close();
            return JsonConvert.DeserializeObject<JsonMessage>(ReqResult);
        }
        /// <summary>
        /// 调用GET请求
        /// </summary>
        /// <param name="url"></param>        
        /// <returns></returns>
        private JsonMessage HttpsGet(string url)
        {
            HttpWebRequest https = (HttpWebRequest)HttpWebRequest.Create(url);
            //utf-8编码                  
            https.ContentType = "application/x-www-form-urlencoded;charset=utf-8";
            https.Method = "GET";
            https.Accept = "text/plain;charset=utf-8";
            Encoding requestEncoding = Encoding.GetEncoding("UTF-8");

            HttpWebResponse myResponse;
            //4读取服务器的返回信息
            try
            {
                myResponse = (HttpWebResponse)https.GetResponse();
            }
            catch (WebException ex)
            {
                myResponse = (HttpWebResponse)ex.Response;
            }

            StreamReader reader = new StreamReader(myResponse.GetResponseStream(), Encoding.UTF8);
            string ReqResult = reader.ReadToEnd();
            reader.Close();
            myResponse.Close();
            return JsonConvert.DeserializeObject<JsonMessage>(ReqResult);
        }
    }



    public class JsonMessage
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool Success { get; set; }
        /// <summary>
        /// 结果编码
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 结果消息
        /// </summary>
        public string Message { get; set; }

        public string Data { get; set; }

    }
    public class SignLift
    {
        /// <summary>
        /// 电梯编号
        /// </summary>
        public string LiftNum { get; set; }
        /// <summary>
        /// 注册代码
        /// </summary>
        public string CertificateNum { get; set; }
        /// <summary>
        /// 经度
        /// </summary>
        public double Longitude { get; set; }
        /// <summary>
        /// 纬度
        /// </summary>
        public double Latitude { get; set; }

        public int PartsTypeId { get; set; }

    }
}