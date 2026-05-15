using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NLayerApp.Dominio.ModuloERP.Agregados.AgregacionClientes;
using Xunit;

namespace NLayerApp.Dominio.PruebaUnit
{
    public class ClientePrueba
    {
        // [fact] indica que es un metodo de prueba unitaria estandar
        [Fact]
        public void Constructor_ConDatosValidos_CrearClienteCreditoCero()
        {
            // 1 Arrange (Preparar): define datos de entrada
            string nombres = "Jose";
            string apellidos = "Mendoza";

            //2 Act (Actuar): Ejecutamos la accion que queremos probar
            var cliente = new Cliente(nombres, apellidos);

            //3 Assert (Afirmar): Verificamos que el resultado sea el esperado
            Assert.NotNull(cliente);
            Assert.Equal(nombres, cliente.Nombres);
            Assert.Equal(apellidos, cliente.Apellidos);
            Assert.Equal(0, cliente.LimitCredito); //Regla de negocio: empeza con credito cero
            Assert.NotEqual(Guid.Empty, cliente.Id); //Verifica que nuestra clase Entidad autogenero el ID

        }
        // [Theory] nos permite ejecutar la misma prueba varias veces con diferentes datos en [InlineData]
        [Theory]
        [InlineData("", "Mendoza")]
        [InlineData("Jose", "")]
        [InlineData(" ", "Mendoza")]
        [InlineData(null, null)]
        public void Constructor_ConNombresInvalidos_lanzaArgumentException(string nombresInvalidos, string apellidosInvalidos)
        {
            //Act & Assert combinados: Esperamos que instanciar la clase lance una Exception
            Assert.Throws<ArgumentException>(() => new Cliente(nombresInvalidos, apellidosInvalidos));
        }

        [Fact]
        public void AsignarLimitCredito_conValorNegativo_LanzaInvalidooperationException()
        {
            //Arange: Preparamos un cliente valido
            var cliente = new Cliente("Ana", "Gomez");
            decimal limiteNegativo = -100m;

            //Act & Assert: Intentamos romper la regla de negocio
            var Excepcion = Assert.Throws<InvalidOperationException>(() => cliente.AsignarLimitCredito(limiteNegativo));

            //Verificamos que el mensaje de error sea exactamente el que definimos
            Assert.Equal("El limite de credito no puede ser negativo.", Excepcion.Message);
        }
    }
}

   


