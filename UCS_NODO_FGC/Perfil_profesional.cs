using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions; //para la comprobacion del email

namespace UCS_NODO_FGC
{
    public partial class Perfil_profesional : Form
    {
        public Clases.Usuarios usuario = new Clases.Usuarios();
        public Clases.conexion_bd conexion = new Clases.conexion_bd();
        public Clases.Usuario_logeado usuariologeado = new Clases.Usuario_logeado();

        public Perfil_profesional()
        {
            InitializeComponent();
        }


        private void txtConfirPass_TextChanged(object sender, EventArgs e)
        {
          
        }

        private void Perfil_profesional_Load(object sender, EventArgs e)
        {
            txtNombreUser.Text = Clases.Usuario_logeado.nombre_usuario;
            txtApellidoUser.Text = Clases.Usuario_logeado.apellido_usuario;
            txtCorreoUser.Text = Clases.Usuario_logeado.correo_usuario;
            txtTlfnUser.Text = Clases.Usuario_logeado.tlfn_usuario;
            lblCargo.Text = Clases.Usuario_logeado.cargo_usuario;
            lblCedula.Text = Convert.ToString(Clases.Usuario_logeado.id_usuario);
            txtNuevoPass.Text = "";
            txtConfirPass.Text = "";


            //permite que la imagen sea redonda
            System.Drawing.Drawing2D.GraphicsPath gp = new System.Drawing.Drawing2D.GraphicsPath();
            gp.AddEllipse(0, 0, picFotoUser.Width - 3, picFotoUser.Height - 3);
            Region rg = new Region(gp);
            picFotoUser.Region = rg;

            picFotoUser.Image = UCS_NODO_FGC.Properties.Resources.img_perfil;
            this.Location = new Point(-178, 5);
        }

        private void btnCambioPic_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog od = new OpenFileDialog();
                od.Filter = "Image Files(*.jpg; *.jpeg; *.png)|*.jpg; *.jpeg; *.png";
                if (od.ShowDialog() == DialogResult.OK)
                {

                    picFotoUser.Image = Image.FromFile(od.FileName);
                    lblNombreImg.Text = od.FileName;
                    lblInfoPic.Location = new Point(68,136);
                    lblInfoPic.Text = "Foto seleccionada";
                }

            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                conexion.cerrarconexion();

            }
        }

        private void ActualizarDatos()
        {
            lblMensajeErrorPass.Visible = false;
            try
            {
                conexion.cerrarconexion();
                if (conexion.abrirconexion() == true)
                {
                    if(txtNombreUser.Text == "")
                    {
                        errorProviderNombre.SetError(txtNombreUser,"Debe proporcionar un nombre válido.");
                        txtNombreUser.Focus();
                    }else if (txtApellidoUser.Text == "")
                    {
                        errorProviderNombre.SetError(txtNombreUser, "");
                        errorProviderApellido.SetError(txtApellidoUser, "Debe proporcionar un apellido válido.");
                        txtApellidoUser.Focus();
                    }
                    else if (txtCorreoUser.Text == "correo@ejemplo.com")
                    {
                        errorProviderApellido.SetError(txtApellidoUser, "");

                        errorProviderCorreo.SetError(txtCorreoUser, "Debe proporcionar un correo electrónico válido.");
                        txtCorreoUser.Focus();
                    }
                    else if(txtNuevoPass.Text == "12345678")
                    {
                        errorProviderCorreo.SetError(txtCorreoUser, "");

                       lblMensajeErrorPass.Visible = false;
                        MessageBox.Show("Contraseña inválida, inténtelo nuevamente.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                        txtNuevoPass.Text = "";
                        txtNuevoPass.Focus();
                        txtConfirPass.Text = "";
                    }else if (txtNuevoPass.Text.Length < 8 && txtConfirPass.Text.Length < 8)
                    {
                        MessageBox.Show("La contraseña debe tener mínimo ocho (8) carácteres.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }else//si todo OK
                    {
                        //comprobacion de igualdad en las contraseñas ingresadas
                        if (txtNuevoPass.Text == txtConfirPass.Text)
                        {
                            txtCorreoUser.BackColor = Color.FromArgb(218, 232, 240);
                            usuario.correo_usuario = txtCorreoUser.Text;
                            txtTlfnUser.BackColor = Color.FromArgb(218, 232, 240);
                            usuario.tlfn_usuario = txtTlfnUser.Text;
                            usuario.password = txtNuevoPass.Text;
                            lblMensajeErrorPass.Visible = false;
                            usuario.nombre_usuario = txtNombreUser.Text;
                            usuario.apellido_usuario = txtApellidoUser.Text;
                            usuario.cargo_usuario = lblCargo.Text;
                            usuario.id_usuario = Convert.ToInt32(lblCedula.Text);
                            usuario.imagen_usuario = Clases.Helper.ImageToByteArray(picFotoUser.Image);
                            int resultado;

                            resultado = Clases.Usuarios.ActualizarUsuarios(conexion.conexion, usuario);
                            conexion.cerrarconexion();
                            if (conexion.abrirconexion() == true)
                            {
                                if (resultado != 0)
                                {
                                    int resultado2 = 0;

                                    resultado2 = Clases.Usuarios.ActualizarFotoUsuario(conexion.conexion, usuario);
                                    conexion.cerrarconexion();
                                    if (resultado2 != 0)
                                    {
                                        MessageBox.Show("Los datos han sido actualizados correctamente.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.None);
                                        conexion.cerrarconexion();
                                        //hay que ver el evento formclosing paraactualizar los datos en el sistema 
                                        //opcion dos: hacer un cierre de sesion para que se actualicen los datos (foto y nombre y eso)

                                        Preguntas_de_seguridad recovery = new Preguntas_de_seguridad();
                                        recovery.ShowDialog();
                                        this.Close();
                                    }
                                    else
                                    {
                                        MessageBox.Show("Hubo un error al actualizar la foto", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }



                                }
                                else
                                {
                                    MessageBox.Show("No se pudo actualizar los datos.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                                conexion.cerrarconexion();
                            }

                        }
                        else
                        {
                            //mensaje de error
                            lblMensajeErrorPass.Visible = true;
                            txtNuevoPass.Text = "";
                            txtNuevoPass.Focus();
                            txtConfirPass.Text = "";
                        }
                    }
                }
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                conexion.cerrarconexion();
            }
        }
        private void btnActualizarDatos_Click(object sender, EventArgs e)
        {
            ActualizarDatos();
        }

        private void txtTlfnUser_KeyPress(object sender, KeyPressEventArgs e)
        {
            Clases.Paneles.solonumeros(e);
        }

        private void txtNuevoPass_Enter(object sender, EventArgs e)
        {
            txtNuevoPass.UseSystemPasswordChar = true;
        }

        private void txtConfirPass_Enter(object sender, EventArgs e)
        {
            txtConfirPass.UseSystemPasswordChar = true;
        }

        private void txtNombreUser_KeyPress(object sender, KeyPressEventArgs e)
        {
            Clases.Paneles.sololetras(e);
            if (txtNombreUser.Text.Length == 0)
            {
                e.KeyChar = e.KeyChar.ToString().ToUpper().ToCharArray()[0];
            }
            else if (txtNombreUser.Text.Length > 0)
            {
                e.KeyChar = e.KeyChar.ToString().ToLower().ToCharArray()[0];
            }
        }

        private void txtApellidoUser_KeyPress(object sender, KeyPressEventArgs e)
        {
            Clases.Paneles.sololetras(e);
            if (txtApellidoUser.Text.Length == 0)
            {
                e.KeyChar = e.KeyChar.ToString().ToUpper().ToCharArray()[0];
            }
            else if (txtApellidoUser.Text.Length > 0)
            {
                e.KeyChar = e.KeyChar.ToString().ToLower().ToCharArray()[0];
            }
        }

        private void txtTlfnUser_Validating(object sender, CancelEventArgs e)
        {
            
           if( Clases.Paneles.comprobarFormatoTlfn(e, txtTlfnUser.Text) == false)
            {
                errorProviderTlfn.SetError(txtTlfnUser, "Debe proporcionar un número de teléfono válido.");
                txtTlfnUser.Focus();
            }else
            {
                errorProviderTlfn.SetError(txtTlfnUser, "");
            }
        }

        private void txtCorreoUser_Validating(object sender, CancelEventArgs e)
        {
            if (Clases.Paneles.ComprobarFormatoEmail(txtCorreoUser.Text) == false)
            {
                errorProviderCorreo.SetError(txtCorreoUser, "Debe proporcionar un correo electrónico válido.");
                txtCorreoUser.Focus();
            }else
            {
                errorProviderCorreo.SetError(txtCorreoUser, "");
            }
        }

        private void txtConfirPass_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar == (int)Keys.Enter)
            {
                ActualizarDatos();
            }
        }
    }
}
