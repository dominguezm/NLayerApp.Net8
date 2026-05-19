using Microsoft.EntityFrameworkCore;
using NLayerApp.Aplicacion.Servicios;
using NLayerApp.Dominio.ModuloBancario.Agregados.AgregacionCuentaBancaria;
using NLayerApp.Dominio.ModuloBancario.Servicios;
using NLayerApp.Infraestructura.Data;
using NLayerApp.Infraestructura.Repositorios;

var builder = WebApplication.CreateBuilder(args);

// 1 Registrar Entity Framework Core con la cadena de conexion leida de appsettings.json
builder.Services.AddDbContext<ContextBancario>(opciones => 
    opciones.UseSqlServer(builder.Configuration.GetConnectionString("ConexionBancaria")));

// 2 REGISTRAR CAPAS
// Cuando el sistema pida esta interfaz, entregale esta Clase Real"
builder.Services.AddScoped<IRepositorioCuentaBancaria, RepositorioCuentaBancaria>();
builder.Services.AddScoped<IServicioTransferenciaBancaria, ServicioTransferenciaBancaria>();
builder.Services.AddScoped<IAppServicioBancario, AppServicioBancario>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(); // Para probar la API visualmente

// Add services to the container.

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
