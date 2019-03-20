using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sinodom.ElevatorCloud.Authorization.Users.Dto
{
    public class CreateOrUpdateUserInput
    {
        [Required]
        public UserEditDto User { get; set; }

        [Required]
        public string[] AssignedRoleNames { get; set; }

        public bool SendActivationEmail { get; set; }

        public bool SetRandomPassword { get; set; }

        public List<long> OrganizationUnits { get; set; }

        public string SignPictureToken { get; set; }
        public string CertificateBackPictureToken { get; set; }
        public string CertificateFrontPictureToken { get; set; }
       public EccpCompanyUserExtensionEditDto CompanyUser { get; set; }
        public CreateOrUpdateUserInput()
        {
            OrganizationUnits = new List<long>();
        }
    }
}
