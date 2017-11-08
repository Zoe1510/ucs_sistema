using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace UCS_NODO_FGC.Clases
{
    public class Preguntas
    {
        public int id_pregunta { get; set; }
        public string pregunta { get; set; }
        public string respuesta { get; set; }
        public int id_recuperacion { get; set; }
        public int id_user1 { get; set; }
        //public Preguntas()
        //{

        //}

        public Preguntas(int Id_pregunta, string Pregunta, string Respuesta, int Id_user1)
        {
            id_pregunta = Id_pregunta;
            pregunta = Pregunta;
            respuesta = Respuesta;
            id_user1 = Id_user1;
        }
        public Preguntas( string Pregunta, string Respuesta) :this(-1, Pregunta, Respuesta, -1)
        { 
        }
        public Preguntas() : this(-1,   string.Empty, string.Empty,  -1)
        {
        }
        public static int GuardarPreguntas(MySqlConnection conexion, int id_pre, string resp, int id_user1, int id_pre2, string resp2, int id_pre3, string resp3)
        {
            int retorno = 0;
            MySqlCommand comando = new MySqlCommand(String.Format("INSERT INTO recuperaciones (id_pregunta1, respuesta, id_user1) VALUES ('{0}', '{1}', '{2}'), ('{3}', '{4}', '{2}'), ('{5}', '{6}', '{2}')", id_pre, resp, id_user1, id_pre2, resp2, id_pre3, resp3), conexion);
            retorno = comando.ExecuteNonQuery();
            return retorno;
        }

        public static List<Preguntas> ObtenerIDPreguntas(MySqlConnection conexion, int id_usuario)
        {

            List<Preguntas> listap = new List<Preguntas>();
            MySqlCommand comando = new MySqlCommand(String.Format("SELECT id_pregunta1, respuesta, id_user1, id_recuperacion FROM recuperaciones WHERE recuperaciones.id_user1='{0}'", id_usuario), conexion);
            MySqlDataReader reader = comando.ExecuteReader();
            while (reader.Read())
            {
                Preguntas p = new Preguntas();
                p.id_pregunta = reader.GetInt32(0);
                p.respuesta = reader.GetString(1);
                p.id_user1 = reader.GetInt32(2);
                p.id_recuperacion = reader.GetInt32(3);

                listap.Add(p);
            }
            return listap;
        }

        public static int ActualizarPreguntas(MySqlConnection conexion, Preguntas p)
        {
        
            int retorno = 0;
            MySqlCommand comando = new MySqlCommand(String.Format("UPDATE recuperaciones SET id_pregunta1='{1}', respuesta='{2}' WHERE id_user1='{0}' AND id_recuperacion='{3}'", p.id_user1, p.id_pregunta, p.respuesta, p.id_recuperacion), conexion);
            retorno = comando.ExecuteNonQuery();
            return retorno;
        }
        public static string ObtenerPregunta(MySqlConnection conexion, int id_pre)
        {
            string pregunta="";
        
            MySqlCommand comando = new MySqlCommand(String.Format("SELECT pregunta FROM preguntas WHERE id_pregunta='{0}'", id_pre), conexion);
            MySqlDataReader reader = comando.ExecuteReader();
            while (reader.Read())
            {
                pregunta = reader.GetString(0);
            }

            return pregunta;
        }
    }
}
