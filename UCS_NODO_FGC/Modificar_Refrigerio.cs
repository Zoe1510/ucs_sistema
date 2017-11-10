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
    public partial class Modificar_Refrigerio : Form
    {
        public Clases.conexion_bd conexion = new Clases.conexion_bd();
        public Clases.Refrigerios refri = new Clases.Refrigerios();
        public Modificar_Refrigerio()
        {
            InitializeComponent();
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

        private void Modificar_Refrigerio_Load(object sender, EventArgs e)
        {
            if (Clases.Refrigerios.idR != 0)
            {
                txtNombreRef.Text = Clases.Refrigerios.nombreR;
                txtContenidoRef.Text = Clases.Refrigerios.contenidoR;
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                conexion.cerrarconexion();
                if (conexion.abrirconexion() == true)
                {
                   
                    if(txtNombreRef.Text == "")
                    {
                        errorProviderNombre.SetError(txtNombreRef, "Debe proporcionar un nombre válido.");
                        txtNombreRef.Focus();
                    }
                    else if(txtContenidoRef.Text =="")
                    {
                        errorProviderNombre.SetError(txtNombreRef, "");
                        errorProviderContenido.SetError(txtContenidoRef, "Debe proporcionar un nombre válido.");
                        txtContenidoRef.Focus();
                    }else
                    {
                        refri.id_ref = Clases.Refrigerios.idR;
                        errorProviderContenido.SetError(txtContenidoRef, "");
                        refri.nombre = txtNombreRef.Text;
                        refri.contenido_ref = txtContenidoRef.Text;
                        int existe = Clases.Refrigerios.ExisteRef(conexion.conexion, refri);
                        conexion.cerrarconexion();
                        

                        if (existe==Clases.Refrigerios.idR)
                        {
                            MessageBox.Show("No se han encontrado cambios.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else if (existe != 0)
                        {
                           
                            errorProviderNombre.SetError(txtNombreRef, "Ya existe este nombre registrado.");
                            txtNombreRef.Clear();
                            txtNombreRef.Focus();
                        }
                        else
                        {
                            errorProviderNombre.SetError(txtNombreRef, "");
                            if (conexion.abrirconexion() == true)
                            {
                               
                                int modificar = Clases.Refrigerios.ModificarRef(conexion.conexion, refri);
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
    }
}
