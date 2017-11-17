using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace UCS_NODO_FGC.Clases
{
    public class INCES
    {
        public int id_cursoINCE { get; set; }
        public string nombre_cursoINCE { get; set; }

        public static int id_curso{ get; set; }
        public static int id_fa { get; set; }
        public static string nombre_curso { get; set; }
        public INCES()
        {

        }

        public INCES(int id, string nombre)
        {
            this.id_cursoINCE = id;
            this.nombre_cursoINCE = nombre;
        }

        public static int AgregarCurso(MySqlConnection conexion, INCES cur)
        {
            int retorno = 0;
            MySqlCommand comando = new MySqlCommand(String.Format("INSERT INTO cursos_inces (nombre_curso_ince) VALUES ('{0}')", cur.nombre_cursoINCE), conexion);
            retorno = comando.ExecuteNonQuery();
            return retorno;
        }

        public static int AgregarAsignacion(MySqlConnection conexion,  int id_fa, int id_curso)
        {
            int retorno = 0;
            MySqlCommand comando = new MySqlCommand(String.Format("INSERT INTO inces_tiene_facilitadores (id_fa_INCE, id_curso_INCE) VALUES ('{0}', '{1}')", id_fa, id_curso), conexion);
            retorno = comando.ExecuteNonQuery();
            return retorno;

        }

        public static int AsignacionExiste(MySqlConnection conexion, int id_curso, int fa)
        {
            int retorno = 0;
            MySqlCommand comando = new MySqlCommand(String.Format("SELECT id_fa_INCE FROM inces_tiene_facilitadores WHERE id_curso_INCE='{0}' AND id_fa_INCE='{1}'", id_curso, fa), conexion);
            MySqlDataReader leer = comando.ExecuteReader();

            while (leer.Read())
            {
                retorno = leer.GetInt32(0);


            }
            return retorno;

        }

        public static int CursoExiste(MySqlConnection conexion, string nombre_cursoINCE)
        {
            int retorno = 0;
            MySqlCommand comando = new MySqlCommand(String.Format("SELECT id_curso_ince FROM cursos_inces WHERE nombre_curso_ince ='{0}'", nombre_cursoINCE), conexion);
            MySqlDataReader leer = comando.ExecuteReader();

            while (leer.Read())
            {
                retorno = leer.GetInt32(0);


            }
            return retorno;

        }

        public static string SeleccionarNombreCurso(MySqlConnection conexion, int id_ince)
        {
            string nombre = "";
            MySqlCommand comando = new MySqlCommand(String.Format("SELECT nombre_curso_ince FROM cursos_inces WHERE id_curso_ince='{0}'", id_ince), conexion);
            MySqlDataReader leer = comando.ExecuteReader();

            while (leer.Read())
            {
                nombre = leer.GetString(0);


            }
            return nombre;
        }

        public static int ActualizarCurso(MySqlConnection conexion, INCES cur)
        {
            int retorno = 0;
            MySqlCommand comando = new MySqlCommand(String.Format("UPDATE cursos_inces SET nombre_curso_ince='{1}' WHERE id_curso_ince='{0}' ", cur.id_cursoINCE, cur.nombre_cursoINCE), conexion);
            retorno = comando.ExecuteNonQuery();
            return retorno;
        }

        public static int EliminarCurso(MySqlConnection conexion, int id_cur)
        {
            int retorno = 0;

            MySqlCommand comando = new MySqlCommand(String.Format("DELETE FROM cursos_inces WHERE id_curso_ince='{0}' ", id_cur), conexion);
            retorno = comando.ExecuteNonQuery();
            return retorno;
        }

        public static int EliminarTodasAsignaciones(MySqlConnection conexion, int id_curs)
        {
            int retorno = 0;

            MySqlCommand comando = new MySqlCommand(String.Format("DELETE FROM inces_tiene_facilitadores WHERE id_curso_INCE ='{0}' ", id_curs), conexion);
            retorno = comando.ExecuteNonQuery();
            return retorno;

        }

        public static int EliminarAsignacion(MySqlConnection conexion, int id_curso, int id_fa)
        {
            int retorno = 0;

            MySqlCommand comando = new MySqlCommand(String.Format("DELETE FROM inces_tiene_facilitadores WHERE id_fa_INCE='{0}' AND id_curso_INCE ='{1}' ", id_fa, id_curso), conexion);
            retorno = comando.ExecuteNonQuery();
            return retorno;

        }
    }
}
