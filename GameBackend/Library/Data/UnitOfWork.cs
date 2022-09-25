using GameBackend.Library.Repositories;
using Npgsql;
using System.Data.Common;

namespace GameBackend.Library.Data
{
    public class UnitOfWork : IDisposable
    {
        public NpgsqlConnection Connection { get; }
        /// <summary>
        /// 配置选项
        /// </summary>
        public ConfigRepository Config { get; }
        /// <summary>
        /// 账号
        /// </summary>
        public AccountRepository Account { get; set; }

        public UnitOfWork(DbFactory factory)
        {
            string t = factory.GetConnectionString();
            this.Connection = new NpgsqlConnection(t);
            this.Config = new ConfigRepository(this.Connection);
            Account = new AccountRepository(this.Connection);
        }
        /// <summary>
        /// 开启事务
        /// </summary>
        public DbTransaction BeginTransaction()
        {
            return this.Connection.BeginTransaction();
        }
        /// <summary>
        /// 打开连接
        /// </summary>
        /// <returns></returns>
        public async Task OpenAsync()
        {
            await this.Connection.OpenAsync();
        }

        public void Dispose()
        {
            this.Connection.Dispose();
        }
    }
}