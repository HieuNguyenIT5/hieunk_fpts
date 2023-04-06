using Microsoft.EntityFrameworkCore;
using Order.App.Extensions;
using Order.Infrastructure;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
builder.Services.AddDbContext<DbContextModel>(options =>
{
    options.UseOracle(builder.Configuration.GetConnectionString("OraDbConnection"), options =>
    {
        options.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
    });
});
builder.Services.AddConfiguration();
var app = builder.Build();
app.Run();
