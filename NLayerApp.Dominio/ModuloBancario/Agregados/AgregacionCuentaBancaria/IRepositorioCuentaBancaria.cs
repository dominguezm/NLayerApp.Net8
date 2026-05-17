using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NLayerApp.Dominio.Bases;

namespace NLayerApp.Dominio.ModuloBancario.Agregados.AgregacionCuentaBancaria
{
    public interface IRepositorioCuentaBancaria
    {
        IUnidadDeTrabajo UnidadDeTrabajo { get; }

        // Metodo para buscar una cuenta por su ID
        Task<CuentaBancaria> GetAsync(Guid cuentaId);

        //Metodo para buscar una cuenta por su numero de cuenta
        Task<CuentaBancaria> GetPorNumCuentaBancariaAsync(string numCuenta);


    }
}
