using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Sinodom.ElevatorCloud.Authorization.Users.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sinodom.ElevatorCloud.Authorization.Users
{
    public  interface IEccpCompanyUserExtensionsAppServicer : IApplicationService
    {
        Task<PagedResultDto<AssociationUseListDto>> GetUsers(GetAssociationUsersInput input);

        Task UpdateCompanyUserCheckStateAsync(int UserId, int CheckState,string Remarks);
    }
}
