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

        public AccountService(DbFactory dbFactory, EncryptionService encryptionService)
        {
            _dbFactory = dbFactory;
            _encryptionService = encryptionService;
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
    }
}
