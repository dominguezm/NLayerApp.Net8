using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using NLayerApp.Dominio.ModuloBancario.Agregados.AgregacionCuentaBancaria;
using NLayerApp.Dominio.Bases;

namespace NLayerApp.Infraestructura.Data
{
    // Hereda de DbContext y tambien implementa nuestra interfaz de dominio IUnidadDeTrabajo
    public class ContextBancario : DbContext, IUnidadDeTrabajo
    {
        // DbSet representa la tabla en la base de datos
        public DbSet<CuentaBancaria> CuentasBancarias { get; set; }
        // El constructor recibe las opciones (cadena de conexion) desde la capa de Presentacion
        public ContextBancario(DbContextOptions<ContextBancario> opciones) : base(opciones) 
        {
        }
        //Implementacion del metodo de nuestro IUnidadDeTrabajo
        public async Task<int> ConfirmarASync()
        {
            //SaveChangesAsync envuelve todos los INSERT, UPDATE, DELETE  en una transaccion automatica
            return await base.SaveChangesAsync();
        }
        //Aqui le decimos a Entity Framework Core como mapear nuestras entidades limpias a tablas SQL
        //metodo de DBcontext
        protected override void OnModelCreating(ModelBuilder constructorModelo)
        {
            //Sepacion de configuraciones de cada tabla en clases distintas
            constructorModelo.ApplyConfigurationsFromAssembly(typeof(ContextBancario).Assembly);

            base.OnModelCreating(constructorModelo);
        }
    }
}
