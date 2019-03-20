using Sinodom.ElevatorCloud.LanFlows.Dtos;

namespace Sinodom.ElevatorCloud.Web.Areas.AppAreaName.Models.LanFlowSchemes
{
    public class CreateOrEditLanFlowSchemeModalViewModel
    {
       public CreateOrEditLanFlowSchemeDto LanFlowScheme { get; set; }

	   
	   public bool IsEditMode => LanFlowScheme.Id.HasValue;
    }
}