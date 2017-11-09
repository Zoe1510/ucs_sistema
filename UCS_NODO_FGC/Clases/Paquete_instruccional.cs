using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace UCS_NODO_FGC.Clases
{
    public class Paquete_instruccional
    {
        public  string bitacora { get; set; }
       
        public string manual { get; set; }
        public string presentacion { get; set; }
        public string contenido { get; set; }

        public int id_pinstruccional { get; set; }


        public static string _bitacora { get; set; }
       
        public static string _manual { get; set; }
        public static string _presentacion { get; set; }
        public static string _contenido { get; set; }
        public static int id_pin { get; set; }
        public static string tipo_curso { get; set; }

        public static int ModificarContenidoPQ(MySqlConnection conexion, Paquete_instruccional pq)
        {
            int resultado = 0;
            MySqlCommand comando = new MySqlCommand(String.Format("UPDATE p_instruccional SET p_contenido='{1}' WHERE id_pinstruccional='{0}' ", pq.id_pinstruccional, pq.contenido), conexion);
            resultado = comando.ExecuteNonQuery();
            return resultado;
        }
        public static int ModificarPresentacionPQ(MySqlConnection conexion, Paquete_instruccional pq)
        {
            int resultado = 0;
            MySqlCommand comando = new MySqlCommand(String.Format("UPDATE p_instruccional SET p_presentacion='{1}' WHERE id_pinstruccional='{0}' ", pq.id_pinstruccional, pq.presentacion), conexion);
            resultado = comando.ExecuteNonQuery();
            return resultado;
        }
        public static int ModificarBitacoraPQ(MySqlConnection conexion, Paquete_instruccional pq)
        {
            int resultado = 0;
            MySqlCommand comando = new MySqlCommand(String.Format("UPDATE p_instruccional SET p_bitacora='{1}' WHERE id_pinstruccional='{0}' ", pq.id_pinstruccional, pq.bitacora), conexion);
            resultado = comando.ExecuteNonQuery();
            return resultado;
        }

      
        public static int ModificarManualPQ(MySqlConnection conexion, Paquete_instruccional pq)
        {
            int resultado = 0;
            MySqlCommand comando = new MySqlCommand(String.Format("UPDATE p_instruccional SET p_manual='{1}' WHERE id_pinstruccional='{0}' ", pq.id_pinstruccional, pq.manual), conexion);
            resultado = comando.ExecuteNonQuery();
            return resultado;
        }
    }
}
