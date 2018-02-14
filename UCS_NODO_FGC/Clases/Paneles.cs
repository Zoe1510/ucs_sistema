using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Text.RegularExpressions; //para la comprobacion del email

namespace UCS_NODO_FGC.Clases
{
    public class Paneles
    {

        public static void boton_cerrar()
        {
            if (MessageBox.Show("¿Desea salir?", "",
         MessageBoxButtons.YesNo, MessageBoxIcon.Question)
         == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        public static void sololetras(KeyPressEventArgs e)
        {
            if (Char.IsLetter(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (Char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (Char.IsSeparator(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

      
        public static void solonumeros(KeyPressEventArgs e)
        {
            //metodo usado para la validacion de solo numeros en el campo cedula del login
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (Char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (Char.IsSeparator(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {

                e.Handled = true;

            }
        }

        public static bool ComprobarFormatoEmail(string correo)
        {
            String sFormato;
            sFormato = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
            if (Regex.IsMatch(correo, sFormato))
            {
                if (Regex.Replace(correo, sFormato, String.Empty).Length == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public static bool comprobarFormatoTlfn(CancelEventArgs e, string tlfn)
        {
            //(0){1}[4,2][0-9]{2}-[0-9]{3}-[0-9]{4} //expresion regular que acepta 0426-000-0000 o 0274-000-0000
            Regex rex = new Regex("(04)[0-9]{9,9}$");//puede ser void en panel
            Regex rex2 = new Regex("(02)[0-9]{9,9}$");
            if (!rex.IsMatch(tlfn) && !rex2.IsMatch(tlfn))
            {
                //MessageBox.Show("Formato de teléfono inválido: debe proporcionar un número válido para continuar con el registro.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Cancel = true;
                return false;
            }else
            {
                return true;
            }
        }

        public static bool comprobarCedula(string ci)
        {
            string formatoCi;
            formatoCi = "(8)[0-9]{7,7}$";
            //string formatoci2 = "(9)[0-9]{7,7}$";
            if (Regex.IsMatch(ci, formatoCi) )
            {
                if (Regex.Replace(ci, formatoCi, String.Empty).Length == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

       
        public static List<Empresa> LlenarCombobox(string nombre)//funciona
        {
            conexion_bd con = new conexion_bd();
            List<Empresa> lista = new List<Empresa>();
            if (con.abrirconexion() == true)
            {
                MySqlCommand comando = new MySqlCommand(String.Format("SELECT id_clientes, nombre_empresa, fee_empresa FROM clientes  WHERE nombre_empresa LIKE ('%{0}%')", nombre), con.conexion);
                MySqlDataReader leer = comando.ExecuteReader();
                while (leer.Read())
                {

                    lista.Add(cli(leer));
                }

            }


            con.cerrarconexion();
            return lista;

        }//funciona
       
        public static List<Preguntas> LlenarComboboxPreguntas(string pregunta)
        {
            conexion_bd con1 = new conexion_bd();
            List<Preguntas> lista = new List<Preguntas>();
            if (con1.abrirconexion() == true)
            {
                MySqlCommand comando = new MySqlCommand(String.Format("SELECT id_pregunta, pregunta FROM preguntas WHERE pregunta LIKE ('%{0}%')", pregunta), con1.conexion);
                MySqlDataReader leer = comando.ExecuteReader();
                while (leer.Read())
                {

                    lista.Add(pre(leer));
                }

            }


            con1.cerrarconexion();
            return lista;

        }//funciona

        
        public static List<Curso_IN> LlenarComboboxCursos(string buscar)//no funciona, muestra la ruta de la clase cursos.inces
        {
            conexion_bd con2 = new conexion_bd();
            List<Curso_IN> lista = new List<Curso_IN>();
            if (con2.abrirconexion() == true)
            {
                MySqlCommand comando = new MySqlCommand(String.Format("SELECT * FROM cursos_inces WHERE nombre_curso_ince LIKE ('%{0}%')", buscar), con2.conexion);
                MySqlDataReader leer = comando.ExecuteReader();
                while (leer.Read())
                {

                    lista.Add(cur(leer));
                }
            }
            con2.cerrarconexion();
            return lista;
        }
        
        public static List<Fa_ince> LlenarComboboxFacilitadoresINCEs()
        {
            conexion_bd con3 = new conexion_bd();
            List<Fa_ince> lista = new List<Fa_ince>();
            if (con3.abrirconexion() == true)
            {
                MySqlCommand comando = new MySqlCommand(String.Format("SELECT id_fa, cedula_fa, nombre_fa, apellido_fa FROM facilitadores  WHERE requerimiento_inces = 1"), con3.conexion);
                MySqlDataReader leer = comando.ExecuteReader();
                while (leer.Read())
                {

                    lista.Add(fa(leer));
                }

            }


            con3.cerrarconexion();
            return lista;

        }

        
        public static List<Facilitador_todos> LlenarCmbxFaTodos()
        {
            conexion_bd con2 = new conexion_bd();
            List<Facilitador_todos> lista = new List<Facilitador_todos>();
            if (con2.abrirconexion() == true)
            {
                MySqlCommand comando = new MySqlCommand(String.Format("SELECT * FROM facilitadores "), con2.conexion);
                MySqlDataReader leer = comando.ExecuteReader();
                while (leer.Read())
                {

                    lista.Add(fa_todos(leer));
                }
            }
            return lista;
        }
        public static List<Facilitador_todos> LlenarCmbxCoFa(int id_fa)
        {
            conexion_bd con2 = new conexion_bd();
            List<Facilitador_todos> lista = new List<Facilitador_todos>();
            if (con2.abrirconexion() == true)
            {
                MySqlCommand comando = new MySqlCommand(String.Format("SELECT * FROM facilitadores  WHERE id_fa != '{0}' ", id_fa), con2.conexion);
                MySqlDataReader leer = comando.ExecuteReader();
                while (leer.Read())
                {

                    lista.Add(fa_todos(leer));
                }
            }
            return lista;
        }
        public static List<Refrigerios> llenarcmbx2Ref(int id_ref)
        {
            conexion_bd con2 = new conexion_bd();
            List<Refrigerios> lista = new List<Refrigerios>();
            if (con2.abrirconexion() == true)
            {
                MySqlCommand comando = new MySqlCommand(String.Format("SELECT * FROM refrigerios WHERE id_ref != '{0}'", id_ref), con2.conexion);
                MySqlDataReader leer = comando.ExecuteReader();
                while (leer.Read())
                {

                    lista.Add(refrig(leer));
                }
            }
            return lista;


        }
        public static List<Cursos> llenarCmbxCursos()
        {
            List<Cursos> lista = new List<Cursos>();
            MySqlDataReader cursos = Conexion.ConsultarBD("SELECT * FROM cursos");
            while (cursos.Read())
            {
                Cursos c = new Cursos();
                c.nombre_formacion12 = Convert.ToString(cursos["nombre_curso"]);
                c.id_curso12 = Convert.ToInt32(cursos["id_cursos"]);
                lista.Add(c);
                
                
            }
            return lista;
        }

        public static Facilitador_todos fa_todos(MySqlDataReader reader)
        {
            Facilitador_todos fa1 = new Facilitador_todos();
            fa1.id_faci = Convert.ToInt32(reader["id_fa"]);
            fa1.ci_facilitador = Convert.ToString(reader["cedula_fa"]);
            string nombre = Convert.ToString(reader["nombre_fa"]);
            string apellido = Convert.ToString(reader["apellido_fa"]);
            fa1.nombreyapellido1 = nombre + " " + apellido;
            return fa1;
        }
        public static Fa_ince fa(MySqlDataReader reader)
        {
            Fa_ince faci = new Fa_ince();
            faci.id_facilitador=Convert.ToInt32(reader["id_fa"]);
            faci.ci_facilitador= Convert.ToString(reader["cedula_fa"]);
            string nombre= Convert.ToString(reader["nombre_fa"]);
            string apellido = Convert.ToString(reader["apellido_fa"]);
            faci.nombreyapellido = nombre+" "+apellido;
            return faci;
        }
        public static Curso_IN cur(MySqlDataReader reader)
        {
            Curso_IN curso = new Curso_IN();
            curso.id_INCE= Convert.ToInt32(reader["id_curso_ince"]);
            curso.nombre_INCE= Convert.ToString(reader["nombre_curso_ince"]);
            return curso;
        }
        public static Preguntas pre(MySqlDataReader reader)
        {
            Preguntas pregunta = new Preguntas();
            pregunta.id_pregunta = Convert.ToInt32(reader["id_pregunta"]);
            pregunta.pregunta = Convert.ToString(reader["pregunta"]);

            return pregunta;
        }
        public static Empresa cli(MySqlDataReader reader)
        {
            Empresa cliente = new Empresa();
            cliente.id_clientes = Convert.ToInt32(reader["id_clientes"]);
            cliente.nombre_empresa = Convert.ToString(reader["nombre_empresa"]);
            cliente.fee = Convert.ToInt32(reader["fee_empresa"]);

            return cliente;
        }
        public static List<Refrigerios> llenarcmbxRef()
        {
            conexion_bd con2 = new conexion_bd();
            List<Refrigerios> lista = new List<Refrigerios>();
            if (con2.abrirconexion() == true)
            {
                MySqlCommand comando = new MySqlCommand(String.Format("SELECT * FROM refrigerios"), con2.conexion);
                MySqlDataReader leer = comando.ExecuteReader();
                while (leer.Read())
                {

                    lista.Add(refrig(leer));
                }
            }
            return lista;
            
           
        }
        public static Refrigerios refrig (MySqlDataReader reader)
        {
            Refrigerios refrigerios = new Refrigerios();
            refrigerios.id_ref = Convert.ToInt32(reader["id_ref"]);
            refrigerios.nombre = Convert.ToString(reader["ref_nombre"]);
            return refrigerios;
        }
        public static int ModificarEmpresa(MySqlConnection conexion, Empresa empresa)
        {
            int resultado = 0;
            MySqlCommand comando = new MySqlCommand(String.Format("UPDATE clientes SET nombre_empresa='{1}', fee_empresa='{2}' WHERE id_clientes='{0}' ", empresa.id_clientes, empresa.nombre_empresa, empresa.fee), conexion);
            resultado = comando.ExecuteNonQuery();
            return resultado;
        }
        public static Clientes AreaExiste(MySqlConnection conexion, Clientes cliente)
        {
            Clientes area = new Clientes();
            MySqlCommand comando = new MySqlCommand(String.Format("SELECT id_area FROM areas WHERE id_cliente1='{0}'", cliente.id_cliente), conexion);
            MySqlDataReader leer = comando.ExecuteReader();

            while (leer.Read())
            {
                area.id_area = leer.GetInt32(0);


            }
            return area;
        }

        public static void VaciarClienteSeleccionado()
        {
            Cliente_seleccionado.id_cliente = 0;
            Cliente_seleccionado.id_area = 0;
            Cliente_seleccionado.correo_cliente = "";
            Cliente_seleccionado.fee_empresa = 2;//2no devuelve ningun valor
            Cliente_seleccionado.nombre_areaEmpresa = "";
            Cliente_seleccionado.nombre_contacto = "";
            Cliente_seleccionado.nombre_empresa = "";
            Cliente_seleccionado.tlfn_cliente = "";
        }
    }
}
