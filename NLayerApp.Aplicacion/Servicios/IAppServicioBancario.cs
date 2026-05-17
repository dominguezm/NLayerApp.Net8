using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NLayerApp.Aplicacion.DTOs;

namespace NLayerApp.Aplicacion.Servicios
{
    public interface IAppServicioBancario
    {
        Task RealizarTransferenciaAsync(SolicitudTransferenciaDTO transferenciaDto);
    }
}
