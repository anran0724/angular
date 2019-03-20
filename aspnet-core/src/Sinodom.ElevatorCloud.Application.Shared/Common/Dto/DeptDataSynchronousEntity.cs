using System;
using System.Collections.Generic;
using System.Text;

namespace Sinodom.ElevatorCloud.Common.Dto
{
    public class DeptDataSynchronousEntity
    {
        /// <summary>
        /// 单位名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string Telephone { get; set; }
        /// <summary>
        /// 单位地址
        /// </summary>
        public string Addresse { get; set; }
        /// <summary>
        /// 经度
        /// </summary>
        public string Longitude { get; set; }
        /// <summary>
        /// 纬度
        /// </summary>
        public string Latitude { get; set; }

        /// <summary>
        /// 企业简介
        /// </summary>
        public string Summary { get; set; }

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
        /// 街道
        /// </summary>
        public int? StreetId { get; set; }
        /// <summary>
        /// 组织机构代码证号
        /// </summary>
        public string OrgOrganizationalCode { get; set; }

        /// <summary>
        /// 法人
        /// </summary>
        public string LegalPerson { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// 营业执照
        /// </summary>
        public byte[] BusinessLicenseBytes { get; set; }

        /// <summary>
        /// 资质照片
        /// </summary>
        public byte[] AptitudePhotoBytes { get; set; }

        /// <summary>
        /// 是否会员
        /// </summary>
        public bool IsMember { get; set; }
        /// <summary>
        /// 角色组
        /// </summary>
        public string RoleGroupCode { get; set; }
        /// <summary>
        /// 是否激活
        /// </summary>
        public bool IsActive { get; set; }
        /// <summary>
        /// 版本ID
        /// </summary>
        public int EditionId { get; set; }
        /// <summary>
        /// 同步公司ID
        /// </summary>
        public int? SyncCompanyId { get; set; }
    }
}
