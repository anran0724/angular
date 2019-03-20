using System;
using System.Collections.Generic;
using System.Text;

namespace Sinodom.ElevatorCloud.EccpBaseElevators.Dtos
{
    public class GetChangeLogByElevatorIdDto
    {
        public string FieldName { get; set; }

        public string NewValue { get; set; }

        public string OldValue { get; set; }

        public DateTime? CreationTime { get; set; }
        public string CreatorUserName { get; set; }

        public Guid? ProfilePictureId { get; set; }

        public string TenantName { get; set; }
    }
}
