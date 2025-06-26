using marketplace_api.Common.Persistence;
using marketplace_api.Extensions;
using marketplace_api.Middlewares;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddControllersWithViews();

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
