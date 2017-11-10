using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace UCS_NODO_FGC
{
    public partial class Registrar_insumo : Form
    {
        Clases.conexion_bd conexion = new Clases.conexion_bd();
        Clases.Insumos insumo = new Clases.Insumos();
        public Registrar_insumo()
        {
            InitializeComponent();
        }

        private void Registrar_insumo_Load(object sender, EventArgs e)
        {

        }

        private void txtContenido_KeyPress(object sender, KeyPressEventArgs e)
        {
            Clases.Paneles.sololetras(e);
            if (txtContenido.Text.Length == 0)
            {
                e.KeyChar = e.KeyChar.ToString().ToUpper().ToCharArray()[0];
            }
            else if (txtContenido.Text.Length > 0)
            {
                e.KeyChar = e.KeyChar.ToString().ToLower().ToCharArray()[0];
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

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                conexion.cerrarconexion();
                if (conexion.abrirconexion() == true)
                {
                    if (txtContenido.Text == "")
                    {

                        errorProviderContenido.SetError(txtContenido, "Debe proporcionar un nombre válido.");
                        txtContenido.Focus();
                    }
                    else
                    {
                        errorProviderContenido.SetError(txtContenido, "");
                        insumo.contenido_insumo= txtContenido.Text;
                        int existe = Clases.Insumos.ExisteInsumo(conexion.conexion, insumo);
                        conexion.cerrarconexion();

                        if (existe > 0)
                        {
                            errorProviderContenido.SetError(txtContenido, "Este insumo ya se encuentra registrado.");
                            txtContenido.Clear();
                            txtContenido.Focus();
                        }
                        else
                        {
                            errorProviderContenido.SetError(txtContenido, "");
                            if (conexion.abrirconexion() == true)
                            {

                                int registrar = Clases.Insumos.AgregarInsumo(conexion.conexion, insumo);
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
    }
}
