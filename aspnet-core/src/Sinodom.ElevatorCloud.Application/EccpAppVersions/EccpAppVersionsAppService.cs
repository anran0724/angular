
using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Sinodom.ElevatorCloud.EccpAppVersions.Dtos;
using Sinodom.ElevatorCloud.Dto;
using Abp.Application.Services.Dto;
using Sinodom.ElevatorCloud.Authorization;
using Abp.Authorization;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using Sinodom.ElevatorCloud.Storage;

namespace Sinodom.ElevatorCloud.EccpAppVersions
{
    [AbpAuthorize(AppPermissions.Pages_Administration_EccpAppVersions)]
    public class EccpAppVersionsAppService : ElevatorCloudAppServiceBase, IEccpAppVersionsAppService
    {
        private readonly IRepository<EccpAppVersion> _eccpAppVersionRepository;
        /// <summary>
        /// The _temp file cache manager.
        /// </summary>
        private readonly ITempFileCacheManager _tempFileCacheManager;
        private readonly IBinaryObjectManager _binaryObjectManager;

        public EccpAppVersionsAppService(IRepository<EccpAppVersion> eccpAppVersionRepository, ITempFileCacheManager tempFileCacheManager, IBinaryObjectManager binaryObjectManager)
        {
            _eccpAppVersionRepository = eccpAppVersionRepository;
            _tempFileCacheManager = tempFileCacheManager;
            _binaryObjectManager = binaryObjectManager;
        }

        public async Task<PagedResultDto<GetEccpAppVersionForView>> GetAll(GetAllEccpAppVersionsInput input)
        {

            var filteredEccpAppVersions = _eccpAppVersionRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.VersionName.Contains(input.Filter) || e.VersionCode.Contains(input.Filter) || e.UpdateLog.Contains(input.Filter) || e.DownloadUrl.Contains(input.Filter) || e.VersionType.Contains(input.Filter));


            var query = (from o in filteredEccpAppVersions

                         select new GetEccpAppVersionForView()
                         {
                             EccpAppVersion = ObjectMapper.Map<EccpAppVersionDto>(o)

                         })
                         ;

            var totalCount = await query.CountAsync();

            var eccpAppVersions = await query
                .OrderBy(input.Sorting ?? "eccpAppVersion.id asc")
                .PageBy(input)
                .ToListAsync();

            return new PagedResultDto<GetEccpAppVersionForView>(
                totalCount,
                eccpAppVersions
            );
        }

        [AbpAuthorize(AppPermissions.Pages_Administration_EccpAppVersions_Edit)]
        public async Task<GetEccpAppVersionForEditOutput> GetEccpAppVersionForEdit(EntityDto input)
        {
            var eccpAppVersion = await _eccpAppVersionRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetEccpAppVersionForEditOutput { EccpAppVersion = ObjectMapper.Map<CreateOrEditEccpAppVersionDto>(eccpAppVersion) };

            return output;
        }

        public async Task CreateOrEdit(UploadCreateOrEditEccpAppVersionDto input)
        {
            if (input.Id == null)
            {
                await Create(input);
            }
            else
            {
                await Update(input);
            }
        }

        [AbpAuthorize(AppPermissions.Pages_Administration_EccpAppVersions_Create)]
        private async Task Create(UploadCreateOrEditEccpAppVersionDto input)
        {
            if (!string.IsNullOrWhiteSpace(input.FileToken)
                && input.FileToken != "4811d3f3-0bfa-4672-b875-2d47299d175d.apk")
            {
                byte[] byteArray = this._tempFileCacheManager.GetFile(input.FileToken);
                if (byteArray == null)
                {
                    throw new UserFriendlyException("There is no such image file with the token: " + input.FileToken);
                }
                var storedFile = new BinaryObject(this.AbpSession.TenantId, byteArray);
                await this._binaryObjectManager.SaveAsync(storedFile);

                input.DownloadUrl = storedFile.Id.ToString();
            }
            var eccpAppVersion = ObjectMapper.Map<EccpAppVersion>(input);



            await _eccpAppVersionRepository.InsertAsync(eccpAppVersion);
        }

        [AbpAuthorize(AppPermissions.Pages_Administration_EccpAppVersions_Edit)]
        private async Task Update(UploadCreateOrEditEccpAppVersionDto input)
        {
            var eccpAppVersion = await _eccpAppVersionRepository.FirstOrDefaultAsync((int)input.Id);
            if (input.FileToken != eccpAppVersion.DownloadUrl && input.FileToken != "4811d3f3-0bfa-4672-b875-2d47299d175d.apk")
            {
                byte[] byteArray = this._tempFileCacheManager.GetFile(input.FileToken);
                if (byteArray == null)
                {
                    throw new UserFriendlyException("There is no such image file with the token: " + input.FileToken);
                }

                if (!string.IsNullOrWhiteSpace(eccpAppVersion.DownloadUrl))
                {
                    await this._binaryObjectManager.DeleteAsync(new Guid(eccpAppVersion.DownloadUrl));
                }

                var storedFile = new BinaryObject(this.AbpSession.TenantId, byteArray);
                await this._binaryObjectManager.SaveAsync(storedFile);

                input.DownloadUrl = storedFile.Id.ToString();
            }
            else
            {
                if (eccpAppVersion.DownloadUrl != null)
                {
                    input.DownloadUrl = eccpAppVersion.DownloadUrl;
                }
            }
            ObjectMapper.Map(input, eccpAppVersion);
        }

        [AbpAuthorize(AppPermissions.Pages_Administration_EccpAppVersions_Delete)]
        public async Task Delete(EntityDto input)
        {
            await _eccpAppVersionRepository.DeleteAsync(input.Id);
        }

        [AbpAllowAnonymous]
        public async Task<GetEccpAppVersionForEditOutput> GetEccpAppVersionByVersionType(string versionType)
        {
            var eccpAppVersion = await _eccpAppVersionRepository.GetAll().OrderByDescending(e => e.VersionCode).FirstOrDefaultAsync(e => e.VersionType == versionType);

            var output = new GetEccpAppVersionForEditOutput { EccpAppVersion = ObjectMapper.Map<CreateOrEditEccpAppVersionDto>(eccpAppVersion) };
            if (output.EccpAppVersion != null)
            {
                //TODO:暂时返回蒲公英下载地址
                output.EccpAppVersion.DownloadUrl = "https://www.pgyer.com/IZvQ";
            }

            return output;
        }
    }
}