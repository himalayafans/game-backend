using GameBackend.Library.Data;
using GameBackend.Library.Extensions;
using Serilog;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.AddLogger();
builder.Services.AddControllers();
builder.AddJwtAuth();
//builder.Services.AddSwaggerDoc();
builder.AddJwtSwagger();
builder.Services.AddSiteServices();

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
