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
    public partial class Registrar_INCES : Form
    {
        Clases.conexion_bd conexion = new Clases.conexion_bd();
        Clases.INCES curso = new Clases.INCES();
        Clases.Fa_ince fa = new Clases.Fa_ince();
        

        public Registrar_INCES()
        {
            InitializeComponent();
        }

        private void llenarcombo()
        {
            
            cmbxFacilitador.ValueMember = "id_facilitador";            
            cmbxFacilitador.DisplayMember = "nombreyapellido";
            cmbxFacilitador.DataSource = Clases.Paneles.LlenarComboboxFacilitadoresINCEs();
            cmbxFacilitador.SelectedIndex = -1;
            
        }          

        private void Registrar_INCES_Load(object sender, EventArgs e)
        {
            if(Curso_IN.In == 1)
            {
                llenarcombo();
            }else
            {
                llenarcomboTodoFa();
            }
           
            txtNombreCurso.Focus();
        }
       

        private void llenarcomboTodoFa()
        {
           
            cmbxFacilitador.ValueMember = "id_faci";
            cmbxFacilitador.DisplayMember = "nombreyapellido1";
            cmbxFacilitador.DataSource = Paneles.LlenarCmbxFaTodos();
            cmbxFacilitador.SelectedIndex = -1;
        }
        private void cmbxFacilitador_SelectionChangeCommitted(object sender, EventArgs e)
        {
            fa.id_facilitador = Convert.ToInt32(cmbxFacilitador.SelectedValue);
            
        }

        private void txtNombreCurso_KeyPress(object sender, KeyPressEventArgs e)
        {
            Clases.Paneles.sololetras(e);
            if (txtNombreCurso.Text.Length == 0)
            {
                e.KeyChar = e.KeyChar.ToString().ToUpper().ToCharArray()[0];
            }
            else if (txtNombreCurso.Text.Length > 0)
            {
                e.KeyChar = e.KeyChar.ToString().ToLower().ToCharArray()[0];
            }
            if ((int)e.KeyChar == (int)Keys.Enter)
            {
                cmbxFacilitador.Focus();
            }
        }        
        private void txtNombreCurso_Validating(object sender, CancelEventArgs e)
        {
            if (txtNombreCurso.Text == "")
            {
                errorProviderNombreCurso.SetError(txtNombreCurso, "Debe proporcionar un nombre válido.");
                txtNombreCurso.Focus();
            }else
            {
                errorProviderNombreCurso.SetError(txtNombreCurso, "");
                cmbxFacilitador.Focus();
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            guardar();
        }

        private void guardar()
        {
            try
            {
                if (txtNombreCurso.Text == "")
                {
                    errorProviderNombreCurso.SetError(txtNombreCurso, "Debe proporcionar un nombre válido.");
                    txtNombreCurso.Focus();
                }
                else
                {
                    errorProviderNombreCurso.SetError(txtNombreCurso, "");
                    if (cmbxFacilitador.SelectedIndex == -1)
                    {
                        errorProviderFacilitador.SetError(cmbxFacilitador, "Debe seleccionar un facilitador.");
                        cmbxFacilitador.Focus();
                    }
                    else //si todo ok
                    {
                        errorProviderFacilitador.SetError(cmbxFacilitador, "");
                        if (Curso_IN.In == 1) //SI viene referenciado de CURSOS_INCES
                        {

                            //el id ya lo tengo del selection commited
                            if (conexion.abrirconexion() == true)
                            {
                                curso.nombre_cursoINCE = txtNombreCurso.Text;
                                //verificar que no exista en la base de datos
                                int existe = Clases.INCES.CursoExiste(conexion.conexion, curso.nombre_cursoINCE);
                                conexion.cerrarconexion();
                                if (existe == 0)//si no existe
                                {
                                    if (conexion.abrirconexion() == true)
                                    {
                                        //agregar a la base de datos
                                        int agregado = Clases.INCES.AgregarCurso(conexion.conexion, curso);
                                        conexion.cerrarconexion();
                                        if (agregado > 0)//curso agregado exitosamente
                                        {
                                            if (conexion.abrirconexion() == true)
                                            {
                                                //seleccionar el id del curso añadido
                                                int id_curso = Clases.INCES.CursoExiste(conexion.conexion, curso.nombre_cursoINCE);
                                                conexion.cerrarconexion();

                                                if (conexion.abrirconexion() == true)
                                                {
                                                    //verificar que no haya asignacion existente
                                                    int asignacion = Clases.INCES.AsignacionExiste(conexion.conexion, id_curso, fa.id_facilitador);
                                                    conexion.cerrarconexion();
                                                    //si el id que retorna no es el id_fa, se puede asignar
                                                    if (asignacion == 0 || asignacion != fa.id_facilitador)
                                                    {
                                                        errorProviderFacilitador.SetError(cmbxFacilitador, "");
                                                        if (conexion.abrirconexion() == true)
                                                        {
                                                            //asignando curso a facilitador
                                                            int asignado = Clases.INCES.AgregarAsignacion(conexion.conexion, fa.id_facilitador, id_curso);
                                                            conexion.cerrarconexion();
                                                            if (asignado > 0)
                                                            {
                                                                MessageBox.Show("Curso registrado con éxito.", "AVISO", MessageBoxButtons.OK);
                                                                this.Close();
                                                            }
                                                        }
                                                    }
                                                    else if (asignacion == fa.id_facilitador)
                                                    {
                                                        errorProviderFacilitador.SetError(cmbxFacilitador, "El facilitador ya está asignado a este curso.");
                                                        cmbxFacilitador.SelectedIndex = -1;
                                                        cmbxFacilitador.Focus();
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            MessageBox.Show("No se pudo agregar el curso.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        }
                                    }

                                }
                                else
                                {
                                    MessageBox.Show("Este curso ya se encuentra registrado.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    txtNombreCurso.Clear();
                                }

                            }

                        }
                        else //Si viene reverenciado de CURSOS_AFI
                        {
                            int id_curs = 0;
                            string nombre_curso = txtNombreCurso.Text;
                            MySqlDataReader id = Conexion.ConsultarBD("SELECT id_curso_afi from cursos_afi where nombre_curso_afi='" + nombre_curso + "'");
                            if (id.Read())
                            {
                                id_curs = Convert.ToInt32(id["id_curso_afi"]);
                            }
                            id.Close();
                            if(id_curs == 0)
                            {
                                MySqlDataReader add = Conexion.ConsultarBD("INSERT INTO cursos_afi (nombre_curso_afi) VALUES ('"+ nombre_curso + "')");
                                add.Close();
                                MySqlDataReader idc = Conexion.ConsultarBD("SELECT id_curso_afi from cursos_afi where nombre_curso_afi='" + nombre_curso + "'");
                                if (idc.Read())
                                {
                                    id_curs = Convert.ToInt32(idc["id_curso_afi"]);
                                }
                                idc.Close();
                                int asignacion = 0;
                                MySqlDataReader asig = Conexion.ConsultarBD("SELECT id_fa FROM afi_tiene_facilitadores WHERE id_fa='"+fa.id_facilitador+"' AND id_cursos_afi='"+id_curs+"'");
                                if (asig.Read())
                                {
                                    asignacion = Convert.ToInt32(asig["id_fa"]);
                                }
                                asig.Close();
                                //si el id que retorna no es el id_fa, se puede asignar
                                if (asignacion == 0 || asignacion != fa.id_facilitador)
                                {
                                    MySqlDataReader leer = Conexion.ConsultarBD("INSERT INTO afi_tiene_facilitadores (id_cursos_afi, id_fa) VALUES ('" + id_curs + "','" + fa.id_facilitador + "' ) ");
                                    leer.Close();

                                    MessageBox.Show("Curso registrado con éxito.", "AVISO", MessageBoxButtons.OK);
                                    this.Close();

                                }
                                else if (asignacion == fa.id_facilitador)
                                {
                                    errorProviderFacilitador.SetError(cmbxFacilitador, "El facilitador ya está asignado a este curso.");
                                    cmbxFacilitador.SelectedIndex = -1;
                                    cmbxFacilitador.Focus();
                                }
                            }
                            else
                            {
                                MessageBox.Show("Este curso ya se encuentra registrado.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                txtNombreCurso.Clear();
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
