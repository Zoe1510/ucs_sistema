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
                MessageBox.Show("Ha ocurrido una error: "+ex.Message);
            }
            
        }

        private void btnModificarCurso_Click(object sender, EventArgs e)
        {
            if(cmbxCurso.SelectedIndex == -1)
            {
                errorProvidercmbx.SetError(cmbxCurso, "Debe seleccionar un curso antes.");
                cmbxCurso.Focus();
            }
            else
            {
                errorProvidercmbx.SetError(cmbxCurso, "");
                conexion.cerrarconexion();
                if (conexion.abrirconexion() == true)
                {
                    buscar = Clases.INCES.SeleccionarNombreCurso(conexion.conexion, ince.id_cursoINCE);
                    conexion.cerrarconexion();

                }
                
                //aqui poner de referencia la ventana nueva para pasarle los parámetros (id y nombre curso)
                Clases.INCES.id_curso = ince.id_cursoINCE;
                Clases.INCES.nombre_curso = buscar;
                Modificar_INCE modi = new Modificar_INCE();
                modi.ShowDialog();
                cmbxCurso.SelectedIndex = -1;
                Clases.INCES.id_curso =0;
                Clases.INCES.nombre_curso ="";
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
                Clases.Insumos ins = new Clases.Insumos();
                MySqlCommand cmd = new MySqlCommand(String.Format("SELECT ince.nombre_curso_ince, fa.nombre_fa, fa.apellido_fa, fa.cedula_fa FROM cursos_inces ince inner join inces_tiene_facilitadores itf on ince.id_curso_ince = itf.id_curso_INCE inner join facilitadores fa on itf.id_fa_INCE = fa.id_fa WHERE ince.nombre_curso_ince LIKE ('%{0}%')", buscar), conexion);
                MySqlDataReader reader = cmd.ExecuteReader();

                dgvInce.Rows.Clear();
                while (reader.Read())
                {
                    
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

        private void BuscarMatch(string buscar)
        {
            try
            {
                int resultado;
                if (conexion.abrirconexion() == true)
                {
                    CargarDatosTabla(conexion.conexion, buscar);
                    conexion.cerrarconexion();
                }
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
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                conexion.cerrarconexion();
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
                        buscar = Clases.INCES.SeleccionarNombreCurso(conexion.conexion, ince.id_cursoINCE);
                        MessageBox.Show(buscar);
                        conexion.cerrarconexion();
                        BuscarMatch(buscar);
                        
                        
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
            //llenar el combobox con los cursos inces registradas:
            cmbxCurso.ValueMember = "id_INCE";
            cmbxCurso.DisplayMember = "nombre_INCE";
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

        private void btnAsignarCurso_Click(object sender, EventArgs e)
        {
            Asignar_INCE asignar = new Asignar_INCE();
            asignar.ShowDialog();
            refrescar();
        }

        private void btnEliminarCurso_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbxCurso.SelectedIndex == -1)
                {
                    errorProvidercmbx.SetError(cmbxCurso, "Debe seleccionar un curso antes.");
                    cmbxCurso.Focus();
                }
                else
                {
                    string nombreCurso;
                    errorProvidercmbx.SetError(cmbxCurso, "");
                    conexion.cerrarconexion();
                    if (conexion.abrirconexion() == true)
                    {
                        nombreCurso = Clases.INCES.SeleccionarNombreCurso(conexion.conexion, ince.id_cursoINCE);
                        conexion.cerrarconexion();

                        if (MessageBox.Show("¿Está seguro de eliminar el curso: " + nombreCurso + "?", "ALERTA", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                        {
                            if (conexion.abrirconexion() == true)
                            {
                                int EliminarAsignaciones = Clases.INCES.EliminarTodasAsignaciones(conexion.conexion, ince.id_cursoINCE);
                                conexion.cerrarconexion();
                                if (conexion.abrirconexion() == true)
                                {
                                    int Eliminarcur = Clases.INCES.EliminarCurso(conexion.conexion, ince.id_cursoINCE);
                                    conexion.cerrarconexion();
                                    if (Eliminarcur > 0)
                                    {
                                        refrescar();
                                        llenarcombo();
                                        ince.id_cursoINCE = 0;
                                    }
                                }

                            }
                        }
                        else
                        {
                            refrescar();
                            llenarcombo();
                            ince.id_cursoINCE = 0;
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

        private void btnEliminarAsignacion_Click(object sender, EventArgs e)
        {
            try
            {
                if(dgvInce.SelectedRows.Count == 1)
                {
                    if (MessageBox.Show("¿Está seguro de eliminar esta asignación? ", "ALERTA", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        if (conexion.abrirconexion() == true)
                        {
                            fa.id_facilitador = Clases.Facilitadores.FacilitadorExiste(conexion.conexion, Clases.Facilitador_Seleccionado.ci_facilitador);
                            conexion.cerrarconexion();
                            if (conexion.abrirconexion() == true)
                            {
                                int id_curs = Clases.INCES.CursoExiste(conexion.conexion, Clases.INCES.nombre_curso);
                                conexion.cerrarconexion();
                                if (conexion.abrirconexion() == true)
                                {
                                    int eliminarA = Clases.INCES.EliminarAsignacion(conexion.conexion, id_curs, fa.id_facilitador);
                                    conexion.cerrarconexion();
                                    if (eliminarA > 0)
                                    {
                                        refrescar();
                                    }else
                                    {
                                        MessageBox.Show("Ha ocurrido un error al eliminar la asignación.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                }
                            }
                            
                        }
                    }else
                    {
                        dgvInce.ClearSelection();
                    }
                }
                else
                {
                    MessageBox.Show("Debe seleccionar una asignación de la lista.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
