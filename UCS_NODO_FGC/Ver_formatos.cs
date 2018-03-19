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
using UCS_NODO_FGC.Clases;
using System.IO;
using System.Diagnostics;

namespace UCS_NODO_FGC
{
    public partial class Ver_formatos : Form
    {
        conexion_bd conexion = new conexion_bd();
        string origenArchivo, nombreArchivo;
        string todo = "";
        int resultado = 0;
        string destino;
        public Ver_formatos()
        {
            InitializeComponent();
            dgvFormatos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            //PARA SELECCIONAR
            dgvFormatos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvFormatos.MultiSelect = false;
            dgvFormatos.ClearSelection();
        }

        private void Ver_formatos_Load(object sender, EventArgs e)
        {
            this.Location = new Point(-5, 0);
            
            llenarDGV(todo);
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
                            dgvFormatos.CurrentRow.Selected = true;
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

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            OpenFileDialog od = new OpenFileDialog();
            od.Filter = "PDF files |*.pdf";
            if (od.ShowDialog() == DialogResult.OK)
            {
                origenArchivo = od.FileName;
                nombreArchivo = Path.GetFileName(origenArchivo);
                todo = "";
                //dizque validacion
                MySqlDataReader existe = Conexion.ConsultarBD("SELECT id_formato FROM formatos WHERE ruta_archivo LIKE '%" + origenArchivo + "%' ");
                
                if(existe.Read())
                {
                    MessageBox.Show("Este formato ya se encuentra añadido", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }else
                {
                    existe.Close();

                    //esto se cambia cuando vaya a instalarlo en ucs
                    string ruta = @"C:\Users\ZM\Documents\Last_repo\ucs_sistema\UCS_NODO_FGC\Archivos\Formatos_guardados";

                     destino = @"C:\Users\ZM\Documents\Last_repo\ucs_sistema\UCS_NODO_FGC\Archivos\Formatos_guardados\" + nombreArchivo;
                    if (Directory.Exists(ruta))//verificar si la carpeta existe
                    {
                        //si existe se copia
                        
                        File.Copy(origenArchivo, destino);
                    //    MessageBox.Show("copiando archivo");
                    }else //caso que no exista
                    {
                        //se crea y luego se copia 
                        Directory.CreateDirectory(ruta);
                        File.Copy(origenArchivo, destino);
                    //    MessageBox.Show("creo directorio y copiando archivo");
                    }
                    conexion.cerrarconexion();
                    if (conexion.abrirconexion() == true)
                    {
                        destino = destino.Replace("\\", "/");
                        string query = @"INSERT INTO formatos (nombre_archivo, ruta_archivo) VALUES ('" + nombreArchivo + "', '" + destino + "')";
                        MySqlCommand cmd = new MySqlCommand(query, conexion.conexion);
                        cmd.ExecuteNonQuery();
                        conexion.cerrarconexion();
                    }
                    
                    llenarDGV(todo);
                    origenArchivo ="";
                    nombreArchivo ="";
                }
               

            }
        }

        private void btnBuscarAreas_Click(object sender, EventArgs e)
        {
            resultado = 0;
            if (txtBuscarTodo.Text == "")
            {
                errorProviderBuscar.SetError(txtBuscarTodo, "Debe introducir algun dato para buscar");
                txtBuscarTodo.Focus();
            }else
            {
                errorProviderBuscar.SetError(txtBuscarTodo, "");
                todo = txtBuscarTodo.Text;
                llenarDGV(todo);
                if (resultado == 1)
                {
                    dgvFormatos.CurrentRow.Selected = true;
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

        private void btnEliminarFa_Click(object sender, EventArgs e)
        {
            if (dgvFormatos.SelectedRows.Count == 1)
            {
                MySqlDataReader existe = Conexion.ConsultarBD("SELECT id_formato FROM formatos WHERE nombre_archivo LIKE '%" + nombreArchivo + "%' ");
                if (existe.Read())
                {
                    int i = Convert.ToInt32(existe["id_formato"]);
                    MySqlDataReader eliminar = Conexion.ConsultarBD("DELETE FROM formatos WHERE id_formato = '"+i+"'");
                    nombreArchivo = "";
                    llenarDGV(nombreArchivo);

                }
            }
            else
            {
                MessageBox.Show("Debe seleccionar un archivo.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void dgvFormatos_MouseClick(object sender, MouseEventArgs e)
        {
            if (dgvFormatos.SelectedRows.Count == 1)
            {
                nombreArchivo = dgvFormatos.SelectedRows[0].Cells[0].Value.ToString();
                MySqlDataReader ruta = Conexion.ConsultarBD(@"SELECT ruta_archivo FROM formatos WHERE nombre_archivo LIKE '%" + nombreArchivo + "%'");
                if (ruta.Read())
                {
                    origenArchivo = Convert.ToString(ruta["ruta_archivo"]);
                    origenArchivo = origenArchivo.Replace("\\", "/");

                }
            }
        }

        private void btnVerArchivo_Click(object sender, EventArgs e)
        {
            if (dgvFormatos.SelectedRows.Count == 1)
            {
              
                Process.Start(origenArchivo);

            }
            else
            {
                MessageBox.Show("Debe seleccionar un archivo.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void llenarDGV(string buscar)
        {
            
            dgvFormatos.Rows.Clear();
            MySqlDataReader formatos = Conexion.ConsultarBD("SELECT * FROM formatos WHERE nombre_archivo LIKE '%" + buscar + "%' OR ruta_archivo LIKE '%" + buscar + "%' ");
            while (formatos.Read())
            {
                dgvFormatos.Rows.Add(formatos["nombre_archivo"]);
                resultado = 1;
            }
            dgvFormatos.ClearSelection();
        }
    }
}
