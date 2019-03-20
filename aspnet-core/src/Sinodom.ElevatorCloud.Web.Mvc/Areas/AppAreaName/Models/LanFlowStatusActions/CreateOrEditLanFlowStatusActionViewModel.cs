using Sinodom.ElevatorCloud.LanFlows.Dtos;

namespace Sinodom.ElevatorCloud.Web.Areas.AppAreaName.Models.LanFlowStatusActions
{
    public class CreateOrEditLanFlowStatusActionModalViewModel
    {
       public CreateOrEditLanFlowStatusActionDto LanFlowStatusAction { get; set; }

	   		public string LanFlowSchemeSchemeName { get; set;}


	   public bool IsEditMode => LanFlowStatusAction.Id.HasValue;
    }
}