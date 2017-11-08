using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace UCS_NODO_FGC.Clases
{
    public class conexion_bd
    {
        public MySqlConnection conexion;

        public conexion_bd()
        {
            conexion = new MySqlConnection("host=127.0.0.1; port=3306; user=root; password=root; database=ucs_bd");
        }

        public bool abrirconexion()
        {
            try
            {

                conexion.Open();
                return true;
            }
            catch (MySqlException er)
            {
                return false;
                throw er;
            }

        }
        public bool cerrarconexion()
        {
            try
            {
                conexion.Close();
                return true;
            }
            catch (MySqlException er)
            {
                return false;
                throw er;
            }

        }

    }
}
