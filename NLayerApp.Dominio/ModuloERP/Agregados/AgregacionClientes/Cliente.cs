using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NLayerApp.Dominio.Bases;

namespace NLayerApp.Dominio.ModuloERP.Agregados.AgregacionClientes
{
    public class Cliente : Entidad
    {
        //las propiedades tiene "private set" no pueden ser modificadas desde afuera
        public string Nombres { get; private set; }
        public string Apellidos { get; private set; }
        public decimal LimitCredito { get; private set; }

        //constructor sin parametros requerido por Entity Framework Core
        protected Cliente() { }

        //constructor que obliga aun cliente inicie en un estado valido
        public Cliente(string nombres, string apellidos)
        {
            if (string.IsNullOrWhiteSpace(nombres) || string.IsNullOrWhiteSpace(apellidos))
                throw new ArgumentException("El nombre y apellidos son obligatorios para registrar un cliente.");
            Nombres = nombres;
            Apellidos = apellidos;
            LimitCredito = 0; // Regla: los clientes nuevos posee credito cero
        }
        
        // Comportamiento: Metodo explicito para modificar el estado (Regla de negocio)
        public void AsignarLimitCredito(decimal nuevoLimite)
        {
            if (nuevoLimite < 0)
                throw new InvalidOperationException("El limite de credito no puede ser negativo.");
            LimitCredito += nuevoLimite;
        }

    }
}
