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
    public partial class Registrar_usuarios : Form
    {
        public Clases.Usuarios usuario = new Clases.Usuarios();
        public Clases.conexion_bd conexion = new Clases.conexion_bd();
        
        public Registrar_usuarios()
        {
            InitializeComponent();
        }

        private void RegistrarUsuario()
        {
            string cargo;
            int contador;
            string tlfn = "00000000000";
            string pass = "12345678";
            string nacionalidad;
            int ci_usuario;
            conexion.cerrarconexion();
            try
            {
                if (conexion.abrirconexion() == true)
                {
                    contador = Clases.Usuarios.retornaFilas(conexion.conexion);
                    conexion.cerrarconexion();
                    if (conexion.abrirconexion() == true)
                    {
                        string correo = "correo" + contador.ToString() + "@ejemplo.com";
                        if (txtCedulaUsuario.Text == "" || txtCedulaUsuario.Text.Length < 7)
                        {
                            errorProviderCI.SetError(txtCedulaUsuario, "Debe proporcionar un número de cédula válido.");
                            txtCedulaUsuario.Focus();

                        }else if (txtNombreUsuario.Text == "" || txtNombreUsuario.Text == "Nombre")
                        {
                            errorProviderCI.SetError(txtCedulaUsuario, "");

                            errorProviderNombre.SetError(txtNombreUsuario, "Debe proporcionar un nombre válido.");
                            txtNombreUsuario.Focus();
                        }else if (txtApellidoUsuario.Text == "Apellido" || txtApellidoUsuario.Text == "")
                        {
                            errorProviderNombre.SetError(txtNombreUsuario, "");

                            errorProviderApellido.SetError(txtApellidoUsuario, "Debe proporcionar un apellido válido.");
                            txtApellidoUsuario.Focus();
                        }else if (cmbxCargoUsuario.SelectedIndex == -1)
                        {
                            errorProviderApellido.SetError(txtApellidoUsuario, "");

                            errorProviderCMBCargo.SetError(cmbxCargoUsuario, "Debe seleccionar un cargo para el usuario.");
                        }else //todo ok entonces registro
                        {
                            errorProviderCMBCargo.SetError(cmbxCargoUsuario, "");

                            usuario.cedula_user = Convert.ToInt32(txtCedulaUsuario.Text);
                            ci_usuario = usuario.cedula_user;
                            usuario.nombre_usuario = txtNombreUsuario.Text;
                            usuario.apellido_usuario = txtApellidoUsuario.Text;
                            cargo = Convert.ToString(cmbxCargoUsuario.SelectedIndex);
                            switch (cargo)
                            {
                                case "0":
                                    cargo = "Asistente";
                                    break;
                                case "1":
                                    cargo = "Coordinador";
                                    break;
                                case "2":
                                    cargo = "Lider";
                                    break;
                            }
                            nacionalidad = Convert.ToString(cmbNacionalidad.SelectedIndex);
                            switch (nacionalidad)
                            {
                                case "0":
                                    nacionalidad = "V";
                                    break;
                                case "1":
                                    nacionalidad = "E";
                                    break;
                            }
                            usuario.nacionalidad_usuario = nacionalidad;
                            usuario.cargo_usuario = cargo;
                            usuario.correo_usuario = correo;
                            usuario.tlfn_usuario = tlfn;
                            usuario.password = pass;
                            usuario.imagen_usuario = Clases.Helper.ImageToByteArray(Properties.Resources.img_perfil);
                            int resultado;
                            int resultado2;
                            resultado = Clases.Usuarios.UsuarioExiste(conexion.conexion, ci_usuario);
                            conexion.cerrarconexion();
                            if (conexion.abrirconexion() == true)
                            {
                                resultado2 = Clases.Usuarios.AgregarUsuarios(conexion.conexion, usuario);
                                conexion.cerrarconexion();

                                if (conexion.abrirconexion() == true)
                                {

                                    if (resultado != 1)
                                    {
                                        if (resultado2 != 0)
                                        {
                                            //Image imagen = UCS_NODO_FGC.Properties.Resources.img_perfil;
                                            //Clases.Usuarios.GuardarFotoPerfil(conexion.conexion, usuario);
                                            if (MessageBox.Show("El usuario fue creado con exito. ¿Desea añadir más usuarios?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                                            {
                                                this.Close();
                                            }
                                            else
                                            {
                                                txtCedulaUsuario.Text = "Cédula";
                                                txtNombreUsuario.Text = "Nombre";
                                                txtApellidoUsuario.Text = "Apellido";
                                                cmbxCargoUsuario.Text = "                          Cargo";
                                                cmbxCargoUsuario.SelectedIndex = -1;
                                            }
                                        }
                                        else
                                        {
                                            MessageBox.Show("Ha ocurrido un error en la base de datos");
                                        }

                                    }
                                    else
                                    {
                                        MessageBox.Show("Ya existe un usuario registrado con esa cédula", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        txtCedulaUsuario.Text = "Cédula";
                                        txtNombreUsuario.Text = "Nombre";
                                        txtApellidoUsuario.Text = "Apellido";
                                        cmbxCargoUsuario.Text = "                          Cargo";
                                        cmbxCargoUsuario.SelectedIndex = -1;

                                    }
                                    conexion.cerrarconexion();
                                }

                            }

                        }
                    }
                }

            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            conexion.cerrarconexion();
        }
        private void btnGuardarUsuario_Click(object sender, EventArgs e)
        {
            RegistrarUsuario();
        }

        private void txtNombreUsuario_KeyPress(object sender, KeyPressEventArgs e)
        {
            Clases.Paneles.sololetras(e);
            if (txtNombreUsuario.Text.Length == 0)
            {
                e.KeyChar = e.KeyChar.ToString().ToUpper().ToCharArray()[0];
            }
            else if (txtNombreUsuario.Text.Length > 0)
            {
                e.KeyChar = e.KeyChar.ToString().ToLower().ToCharArray()[0];
            }
        }
        private void txtApellidoUsuario_KeyPress(object sender, KeyPressEventArgs e)
        {
            Clases.Paneles.sololetras(e);
            if (txtApellidoUsuario.Text.Length == 0)
            {
                e.KeyChar = e.KeyChar.ToString().ToUpper().ToCharArray()[0];
            }
            else if (txtApellidoUsuario.Text.Length > 0)
            {
                e.KeyChar = e.KeyChar.ToString().ToLower().ToCharArray()[0];
            }
        }

        private void Registrar_usuarios_Load(object sender, EventArgs e)
        {
            txtCedulaUsuario.Focus();
            this.Location = new Point(-5, 0);
            txtCedulaUsuario.SelectionStart = txtCedulaUsuario.Text.Length;
            if (Clases.Usuario_logeado.cargo_usuario == "Lider")
            {
                cmbxCargoUsuario.Items.Add("Asistente");
                cmbxCargoUsuario.Items.Add("Coordinador");
                cmbxCargoUsuario.Items.Add("Líder");
            }
            else if (Clases.Usuario_logeado.cargo_usuario == "Coordinador")
            {
                cmbxCargoUsuario.Items.Clear();
                cmbxCargoUsuario.Items.Add("Asistente");
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
        private void txtCedulaUsuario_KeyPress(object sender, KeyPressEventArgs e)
        {
            Clases.Paneles.solonumeros(e);
        }
        private void txtCedulaUsuario_KeyDown(object sender, KeyEventArgs e)
        {
            while (txtCedulaUsuario.Text == "Cédula")
            {
                txtCedulaUsuario.Text = "";

            }
        }

        private void txtCedulaUsuario_Leave(object sender, EventArgs e)
        {
            if (txtCedulaUsuario.Text == "")
            {
                txtCedulaUsuario.Text = "Cédula";
            }
        }

        private void txtCedulaUsuario_Click(object sender, EventArgs e)
        {
            if (txtCedulaUsuario.Text != "")
            {

            }
            else
            {
                txtCedulaUsuario.Text = "";
            }
        }

        private void txtNombreUsuario_Click(object sender, EventArgs e)
        {
            if (txtNombreUsuario.Text != "")
            {
                txtCedulaUsuario.SelectionStart = txtCedulaUsuario.Text.Length;
            }
            else
            {
                txtNombreUsuario.Text = "";
            }
        }

        private void txtNombreUsuario_Leave(object sender, EventArgs e)
        {
            if (txtNombreUsuario.Text == "")
            {
                txtNombreUsuario.Text = "Nombre";
            }
        }

        private void txtNombreUsuario_KeyDown(object sender, KeyEventArgs e)
        {
            while (txtNombreUsuario.Text == "Nombre")
            {
                txtNombreUsuario.Text = "";

            }
        }

        private void txtApellidoUsuario_Leave(object sender, EventArgs e)
        {
            if (txtApellidoUsuario.Text == "")
            {
                txtApellidoUsuario.Text = "Apellido";
            }
        }

        private void txtApellidoUsuario_Click(object sender, EventArgs e)
        {
            if (txtApellidoUsuario.Text != "")
            {

            }
            else
            {
                txtApellidoUsuario.Text = "";
            }
        }

        private void txtApellidoUsuario_KeyDown(object sender, KeyEventArgs e)
        {
            while (txtApellidoUsuario.Text == "Apellido")
            {
                txtApellidoUsuario.Text = "";

            }
        }

        private void txtApellidoUsuario_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtNombreUsuario_TextChanged(object sender, EventArgs e)
        {

        }

        private void cmbxCargoUsuario_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtCedulaUsuario_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtCedulaUsuario_Validating(object sender, CancelEventArgs e)
        {
            if (Clases.Paneles.comprobarCedula(txtCedulaUsuario.Text) == true)
            {
                cmbNacionalidad.SelectedIndex = 1;
            }
            else
            {
                cmbNacionalidad.SelectedIndex = 0;
            }
        }

        private void cmbNacionalidad_Validating(object sender, CancelEventArgs e)
        {
            if (Clases.Paneles.comprobarCedula(txtCedulaUsuario.Text) == true)
            {
                cmbNacionalidad.SelectedIndex = 1;
            }
            else
            {
                cmbNacionalidad.SelectedIndex = 0;
            }
        }
    }
}
