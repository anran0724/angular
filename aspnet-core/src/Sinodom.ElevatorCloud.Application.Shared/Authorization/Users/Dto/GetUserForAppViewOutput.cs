namespace Sinodom.ElevatorCloud.Authorization.Users.Dto
{
    using System;

    public class GetUserForAppViewOutput
    {
        public Guid? ProfilePictureId { get; set; }

        public UserEditDto User { get; set; }

        public EccpCompanyUserExtensionEditDto CompanyUser { get; set; }
    }
}