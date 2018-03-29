using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UCS_NODO_FGC.Clases
{
    public class R_Formacion_DatosGenerales
    {

        public string nombre_formacion { get; set; }
        public string nombre_creador { get; set; }
        public string tipo_formacion { get; set; }
        public string duracion_formacion { get; set; }
        public string estatus_formacion { get; set; }
        public string tiempo_total { get; set; }
        public string nombreyapellido { get; set; } //este es para el Emitido por:
        public string fecha_actual { get; set; }
        public string fecha_inicio { get; set; }
        public string fecha_culminacion { get; set; }
        public string hora_emision { get; set; }

        public Byte[] Logo { get; set; }

        public List<R_EtapaFormacion> listaEtapa = new List<R_EtapaFormacion>();
    }
}
