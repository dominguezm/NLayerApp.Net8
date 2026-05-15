using NLayerApp.Dominio.ModuloBancario.Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NLayerApp.Dominio.ModuloBancario.Agregados.AgregacionCuentaBancaria;

namespace NLayerApp.Dominio.ModuloBancario.Servicios
{
    public class ServicioTransferenciaBancaria : IServicioTransferenciaBancaria
    {
        public void RealizarTranferencia(decimal monto, CuentaBancaria cuentaOrigen, CuentaBancaria cuentaDestino)
        {
            //1 Validaciones basicas de entrada
            if (cuentaOrigen == null)
                throw new ArgumentNullException(nameof(cuentaOrigen), "La cuenta de origen no puede ser nula.");
            if (cuentaDestino == null)
                throw new ArgumentNullException(nameof(cuentaDestino), "La cuenta de destino no pueder nula.");
            if (cuentaOrigen.Id == cuentaDestino.Id)
                throw new InvalidOperationException("No se puede transferir dinero a la misma cuenta.");

            //2 ejecucion de la logica de negocio apoyandonos en las entradas
            // Intentamos retirar el dinero de la cuenta origen
            cuentaOrigen.Retirar(monto);

            //Si el retiro fue exitoso (no lanzo excepcion por falta de fondos o bloqueo,
            //procedemos a retirar la cuenta destino
            cuentaDestino.Depositar(monto);
        }
    }
}
