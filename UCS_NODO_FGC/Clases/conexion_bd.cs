using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;


namespace UCS_NODO_FGC.Clases
{
    //En vez de enviar la variable conexion como parametro a todas las funciones se declara de manera estatic y solo haria falta llamar a la clase
    // como en Nueva_formacion_InCompany_Load
    public static class Conexion
    {
        public static MySqlConnection bd;

        public static String clave = "root";


        public static MySqlDataReader ConsultarBD(String query)
        {
            abrirconexion();
            MySqlCommand comando = new MySqlCommand(String.Format(query), bd);
            return comando.ExecuteReader();
        }

        public static bool abrirconexion()
        {
            try
            {
                //¿Causa algun problema cerrarla antes de abrirla?
                cerrarconexion();
                bd.Open();
                return true;
            }
            catch (MySqlException er)
            {
                return false;
                throw er;
            }

        }

        public static bool cerrarconexion()
        {
            try
            {
                bd.Close();
                return true;
            }
            catch (MySqlException er)
            {
                return false;
                throw er;
            }

        }

    }

    public class conexion_bd
    {
        public MySqlConnection conexion;

        public conexion_bd()
        {

            conexion = new MySqlConnection("host=127.0.0.1; port=3306; user=root; password="+Conexion.clave+"; database=ucs_bd");
            Conexion.bd = conexion;
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
