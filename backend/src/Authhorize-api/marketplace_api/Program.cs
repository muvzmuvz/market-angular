using marketplace_api.Common.Persistence;
using marketplace_api.Configuration;
using marketplace_api.Extensions;
using marketplace_api.Middlewares;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddControllersWithViews();

builder.Services.Configure<RabbitMqConfiguration>
    (builder.Configuration.GetSection(nameof(RabbitMqConfiguration)));


builder.AddData(builder.Configuration)
    .AddCors()
    .AddSwagger()
    .AddServices()
    .AddAuth()
    .AddMapping()
    .AddSerilog();

var app = builder.Build();


app.UseCors("CorsPolicy"); 

app.UseStaticFiles();

app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.UseIdentityServer();
app.UseAuthentication();  
app.UseAuthorization();

app.UseSwagger();
app.UseSwaggerUI();

app.UseMiddleware<DomainEventsMiddleware>();
app.UseMiddleware<ExceptionMiddlaware>();

app.MapControllers();

app.Run();
