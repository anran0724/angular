using Sinodom.ElevatorCloud.EccpBaseSaicUnits.Dtos;
using Abp.Extensions;

namespace Sinodom.ElevatorCloud.Web.Areas.AppAreaName.Models.EccpBaseSaicUnits
{
    public class CreateOrEditEccpBaseSaicUnitModalViewModel
    {
        public EditEccpBaseSaicUnitDto EccpBaseSaicUnit { get; set; }

        public string ECCPBaseAreaName { get; set; }

        public string ECCPBaseAreaName2 { get; set; }

        public string ECCPBaseAreaName3 { get; set; }

        public string ECCPBaseAreaName4 { get; set; }


        public bool IsEditMode => EccpBaseSaicUnit.Id.HasValue;
    }
}