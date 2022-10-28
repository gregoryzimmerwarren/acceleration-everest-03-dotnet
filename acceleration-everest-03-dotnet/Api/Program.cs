using AppServices.DependencyInjections;
using DomainServices.DependencyInjections;
using FluentValidation.AspNetCore;
using Infrastructure.Data.DependencyInjections;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddAppServicesConfiguration();
builder.Services.AddDomainServicesConfiguration();
builder.Services.AddInfrastructureDataDependecyInjections(builder.Configuration);
builder.Services.AddAutoMapper(Assembly.Load(nameof(AppServices)));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();