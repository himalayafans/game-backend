using GameBackend.Library.Data;
using GameBackend.Library.Services;

namespace GameBackend.Library.Extensions
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 增加站点相关服务
        /// </summary>
        public static void AddSiteServices(this IServiceCollection services)
        {
            services.AddScoped<DbFactory, DbFactory>();
            services.AddScoped<Database, Database>();
            services.AddScoped<EncryptionService, EncryptionService>();
        }
    }
}