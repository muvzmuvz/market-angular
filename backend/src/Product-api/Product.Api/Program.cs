using Hangfire;
using marketplace_api.Extensions;
using Microsoft.EntityFrameworkCore;
using Products.Api.BackgroundServices;
using Products.Api.Data;
using Products.Api.Interfaces;
using Products.Api.Service;
using Products.Api.Settings;
using Products_Api.Cron;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();
builder.Services.AddHttpClient();
builder.Services.AddHttpContextAccessor();

builder.Services.AddHostedService<RabbitMQConsumerService>();
builder.Services.AddSingleton<IRedisShopService, RedisShopService>();
builder.Services.AddScoped<UpdateShopCache>();

builder.Services.Configure<RabbitMQSettings>
    (builder.Configuration.GetSection(nameof(RabbitMQSettings)));

builder.AddData(builder.Configuration)
   .AddCors()
   .AddSwagger()
   .AddServices()
   .AddAuth()
   .AddMapping()
   .AddRedis(builder.Configuration)
   .AddSerilog()
   .AddHangFire(builder.Configuration);

var app = builder.Build();

app.UseCors("CorsPolicy");

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ProductDbContext>();
    try
    {
        dbContext.Database.Migrate();
        Console.WriteLine("запуск миграции");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"ошибка: {ex.Message}");
        throw;
    }
}

app.MapControllers();
app.UseMiddleware<Products.Api.Midleware.ExceptionMidleware>();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHangfireDashboard();

app.UseHttpsRedirection();

RecurringJob.AddOrUpdate<UpdateShopCache>(
    "update-shop-cache-hourly",
    x => x.GetDataFromApiAsync(),
    Cron.Hourly);


app.Run();
