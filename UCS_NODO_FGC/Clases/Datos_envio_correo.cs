using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UCS_NODO_FGC.Clases
{
    public class Datos_envio_correo
    {
        public static int idcurso { get; set; }
        public static string nombreformacion { get; set; }
        public static int opcion { get; set; } //0=INVALIDO; 1=correo participante; 2=Correo Facilitador, 3=correo Cliente
        public static int idarea { get; set; }
        public static List<string> correos_participantes = new List<string>();
    }
}
