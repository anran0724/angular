using Sinodom.ElevatorCloud.EccpAppVersions.Dtos;

namespace Sinodom.ElevatorCloud.Web.Areas.AppAreaName.Models.EccpAppVersions
{
    public class CreateOrEditEccpAppVersionModalViewModel
    {
       public CreateOrEditEccpAppVersionDto EccpAppVersion { get; set; }

	   
	   public bool IsEditMode => EccpAppVersion.Id.HasValue;
    }
}