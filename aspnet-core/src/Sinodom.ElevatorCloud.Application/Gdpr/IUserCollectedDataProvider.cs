using System.Collections.Generic;
using System.Threading.Tasks;
using Abp;
using Sinodom.ElevatorCloud.Dto;

namespace Sinodom.ElevatorCloud.Gdpr
{
    public interface IUserCollectedDataProvider
    {
        Task<List<FileDto>> GetFiles(UserIdentifier user);
    }
}
