using System;
using System.Collections.Generic;
using System.Text;

namespace Sinodom.ElevatorCloud.Common.Dto
{
    public class ElevatorsDataSynchronousEntity
    {
        /// <summary>
        /// 电梯名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 注册证号
        /// </summary>
        public string CertificateNum { get; set; }
        /// <summary>
        /// 出厂编号
        /// </summary>
        public string MachineNum { get; set; }

        /// <summary>
        /// 安装地址
        /// </summary>
        public string InstallationAddress { get; set; }
        /// <summary>
        /// 安装时间
        /// </summary>
        public DateTime? InstallationDatetime { get; set; }

        /// <summary>
        /// 省
        /// </summary>
        public int? ProvinceId { get; set; }
        /// <summary>
        /// 城市
        /// </summary>
        public int? CityId { get; set; }
        /// <summary>
        /// 区
        /// </summary>
        public int? DistrictId { get; set; }
        /// <summary>
        /// 省
        /// </summary>
        public string ProvinceName { get; set; }
        /// <summary>
        /// 城市
        /// </summary>
        public string CityName { get; set; }
        /// <summary>
        /// 区
        /// </summary>
        public string DistrictName { get; set; }
        /// <summary>
        /// 街道
        /// </summary>
        public int? StreetId { get; set; }
        /// <summary>
        /// 经度
        /// </summary>
        public string Longitude { get; set; }
        /// <summary>
        /// 纬度
        /// </summary>
        public string Latitude { get; set; }
        /// <summary>
        /// 同步数据ID
        /// </summary>
        public int? SyncElevatorId { get; set; }
        /// <summary>
        /// 使用场所ID
        /// </summary>
        public int? EccpDictPlaceTypeId { get; set; }

        /// <summary>
        /// 使用场所
        /// </summary>
        public string EccpDictPlaceTypeName { get; set; }
        /// <summary>
        /// 电梯类型ID
        /// </summary>
        public int? EccpDictElevatorTypeId { get; set; }

        /// <summary>
        /// 电梯类型
        /// </summary>
        public string EccpDictElevatorTypeName { get; set; }
        /// <summary>
        /// 电梯状态ID
        /// </summary>
        public int? ECCPDictElevatorStatusId { get; set; }

        /// <summary>
        /// 电梯状态
        /// </summary>
        public string ECCPDictElevatorStatusName { get; set; }
        /// <summary>
        /// 园区ID
        /// </summary>
        public int? ECCPBaseCommunityId { get; set; }

        /// <summary>
        /// 园区
        /// </summary>
        public string ECCPBaseCommunityName { get; set; }
        /// <summary>
        /// 维保公司同步ID
        /// </summary>
        public int? MaintenanceCompanyId { get; set; }
        /// <summary>
        /// 使用公司同步ID
        /// </summary>
        public int? PropertyCompanyId { get; set; }
        /// <summary>
        /// 维保公司ID
        /// </summary>
        public int? ECCPBaseMaintenanceCompanyId { get; set; }

        /// <summary>
        /// 使用单位ID
        /// </summary>
        public int? ECCPBasePropertyCompanyId { get; set; }

        /// <summary>
        /// 租户ID（使用单位租户ID）
        /// </summary>
        public int? TenantId { get; set; }

        /// <summary>
        /// 用户编号
        /// </summary>
        public string CustomNum { get; set; }
        /// <summary>
        /// 制造许可证编号
        /// </summary>
        public string ManufacturingLicenseNumber { get; set; }
        /// <summary>
        /// 楼层数
        /// </summary>
        public int? FloorNumber { get; set; }
        /// <summary>
        /// 门数
        /// </summary>
        public int? GateNumber { get; set; }
        /// <summary>
        /// 额定速度
        /// </summary>
        public float? RatedSpeed { get; set; }
        /// <summary>
        /// 载重量
        /// </summary>
        public float? Deadweight { get; set; }
        /// <summary>
        /// 省份简称
        /// </summary>
        public string AreaName { get; set; }

        /// <summary>
        /// 电梯编号
        /// </summary>
        public string ElevatorNum { get; set; }
        /// <summary>
        /// 是否安装
        /// </summary>
        public bool IsInstall { get; set; }
        /// <summary>
        /// 是否发放
        /// </summary>
        public bool IsGrant { get; set; }
        /// <summary>
        /// 安装日期
        /// </summary>
        public DateTime? InstallDateTime { get; set; }
        /// <summary>
        /// 发放日期
        /// </summary>
        public DateTime? GrantDateTime { get; set; }
    }
}
