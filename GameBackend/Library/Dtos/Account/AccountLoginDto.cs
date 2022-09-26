using System.ComponentModel.DataAnnotations;

namespace GameBackend.Library.Dtos.Account
{
    public class AccountLoginDto
    {
        /// <summary>
        /// 登录账号
        /// </summary>
        [Required(ErrorMessage = "登录账号不能为空")]

        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// 登录密码
        /// </summary>
        [Required(ErrorMessage = "密码不能为空")]
        public string Password { get; set; } = string.Empty;
    }
}