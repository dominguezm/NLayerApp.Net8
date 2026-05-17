using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Moq;
using NLayerApp.Aplicacion.DTOs;
using NLayerApp.Aplicacion.Servicios;
using NLayerApp.Dominio.ModuloBancario.Agregados.AgregacionCuentaBancaria;
using NLayerApp.Dominio.ModuloBancario.Servicios;
using NLayerApp.Dominio.Bases;
using Xunit;

namespace NLayerApp.Aplicacion.Pruebas
{
    public class AppServicioBancarioPruebas
    {
        [Fact]
        public async Task RealizarTransferenciaAsync_CuentasValidas_OrquestaLaTransferenciaYGuardaLosCambios()
        {
            // 1 ARRANGE
            //creamos cuentas validas en memoria (para que el Mock las devuelva)
            var cuentaOrigen = new CuentaBancaria("ORG-123", Guid.NewGuid());
            cuentaOrigen.Depositar(500m);// Le damos fondos para que no falle el dominio
            var CuentaDestino = new CuentaBancaria("DST-456",Guid.NewGuid());
            //creamos los Mocks (Los dobles de prueba)
            var mockUnidadDeTrabajo = new Mock<IUnidadDeTrabajo>();
            var mockRepositorio = new Mock<IRepositorioCuentaBancaria>();
            var mockDominioServicio = new Mock<IServicioTransferenciaBancaria>();

            // ENTRENAMOS AL MOCK DEL REPOSITORIO
            // Cuando te pidan la propiedad UnidadDeTrabajo, devuelve nuestro mockUnidadDeTrabajo
            mockRepositorio.Setup(r => r.UnidadDeTrabajo).Returns(mockUnidadDeTrabajo.Object);
            // Cuando te llaman a GetPorNumCuentaBancariaAsync con 'ORG-123' devuelve CuentaOrigen
            mockRepositorio.Setup(r => r.GetPorNumCuentaBancariaAsync("ORG-123")).ReturnsAsync(cuentaOrigen);
            mockRepositorio.Setup(r => r.GetPorNumCuentaBancariaAsync("DST-456")).ReturnsAsync(CuentaDestino);
            // Instanciamos el servicio real que queremos probar, inyectanfole los "actores"
            var appServicio = new AppServicioBancario(mockRepositorio.Object, mockDominioServicio.Object);
            //Creamos el DTO simulando lo que llegaria desde la pagina web (la API HTTP)
            var solicitudDto = new SolicitudTransferenciaDTO
            {
                NumCuentaOrigen = "ORG-123",
                NumCuentaDestino = "DST-456",
                Monto = 100m
            };

            // 2 ACT
            await appServicio.RealizarTransferenciaAsync(solicitudDto);

            // 3 ASSERT
            // Verificamos que el AppServicio realmente llamo al Serviciode Dominio
            // Exactamente 1 vez con los parametros correctos
            mockDominioServicio.Verify(ds => ds.RealizarTranferencia(
                100m,
                cuentaOrigen,
                CuentaDestino), 
                Times.Once); // 1 vez
            //Verificamos que el AppServicio le dijo a la BAse de Datos que guardara los cambios
            mockUnidadDeTrabajo.Verify(uow => uow.ConfirmarASync(), Times.Once);

        }

        [Fact]
        public async Task RealizarTRansferenciaAsync_CuentaNoExiste_LamzaApplicationException()
        {
            // Arrange
            var mockRepositorio = new Mock<IRepositorioCuentaBancaria>();
            var mockDominioServicio = new Mock<IServicioTransferenciaBancaria>();

            // Entrenamos al mock para que devuelva NULL (simulando que la cuenta no se encontro en la BD)
            mockRepositorio.Setup(r => r.GetPorNumCuentaBancariaAsync(It.IsAny<string>())).ReturnsAsync((CuentaBancaria)null!);
            var appServicio = new AppServicioBancario(mockRepositorio.Object, mockDominioServicio.Object);
            var solicitudDto = new SolicitudTransferenciaDTO { NumCuentaOrigen = "INVALID", NumCuentaDestino = "DST-456", Monto = 100m };

            // Act & Assert
            var exception  = await Assert.ThrowsAsync<ApplicationException>(() => appServicio.RealizarTransferenciaAsync(solicitudDto));
            Assert.Equal("Una de las cuentas no existe en el sistema.", exception.Message);

            // Aseguremos que, como fallo al inicio, NUNCA se llamo a la UnidadDeTrabajo para guardar nada
            var mockUnidadDeTrabajo = new Mock<IUnidadDeTrabajo>();
            mockUnidadDeTrabajo.Verify(uow => uow.ConfirmarASync(), Times.Never);

        }
    }
}
