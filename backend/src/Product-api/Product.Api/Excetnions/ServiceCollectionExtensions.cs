using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Products.Api.Data;

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
       options.Audience = "web";

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
    return builder;
  }

  public static WebApplicationBuilder AddMapping(this WebApplicationBuilder builder)
  {
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
