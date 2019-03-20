using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Runtime.Session;

namespace Sinodom.ElevatorCloud.Extensions
{
    public interface IEccpAbpSessionExtensions : IAbpSession
    {
        /// <summary>
        /// 省ID
        /// </summary>
        int? ProvinceId { get; }
        /// <summary>
        /// 城市ID
        /// </summary>
        int? CityId { get; }
        /// <summary>
        /// 区ID
        /// </summary>
        int? DistrictId { get; }
        /// <summary>
        /// 街道ID
        /// </summary>
        int? StreetId { get; }
        /// <summary>
        /// 版本ID
        /// </summary>
        int? EditionId { get; }
        /// <summary>
        /// 版本类型名称
        /// </summary>
        string EditionTypeName { get; }
        /// <summary>
        /// 公司名称
        /// </summary>
        string CompanieName { get; }
    }
}
