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
    public partial class Registrar_refrigerio : Form
    {
        public Clases.conexion_bd conexion = new Clases.conexion_bd();
        public Clases.Refrigerios refri = new Clases.Refrigerios();
        public Registrar_refrigerio()
        {
            InitializeComponent();
        }

        private void Registrar_refrigerio_Load(object sender, EventArgs e)
        {

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
        private void txtNombreRef_KeyPress(object sender, KeyPressEventArgs e)
        {
            Clases.Paneles.sololetras(e);
            if (txtNombreRef.Text.Length == 0)
            {
                e.KeyChar = e.KeyChar.ToString().ToUpper().ToCharArray()[0];
            }
            else if (txtNombreRef.Text.Length > 0)
            {
                e.KeyChar = e.KeyChar.ToString().ToLower().ToCharArray()[0];
            }
        }

        private void txtContenidoRef_KeyPress(object sender, KeyPressEventArgs e)
        {
            Clases.Paneles.sololetras(e);
            if (txtNombreRef.Text.Length == 0)
            {
                e.KeyChar = e.KeyChar.ToString().ToUpper().ToCharArray()[0];
            }
            else if (txtNombreRef.Text.Length > 0)
            {
                e.KeyChar = e.KeyChar.ToString().ToLower().ToCharArray()[0];
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                conexion.cerrarconexion();
                if (conexion.abrirconexion() == true)
                {
                    if (txtNombreRef.Text == "")
                    {
                        errorProviderNombre.SetError(txtNombreRef, "Debe proporcionar un nombre válido.");
                        txtNombreRef.Focus();
                    }
                    else if (txtContenidoRef.Text == "")
                    {
                        errorProviderNombre.SetError(txtNombreRef, "");
                        errorProviderContenido.SetError(txtContenidoRef, "Debe proporcionar un nombre válido.");
                        txtContenidoRef.Focus();
                    }
                    else
                    {
                        errorProviderContenido.SetError(txtContenidoRef, "");
                        refri.nombre = txtNombreRef.Text;
                        refri.contenido_ref = txtContenidoRef.Text;
                        int existe = Clases.Refrigerios.ExisteRef(conexion.conexion, refri);
                        conexion.cerrarconexion();

                         if (existe > 0)
                        {
                            errorProviderNombre.SetError(txtNombreRef, "No se permiten datos duplicados.");
                            errorProviderContenido.SetError(txtContenidoRef, "No se permiten datos duplicados.");
                            txtNombreRef.Clear();
                            txtContenidoRef.Clear();
                            txtNombreRef.Focus();
                        }
                        else
                        {
                            errorProviderNombre.SetError(txtNombreRef, "");
                            errorProviderContenido.SetError(txtContenidoRef, "");
                            conexion.cerrarconexion();
                            if (conexion.abrirconexion() == true)
                            {
                                int registrar = Clases.Refrigerios.AgregarRef(conexion.conexion, refri);
                                conexion.cerrarconexion();
                                if (registrar > 0)
                                {
                                    MessageBox.Show("Registro exitoso.", "AVISO", MessageBoxButtons.OK);
                                    this.Close();
                                }
                            }
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

        private void label9_Click(object sender, EventArgs e)
        {

        }
    }
}
