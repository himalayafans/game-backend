using Dapper;
using Dapper.Contrib.Extensions;
using GameBackend.Library.Core;
using GameBackend.Library.Data;
using GameBackend.Library.Entities;
using Npgsql;
using System.Data.Common;

namespace GameBackend.Library.Repositories
{
    /// <summary>
    /// 配置信息存储库
    /// </summary>
    public class ConfigRepository : BaseRepository
    {
        private const string DbVersionKey = "DbVersion";
        private const string AppNameKey = "AppName";

        public ConfigRepository(NpgsqlConnection connection) : base(connection)
        {
        }
        /// <summary>
        /// 通过配置项的名称获取配置
        /// </summary>
        public async Task<Config> GetByName(string name)
        {
            string sql = $"select * from config where name=@Name;";
            Config config = await this.Connection.QueryFirstOrDefaultAsync<Config>(sql, new { Name = name.Trim() });
            return config;
        }
        /// <summary>
        /// 保存配置项，若存在则更新，若不存在，则插入
        /// </summary>
        private async Task Save(string name, string value, DbTransaction? transaction = null)
        {
            // 查询是否存在该记录，若没有则插入，若存在则更新
            Config config = await this.GetByName(name);
            if (config == null)
            {
                config = new Config()
                {
                    id = Guid.NewGuid(),
                    name = name.Trim(),
                    value = value.Trim()
                };
                await this.Connection.InsertAsync(config, transaction);
            }
            else
            {
                config.value = value.Trim();
                await this.Connection.UpdateAsync(config, transaction);
            }
        }

        /// <summary>
        /// 获取数据库版本
        /// </summary>
        public async Task<Version?> GetDbVersion()
        {
            Config config = await this.GetByName(DbVersionKey);
            if (config == null)
            {
                return null;
            }
            else
            {
                return new Version(config.value);
            }
        }
        /// <summary>
        /// 设置数据库版本
        /// </summary>
        public async Task SetDbVersion(Version version, DbTransaction? transaction = null)
        {
            await this.Save(DbVersionKey, version.ToString(), transaction);
        }
        /// <summary>
        /// 获取应用名称，用于检查该数据库是否属于本应用
        /// </summary>
        public async Task<string?> GetAppName()
        {
            Config config = await this.GetByName(AppNameKey);
            if (config == null)
            {
                return null;
            }
            else
            {
                return config.value;
            }
        }
        /// <summary>
        /// 设置应用程序标识，一般只在初始化数据库调用一次
        /// </summary>
        /// <returns></returns>
        public async Task SetAppName(DbTransaction? transaction = null)
        {
            await this.Save(AppNameKey, Constants.AppName, transaction);
        }
    }
}


