using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using NLayerApp.Aplicacion.DTOs;
using NLayerApp.Aplicacion.Servicios;

namespace NLayerApp.Presentacion.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankingController(IAppServicioBancario appServicioBancario) : ControllerBase
    {
        [HttpPost("transferir")]
        public async Task<IActionResult> Transferir([FromBody] SolicitudTransferenciaDTO solicitudDto)
        {
            try
            {
                //Delegamos la peticion inmediatamente a la capa de Aplicacion
                await appServicioBancario.RealizarTransferenciaAsync(solicitudDto);
                //Si todo salio bien, devolvemos un HTTP 200 OK con un mensaje
                return Ok(new { Message = "Transferencia realizada con éxito de forma íntegra." });
            }
            catch (ApplicationException ex)
            {
                //Errores controlados de validacion de la aplicacion (ejm cuenta no existe)
                return BadRequest(new {Error = ex.Message});
            }
            catch (InvalidOperationException ex)
            {
                //Errores de nuestras reglas de negocio del dominoi (ej fondos insuficientes)
                return BadRequest(new { Error = ex.Message });
            }
            catch (Exception)
            {
                //Errores improvistos del servidor
                return StatusCode(500, new {Message = "Ocurrió un error interno en el servidor bancario." });
            }
        }
    }
}
