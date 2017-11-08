using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UCS_NODO_FGC.Clases
{
    public class Usuario_logeado
    {
        public static int id_usuario { get; set; }
        public static string cargo_usuario { get; set; }
        public static string nombre_usuario { get; set; }
        public static string apellido_usuario { get; set; }
        public static string password { get; set; }
        public static string tlfn_usuario { get; set; }
        public static byte[] imagen_usuario { get; set; }
        public static string correo_usuario { get; set; }
    }
}
