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
    public partial class Perfil_personal : Form
    {
        public Clases.conexion_bd conexion = new Clases.conexion_bd();
        public Clases.Usuarios usuario = new Clases.Usuarios();
        public Perfil_personal()
        {
            InitializeComponent();
        }

        private void Perfil_personal_Load(object sender, EventArgs e)
        {
            txtNombreUser.Text = Clases.Usuario_logeado.nombre_usuario;
            txtApellidoUser.Text = Clases.Usuario_logeado.apellido_usuario;
            txtCorreoUser.Text = Clases.Usuario_logeado.correo_usuario;
            txtTlfnUser.Text = Clases.Usuario_logeado.tlfn_usuario;
            lblCargo.Text = Clases.Usuario_logeado.cargo_usuario;
            lblCedula.Text = Convert.ToString(Clases.Usuario_logeado.id_usuario);
            lblNombreUsuario.Text = Clases.Usuario_logeado.nombre_usuario +" "+ Clases.Usuario_logeado.apellido_usuario;
            //permite que la imagen sea redonda
            System.Drawing.Drawing2D.GraphicsPath gp = new System.Drawing.Drawing2D.GraphicsPath();
            gp.AddEllipse(0, 0, picFotoUser.Width - 3, picFotoUser.Height - 3);
            Region rg = new Region(gp);
            picFotoUser.Region = rg;
            picFotoUser.Image =Clases.Helper.ByteArrayToImage( Clases.Usuario_logeado.imagen_usuario);

            this.Location = new Point(-5, 0);
        }


        private void ActualizarDatos()
        {
          
            try
            {
                conexion.cerrarconexion();
                if (conexion.abrirconexion() == true)
                {
                    if (txtNombreUser.Text == "")
                    {
                        errorProviderNombre.SetError(txtNombreUser, "Debe proporcionar un nombre válido.");
                        txtNombreUser.Focus();
                    }
                    else if (txtApellidoUser.Text == "")
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
                    }else//si todo OK
                    {
                        btnActualizarDatos.Enabled = true;
                        txtCorreoUser.BackColor = Color.FromArgb(218, 232, 240);
                        usuario.correo_usuario = txtCorreoUser.Text;
                        txtTlfnUser.BackColor = Color.FromArgb(218, 232, 240);
                        usuario.tlfn_usuario = txtTlfnUser.Text;
                            
                        usuario.nombre_usuario = txtNombreUser.Text;
                        usuario.apellido_usuario = txtApellidoUser.Text;
                        usuario.cargo_usuario = lblCargo.Text;
                        usuario.id_usuario = Convert.ToInt32(lblCedula.Text);
                        usuario.imagen_usuario = Clases.Helper.ImageToByteArray(picFotoUser.Image);
                        usuario.password = Clases.Usuario_logeado.password;
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
                                    MessageBox.Show("Deberá iniciar sesión nuevamente.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    conexion.cerrarconexion();
                                    this.Close();
                                    Inicio_Sesion ini = new Inicio_Sesion();

                                    ini.Show();

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
                }
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                conexion.cerrarconexion();
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog od = new OpenFileDialog();
                od.Filter = "Image Files(*.jpg; *.jpeg; *.png)|*.jpg; *.jpeg; *.png";
                if (od.ShowDialog() == DialogResult.OK)
                {

                    picFotoUser.Image = Image.FromFile(od.FileName);
                   
                    lblInfoPic.Location = new Point(68, 136);
                    lblInfoPic.Text = "Foto seleccionada";
                    btnActualizarDatos.Enabled = true;
                }

            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                conexion.cerrarconexion();

            }
        }

        private void Editable()
        {
            txtNombreUser.Enabled = true;
            txtApellidoUser.Enabled = true;
            txtCorreoUser.Enabled = true;
            txtTlfnUser.Enabled = true;
        }
        
        private void NoEditable()
        {
            txtNombreUser.Enabled =false;
            txtApellidoUser.Enabled = false;
            txtCorreoUser.Enabled = false;
            txtTlfnUser.Enabled = false;
        }
        private void btnEditarPerfil_Click(object sender, EventArgs e)
        {
            Editable();
        }

        private void lblOlvidarContraseña_Click(object sender, EventArgs e)
        {
            Cambio_contraseña cambio = new Cambio_contraseña();
            cambio.ShowDialog();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            Clases.Usuarios.ActualizarPreguntas = 1;
            Preguntas_de_seguridad preguntas = new Preguntas_de_seguridad();
            preguntas.ShowDialog();
            Clases.Usuarios.ActualizarPreguntas = 0;
        }

        private void btnActualizarDatos_Click(object sender, EventArgs e)
        {
            ActualizarDatos();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Desea cancelar la operación?", "",
      MessageBoxButtons.YesNo, MessageBoxIcon.Question)
      == DialogResult.Yes)
            {
                this.Close();
                NoEditable();
            }
        }

        private void txtNombreUser_KeyPress(object sender, KeyPressEventArgs e)
        {
            Clases.Paneles.sololetras(e);
            btnActualizarDatos.Enabled = true;
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
            btnActualizarDatos.Enabled = true;
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

            if (Clases.Paneles.comprobarFormatoTlfn(e, txtTlfnUser.Text) == false)
            {
                errorProviderTlfn.SetError(txtTlfnUser, "Debe proporcionar un número de teléfono válido.");
                txtTlfnUser.Focus();
            }
            else
            {
                errorProviderTlfn.SetError(txtTlfnUser, "");
                btnActualizarDatos.Enabled = true;
            }
        }

        private void txtCorreoUser_Validating(object sender, CancelEventArgs e)
        {
            if (Clases.Paneles.ComprobarFormatoEmail(txtCorreoUser.Text) == false)
            {
                errorProviderCorreo.SetError(txtCorreoUser, "Debe proporcionar un correo electrónico válido.");
                txtCorreoUser.Focus();
            }
            else
            {
                errorProviderCorreo.SetError(txtCorreoUser, "");
                btnActualizarDatos.Enabled = true;
            }
        }

    }
}
