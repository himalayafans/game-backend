using GameBackend.Library.Data;

namespace GameBackend.Library.Extensions
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 增加数据库服务
        /// </summary>
        public static void AddDatabase(this IServiceCollection services)
        {
            services.AddScoped<DbFactory, DbFactory>();
            services.AddScoped<Database, Database>();
        }
    }
}
