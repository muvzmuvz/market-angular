using marketplace_api.Common.interfaces;
using marketplace_api.Common.Persistence;
using marketplace_api.IdentityServer;
using marketplace_api.MappingProfile;
using marketplace_api.Models;
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
    builder.Services.AddIdentityServer(options =>
    {
      options.UserInteraction.LoginUrl = "/Authorize/Login";

      options.UserInteraction.LogoutUrl = "/Authorize/Logout";
    })
       .AddDeveloperSigningCredential()
       .AddInMemoryIdentityResources(Config.IdentityResources)
       .AddInMemoryApiScopes(Config.ApiScopes)
       .AddInMemoryClients(Config.Clients)
       .AddAspNetIdentity<UserIdentity>()
       .AddProfileService<CustomProfileService>();

    builder.Services.ConfigureApplicationCookie(config =>
    {
      config.LoginPath = "/Authorize/Login";
      config.LogoutPath = "/Authorize/Logout";
    });

    builder.Services
      .Configure<IdentityServerSettings>(builder.Configuration.GetSection(nameof(IdentityServerSettings)));

    builder.Services.AddAuthentication("Bearer")
     .AddJwtBearer("Bearer", options =>
     {
       options.Authority = "http://localhost:5042"; 
       options.Audience = "web";

       if (builder.Environment.IsDevelopment())
       {
         options.TokenValidationParameters.ValidateIssuer = false;
         options.RequireHttpsMetadata = false;
       }
     });

    builder.Services.AddAuthorization(options =>
    {
      options.AddPolicy("RequireAdminRole", policy =>
          policy.RequireRole("Admin"));

      options.AddPolicy("ApiScope", policy =>
      {
        policy.RequireAuthenticatedUser();
        policy.RequireClaim("scope", "api"); 
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

    builder.Services.AddIdentity<UserIdentity, IdentityRole<Guid>>()
      .AddEntityFrameworkStores<AuthorizeDbContext>()
      .AddDefaultTokenProviders();

    return builder;
  }

  public static WebApplicationBuilder AddServices(this WebApplicationBuilder builder)
  {
    builder.Services.AddScoped<IUserRepository, UserRepository>();
    builder.Services.AddScoped<IShopRepository, ShopRepository>();
    builder.Services.AddScoped<IShopService, ShopService>();
    builder.Services.AddScoped<IAuthService, AuthService>();
    builder.Services.AddScoped<IImageService, ImageService>();
    builder.Services.AddScoped<IAccountService, AccountService>();
    builder.Services.AddScoped<ISiteInitializerService, SiteInitializerService>();
    builder.Services.AddScoped<IUnitOfWork>(
      serviceProvider => serviceProvider.GetRequiredService<AuthorizeDbContext>());

    return builder;
  }

  public static WebApplicationBuilder AddMapping(this WebApplicationBuilder builder)
  {
    builder.Services.AddAutoMapper(
        typeof(UserProfile)
      , typeof(ShopProfile));

    return builder; 
  }

  public static WebApplicationBuilder AddCors(this WebApplicationBuilder builder)
  {
    builder.Services.AddCors(options => options
    .AddPolicy("CorsPolicy", builder =>
    {
      builder.WithOrigins("http://127.0.0.1:4200", "http://localhost:4200")
          .AllowAnyMethod()
          .AllowAnyHeader()
          .AllowCredentials();
    }));

    return builder;
  }
}
