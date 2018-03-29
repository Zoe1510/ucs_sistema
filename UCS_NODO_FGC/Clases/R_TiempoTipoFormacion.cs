using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UCS_NODO_FGC.Clases
{
    public class R_TiempoTipoFormacion
    {
        public string nombreyapellido { get; set; }
        public string tipo_formacion { get; set; }
        public string nro_formaciones { get; set; }
        public string fecha_vieja { get; set; }
        public string fecha_hoy { get; set; }
        public string duracion { get; set; }
        public string hora_emision { get; set; }
        public Byte[] Logo { get; set; }
    }
}
