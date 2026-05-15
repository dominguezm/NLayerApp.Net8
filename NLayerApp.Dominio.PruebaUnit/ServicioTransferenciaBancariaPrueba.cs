using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NLayerApp.Dominio.ModuloBancario.Agregados.AgregacionCuentaBancaria;
using NLayerApp.Dominio.ModuloBancario.Servicios;
using Xunit;

namespace NLayerApp.Dominio.PruebaUnit
{
    public class ServicioTransferenciaBancariaPrueba
    {
        [Fact]
        public void RealizarTranferencia_TransferenciaValida_ActualizaSaldosCorrectamente()
        {
            // 1 Arrange (Preparar)
            var cuentaOrigen = new CuentaBancaria("ORG-123", Guid.NewGuid());
            cuentaOrigen.Depositar(500m); //le damos 500 de saldo inicial

            var cuentaDestino = new CuentaBancaria("DST-456", Guid.NewGuid());
            //El destino nace con saldo cero
            var servicioTransferencia = new ServicioTransferenciaBancaria();
            decimal montoTranferencia = 200m;

            // 2 Act (Actuar)
            servicioTransferencia.RealizarTranferencia(montoTranferencia, cuentaOrigen, cuentaDestino);

            // 3 Assert (Afirmar)
            Assert.Equal(300m, cuentaOrigen.Balance); // 500m - 200m = 300
            Assert.Equal(200m, cuentaDestino.Balance); // 0m + 200m = 200
        }

        [Fact]
        public void RealizarTransferencia_MismaCuenta_LanzaInvalidOperationException()
        {
            // Arrange
            var cuenta = new CuentaBancaria("ACC-123", Guid.NewGuid());
            var servicioTransferencia = new ServicioTransferenciaBancaria();

            //Act & Assert
            var exception = Assert.Throws<InvalidOperationException>(() => servicioTransferencia.RealizarTranferencia(100m, cuenta, cuenta)); //origen destino son la misma cuenta

            Assert.Equal("No se puede transferir dinero a la misma cuenta.", exception.Message);
        }

        [Fact]
        public void RealizarTransferencia_SinFondosEnOrigen_LanzaExcepcionYNoAlteraDestino()
        {
            // Arrange
            var cuentaOrigen = new CuentaBancaria("ORG-123", Guid.NewGuid());
            cuentaOrigen.Depositar(50m); //depositamos 50

            var cuentaDestino = new CuentaBancaria("DST-456", Guid.NewGuid());
            //cuenta destino tiene cero

            var servicioTransferencia = new ServicioTransferenciaBancaria();
            decimal montoTransferencia = 100m;


            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => servicioTransferencia.RealizarTranferencia(montoTransferencia, cuentaOrigen, cuentaDestino));

            // ASSERT Verificamos que el saldo del destino sigue en 0
            Assert.Equal(0m, cuentaDestino.Balance);

            // verificamos que el origen tampoco perdio su dinero
            Assert.Equal(50m, cuentaOrigen.Balance);
        }

        [Fact]
        public void RealizarTransferencia_CuentasNulas_LanzaArgumentNullException()
        {
            // Arrange
            var cuentaValida = new CuentaBancaria("ACC-123", Guid.NewGuid());
            var servicioTransferencia = new ServicioTransferenciaBancaria();

            // Act & Assert
            Assert.Throws <ArgumentNullException>(() => servicioTransferencia.RealizarTranferencia(100m, null!, cuentaValida));
            Assert.Throws <ArgumentNullException>(() => servicioTransferencia.RealizarTranferencia(100m, cuentaValida, null!));
        }
    }
}
