using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace UCS_NODO_FGC.Clases
{
    public class Refrigerios
    {
        public int id_ref { get; set; }
        public string nombre { get; set; }
        public string contenido_ref { get; set; }

        public static int idR { get; set; }
        public static string nombreR { get; set; }
        public static string contenidoR { get; set; }

        public Refrigerios()
        {

        }

        public Refrigerios(int id, string n, string d)
        {
            this.id_ref = id;
            this.nombre = n;
            this.contenido_ref = d;
        }

        public static int AgregarRef(MySqlConnection conexion, Refrigerios re)
        {
            int retorno = 0;
            MySqlCommand comando = new MySqlCommand(String.Format("INSERT INTO refrigerios ( ref_nombre, ref_contenido) VALUES ('{0}', '{1}')", re.nombre, re.contenido_ref), conexion);
            retorno = comando.ExecuteNonQuery();
            return retorno;
        }

        public static int ExisteRef(MySqlConnection conexion, Refrigerios re)
        {
            int id_ref = 0;
            MySqlCommand comando = new MySqlCommand(String.Format("SELECT id_ref FROM refrigerios WHERE ref_nombre='{0}' OR ref_contenido= '{1}' ", re.nombre, re.contenido_ref), conexion);
            MySqlDataReader leer = comando.ExecuteReader();

            while (leer.Read())
            {
                id_ref = leer.GetInt32(0);


            }
            
            return id_ref;
        }

        public static int EliminarRef(MySqlConnection conexion, Refrigerios re)
        {
            int retorno = 0;

            MySqlCommand comando = new MySqlCommand(String.Format("DELETE FROM refrigerios WHERE id_ref='{0}' ",re.id_ref), conexion);
            retorno = comando.ExecuteNonQuery();
            return retorno;
        }

        public static int ModificarRef(MySqlConnection conexion, Refrigerios re)
        {

            int retorno = 0;

            MySqlCommand comando = new MySqlCommand(String.Format("UPDATE refrigerios SET  ref_nombre='{1}', ref_contenido='{2}'  WHERE id_ref='{0}' ", re.id_ref, re.nombre, re.contenido_ref), conexion);
            retorno = comando.ExecuteNonQuery();
            return retorno;
        }
    }
}
