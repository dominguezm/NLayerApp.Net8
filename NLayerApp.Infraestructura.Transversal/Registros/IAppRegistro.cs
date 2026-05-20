using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayerApp.Infraestructura.Transversal.Registros
{
    public interface IAppRegistro<T>
    {
        void RegistroInformacion(string mensaje);
        void RegistroAdvertencia(string mensaje);
        void RegistroError(Exception exception, string mensaje);
    }
}
