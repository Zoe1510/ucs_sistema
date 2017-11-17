using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UCS_NODO_FGC
{
    public partial class Inicio_Sesion : Form
    {
        public Clases.Usuarios usuario = new Clases.Usuarios();
        public Clases.Usuarios usuarioIngresado { get; set; }
        public Clases.conexion_bd conexion = new Clases.conexion_bd();
        public int id_usuario { get; set; }
        public string nombre_usuario { get; set; }
        public string apellido_usuario;
        public string cargo_usuario;
        public string tlfn_usuario;

        public Inicio_Sesion()
        {
            InitializeComponent();
        }
        private void Inicio_de_sesion_Load(object sender, EventArgs e)
        {
            Txt_id_user.Focus();
            Txt_id_user.SelectionStart = Txt_id_user.Text.Length;
        }
        private void Txt_id_user_Click(object sender, EventArgs e)
        {
            //metodo usado para el evento click sobre el textbox cedula
            if(Txt_id_user.Text != "")
            {

            }else
            {
                Txt_id_user.Text = "";
            }
            
        }

        private void Txt_pass_user_Click(object sender, EventArgs e)
        {

            //metodo usado para el evento click sobre el textbox de la contraseña

            Txt_pass_user.UseSystemPasswordChar = true;
            Txt_pass_user.Text = "";

        }

        private void Txt_id_user_KeyPress(object sender, KeyPressEventArgs e)
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

        private void btn_login_Click(object sender, EventArgs e)
        {
            login();
        }

      
        public void login ()
        {
            try
            {
                if (conexion.abrirconexion() == true)
                {
                    if ((Txt_id_user.Text != "") && (Txt_pass_user.Text != ""))
                    {
                        if ((Txt_id_user.Text != "Cédula") && (Txt_pass_user.Text != "Contraseña"))
                        {
                            

                            usuario.cedula_user = Convert.ToInt32(Txt_id_user.Text);
                            usuario.password = Txt_pass_user.Text;
                            int ci_existe = Clases.Usuarios.UsuarioExiste(conexion.conexion, usuario.cedula_user);
                            conexion.cerrarconexion();

                            if(ci_existe > 0 )//si la cedula introducida existe en el registro de usuarios
                            {
                                if (conexion.abrirconexion() == true)
                                {
                                    usuarioIngresado = Clases.Usuarios.IniciarSesion(conexion.conexion, usuario);
                                    conexion.cerrarconexion();

                                    if (usuarioIngresado.id_usuario != 0)//si la contraseña corresponde al id:
                                    {
                                        Clases.Usuario_logeado.cedula_user = usuario.cedula_user;
                                        Clases.Usuario_logeado.nombre_usuario = usuarioIngresado.nombre_usuario;
                                        Clases.Usuario_logeado.apellido_usuario = usuarioIngresado.apellido_usuario;
                                        Clases.Usuario_logeado.id_usuario = usuarioIngresado.id_usuario;
                                        Clases.Usuario_logeado.tlfn_usuario = usuarioIngresado.tlfn_usuario;
                                        Clases.Usuario_logeado.cargo_usuario = usuarioIngresado.cargo_usuario;
                                        Clases.Usuario_logeado.password = usuario.password;
                                        Clases.Usuario_logeado.correo_usuario = usuarioIngresado.correo_usuario;
                                        Clases.Usuario_logeado.imagen_usuario = usuarioIngresado.imagen_usuario;
                                        Txt_id_user.Clear();
                                        Txt_pass_user.Clear();

                                        this.Visible = false; 
                                        //Si el usuario es lider: 
                                        if (Clases.Usuario_logeado.cargo_usuario == "Lider")
                                        {
                                            Pagina_principal frm_main_Lider = new Pagina_principal();
                                            frm_main_Lider.Show();

                                        }
                                        else if (Clases.Usuario_logeado.cargo_usuario == "Coordinador")  //Si el usuario es coordinador
                                        {
                                            Pagina_principal_Colaborador frm_main_colaborador = new Pagina_principal_Colaborador();
                                            frm_main_colaborador.Show();
                                        }
                                        else if (Clases.Usuario_logeado.cargo_usuario == "Asistente")  //Si el usuario es asistente 
                                        {
                                            Pagina_principal_Asistente frm_main_Asistente = new Pagina_principal_Asistente();
                                            frm_main_Asistente.Show();
                                        }

                                    }
                                    else//si las contraseñas no coinciden
                                    {
                                        conexion.cerrarconexion();
                                        MessageBox.Show("Contraseña incorrecta, inténtelo nuevamente.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        Txt_pass_user.UseSystemPasswordChar = true;
                                        Txt_pass_user.Text = "";
                                        Txt_pass_user.Focus();
                                        
                                        
                                    }
                                    conexion.cerrarconexion();
                                }

                            }
                            else //si no existe en la base de datos
                            {
                                conexion.cerrarconexion();
                                MessageBox.Show("Usted no se encuentra registrado en la base de datos del sistema.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                Txt_id_user.Text = "Cédula";
                                Txt_id_user.Focus();
                                Txt_pass_user.UseSystemPasswordChar = false;
                                Txt_pass_user.Text = "Contraseña";
                            }
                            
                        }
                        else
                        {
                            MessageBox.Show("Debe intruducir todos sus datos", "¡Aviso!");
                        }

                    }
                    else if ((Txt_id_user.Text == "") || (Txt_pass_user.Text == ""))
                    {
                        MessageBox.Show("Debe intruducir todos sus datos", "¡Aviso!");
                    }

                    conexion.cerrarconexion();
                }
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                conexion.cerrarconexion();
            }
        }
        private void btn_cerrar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Desea salir?", "",
         MessageBoxButtons.YesNo, MessageBoxIcon.Question)
         == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void btn_minimizar_Click(object sender, EventArgs e)
        {
           // this.WindowState = FormWindowState.Maximized;
            this.WindowState= FormWindowState.Minimized;
            //this.WindowState= FormWindowState.Normal;
        }

        private void Txt_pass_user_Enter(object sender, EventArgs e)
        {
            // metodo usado para el evento click sobre el textbox de la contraseña

            Txt_pass_user.UseSystemPasswordChar = true;
            Txt_pass_user.Text = "";
        }

        private void Txt_id_user_Enter(object sender, EventArgs e)
        {
            Txt_id_user.Focus();
            Txt_id_user.SelectionStart = Txt_id_user.Text.Length;
           
        }

        private void Txt_id_user_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void Txt_pass_user_Leave(object sender, EventArgs e)
        {
            while (Txt_pass_user.Text == "")
            {
                Txt_pass_user.UseSystemPasswordChar = false;
                Txt_pass_user.Text = "Contraseña";
            }
        }

        private void Txt_id_user_KeyDown(object sender, KeyEventArgs e)
        {
            while (Txt_id_user.Text == "Cédula")
            {
                Txt_id_user.Text = "";

            } 
        }

        private void Txt_id_user_Leave(object sender, EventArgs e)
        {
            if (Txt_id_user.Text == "")
            {
                Txt_id_user.Text = "Cédula";
            }
        }

        private void Txt_pass_user_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar == (int)Keys.Enter)
            {
                login();

            }
            //else
            //{
            //    Txt_pass_user.Text = "";
            //    Txt_pass_user.UseSystemPasswordChar = true; GERENA ERROR NO ME DEJABA PONER MAS DE 1 CARACTER
                
            //}
        }

        private void Inicio_Sesion_Load(object sender, EventArgs e)
        {

        }

        private void Txt_pass_user_KeyDown(object sender, KeyEventArgs e)
        {
            while (Txt_pass_user.Text == "Contraseña")
            {
                Txt_pass_user.Text = "";

            }
        }

        private void lblOlvidarContraseña_Click(object sender, EventArgs e)
        {
            Opciones_recuperacion op = new Opciones_recuperacion();
            op.ShowDialog();
        }
    }
}
