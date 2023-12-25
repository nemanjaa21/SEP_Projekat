using AutoMapper;
using BankService.Data;
using BankService.Interfaces;
using BankService.Repository;
using BankService.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<BankDbContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("BankDB")));

builder.Services.AddScoped<DbContext, BankDbContext>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IBanksService, BanksService>();
builder.Services.AddScoped<ICardService, CardService>();
builder.Services.AddScoped<IMerchantService, MerchantService>();
builder.Services.AddScoped<IPSPService, PSPService>();
builder.Services.AddScoped<ITransactionService, TransactionService>();

builder.Services.AddCors(o => o.AddPolicy("CORSpolicy", p => p.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

var app = builder.Build();

// Configure the HTTP request pipeline.
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
