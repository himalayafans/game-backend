using System.ComponentModel.DataAnnotations;

namespace GameBackend.Library.Dtos.Account
{
    /// <summary>
    /// 账号登录Request
    /// </summary>
    public class AccountLoginDto
    {
        /// <summary>
        /// 用户名或Email
        /// </summary>
        [Required(ErrorMessage = "该字段不能为空")]

        public string NameOrEmail { get; set; } = string.Empty;
        /// <summary>
        /// 登录密码
        /// </summary>
        [Required(ErrorMessage = "密码不能为空")]
        public string Password { get; set; } = string.Empty;
    }
}