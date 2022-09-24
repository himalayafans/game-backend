using Dapper;
using Npgsql;

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

    }
}
