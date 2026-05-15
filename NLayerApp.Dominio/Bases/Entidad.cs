using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayerApp.Dominio.Bases
{
    public abstract class Entidad
    {
        public Guid Id { get; protected set; }

        protected Entidad()
        {
            // Identificador unico por defecto para crear cualquier entidad
            Id = Guid.NewGuid();
        }
    }
}
