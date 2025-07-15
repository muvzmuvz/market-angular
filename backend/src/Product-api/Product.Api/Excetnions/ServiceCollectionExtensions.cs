using FluentValidation;
using Hangfire;
using Hangfire.PostgreSql;
using marketplace_api.services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Products.Api.Data;
using Products.Api.Interfaces;
using Products.Api.MappingProfile;
using Products.Api.Models;
using Products.Api.Repository;
using Products.Api.Service;
using Products_Api.BackgroundServices.Service;
using Products_Api.Cron;
using Products_Api.Interfaces;
using Products_Api.MappingProfile;
using Products_Api.Repository;
using Products_Api.Service;
using Products_Api.Validation;
using Serilog;

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
    builder.Services.AddAuthentication();
    builder.Services.AddAuthorization();

    builder.Services.AddAuthentication("Bearer")
     .AddJwtBearer("Bearer", options =>
     {
       options.Authority = "http://localhost:5042";
       options.Audience = "api";

       if (builder.Environment.IsDevelopment())
       {
         options.TokenValidationParameters.ValidateIssuer = false;
         options.RequireHttpsMetadata = false;
       }
     });

    return builder;
  }

  public static WebApplicationBuilder AddData(this WebApplicationBuilder builder, IConfiguration configuration)
  {
    builder.Services.AddDbContext<ProductDbContext>(options =>
    {
      options.UseNpgsql(configuration.GetConnectionString("DbConfig"));
    });

    return builder;
  }

  public static WebApplicationBuilder AddServices(this WebApplicationBuilder builder)
  {
    builder.Services.AddScoped<IProductRepository, ProductRepository>();
    builder.Services.AddScoped<IProductService, ProductService>();
    builder.Services.AddScoped<IImageService, ImageService>();
    builder.Services.AddScoped<IUnitOfWork>(
      serviceProvider => serviceProvider.GetRequiredService<ProductDbContext>());
    builder.Services.AddScoped<ICartRepository, CartRepository>();
    builder.Services.AddScoped<ICartService, CartService>();
    builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
    builder.Services.AddScoped<IProductFavourityRepository, ProductFavourityRepository>();
    builder.Services.AddScoped<IProductFavourityService, ProductFavourityService>();
    builder.Services.AddScoped<IProductHistoryRepository, ProductHistoryRepository>();
    builder.Services.AddScoped<IProductHistoryService, ProductHistoryService>();

    builder.Services.AddHostedService<RabbitMQConsumerServiceUser>();

    builder.Services.AddHostedService<RabbitMQConsumerServiceShop>();
    builder.Services.AddSingleton<IRedisShopService, RedisShopService>();
    builder.Services.AddScoped<UpdateShopCache>();

    return builder;
  }

  public static WebApplicationBuilder AddMapping(this WebApplicationBuilder builder)
  {
    builder.Services.AddAutoMapper(
       typeof(ProductProfile),
       typeof(ProductHistoryProfile),
       typeof(ProductFavourityProfile));

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

  public static WebApplicationBuilder AddRedis(this WebApplicationBuilder builder, IConfiguration configuration)
  {
    builder.Services.AddStackExchangeRedisCache(options =>
    {
      options.Configuration = configuration.GetConnectionString("Redis");
      options.InstanceName = "";
    });

    return builder;
  }

  public static WebApplicationBuilder AddSerilog(this WebApplicationBuilder builder)
  {
    builder.Host.UseSerilog((context, services, configuration) => configuration
    .ReadFrom.Configuration(context.Configuration)
    .ReadFrom.Services(services)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day));

    return builder;
  }

  public static WebApplicationBuilder AddHangFire(this WebApplicationBuilder builder, IConfiguration configuration) 
  {
    builder.Services.AddHangfire(config =>
    config.UsePostgreSqlStorage(configuration.GetConnectionString("DbConfig")));

    builder.Services.AddHangfireServer();

    return builder;
  }

  public static WebApplicationBuilder AddMediator(this WebApplicationBuilder builder)
  {
    builder.Services.AddMediatR(typeof(Program));
    builder.Services.AddValidatorsFromAssemblyContaining<Program>();

    builder.Services.AddTransient(
        typeof(IPipelineBehavior<,>),
        typeof(ValidateBehavior<,>)
    );

    return builder;
  }
}
