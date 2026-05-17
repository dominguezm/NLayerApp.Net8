using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayerApp.Dominio.Bases
{
    public interface IUnidadDeTrabajo : IDisposable
    {
        //confirma todos los cambios realizados en la unidad
        Task<int> ConfirmarASync();
    }
}
