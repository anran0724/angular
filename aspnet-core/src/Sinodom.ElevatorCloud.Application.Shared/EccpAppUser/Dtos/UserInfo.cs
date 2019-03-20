 
namespace Sinodom.ElevatorCloud.EccpAppUser.Dtos
{
    /// <summary>
    /// 用户详细表
    /// </summary>
    public class UserInfo 
    {
        /// <summary>
        /// 用户主键
        /// </summary>
		public long UserId { get; set; }
        /// <summary>
        /// 电梯云用户主键
        /// </summary>
        public int? SyncUserId { get; set; }


    }
}