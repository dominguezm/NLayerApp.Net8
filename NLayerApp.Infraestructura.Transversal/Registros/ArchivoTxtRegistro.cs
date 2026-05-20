using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;

namespace NLayerApp.Infraestructura.Transversal.Registros
{
    public class ArchivoTxtRegistro<T> : IAppRegistro<T>
    {
        private readonly string _logRutaDeArchivo;
        private static readonly object _llave= new object();

        public ArchivoTxtRegistro()
        {
            //Definimos donde se guardara el archivo.
            //BaseDirectory es la carpeta donde corre la API (bin/debug/net8.0)
            string logRuta = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs");

            //Si la carpeta "Logs" no existe, la creamos
            if (!Directory.Exists(logRuta))
                Directory.CreateDirectory(logRuta);

            // El archivo se llamara "Log_20260520.txt" (cambia segun la fecha del dia)
            _logRutaDeArchivo = Path.Combine(logRuta, $"Log_{DateTime.Now:yyyyMMdd}.txt");
        }

        private void EscribirArchivo(string nivel, string mensaje)
        {
            // Formato: [fecha/Hora] [Nivel] [Clase que llamo a log] -> Mensaje
            string logLinea = $"[{DateTime.Now:HH:mm:ss}] [{nivel}] [{typeof(T).Name}] -> {mensaje}{Environment.NewLine}";

            // Bloqueamos el hilo un milisegundo para que dos peticiones web no intenten escribir en el mismo archivo a la vez
            lock (_llave)
                File.AppendAllText(_logRutaDeArchivo, logLinea);
        }

        public void RegistroAdvertencia(string mensaje)
        {
            EscribirArchivo("WARN", mensaje);
        }

        public void RegistroError(Exception exception, string mensaje)
        {
            EscribirArchivo("ERROR", $"{mensaje} | Detalles: {exception.Message}");
        }

        public void RegistroInformacion(string mensaje)
        {
            EscribirArchivo("INFO", mensaje);
        }
    }
}
