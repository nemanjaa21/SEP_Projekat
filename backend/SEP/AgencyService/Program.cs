using AgencyService.Data;
using AgencyService.Interfaces;
using AgencyService.Mapping;
using AgencyService.Repository;
using AgencyService.Service;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AgencyServiceDBContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("AgencyDB")));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<DbContext, AgencyServiceDBContext>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IServiceOfferItemService, ServiceOfferItemService>();
builder.Services.AddScoped<IServiceOfferService,  ServiceOfferService>();
builder.Services.AddScoped<IPaymentServiceService, PaymentServiceService>();
builder.Services.AddScoped<IAgencyService, AgencyService.Service.AgencyService>();

builder.Services.AddAuthorization();

var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new MappingProfile());
});
IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

var _logger = new LoggerConfiguration().WriteTo.File("C:\\Users\\Zdravko\\Desktop\\SEP_Projekat\\Logs\\logs.log", rollingInterval: RollingInterval.Day).CreateLogger();
builder.Logging.AddSerilog(_logger);

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
