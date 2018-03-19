using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UCS_NODO_FGC.Clases
{
    public class R_Formacion_lista
    {
        public string nombreyapellido { get; set; }
        public string nombre_formacion { get; set; }
        public string nombre_facilitador { get; set; }
        public string tipo_formacion { get; set; }
        public string duracion_formacion { get; set; }
        public string fecha_inicio { get; set; }
        public string fecha_culminacion { get; set; }
        public string cantidad_participantes { get; set; }
        public Byte[] Logo { get; set; }

        public List<R_Participantes_postulados> lista_participantes = new List<R_Participantes_postulados>();
    }
}
