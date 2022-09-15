using FluentValidation.AspNetCore;
using FluentValidation;
using AppServices.Services;
using DomainService.Services;
using DomainModels.Entities;
using AppServices.Validator;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddSingleton<ICustomerService, CustomerService>();
builder.Services.AddTransient<ICustomerAppService, CustomerAppService>();
builder.Services.AddScoped<IValidator<CustomerEntity>, CustomerValidator>();

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
