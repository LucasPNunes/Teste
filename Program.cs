using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using VendasLitoral.Domain.Repositories;
using VendasLitoral.Infrastructure.Context;
using VendasLitoral.Infrastructure.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<IPedidoRepository, PedidoRepository>();
builder.Services.AddScoped<IVendedorRepository, VendedorRepository>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllersWithViews()
    .AddNewtonsoftJson(options =>
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
    );

var connectionString = builder.Configuration.GetConnectionString("DBConnection");
builder.Services.AddDbContext<ConnectionContext>(options =>
    options.UseSqlServer(connectionString)
);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});

var app = builder.Build();

app.UseCors("AllowAll");

app.MapControllers();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();