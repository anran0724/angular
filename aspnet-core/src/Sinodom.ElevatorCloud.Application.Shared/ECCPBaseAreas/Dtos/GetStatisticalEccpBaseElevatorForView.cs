
using System.Collections.Generic;
using System.Linq;
namespace Sinodom.ElevatorCloud.ECCPBaseAreas.Dtos
{
    public class GetStatisticalEccpBaseElevatorForView
    {
        public string Name { get; set; }

        public string CertificateNum { get; set; }

        public string PlanCheckDate { get; set; }


        public string ComplateDate { get; set; }

        public IQueryable<MaintenanceUser> MaintenanceUserNameList { get; set; }
    }


    public class MaintenanceUser {

          public  string MaintenanceUserName { get; set; }
          
    }
}
