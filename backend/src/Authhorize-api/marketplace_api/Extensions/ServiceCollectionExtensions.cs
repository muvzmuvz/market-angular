using marketplace_api.Common.interfaces;
using marketplace_api.Common.Persistence;
using marketplace_api.IdentityServer;
using marketplace_api.repositories;
using marketplace_api.services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using System;

namespace marketplace_api.Extensions;

public static class ServiceCollectionExtensions
{

  public static WebApplicationBuilder AddSwagger(this WebApplicationBuilder builder)
  {
    builder.Services.AddSwaggerGen(option =>
    {
      option.SwaggerDoc("v1", new OpenApiInfo
      {
        Title = "Orders Api",
        Version = "v1",
      });

      option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
      {
        In = ParameterLocation.Header,
        Description = "Please Enter a valid  token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
      });

      option.AddSecurityRequirement(new OpenApiSecurityRequirement
      {
      });
    });

    return builder;
  }

  public static WebApplicationBuilder AddAuth(this WebApplicationBuilder builder)
  {
    builder.Services.AddIdentityServer()
       .AddDeveloperSigningCredential()
       .AddInMemoryIdentityResources(Config.IdentityResources)
       .AddInMemoryApiScopes(Config.ApiScopes)
       .AddInMemoryClients(Config.Clients)
       .AddAspNetIdentity<IdentityUser<Guid>>()
       .AddProfileService<CustomProfileService>();

    builder.Services.AddAuthentication()
        .AddLocalApi("Bearer", option =>
        {
          option.ExpectedScope = "api";
        });

    builder.Services.AddAuthorization(options =>
    {
      options.AddPolicy("Bearer", policy =>
      {
        policy.AddAuthenticationSchemes("Bearer");
        policy.RequireAuthenticatedUser();
      });
    });

    return builder;
  }

  public static WebApplicationBuilder AddData(this WebApplicationBuilder builder, IConfiguration configuration)
  {
    builder.Services.AddDbContext<AuthorizeDbContext>(options =>
    {
      options.UseNpgsql(configuration.GetConnectionString("DbConfig"));
    });

    builder.Services.AddIdentity<IdentityUser<Guid>, IdentityRole<Guid>>()
      .AddEntityFrameworkStores<AuthorizeDbContext>()
      .AddDefaultTokenProviders();



    return builder;
  }

  public static WebApplicationBuilder AddServices(this WebApplicationBuilder builder)
  {
    builder.Services.AddScoped<IUserRepository, UserRepository>();
    builder.Services.AddScoped<IAuthService, AuthService>();
    builder.Services.AddScoped<IImageService, ImageService>();

    return builder;
  }
}
