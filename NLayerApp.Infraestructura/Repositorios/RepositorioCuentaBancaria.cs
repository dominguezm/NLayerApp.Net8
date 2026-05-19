using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using NLayerApp.Dominio.ModuloBancario.Agregados.AgregacionCuentaBancaria;
using NLayerApp.Dominio.Bases;
using NLayerApp.Infraestructura.Data;

namespace NLayerApp.Infraestructura.Repositorios
{
    //Implelemtamos la interfaz que definimos en la capa de Dominio
    public class RepositorioCuentaBancaria(ContextBancario context) : IRepositorioCuentaBancaria
    {
        // La UnidadDeTrabajo es simplemente nuestro ContextBancario, ya que implementa la interfaz
        public IUnidadDeTrabajo UnidadDeTrabajo => context;

        public async Task<CuentaBancaria> GetAsync(Guid cuentaId)
        {
            // FirsOrDefaultAsync hace el "SELECT * FROM CuentasBancarias WHERE Id = @Id"
            return await context.CuentasBancarias.FirstOrDefaultAsync(b => b.Id == cuentaId);
        }

        public async Task<CuentaBancaria> GetPorNumCuentaBancariaAsync(string numCuenta)
        {
            return await context.CuentasBancarias.FirstOrDefaultAsync(b => b.NumCuentaBancaria == numCuenta);
        }
    }
}
