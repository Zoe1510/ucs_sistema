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
using System.Diagnostics;
using UCS_NODO_FGC.Clases;

namespace UCS_NODO_FGC
{
    public partial class Nueva_formacion_InCompany : Form
    {
        Formaciones formacion = new Clases.Formaciones();
        Empresa cliente = new Empresa();
        conexion_bd conexion = new Clases.conexion_bd();
        Tiempos_curso time = new Clases.Tiempos_curso();
        Facilitadores fa = new Clases.Facilitadores();
        Facilitadores Cofa = new Clases.Facilitadores();
        Facilitadores faDatos = new Clases.Facilitadores();
        Paquete_instruccional p_inst = new Clases.Paquete_instruccional();
        List<Paquete_instruccional> pLista = new List<Clases.Paquete_instruccional>();
        bool guardar = false;
        string duracion = "";
        bool ExisteFormacion;
        string presentacion = "";
        string contenido = "";
        string bloques = "";
        string bitacora = "";
        string manual = "";

        DateTime fecha_creacion, FinalE1, FinalE2, FinalE3, inicioE2, inicioE3;
        public Nueva_formacion_InCompany()
        {
            InitializeComponent();
        }

        private void Nueva_formacion_InCompany_Load(object sender, EventArgs e)
        {
          
            if (Clases.Formaciones.creacion == true)//si viene referenciado del boton de la pagina principal
            {//------------------------------------------todo hay que hacerlo aquí(un nuevo ingreso)
                this.Location = new Point(-5, 0);

                fecha_creacion = DateTime.Now;

                LabelCabecera.Text = "Nuevo InCompany: Información básica";
                LabelCabecera.Location = new Point(115, 31);

                lblEtapaSiguiente.Text = "Nivel Intermedio";
                lblEtapaSiguiente.Location = new Point(17, 529);

                lblEtapafinal.Text = "Nivel Avanzado";
                lblEtapafinal.Location = new Point(22, 570);

                dtpFechaCurso.Value = DateTime.Today;
                dtpSegundaFecha.Value = DateTime.Today;

                btnVerPresentacion.Enabled = false;
                btnVerContenido.Enabled = false;
                btnRutaContenido.Enabled = false;
                btnRutaPresentacion.Enabled = false;
                btnRutaBitacora.Enabled = false;
                btnRutaManual.Enabled = false;
                btnVerBitacora.Enabled = false;
                btnVerManual.Enabled = false;

                //como estarán los botones inicialmente para cada nivel
                Load_Sig_Re();

                formacion.etapa_curso = 1;
                formacion.tipo_formacion = "InCompany";
                formacion.id_user = Usuario_logeado.id_usuario;
                //btnSiguienteEtapa.Enabled = true; //Solo para tomar ss

                //controles del nivel intermedio
                Controles_nivel_intermedio_EstatusInicial();

                conexion.cerrarconexion();
                if (conexion.abrirconexion() == true)
                {
                    string difu = "";
                    CargarDatosInsumos(conexion.conexion, difu);
                    dgvInsumos.ClearSelection();
                    conexion.cerrarconexion();
                }

                int i = 0;
                llenarcmbxEmpresas(i);
                //llenarcomboSolicitud();
                //cargar los facilitadores del nivel_intermedio
                llenarcomboFacilitador();
            }
                
        }
        /*---------------------- METODOS ------------------------*/
        private void llenarcmbxEmpresas(int id)
        {
            MySqlDataReader SolicitadoPor = Conexion.ConsultarBD("SELECT nombre_empresa FROM clientes WHERE id_clientes !='" + id + "'");
            while (SolicitadoPor.Read())
            {
                cmbxSolicitadoPor.Items.Add(SolicitadoPor["nombre_empresa"]);
            }
            SolicitadoPor.Close();
        }
        private void Load_Sig_Re()
        {
            btnRetomar.Enabled = false;
            btnSiguienteEtapa.Enabled = false;
            btnModificar.Enabled = false;
            btnGuardar.Enabled = true;
            btnPausar.Enabled = true;
            btnLimpiar.Enabled = true;
        }
        private void vaciarFormacion()
        {
            if (pnlNivel_basico.Visible == true)
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
                cmbxSolicitadoPor.SelectedIndex = -1;

            }
            else if (pnlNivel_intermedio.Visible == true)
            {
                fa.id_facilitador = 0;
                Cofa.id_facilitador = 0;

                rdbNoRef.Checked = false;
                rdbSiRef.Checked = false;
                cmbxFa.SelectedIndex = -1;
                cmbxCoFa.SelectedIndex = -1;
                chkbCoFacilitador.Checked = false;
                txtCorreoFa.Clear();
                txtCorreoCoFa.Clear();
                txtTlfnFa.Clear();
                txtTlfnCoFa.Clear();

            }
            else if (pnlNivel_avanzado.Visible == true)
            {
                cmbxHorarios.SelectedIndex = -1;
                cmbxHorario2.SelectedIndex = -1;
                rdbNoIgualHorario.Checked = false;
                rdbNoMantenerAula.Checked = false;
                rdbNoMantenerRef.Checked = false;
                rdbNoRef.Checked = false;
                rdbSiIgualHorario.Checked = false;
                rdbSiMantenerAula.Checked = false;
                rdbSiRef.Checked = false;
                cmbxTipoRefrigerio.SelectedIndex = -1;
                txtAula.Clear();

            }

        }
        private void deshabiltarControlesBasico()
        {
            //para el boton pausar en el (nivel_basico)
            txtNombreFormacion.Enabled = false;
            cmbxSolicitadoPor.Enabled = false;
            cmbxDuracionFormacion.Enabled = false;
            cmbxBloques.Enabled = false;
            btnRutaContenido.Enabled = false;
            btnRutaPresentacion.Enabled = false;
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
        private void Controles_nivel_intermedio_EstatusInicial()
        {
            gpbRefrigerio.Enabled = false;
            gpbFecha.Enabled = true;          
            gpbFacilitador.Enabled = false;
            gpbDatosFa.Enabled = false;
            chkbCoFacilitador.Enabled = false;
            gpbCoFa.Enabled = false;
            gpbDatosCoFa.Enabled = false;
        }
        //private void llenarcomboSolicitud()
        //{
        //    cmbxSolicitadoPor.ValueMember = "id_clientes";
        //    cmbxSolicitadoPor.DisplayMember= "nombre_empresa";
        //    cmbxSolicitadoPor.DataSource = llenarSolicitud();
        //    cmbxSolicitadoPor.SelectedIndex = -1;
        //}
        private List<Empresa> llenarSolicitud()
        {
            List<Empresa> lista = new List<Empresa>();
            MySqlDataReader leer = Conexion.ConsultarBD("SELECT * FROM clientes");
            while (leer.Read())
            {
                Empresa e = new Empresa();
                e.nombre_empresa = Convert.ToString(leer["nombre_empresa"]);
                e.id_clientes = Convert.ToInt32(leer["id_clientes"]);
                lista.Add(e);

            }
            leer.Close();
            return lista;
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
                        Clases.Paquete_instruccional.tipo_curso = "InCompany";
                        btnVerContenido.Enabled = true;

                        if (Paquete_instruccional._presentacion != "")
                            btnVerPresentacion.Enabled = true;
                        else
                            btnVerPresentacion.Enabled = false;

                        if (Paquete_instruccional._manual != "")
                            btnVerManual.Enabled = true;
                        else
                            btnVerManual.Enabled = false;
                        if (Paquete_instruccional._bitacora != "")
                            btnVerBitacora.Enabled = true;
                        else
                            btnVerBitacora.Enabled = false;

                        
                        Ver_paqueteInstruccional verp = new Ver_paqueteInstruccional();
                        verp.ShowDialog();
                        formacion.pq_inst = id_pq;
                        ExisteFormacion = true;

                        //Por si ya existe un curso con el mismo nombre pero finalizado, los botones obtendran el valor de la bd
                        contenido = pq.contenido;
                        manual = pq.manual;
                        bitacora = pq.bitacora;
                        presentacion = pq.presentacion;

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

        private void GuardarBasico()
        {
            try
            {
                if (txtNombreFormacion.Text == "")
                {
                    errorProviderNombreF.SetError(txtNombreFormacion, "Debe proporcionar el nombre la formación.");
                    txtNombreFormacion.Focus();
                }
                else
                {
                    errorProviderNombreF.SetError(txtNombreFormacion, "");
                    if (cmbxSolicitadoPor.SelectedIndex == -1)
                    {
                        errorProviderSolicitado.SetError(cmbxSolicitadoPor, "Debe proporcionar el nombre de quien solicita.");
                        cmbxSolicitadoPor.Focus();
                    }
                    else
                    {
                        errorProviderSolicitado.SetError(cmbxSolicitadoPor, "");
                        if (cmbxDuracionFormacion.SelectedIndex == -1)
                        {
                            errorProviderDuracionF.SetError(cmbxDuracionFormacion, "Debe proporcionar la duración de la formación.");
                            cmbxDuracionFormacion.Focus();
                        }
                        else
                        {
                            errorProviderDuracionF.SetError(cmbxDuracionFormacion, "");
                            if (cmbxBloques.SelectedIndex == -1)
                            {
                                errorProviderBloque.SetError(cmbxBloques, "Debe proporcionar los bloques de la formación.");
                                cmbxBloques.Focus();
                            }
                            else
                            {
                                errorProviderBloque.SetError(cmbxBloques, "");
                                if (contenido == "")
                                {
                                    errorProviderContenido.SetError(btnRutaContenido, "Debe seleccionar un contenido para la formación.");

                                }
                                else //si el contenido existe
                                {
                                    errorProviderContenido.SetError(btnRutaContenido, "");
                                    if (btnVerPresentacion.Enabled == false)
                                    {
                                        p_inst.presentacion = "";
                                    }
                                    if (btnVerBitacora.Enabled == false)
                                    {
                                        p_inst.bitacora = "";
                                    }
                                    if (btnVerManual.Enabled == false)
                                    {
                                        p_inst.manual = "";
                                    }

                                    formacion.nombre_formacion = txtNombreFormacion.Text;
                                    formacion.tipo_formacion = "InCompany";

                                    FinalE1 = DateTime.Now;
                                    formacion.fecha_inicial = fecha_creacion;
                                    formacion.TiempoEtapa = Convert.ToString(FinalE1 - fecha_creacion);
                                    //MessageBox.Show(formacion.TiempoEtapa);
                                    formacion.id_user = Clases.Usuario_logeado.id_usuario;
                                    formacion.etapa_curso = 1;//representa la etapa actual: nivel_basico (cambiará para cada panel)
                                    formacion.solicitado = cmbxSolicitadoPor.Text;
                                    if (ExisteFormacion == false)
                                    {
                                        //se valida si el paquete ya existe
                                        if (conexion.abrirconexion() == true)
                                            p_inst.id_pinstruccional = Clases.Formaciones.ObtenerIdPaquete(conexion.conexion, p_inst);
                                        conexion.cerrarconexion();

                                        if (p_inst.id_pinstruccional == 0)
                                        {

                                            int resultado2 = 0;
                                            if (conexion.abrirconexion() == true)
                                            {

                                                resultado2 = Formaciones.GuardarPaqueteInstruccional(conexion.conexion, p_inst);
                                                conexion.cerrarconexion();
                                            }
                                            int id_paquete = 0;


                                            if (resultado2 != 0)//si se guardó con éxito: recoger el id de ese paquete.
                                            {
                                                if (conexion.abrirconexion() == true)
                                                    id_paquete = Clases.Formaciones.ObtenerIdPaquete(conexion.conexion, p_inst);

                                                conexion.cerrarconexion();

                                                if (id_paquete != 0)//se obtiene un id_intruccional cooncordante con el archivo subido
                                                {
                                                    formacion.pq_inst = id_paquete;
                                                    int resultado = 0;
                                                    if (conexion.abrirconexion() == true)
                                                    {

                                                        resultado = Clases.Formaciones.AgregarNuevaFormacion(conexion.conexion, formacion);

                                                        conexion.cerrarconexion();
                                                    }


                                                    if (resultado > 0 && resultado2 > 0)
                                                    {
                                                        int id_curso = 0;
                                                        if (conexion.abrirconexion() == true)
                                                        {
                                                            //esto es para recoger el id del curso que se acaba de agregar (ignorar el nombre del método)
                                                            id_curso = Clases.Formaciones.CursoEjecucionExiste(conexion.conexion, formacion);
                                                            conexion.cerrarconexion();
                                                        }

                                                        if (id_curso != 0)
                                                        {
                                                            formacion.id_curso = id_curso;
                                                            if (conexion.abrirconexion() == true)
                                                            {
                                                                int agregarUGC = Clases.Formaciones.Agregar_U_g_C(conexion.conexion, id_curso, formacion.id_user, fecha_creacion, FinalE1);
                                                                conexion.cerrarconexion();
                                                                if (agregarUGC > 0)
                                                                {
                                                                    guardar = true;
                                                                    MessageBox.Show("La formación se ha agregado correctamente.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.None);
                                                                }
                                                            }


                                                        }
                                                        else
                                                        {
                                                            MessageBox.Show("Error: No se pudo agregar la formación.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                        }

                                                    }
                                                    else
                                                    {
                                                        MessageBox.Show("Error: No se pudo agregar la formación 1.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                    }


                                                }
                                                else//si no se encuentra el id del paquete 
                                                {
                                                    MessageBox.Show("No hay id paquete");
                                                }


                                            }
                                            else
                                            {
                                                MessageBox.Show("No hay id_curso");
                                            }


                                        }
                                        else
                                        {
                                            VerPaqueteInst(p_inst.id_pinstruccional);
                                            int resultado = 0;
                                            if (conexion.abrirconexion() == true)
                                            {

                                                resultado = Clases.Formaciones.AgregarNuevaFormacion(conexion.conexion, formacion);

                                                conexion.cerrarconexion();
                                            }

                                            if (resultado > 0)
                                            {
                                                int id_curso = 0;
                                                if (conexion.abrirconexion() == true)
                                                {
                                                    //esto es para recoger el id del curso que se acaba de agregar (ignorar el nombre del método)
                                                    id_curso = Clases.Formaciones.CursoEjecucionExiste(conexion.conexion, formacion);
                                                    conexion.cerrarconexion();
                                                }

                                                if (id_curso != 0)
                                                {
                                                    formacion.id_curso = id_curso;
                                                    if (conexion.abrirconexion() == true)
                                                    {
                                                        int agregarUGC = Clases.Formaciones.Agregar_U_g_C(conexion.conexion, id_curso, formacion.id_user, fecha_creacion, FinalE1);
                                                        conexion.cerrarconexion();
                                                        if (agregarUGC > 0)
                                                        {
                                                            guardar = true;
                                                            MessageBox.Show("La formación se ha agregado correctamente.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.None);
                                                        }
                                                    }


                                                }
                                                else
                                                {
                                                    MessageBox.Show("Error: No se pudo agregar la formación.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                }

                                            }
                                            else
                                            {
                                                MessageBox.Show("Error: No se pudo agregar la formación.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                            }
                                        }
                                    }
                                    else //if formacionExiste==true
                                    {
                                        int resultado = 0;
                                        if (conexion.abrirconexion() == true)
                                        {

                                            resultado = Clases.Formaciones.AgregarNuevaFormacion(conexion.conexion, formacion);

                                            conexion.cerrarconexion();
                                        }

                                        if (resultado > 0)
                                        {
                                            int id_curso = 0;
                                            if (conexion.abrirconexion() == true)
                                            {
                                                //esto es para recoger el id del curso que se acaba de agregar (ignorar el nombre del método)
                                                id_curso = Clases.Formaciones.CursoEjecucionExiste(conexion.conexion, formacion);
                                                conexion.cerrarconexion();
                                            }

                                            if (id_curso != 0)
                                            {
                                                formacion.id_curso = id_curso;
                                                if (conexion.abrirconexion() == true)
                                                {
                                                    int agregarUGC = Clases.Formaciones.Agregar_U_g_C(conexion.conexion, id_curso, formacion.id_user, fecha_creacion, FinalE1);
                                                    conexion.cerrarconexion();
                                                    if (agregarUGC > 0)
                                                    {
                                                        guardar = true;
                                                        MessageBox.Show("La formación se ha agregado correctamente.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.None);
                                                    }
                                                }


                                            }
                                            else
                                            {
                                                MessageBox.Show("Error: No se pudo agregar la formación.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                            }

                                        }
                                        else
                                        {
                                            MessageBox.Show("Error: No se pudo agregar la formación.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        private void GuardarIntermedio()
        {

        }
        private void GuardarAvanzado()
        {

        }
                
        /*---------------------- Panel lateral derecho ------------------------*/
        private void btnSiguienteEtapa_Click(object sender, EventArgs e)
        {
           
            //cuando Siguiente etapa, quita el panel actual
            if (pnlNivel_basico.Visible == true)
            {
                LabelCabecera.Text = "Nuevo Incompany: Detalles técnicos";
                LabelCabecera.Location = new Point(115, 31);

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
                    LabelCabecera.Text = "Nuevo InCompany: Logística";
                    LabelCabecera.Location = new Point(200, 31);

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

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (Clases.Formaciones.creacion == true)
            {
                if (pnlNivel_basico.Visible == true)
                {
                   
                    GuardarBasico();
                    if (guardar == true)
                    {
                        btnSiguienteEtapa.Enabled = true;
                        btnPausar.Enabled = true;
                        btnLimpiar.Enabled = true;

                        btnModificar.Enabled = false;
                        btnRetomar.Enabled = false;
                        btnGuardar.Enabled = false;

                    }

                }
                else
                {
                    if (pnlNivel_intermedio.Visible == true)
                    {
                        GuardarIntermedio();
                        if (guardar == true)
                        {
                            btnSiguienteEtapa.Enabled = true;
                            btnPausar.Enabled = true;
                            btnLimpiar.Enabled = true;

                            btnModificar.Enabled = false;
                            btnRetomar.Enabled = false;
                            btnGuardar.Enabled = false;
                        }
                    }
                    else
                    {
                        if (pnlNivel_avanzado.Visible == true)
                        {
                            GuardarAvanzado();
                        }
                    }
                }

            }

        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            vaciarFormacion();
            btnSiguienteEtapa.Enabled = false;
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

        private void btnRetomar_Click(object sender, EventArgs e)
        {
            //cuando retomar = habilitar los controles
            if (pnlNivel_basico.Visible == true)
            {
                if(guardar == false)
                {
                    btnGuardar.Enabled = true;
                }else
                {
                    btnGuardar.Enabled = false;
                }
                txtNombreFormacion.Enabled = true;
                txtNombreFormacion.Focus();
                cmbxSolicitadoPor.Enabled = true;
                cmbxDuracionFormacion.Enabled = true;
                cmbxBloques.Enabled = true;
                btnRutaContenido.Enabled = true;
                btnRutaPresentacion.Enabled = true;
                //Y lo deja igual que el load
            }
            else if (pnlNivel_intermedio.Visible == true)
            {

                gpbRefrigerio.Enabled = true;
                dtpFechaCurso.Enabled = true;
                dtpSegundaFecha.Enabled = true;
                chkbCoFacilitador.Enabled = true;
                cmbxFa.Enabled = true;
                

            }
            else if (pnlNivel_avanzado.Visible == true)
            {
                cmbxHorarios.Enabled = true;
                cmbxTipoRefrigerio.Enabled = true;
                txtAula.Enabled = true;
                gpbInsumos.Enabled = true;
            }

            Load_Sig_Re();
            if (guardar == true)
            {
                btnSiguienteEtapa.Enabled = true;
            }
        }


        /*---------------------- Controles nivel_basico------------------------*/

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
                cmbxSolicitadoPor.Focus();
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
            if (txtNombreFormacion.Text != "")//siempre y cuando el txt contenga algo, se valida
            {
                formacion.estatus = "En curso"; //predeterminado en esta etapa
                formacion.tipo_formacion = "InCompany"; //predeterminado para este form
                formacion.nombre_formacion = txtNombreFormacion.Text;

                //aqui habrá que verificar (agarrar el id_cursos) a ver si está ya registrado en el sistema, de acuerdo al tipo, estatus y nombre especificado.

                List<Formaciones> lista = new List<Formaciones>();
                List<string> nombre = new List<string>();
                MySqlDataReader leer = Conexion.ConsultarBD("SELECT id_cursos, solicitud_curso  FROM cursos WHERE nombre_curso = '" + formacion.nombre_formacion+ "' AND tipo_curso='InCompany' AND estatus_curso LIKE 'En curso'");
                while (leer.Read())
                {
                    Formaciones f = new Formaciones();
                    f.id_curso = Convert.ToInt32(leer["id_cursos"]);
                    string n = Convert.ToString(leer["solicitud_curso"]);
                    f.estatus = "En curso";
                    lista.Add(f);
                    nombre.Add(n);
                }
                leer.Close();

                //MySqlDataReader leer2 = Conexion.ConsultarBD("SELECT id_cursos, solicitud_curso FROM cursos WHERE nombre_curso = '" + formacion.nombre_formacion + "' AND tipo_curso='InCompany' AND estatus_curso LIKE 'Reprogramado'");
                //while (leer2.Read())
                //{
                //    Formaciones f = new Formaciones();
                //    f.id_curso = Convert.ToInt32(leer2["id_cursos"]);
                //    f.solicitado = Convert.ToString(leer["solicitud_curso"]);
                //    f.estatus = "Reprogramado";
                //    lista.Add(f);
                //}
                //leer2.Close();

                //si está: buscar todos los clientes que lo hayan solicitado (en la tabla clientes solicitan cursos) 
                for (int i = 0; i < nombre.Count; i++)
                {
                    for (int a = 0; a < cmbxSolicitadoPor.Items.Count; a++)
                    {
                        if (cmbxSolicitadoPor.Items[a].ToString() == nombre[i])
                        {
                            cmbxSolicitadoPor.Items.RemoveAt(a);
                        }
                    }

                }

                if (formacion.pq_inst == 0)
                {
                    conexion.cerrarconexion();
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
                            btnRutaManual.Enabled = true;
                            btnRutaBitacora.Enabled = true;
                        }

                    }
                }
                
                //para luego recorrer el combobox y retirar los que ya han solicitado el curso y dejar los que no. (igual que inces)
                //hacer la misma busqueda pero con el estatus Reprogramado, en caso de que En curso, no haya ningun resultados.
                //y realizar el mismo procedimiento con el combobox
                //si no: seguir normalmente
            }
        }

        private void cmbxSolicitadoPor_SelectionChangeCommitted(object sender, EventArgs e)
        {
            cliente.id_clientes = Convert.ToInt32(cmbxSolicitadoPor.SelectedValue);
            cliente.nombre_empresa= cmbxSolicitadoPor.Text;
            formacion.solicitado = cliente.nombre_empresa;

            //validar si la empresa seleccionada ha solicitado un curso del mismo nombre.
            MySqlDataReader leer = Conexion.ConsultarBD("SELECT id_cursos from cursos where nombre_curso='" + formacion.nombre_formacion + "' AND solicitud_curso='" + formacion.solicitado + "' AND tipo_curso='Incompany'");
            if (leer.Read())
            {
                errorProviderNombreF.SetError(txtNombreFormacion, "El cliente seleccionado ya ha solicitado esta formación.");
                txtNombreFormacion.Focus();
            }
            else
            {
                errorProviderNombreF.SetError(txtNombreFormacion, "");
            }
            leer.Close();
        }
        //comboboxDuracionFormacion (Nivel_basico)
        private void cmbxDuracionFormacion_Validating(object sender, CancelEventArgs e)
        {
            if (cmbxDuracionFormacion.SelectedIndex == -1)
            {
                errorProviderDuracionF.SetError(cmbxDuracionFormacion, "Debe proporcionar la duración de la formación.");
                cmbxDuracionFormacion.Focus();
                formacion.duracion = "";
            }
            else
            {
                errorProviderDuracionF.SetError(cmbxDuracionFormacion, "");
                duracion = Convert.ToString(cmbxDuracionFormacion.SelectedIndex);//se obtiene el valor de la duracion del curso
                switch (duracion)
                {
                    case "0":
                        duracion = "4";
                        cmbxBloques.Items.Clear();
                        cmbxBloques.Items.Add("1");
                        cmbxBloques.SelectedIndex = 0;
                        formacion.bloque_curso = "1";
                        cmbxBloques.Enabled = false;
                        break;
                    case "1":
                        duracion = "8";
                        cmbxBloques.Items.Clear();
                        cmbxBloques.Items.Add("1");
                        cmbxBloques.Items.Add("2");
                        cmbxBloques.SelectedIndex = -1;
                        cmbxBloques.Enabled = true;
                        break;
                    case "2":
                        duracion = "16";
                        cmbxBloques.Items.Clear();
                        cmbxBloques.Items.Add("2");
                        cmbxBloques.Enabled = false;
                        cmbxBloques.SelectedIndex = 0;
                        formacion.bloque_curso = "2";
                        break;
                }
                formacion.duracion = duracion;
            }
        }

        //comboboxBloquesFormacion (Nivel_basico)
        private void cmbxBloques_Validating(object sender, CancelEventArgs e)
        {
            if (cmbxBloques.SelectedIndex == -1)
            {
                errorProviderBloque.SetError(cmbxBloques, "Debe proporcionar los bloques de la formación.");
                cmbxBloques.Focus();
                formacion.bloque_curso = "";
            }
            else
            {
                errorProviderBloque.SetError(cmbxBloques, "");
                bloques = Convert.ToString(cmbxBloques.SelectedIndex);
                if (duracion == "8")
                {
                    switch (bloques)
                    {
                        case "0":
                            bloques = "1";
                            break;
                        case "1":
                            bloques = "2";
                            break;
                    }
                    formacion.bloque_curso = bloques;

                }
                else if (duracion == "16")
                {
                    switch (bloques)
                    {
                        case "0":
                            bloques = "2";
                            break;

                    }
                    formacion.bloque_curso = bloques;
                }
                else if (duracion == "4")
                {

                    bloques = "1";

                    formacion.bloque_curso = bloques;
                }
            }
        }


        //Boton para escoger la ruta del PDF contenido (Nivel_basico)
        private void btnRutaContenido_Click(object sender, EventArgs e)
        {
            try
            {
                if (ExisteFormacion == false)
                {

                    OpenFileDialog od = new OpenFileDialog();
                    od.Filter = "PDF files |*.pdf";
                    if (od.ShowDialog() == DialogResult.OK)
                    {
                        
                        contenido = od.FileName;
                        //para evitar que mysql borre los "\" se sustituyen por "/" que funcionan igual
                        contenido = contenido.Replace("\\", "/");
                        p_inst.contenido = contenido;
                        btnVerContenido.Enabled = true;

                    }
                }


            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                conexion.cerrarconexion();

            }
        }
        //Boton para ver el contenido (Nivel_basico)
        private void btnVerContenido_Click(object sender, EventArgs e)
        {
            Process.Start(contenido);
        }


        //Boton para escoger la ruta del PPT presentacion (Nivel_basico)
        private void btnRutaPresentacion_Click(object sender, EventArgs e)
        {
            try
            {
                if (ExisteFormacion == false)
                {
                    OpenFileDialog od = new OpenFileDialog();
                    od.Filter = "PPT files |*.ppt;*.pptx;*.pptm";
                    if (od.ShowDialog() == DialogResult.OK)
                    {
                        //para evitar que mysql borre los "\" se sustituyen por "/" que funcionan igual
                        
                        presentacion = od.FileName;
                        presentacion = presentacion.Replace("\\", "/");
                        p_inst.presentacion = presentacion;
                        btnVerPresentacion.Enabled = true;
                        
                        

                    }
                    else
                    {
                        p_inst.presentacion = "";
                        btnVerPresentacion.Enabled = false;
                    }
                }


            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                conexion.cerrarconexion();

            }

        }
        //Boton para ver la presentacion (Nivel_basico)
        private void btnVerPresentacion_Click(object sender, EventArgs e)
        {
            Process.Start(presentacion);
        }

        private void btnRutaBitacora_Click(object sender, EventArgs e)
        {
            try
            {
                if (ExisteFormacion == false)
                {

                    OpenFileDialog od = new OpenFileDialog();
                    od.Filter = "PDF files |*.pdf";
                    if (od.ShowDialog() == DialogResult.OK)
                    {
                        //para evitar que mysql borre los "\" se sustituyen por "/" que funcionan igual                    
                                              
                        bitacora = od.FileName;
                        bitacora = bitacora.Replace("\\", "/");
                        p_inst.bitacora = bitacora;
                        btnVerBitacora.Enabled = true;

                    }
                }


            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                conexion.cerrarconexion();

            }
        }
        private void btnVerBitacora_Click(object sender, EventArgs e)
        {
            Process.Start(bitacora);
        }

        private void btnRutaManual_Click(object sender, EventArgs e)
        {
            try
            {
                if (ExisteFormacion == false)
                {

                    OpenFileDialog od = new OpenFileDialog();
                    od.Filter = "PDF files |*.pdf";
                    if (od.ShowDialog() == DialogResult.OK)
                    {
                        //para evitar que mysql borre los "\" se sustituyen por "/" que funcionan igual
                    
                        manual = od.FileName;
                        manual = manual.Replace("\\", "/");
                        p_inst.manual = manual;
                        btnVerManual.Enabled = true;

                    }
                }


            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                conexion.cerrarconexion();

            }
        }
        private void btnVerManual_Click(object sender, EventArgs e)
        {
            Process.Start(manual);
        }



        /*---------------------- Controles nivel_intermedio------------------------*/

        private void rdbNoInstalaciones_CheckedChanged(object sender, EventArgs e)
        {
            rdbSiRef.Checked = false;
            rdbNoRef.Checked = false;
            gpbRefrigerio.Enabled =false;
        }
        private void rdbInstalaciones_CheckedChanged(object sender, EventArgs e)
        {
            gpbRefrigerio.Enabled = true;
        }
        
        private void dtpFechaCurso_ValueChanged(object sender, EventArgs e)
        {
            inicioE2 = DateTime.Now;
            if (dtpFechaCurso.Value <= DateTime.Today)
            {
                errorProviderFecha.SetError(dtpFechaCurso, "La fecha seleccionada es inválida.");
                dtpFechaCurso.Focus();
            }
            else
            {

                errorProviderFecha.SetError(dtpFechaCurso, "");
                time.fecha_curso = dtpFechaCurso.Value.ToString("yyyy-MM-dd");
                if ((formacion.duracion == "8" && formacion.bloque_curso == "2") || (formacion.duracion == "16"))
                {                    
                    dtpSegundaFecha.Enabled = true;
                    dtpSegundaFecha.Focus();
                }
                else if ((formacion.duracion == "4") || (formacion.duracion == "8" && formacion.bloque_curso == "1"))
                {
                    gpbFacilitador.Enabled = true;
                    cmbxFa.Focus();
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
                time.fecha_curso = dtpFechaCurso.Value.ToString("yyyy-MM-dd");
                if ((formacion.duracion == "8" && formacion.bloque_curso == "2") || (formacion.duracion == "16"))
                {
                    dtpSegundaFecha.Focus();
                }
                else if ((formacion.duracion == "4") || (formacion.duracion == "8" && formacion.bloque_curso == "1"))
                {
                    gpbFacilitador.Enabled = true;
                }


            }
        }        

        private void dtpSegundaFecha_ValueChanged(object sender, EventArgs e)
        {
            if (dtpSegundaFecha.Value <= dtpFechaCurso.Value)
            {
                errorProviderFecha.SetError(dtpSegundaFecha, "La fecha seleccionada es inválida.");
                dtpSegundaFecha.Focus();
            }
            else
            {
                errorProviderFecha.SetError(dtpSegundaFecha, "");
                time.fechaDos_curso = dtpSegundaFecha.Value.ToString("yyyy-MM-dd");
                gpbFacilitador.Enabled = true;
            }
        }       
        private void dtpSegundaFecha_Validating(object sender, CancelEventArgs e)
        {
            if (dtpSegundaFecha.Value <= dtpFechaCurso.Value)
            {
                errorProviderFecha.SetError(dtpSegundaFecha, "La fecha seleccionada es inválida.");
                dtpSegundaFecha.Focus();
            }
            else
            {
                errorProviderFecha.SetError(dtpSegundaFecha, "");
                time.fechaDos_curso = dtpSegundaFecha.Value.ToString("yyyy-MM-dd HH:mm:ss"); ;
                gpbFacilitador.Enabled = true;
            }
        }

        private void chkbCoFacilitador_CheckedChanged(object sender, EventArgs e)
        {
            if (chkbCoFacilitador.Checked == true)
            {
                gpbCoFa.Enabled = true;
                cmbxCoFa.Enabled = true;
                cmbxCoFa.Focus();

            }
            else
            {
                gpbCoFa.Enabled = false;
            }
        }
        private void cmbxFa_SelectionChangeCommitted(object sender, EventArgs e)
        {
            fa.id_facilitador = Convert.ToInt32(cmbxFa.SelectedValue);
            llenarComboCoFa(fa.id_facilitador);

        }

        private void cmbxFa_Validating(object sender, CancelEventArgs e)
        {
            //validar si este facilitador estará disponible para esa o esas fechas
            if ((formacion.duracion == "4") || (formacion.duracion == "8" && formacion.bloque_curso == "1"))
            {

                //validar si estará disponible para la fecha del dia 1
                if (conexion.abrirconexion() == true)
                {
                    int fa_disponible = Clases.Facilitadores.FacilitadorDisponibleDia(conexion.conexion, time.fecha_curso, fa.id_facilitador);
                    conexion.cerrarconexion();
                    if (fa_disponible != 0)//si retorna algo es que este facilitador ya está ocupado para esa fecha
                    {
                        MessageBox.Show("El facilitador está ocupado en la fecha seleccionada.");
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
            else if ((formacion.duracion == "16") || (formacion.duracion == "8" && formacion.bloque_curso == "2"))
            {

                //validar ambas fechas
                if (conexion.abrirconexion() == true)
                {
                    int fa_disponible = Clases.Facilitadores.FacilitadorDisponibleDia(conexion.conexion, time.fecha_curso, fa.id_facilitador);
                    conexion.cerrarconexion();
                    if (fa_disponible > 0)//si retorna algo es que este facilitador ya está ocupado para esa fecha
                    {
                        MessageBox.Show("El facilitador está ocupado en esta fecha: " + time.fecha_curso + ".");
                        cmbxFa.SelectedIndex = -1;
                        fa.id_facilitador = 0;
                        cmbxFa.Focus();
                    }
                    else
                    {
                        MessageBox.Show(fa_disponible.ToString());
                        errorProviderPresentacion.SetError(cmbxFa, "");
                        if (conexion.abrirconexion() == true)
                        {
                            int fa_disponibleDia2 = Clases.Facilitadores.FacilitadorDisponibleDia2(conexion.conexion, time.fechaDos_curso, fa.id_facilitador);
                            conexion.cerrarconexion();
                            if (fa_disponibleDia2 > 0)//si retorna algo es que este facilitador ya está ocupado para esa fecha
                            {
                                MessageBox.Show("El facilitador está ocupado en esta fecha: " + time.fechaDos_curso + ".");
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
            }
        }

        private void cmbxCoFa_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Cofa.id_facilitador = Convert.ToInt32(cmbxCoFa.SelectedValue);
        }

        private void cmbxCoFa_Validating(object sender, CancelEventArgs e)
        {
            //validar si este facilitador estará disponible para esa o esas fechas
            if ((formacion.duracion == "4") || (formacion.duracion == "8" && formacion.bloque_curso == "1"))
            {

                //validar si estará disponible para la fecha del dia 1
                if (conexion.abrirconexion() == true)
                {
                    int fa_disponible = Clases.Facilitadores.CoFacilitadorDisponibleDia(conexion.conexion, time.fecha_curso, Cofa.id_facilitador);
                    conexion.cerrarconexion();
                    if (fa_disponible != 0)//si retorna algo es que este facilitador ya está ocupado para esa fecha
                    {
                        MessageBox.Show("El Co-facilitador está ocupado en la fecha seleccionada.");
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
            else if ((formacion.duracion == "16") || (formacion.duracion == "8" && formacion.bloque_curso == "2"))
            {
                //validar ambas fechas
                if (conexion.abrirconexion() == true)
                {
                    int fa_disponible = Clases.Facilitadores.CoFacilitadorDisponibleDia(conexion.conexion, time.fecha_curso, Cofa.id_facilitador);
                    conexion.cerrarconexion();
                    if (fa_disponible != 0)//si retorna algo es que este facilitador ya está ocupado para esa fecha
                    {
                        MessageBox.Show("El Co-facilitador está ocupado en esta fecha: " + time.fecha_curso + ".");
                        cmbxCoFa.SelectedIndex = -1;
                        Cofa.id_facilitador = 0;
                        cmbxCoFa.Focus();
                    }
                    else
                    {
                        errorProviderPresentacion.SetError(cmbxCoFa, "");
                        if (conexion.abrirconexion() == true)
                        {
                            int fa_disponibleDia2 = Clases.Facilitadores.CoFacilitadorDisponibleDia2(conexion.conexion, time.fechaDos_curso, Cofa.id_facilitador);
                            conexion.cerrarconexion();
                            if (fa_disponibleDia2 != 0)//si retorna algo es que este facilitador ya está ocupado para esa fecha
                            {
                                MessageBox.Show("El Co-facilitador está ocupado en la fecha : " + time.fechaDos_curso + ".");
                                cmbxCoFa.SelectedIndex = -1;
                                Cofa.id_facilitador = 0;
                                cmbxCoFa.Focus();
                            }
                            else
                            {
                                errorProviderPresentacion.SetError(cmbxCoFa, "");
                                //si todo bien, cargar los datos en el gpbDatosFa
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
            }
        }

       
               

        


    }
}
