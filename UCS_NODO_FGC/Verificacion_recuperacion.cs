using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Mail;
using System.Net;
using MySql.Data.MySqlClient;
namespace UCS_NODO_FGC
{
    public partial class Verificacion_recuperacion : Form
    {
        public Clases.Usuarios usuario = new Clases.Usuarios();
        public Clases.conexion_bd conexion = new Clases.conexion_bd();
        string newpass = "";
        public Verificacion_recuperacion()
        {
            InitializeComponent();
        }

        private void Verificacion_recuperacion_Load(object sender, EventArgs e)
        {
            if (Clases.Recuperacion_contraseña.Opcion == 2)
            {
               newpass = GenerarContraseñaNueva();
            }
        }

        //metodos
        private void LeerDatos()
        {
            try
            {
                if (txtCedula.Text == "" || txtCedula.Text.Length <7)
                {
                    errorProviderCedula.SetError(txtCedula, "Debe proporcionar un número de cédula válido.");
                    txtCedula.Focus();
                    //MessageBox.Show("Debe proporcionar su número de cédula.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else if (txtCorreo.Text == "")
                {
                    errorProviderCedula.SetError(txtCedula, "");
                    errorProvider2.SetError(txtCorreo, "Debe proporcionar un correo válido para continuar con el registro.");
                    txtCorreo.Focus();
                }
                else if(txtNombre.Text == "")
                {
                    errorProvider2.SetError(txtCorreo, "");
                    errorProviderNombre.SetError(txtNombre, "Debe proporcionar su nombre.");
                    txtNombre.Focus();
                    //MessageBox.Show("Debe proporcionar su nombre.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else// si los campos están llenos
                {
                    errorProviderNombre.SetError(txtNombre, "");

                    usuario.cedula_user = Convert.ToInt32(txtCedula.Text);
                    usuario.correo_usuario = txtCorreo.Text;
                    usuario.nombre_usuario = txtNombre.Text;

                    if (Clases.Recuperacion_contraseña.Opcion == 1)//si viene desde preguntas de seguridad
                    {
                        //mandarlo al form de preguntas
                       
                        conexion.cerrarconexion();
                        if (conexion.abrirconexion() == true)
                        {
                            int resultado = Clases.Usuarios.BuscarCorreoUsuario(conexion.conexion, usuario);
                            conexion.cerrarconexion();

                            if (resultado != 0)
                            {
                                Clases.Recuperacion_contraseña.cedula = usuario.cedula_user;
                                Clases.Recuperacion_contraseña.nombre = usuario.nombre_usuario;
                                Clases.Recuperacion_contraseña.correo = usuario.correo_usuario;
                                Clases.Recuperacion_contraseña.id_usuario = resultado;

                                Recuperacion_preguntas formpreguntas = new Recuperacion_preguntas();
                                Clases.Recuperacion_contraseña.Opcion = 0;
                                this.Close();
                                formpreguntas.ShowDialog();
                            }
                            else
                            {

                                MessageBox.Show("Los datos no coinciden con la base de datos.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Error);

                            }

                        }
                        
                    }
                    else if (Clases.Recuperacion_contraseña.Opcion == 2)//si viene desde correo electronico
                    {
                        conexion.cerrarconexion();
                        if (conexion.abrirconexion() == true)
                        {

                            int resultado = Clases.Usuarios.BuscarCorreoUsuario(conexion.conexion, usuario);

                            conexion.cerrarconexion();

                            if (resultado != 0)
                            {
                                if (conexion.abrirconexion() == true)
                                {
                                   
                                            int retorno = Clases.Usuarios.CambiarContraseña(conexion.conexion, newpass,resultado);
                                            conexion.cerrarconexion();
                                            EnviarCorreo(newpass, usuario.correo_usuario);
                                            this.Close();
                                        
                                                                 
                                   
                                }

                            }
                            else
                            {

                                MessageBox.Show("Los datos no coinciden con la base de datos.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Error);

                            }


                        }

                    }

                }
                
            }
            catch(MySqlException ex)
            {
                MessageBox.Show("Ha ocurrido un error: " + ex.Message, "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string GenerarContraseñaNueva()
        {
            Random rnd = new Random();
            int new_pass = rnd.Next(10000000, 999999999);
            String nueva_contraseña = new_pass.ToString();
            return nueva_contraseña;
        }
        private void EnviarCorreo(String contraseña_nueva, String correo)
        {
            //String remitente = "patricia-bermudez@outlook.com"; //colocar correo predeterminado para envio de contraseñas nuevas
            String destintario = correo;
            String asunto = "Recuperación de contraseña";
            String mensaje = "Su nueva contraseña es: " + contraseña_nueva;
            //MailMessage ms = new MailMessage(remitente, destintario, asunto, mensaje);
            //SmtpClient smtp = new SmtpClient("smtp.live.com", 587);
            MailMessage msg = new MailMessage();
            SmtpClient smtp = new SmtpClient();
            String remitente = "soporteucs@gmail.com";
            msg.To.Add(destintario);
            msg.From = new MailAddress(remitente, "Universidad Coorporativa Sigo", System.Text.Encoding.UTF8);
            msg.Subject = asunto;
            msg.SubjectEncoding = System.Text.Encoding.UTF8;
            msg.Body = mensaje;
            msg.BodyEncoding = System.Text.Encoding.UTF8;
            msg.IsBodyHtml = false;

            smtp.Credentials = new System.Net.NetworkCredential(remitente, "ucs.29933526"); //entre comillas va el password de ese correo electronico
            smtp.Port = 587;
            smtp.Host = "smtp.gmail.com";
            smtp.EnableSsl = true;
            //smtp.Credentials = new NetworkCredential(remitente, "yeb909cr1"); //entre comillas va el password de ese correo electronico

            try
            {
                Task.Run(() =>
                {
                    smtp.Send(msg);
                    msg.Dispose();
                    //smtp.Send(ms);
                    //ms.Dispose();
                    MessageBox.Show("Correo enviado. Revise su bandeja de entrada.", "AVISO", MessageBoxButtons.OK);
                    Clases.Recuperacion_contraseña.Opcion = 0;
                    
                }
                );

                MessageBox.Show("Esta tarea puede tardar algunos minutos, por favor espere");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al enviar el correo electrónico: " + ex.Message);

            }


        }

        //eventos botones
        private void txtCorreo_Validating(object sender, CancelEventArgs e)
        {
            if (Clases.Paneles.ComprobarFormatoEmail(txtCorreo.Text) == false)
            {
                errorProvider2.SetError(txtCorreo, "Debe proporcionar un correo válido para continuar con el registro.");
                txtCorreo.Focus();
            }else
            {
                errorProvider2.SetError(txtCorreo, "");
            }
        }

        private void txtCedula_KeyPress(object sender, KeyPressEventArgs e)
        {
            Clases.Paneles.solonumeros(e);
        }

        private void txtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            Clases.Paneles.sololetras(e);
            if ((int)e.KeyChar == (int)Keys.Enter)
            {
                LeerDatos();
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Desea cancelar la operación?", "",
      MessageBoxButtons.YesNo, MessageBoxIcon.Question)
      == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void btnGuardarPDS_Click(object sender, EventArgs e)
        {
            LeerDatos();
        }

        private void btn_minimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btn_cerrar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Desea salir?", "",
        MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        == DialogResult.Yes)
            {
                this.Close();
            }
        }
        public int xClick = 0, yClick = 0;
        private void Verificacion_recuperacion_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)

            { xClick = e.X; yClick = e.Y; }

            else

            { this.Left = this.Left + (e.X - xClick); this.Top = this.Top + (e.Y - yClick); }
        }
    }
}
