using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace UCS_NODO_FGC.Clases
{
    public class Empresa
    {
        public int id_clientes { get; set; }
        public string nombre_empresa { get; set; }
        public int fee { get; set; }

        public static int ModificarArea { get; set; }

      
    }

    
}
