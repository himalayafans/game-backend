using GameBackend.Library.Data;
using GameBackend.Library.Services;
using System.Reflection;

namespace GameBackend.Library.Extensions
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 增加Swagger接口文档服务
        /// </summary>
        [Obsolete("该方法已作废，请使用AddJwtSwagger()代替")]
        public static void AddSwaggerDoc(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Version = "v1",
                    Title = "Game API",
                    Description = ""
                });
                // 设置Swagger文档
                string xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                string xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                s.IncludeXmlComments(xmlPath);
            });
        }
    }
}