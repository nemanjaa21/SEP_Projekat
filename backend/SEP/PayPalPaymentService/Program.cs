using PayPalPaymentService.Interfaces;
using PayPalPaymentService.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IPayPalPaymentService, PayPalPaymentServiceImpl>();

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
