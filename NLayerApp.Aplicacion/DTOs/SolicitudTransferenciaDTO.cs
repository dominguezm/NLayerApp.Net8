using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayerApp.Aplicacion.DTOs;

public class SolicitudTransferenciaDTO
{
    public string NumCuentaOrigen { get; set; }
    public string NumCuentaDestino { get; set; }
    public decimal Monto { get; set; }
    
}
