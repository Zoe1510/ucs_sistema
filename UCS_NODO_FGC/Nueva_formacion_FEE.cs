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
    public partial class Nueva_formacion_FEE : Form
    {
        Clases.Formaciones formacion = new Clases.Formaciones();
        Clases.conexion_bd conexion = new Clases.conexion_bd();
        Clases.Tiempos_curso time = new Clases.Tiempos_curso();
        Clases.Facilitadores fa = new Clases.Facilitadores();
        Clases.Facilitadores Cofa = new Clases.Facilitadores();
        Clases.Facilitadores faDatos = new Clases.Facilitadores();
        bool guardar;
        string presentacion = "";
        string contenido = "";
        string manual = "";
        string bitacora = "";
        string bloques = "";
        bool ExisteFormacion;
        public Nueva_formacion_FEE()
        {
            InitializeComponent();
        }

        private void Nueva_formacion_FEE_Load(object sender, EventArgs e)
        {
            if (Clases.Formaciones.creacion == true)//si viene referenciado del boton de la pagina principal
            {//------------------------------------------todo hay que hacerlo aquí(un nuevo ingreso)
                this.Location = new Point(-5, 0);

                LabelCabecera.Text = "Nuevo FEE: Información básica";
                LabelCabecera.Location = new Point(185, 31);

                lblEtapaSiguiente.Text = "Nivel Intermedio";
                lblEtapaSiguiente.Location = new Point(17, 529);

                lblEtapafinal.Text = "Nivel Avanzado";
                lblEtapafinal.Location = new Point(22, 570);

                //como estarán los botones inicialmente para cada nivel
                Load_Sig_Re();

                btnVerPresentacion.Enabled = false;
                btnVerContenido.Enabled = false;
                btnRutaContenido.Enabled = false;
                btnRutaPresentacion.Enabled = false;
                btnRutaBitacora.Enabled = false;              
                btnVerBitacora.Enabled = false;
              

                //controles del nivel intermedio
                Controles_nivel_intermedio_EstatusInicial();


                conexion.cerrarconexion();
                if (conexion.abrirconexion() == true)
                {
                    //aqui de una vez se carga la publicidad existente para el Nivel_intermedio
                    string difu = "";
                    CargarDatosPublicidad(conexion.conexion, difu);
                    dgvMediosDifusion.ClearSelection();
                    conexion.cerrarconexion();
                    if (conexion.abrirconexion() == true)
                    {
                        CargarDatosInsumos(conexion.conexion, difu);
                        dgvInsumos.ClearSelection();
                        conexion.cerrarconexion();
                    }
                    //cargar los facilitadores del nivel_intermedio

                }

                llenarcomboFacilitador();
            }
        }

       /*------------------METODOS------------------------*/
        private void Load_Sig_Re()
        {
            btnRetomar.Enabled = false;
            btnSiguienteEtapa.Enabled = false;
            btnModificar.Enabled = false;
            btnGuardar.Enabled = true;
            btnPausar.Enabled = true;
            btnLimpiar.Enabled = true;
        }

        private void deshabiltarControlesBasico()
        {
            //para el boton pausar en el (nivel_basico)
            txtNombreFormacion.Enabled = false;
            txtSolicitadoPor.Enabled = false;
            cmbxDuracionFormacion.Enabled = false;
            cmbxBloques.Enabled = false;
            btnRutaContenido.Enabled = false;
            btnRutaPresentacion.Enabled = false;
        }
        private void Controles_nivel_intermedio_EstatusInicial()
        {
            gpbRefrigerio.Enabled = false;
            gpbFechaHora.Enabled = true;
            
            gpbDifusion.Enabled = true;
            gpbFacilitador.Enabled = false;
            gpbDatosFa.Enabled = false;
            chkbCoFacilitador.Enabled = false;
            gpbCoFa.Enabled = false;
            gpbDatosCoFa.Enabled = false;
        }

        private void CargarDatosPublicidad(MySqlConnection conexion, string di)
        {
            //usado para cargarla publicidad(Nivel_intermedio)
            try
            {
                MySqlCommand comando = new MySqlCommand(String.Format("SELECT dif_contenido FROM difusion WHERE dif_contenido LIKE ('%{0}%')", di), conexion);
                MySqlDataReader reader = comando.ExecuteReader();

                dgvMediosDifusion.Rows.Clear();
                while (reader.Read())
                {
                    Clases.Difusion dif = new Clases.Difusion();
                    dif.contenido_dif = reader.GetString(0);
                    dgvMediosDifusion.Rows.Add(dif.contenido_dif);
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error: " + ex.Message);


            }
        }
        private void CargarDatosInsumos(MySqlConnection conexion, string buscar)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand(String.Format("SELECT ins_contenido FROM insumos WHERE ins_contenido LIKE ('%{0}%')", buscar), conexion);
                MySqlDataReader reader = cmd.ExecuteReader();

                dgvInsumos.Rows.Clear();
                while (reader.Read())
                {
                    Clases.Insumos ins = new Clases.Insumos();
                    ins.contenido_insumo = reader.GetString(0);

                    dgvInsumos.Rows.Add(ins.contenido_insumo);

                }
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show(ex.Message);


            }
        }

        private void llenarcomboFacilitador()
        {
            //llenar el combobox con las empresas registradas:
            cmbxFa.ValueMember = "id_faci";
            cmbxFa.DisplayMember = "nombreyapellido1";
            cmbxFa.DataSource = Clases.Paneles.LlenarCmbxFaTodos();
            cmbxFa.SelectedIndex = -1;
        }
        private void llenarComboCoFa(int id_facilitador)
        {
            //llenar el combobox con las empresas registradas:
            cmbxCoFa.ValueMember = "id_faci";
            cmbxCoFa.DisplayMember = "nombreyapellido1";
            cmbxCoFa.DataSource = Clases.Paneles.LlenarCmbxCoFa(id_facilitador);
            cmbxCoFa.SelectedIndex = -1;
        }

        private void vaciarFormacion()
        {
            //usado en el nivel_basico
            formacion.duracion = "";
            formacion.pq_inst = 0;
            contenido = "";
            presentacion = "";
            formacion.nombre_formacion = "";
            formacion.id_user = 0;
            txtNombreFormacion.Clear();
            cmbxDuracionFormacion.SelectedIndex = -1;
            cmbxBloques.SelectedIndex = -1;
            txtSolicitadoPor.Clear();

        }
        private void VerPaqueteInst(int id_pq)
        {
            try
            {
                if (MessageBox.Show("Existe un paquete instruccional para esta formación.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
                {
                    conexion.cerrarconexion();
                    if (conexion.abrirconexion() == true)
                    {
                        Clases.Paquete_instruccional pq = new Clases.Paquete_instruccional();
                        pq = Clases.Formaciones.obtenerTodoPq(conexion.conexion, id_pq);
                        conexion.cerrarconexion();
                        Clases.Paquete_instruccional._bitacora = pq.bitacora;
                        Clases.Paquete_instruccional._contenido = pq.contenido;

                        Clases.Paquete_instruccional._manual = pq.manual;
                        Clases.Paquete_instruccional._presentacion = pq.presentacion;
                        Clases.Paquete_instruccional.id_pin = id_pq;
                        Clases.Paquete_instruccional.tipo_curso = formacion.tipo_formacion;
                        Ver_paqueteInstruccional verp = new Ver_paqueteInstruccional();
                        verp.ShowDialog();
                        formacion.pq_inst = id_pq;
                        ExisteFormacion = true;

                    }
                }
                else
                {
                    if (MessageBox.Show("¿Desea utilizar este paquete instruccional para la formación?") == DialogResult.Yes)
                    {
                        formacion.pq_inst = id_pq;
                        ExisteFormacion = true;
                    }
                    else
                    {
                        ExisteFormacion = false;
                    }

                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                conexion.cerrarconexion();

            }
        }

        /*--------------------Botones del panel lateral derecho------------------------*/
        private void btnSiguienteEtapa_Click(object sender, EventArgs e)
        {
            guardar = false;
            //cuando Siguiente etapa, quita el panel actual
            if (pnlNivel_basico.Visible == true)
            {
                LabelCabecera.Text = "Nuevo FEE: Detalles técnicos";
                LabelCabecera.Location = new Point(180, 31);

                lblEtapaSiguiente.Text = "Nivel Avanzado";
                lblEtapaSiguiente.Location = new Point(22, 529);
                lblEtapafinal.Text = "Añadir participantes";
                lblEtapafinal.Location = new Point(3, 570);
                pnlNivel_basico.Visible = false;


                
            }
            else
            {
                if (pnlNivel_intermedio.Visible == true)
                {
                    LabelCabecera.Text = "Nuevo FEE: Logística";
                    LabelCabecera.Location = new Point(250, 31);

                    pnlNivel_intermedio.Visible = false;

                    lblEtapaSiguiente.Location = new Point(3, 529);
                    lblEtapaSiguiente.Text = "Añadir participantes";

                    lblEtapafinal.Location = new Point(3, 570);
                    lblEtapafinal.Text = "Día de la formación";




                    pnlNivel_avanzado.Visible = true;
                    Load_Sig_Re();

                }
            }
        }

        private void btnPausar_Click(object sender, EventArgs e)
        {
            //si pausa, deshabilita los controles:
            deshabiltarControlesBasico();
            //además se deshabilita guardar, pausar, siguiente etapa, modificar, limpiar
            //se habilita retomar

            btnRetomar.Enabled = true; //habilitado

            btnSiguienteEtapa.Enabled = false;
            btnModificar.Enabled = false;
            btnGuardar.Enabled = false;
            btnPausar.Enabled = false;
            btnLimpiar.Enabled = false;
        }



        /* ------------- Controles del nivel básico -------------------*/

        //txtNombreFormacion (Nivel_basico)
        private void txtNombreFormacion_KeyPress(object sender, KeyPressEventArgs e)
        {
            Clases.Paneles.sololetras(e);
            if (txtNombreFormacion.Text.Length == 0)
            {
                e.KeyChar = e.KeyChar.ToString().ToUpper().ToCharArray()[0];
            }
            else if (txtNombreFormacion.Text.Length > 0)
            {
                e.KeyChar = e.KeyChar.ToString().ToLower().ToCharArray()[0];
            }
            if ((int)e.KeyChar == (int)Keys.Enter)
            {
                txtSolicitadoPor.Focus();
            }
        }
        private void txtNombreFormacion_Validating(object sender, CancelEventArgs e)
        {
            if (txtNombreFormacion.Text == "")
            {
                errorProviderNombreF.SetError(txtNombreFormacion, "Debe proporcionar el nombre de la formación.");
                txtNombreFormacion.Focus();
            }
            else
            {
                errorProviderNombreF.SetError(txtNombreFormacion, "");
            }
        }
        private void txtNombreFormacion_Leave(object sender, EventArgs e)
        {
            //validar si existe un curso de cualquier estatus en la base de datos con el mismo nombre
            //para recuperar el paquete instruccional o dejar que el usuario lo escoja
            /*REVISAR, CODIGO TOMADO DESDE FORMACIONES ABIERTAS*/
            try
            {
                if (txtNombreFormacion.Text != "")//siempre y cuando el txt contenga algo, se valida
                {
                    if (conexion.abrirconexion() == true)
                    {
                        formacion.estatus = "En curso"; //predeterminado en esta etapa
                        formacion.tipo_formacion = "FEE"; //predeterminado para este form
                        formacion.nombre_formacion = txtNombreFormacion.Text;
                        conexion.cerrarconexion();
                        if (conexion.abrirconexion() == true)
                        {
                            int existeReprogramado = 0;
                            string statusR = "Reprogramado";
                            existeReprogramado = Clases.Formaciones.CursoOtroStatusExiste(conexion.conexion, formacion, statusR);
                            conexion.cerrarconexion();

                            if (existeReprogramado != 0)//Si existe curso reprogramado
                            {
                                errorProviderNombreF.SetError(txtNombreFormacion, "Ya existe una formacion con este nombre en estado 'Reprogramado'");
                                txtNombreFormacion.Focus();
                                vaciarFormacion();
                                ExisteFormacion = false;//significa que no existe, el usuario podrá establecer su propio paquete instruccional
                                btnRutaContenido.Enabled = true;
                                btnRutaPresentacion.Enabled = true;

                            }
                            else
                            {
                                errorProviderNombreF.SetError(txtNombreFormacion, "");

                                if (conexion.abrirconexion() == true)
                                {
                                    int existeEjecucion = Clases.Formaciones.CursoEjecucionExiste(conexion.conexion, formacion);
                                    conexion.cerrarconexion();

                                    if (existeEjecucion != 0)
                                    {
                                        errorProviderNombreF.SetError(txtNombreFormacion, "Ya existe una formacion con este nombre en estado 'En curso'");
                                        txtNombreFormacion.Focus();
                                        vaciarFormacion();
                                        ExisteFormacion = false;//significa que no existe, el usuario podrá establecer su propio paquete instruccional
                                        btnRutaContenido.Enabled = true;
                                        btnRutaPresentacion.Enabled = true;
                                    }
                                    else
                                    {
                                        errorProviderNombreF.SetError(txtNombreFormacion, "");
                                        //luego de la busqueda en su propio tipo de curso, se podrá añadir el curso
                                        //pero se busca cualquier concordancia de nombre en otros tipos de curso para el paquete instruccional
                                        if (conexion.abrirconexion() == true)
                                        {
                                            List<Clases.Paquete_instruccional> paqueteExiste = new List<Clases.Paquete_instruccional>();
                                            paqueteExiste = Clases.Formaciones.ObtenerPaqueteStatusCursoDistinto(conexion.conexion, formacion);
                                            conexion.cerrarconexion();

                                            if (paqueteExiste.Count != 0)
                                            {

                                                List<int> IdDistintos = new List<int>();
                                                //selecciona solo los números únicos
                                                IdDistintos = paqueteExiste.Select(x => x.id_pinstruccional).Distinct().ToList();

                                                if (IdDistintos.Count == 1)
                                                {

                                                    VerPaqueteInst(IdDistintos[0]);
                                                }

                                            }
                                            else
                                            {

                                                ExisteFormacion = false;//significa que no existe, el usuario podrá establecer su propio paquete instruccional
                                                btnRutaContenido.Enabled = true;
                                                btnRutaPresentacion.Enabled = true;
                                            }

                                        }

                                    }
                                }
                            }
                        }
                    }
                }

            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                conexion.cerrarconexion();

            }
        }


        //txtSolicitadoPor (Nivel_basico)
        private void txtSolicitadoPor_KeyPress(object sender, KeyPressEventArgs e)
        {
            Clases.Paneles.sololetras(e);
            if (txtNombreFormacion.Text.Length == 0)
            {
                e.KeyChar = e.KeyChar.ToString().ToUpper().ToCharArray()[0];
            }
            else if (txtNombreFormacion.Text.Length > 0)
            {
                e.KeyChar = e.KeyChar.ToString().ToLower().ToCharArray()[0];
            }
            if ((int)e.KeyChar == (int)Keys.Enter)
            {
                cmbxDuracionFormacion.Focus();
            }
        }
        private void txtSolicitadoPor_Validating(object sender, CancelEventArgs e)
        {
            if (txtSolicitadoPor.Text == "")
            {
                errorProviderSolicitado.SetError(txtSolicitadoPor, "Debe proporcionar el nombre de quien solicita.");
                txtSolicitadoPor.Focus();
            }
            else
            {
                errorProviderSolicitado.SetError(txtSolicitadoPor, "");
                formacion.solicitado = txtSolicitadoPor.Text;
            }
        }




        /*-------------------- Controles del Nivel_intermedio ------------------------*/
        private void cmbxFa_SelectionChangeCommitted(object sender, EventArgs e)
        {
            fa.id_facilitador = Convert.ToInt32(cmbxFa.SelectedValue);
            llenarComboCoFa(fa.id_facilitador);
        }

        private void cmbxFa_Validating(object sender, CancelEventArgs e)
        {
            //validar si este facilitador estará disponible para esa o esas fechas
            if ((formacion.duracion == "8" && formacion.bloque_curso == "1"))
            {

                //validar si estará disponible para la fecha del dia 1
                if (conexion.abrirconexion() == true)
                {
                    int fa_disponible = Clases.Facilitadores.FacilitadorDisponibleDia1(conexion.conexion, time.fecha_curso);
                    conexion.cerrarconexion();
                    if (fa_disponible == fa.id_facilitador)//si el id que retorna es el del facilitador seleccionado: este ya está ocupado para esa fecha
                    {
                        errorProviderPresentacion.SetError(cmbxFa, "El facilitador está ocupado en la fecha seleccionada.");
                        cmbxFa.SelectedIndex = -1;
                        fa.id_facilitador = 0;
                        cmbxFa.Focus();
                    }
                    else
                    {
                        errorProviderPresentacion.SetError(cmbxFa, "");
                        //si todo bien, cargar los datos en el gpbDatosFa
                        gpbDatosFa.Enabled = true;
                        chkbCoFacilitador.Enabled = true;
                        if (conexion.abrirconexion() == true)
                        {
                            faDatos = Clases.Facilitadores.SeleccionarFaPorID(conexion.conexion, fa.id_facilitador);

                            conexion.cerrarconexion();
                            txtTlfnFa.Text = faDatos.tlfn_facilitador;
                            txtCorreoFa.Text = faDatos.correo_facilitador;
                        }


                    }
                }
            }          
            
        }

        private void cmbxCoFa_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Cofa.id_facilitador = Convert.ToInt32(cmbxCoFa.SelectedValue);
        }

        private void cmbxCoFa_Validating(object sender, CancelEventArgs e)
        {
            //validar si este facilitador estará disponible para esa o esas fechas
            if ((formacion.duracion == "8" && formacion.bloque_curso == "1"))
            {

                //validar si estará disponible para la fecha del dia 1
                if (conexion.abrirconexion() == true)
                {
                    int fa_disponible = Clases.Facilitadores.FacilitadorDisponibleDia1(conexion.conexion, time.fecha_curso);
                    conexion.cerrarconexion();
                    if (fa_disponible == Cofa.id_facilitador)//si el id que retorna es el del facilitador seleccionado: este ya está ocupado para esa fecha
                    {
                        errorProviderPresentacion.SetError(cmbxCoFa, "El Co-facilitador está ocupado en la fecha seleccionada.");
                        cmbxCoFa.SelectedIndex = -1;
                        Cofa.id_facilitador = 0;
                        cmbxCoFa.Focus();
                    }
                    else
                    {
                        errorProviderPresentacion.SetError(cmbxCoFa, "");
                        //si todo bien, cargar los datos en el gpbDatosCoFa
                        gpbDatosCoFa.Enabled = true;

                        if (conexion.abrirconexion() == true)
                        {
                            faDatos = Clases.Facilitadores.SeleccionarFaPorID(conexion.conexion, Cofa.id_facilitador);

                            conexion.cerrarconexion();
                            txtTlfnCoFa.Text = faDatos.tlfn_facilitador;
                            txtCorreoCoFa.Text = faDatos.correo_facilitador;
                        }


                    }
                }
            }
            
        }

        private void dtpFechaCurso_Validating(object sender, CancelEventArgs e)
        {
            if (dtpFechaCurso.Value <= DateTime.Today)
            {
                errorProviderFecha.SetError(dtpFechaCurso, "La fecha seleccionada es inválida.");
                dtpFechaCurso.Focus();
            }
            else
            {
                errorProviderFecha.SetError(dtpFechaCurso, "");
                time.fecha_curso = dtpFechaCurso.Value;
                if ((formacion.duracion == "8" && formacion.bloque_curso == "1"))
                {
                    gpbFacilitador.Enabled = true;
                }


            }
        }

    }
}
