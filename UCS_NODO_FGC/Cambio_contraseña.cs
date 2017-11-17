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
    public partial class Cambio_contraseña : Form
    {
        public Clases.conexion_bd conexion = new Clases.conexion_bd();
        public Cambio_contraseña()
        {
            InitializeComponent();
        }

        private void Actualizar()
        {
            txtNuevoPass.UseSystemPasswordChar = true;
            txtConfirPass.UseSystemPasswordChar = true;
            try
            {
                conexion.cerrarconexion();
                if (conexion.abrirconexion() == true)
                {
                    if (Clases.Recuperacion_contraseña.cedula != 0) // si viene referenciado desde PREGUNTAS DE SEGURIDAD
                    {
                        int resultado = Clases.Usuarios.ActualizarContraseña(conexion.conexion, Clases.Recuperacion_contraseña.id_usuario, txtNuevoPass.Text);
                        conexion.cerrarconexion();
                        if (resultado != 0)
                        {
                            MessageBox.Show("Contraseña actualizada.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Close();
                            Clases.Recuperacion_contraseña.cedula = 0;
                            Clases.Recuperacion_contraseña.nombre = "";
                            Clases.Recuperacion_contraseña.Opcion = 0;
                        }
                    }else//se ejecuta si viene por edicion en el perfil
                    {
                        MessageBox.Show(txtNuevoPass.Text);
                        int resultado = Clases.Usuarios.ActualizarContraseña(conexion.conexion,Clases.Usuario_logeado.id_usuario, txtNuevoPass.Text);
                        conexion.cerrarconexion();
                        if (resultado != 0)
                        {
                            MessageBox.Show("Contraseña actualizada.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Close();
                            
                        }
                    }
                   
                }

            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show("Ha ocurrido un error: " + ex.Message, "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtNuevoPass_Validating(object sender, CancelEventArgs e)
        {
            //txtNuevoPass.UseSystemPasswordChar = true;
            if(txtNuevoPass.TextLength < 8)
            {
                errorProviderNewPass.SetError(txtNuevoPass, "La contraseña debe tener mínimo 8 caráteres.");
                txtNuevoPass.Clear();
                txtNuevoPass.Focus();
            }else if(txtNuevoPass.Text == "12345678") 
            {
                errorProviderNewPass.SetError(txtNuevoPass, "");
                errorProviderNewPass.SetError(txtNuevoPass, "Contraseña inválida.");
                txtNuevoPass.Clear();
                txtNuevoPass.Focus();
            }else
            {
                errorProviderNewPass.SetError(txtNuevoPass, "");
                txtConfirPass.Enabled = true;
            }
        }

        private void txtConfirPass_Validating(object sender, CancelEventArgs e)
        {
            //txtConfirPass.UseSystemPasswordChar = true;
            if (txtConfirPass.Text != txtNuevoPass.Text)
            {
                errorProviderConfirPass.SetError(txtConfirPass, "Las contraseñas no coinciden.");
                errorProviderNewPass.SetError(txtNuevoPass, "Las contraseñas no coinciden.");
                txtConfirPass.Clear();
                txtConfirPass.Focus();
            }else
            {
                errorProviderNewPass.SetError(txtNuevoPass, "");
                errorProviderConfirPass.SetError(txtConfirPass, "");
            }
        }

        private void btnActualizarPass_Click(object sender, EventArgs e)
        {
            Actualizar();
        }

        private void Cambio_contraseña_Load(object sender, EventArgs e)
        {
            txtNuevoPass.UseSystemPasswordChar = true;
            txtConfirPass.UseSystemPasswordChar = true;
        }

        private void btn_cerrar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Desea cancelar la operación?", "",
       MessageBoxButtons.YesNo, MessageBoxIcon.Question)
       == DialogResult.Yes)
            {
                this.Close();
            }
        }
    }
}
