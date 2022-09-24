using Dapper;
using Npgsql;
using System.Data.Common;

namespace GameBackend.Library.Data
{
    /// <summary>
    /// 数据库
    /// </summary>
    public class Database
    {
        private DbFactory _dbFactory;

        public Database(DbFactory dbFactory)
        {
            _dbFactory = dbFactory ?? throw new ArgumentNullException(nameof(dbFactory));
        }

        private async Task CreateTables(DbConnection connection)
        {
            string sql = Properties.Resources.database;
            await connection.ExecuteAsync(sql);
        }
        /// <summary>
        /// 获取数据中表的总数
        /// </summary>
        private async Task<int> GetTableCount(DbConnection connection)
        {
            string sql = "SELECT count(*) FROM information_schema.tables where table_schema = 'public';";
            int count = await connection.ExecuteScalarAsync<int>(sql);
            return count;
        }
        private async Task CreateRows(UnitOfWork work)
        {
            Version version = new Version(1, 0, 0, 0);
            await work.Config.SetDbVersion(version);
            await work.Config.SetAppName();
        }
        /// <summary>
        /// 播种数据
        /// </summary>
        public async Task Seed()
        {
            using (var work = _dbFactory.StartWork())
            {
                await work.Connection.OpenAsync();
                int tableCount = await GetTableCount(work.Connection);
                if (tableCount == 0)
                {
                    var trans = work.Connection.BeginTransaction();
                    try
                    {
                        Console.WriteLine("start creating table...");
                        await CreateTables(work.Connection);
                        Console.WriteLine("start creating row...");
                        await CreateRows(work);
                        trans.Commit();
                        Console.WriteLine("Data seeding succeeded.");
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        Console.WriteLine("Error:" + ex.Message);
                    }
                }
                else
                {
                    Console.WriteLine("The database is not empty, skip the initialization process...");
                }
            }
        }
    }
}
