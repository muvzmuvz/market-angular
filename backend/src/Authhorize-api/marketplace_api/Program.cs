using marketplace_api.Common.Persistence;
using marketplace_api.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddControllersWithViews();


builder.AddData(builder.Configuration)
    .AddSwagger()
    .AddServices()
    .AddAuth()
    .AddMapping();

var app = builder.Build();

app.UseEndpoints(endpoints =>
{
  endpoints.MapDefaultControllerRoute();

  endpoints.MapControllers(); 
});

app.UseAuthorization();


app.UseIdentityServer();
app.UseAuthentication();
app.UseAuthorization();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();
