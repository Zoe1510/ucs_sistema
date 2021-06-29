using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UCS_NODO_FGC.Clases
{
    public class Facilitador_Seleccionado
    {
        public static int VER { get; set; } //(1 para ver info. 0 para modificar
        public static int id_facilitador { get; set; }
        public static string ci_facilitador { get; set; }
        public static string nombre_facilitador { get; set; }
        public static string apellido_facilitador { get; set; }
        public static string especialidad_facilitador { get; set; }
        public static string ubicacion_facilitador { get; set; }
        public static string tlfn_facilitador { get; set; }
        public static string correo_facilitador { get; set; }
        public static string nacionalidad_fa { get; set; }
        public static int requerimiento_ince { get; set; }
    }

    public class Reporte_Seleccionado
    {
        public static int id_reporte { get; set; }
        public static string creador { get; set; }
        public static string fecha_emision { get; set; }
        public static string nombre_reporte { get; set; }

    }
}
