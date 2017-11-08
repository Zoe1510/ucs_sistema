using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace UCS_NODO_FGC.Clases
{
    public class Difusion
    {
        public int id_dif { get; set; }
        public string contenido_dif { get; set; }

        public static int idD { get; set; }
        public static string contenido { get; set; }
        public Difusion()
        {

        }

        public Difusion(int i, string c)
        {
            this.id_dif = i;
            this.contenido_dif = c;
        }

        public static int AgregarPublicidad(MySqlConnection conexion, Difusion d)
        {
            int retorno = 0;
            MySqlCommand comando = new MySqlCommand(String.Format("INSERT INTO difusion (dif_contenido) VALUES ('{0}')", d.contenido_dif), conexion);
            retorno = comando.ExecuteNonQuery();
            return retorno;
        }

        public static int ExisteDif(MySqlConnection conexion, Difusion d)
        {
            int id_dif = 0;
            MySqlCommand comando = new MySqlCommand(String.Format("SELECT id_difusion FROM difusion WHERE dif_contenido ='{0}'", d.contenido_dif), conexion);
            MySqlDataReader leer = comando.ExecuteReader();

            while (leer.Read())
            {
                id_dif = leer.GetInt32(0);


            }

            return id_dif;
        }

        public static int EliminarDif(MySqlConnection conexion, Difusion d)
        {
            int retorno = 0;

            MySqlCommand comando = new MySqlCommand(String.Format("DELETE FROM difusion WHERE id_difusion='{0}' ", d.id_dif), conexion);
            retorno = comando.ExecuteNonQuery();
            return retorno;
        }

        public static int ModificarDif(MySqlConnection conexion, Difusion d)
        {

            int retorno = 0;

            MySqlCommand comando = new MySqlCommand(String.Format("UPDATE difusion SET  dif_contenido='{1}' WHERE id_difusion='{0}' ", d.id_dif,d.contenido_dif), conexion);
            retorno = comando.ExecuteNonQuery();
            return retorno;
        }

    }
}
