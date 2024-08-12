using Autofac;
using Autofac.Core;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Papara.Api.Middlewares;
using Papara.Business.Command;
using Papara.Business.Cqrs.CqrsCommand;
using Papara.Business.Mapper;
using Papara.Business.Services;
using Papara.Business.Validation;
using Papara.Data.DatabaseContext;
using Papara.Data.UnitOfWork;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// JWT Ayarları
var secret = builder.Configuration["JwtSettings:SecretKey"];
builder.Services.AddSingleton(new TokenService(secret));
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = "alperenPapara.com",
        ValidAudience = "alperenPapara.com",
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret))
    };
});
builder.Services.AddDistributedMemoryCache(); 
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); 
    options.Cookie.HttpOnly = true; 
    options.Cookie.IsEssential = true; 
});


builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        // JSON döngü algılamayı engellemek için ReferenceLoopHandling ayarı yapılıyor
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
    })
    .AddFluentValidation(x =>
    {
        // FluentValidation kullanımı için validatorları kaydediyoruz
        x.RegisterValidatorsFromAssemblyContaining<BaseValidator>();
    });

builder.Services.AddDbContext<Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MsSQLConnection")), ServiceLifetime.Scoped);
builder.Services.AddEndpointsApiExplorer();

var config = new MapperConfiguration(cfg =>
{
    cfg.AddProfile(new MapperConfig());
});
builder.Services.AddSingleton(config.CreateMapper());

// Add MediatR - Ensure all handlers are registered
builder.Services.AddMediatR(typeof(CreateCategoryCommand).GetTypeInfo().Assembly);
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();


builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Papara API", Version = "v1" });

    // JWT için yetkilendirme desteği ekleyin
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header,
            },
            new List<string>()
        }
    });
});



var app = builder.Build();

app.UseSession();

app.UseMiddleware<ErrorHandlerMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
