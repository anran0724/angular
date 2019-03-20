using Abp.Runtime.Validation;
using Sinodom.ElevatorCloud.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sinodom.ElevatorCloud.Authorization.Users.Dto
{
    public class GetAssociationUsersInput : PagedAndSortedInputDto, IShouldNormalize
    {

        public string Filter { get; set; }

        public int CheckState { get; set; }
        public void Normalize()
        {
            if (string.IsNullOrEmpty(Sorting))
            {
                Sorting = "Name,Surname";
            }
        }
    }
}
