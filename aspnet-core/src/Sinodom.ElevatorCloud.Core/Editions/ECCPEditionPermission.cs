using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Application.Editions;
using Abp.Domain.Entities.Auditing;

namespace Sinodom.ElevatorCloud.Editions
{
    [Table("ECCPEditionPermissions")]
    public class ECCPEditionPermission : FullAuditedEntity
    {
        [Required]
        [StringLength(ECCPEditionPermissionConsts.MaxNameLength, MinimumLength = ECCPEditionPermissionConsts.MinNameLength)]
        public virtual string Name { get; set; }

        public virtual bool IsGranted { get; set; }

        public virtual int EditionId { get; set; }

        public Edition Edition { get; set; }
    }
}