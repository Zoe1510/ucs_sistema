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
            //llenar el combobox con las empresas registradas:
            cmbxFacilitador.ValueMember = "id_facilitador";            
            cmbxFacilitador.DisplayMember = "nombreyapellido";
            cmbxFacilitador.DataSource = Clases.Paneles.LlenarComboboxFacilitadoresINCEs();
            cmbxFacilitador.SelectedIndex = -1;
            
        }          

        private void Registrar_INCES_Load(object sender, EventArgs e)
        {
            llenarcombo();
            txtNombreCurso.Focus();
        }
        int id = 0;
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
                        curso.nombre_cursoINCE = txtNombreCurso.Text;

                        //el id ya lo tengo del selection commited
                        if (conexion.abrirconexion() == true)
                        {
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
                                                //verificar que no exista asignacion existente
                                                int asignacion = Clases.INCES.AsignacionExiste(conexion.conexion, id_curso, fa.id_facilitador);
                                                conexion.cerrarconexion();
                                                //si el id que retorna no es el id_fa, se puede asignar
                                                if (asignacion == 0 || asignacion != id)
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
                                                else if (asignacion == id)
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
