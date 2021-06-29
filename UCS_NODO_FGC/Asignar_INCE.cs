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
            if (Curso_IN.In == 0)
            {
                llenarcomboAfi();
            }
            else if (Curso_IN.In == 1)
            {

                llenarcomboCurso();
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
                        if (Curso_IN.In == 1)//si viene referenciado de cursos inces
                        {
                            conexion.cerrarconexion();
                            if (conexion.abrirconexion() == true)
                            {
                                //verificar que no exista asignacion 
                                int asignacion = Clases.INCES.AsignacionExiste(conexion.conexion, ince.id_cursoINCE, fa.id_facilitador);
                                conexion.cerrarconexion();
                                //si el id que retorna no es el id_fa, se puede asignar
                                if (asignacion == 0 || asignacion != fa.id_facilitador)
                                {
                                    conexion.cerrarconexion();
                                    if (conexion.abrirconexion() == true)
                                    {
                                        //asignando curso a facilitador
                                        int asignado = Clases.INCES.AgregarAsignacion(conexion.conexion, fa.id_facilitador, ince.id_cursoINCE);
                                        conexion.cerrarconexion();
                                        if (asignado > 0)
                                        {
                                            MessageBox.Show("Curso asignado con éxito.", "AVISO", MessageBoxButtons.OK);
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
                        }else //si viene referenciado desde cursos_afi
                        {
                            int asignacion = 0;
                            MySqlDataReader asig = Conexion.ConsultarBD("SELECT id_fa FROM afi_tiene_facilitadores WHERE id_fa='" + fa.id_facilitador + "' AND id_cursos_afi='" + ince.id_cursoINCE + "'");
                            if (asig.Read())
                            {
                                asignacion = Convert.ToInt32(asig["id_fa"]);
                            }
                            asig.Close();
                            //si el id que retorna no es el id_fa, se puede asignar
                            if (asignacion == 0 || asignacion != fa.id_facilitador)
                            {
                                MySqlDataReader leer = Conexion.ConsultarBD("INSERT INTO afi_tiene_facilitadores (id_cursos_afi, id_fa) VALUES ('" + ince.id_cursoINCE + "','" + fa.id_facilitador + "' ) ");
                                leer.Close();

                                MessageBox.Show("Curso asignado con éxito.", "AVISO", MessageBoxButtons.OK);
                                this.Close();
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

        private void llenarcomboAfi()
        {

            string buscar1 = "";
            //llenar el combobox con los cursos inces registradas:
            cmbxCurso.ValueMember = "id_AFI";
            cmbxCurso.DisplayMember = "nombre_AFI";
            cmbxCurso.DataSource = Paneles.SeleccionarAFI(buscar1);

            cmbxCurso.SelectedIndex = -1;
            llenarcomboTodoFa();

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

            cmbxFacilitador.ValueMember = "id_facilitador";
            cmbxFacilitador.DisplayMember = "nombreyapellido";
            cmbxFacilitador.DataSource = Clases.Paneles.LlenarComboboxFacilitadoresINCEs();
            cmbxFacilitador.SelectedIndex = -1;

        }
        private void llenarcomboTodoFa()
        {

            cmbxFacilitador.ValueMember = "id_faci";
            cmbxFacilitador.DisplayMember = "nombreyapellido1";
            cmbxFacilitador.DataSource = Paneles.LlenarCmbxFaTodos();
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

        private void cmbxCurso_Leave(object sender, EventArgs e)
        {
            
            List<int> lista_id = new List<int>();
            if (Curso_IN.In == 0) //si viene referenciado desde cursos AFI
            {
                MySqlDataReader nombreSolicitud = Conexion.ConsultarBD("SELECT id_fa FROM afi_tiene_facilitadores WHERE id_cursos_afi= '" + ince.id_cursoINCE + "'");
                while (nombreSolicitud.Read())
                {
                    int n = Convert.ToInt32(nombreSolicitud["id_fa"]);
                    lista_id.Add(n);

                }
                nombreSolicitud.Close();
            }else //si viene de cursos inces
            {
                MySqlDataReader inces = Conexion.ConsultarBD("SELECT id_fa_INCE FROM inces_tiene_facilitadores WHERE id_curso_INCE='" + ince.id_cursoINCE + "' ");
                while (inces.Read())
                {
                    int n = Convert.ToInt32(inces["id_fa_INCE"]);
                    lista_id.Add(n);
                }
            }
            List<Facilitador_todos> lista = new List<Facilitador_todos>();
            List<Fa_ince> lista1 = new List<Fa_ince>();

            if (Curso_IN.In == 0) //si viene referenciado desde cursos AFI
            {
                lista = Paneles.LlenarCmbxFaTodos();
                for (int x = 0; x < lista_id.Count; x++)
                {

                    for (int i = 0; i < lista.Count; i++)
                    {
                        if (lista[i].id_faci == lista_id[x])
                        {
                            lista.RemoveAt(i);
                        }
                    }

                }
                cmbxFacilitador.ValueMember = "id_faci";
                cmbxFacilitador.DisplayMember = "nombreyapellido1";
                cmbxFacilitador.DataSource = lista;
                cmbxFacilitador.SelectedIndex = -1;
            }
            else
            {
                lista1 = Paneles.LlenarComboboxFacilitadoresINCEs();

                for (int x = 0; x < lista_id.Count; x++)
                {
                    for (int i = 0; i < lista1.Count; i++)
                    {
                        if (lista1[i].id_facilitador == lista_id[x])
                        {
                            lista1.RemoveAt(i);
                        }
                    }
                }
                cmbxFacilitador.ValueMember = "id_facilitador";
                cmbxFacilitador.DisplayMember = "nombreyapellido";
                cmbxFacilitador.DataSource = lista1;
                cmbxFacilitador.SelectedIndex = -1;
            }

        }
    }
}
