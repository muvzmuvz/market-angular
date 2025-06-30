using marketplace_api.Extensions;
using Microsoft.EntityFrameworkCore;
using Products.Api.Data;
using Products.Api.Settings;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();

builder.Services.Configure<RabbitMQSettings>
    (builder.Configuration.GetSection(nameof(RabbitMQSettings)));

builder.Services.AddStackExchangeRedisCache(options =>
{
  options.Configuration = "localhost";
  options.InstanceName = "localhost";
});

builder.AddData(builder.Configuration)
   .AddCors()
   .AddSwagger()
   .AddServices()
   .AddAuth()
   .AddMapping();

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

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.Run();
