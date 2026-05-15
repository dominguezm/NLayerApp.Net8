using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NLayerApp.Dominio.ModuloBancario.Agregados.AgregacionCuentaBancaria;

namespace NLayerApp.Dominio.ModuloBancario.Servicios
{
    public interface IServicioTransferenciaBancaria
    {
        //Define la accion de negocio de transferir el dinero
        void RealizarTranferencia(decimal monto, CuentaBancaria cuentaOrigen, CuentaBancaria cuentaDestino);
    }
}
