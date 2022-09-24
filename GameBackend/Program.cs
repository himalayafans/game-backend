using GameBackend.Library.Data;
using GameBackend.Library.Extensions;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(s =>
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
builder.Services.AddDatabase();

var app = builder.Build();

#region 播种数据
if (args.Contains("seed"))
{
    ConfigurationManager configuration = builder.Configuration;
    string connString = configuration.GetConnectionString("PostgreSQL");
    if (String.IsNullOrEmpty(connString))
    {
        Console.WriteLine("Error:ConnectionString in appsettings.json file cannot be empty!");
    }
    else
    {
        using (var serviceScope = app.Services.CreateScope())
        {
            var services = serviceScope.ServiceProvider;
            var database = services.GetRequiredService<Database>();
            Task task = database.Seed();
            task.Wait();
        }
    }
    return; // 退出应用程序
}
#endregion


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
