using Abp.Application.Services.Dto;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sinodom.ElevatorCloud.Authorization.Users.Dto
{
    public class AssociationUseListDto : EntityDto<long>, IPassivable, IHasCreationTime
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string UserName { get; set; }
        public string EmailAddress { get; set; }
        public string CompanyName { get; set; }
        public string Mobile { get; set; }
        public int CheckState { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreationTime { get; set; }
        public int CompanyType { get; set; }
    }
}
