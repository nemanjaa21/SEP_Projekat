using BankService.Repository;
using BitcoinPaymentService.Data;
using BitcoinPaymentService.Interfaces;
using BitcoinPaymentService.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<BitcoinDbContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("CryptoDB")));

builder.Services.AddScoped<DbContext, BitcoinDbContext>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<ITransactionService, TransactionService>();
builder.Services.AddScoped<IHelperService, HelperService>();
builder.Services.AddScoped<IBitcoinPaymentService, BitcoinPaymentServiceImpl>();


builder.Services.AddCors(o => o.AddPolicy("CORSpolicy", p => p.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseCors("CORSpolicy");

app.MapControllers();

app.Run();
