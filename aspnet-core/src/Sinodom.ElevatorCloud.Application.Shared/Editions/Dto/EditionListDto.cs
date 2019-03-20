using System;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities.Auditing;

namespace Sinodom.ElevatorCloud.Editions.Dto
{
    public class EditionListDto : EntityDto, IHasCreationTime
    {
        public string Name { get; set; }

        public string DisplayName { get; set; }

        public DateTime CreationTime { get; set; }

        public int? ECCPEditionsTypeId { get; set; }

        public string ECCPEditionsTypeName { get; set; }

        public bool IsRegister { get; set; }
    }
}
