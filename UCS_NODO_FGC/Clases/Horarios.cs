using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Text.RegularExpressions; //para la comprobacion del email


namespace UCS_NODO_FGC.Clases
{
    public class Horarios
    {
        public int id_horario { get; set; }
        public string contenido_horario { get; set; }

        public Horarios()
        {

        }

        public Horarios(int id, string cont )
        {
            this.id_horario = id;
            this.contenido_horario = cont;
        }

        public static List<Horarios> llenarcmbxHorario(string tipo)
        {
            conexion_bd con = new conexion_bd();
            List<Horarios> lista = new List<Horarios>();
            con.cerrarconexion();
            if (con.abrirconexion() == true)
            {
                MySqlCommand comando = new MySqlCommand(String.Format("SELECT idhorarios, horario FROM horarios WHERE tipo_horario='{0}'", tipo), con.conexion);
                MySqlDataReader leer = comando.ExecuteReader();
                while (leer.Read())
                {
                    Horarios h = new Horarios();
                    h.id_horario= Convert.ToInt32(leer["idhorarios"]);
                    h.contenido_horario= Convert.ToString(leer["horario"]);
                    lista.Add(h);
                }
                
            }
            con.cerrarconexion();
            return lista;

        }

        public static List<Horarios> llenarcmbxHorario2(string tipo, int id)
        {
            conexion_bd con = new conexion_bd();
            List<Horarios> lista = new List<Horarios>();
            con.cerrarconexion();
            if (con.abrirconexion() == true)
            {
                MySqlCommand comando = new MySqlCommand(String.Format("SELECT idhorarios, horario FROM horarios WHERE tipo_horario='{0}' AND idhorarios !='{1}' ",tipo, id), con.conexion);
                MySqlDataReader leer = comando.ExecuteReader();
                while (leer.Read())
                {
                    Horarios h = new Horarios();
                    h.id_horario = Convert.ToInt32(leer["idhorarios"]);
                    h.contenido_horario = Convert.ToString(leer["horario"]);
                    lista.Add(h);
                }

            }
            con.cerrarconexion();
            return lista;

        }
    }
}
