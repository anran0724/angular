using Abp.Application.Services.Dto;
using Sinodom.ElevatorCloud.ECCPBaseAreas;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Sinodom.ElevatorCloud.Common.Dto
{
    /// <summary>
    /// 区域
    /// </summary>
    public class SyncAreaInput : FullAuditedEntityDto<int?>
    {
        public int ParentId { get; set; }

        [StringLength(ECCPBaseAreaConsts.MaxCodeLength, MinimumLength = ECCPBaseAreaConsts.MinCodeLength)]
        public string Code { get; set; }

        [Required]
        [StringLength(ECCPBaseAreaConsts.MaxNameLength, MinimumLength = ECCPBaseAreaConsts.MinNameLength)]
        public string Name { get; set; }

        public int Level { get; set; }

        public string Path { get; set; }
    }
}
