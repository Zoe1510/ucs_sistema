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
    public class Facilitadores
    {
        public int id_facilitador { get; set; }
        public string ci_facilitador { get; set; }
        public string nombre_facilitador { get; set; }
        public string apellido_facilitador { get; set; }
        public string especialidad_facilitador { get; set; }
        public string ubicacion_facilitador { get; set; }
        public string tlfn_facilitador { get; set; }
        public string correo_facilitador { get; set; }
        public string nacionalidad_fa { get; set; }
        public string nombreyapellido { get; set; }
        
        public int requerimiento_ince { get; set; }
        public Facilitadores()
        {

        }

        public Facilitadores(string ci, string nombre, string apellido,string nyA, string especialidad, string ubicacion, string tlfn, string correo, int reque, string nac)
        {
            this.ci_facilitador = ci;
            this.nombre_facilitador = nombre;
            this.apellido_facilitador = apellido;
            this.especialidad_facilitador = especialidad;
            this.ubicacion_facilitador = ubicacion;
            this.tlfn_facilitador = tlfn;
            this.correo_facilitador = correo;
            this.requerimiento_ince = reque;
            this.nombreyapellido = nyA;
            this.nacionalidad_fa = nac;
            
        }

        public static int AgregarFacilitador(MySqlConnection conexion, Facilitadores facilitador)
        {
            int retorno = 0;
            MySqlCommand comando = new MySqlCommand(String.Format("INSERT INTO facilitadores ( cedula_fa, nacionalidad_fa, nombre_fa, apellido_fa, tlfn_fa, correo_fa, ubicacion_fa, especialidad_fa, requerimiento_inces) VALUES ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}')", facilitador.ci_facilitador, facilitador.nacionalidad_fa, facilitador.nombre_facilitador,facilitador.apellido_facilitador, facilitador.tlfn_facilitador, facilitador.correo_facilitador, facilitador.ubicacion_facilitador, facilitador.especialidad_facilitador, facilitador.requerimiento_ince), conexion);
            retorno = comando.ExecuteNonQuery();
            return retorno;
        }

        public static int FacilitadorExiste(MySqlConnection conexion, string ci_facilitador)
        {
            int retorno = 0;
            MySqlCommand comando = new MySqlCommand(String.Format("SELECT id_fa FROM facilitadores WHERE cedula_fa LIKE ('%{0}%') ", ci_facilitador), conexion);
            MySqlDataReader leer = comando.ExecuteReader();

            while (leer.Read())
            {

                retorno =leer.GetInt32(0);

            }
            return retorno;
        }

        public static Facilitadores SeleccionarFa(MySqlConnection conexion, Facilitadores fa)
        {
            Facilitadores facilitador = new Facilitadores();
            MySqlCommand comando = new MySqlCommand(String.Format("SELECT id_fa, cedula_fa, nacionalidad_fa, nombre_fa, apellido_fa, tlfn_fa, correo_fa, ubicacion_fa, especialidad_fa, requerimiento_inces FROM facilitadores WHERE cedula_fa LIKE ('%{0}%') AND nacionalidad_fa LIKE ('%{1}%')", fa.ci_facilitador, fa.nacionalidad_fa), conexion);
            MySqlDataReader reader = comando.ExecuteReader();
            while (reader.Read())
            {
                facilitador.id_facilitador = reader.GetInt32(0);
                facilitador.ci_facilitador = reader.GetString(1);
                facilitador.nacionalidad_fa = reader.GetString(2);
                facilitador.nombre_facilitador = reader.GetString(3);
                facilitador.apellido_facilitador = reader.GetString(4);
                facilitador.tlfn_facilitador = reader.GetString(5);
                facilitador.correo_facilitador = reader.GetString(6);
                facilitador.ubicacion_facilitador = reader.GetString(7);
                facilitador.especialidad_facilitador = reader.GetString(8);
                facilitador.requerimiento_ince = reader.GetInt32(9);
            }
            return facilitador;
        }

        public static int EliminarFa(MySqlConnection conexion, string id_fa)
        {
            int retorno = 0;
            MySqlCommand comando = new MySqlCommand(String.Format("DELETE FROM facilitadores WHERE cedula_fa='{0}'", id_fa), conexion);
            retorno = comando.ExecuteNonQuery();
            return retorno;
        }

        public static int ActualizarFa (MySqlConnection conexion, Facilitadores fa)
        {
            int retorno = 0;
            MySqlCommand comando = new MySqlCommand(String.Format("UPDATE facilitadores SET  nombre_fa='{1}', apellido_fa='{2}', tlfn_fa='{3}', correo_fa='{4}', ubicacion_fa='{5}', especialidad_fa='{6}', cedula_fa='{7}', nacionalidad_fa='{8}', requerimiento_inces='{9}'  WHERE id_fa='{0}' ", fa.id_facilitador, fa.nombre_facilitador, fa.apellido_facilitador, fa.tlfn_facilitador, fa.correo_facilitador, fa.ubicacion_facilitador, fa.especialidad_facilitador, fa.ci_facilitador, fa.nacionalidad_fa, fa.requerimiento_ince), conexion);
            retorno = comando.ExecuteNonQuery();
            return retorno;
        }

    }
}
