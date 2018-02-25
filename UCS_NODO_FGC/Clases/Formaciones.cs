using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace UCS_NODO_FGC.Clases
{
    public class Formaciones
    {
        public string bloque_curso { get; set; }
        public DateTime fecha_inicial { get; set; }

        public string TiempoEtapa { get; set; }
        public string solicitado { get; set; }

        public string duracion { get; set; }

        public string estatus { get; set; }

        public string nombre_formacion { get; set; }

        public string tipo_formacion { get; set; }
        public int etapa_curso { get; set; }
        //la etapa puede ser 1, 2 o 3, representan los paneles de cada form
        
        public DateTime dia1 { get; set; }
        public DateTime dia2 { get; set; }
        public int id_user { get; set; }

        public int id_curso { get; set; }
        public int pq_inst { get; set; }

        public static bool creacion { get; set; } //para saber si la formación se crea o se modifica
        public Formaciones()
        {

        }

        public Formaciones(DateTime fi, string sol, string d, string sta, string nomb, string tipo,  int pq, int idcurso, int id_user)
        {
            this.fecha_inicial = fi;
            this.solicitado = sol;
            this.duracion = d;
            this.estatus = sta;
            this.nombre_formacion = nomb;
            this.tipo_formacion = tipo;
            
            this.pq_inst = pq;
            this.id_curso = idcurso;
            this.id_user = id_user;
            

        }
       
        public static int AgregarNuevaFormacion(MySqlConnection conexion, Formaciones form)
        {
            int retorno = 0;
            string query = @"INSERT INTO cursos (estatus_curso, tipo_curso, duracion_curso, nombre_curso, fecha_creacion, id_usuario1, id_p_inst, bloque_curso, solicitud_curso, etapa_curso, duracionE1) VALUES (?estatus, ?tipo, ?duracion, ?nombre, ?fechainicio,?id_user, ?id_pq, ?bloque, ?solicitado, ?etapa, ?tiempoE1)";
            MySqlCommand cmd = new MySqlCommand(query, conexion);
            cmd.Parameters.AddWithValue("?estatus", form.estatus);
            cmd.Parameters.AddWithValue("?tipo", form.tipo_formacion);
            cmd.Parameters.AddWithValue("?duracion", form.duracion);
            cmd.Parameters.AddWithValue("?nombre", form.nombre_formacion);
           
            cmd.Parameters.AddWithValue("?fechainicio", form.fecha_inicial);
            //cmd.Parameters.AddWithValue("?fechafinal", form.fecha_final_formacion);
            cmd.Parameters.AddWithValue("?id_user", form.id_user);
            cmd.Parameters.AddWithValue("?id_pq", form.pq_inst);
            cmd.Parameters.AddWithValue("?bloque", form.bloque_curso);
            cmd.Parameters.AddWithValue("?solicitado", form.solicitado);
            cmd.Parameters.AddWithValue("?etapa", form.etapa_curso);
            cmd.Parameters.AddWithValue("?tiempoE1", form.TiempoEtapa);
            //el id debe venir de la persona logueada o sea de la clases.usuariologueado.id_usuario
            retorno = cmd.ExecuteNonQuery();
            return retorno;
        }

        public static int Agregar_U_g_C(MySqlConnection conexion, int id_curso, int id_usuario, DateTime fecha_inicial, DateTime fecha_final)
        {
            int retorno = 0;
            string query = @"INSERT INTO user_gestionan_cursos (cursos_id_cursos, usuarios_id_user, fecha_mod_inicio, fecha_mod_final) VALUES (?idcurso, ?idusuario, ?fechaInicio, ?fechaFinal)";
            MySqlCommand cmd = new MySqlCommand(query, conexion);
            cmd.Parameters.AddWithValue("?idcurso", id_curso);
            cmd.Parameters.AddWithValue("?idusuario", id_usuario);
            cmd.Parameters.AddWithValue("?fechaInicio", fecha_inicial);
            cmd.Parameters.AddWithValue("?fechaFinal", fecha_final);
            retorno = cmd.ExecuteNonQuery();
            return retorno;
        }
        public static int CursoEjecucionExiste(MySqlConnection conexion, Formaciones form)
        {
            int resultado = 0;
            MySqlCommand cmd = new MySqlCommand(String.Format("SELECT id_cursos FROM cursos WHERE nombre_curso = '{0}' AND tipo_curso='{1}' AND estatus_curso LIKE ('%{2}%')", form.nombre_formacion, form.tipo_formacion, form.estatus), conexion);
            MySqlDataReader leer = cmd.ExecuteReader();

            while (leer.Read())
            {
                resultado = leer.GetInt32(0);
               
            }

            return resultado;
        }

        //Funciona de javier
        public static bool ExisteCursoInCompany(String nombre_formacion, String cliente)
        {
            //MySqlDataReader leer = Conexion.ConsultarBD("SELECT id_p_inst FROM cursos WHERE nombre_curso = '" + form.nombre_formacion + "' AND tipo_curso='" + form.tipo_formacion + "' AND estatus_curso LIKE ('%" + status + "%')");

            return false;
        }

        public static int CursoOtroStatusExiste(Formaciones form, string status)
        {
            int resultado = 0;
            MySqlDataReader leer = Conexion.ConsultarBD("SELECT id_p_inst FROM cursos WHERE nombre_curso = '"+ form.nombre_formacion + "' AND tipo_curso='"+ form.tipo_formacion + "' AND estatus_curso LIKE ('%"+ status + "%')");
           
            while (leer.Read())
            {
                resultado = leer.GetInt32(0);
                
            }

            return resultado;
        }

        public static Paquete_instruccional obtenerTodoPq(MySqlConnection conexion, int id)
        {
            Paquete_instruccional p = new Paquete_instruccional();
            MySqlCommand cmd = new MySqlCommand(String.Format("SELECT p_presentacion, p_contenido, p_manual, p_bitacora FROM p_instruccional WHERE id_pinstruccional='{0}'", id),conexion);
            MySqlDataReader leer = cmd.ExecuteReader();

            while (leer.Read())
            {

               if(leer.GetString(0) == null)
                {
                    p.presentacion = "";
                }
                else
                {
                    p.presentacion = leer.GetString(0);
                   
                }
                
               if(leer.GetString(1) != null)
                {
                    p.contenido = leer.GetString(1);
                }else
                {
                    p.contenido = "";
                }

                if (leer.GetString(2) != null)
                {
                    p.manual = leer.GetString(2);
                }
                else
                {
                    p.manual = "";
                }

                if (leer.GetString(3) != null)
                {
                    p.bitacora = leer.GetString(3);
                }
                else
                {
                    p.bitacora = "";
                }

         

            }
            return p;
        }
        public static List<Paquete_instruccional> ObtenerPaqueteStatusCursoDistinto(MySqlConnection conexion, Formaciones form)
        {
            List<Paquete_instruccional> ListaP = new List<Paquete_instruccional>();
            MySqlCommand cmd = new MySqlCommand(String.Format("SELECT id_p_inst FROM cursos WHERE nombre_curso='{0}' AND tipo_curso='{1}' ", form.nombre_formacion, form.tipo_formacion), conexion);
            MySqlDataReader leer = cmd.ExecuteReader();
            while (leer.Read())
            {
                Paquete_instruccional p = new Paquete_instruccional();
                p.id_pinstruccional = leer.GetInt32(0);
                ListaP.Add(p);
            }
            return ListaP;
        }
        public static int ObtenerIdPaquete(MySqlConnection conexion, Paquete_instruccional pq)
        {
            int retorno = 0;
            string query = @"SELECT id_pinstruccional FROM p_instruccional WHERE p_contenido = ?contenido ";
            MySqlCommand cmd = new MySqlCommand(query, conexion);
            MySqlParameter contenido = new MySqlParameter("?contenido", MySqlDbType.VarChar);
            contenido.Value = pq.contenido;
            cmd.Parameters.Add(contenido);

            MySqlDataReader leer = cmd.ExecuteReader();

            while (leer.Read())
            {
                retorno = leer.GetInt32(0);

            }

            return retorno;
        }
        public static int GuardarPaqueteInstruccional(MySqlConnection conexion, Paquete_instruccional pq)
        {
            int retorno = 0;
            string query = @"INSERT INTO p_instruccional (p_presentacion, p_contenido, p_manual, p_bitacora) VALUES ( ?presentacion, ?contenido, ?manual, ?bitacora)";
            MySqlCommand cmd = new MySqlCommand(query, conexion);
            

            MySqlParameter presentacion = new MySqlParameter("?presentacion", MySqlDbType.VarChar);
            presentacion.Value = pq.presentacion;
            cmd.Parameters.Add(presentacion);

          

            MySqlParameter contenido = new MySqlParameter("?contenido", MySqlDbType.VarChar);
            contenido.Value = pq.contenido;
            cmd.Parameters.Add(contenido);

            MySqlParameter manual = new MySqlParameter("?manual", MySqlDbType.VarChar);
            manual.Value = pq.manual;
            cmd.Parameters.Add(manual);

            MySqlParameter bitacora = new MySqlParameter("?bitacora", MySqlDbType.VarChar);
            bitacora.Value = pq.bitacora;
            cmd.Parameters.Add(bitacora);

           

            retorno = cmd.ExecuteNonQuery();
            return retorno;

        }

        
    }

    public class Cursos
    {       
        public string nombre_formacion12 { get; set; }        
        public  int id_curso12 { get; set; }

        
        public static string solicitud_formacion13 { get; set; }
        public static string tipo_formacion13 { get; set; }
        public static string nombre_formacion13 { get; set; }
        public static string estatus_formacion13 { get; set; }
        public static int etapa_formacion13 { get; set; }
        public static string nombreCreador_formacion13 { get; set; }
        public static int id_curso13 { get; set; }
        public static int id_user13 { get; set; }



    }

   
}
