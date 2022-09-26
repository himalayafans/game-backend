using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Text;

namespace GameBackend.Library.Extensions
{
    public static class BuilderExtensions
    {
        /// <summary>
        /// 增加JWT Token认证服务
        /// </summary>
        public static void AddJwtAuth(this WebApplicationBuilder builder)
        {
            builder.Services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = true
                };
            });
            builder.Services.AddAuthorization();
        }
        public static void AddJwtSwagger(this WebApplicationBuilder builder)
        {
            // 代码来源： https://dev.to/moe23/net-6-minimal-api-authentication-jwt-with-swagger-and-open-api-2chh
            var securityScheme = new OpenApiSecurityScheme()
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "JSON Web Token based security",
            };
            var securityReq = new OpenApiSecurityRequirement()
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] {}
                }
            };
            //var contact = new OpenApiContact()
            //{
            //    Name = "Mohamad Lawand",
            //    Email = "hello@mohamadlawand.com",
            //    Url = new Uri("http://www.mohamadlawand.com")
            //};
            //var license = new OpenApiLicense()
            //{
            //    Name = "Free License",
            //    Url = new Uri("http://www.mohamadlawand.com")
            //};

            //var info = new OpenApiInfo()
            //{
            //    Version = "v1",
            //    Title = "Minimal API - JWT Authentication with Swagger demo",
            //    Description = "Implementing JWT Authentication in Minimal API",
            //    TermsOfService = new Uri("http://www.example.com"),
            //    Contact = contact,
            //    License = license
            //};

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(o =>
            {
                //o.SwaggerDoc("v1", info);
                o.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Version = "v1",
                    Title = "Game API",
                    Description = ""
                });
                o.AddSecurityDefinition("Bearer", securityScheme);
                o.AddSecurityRequirement(securityReq);
            });
        }
        /// <summary>
        /// 增加日志服务
        /// </summary>
        public static void AddLogger(this WebApplicationBuilder builder)
        {
            var logger = new LoggerConfiguration().ReadFrom.Configuration(builder.Configuration).Enrich.FromLogContext().CreateLogger();
            builder.Logging.ClearProviders();
            builder.Logging.AddSerilog(logger);
        }
    }
}
