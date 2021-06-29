using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UCS_NODO_FGC.Clases
{
    public class Curso_IN
    {
        public int id_INCE { get; set; }
        public string nombre_INCE { get; set; }

        public static int In { get; set; } //si es 1, viene de inces, si es 0 viene de AFI
    }
}
