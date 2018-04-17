using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;

namespace UCS_NODO_FGC.Clases
{
  
    public class Usuarios
    {
        public static int ActualizarPreguntas { get; set; } 
        public int id_usuario { get; set; }
        public int cedula_user { get; set; }
        public string cargo_usuario { get; set; }
        public string nombre_usuario { get; set; }
        public string apellido_usuario { get; set; }
        public string password { get; set; }
        public string tlfn_usuario { get; set; }
        public string nacionalidad_usuario { get; set; }
        public string correo_usuario { get; set; }

        public byte[] imagen_usuario { get; set; }
        

        public Usuarios()
        {

        }

        public Usuarios(int Id_user, int cedula,string nacionalidad, string nombre, string apellido, string password, string tlfn_usuario, string cargo_usuario, string correo_usuario)
        {
            this.id_usuario = Id_user;
            this.cedula_user = cedula;
            this.nombre_usuario = nombre;
            this.apellido_usuario = apellido;
            this.password = password;
            this.tlfn_usuario = tlfn_usuario;
            this.cargo_usuario = cargo_usuario;
            this.correo_usuario = correo_usuario;
        }

        
         
        public static int AgregarUsuarios(MySqlConnection conexion, Usuarios usuario)
        {
            int retorno = 0;
            string query = @"INSERT INTO usuarios (cedula_user, nacionalidad_user, nombre_user, apellido_user, cargo_user, tlfn_user, pass_user, correo_user, imagen_user) VALUES (?ci, ?nacionalidad, ?nombre, ?apellido, ?cargo, ?tlfn, SHA1(?pass), ?correo, ?imagen)";


            MySqlCommand cmd = new MySqlCommand(query, conexion);

            cmd.Parameters.AddWithValue("?ci", usuario.cedula_user);
            cmd.Parameters.AddWithValue("?nacionalidad", usuario.nacionalidad_usuario);
            cmd.Parameters.AddWithValue("?nombre", usuario.nombre_usuario);
            cmd.Parameters.AddWithValue("?apellido", usuario.apellido_usuario);
            cmd.Parameters.AddWithValue("?cargo", usuario.cargo_usuario);
            cmd.Parameters.AddWithValue("?tlfn", usuario.tlfn_usuario);
            cmd.Parameters.AddWithValue("?pass", usuario.password);
            cmd.Parameters.AddWithValue("?correo", usuario.correo_usuario);

            MySqlParameter imageParam = new MySqlParameter("?imagen", MySqlDbType.MediumBlob);
            imageParam.Value = usuario.imagen_usuario;
            cmd.Parameters.Add(imageParam);

            retorno = cmd.ExecuteNonQuery();
            return retorno;
             
        }

        public static int ActualizarUsuarios(MySqlConnection conexion, Usuarios usuario)
         {
            int retorno = 0;

            MySqlCommand comando = new MySqlCommand(String.Format("UPDATE usuarios SET  nombre_user='{1}', apellido_user='{2}', tlfn_user='{3}', pass_user=SHA1('{4}'), correo_user='{5}', cargo_user='{6}', cedula_user='{7}'  WHERE id_user='{0}' ", usuario.id_usuario, usuario.nombre_usuario, usuario.apellido_usuario, usuario.tlfn_usuario, usuario.password, usuario.correo_usuario, usuario.cargo_usuario, usuario.cedula_user), conexion);
            retorno = comando.ExecuteNonQuery();
            return retorno;
         }

        public static int ActualizarContraseña(MySqlConnection conexion, int id_user, string pass)
        {
            int retorno = 0;

            MySqlCommand comando = new MySqlCommand(String.Format("UPDATE usuarios SET pass_user=SHA1('{1}') WHERE id_user='{0}' ", id_user, pass), conexion);
            retorno = comando.ExecuteNonQuery();
            return retorno;
        }
        public static int ActualizarFotoUsuario(MySqlConnection conexion, Usuarios usuario)
        {
            int retorno = 0;
            string query = @"UPDATE usuarios SET imagen_user = ?foto WHERE id_user = ?id";
            MySqlCommand comando = new MySqlCommand(query, conexion);
            comando.Parameters.AddWithValue("?id", usuario.id_usuario);
            MySqlParameter imageParam = new MySqlParameter("?foto", MySqlDbType.MediumBlob);
            imageParam.Value = usuario.imagen_usuario;
            comando.Parameters.Add(imageParam);
            
            retorno = comando.ExecuteNonQuery();
            return retorno;
        }
        public static int EliminarUsuarios(MySqlConnection conexion, int ci_usuario, string nacionalidad)
        {
            //modifica la cedula del usuario pero no lo elimina. así se quedan los registros y el usuario "eliminado" no puede acceder a su cuenta
            int retorno = 0;
            string query = @"UPDATE usuarios SET cedula_user= ?novaCi,  pass_user=SHA1(?novaPass) WHERE cedula_user= ?ci AND nacionalidad_user= ?nac";
            MySqlCommand comando = new MySqlCommand(query, conexion);
            comando.Parameters.AddWithValue("?ci", ci_usuario);
            comando.Parameters.AddWithValue("?nac", nacionalidad);
            comando.Parameters.AddWithValue("?novaCi", 0);
            comando.Parameters.AddWithValue("?novaPass", "UsuarioEliminado");
            retorno = comando.ExecuteNonQuery();
            return retorno;
        }

        public static int lideresexisten(MySqlConnection conexion, string cargo_usuario)
        {
            int retorno = 0;
            
            MySqlCommand comando = new MySqlCommand(String.Format("SELECT id_user FROM usuarios WHERE cargo_user LIKE ('%{0}%')", cargo_usuario), conexion);
            MySqlDataReader leer = comando.ExecuteReader();
            while (leer.Read())
            {
                retorno = leer.GetInt32(0);
            }
            return retorno;
        }

        public static int UsuarioExiste(MySqlConnection conexion, int ci_usuario)
        {
            int retorno = 0;
            MySqlCommand comando = new MySqlCommand(String.Format("SELECT id_user FROM usuarios WHERE cedula_user LIKE ('%{0}%')", ci_usuario), conexion);
            MySqlDataReader leer = comando.ExecuteReader();

            while (leer.Read())
            {
                
                retorno = retorno + 1;

            }
            return retorno;
        }
        public static Usuarios IniciarSesion(MySqlConnection conexion, Usuarios usuario)
        {
            Usuarios usuarioingresado = new Usuarios();
            MySqlCommand comando = new MySqlCommand(String.Format("SELECT nacionalidad_user, nombre_user, apellido_user, cargo_user, tlfn_user, correo_user, id_user, imagen_user FROM usuarios WHERE cedula_user LIKE ('%{0}%') AND pass_user=SHA1('{1}')", usuario.cedula_user, usuario.password), conexion);
            MySqlDataReader leer = comando.ExecuteReader();

            while (leer.Read())
            {
                usuarioingresado.nacionalidad_usuario= leer.GetString(0);
                usuarioingresado.nombre_usuario = leer.GetString(1);
                usuarioingresado.apellido_usuario = leer.GetString(2);
                usuarioingresado.cargo_usuario = leer.GetString(3);
                usuarioingresado.tlfn_usuario = leer.GetString(4);
                usuarioingresado.correo_usuario = leer.GetString(5);
                usuarioingresado.id_usuario = leer.GetInt32(6);

                if(leer["imagen_user"] != DBNull.Value)
                {
                    usuarioingresado.imagen_usuario = (byte[])leer["imagen_user"];
                }else
                {
                    usuarioingresado.imagen_usuario = Helper.ImageToByteArray(Properties.Resources.img_perfil);
                }

            }
            return usuarioingresado;

        }
        public static Usuarios obtenerUsuario(MySqlConnection conexion, int idusuario, string nacionalidad)
        {
            Usuarios usuarioIn = new Usuarios();
            MySqlCommand comando = new MySqlCommand(String.Format("SELECT id_user, nacionalidad_user, nombre_user, apellido_user, cargo_user, tlfn_user, pass_user, correo_user FROM usuarios WHERE cedula_user LIKE ('%{0}%') AND nacionalidad_user LIKE ('%{1}%')", idusuario, nacionalidad), conexion);
            MySqlDataReader leer = comando.ExecuteReader();

            while (leer.Read())
            {
                usuarioIn.id_usuario = leer.GetInt32(0);
                usuarioIn.nacionalidad_usuario = leer.GetString(1);
                usuarioIn.nombre_usuario = leer.GetString(2);
                usuarioIn.apellido_usuario = leer.GetString(3);
                usuarioIn.cargo_usuario = leer.GetString(4);
                usuarioIn.tlfn_usuario = leer.GetString(5);
                usuarioIn.password = leer.GetString(6);
                usuarioIn.correo_usuario = leer.GetString(7);
            }

            return usuarioIn;

        }

       public static int retornaFilas(MySqlConnection conexion)
        {
            int retorno = 0;
            MySqlCommand comando = new MySqlCommand(String.Format("SELECT * FROM usuarios "),conexion);
            MySqlDataReader leer = comando.ExecuteReader();

            while (leer.Read())
            {
                retorno = retorno + 1;
            }
                return retorno;
        }

        public static int BuscarCorreoUsuario(MySqlConnection conexion, Usuarios us)
        {

            int retorno = 0;


            Usuarios usuario_bc = new Usuarios();
            MySqlCommand comando = new MySqlCommand(String.Format("SELECT id_user FROM usuarios WHERE correo_user LIKE ('%{0}%') AND cedula_user= '{1}' AND nombre_user LIKE ('%{2}%')", us.correo_usuario, us.cedula_user, us.nombre_usuario), conexion);
            MySqlDataReader leer = comando.ExecuteReader();

            while (leer.Read())
            {
                retorno = leer.GetInt32(0);
            }

            return retorno;

        }
        public static int CambiarContraseña(MySqlConnection conexion, String new_password, int id_usuario)
        {

            int retorno = 0;
            Usuarios usuario_bc = new Usuarios();
            MySqlCommand comando = new MySqlCommand(String.Format("UPDATE usuarios SET pass_user=SHA1('{0}')  WHERE id_user='{1}' ", new_password, id_usuario), conexion);
            retorno = comando.ExecuteNonQuery();
            return retorno;

        }


    }
}
