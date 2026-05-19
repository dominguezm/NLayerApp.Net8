using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NLayerApp.Dominio.ModuloBancario.Agregados.AgregacionCuentaBancaria;

namespace NLayerApp.Infraestructura.ConfiguracionesDeEntidad
{
    public class ConfiguracionCuentaBancaria : IEntityTypeConfiguration<CuentaBancaria>
    {
        public void Configure(EntityTypeBuilder<CuentaBancaria> constructor)
        {
            // 1 Nombre de la tabla
            constructor.ToTable("CuentasBancarias");

            // 2 Clave primaria (Viene de nuestra clase base Entity)
            constructor.HasKey(b => b.Id);

            // 3 Configuracion de propiedades
            constructor.Property(b => b.NumCuentaBancaria)
                .IsRequired()
                .HasMaxLength(30);
            constructor.Property(b => b.Balance)
                .IsRequired()
                .HasColumnType("decimal(18,2)");
            constructor.Property(b =>b.EsBloqueado)
                .IsRequired();
        }
    }
}
