using Dapper.Contrib.Extensions;
using GameBackend.Library.Data;
using GameBackend.Library.Dtos.Account;
using GameBackend.Library.Exceptions;

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
                    throw new ModelException(nameof(account.Name), "该账号已存在");
                }
                if (await work.Account.IsExistEmail(account.Email))
                {
                    throw new ModelException(nameof(account.Email), "该邮箱已被注册");
                }
                string hash = _encryptionService.PasswordHash(account.Password.Trim());
                await work.Account.Insert(account.Name, account.Email, hash);
            }
        }
        /// <summary>
        /// 登录账号
        /// </summary>
        public async Task Login(AccountLoginDto account)
        {
            using (var work = _dbFactory.StartWork())
            {
                await work.Connection.OpenAsync();
                var result = await work.Account.GetFromNameOrEmail(account.Name);
                var error = "账号或密码错误,请重新输入";
                if(result == null)
                {
                    throw new SiteException(error);
                }
                if(_encryptionService.PasswordHash(account.Password) != result.password)
                {
                    throw new SiteException(error);
                }

            }
        }
    }
}
