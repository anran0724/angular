using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sinodom.ElevatorCloud
{
    public interface IHasArea
    {
        int? ProvinceId { get; set; }
        int? CityId { get; set; }
        int? DistrictId { get; set; }
        int? StreetId { get; set; }
    }
}
