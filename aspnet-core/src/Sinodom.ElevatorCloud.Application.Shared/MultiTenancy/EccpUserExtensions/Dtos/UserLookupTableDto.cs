using Abp.Application.Services.Dto;

namespace Sinodom.ElevatorCloud.MultiTenancy.EccpUserExtensions.Dtos
{
    public class UserLookupTableDto
    {
		public long Id { get; set; }

		public string DisplayName { get; set; }
    }
}