using Dapper.Contrib.Extensions;
using GameBackend.Library.Enums;

namespace GameBackend.Library.Entities
{
    /// <summary>
    /// 账户
    /// </summary>
    [Table("account")]
    public class Account
    {
        [ExplicitKey]
        public Guid id { get; set; }
        /// <summary>
        /// 登录账号名
        /// </summary>
        public string name { get; set; } = string.Empty;
        /// <summary>
        /// Email
        /// </summary>
        public string email { get; set; } = string.Empty ;
        /// <summary>
        /// 登录密码哈希值
        /// </summary>
        public string password { get; set; } = string.Empty ;
        /// <summary>
        /// 头像
        /// </summary>
        public string avatar { get; set; } = string.Empty ;
        /// <summary>
        /// 邮箱是否激活
        /// </summary>
        public bool is_active_email { get; set; } = false;
        /// <summary>
        /// 最近更新时间
        /// </summary>
        public DateTime last_updated { get; set; } = DateTime.Now;
        /// <summary>
        /// 账户状态
        /// </summary>
        public AccountStatus status { get; set; } = AccountStatus.Enable;
        /// <summary>
        /// 注册时间
        /// </summary>
        public DateTime create_time { get; set; } = DateTime.Now;
    }
}
