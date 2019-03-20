using Sinodom.ElevatorCloud.ECCPBaseCommunities;
using Sinodom.ElevatorCloud.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sinodom.ElevatorCloud.Tests.TestDatas
{
    public class TestECCPBaseCommunitiesBuilder
    {

        private readonly ElevatorCloudDbContext _context;

        public TestECCPBaseCommunitiesBuilder(ElevatorCloudDbContext context)
        {
            this._context = context;
        }

        public void Create()
        {
            this.CreateCommunities("测试园区1", "测试地址1", "测试经纬度1", "测试经纬度2");
            this.CreateCommunities("测试园区", "地址", "123", "123");
        }

        private void CreateCommunities(string name, string address, string lat, string lng)
        {
            var areaEntity = this._context.ECCPBaseAreas.FirstOrDefault(e => e.Name == "海南省");

            this._context.ECCPBaseCommunities.Add(
                            new ECCPBaseCommunity
                            {
                                Name = name,
                                Address = address,
                                Longitude = lat,
                                Latitude = lng,
                                ProvinceId = areaEntity.Id
                            });

            this._context.SaveChanges();
        }

    }
}
