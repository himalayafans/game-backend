using Npgsql;

namespace GameBackend.Library.Data
{
    public class DbFactory
    {
        private readonly IConfiguration Configuration;


        public DbFactory(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        /// <summary>
        /// 获取连接字符串
        /// </summary>
        public string GetConnectionString()
        {
            return Configuration.GetConnectionString("PostgreSQL");
        }
        /// <summary>
        /// 启动一个新的工作单元
        /// </summary>
        public UnitOfWork StartWork()
        {
            return new UnitOfWork(this);
        }
    }
}
