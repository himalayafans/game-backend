namespace GameBackend.Library.Dtos.Account
{
    /// <summary>
    /// 账号登录Response对象
    /// </summary>
    public class AccountLoginResultDto
    {
        /// <summary>
        /// 账号ID
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string Name { get; set; } = "";
        /// <summary>
        /// Token
        /// </summary>
        public string Token { get; set; } = "";
    }
}
