using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NLayerApp.Dominio.ModuloBancario.Agregados.AgregacionCuentaBancaria;
using Xunit;

namespace NLayerApp.Dominio.PruebaUnit
{
    public class CuentaBancariaPrueba
    {
        [Fact]
        public void Constructor_ConDatosValidos_CreaCuentaDesbloqueadaYConBalanceCero()
        {
            //Arrange
            string numCuenta = "PE51-1234-5678-9012";
            Guid clienteId = Guid.NewGuid();

            //Act
            var cuenta = new CuentaBancaria(numCuenta, clienteId);

            //Assert
            Assert.NotNull(cuenta);
            Assert.Equal(numCuenta, cuenta.NumCuentaBancaria);
            Assert.Equal(clienteId, cuenta.ClienteId);
            Assert.Equal(0m, cuenta.Balance);
            Assert.False(cuenta.EsBloqueado);
        }

        [Fact]
        public void Depositar_MontoValido_IncrementaElBalance()
        {
            // Arrange
            var cuenta = new CuentaBancaria("123", Guid.NewGuid());
            decimal montoDeposito = 250.50m;

            // Act
            cuenta.Depositar(montoDeposito);

            // Assert
            Assert.Equal(montoDeposito, cuenta.Balance);
        }

        [Fact]
        public void Retirar_SinFondosSuficientes_LanzaInvalidOperationException()
        {
            // Arrange
            var cuenta = new CuentaBancaria("123", Guid.NewGuid());
            cuenta.Depositar(50m); //Tenemos 50 de saldo
            decimal montoRetiro = 100m; //Queremos retirar 100

            // Act & Assert
            var excepcion = Assert.Throws<InvalidOperationException>(() => cuenta.Retirar(montoRetiro));
            Assert.Equal("Fondos insuficientes para realizar el retiro.", excepcion.Message);
        }

        [Fact]
        public void Depositar_EnCuentaBloqueada_LanzainvalidOperationException()
        {
            // Arrange
            var cuenta = new CuentaBancaria("123", Guid.NewGuid());
            cuenta.Bloquear(); //Bloqueamos la cuenta (Regla de negocio)

            // Act & Assert
            var exception = Assert.Throws<InvalidOperationException>(() => cuenta.Depositar(50m));
            Assert.Equal("No se puede realizar deposito en una cuenta bloqueada.", exception.Message);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-50)]
        public void Retirar_montoCeroNegativo_LanzaArgumentException(decimal montoInvalido)
        {
            // Arrange
            var cuenta = new CuentaBancaria("123", Guid.NewGuid());
            cuenta.Depositar(100m);

            //Act & Assert
            Assert.Throws<ArgumentException>(() => cuenta.Retirar(montoInvalido));
        }
    }
}
