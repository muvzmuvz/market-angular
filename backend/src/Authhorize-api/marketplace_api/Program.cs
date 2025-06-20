using marketplace_api.Common.Persistence;
using marketplace_api.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddControllersWithViews();


builder.Services.AddCors(options => options
    .AddPolicy("CorsPolicy", builder =>
    {
      builder.WithOrigins("http://127.0.0.1:4200", "http://localhost:4200")
          .AllowAnyMethod() 
          .AllowAnyHeader()
          .AllowCredentials();
    }));

builder.AddData(builder.Configuration)
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

app.MapControllers();

app.Run();
