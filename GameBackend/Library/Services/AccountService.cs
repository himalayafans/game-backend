using Dapper.Contrib.Extensions;
using GameBackend.Library.Data;
using GameBackend.Library.Dtos.Account;
using GameBackend.Library.Entities;
using GameBackend.Library.Exceptions;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Net;

namespace GameBackend.Library.Services
{
    /// <summary>
    /// 账号服务
    /// </summary>
    public class AccountService
    {
        private DbFactory _dbFactory;
        private EncryptionService _encryptionService;
        private IConfiguration _configuration;

        public AccountService(DbFactory dbFactory, EncryptionService encryptionService, IConfiguration configuration)
        {
            _dbFactory = dbFactory;
            _encryptionService = encryptionService;
            _configuration = configuration;
        }

        /// <summary>
        /// 注册账号
        /// </summary>
        public async Task Register(AccountRegisterDto account)
        {
            using (var work = _dbFactory.StartWork())
            {
                await work.Connection.OpenAsync();
                if (await work.Account.IsExistName(account.Name))
                {
                    throw new ModelException(nameof(account.Name), "该账号已存在", HttpStatusCode.BadRequest);
                }
                if (await work.Account.IsExistEmail(account.Email))
                {
                    throw new ModelException(nameof(account.Email), "该邮箱已被注册", HttpStatusCode.BadRequest);
                }
                string hash = _encryptionService.PasswordHash(account.Password.Trim());
                await work.Account.Insert(account.Name, account.Email, hash);
            }
        }
        /// <summary>
        /// 登录账号
        /// </summary>
        public async Task<AccountLoginResultDto> Login(AccountLoginDto request)
        {
            using (var work = _dbFactory.StartWork())
            {
                await work.Connection.OpenAsync();
                var account = await work.Account.GetFromNameOrEmail(request.NameOrEmail);
                var error = "账号或密码错误,请重新输入";
                if (account == null)
                {
                    throw new HttpException(HttpStatusCode.Unauthorized, error);
                }
                var hash = _encryptionService.PasswordHash(request.Password);
                if (hash != account.password)
                {
                    throw new HttpException(HttpStatusCode.Unauthorized, error);
                }
                var token = BuildToken(account);
                return new AccountLoginResultDto() { Id = account.id, Name = account.name, Token = token, Avatar = account.avatar };
            }
        }
        /// <summary>
        /// 创建Jwt Token
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        private string BuildToken(Account account)
        {
            var issuer = _configuration["Jwt:Issuer"];
            var audience = _configuration["Jwt:Audience"];
            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, account.name),
                //new Claim(ClaimTypes.Role, ""),
                new Claim(ClaimTypes.NameIdentifier, account.id.ToString()), // 用户的ID， 参考资料：https://stackoverflow.com/a/11147240
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()) // 每个token的唯一标识
             };
            var securityKey = new SymmetricSecurityKey(key);
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            var tokenDescriptor = new JwtSecurityToken(issuer, audience, claims, expires: DateTime.Now.AddDays(7), signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }
    }
}