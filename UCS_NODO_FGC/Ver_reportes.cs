using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UCS_NODO_FGC.Clases;
using MySql.Data.MySqlClient;
using System.IO;
using System.Diagnostics;

namespace UCS_NODO_FGC
{
    public partial class Ver_reportes : Form
    {
        string origenArchivo, nombreArchivo;
        string todo = "";
        int resultado = 0;
        public Ver_reportes()
        {
            InitializeComponent();
            dgvReportes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            //PARA SELECCIONAR
            dgvReportes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvReportes.MultiSelect = false;
            dgvReportes.ClearSelection();
        }

        private void Ver_reportes_Load(object sender, EventArgs e)
        {
            this.Location = new Point(-5, 0);
            llenarDGV(todo);
            if(Usuario_logeado.cargo_usuario!= "Asistente")
            {
                grpbOpciones.Height = 317;
                btnEliminar.Enabled = true;
            }else
            {
                grpbOpciones.Height = 245;
                btnEliminar.Enabled = false;
            }
        }

        private void txtBuscarTodo_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Clases.Paneles.letrasynumeros(e); //revisar metodo, algo falla
            if (txtBuscarTodo.Text.Length == 0)
            {
                e.KeyChar = e.KeyChar.ToString().ToUpper().ToCharArray()[0];
            }
            else if (txtBuscarTodo.Text.Length > 0)
            {
                e.KeyChar = e.KeyChar.ToString().ToLower().ToCharArray()[0];
            }
            if ((int)e.KeyChar == (int)Keys.Enter)
            {

                try
                {
                    if (txtBuscarTodo.Text != "")
                    {
                        todo = txtBuscarTodo.Text;
                        llenarDGV(todo);
                        if (resultado == 1)
                        {
                            dgvReportes.CurrentRow.Selected = true;
                            txtBuscarTodo.Clear();
                        }
                        txtBuscarTodo.Text = "Escriba aquí";


                    }
                    else
                    {
                        MessageBox.Show("Debe introducir algun dato para buscar.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    }


                }
                catch (MySql.Data.MySqlClient.MySqlException ex)
                {
                    MessageBox.Show(ex.Message);

                }
            }
        }
        private void txtBuscarTodo_KeyDown(object sender, KeyEventArgs e)
        {
            while (txtBuscarTodo.Text == "Escriba aquí")
            {
                txtBuscarTodo.Text = "";

            }
        }
        private void txtBuscarTodo_Leave(object sender, EventArgs e)
        {
            if (txtBuscarTodo.Text == "")
            {
                txtBuscarTodo.Text = "Escriba aquí";
            }
        }
        private void txtBuscarTodo_Click(object sender, EventArgs e)
        {

            if (txtBuscarTodo.Text != "")
            {

            }
            else
            {
                txtBuscarTodo.Text = "";
            }
        }

        private void llenarDGV(string b)
        {
            dgvReportes.Rows.Clear();
            MySqlDataReader rpt = Conexion.ConsultarBD("SELECT * FROM reportes WHERE nombre_reporte LIKE '%" + b + "%' OR ruta_reporte LIKE '%" + b + "%' ");
            while (rpt.Read())
            {
                dgvReportes.Rows.Add(rpt["nombre_reporte"]);
                resultado = 1;
            }
            dgvReportes.ClearSelection();
            rpt.Close();
        }

        private void btnBuscarAreas_Click(object sender, EventArgs e)
        {
            resultado = 0;
            if (txtBuscarTodo.Text == "")
            {
                errorProviderBuscar.SetError(txtBuscarTodo, "Debe introducir algun dato para buscar");
                txtBuscarTodo.Focus();
            }
            else
            {
                errorProviderBuscar.SetError(txtBuscarTodo, "");
                todo = txtBuscarTodo.Text;
                llenarDGV(todo);
                if (resultado == 1)
                {
                    dgvReportes.CurrentRow.Selected = true;
                    txtBuscarTodo.Clear();
                    txtBuscarTodo.Text = "Escriba aquí";
                }
            }
        }

        private void btnRefrescar_Click(object sender, EventArgs e)
        {
            todo = "";
            llenarDGV(todo);
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvReportes.SelectedRows.Count == 1)
            {
                if(MessageBox.Show("¿Está seguro de eliminar el reporte seleccionado?","AVISO", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    MySqlDataReader existe = Conexion.ConsultarBD("SELECT id_reporte FROM reportes WHERE nombre_reporte LIKE '%" + nombreArchivo + "%' ");
                    if (existe.Read())
                    {
                        int i = Convert.ToInt32(existe["id_reporte"]);
                        MySqlDataReader eliminar = Conexion.ConsultarBD("DELETE FROM reportes WHERE id_reporte = '" + i + "'");
                        eliminar.Close();
                        nombreArchivo = "";
                        llenarDGV(nombreArchivo);

                    }
                }
                
            }
            else
            {
                MessageBox.Show("Debe seleccionar un archivo.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }


        private void dgvReportes_MouseClick(object sender, MouseEventArgs e)
        {
            if (dgvReportes.SelectedRows.Count == 1)
            {
                nombreArchivo = dgvReportes.SelectedRows[0].Cells[0].Value.ToString();
                MySqlDataReader ruta = Conexion.ConsultarBD(@"SELECT * FROM reportes WHERE nombre_reporte LIKE '%" + nombreArchivo + "%'");
                if (ruta.Read())
                {
                    Reporte_Seleccionado.nombre_reporte = nombreArchivo;
                    origenArchivo = Convert.ToString(ruta["ruta_reporte"]);
                    origenArchivo = origenArchivo.Replace("\"","/");
                    MessageBox.Show(origenArchivo);
                    Reporte_Seleccionado.id_reporte = Convert.ToInt32(ruta["id_reporte"]);
                    Reporte_Seleccionado.fecha_emision = Convert.ToString(ruta["fecha_creacion"]);
                    int idusuario = Convert.ToInt32(ruta["id_creador_usuario"]);
                    MySqlDataReader id = Conexion.ConsultarBD("select nombre_user from usuarios where id_user='"+idusuario+"'");
                    if (id.Read())
                    {
                        Reporte_Seleccionado.creador = Convert.ToString(id["nombre_user"]);
                    }
                    id.Close();
                    

                }
                ruta.Close();
            }
        }

        private void btnDetalles_Click(object sender, EventArgs e)
        {
            if (dgvReportes.SelectedRows.Count == 1)
            {
                Detalle_reporte details = new Detalle_reporte();
                details.ShowDialog();
                Reporte_Seleccionado.id_reporte = 0;
                Reporte_Seleccionado.fecha_emision = "";
                Reporte_Seleccionado.creador = "";
                Reporte_Seleccionado.nombre_reporte = "";
                todo = "";
                llenarDGV(todo);
            }
            else
            {
                MessageBox.Show("Debe seleccionar un archivo.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnVerArchivo_Click(object sender, EventArgs e)
        {
            if (dgvReportes.SelectedRows.Count == 1)
            {

                Process.Start(origenArchivo);

            }
            else
            {
                MessageBox.Show("Debe seleccionar un archivo.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}
