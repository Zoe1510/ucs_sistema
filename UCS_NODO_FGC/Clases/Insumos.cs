using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace UCS_NODO_FGC.Clases
{
    public class Insumos
    {
        public int id_insumos { get; set; }
        public string contenido_insumo { get; set; }

        public static int id_insu { get; set;}
        public static string contenido_insu { get; set; }

        public Insumos()
        {

        }

        public Insumos(int id, string conte)
        {
            this.id_insumos = id;
            this.contenido_insumo = conte;
        }

        public static int AgregarInsumo(MySqlConnection conexion, Insumos i)
        {
            int retorno = 0;
            MySqlCommand comando = new MySqlCommand(String.Format("INSERT INTO insumos (ins_contenido) VALUES ('{0}')", i.contenido_insumo), conexion);
            retorno = comando.ExecuteNonQuery();
            return retorno;
        }

        public static int ExisteInsumo(MySqlConnection conexion, Insumos i)
        {
            int id_insumo = 0;
            MySqlCommand comando = new MySqlCommand(String.Format("SELECT id_insumos FROM insumos WHERE ins_contenido ='{0}'", i.contenido_insumo), conexion);
            MySqlDataReader leer = comando.ExecuteReader();

            while (leer.Read())
            {
                id_insumo = leer.GetInt32(0);


            }

            return id_insumo;
        }

        public static int ActualizarInsumo(MySqlConnection conexion, Insumos i)
        {
            int retorno = 0;
            MySqlCommand comando = new MySqlCommand(String.Format("UPDATE insumos SET  ins_contenido='{1}' WHERE id_insumos='{0}' ", i.id_insumos, i.contenido_insumo), conexion);
            retorno = comando.ExecuteNonQuery();
            return retorno;
        }

        public static int EliminarInsumo(MySqlConnection conexion, Insumos i)
        {
            int retorno = 0;

            MySqlCommand comando = new MySqlCommand(String.Format("DELETE FROM insumos WHERE id_insumos='{0}' ",i.id_insumos), conexion);
            retorno = comando.ExecuteNonQuery();
            return retorno;
        }
    }
}
