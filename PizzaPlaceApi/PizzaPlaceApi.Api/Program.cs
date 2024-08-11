using Microsoft.EntityFrameworkCore;
using PizzaPlaceApi.Application.Interfaces;
using PizzaPlaceApi.Application.Services;
using PizzaPlaceApi.Domain.Repositories;
using PizzaPlaceApi.Infrastructure.Data;
using PizzaPlaceApi.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<ICsvImportService, CsvImportService>();
builder.Services.AddScoped<IPizzaTypeService, PizzaTypeService>();
builder.Services.AddScoped<IPizzaTypeRepository, PizzaTypeRepository>();
builder.Services.AddScoped<IPizzaService, PizzaService>();
builder.Services.AddScoped<IPizzaRepository, PizzaRepository>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();



// Configure DbContext for SQLite
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=app.db"));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
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
