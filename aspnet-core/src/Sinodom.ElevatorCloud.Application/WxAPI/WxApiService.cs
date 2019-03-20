
using Abp.Domain.Repositories;
using QRCoder;
using Sinodom.ElevatorCloud.MultiTenancy.UserExtensions;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Abp.Domain.Uow;
using Sinodom.ElevatorCloud.Authorization.Users;

namespace Sinodom.ElevatorCloud.WxAPI
{
    public class WxApiService : ElevatorCloudAppServiceBase, IWxApiService
    {
        private readonly IRepository<EccpCompanyUserExtension, int> _eccpCompanyUserExtensionRepository;

        private readonly IRepository<User, long> _userRepository;

        public WxApiService(IRepository<EccpCompanyUserExtension, int> eccpCompanyUserExtensionRepository, IRepository<User, long> userRepository)
        {
            this._eccpCompanyUserExtensionRepository = eccpCompanyUserExtensionRepository;
            this._userRepository = userRepository;
        }
        public string GetTenantsByMobile(string mobile)
        {
            using (this.UnitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant))
            {
                var userExtension = _eccpCompanyUserExtensionRepository.GetAll().Where(e => e.Mobile == mobile);               

                var userTenantsList = from u in userExtension
                                      join o1 in this._userRepository.GetAll() on u.UserId equals
                                     o1.Id into j1
                                      from s1 in j1.DefaultIfEmpty()
                                      join o2 in TenantManager.Tenants on s1.TenantId equals
                                      o2.Id into j2
                                      from s2 in j2.DefaultIfEmpty()
                                      select new
                                      {
                                          UserId = s1 == null ? 0 : s1.Id,
                                          TenantId = s1 == null?0: s1.TenantId,
                                          TenantName = s2==null?string.Empty: s2.Name
                                      };
                var list = userTenantsList.ToList();

                return JsonConvert.SerializeObject(list);
            }
        }

        /// <summary>
        /// 获取工作用户2维码
        /// </summary>
        /// <param name="key"></param>
        /// <param name="userId"></param>
        /// <param name="workId"></param>
        /// <param name="longitude">经度</param>
        /// <param name="latitude">纬度</param>
        /// <returns></returns>
        public string GetWorkQrCode(string key, int userId, int workId, string longitude, string latitude)
        {
            Config.Width = 614;
            Config.Height = 874;
            //Config.Width2 = 543;
            //Config.Height2 = 543;
            //Config.TotalCount = 1;
            Config.Level = QRCodeGenerator.ECCLevel.Q;
            Config.ImageType = "png";
            Config.BottomSize = 48;
            string dir = DateTime.Now.Ticks.ToString();
            string file = string.Empty;
            int width = Config.Width;
            int height = Config.Height;
            //int width2 = Config.Width2;
            // int height2 = Config.Height2;
            //string bottom = Config.Bottom;
            //Font fontCompany = new Font("微软雅黑", 70);
            //Font fontCompanyCode = new Font("华文宋体", 120);
            using (Bitmap bmpPNG = new Bitmap(width, height))
            {
                Byte[] byteArray;
                using (Graphics g = Graphics.FromImage(bmpPNG))
                {
                    QrCodeContent qrCodeContent = new QrCodeContent();
                    qrCodeContent.key = key;
                    qrCodeContent.userId = userId;
                    qrCodeContent.workId = workId;
                    qrCodeContent.longitude = longitude;
                    qrCodeContent.latitude = latitude;

                    string content = "1<=>"+JsonConvert.SerializeObject(qrCodeContent);
                    g.Clear(Color.White);
                    var qrGenerator = new QRCodeGenerator();
                    var qrCodeData = qrGenerator.CreateQrCode(content, Config.Level);
                    var qrCode = new QRCode(qrCodeData);
                    Bitmap bmpCode = qrCode.GetGraphic(20);
                    //code.QRCodeErrorCorrect = Config.Level;
                    //code.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
                    //code.QRCodeScale = 4;
                    //code.QRCodeVersion = 8;                  
                    //Bitmap bmpCode = code.Encode(content);
                    MemoryStream ms = new MemoryStream();
                    bmpCode.Save(ms, ImageFormat.Png);
                    byteArray = ms.ToArray();
                    ms.Close();
                }
                return Convert.ToBase64String(byteArray);
            }
        }

        public class QrCodeContent
        {
           public string key { get; set; }
            public int userId { get; set; }
            public int workId { get; set; }
            public string longitude { get; set; }
            public string latitude { get; set; }

            public DateTime createTime { get {
                    return DateTime.Now;
                } }
        }

        public class Config
        {
            public static int Width { get; set; }
            public static int Height { get; set; }
            public static int Width2 { get; set; }
            public static int Height2 { get; set; }

            public static int TotalCount { get; set; }

            public static QRCodeGenerator.ECCLevel Level { get; set; }

            public static string ImageType { get; set; }

            public static string Bottom { get; set; }

            public static float BottomSize { get; set; }
        }
    }
}
