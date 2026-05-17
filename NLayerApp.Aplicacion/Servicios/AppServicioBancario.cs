using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NLayerApp.Aplicacion.DTOs;
using NLayerApp.Dominio.ModuloBancario.Agregados.AgregacionCuentaBancaria;
using NLayerApp.Dominio.ModuloBancario.Servicios;

namespace NLayerApp.Aplicacion.Servicios
{
    public class AppServicioBancario(IRepositorioCuentaBancaria repositorioCuenta, IServicioTransferenciaBancaria dominioServicioTranferencia) : IAppServicioBancario
    {
        public async Task RealizarTransferenciaAsync(SolicitudTransferenciaDTO transferenciaDto)
        {
            // 1 OBTENER DATOS
            var cuentaOrigen = await repositorioCuenta.GetPorNumCuentaBancariaAsync(transferenciaDto.NumCuentaOrigen);
            var cuentaDeastino = await repositorioCuenta.GetPorNumCuentaBancariaAsync(transferenciaDto.NumCuentaDestino);

            if (cuentaOrigen == null || cuentaDeastino == null)
                throw new ApplicationException("Una de las cuentas no existe en el sistema.");

            // 2 EJECUTAR LOGICA DE NEGOCIO
            // Aqui le pasamos la responsabilidad a nuestro servicio de dominio que ya probamos
            dominioServicioTranferencia.RealizarTranferencia(transferenciaDto.Monto, cuentaOrigen, cuentaDeastino);

            // 3 GUARDAR CAMBIOS
            // La UnidadDeTrabajo garantiza que se guarde ambos cambios (retiro y deposito) o ninguno
            await repositorioCuenta.UnidadDeTrabajo.ConfirmarASync();

        }
    }
}
