using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using UCS_NODO_FGC.Clases;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;

namespace UCS_NODO_FGC
{
    public partial class Ver_participantes : Form
    {

        Formaciones formaciones = new Formaciones();
        Participantes part = new Participantes();
        int resultado = 0;
        string nombre_user;
        public Ver_participantes()
        {
            InitializeComponent();

            dgvParticipantes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            //PARA SELECCIONAR
            dgvParticipantes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvParticipantes.MultiSelect = false;
            dgvParticipantes.ClearSelection();
        }

        private void Ver_participantes_Load(object sender, EventArgs e)
        {
            this.Location = new Point(-5, 0);
            llenarDGV();
            dtpFechaCurso.Value = DateTime.Today;
            btnEliminar.Enabled = false;
        }
        /*----------------------METODOS------------------------*/

        private void llenarDGV()
        {
           
           try
            {
                MySqlDataReader participantes = Conexion.ConsultarBD("SELECT nombre_par, cedula_par, apellido_par, correo_par, id_cli1, nombreE FROM participantes");
                while (participantes.Read())
                {

                    part.nombreE = Convert.ToString(participantes["nombreE"]);
                    part.ci_participante = Convert.ToInt32(participantes["cedula_par"]);
                    part.id_cli1 = Convert.ToInt32(participantes["id_cli1"]);
                    part.nombreP = Convert.ToString(participantes["nombre_par"]);
                    part.apellidoP = Convert.ToString(participantes["apellido_par"]);
                    part.correoP = Convert.ToString(participantes["correo_par"]);
                    dgvParticipantes.Rows.Add(part.ci_participante, part.nombreP, part.apellidoP, part.correoP, part.nombreE);
                    
                }
                dgvParticipantes.ClearSelection();
                participantes.Close();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void refrescar()
        {
            llenarDGV();
            txtBuscarNombre.Clear();
            cmbxEstatus.SelectedIndex = -1;
            dtpFechaCurso.Value = DateTime.Today;
        } 

        /*----------------------EVENTOS-------------------*/
        private void btnModificar_Click(object sender, EventArgs e)
        {
            Modificar_participante par = new Modificar_participante();
            par.ShowDialog();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (txtBuscarNombre.Text == "" && cmbxEstatus.SelectedIndex == -1)
            {
                errorProviderCmbxNombre.SetError(txtBuscarNombre, "Debe escribir el nombre de una formación.");
                errorProviderCmbxEstatus.SetError(cmbxEstatus, "Debe seleccionar un estatus.");
                txtBuscarNombre.Focus();
            }
            else
            {
                errorProviderCmbxNombre.SetError(txtBuscarNombre, "");
                errorProviderCmbxEstatus.SetError(cmbxEstatus, "");
                if (txtBuscarNombre.Text == "")
                {
                    errorProviderCmbxNombre.SetError(txtBuscarNombre, "Debe escribir el nombre de una formación.");
                    txtBuscarNombre.Focus();
                }
                else
                {
                    errorProviderCmbxNombre.SetError(txtBuscarNombre, "");
                    if (cmbxEstatus.SelectedIndex == -1)
                    {
                        errorProviderCmbxEstatus.SetError(cmbxEstatus, "Debe seleccionar un estatus.");
                        cmbxEstatus.Focus();
                    }
                    else
                    {
                        resultado = 0;
                        errorProviderCmbxEstatus.SetError(cmbxEstatus, "");
                        string nombre = txtBuscarNombre.Text;
                        string estatus = formaciones.estatus;
                        //aqui debe estar el metodo que permita llenar el dgv con los parametros de busqueda
                        if (resultado == 1)
                        {
                            dgvParticipantes.CurrentRow.Selected = true;
                            resultado = 0;
                            txtBuscarNombre.Clear();
                            cmbxEstatus.SelectedIndex = -1;
                            dtpFechaCurso.Value = DateTime.Today;

                        }
                        else
                        {
                            MessageBox.Show("La búsqueda no ha arrojado resultados.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            refrescar();
                        }
                    }
                }
            }
        }

        private void btnRefrescar_Click(object sender, EventArgs e)
        {
            refrescar();
        }
    }
}
