using Sinodom.ElevatorCloud.EntityFrameworkCore;
using Sinodom.ElevatorCloud.MultiTenancy.UserExtensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sinodom.ElevatorCloud.Tests.TestDatas
{
    public class TestEccpCompanyUserExtensionsBuilder
    {
        private readonly ElevatorCloudDbContext _context;

        public TestEccpCompanyUserExtensionsBuilder(ElevatorCloudDbContext context)
        {
            this._context = context;
        }

        public void Create()
        {
            this.CreateCompanyUserExtensions();
        }

        private void CreateCompanyUserExtensions()
        {
            this._context.EccpCompanyUserExtensions.Add(
                new EccpCompanyUserExtension
                {
                    IdCard = "10000",
                    Mobile = "13812312312",
                    SignPictureId = new Guid("ceb4a760-a3d9-dd62-8b1b-39ea28a2d423"),
                    CertificateFrontPictureId = new Guid("ceb4a760-a3d9-dd62-8b1b-39ea28a2d423"),
                    CertificateBackPictureId = new Guid("ceb4a760-a3d9-dd62-8b1b-39ea28a2d423"),
                    CheckState = 1,
                    UserId = 1
                });

            this._context.SaveChanges();
        }
    }
}
