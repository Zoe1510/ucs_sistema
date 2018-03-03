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
using System.Diagnostics;
using System.IO;

namespace UCS_NODO_FGC
{
    public partial class Nueva_formacion_FEE : Form
    {
        Formaciones formacion = new Clases.Formaciones();
        conexion_bd conexion = new Clases.conexion_bd();
        Tiempos_curso time = new Clases.Tiempos_curso();
        Facilitadores fa = new Clases.Facilitadores();
        Facilitadores Cofa = new Clases.Facilitadores();
        Facilitadores faDatos = new Clases.Facilitadores();
        Paquete_instruccional p_inst = new Clases.Paquete_instruccional();

        bool guardar, ExisteFormacion;
        string presentacion = "";
        string contenido = "";
        string manual = "";
        string bitacora = "";
        string bloques = "";
        string horario;
        int id_refrigerio, id_horario;

        DateTime fecha_creacion, FinalE1, FinalE2, FinalE3, inicioE2, inicioE3;
        List<string> nombre_publicidad = new List<string>();
        List<string> lista_insumo = new List<string>();
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

                dtpFechaCurso.Value = DateTime.Today;

                //estado inicial de los bloques de esta formación
                cmbxBloques.SelectedIndex = 0;
                cmbxBloques.Enabled = false;

                formacion.bloque_curso = "1";
                formacion.etapa_curso = 1;
                formacion.tipo_formacion = "FEE";
                formacion.id_user = Usuario_logeado.id_usuario;

                //como estarán los botones inicialmente para cada nivel
                Load_Sig_Re();

                fecha_creacion = DateTime.Now;
                //btnSiguienteEtapa.Enabled = true; //Solo para tomar ss 

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
                llenarcomboRefrigerio();
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
        private void llenarcomboRefrigerio()
        {
            //llenar el combobox de la tercera etapa con los tipos de refrigerios
            cmbxTipoRefrigerio.ValueMember = "id_ref";
            cmbxTipoRefrigerio.DisplayMember = "nombre";
            cmbxTipoRefrigerio.DataSource = Paneles.llenarcmbxRef();
            cmbxTipoRefrigerio.SelectedIndex = -1;
        }
        private void llenarComboHorario(string tipo)
        {
            //llenar el combobox con los horarios registrados en la BD
            cmbxHorarios.ValueMember = "id_horario";
            cmbxHorarios.DisplayMember = "contenido_horario";
            cmbxHorarios.DataSource = Horarios.llenarcmbxHorario(tipo);
            cmbxHorarios.SelectedIndex = -1;
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
                txtSolicitadoPor.Clear();
            }else if (pnlNivel_intermedio.Visible == true)
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

            }else if (pnlNivel_avanzado.Visible == true)
            {
                cmbxHorarios.SelectedIndex = -1;
                cmbxTipoRefrigerio.SelectedIndex = -1;
                txtAula.Clear();
               
            }



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
                        GuardarBasico();
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
                if( ExisteFormacion == false)
                {
                    if (txtNombreFormacion.Text == "")
                    {
                        errorProviderNombreF.SetError(txtNombreFormacion, "Debe proporcionar un nombre a la formación.");
                        txtNombreFormacion.Focus();
                    }
                    else
                    {
                        errorProviderNombreF.SetError(txtNombreFormacion, "");
                        txtSolicitadoPor.Focus();//para que el foco se mueva al siguiente paso
                        if (txtSolicitadoPor.Text == "")
                        {
                            errorProviderSolicitado.SetError(txtSolicitadoPor, "Debe proporcionar esta información.");
                            txtSolicitadoPor.Focus();

                        }
                        else
                        {
                            errorProviderSolicitado.SetError(txtSolicitadoPor, "Debe proporcionar esta información.");
                            cmbxDuracionFormacion.Focus();
                            if (cmbxDuracionFormacion.SelectedIndex == -1)
                            {
                                errorProviderDuracionF.SetError(cmbxDuracionFormacion, "Debe proporcionar la duración de la formación.");
                                cmbxDuracionFormacion.Focus();
                            }
                            else
                            {
                                errorProviderDuracionF.SetError(cmbxDuracionFormacion, "");
                                if (btnVerContenido.Enabled == false)
                                {
                                    errorProviderContenido.SetError(btnRutaContenido, "Debe seleccionar un contenido para la formación.");
                                }
                                else
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
                                    p_inst.manual = "";

                                    formacion.fecha_inicial = fecha_creacion;
                                    formacion.TiempoEtapa = Convert.ToString(FinalE1 - fecha_creacion);

                                    if (conexion.abrirconexion() == true)
                                        p_inst.id_pinstruccional = Formaciones.ObtenerIdPaquete(conexion.conexion, p_inst);                                  
                                    
                                    conexion.cerrarconexion();

                                    //si no arroja coincidencias, no existe un paquete con el mismo contenido-- PROCEDE A GUARDAR
                                    if (p_inst.id_pinstruccional == 0)
                                    {
                                        int resultado2 = 0;

                                        if (conexion.abrirconexion() == true)
                                            resultado2 = Formaciones.GuardarPaqueteInstruccional(conexion.conexion, p_inst);

                                        conexion.cerrarconexion();
                                        //si se guardó con éxito: recoger el id de ese paquete.
                                        if (resultado2 != 0)
                                        {
                                            int id_paquete = 0;
                                            if (conexion.abrirconexion() == true)
                                                id_paquete = Clases.Formaciones.ObtenerIdPaquete(conexion.conexion, p_inst);

                                            formacion.pq_inst = id_paquete;
                                            conexion.cerrarconexion();
                                            if (id_paquete != 0)//se obtiene un id_intruccional cooncordante con el archivo subido
                                            {

                                                if (conexion.abrirconexion() == true)
                                                {
                                                    int resultado = 0;
                                                    resultado = Clases.Formaciones.AgregarNuevaFormacion(conexion.conexion, formacion);

                                                    conexion.cerrarconexion();
                                                    if (resultado > 0 && resultado2 > 0)
                                                    {
                                                        if (conexion.abrirconexion() == true)
                                                        {
                                                            int id_curso = Clases.Formaciones.CursoEjecucionExiste(conexion.conexion, formacion);
                                                            conexion.cerrarconexion();

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

                                                                        if (formacion.duracion == "8")
                                                                        {
                                                                            string tipo = "A";
                                                                            llenarComboHorario(tipo);
                                                                            gpbRefrigerio.Enabled = true;
                                                                        }
                                                                        else
                                                                        {
                                                                            //averiguar horarios usados en las jornadas de 6 hrs

                                                                        }

                                                                    }
                                                                }


                                                            }
                                                            else
                                                            {
                                                                MessageBox.Show("Error: No se pudo agregar la formación.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        MessageBox.Show("Error: No se pudo agregar la formación.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                    }

                                                }
                                            }
                                            else//si no se encuentra el id del paquete 
                                            {
                                                MessageBox.Show("No hay id paquete");
                                            }
                                        }

                                    }
                                    else//consiguió un paquete igualito
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
                                                        if (formacion.duracion == "8")
                                                        {
                                                            string tipo = "A";
                                                            llenarComboHorario(tipo);
                                                            gpbRefrigerio.Enabled = true;
                                                        }
                                                        else
                                                        {
                                                            //averiguar horarios usados en las jornadas de 6 hrs

                                                        }
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
                else //Si ExisteFormacion == true
                {
                    if (txtNombreFormacion.Text == "")
                    {
                        errorProviderNombreF.SetError(txtNombreFormacion, "Debe proporcionar el nombre de la formación.");
                        txtNombreFormacion.Focus();
                    }
                    else
                    {
                        errorProviderNombreF.SetError(txtNombreFormacion, "");
                        if (txtSolicitadoPor.Text == "")
                        {
                            errorProviderSolicitado.SetError(txtSolicitadoPor, "Debe proporcionar el nombre de quien solicita.");
                            txtSolicitadoPor.Focus();
                        }
                        else
                        {
                            errorProviderSolicitado.SetError(txtSolicitadoPor, "");
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

                                    p_inst.bitacora = "";
                                    p_inst.manual = "";
                                    formacion.fecha_inicial = fecha_creacion;
                                    formacion.TiempoEtapa = Convert.ToString(FinalE1 - fecha_creacion);
                                    formacion.etapa_curso = 1;//representa la etapa actual: nivel_basico (cambiará para cada panel)
                                    formacion.id_user = Clases.Usuario_logeado.id_usuario;
                                    formacion.nombre_formacion = txtNombreFormacion.Text;
                                    p_inst.id_pinstruccional = formacion.pq_inst;
                                    conexion.cerrarconexion();
                                    if (conexion.abrirconexion() == true)
                                    {
                                        int cursoexiste = Clases.Formaciones.CursoEjecucionExiste(conexion.conexion, formacion);
                                        conexion.cerrarconexion();

                                        if (cursoexiste == 0)
                                        {
                                            if (conexion.abrirconexion() == true)
                                            {
                                                int resultado = 0;
                                                resultado = Clases.Formaciones.AgregarNuevaFormacion(conexion.conexion, formacion);

                                                conexion.cerrarconexion();
                                                if (resultado > 0)
                                                {
                                                    if (conexion.abrirconexion() == true)
                                                    {
                                                        int id_curso = Clases.Formaciones.CursoEjecucionExiste(conexion.conexion, formacion);
                                                        conexion.cerrarconexion();

                                                        if (id_curso != 0)
                                                        {
                                                            if (conexion.abrirconexion() == true)
                                                            {
                                                                int agregarUGC = Clases.Formaciones.Agregar_U_g_C(conexion.conexion, id_curso, formacion.id_user, fecha_creacion, FinalE1);
                                                                conexion.cerrarconexion();
                                                                if (agregarUGC > 0)
                                                                {
                                                                    guardar = true;
                                                                    MessageBox.Show("La formación se ha agregado correctamente.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.None);
                                                                    if (formacion.duracion == "8")
                                                                    {
                                                                        string tipo = "A";
                                                                        llenarComboHorario(tipo);
                                                                        gpbRefrigerio.Enabled = true;
                                                                    }
                                                                    else
                                                                    {
                                                                        //averiguar horarios usados en las jornadas de 6 hrs

                                                                    }
                                                                }
                                                            }


                                                        }
                                                        else
                                                        {
                                                            //error no se pudo agregar la formacion
                                                            MessageBox.Show("Error: No se pudo agregar la formación.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                        }
                                                    }
                                                }

                                            }

                                        }
                                        else
                                        {
                                            MessageBox.Show("No se puede agregar esta formación. Existe una con el mismo nombre en estatus 'En curso'", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                            vaciarFormacion();
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
            try
            {
                if(rdbSiRef.Checked==false && rdbNoRef.Checked == false)
                {
                    errorProviderBloque.SetError(gpbRefrigerio, "Debe proporcionar esta información.");
                }else
                {
                    errorProviderBloque.SetError(gpbRefrigerio, "");
                    dtpFechaCurso.Focus();
                    if(dtpFechaCurso.Value <= DateTime.Today)
                    {
                        errorProviderFecha.SetError(dtpFechaCurso, "La fecha seleccionada es inválida.");
                        dtpFechaCurso.Focus();
                    }else
                    {
                        errorProviderFecha.SetError(dtpFechaCurso, "");
                        cmbxFa.Focus();
                        if (cmbxFa.SelectedIndex == -1)
                        {
                            errorProviderContenido.SetError(cmbxFa, "Debe seleccionar un facilitador para la formación.");
                        }else
                        {
                            errorProviderContenido.SetError(cmbxFa, "");
                            if (chkbCoFacilitador.Checked == true && cmbxCoFa.SelectedIndex == -1)
                            {
                                errorProviderContenido.SetError(cmbxCoFa, "No ha seleccionado ningún co-facilitador.");
                                cmbxCoFa.Focus();
                            }
                            else
                            {

                                if (nombre_publicidad.Count <= 0)
                                {
                                    errorProviderBloque.SetError(dgvMediosDifusion, "Debe seleccionar al menos 1 medio de difusión.");
                                }else
                                {
                                    time.fecha_curso = dtpFechaCurso.Value.ToString("yyyy-MM-dd HH:mm:ss");
                                  
                                    String duracionE2 = Convert.ToString(FinalE2 - inicioE2);
                                    List<Difusion> medios_publicitarios = new List<Difusion>();
                                    int id_curso = 0;

                                    //se obtiene el id del curso
                                    MySqlDataReader obtener_id_curso = Conexion.ConsultarBD("SELECT id_cursos FROM cursos WHERE nombre_curso = '" + txtNombreFormacion.Text + "' AND tipo_curso='FEE'");
                                    if (obtener_id_curso.Read())
                                    {
                                        id_curso = int.Parse(obtener_id_curso["id_cursos"].ToString());
                                    }
                                    obtener_id_curso.Close();
                                    Difusion d = new Difusion();
                                    //crear una lista donde se agreguen los nombres de cada fila para luego buscar el id de ellos (los seleccionado) y guardarlo en la bd
                                    for (int i = 0; i < nombre_publicidad.Count; i++)
                                    {
                                        string publi = nombre_publicidad[i].ToString();
                                        MySqlDataReader seleccionId = Conexion.ConsultarBD("SELECT id_difusion FROM difusion WHERE dif_contenido = '" + publi + "' LIMIT 1");
                                        while (seleccionId.Read())
                                        {
                                            d.id_dif = int.Parse(seleccionId["id_difusion"].ToString());
                                            d.contenido_dif = publi;
                                            medios_publicitarios.Add(d);

                                        }

                                        seleccionId.Close();
                                        MySqlDataReader guardarRelacionCursoPublicidad = Conexion.ConsultarBD("INSERT INTO cursos_tiene_publicidad (ctp_id_curso, ctp_id_difusion, ctp_contenido_dif) VALUES('" + id_curso + "', '" + medios_publicitarios[i].id_dif + "', '" + medios_publicitarios[i].contenido_dif + "')");

                                        guardarRelacionCursoPublicidad.Close();
                                    }
                                    if (rdbSiRef.Checked == false)
                                    {
                                        gpbSeleccionRef.Enabled = false;
                                        gpbSeleccionRef.Visible = true;
                                        MySqlDataReader ActualizarCursoTieneRef = Conexion.ConsultarBD("UPDATE cursos SET tiene_ref='0' ");
                                        ActualizarCursoTieneRef.Close();
                                    }
                                    else
                                    {
                                        //llenar el cmbx de horarios:
                                        cmbxHorarios.Items.Clear();
                                        string tipo = "B";
                                        llenarComboHorario(tipo);

                                        gpbSeleccionRef.Enabled = true;
                                        gpbSeleccionRef.Visible = true;
                                        MySqlDataReader ActualizarCursoTieneRef = Conexion.ConsultarBD("UPDATE cursos SET tiene_ref='1' ");
                                        ActualizarCursoTieneRef.Close();
                                    }

                                    //se actualiza el curso con los datos obtenidos de la segunda etapa, como las fechas
                                    MySqlDataReader ActualizarCursoSegundaEtapa = Conexion.ConsultarBD("UPDATE cursos SET etapa_curso ='2', fecha_uno='" + dtpFechaCurso.Value.ToString("yyyy-MM-dd HH:mm:ss") + "', duracionE2='" + duracionE2 + "', bloque_curso= '" + formacion.bloque_curso + "' WHERE id_cursos='" + id_curso + "'");
                                    ActualizarCursoSegundaEtapa.Close();
                                    // si el checkbox esta seleccionado es que tiene co-facilitador
                                    if (chkbCoFacilitador.Checked == true && cmbxCoFa.SelectedIndex != -1)
                                    {
                                        MySqlDataReader FacilitadorCurso = Conexion.ConsultarBD("INSERT INTO cursos_tienen_fa (cursos_id_cursos, facilitadores_id_fa, ctf_id_cofa, ctf_fecha) VALUES ('" + id_curso + "', '" + fa.id_facilitador + "', '" + Cofa.id_facilitador + "', '" + dtpFechaCurso.Value.ToString("yyyy-MM-dd HH:mm:ss") + "')");
                                        FacilitadorCurso.Close();
                                        guardar = true;
                                        MessageBox.Show("Los datos se han agregado correctamente.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.None);
                                    }
                                    else // sino, solo guarda al facilitador
                                    {
                                        MySqlDataReader FacilitadorCurso = Conexion.ConsultarBD("INSERT INTO cursos_tienen_fa (cursos_id_cursos, facilitadores_id_fa, ctf_fecha) VALUES ('" + id_curso + "', '" + fa.id_facilitador + "', '" + dtpFechaCurso.Value.ToString("yyyy-MM-dd HH:mm:ss") + "')");
                                        FacilitadorCurso.Close();
                                        guardar = true;
                                        MessageBox.Show("Los datos se han agregado correctamente.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.None);
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

        private void GuardarAvanzado()
        {
            try
            {
                if (cmbxHorarios.SelectedIndex == -1)
                {
                    errorProviderHora.SetError(cmbxHorarios, "Debe seleccionar uno de los horarios disponibles.");
                    cmbxHorarios.Focus();
                }
                else
                {
                    errorProviderHora.SetError(cmbxHorarios, "");
                    if (gpbSeleccionRef.Enabled == true)
                    {
                        if (cmbxTipoRefrigerio.SelectedIndex == -1)
                        {
                            errorProviderFecha.SetError(cmbxTipoRefrigerio, "Debe seleccionar un tipo de refrigerio para la formación.");
                            cmbxTipoRefrigerio.Focus();

                        }
                    }
                    errorProviderFecha.SetError(cmbxTipoRefrigerio, "");

                    if (txtAula.Text == "")
                    {
                        errorProviderNombreF.SetError(txtAula, "Debe proporcionar el aula en el que se dictará la formación.");
                        txtAula.Focus();
                    }
                    else
                    {
                        errorProviderNombreF.SetError(txtAula, "");
                        if (lista_insumo.Count <= 0)
                        {
                            errorProviderContenido.SetError(dgvInsumos, "Debe seleccionar los insumos que se usarán durante la formación.");

                        }
                        else
                        {
                            errorProviderContenido.SetError(dgvInsumos, "");
                                                        
                            String duracionE3 = Convert.ToString(FinalE3 - inicioE3);
                            string aula = txtAula.Text;

                            List<Insumos> insumos_curso = new List<Insumos>();

                            int id_curso = 0;

                            //se obtiene el id del curso
                            MySqlDataReader obtener_id_curso = Conexion.ConsultarBD("SELECT id_cursos FROM cursos WHERE nombre_curso = '" + txtNombreFormacion.Text + "' AND tipo_curso='FEE'");
                            if (obtener_id_curso.Read())
                            {
                                id_curso = int.Parse(obtener_id_curso["id_cursos"].ToString());
                            }
                            obtener_id_curso.Close();

                            Insumos insu = new Insumos();
                            //crear una lista donde se agreguen los nombres de cada fila para luego buscar el id de ellos (los seleccionado) y guardarlo en la bd
                            for (int i = 0; i < lista_insumo.Count; i++)
                            {
                                string publi = lista_insumo[i].ToString();
                                MySqlDataReader seleccionId = Conexion.ConsultarBD("SELECT id_insumos FROM insumos WHERE ins_contenido = '" + publi + "' LIMIT 1");
                                while (seleccionId.Read())
                                {

                                    insu.id_insumos = int.Parse(seleccionId["id_insumos"].ToString());
                                    insu.contenido_insumo = publi;
                                    insumos_curso.Add(insu);

                                }

                                seleccionId.Close();
                                MySqlDataReader guardarRelacionCursoInsumo = Conexion.ConsultarBD("INSERT INTO cursos_tienen_insumos (cti_id_curso, cti_id_insumo, cti_contenido_insumo) VALUES('" + id_curso + "', '" + insumos_curso[i].id_insumos + "', '" + insumos_curso[i].contenido_insumo + "')");

                                guardarRelacionCursoInsumo.Close();
                            }

                            ////se actualiza la informacion del curso con los valores nuevos: 

                            if (id_refrigerio > 0)
                            {
                                MySqlDataReader ActualizarCursoTerceraEtapa = Conexion.ConsultarBD("UPDATE cursos SET etapa_curso ='3', duracionE3='" + duracionE3 + "', horario_uno='" + id_horario + "', aula_dia1='" + aula + "', id_ref1='" + id_refrigerio + "' WHERE id_cursos='" + id_curso + "'");
                                ActualizarCursoTerceraEtapa.Close();
                            }
                            else
                            {
                                MySqlDataReader ActualizarCursoTerceraEtapa = Conexion.ConsultarBD("UPDATE cursos SET etapa_curso ='3', duracionE3='" + duracionE3 + "', horario_uno='" + id_horario + "', aula_dia1='" + aula + "' WHERE id_cursos='" + id_curso + "'");
                                ActualizarCursoTerceraEtapa.Close();
                            }

                            MessageBox.Show("Los datos se han agregado correctamente.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.None);


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

                if (formacion.duracion == "8" && formacion.bloque_curso == "1")
                {
                    Controles_nivel_intermedio_EstatusInicial();
                    gpbRefrigerio.Enabled = true;
                    rdbNoRef.Checked = true;
                }
                pnlNivel_intermedio.Visible = true;
                Load_Sig_Re();
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

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (Clases.Formaciones.creacion == true)
            {
                if (pnlNivel_basico.Visible == true)
                {
                    FinalE1 = DateTime.Now;
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
                        FinalE2 = DateTime.Now;
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
                            FinalE3 = DateTime.Now;
                            GuardarAvanzado();
                        }
                    }
                }

            }

        }

        private void btnRetomar_Click(object sender, EventArgs e)
        {
            //cuando retomar = habilitar los controles
            if (pnlNivel_basico.Visible == true)
            {
                txtNombreFormacion.Enabled = true;
                txtSolicitadoPor.Enabled = true;
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
                chkbCoFacilitador.Enabled = true;
                cmbxFa.Enabled = true;
                gpbDifusion.Enabled = true;
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
        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            vaciarFormacion();
            btnSiguienteEtapa.Enabled = false;
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
                            existeReprogramado = Clases.Formaciones.CursoOtroStatusExiste(formacion, statusR);
                            conexion.cerrarconexion();

                            if (existeReprogramado != 0)//Si existe curso reprogramado
                            {
                                errorProviderNombreF.SetError(txtNombreFormacion, "Ya existe una formacion con este nombre en estado 'Reprogramado'");
                                txtNombreFormacion.Text = "";
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
                                                btnRutaBitacora.Enabled = true;
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
                
            }
        }
        private void cmbxDuracionFormacion_SelectionChangeCommitted(object sender, EventArgs e)
        {
            string duracion = Convert.ToString(cmbxDuracionFormacion.SelectedIndex);//se obtiene el valor de la duracion del curso
            switch (duracion)
            {
                case "0":
                    duracion = "6";
                    break;
                case "1":
                    duracion = "8";
                    break;

            }
            formacion.duracion = duracion;
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

        /*-------------------- Controles del Nivel_intermedio ------------------------*/
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
            //validar si estará disponible para la fecha del dia 1
            if (conexion.abrirconexion() == true)
            {
                int fa_disponible = Clases.Facilitadores.FacilitadorDisponibleDia(conexion.conexion, time.fecha_curso, fa.id_facilitador);
                conexion.cerrarconexion();
                if (fa_disponible !=0 )//si el id que retorna es el del facilitador seleccionado: este ya está ocupado para esa fecha
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
                        faDatos =Facilitadores.SeleccionarFaPorID(conexion.conexion, fa.id_facilitador);
                        conexion.cerrarconexion();
                        txtTlfnFa.Text = faDatos.tlfn_facilitador;
                        txtCorreoFa.Text = faDatos.correo_facilitador;
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
            //validar si estará disponible para la fecha del dia 1
            if (conexion.abrirconexion() == true)
            {
                int fa_disponible = Clases.Facilitadores.FacilitadorDisponibleDia(conexion.conexion, time.fecha_curso, Cofa.id_facilitador);
                conexion.cerrarconexion();
                if (fa_disponible !=0 )//si el id que retorna es el del facilitador seleccionado: este ya está ocupado para esa fecha
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
                time.fecha_curso = dtpFechaCurso.Value.ToString("yyyy-MM-dd"); ;
                   gpbFacilitador.Enabled = true;
                
            }
        }

        

        private void dgvMediosDifusion_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Detecta si se ha seleccionado el header de la grilla
            //
            if (e.RowIndex == -1)
                return;

            if (dgvMediosDifusion.Columns[e.ColumnIndex].Name == "seleccionar_opcion")
            {

                //
                // Se toma la fila seleccionada
                //
                DataGridViewRow row = dgvMediosDifusion.Rows[e.RowIndex];

                //
                // Se selecciona la celda del checkbox
                //
                DataGridViewCheckBoxCell cellSelecion = row.Cells["seleccionar_opcion"] as DataGridViewCheckBoxCell;


                if (Convert.ToBoolean(cellSelecion.Value))
                {
                    row.DefaultCellStyle.BackColor = Color.Green;
                    string nombre = row.Cells["opcion_difusion"].Value.ToString();
                    nombre_publicidad.Add(nombre);


                }
                else
                {
                    row.DefaultCellStyle.BackColor = Color.Red;

                    for (int i = 0; i < nombre_publicidad.Count; i++)
                    {

                        if (row.Cells["opcion_difusion"].Value.ToString() == nombre_publicidad[i].ToString())
                        {
                            nombre_publicidad.RemoveAt(i);
                        }


                    }

                }


            }


        }
        private void dgvMediosDifusion_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            //aporte de rafael
            if (dgvMediosDifusion.IsCurrentCellDirty)
            {
                dgvMediosDifusion.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        /*-----------------Controles Nivel Avanzado--------------------------------*/
        private void dgvInsumos_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            //aporte de rafael
            if (dgvInsumos.IsCurrentCellDirty)
            {
                dgvInsumos.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }
        private void dgvInsumos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Detecta si se ha seleccionado el header de la grilla
            //
            if (e.RowIndex == -1)
                return;
            if (dgvInsumos.Columns[e.ColumnIndex].Name == "seleccion_opcion")
            {

                //
                // Se toma la fila seleccionada
                //
                DataGridViewRow row = dgvInsumos.Rows[e.RowIndex];

                //
                // Se selecciona la celda del checkbox
                //
                DataGridViewCheckBoxCell cellSelecion = row.Cells["seleccion_opcion"] as DataGridViewCheckBoxCell;


                if (Convert.ToBoolean(cellSelecion.Value))
                {
                    row.DefaultCellStyle.BackColor = Color.Green;
                    string nombre = row.Cells["insumo"].Value.ToString();
                    lista_insumo.Add(nombre);


                }
                else
                {
                    row.DefaultCellStyle.BackColor = Color.Red;

                    for (int i = 0; i < lista_insumo.Count; i++)
                    {

                        if (row.Cells["insumo"].Value.ToString() == lista_insumo[i].ToString())
                        {
                            lista_insumo.RemoveAt(i);
                        }


                    }

                }

            }
        }

        private void cmbxHorarios_SelectionChangeCommitted(object sender, EventArgs e)
        {
            inicioE3 = DateTime.Now;
            id_horario = Convert.ToInt32(cmbxHorarios.SelectedIndex);       
        }

        private void cmbxTipoRefrigerio_SelectionChangeCommitted(object sender, EventArgs e)
        {
            id_refrigerio = Convert.ToInt32(cmbxTipoRefrigerio.SelectedValue);
           
        }
    }
}
