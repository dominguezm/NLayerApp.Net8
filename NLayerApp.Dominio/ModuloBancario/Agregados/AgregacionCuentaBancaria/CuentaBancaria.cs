using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NLayerApp.Dominio.Bases;

namespace NLayerApp.Dominio.ModuloBancario.Agregados.AgregacionCuentaBancaria
{
    public class CuentaBancaria : Entidad
    {
        //propiedades protegidas.
        public string NumCuentaBancaria { get; private set; }
        public decimal Balance { get; private set; }
        public Guid ClienteId { get; private set; }
        public bool EsBloqueado { get; private set; }

        //Constructor vacio para frameworks Entity Framework Core
        protected CuentaBancaria() { }

        // Constructor Establece el estado inicial valido de una cuenta
        public CuentaBancaria(string numCuentaBancaria, Guid clienteId)
        {
            if (string.IsNullOrWhiteSpace(numCuentaBancaria))
                throw new ArgumentException("El numero de cuenta es obligatorio.", nameof(numCuentaBancaria));
            NumCuentaBancaria = numCuentaBancaria;
            ClienteId = clienteId;
            Balance = 0m; //Se crea con Balance cero exato decimal *m
            EsBloqueado = false; //Se crea desbloqueado
        }

        //Comportamiento 1: Depositar dinero
        public void Depositar(decimal monto)
        {
            if (EsBloqueado)
                throw new InvalidOperationException("No se puede realizar deposito en una cuenta bloqueada.");
            if (monto <= 0)
                throw new ArgumentException("El monto a depositar debe ser mayor a cero.");

            Balance += monto;
        }

        //comportamiento 2: Retirar dinero
        public void Retirar(decimal monto)
        {
            if (EsBloqueado)
                throw new InvalidOperationException("No se puede realizar retiros en una cuenta bloqueada.");
            if (monto <= 0)
                throw new ArgumentException("El monto a retirar debe ser mayor a cero.");
            if (Balance < monto)
                throw new InvalidOperationException("Fondos insuficientes para realizar el retiro.");

            Balance -= monto;
        }

        //Comportamiento 3 y 4: Bloquear y desbloquear cuenta
        public void Bloquear()
        {
            EsBloqueado = true;
        }

        public void Desbloquear()
        {
            EsBloqueado = false;
        }
    }
}
