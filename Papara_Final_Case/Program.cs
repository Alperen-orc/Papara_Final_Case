using Autofac;
using Autofac.Core;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Papara.Api.Middlewares;
using Papara.Business.Cqrs.CqrsCommand;
using Papara.Business.Mapper;
using Papara.Business.Validation;
using Papara.Data.DatabaseContext;
using Papara.Data.UnitOfWork;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddFluentValidation(x =>
{
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


builder.Services.AddSwaggerGen();



var app = builder.Build();

app.UseMiddleware<ErrorHandlerMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
