using Application.ConcreteObjects;
using Application.Core;
using Application.Interfaces;
using Application.Services;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Interfaces;
using Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DataBaseContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});


builder.Services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddTransient<IInvestmentPerformanceService, InvestmentPerformanceService>();
builder.Services.AddTransient<IInvestmentRepository, InvestmentRepository>();
builder.Services.AddTransient<IStockInvestmentRepository, StockInvestmentRepository>();
builder.Services.AddTransient<IStockRepository, StockRepository>();
builder.Services.AddScoped<InvestmentDetailFactory>();
builder.Services.AddScoped<StockDetail>();
builder.Services.AddScoped<BondDetail>();
builder.Services.AddScoped<MutualFundDetail>();


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

var context = app.Services.CreateScope().ServiceProvider.GetRequiredService<DataBaseContext>();
context.Database.Migrate();
await Seed.SeedData(context);

app.Run();
