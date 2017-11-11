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
    public partial class Cursos_INCES : Form
    {
        Clases.conexion_bd conexion = new Clases.conexion_bd();
        Clases.INCES ince = new Clases.INCES();
        Clases.Facilitadores fa = new Clases.Facilitadores();

        int retorno = 0;
        string buscar = "";
        public Cursos_INCES()
        {
            InitializeComponent();
            dgvInce.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            //PARA SELECCIONAR
            dgvInce.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvInce.MultiSelect = false;
            dgvInce.ClearSelection();
        }

        private void Cursos_INCES_Load(object sender, EventArgs e)
        {
            try
            {
                this.Location = new Point(-5, 0);
                refrescar();
                conexion.cerrarconexion();

                
                llenarcombo();
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show(ex.Message);


            }
            
        }

        private void btnModificarCurso_Click(object sender, EventArgs e)
        {
            if(cmbxCurso.SelectedIndex == 1)
            {
                //aqui poner de referencia la ventana nueva para pasarle los parámetros (id y nombre curso)
                refrescar();
                
                llenarcombo();
            }
            
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            Registrar_INCES ince = new Registrar_INCES();
            ince.ShowDialog();
            refrescar();
           
            llenarcombo();
        }

        private void CargarDatosTabla(MySqlConnection conexion, string buscar)//filtrado por nombre curso
        {
            try
            {
              
                MySqlCommand cmd = new MySqlCommand(String.Format("SELECT ince.nombre_curso_ince, fa.nombre_fa, fa.apellido_fa, fa.cedula_fa FROM cursos_inces ince inner join inces_tiene_facilitadores itf on ince.id_curso_ince = itf.id_curso_INCE inner join facilitadores fa on itf.id_fa_INCE = fa.id_fa WHERE ince.nombre_curso_ince LIKE ('%{0}%')", buscar), conexion);
                MySqlDataReader reader = cmd.ExecuteReader();

                dgvInce.Rows.Clear();
                while (reader.Read())
                {
                    Clases.Insumos ins = new Clases.Insumos();
                    ins.contenido_insumo = reader.GetString(0);
                    fa.nombre_facilitador =reader.GetString(1);
                    fa.apellido_facilitador = reader.GetString(2);
                    fa.ci_facilitador = reader.GetString(3);
                    dgvInce.Rows.Add(ins.contenido_insumo, fa.nombre_facilitador, fa.apellido_facilitador, fa.ci_facilitador);
                    retorno = 1;
                }
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show(ex.Message);


            }
            
        }

        private void refrescar()
        {
            dgvInce.ReadOnly = true;
            try
            {
                if (conexion.abrirconexion() == true)
                {
                    buscar = "";
                    
                    CargarDatosTabla(conexion.conexion, buscar);
                    dgvInce.ClearSelection();

                    conexion.cerrarconexion();
                }
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                conexion.cerrarconexion();
            }
        }

        private void BuscarMatch(MySqlConnection conexion, string buscar)
        {
            int resultado;
            CargarDatosTabla(conexion, buscar);

            resultado = retorno;
            if (resultado == 1)
            {

                //aqui, si se encuentra un resultado compatible, se debe seleccionar la fila que corresponda con el match
                dgvInce.CurrentRow.Selected = true;
                retorno = 0;
            }
            else
            {
                MessageBox.Show("No se ha encontrado ninguna concordancia con los datos introducidos", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            dgvInce.ReadOnly = true;
            try
            {
                if (cmbxCurso.SelectedIndex != -1)
                {
                    errorProvidercmbx.SetError(cmbxCurso, "");
                    if (conexion.abrirconexion() == true)
                    {
                        buscar = cmbxCurso.DisplayMember;

                        BuscarMatch(conexion.conexion, buscar);
                        
                        conexion.cerrarconexion();
                        buscar = "";
                    }


                }
                else
                {
                    errorProvidercmbx.SetError(cmbxCurso, "Debe seleccionar un curso antes.");
                    cmbxCurso.Focus();
                    
                }


            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                conexion.cerrarconexion();
            }
        }

        private void dgvince_MouseClick(object sender, MouseEventArgs e)
        {
            //cuando se selecciona una fila del dgv, se toman los valores de nombre_curso y la cédula del facilitador 
            //para la posible eliminacion de asignacion 
            dgvInce.ReadOnly = true;
            if(dgvInce.SelectedRows.Count == 1)
            {
                Clases.INCES.nombre_curso = dgvInce.SelectedRows[0].Cells[0].Value.ToString();
                Clases.Facilitador_Seleccionado.ci_facilitador = dgvInce.SelectedRows[0].Cells[3].Value.ToString();
                
            }
        }

        private void llenarcombo()
        {

            string buscar1 = "";
            //llenar el combobox con las empresas registradas:
            cmbxCurso.ValueMember = "id_curso_ince";
            cmbxCurso.DisplayMember = "nombre_curso_ince";
            cmbxCurso.DataSource = Clases.Paneles.LlenarComboboxCursos(buscar1);
            
            cmbxCurso.SelectedIndex = -1;
            
        }

        private void cmbxCurso_SelectionChangeCommitted(object sender, EventArgs e)
        {
            ince.id_cursoINCE = Convert.ToInt32(cmbxCurso.SelectedValue);
        }

        private void btnRefrescar_Click(object sender, EventArgs e)
        {
            
            refrescar();
            llenarcombo();
        }
    }
}
