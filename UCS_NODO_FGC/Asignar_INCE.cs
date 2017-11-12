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
    public partial class Asignar_INCE : Form
    {
        Clases.conexion_bd conexion = new Clases.conexion_bd();
        Clases.INCES ince = new Clases.INCES();
        Clases.Facilitadores fa = new Clases.Facilitadores();
        
        public Asignar_INCE()
        {
            InitializeComponent();
        }

        private void Asignar_INCE_Load(object sender, EventArgs e)
        {
            cmbxCurso.Focus();
            llenarcomboCurso();
            
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

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            Asignar();

        }

        public void Asignar()
        {
            try
            {
                if (cmbxCurso.SelectedIndex == -1)
                {
                    errorProvidercurso.SetError(cmbxCurso, "Debe seleccionar un curso.");
                    cmbxCurso.Focus();
                }
                else
                {
                    errorProvidercurso.SetError(cmbxCurso, "");
                    if (cmbxFacilitador.SelectedIndex == -1)
                    {
                        errorProviderfacilitador.SetError(cmbxFacilitador, "Debe seleccionar un facilitador.");
                        cmbxFacilitador.Focus();
                    }
                    else
                    {
                        errorProviderfacilitador.SetError(cmbxFacilitador, "");
                        conexion.cerrarconexion();
                        if (conexion.abrirconexion() == true)
                        {
                            //verificar que no exista asignacion existente
                            int asignacion = Clases.INCES.AsignacionExiste(conexion.conexion, ince.id_cursoINCE, fa.id_facilitador);
                            conexion.cerrarconexion();
                            //si el id que retorna no es el id_fa, se puede asignar
                            if (asignacion == 0 || asignacion != fa.id_facilitador)
                            {
                                
                                if (conexion.abrirconexion() == true)
                                {
                                    //asignando curso a facilitador
                                    int asignado = Clases.INCES.AgregarAsignacion(conexion.conexion, fa.id_facilitador, ince.id_cursoINCE);
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
                                errorProviderfacilitador.SetError(cmbxFacilitador, "El facilitador ya está asignado a este curso.");
                                cmbxFacilitador.SelectedIndex = -1;
                                cmbxFacilitador.Focus();                               
                            }
                        }

                    }
                }
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show("Ha ocurrido una error: " + ex.Message);
            }
        }

        private void llenarcomboCurso()
        {

            string buscar1 = "";
            //llenar el combobox con los cursos inces registradas:
            cmbxCurso.ValueMember = "id_INCE";
            cmbxCurso.DisplayMember = "nombre_INCE";
            cmbxCurso.DataSource = Clases.Paneles.LlenarComboboxCursos(buscar1);

            cmbxCurso.SelectedIndex = -1;
            llenarcomboFa();

        }
        private void llenarcomboFa()
        {
            //llenar el combobox con las empresas registradas:
            cmbxFacilitador.ValueMember = "id_facilitador";
            cmbxFacilitador.DisplayMember = "nombreyapellido";
            cmbxFacilitador.DataSource = Clases.Paneles.LlenarComboboxFacilitadoresINCEs();
            cmbxFacilitador.SelectedIndex = -1;

        }

        private void cmbxCurso_SelectionChangeCommitted(object sender, EventArgs e)
        {
            ince.id_cursoINCE = Convert.ToInt32(cmbxCurso.SelectedValue);
        }

        private void cmbxFacilitador_SelectionChangeCommitted(object sender, EventArgs e)
        {
            fa.id_facilitador = Convert.ToInt32(cmbxFacilitador.SelectedValue);
        }

        private void cmbxFacilitador_Validating(object sender, CancelEventArgs e)
        {
            if (cmbxFacilitador.SelectedIndex == -1)
            {
                errorProviderfacilitador.SetError(cmbxFacilitador, "Debe seleccionar un facilitador.");
                cmbxFacilitador.Focus();
            }else
            {
                errorProviderfacilitador.SetError(cmbxFacilitador, "");
            }
        }

        private void cmbxCurso_Validating(object sender, CancelEventArgs e)
        {
            if(cmbxCurso.SelectedIndex == -1)
            {
                errorProvidercurso.SetError(cmbxCurso, "Debe seleccionar un curso.");
                cmbxCurso.Focus();
            }else
            {
                errorProvidercurso.SetError(cmbxCurso, "");
            }
        }
    }
}
