using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PaymentServiceProvider.Data;
using PaymentServiceProvider.Interfaces;
using PaymentServiceProvider.Mapping;
using PaymentServiceProvider.RabbitMQ;
using PaymentServiceProvider.Repository;
using PaymentServiceProvider.Services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<PaymentServiceDbContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("PaymentServiceDB")));
builder.Services.AddScoped<DbContext, PaymentServiceDbContext>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IPaymentService, PaymentServiceImpl>();
builder.Services.AddScoped<IMerchantService, MerchantServiceImpl>();
builder.Services.AddSingleton<IRabbitMqUtil, RabbitMqUtil>();

builder.Services.AddSwaggerGen();

builder.Services.AddCors(o => o.AddPolicy("CORSpolicy", p => p.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new MappingProfile());
});
IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

var app = builder.Build();

app.UseHttpsRedirection();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.UseCors("CORSpolicy");

app.MapControllers();

app.Run();
