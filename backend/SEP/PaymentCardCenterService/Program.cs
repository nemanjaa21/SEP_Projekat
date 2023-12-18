using Microsoft.EntityFrameworkCore;
using PaymentCardCenterService.Data;
using PaymentCardCenterService.Interfaces;
using PaymentCardCenterService.Repository;
using PaymentCardCenterService.Service;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<BankDbContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("BankDB")));

builder.Services.AddScoped<DbContext, BankDbContext>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IPCCService, PCCService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
