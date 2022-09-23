using GameBackend.Library.Data;

namespace GameBackend.Library.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddDatabase(this IServiceCollection services)
        {
            services.AddTransient<DbFactory, DbFactory>();
        }
    }
}
