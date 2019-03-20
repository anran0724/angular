using System;
using System.Collections.Generic;
using System.Text;

namespace Sinodom.ElevatorCloud.Common.Dto
{
    public class UserDataSynchronousEntity
    {
        /// <summary>
        /// 用户名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 邮箱地址
        /// </summary>
        public string EmailAddress { get; set; }
        /// <summary>
        /// 姓
        /// </summary>
        public string Surname { get; set; }
        /// <summary>
        /// 登录名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 公司
        /// </summary>
        public string Companies { get; set; }
        /// <summary>
        /// 公司ID
        /// </summary>
        public int? CompaniesId { get; set; }
        /// <summary>
        /// 身份证
        /// </summary>
        public string IdCard { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        public string Mobile { get; set; }
        /// <summary>
        /// 签名
        /// </summary>
        public byte[] SignPictureBytes { get; set; }
        /// <summary>
        /// 特种设备操作证首页照片
        /// </summary>
        public byte[] CertificateBackPictureBytes { get; set; }
        /// <summary>
        /// 特种设备操作证有效期照片
        /// </summary>
        public byte[] CertificateFrontPictureBytes { get; set; }
        /// <summary>
        /// 到期日期
        /// </summary>
        public DateTime? ExpirationDate { get; set; }
        /// <summary>
        /// 审核状态
        /// </summary>
        public int CheckState { get; set; }
        /// <summary>
        /// 角色
        /// </summary>
        public string RoleCode { get; set; }
        /// <summary>
        /// 角色组
        /// </summary>
        public string RoleGroupCode { get; set; }
        /// <summary>
        /// 同步用户ID
        /// </summary>
        public int? SyncUserId { get; set; }

        public int? TenantId { get; set; }
    }
}
