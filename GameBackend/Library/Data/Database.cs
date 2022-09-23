using Dapper;
using Npgsql;

namespace GameBackend.Library.Data
{
    /// <summary>
    /// 数据库
    /// </summary>
    public class Database
    {
        private string _connectionString = string.Empty;
        private NpgsqlConnection _connection;

        public Database(string connectionString)
        {
            _connectionString = connectionString;
            _connection = new NpgsqlConnection(connectionString);
        }

        private void CreateTables()
        {
            string sql = Properties.Resources.database;
            _connection.Execute(sql);
        }
        /// <summary>
        /// 获取数据中表的总数
        /// </summary>
        private int GetTableCount()
        {
            string sql = "SELECT count(*) FROM information_schema.tables where table_schema = 'public';";
            int count = _connection.ExecuteScalar<int>(sql);
            return count;
        }
        private void CreateRows()
        {

        }
        /// <summary>
        /// 播种数据
        /// </summary>
        public void Seed()
        {
            _connection.Open();
            int tableCount = GetTableCount();
            if (tableCount == 0)
            {
                var trans = _connection.BeginTransaction();
                try
                {
                    Console.WriteLine("start creating table...");
                    CreateTables();
                    Console.WriteLine("start creating row...");
                    CreateRows();
                    Console.WriteLine("Data seeding succeeded.");
                    trans.Commit();
                }
                catch (Exception)
                {
                    trans.Rollback();
                    throw;
                }               
            }
            else
            {
                Console.WriteLine("The database is not empty, skip the initialization process...");
            }           
        }
    }
}
