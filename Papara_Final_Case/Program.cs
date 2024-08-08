using Autofac;
using Autofac.Core;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Papara.Business.Cqrs.CqrsCommand;
using Papara.Business.Mapper;
using Papara.Data.DatabaseContext;
using Papara.Data.UnitOfWork;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

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
