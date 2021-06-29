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
    public partial class Modificar_difusion : Form
    {
        public Clases.conexion_bd conexion = new Clases.conexion_bd();
        public Clases.Difusion dif = new Clases.Difusion();
        public Modificar_difusion()
        {
            InitializeComponent();
        }

        private void Modificar_difusion_Load(object sender, EventArgs e)
        {
            if (Clases.Difusion.idD != 0)
            {
                txtContenido.Text = Clases.Difusion.contenido;
            }
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

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                conexion.cerrarconexion();
                if (conexion.abrirconexion() == true)
                {
                    dif.id_dif = Clases.Difusion.idD;
                    if (txtContenido.Text == "")
                    {
                        
                        errorProviderContenido.SetError(txtContenido, "Debe proporcionar un nombre válido.");
                        txtContenido.Focus();
                    }
                    else
                    {
                        errorProviderContenido.SetError(txtContenido, "");
                        dif.contenido_dif = txtContenido.Text;
                        int existe = Clases.Difusion.ExisteDif(conexion.conexion,dif);
                        conexion.cerrarconexion();

                        if (existe == Clases.Difusion.idD)
                        {
                            MessageBox.Show("No se han encontrado cambios.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else if (existe != 0)
                        {
                            errorProviderContenido.SetError(txtContenido, "Esta opción ya se encuentra registrada.");
                            txtContenido.Clear();
                            txtContenido.Focus();                                                        
                        }
                        else
                        {
                            errorProviderContenido.SetError(txtContenido, "");
                            conexion.cerrarconexion();
                            if (conexion.abrirconexion() == true)
                            {
                                int modificar = Clases.Difusion.ModificarDif(conexion.conexion, dif);
                                conexion.cerrarconexion();
                                if (modificar > 0)
                                {
                                    MessageBox.Show("Los datos han sido modificados exitosamente.", "", MessageBoxButtons.OK);
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

        private void btnCancelar_Click(object sender, EventArgs e)
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
