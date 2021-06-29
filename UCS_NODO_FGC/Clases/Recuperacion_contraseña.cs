using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UCS_NODO_FGC.Clases
{
    public class Recuperacion_contraseña
    {
        public static int Opcion { get; set; } 
        //opcion1: para preguntas de seguridad
        //opcion2 para correo electronico
        //opcion3: para cambio de contraseña la primera vez que entra al sistema 
        public static string nombre { get; set; }
        public static string correo { get; set; }
        public static int cedula { get; set; }
        public static int id_usuario { get; set; }
       
    }
}
