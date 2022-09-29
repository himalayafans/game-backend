using GameBackend.Library.Data;
using GameBackend.Library.Extensions;
using Serilog;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
// Add services to the container.
builder.AddLogger();
builder.Services.AddControllers();
builder.AddJwtAuth();
builder.AddJwtSwagger();
builder.AddSiteServices();

var app = builder.Build();

#region 播种数据
if (args.Contains("seed"))
{
    ConfigurationManager configuration = builder.Configuration;
    string connString = configuration.GetConnectionString("Default");
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

app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
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
