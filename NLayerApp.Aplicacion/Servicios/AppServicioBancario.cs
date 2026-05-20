using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NLayerApp.Aplicacion.DTOs;
using NLayerApp.Dominio.ModuloBancario.Agregados.AgregacionCuentaBancaria;
using NLayerApp.Dominio.ModuloBancario.Servicios;

using NLayerApp.Infraestructura.Transversal.Registros;

namespace NLayerApp.Aplicacion.Servicios
{
    public class AppServicioBancario(
        IRepositorioCuentaBancaria repositorioCuenta, 
        IServicioTransferenciaBancaria dominioServicioTranferencia,
        IAppRegistro<AppServicioBancario> registro) : IAppServicioBancario //Agregamos el registro de log
    {
        public async Task RealizarTransferenciaAsync(SolicitudTransferenciaDTO transferenciaDto)
        {
            // 1 OBTENER DATOS
            var cuentaOrigen = await repositorioCuenta.GetPorNumCuentaBancariaAsync(transferenciaDto.NumCuentaOrigen);
            var cuentaDeastino = await repositorioCuenta.GetPorNumCuentaBancariaAsync(transferenciaDto.NumCuentaDestino);

            if (cuentaOrigen == null || cuentaDeastino == null)
            {
                registro.RegistroAdvertencia("Transferencia fallida: Una de las cuentas no existe en la base de datos.");
                throw new ApplicationException("Una de las cuentas no existe en el sistema.");
            }
            try
            {
                // 2 EJECUTAR LOGICA DE NEGOCIO
                // Aqui le pasamos la responsabilidad a nuestro servicio de dominio que ya probamos
                dominioServicioTranferencia.RealizarTranferencia(transferenciaDto.Monto, cuentaOrigen, cuentaDeastino);

                // 3 GUARDAR CAMBIOS
                // La UnidadDeTrabajo garantiza que se guarde ambos cambios (retiro y deposito) o ninguno
                await repositorioCuenta.UnidadDeTrabajo.ConfirmarASync();

                registro.RegistroInformacion("Transferencia ejecutada y guardad con exito.");
            }
            catch (Exception ex)
            {
                //Si el dominio lanza error por fondos insuficientes, lo registramos como advertencia o error
                registro.RegistroError(ex, "Error durante la ejecucion de la tranferencia en el dominio.");
                throw; //Realizamos la excepcion para que la API responda con un HTTP 400
            }
        }
    }
}
