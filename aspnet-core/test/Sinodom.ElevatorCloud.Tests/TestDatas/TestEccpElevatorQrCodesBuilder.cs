using Sinodom.ElevatorCloud.EccpElevatorQrCodes;
using Sinodom.ElevatorCloud.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sinodom.ElevatorCloud.Tests.TestDatas
{
    public class TestEccpElevatorQrCodesBuilder
    {
        private readonly ElevatorCloudDbContext _context;

        public TestEccpElevatorQrCodesBuilder(ElevatorCloudDbContext context)
        {
            this._context = context;
        }

        public void Create()
        {
            this.CreateElevatorQrCodes();
        }

        private void CreateElevatorQrCodes()
        {
            var elevatorsEntity = this._context.EccpBaseElevators.FirstOrDefault(e => e.Name == "测试电梯5");
            var newElevatorsEntity = this._context.EccpBaseElevators.FirstOrDefault(e => e.Name == "测试电梯6");

            this._context.EccpElevatorQrCodes.Add(
                 new EccpElevatorQrCode
                 {
                     AreaName = "黑",
                     ElevatorNum = "999999",
                     ElevatorId = elevatorsEntity.Id,
                     ImgPicture = "测试图片ID",
                 });

            this._context.EccpElevatorQrCodes.Add(
                 new EccpElevatorQrCode
                 {
                     AreaName = "辽",
                     ElevatorNum = "999999",
                     ElevatorId = newElevatorsEntity.Id,
                     ImgPicture = "测试图片ID",
                 });

            this._context.SaveChanges();
        }
    }
}
