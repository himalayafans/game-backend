using Dapper;
using Dapper.Contrib.Extensions;
using GameBackend.Library.Common;
using GameBackend.Library.Entities;
using GameBackend.Library.Exceptions;
using Npgsql;
using System.Data.Common;

namespace GameBackend.Library.Repositories
{
    /// <summary>
    /// 用户账号存储库
    /// </summary>
    public class AccountRepository : BaseRepository
    {
        public AccountRepository(NpgsqlConnection connection) : base(connection)
        {
        }
        /// <summary>
        /// 检查是否存在该登录账号
        /// </summary>
        public async Task<bool> IsExistName(string name)
        {
            string sql = "select count(*) from account where lower(name)=@Name;";
            var count = await Connection.ExecuteScalarAsync<int>(sql, new { Name = name.Trim().ToLower() });
            return count > 0;
        }
        /// <summary>
        /// 检查是否存在Email
        /// </summary>
        public async Task<bool> IsExistEmail(string email)
        {
            string sql = "select count(*) from account where lower(email)=@Email;";
            var count = await Connection.ExecuteScalarAsync<int>(sql, new { Email = email.Trim().ToLower() });
            return count > 0;
        }
        /// <summary>
        /// 插入数据
        /// </summary>
        public async Task<Account> Insert(string name, string email, string passwordHash, string role = RoleNames.User, DbTransaction? transaction = null)
        {
            var now = DateTime.Now;
            var account = new Account()
            {
                id = Guid.NewGuid(),
                name = name.Trim().ToLower(),
                email = email.Trim().ToLower(),
                password = passwordHash,
                avatar = "",
                is_active_email = false,
                last_updated = now,
                status = Enums.AccountStatus.Enable,
                create_time = now,
                role = role
            };
            await this.Connection.InsertAsync(account, transaction);
            return account;
        }
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="id">用户ID</param>
        /// <param name="newPasswordHash">新的密码哈希值</param>
        public async Task ModifyPassword(Guid id, string newPasswordHash)
        {
            string sql = "update account set password=@Password,last_updated=@LastUpdated where id=@Id;";
            await this.Connection.ExecuteAsync(sql, new { Id = id.ToString(), Password = newPasswordHash, LastUpdated = DateTime.Now });
        }
        /// <summary>
        /// 修改头像
        /// </summary>
        /// <param name="id">用户ID</param>
        /// <param name="avatar">新的头像URL</param>
        public async Task<Account> ModifyAvatar(Guid id, string avatar)
        {
            string sql = "update account set avatar=@Avatar where id=@Id;";
            await this.Connection.ExecuteAsync(sql, new { Id = id.ToString(), Avatar = avatar.Trim() });
            return await this.Connection.GetAsync<Account>(id);
        }
        /// <summary>
        /// 通过登录账号或邮箱获取账号
        /// </summary>
        public async Task<Account?> GetFromNameOrEmail(string nameOrEmail)
        {
            nameOrEmail = nameOrEmail.Trim().ToLower();
            string sql = "select * from account where lower(name)=@Name or lower(email)=@Email;";
            var account = await this.Connection.QueryFirstOrDefaultAsync<Account?>(sql, new { Name = nameOrEmail, Email = nameOrEmail });
            return account;
        }
        /// <summary>
        /// 修改用户角色
        /// </summary>
        public async Task<Account> ModifyRole(Guid id, string role)
        {
            string sql = "update account set role=@Role where id=@Id;";
            await this.Connection.ExecuteAsync(sql, new { Id = id, Role = role });
            return await this.Connection.GetAsync<Account>(id);
        }
    }
}
