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

namespace UCS_NODO_FGC
{
    public partial class Cursos_AFI : Form
    {
        Facilitadores fa = new Clases.Facilitadores();
        conexion_bd conexion = new Clases.conexion_bd();
        Curso_AFI afi = new Curso_AFI();
        int retorno = 0;
        string buscar = "", nombre_curso="";
        public Cursos_AFI()
        {
            InitializeComponent();
            dgvInce.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            //PARA SELECCIONAR
            dgvInce.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvInce.MultiSelect = false;
            dgvInce.ClearSelection();
        }

       

        private void Cursos_AFI_Load(object sender, EventArgs e)
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
                MessageBox.Show("Ha ocurrido una error: " + ex.Message);
            }
        }

        //--------METODOS----------------
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

        public List<Curso_AFI> SeleccionarAFI(string b)
        {
            List<Curso_AFI> lista = new List<Curso_AFI>();
            MySqlDataReader leer = Conexion.ConsultarBD("SELECT * FROM cursos_afi WHERE nombre_curso_afi LIKE '"+b+"'");
            while (leer.Read())
            {
                Curso_AFI c = new Curso_AFI();
                c.id_AFI = Convert.ToInt32(leer["id_curso_afi"]);
                c.nombre_AFI = Convert.ToString(leer["nombre_curso_afi"]);
                lista.Add(c);
            }
            return lista;
        }

        private void llenarcombo()
        {

            string buscar1 = "";
            //llenar el combobox con los cursos inces registradas:
            cmbxCurso.ValueMember = "id_AFI";
            cmbxCurso.DisplayMember = "nombre_AFI";
            cmbxCurso.DataSource =SeleccionarAFI(buscar1);

            cmbxCurso.SelectedIndex = -1;

        }

        private void CargarDatosTabla(MySqlConnection conexion, string buscar)//filtrado por nombre curso
        {
            try
            {
                string nombre_curso = "";
                MySqlCommand cmd = new MySqlCommand(String.Format("SELECT afi.nombre_curso_afi, fa.nombre_fa, fa.apellido_fa, fa.cedula_fa FROM cursos_afi afi inner join afi_tiene_facilitadores atf on afi.id_curso_afi = atf.id_cursos_afi inner join facilitadores fa on atf.id_fa = fa.id_fa WHERE afi.nombre_curso_afi LIKE ('%{0}%')", buscar), conexion);
                MySqlDataReader reader = cmd.ExecuteReader();

                dgvInce.Rows.Clear();
                while (reader.Read())
                {

                    nombre_curso = reader.GetString(0);
                    fa.nombre_facilitador = reader.GetString(1);
                    fa.apellido_facilitador = reader.GetString(2);
                    fa.ci_facilitador = reader.GetString(3);
                    dgvInce.Rows.Add(nombre_curso, fa.nombre_facilitador, fa.apellido_facilitador, fa.ci_facilitador);
                    retorno = 1;
                }
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show(ex.Message);


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

        //--------EVENTOS-----------------
        private void btnRefrescar_Click(object sender, EventArgs e)
        {
            refrescar();
            llenarcombo();
        }
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            dgvInce.ReadOnly = true;
            try
            {
                if (cmbxCurso.SelectedIndex != -1)
                {
                    errorProvidercmbx.SetError(cmbxCurso, "");
                    MySqlDataReader leer = Conexion.ConsultarBD("SELECT nombre_curso_afi FROM cursos_afi WHERE id_curso_afi='" + afi.id_AFI + "'");
                    if (leer.Read())
                    {
                        buscar = Convert.ToString(leer["nombre_curso_afi"]);
                    }

                    BuscarMatch(buscar);                    
                    buscar = "";
                    
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
        private void cmbxCurso_SelectionChangeCommitted(object sender, EventArgs e)
        {
            afi.id_AFI = Convert.ToInt32(cmbxCurso.SelectedValue);
            afi.nombre_AFI = Convert.ToString(cmbxCurso.Text);
        }
        private void dgvince_MouseClick(object sender, MouseEventArgs e)
        {
            //cuando se selecciona una fila del dgv, se toman los valores de nombre_curso y la cédula del facilitador 
            //para la posible eliminacion de asignacion 
            dgvInce.ReadOnly = true;
            if (dgvInce.SelectedRows.Count == 1)
            {
                nombre_curso = dgvInce.SelectedRows[0].Cells[0].Value.ToString();
                Facilitador_Seleccionado.ci_facilitador = dgvInce.SelectedRows[0].Cells[3].Value.ToString();

            }
        }
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            Curso_IN.In = 0;
            Registrar_INCES ince = new Registrar_INCES();
            ince.ShowDialog();
            refrescar();

            llenarcombo();
        }
        private void btnAsignarCurso_Click(object sender, EventArgs e)
        {
            Curso_IN.In = 0;
            Asignar_INCE asignar = new Asignar_INCE();
            asignar.ShowDialog();
            refrescar();
        }
        private void btnModificarCurso_Click(object sender, EventArgs e)
        {
            if (cmbxCurso.SelectedIndex == -1)
            {
                errorProvidercmbx.SetError(cmbxCurso, "Debe seleccionar un curso antes.");
                cmbxCurso.Focus();
            }
            else
            {
                errorProvidercmbx.SetError(cmbxCurso, "");
                MySqlDataReader leer = Conexion.ConsultarBD("SELECT nombre_curso_afi FROM cursos_afi WHERE id_curso_afi='" + afi.id_AFI + "'");
                if (leer.Read())
                {
                    buscar = Convert.ToString(leer["nombre_curso_afi"]);
                }

                //aqui poner de referencia la ventana nueva para pasarle los parámetros (id y nombre curso)
                Curso_AFI.id_cursos_afi =afi.id_AFI;
                Curso_AFI.nombre_cursos_afi = buscar;
                Curso_IN.In = 0;
                Modificar_INCE modi = new Modificar_INCE();
                modi.ShowDialog();
                cmbxCurso.SelectedIndex = -1;
                Curso_AFI.id_cursos_afi = 0;
                Curso_AFI.nombre_cursos_afi = "";
                refrescar();
                llenarcombo();
            }

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

                    errorProvidercmbx.SetError(cmbxCurso, "");

                    if (MessageBox.Show("¿Está seguro de eliminar el curso: " + afi.nombre_AFI + "?", "ALERTA", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        MySqlDataReader delA = Conexion.ConsultarBD("DELETE FROM afi_tiene_facilitadores WHERE id_cursos_afi ='" + afi.id_AFI + "' ");
                        MySqlDataReader delC = Conexion.ConsultarBD("DELETE FROM cursos_afi WHERE id_curso_afi ='" + afi.id_AFI + "' ");

                        refrescar();
                        llenarcombo();
                        afi.id_AFI = 0;

                    }
                    else
                    {
                        refrescar();
                        llenarcombo();
                        afi.id_AFI = 0;
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
                if (dgvInce.SelectedRows.Count == 1)
                {
                    if (MessageBox.Show("¿Está seguro de eliminar esta asignación? ", "ALERTA", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        if (conexion.abrirconexion() == true)
                            fa.id_facilitador = Clases.Facilitadores.FacilitadorExiste(conexion.conexion, Clases.Facilitador_Seleccionado.ci_facilitador);
                        conexion.cerrarconexion();

                        int id_curs = 0;
                        MySqlDataReader id = Conexion.ConsultarBD("SELECT id_curso_afi from cursos_afi where nombre_curso_afi='" + nombre_curso + "'");
                        if (id.Read())
                        {
                            id_curs = Convert.ToInt32(id["id_curso_afi"]);
                        }

                        MySqlDataReader del = Conexion.ConsultarBD("DELETE FROM afi_tiene_facilitadores WHERE id_fa='" + fa.id_facilitador + "' AND id_cursos_afi ='" + id_curs + "' ");
                        refrescar();

                    }
                    else
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
