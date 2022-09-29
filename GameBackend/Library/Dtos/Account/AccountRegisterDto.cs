using System.ComponentModel.DataAnnotations;

namespace GameBackend.Library.Dtos.Account
{
    public class AccountRegisterDto
    {
        /// <summary>
        /// 用户名
        /// </summary>
        [Required(ErrorMessage = "用户名不能为空")]
        [StringLength(30, MinimumLength = 5, ErrorMessage = "用户名的长度需为 5 至30 个字符")]
        [RegularExpression(@"^[0-9a-zA-Z]+$", ErrorMessage = "用户名只允许包含字母或数字")]
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// 注册邮箱
        /// </summary>
        [Required(ErrorMessage = "Email不能为空")]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        /// <summary>
        /// 登录密码
        /// </summary>
        [Required(ErrorMessage = "密码不能为空")]
        [RegularExpression(@"^[0-9a-zA-Z_]+$", ErrorMessage = "密码只允许包含字母、数字或下划线")]
        public string Password { get; set; } = string.Empty;
    }
}