using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UCS_NODO_FGC.Clases
{
    public class Participantes
    {
        public int id_participante { get; set; }
        public int ci_participante { get; set; }
        public int id_cli1 { get; set; }
        public string nombreP { get; set; }
        public string nacionalidad { get; set; }
        public string apellidoP { get; set; }
        public string correoP { get; set; }
        public string cargoE { get; set; }
        public string nivelE { get; set; }
        public string nombreE { get; set; }

        public Participantes()
        {

        }


        public Participantes(int id, int ci, string n, string a, string co, string ca, string ni, string ne)
        {
            this.id_participante = id;
            this.ci_participante = ci;
            this.nombreP = n;
            this.apellidoP = a;
            this.correoP = co;
            this.cargoE = ca;
            this.nivelE = ni;
            this.nombreE = ne;
        }
    }
}
