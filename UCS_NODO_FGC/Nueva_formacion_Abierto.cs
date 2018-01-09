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
    public partial class Nueva_formacion_Abierto : Form
    {
        Clases.Formaciones formacion = new Clases.Formaciones();
        Clases.conexion_bd conexion = new Clases.conexion_bd();
        Clases.Tiempos_curso time = new Clases.Tiempos_curso();
        Clases.Facilitadores fa = new Clases.Facilitadores();
        Clases.Facilitadores Cofa = new Clases.Facilitadores();
        Clases.Facilitadores faDatos = new Clases.Facilitadores();
        Clases.Paquete_instruccional p_inst = new Clases.Paquete_instruccional();
        List<Clases.Paquete_instruccional> pLista = new List<Clases.Paquete_instruccional>();
        bool guardar = false;
        string duracion = "";
        bool ExisteFormacion;
        string presentacion = "";
        string contenido="";
        string bloques = "";

        DateTime fecha_creacion, fecha_modifinal;


        public Nueva_formacion_Abierto()
        {
            InitializeComponent();
        }
        
        //Load
        private void Nueva_formacion_Load(object sender, EventArgs e)
        {
            if (Clases.Formaciones.creacion == true)//si viene desde la pagina principal
            {
                LabelCabecera.Text = "Nuevo Abierto: Información básica";
                LabelCabecera.Location = new Point(150, 31);

                lblEtapaSiguiente.Text = "Nivel Intermedio";
                lblEtapaSiguiente.Location = new Point(17, 529);

                lblEtapafinal.Text = "Nivel Avanzado";
                lblEtapafinal.Location = new Point(22, 570);

                this.Location = new Point(-5, 0);

                fecha_creacion = DateTime.Now;

                btnVerPresentacion.Enabled = false;
                btnVerContenido.Enabled = false;
                btnRutaContenido.Enabled = false;
                btnRutaPresentacion.Enabled = false;

                //como estarán los botones inicialmente para cada nivel
                Load_Sig_Re();

                //btnSiguienteEtapa.Enabled = true; //Solo para tomar ss


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
                   
                    
                }
                //cargar los facilitadores del nivel_intermedio
                llenarcomboFacilitador();
            }
            
            
        }


        /* ------------- Metodos -------------------*/
        private void Controles_nivel_intermedio_EstatusInicial()
        {
            gpbRefrigerio.Enabled = false;
            gpbFecha.Enabled = true;
            gpbSegundaFecha.Enabled = false;
            gpbDifusion.Enabled = true;
            gpbFacilitador.Enabled = false;
            gpbDatosFa.Enabled = false;
            chkbCoFacilitador.Enabled = false;
            gpbCoFa.Enabled = false;
            gpbDatosCoFa.Enabled = false;
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
        private void Load_Sig_Re()
        {
            btnRetomar.Enabled = false;
            btnSiguienteEtapa.Enabled = false;
            btnModificar.Enabled = false;

            btnGuardar.Enabled = true;
            btnPausar.Enabled = true;
            btnLimpiar.Enabled = true;
        }
        private void GuardarBasico()
        {
            //el estatus del curso en esta etapa siempre será "En curso"
            // los posibles estatus son: En curso, Reprogramado, Suspendido

            try
            {
                if (ExisteFormacion == false)
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
                                    if (contenido == "")
                                    {
                                        errorProviderContenido.SetError(btnRutaContenido, "Debe seleccionar un contenido para la formación.");

                                    }
                                    else //si el contenido existe
                                    {
                                        errorProviderContenido.SetError(btnRutaContenido, "");
                                        if (conexion.abrirconexion() == true)
                                        {
                                            if (btnVerPresentacion.Enabled == false)
                                            {
                                                p_inst.presentacion = "";

                                            }
                                            p_inst.bitacora = "";
                                            p_inst.manual = "";
                                            formacion.fecha_inicial = fecha_creacion;
                                            formacion.TiempoEtapa = Convert.ToString(fecha_modifinal - fecha_creacion);
                                            //MessageBox.Show(formacion.TiempoEtapa);
                                            formacion.id_user = Clases.Usuario_logeado.id_usuario;

                                            p_inst.id_pinstruccional = Clases.Formaciones.ObtenerIdPaquete(conexion.conexion, p_inst);

                                            formacion.etapa_curso = 1;//representa la etapa actual: nivel_basico (cambiará para cada panel)

                                            conexion.cerrarconexion();
                                            if (p_inst.id_pinstruccional == 0)//si no arroja coincidencias, no existe un paquete con el mismo contenido-- PROCEDE A GUARDAR
                                            {
                                                if (conexion.abrirconexion() == true)
                                                {

                                                    int resultado2 = Clases.Formaciones.GuardarPaqueteInstruccional(conexion.conexion, p_inst);
                                                    conexion.cerrarconexion();


                                                    if (conexion.abrirconexion() == true)
                                                    {
                                                        if (resultado2 != 0)//si se guardó con éxito: recoger el id de ese paquete.
                                                        {
                                                            int id_paquete = Clases.Formaciones.ObtenerIdPaquete(conexion.conexion, p_inst);
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
                                                                                    int agregarUGC = Clases.Formaciones.Agregar_U_g_C(conexion.conexion, id_curso, formacion.id_user,fecha_creacion, fecha_modifinal);
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
                                                                    }
                                                                    else
                                                                    {
                                                                        MessageBox.Show("Error: No se pudo agregar la formación 1.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                                    }

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
                                                }
                                            }
                                            else//consiguió un paquete igualito
                                            {
                                                VerPaqueteInst(p_inst.id_pinstruccional);
                                            }


                                        }

                                    }
                                }
                            }
                        }
                    }


                } else //Si ExisteFormacion == true
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
                                    formacion.TiempoEtapa = Convert.ToString(fecha_modifinal - fecha_creacion);
                                    formacion.etapa_curso = 1;//representa la etapa actual: nivel_basico (cambiará para cada panel)
                                    formacion.id_user = Clases.Usuario_logeado.id_usuario;

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
                                                                int agregarUGC = Clases.Formaciones.Agregar_U_g_C(conexion.conexion, id_curso, formacion.id_user, fecha_creacion, fecha_modifinal);
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
                                                            //error no se pudo agregar la formacion
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
                MessageBox.Show("Error: "+ex.Message);
                conexion.cerrarconexion();

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

        private void GuardarIntermedio()
        {
            try
            {
                //primer caso: cuando la formacion es de 4 hrs

                if (formacion.duracion == "4")
                {
                    if(dtpFechaCurso.Value <= DateTime.Today)
                    {
                        errorProviderFecha.SetError(dtpFechaCurso, "La fecha seleccionada es inválida.");
                        dtpFechaCurso.Focus();
                    }
                    else
                    {
                        errorProviderFecha.SetError(dtpFechaCurso, "");
                        //crear una lista donde se agreguen los nombres de cada fila para luego buscar el id de ellos (los seleccionado) y guardarlo en la bd

                        foreach (DataGridViewRow r in dgvMediosDifusion.Rows)
                        {
                            DataGridViewCheckBoxCell ck = r.Cells["seleccionar_opcion"] as DataGridViewCheckBoxCell;
                            if (Convert.ToBoolean(ck.Value))
                            {

                            }
                            else
                            {

                            }

                        }
                        //if ()
                        time.fecha_curso = dtpFechaCurso.Value;
                    }
                }
                else
                {
                    //segundo caso: cuando la formaicon es de 8 horas y tiene dos bloques, o sea, dos diías de 4 hrs
                    if(formacion.duracion=="8" && formacion.bloque_curso == "2")
                    {

                    }else
                    {
                        //tercer caso: cuando la formacion es de 8 horas y tiene 1 bloque, o sea, se hace en un día
                        if(formacion.duracion == "8" && formacion.bloque_curso == "1")
                        {

                        }else
                        {
                            //ultimo caso: cuando la formacion es de 16 horas, siempre tendrá dos bloques, o sea, dos dias
                            if(formacion.duracion == "16")
                            {

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
        /* ------------- Fin Metodos -------------------*/




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
                if(txtNombreFormacion.Text != "")//siempre y cuando el txt contenga algo, se valida
                {
                    if (conexion.abrirconexion() == true)
                    {
                        formacion.estatus = "En curso"; //predeterminado en esta etapa
                        formacion.tipo_formacion = "Abierto"; //predeterminado para este form
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
                                            List<Clases.Paquete_instruccional> paqueteExiste =  new List<Clases.Paquete_instruccional>();
                                            paqueteExiste = Clases.Formaciones.ObtenerPaqueteStatusCursoDistinto(conexion.conexion, formacion);
                                            conexion.cerrarconexion();

                                            if(paqueteExiste.Count != 0)
                                            {
                                              
                                                List<int> IdDistintos = new List<int>();
                                                //selecciona solo los números únicos
                                               IdDistintos= paqueteExiste.Select(x => x.id_pinstruccional).Distinct().ToList();
                                                
                                                if(IdDistintos.Count == 1)
                                                {
                                                   
                                                    VerPaqueteInst(IdDistintos[0]);
                                                }
                                                
                                            }else
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
                        cmbxBloques.Enabled = true;
                        break;
                    case "1":
                        duracion = "8";
                        cmbxBloques.Items.Clear();
                        cmbxBloques.Items.Add("1");
                        cmbxBloques.Items.Add("2");
                        cmbxBloques.Enabled = true;
                        break;
                    case "2":
                        duracion = "16";
                        cmbxBloques.Items.Clear();
                        cmbxBloques.Items.Add("2");
                        cmbxBloques.Enabled = true;
                        break;
                }
                formacion.duracion = duracion;
            }
        }


        //comboboxBloquesFormacion (Nivel_basico)
        private void cmbxBloques_Validating(object sender, CancelEventArgs e)
        {
            if(cmbxBloques.SelectedIndex == -1)
            {
                errorProviderBloque.SetError(cmbxBloques, "Debe proporcionar los bloques de la formación.");
                cmbxBloques.Focus();
                formacion.bloque_curso = "";
            }else
            {
                errorProviderBloque.SetError(cmbxBloques, "");
                bloques = Convert.ToString(cmbxBloques.SelectedIndex);
                if(duracion == "8")
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

                }else if (duracion == "16")
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
            {   if(ExisteFormacion == false)
                {
                    
                    OpenFileDialog od = new OpenFileDialog();
                    od.Filter = "PDF files |*.pdf";
                    if (od.ShowDialog() == DialogResult.OK)
                    {
                        contenido = od.FileName;
                       
                        p_inst.contenido =contenido;
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
                        presentacion = od.FileName;
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


        /* ------------- Final controles del nivel básico -------------------*/



        /* ------------- Botones del panel lateral derecho (aplica para todos los niveles) -------------------*/

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if(Clases.Formaciones.creacion == true)
            {
                if (pnlNivel_basico.Visible == true)
                {
                    fecha_modifinal = DateTime.Now;
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

                }else
                {
                    if (pnlNivel_intermedio.Visible == true)
                    {
                        
                    }
                    else
                    {
                        if (pnlNivel_avanzado.Visible == true)
                        {

                        }
                    }
                }

            }                     

        }             

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            vaciarFormacion();
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
                txtNombreFormacion.Enabled = true;
                txtSolicitadoPor.Enabled = true;
                cmbxDuracionFormacion.Enabled = true;
                cmbxBloques.Enabled = true;
                btnRutaContenido.Enabled = true;
                btnRutaPresentacion.Enabled = true;
                //Y lo deja igual que el load
            }else
            {
                if (pnlNivel_intermedio.Visible == true)
                {

                }
            }

            Load_Sig_Re();
            if (guardar == true)
            {
                btnSiguienteEtapa.Enabled = true;
            }
        }

        private void btnSiguienteEtapa_Click(object sender, EventArgs e)
        {
            guardar = false;
            //cuando Siguiente etapa, quita el panel actual
            if (pnlNivel_basico.Visible == true)
            {
                LabelCabecera.Text = "Nuevo Abierto: Detalles técnicos";
                LabelCabecera.Location = new Point(180, 31);

                lblEtapaSiguiente.Text = "Nivel Avanzado";
                lblEtapaSiguiente.Location = new Point(22, 529);
                lblEtapafinal.Text = "Añadir participantes";
                lblEtapafinal.Location = new Point(3, 570);
                pnlNivel_basico.Visible = false;


                //comportamiento del panel nivel_intermedio de acuerdo a la duracion del curso
                if (formacion.duracion == "4")
                {
                    Controles_nivel_intermedio_EstatusInicial();
                    dtpFechaCurso.Focus();
                }
                else
                {
                    if (formacion.duracion == "8" && formacion.bloque_curso == "1")
                    {
                        Controles_nivel_intermedio_EstatusInicial();
                        gpbRefrigerio.Enabled = true;
                        rdbNoRef.Checked = true;
                    }
                    else if (formacion.duracion == "8" && formacion.bloque_curso == "2")
                    {
                        Controles_nivel_intermedio_EstatusInicial();

                        dtpFechaCurso.Focus();
                    }
                    else
                    {
                        if (formacion.duracion == "16")
                        {
                            Controles_nivel_intermedio_EstatusInicial();

                            gpbRefrigerio.Enabled = true;
                            rdbNoRef.Checked = true;
                        }
                    }
                }
                pnlNivel_intermedio.Visible = true;
                Load_Sig_Re();
            }
            else
            {
                if (pnlNivel_intermedio.Visible == true)
                {
                    LabelCabecera.Text = "Nuevo Abierto: Logística";
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
            
            // Y lo deja igual que cuando el load
            
        }

       

        /* ------------- Final botones del panel lateral derecho (aplica para todos los niveles) -------------------*/

        /* ------------- Controles del Nivel_intermedio -------------------*/

        private void chkbCoFacilitador_CheckedChanged(object sender, EventArgs e)
        {
            if (chkbCoFacilitador.Checked == true)
            {
                gpbCoFa.Enabled = true;
                cmbxCoFa.Focus();
                
            }else
            {
                gpbCoFa.Enabled = false;
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
                time.fechaDos_curso = dtpSegundaFecha.Value;
                gpbFacilitador.Enabled = true;
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
                    int fa_disponible = Clases.Facilitadores.FacilitadorDisponibleDia(conexion.conexion, time.fecha_curso, fa.id_facilitador, formacion.id_curso);
                    conexion.cerrarconexion();
                    if (fa_disponible != 0)//si retorna algo es que este facilitador ya está ocupado para esa fecha
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
            else if ((formacion.duracion == "16") || (formacion.duracion == "8" && formacion.bloque_curso == "2"))
            {
                //validar ambas fechas
                if (conexion.abrirconexion() == true)
                {
                    int fa_disponible = Clases.Facilitadores.FacilitadorDisponibleDia(conexion.conexion, time.fecha_curso, fa.id_facilitador, formacion.id_curso);
                    conexion.cerrarconexion();
                    if (fa_disponible !=0)//si retorna algo es que este facilitador ya está ocupado para esa fecha
                    {
                        errorProviderPresentacion.SetError(cmbxFa, "El facilitador está ocupado en esta fecha : " + dtpFechaCurso.ToString() + ".");
                        cmbxFa.SelectedIndex = -1;
                        fa.id_facilitador = 0;
                        cmbxFa.Focus();
                    }
                    else
                    {
                        errorProviderPresentacion.SetError(cmbxFa, "");
                        if (conexion.abrirconexion() == true)
                        {
                            int fa_disponibleDia2 = Clases.Facilitadores.FacilitadorDisponibleDia2(conexion.conexion, time.fechaDos_curso, fa.id_facilitador, formacion.id_curso);
                            conexion.cerrarconexion();
                            if (fa_disponibleDia2 !=0)//si retorna algo es que este facilitador ya está ocupado para esa fecha
                            {
                                errorProviderPresentacion.SetError(cmbxFa, "El facilitador está ocupado en esta fecha : " + dtpSegundaFecha.ToString() + ".");
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
                    int fa_disponible = Clases.Facilitadores.CoFacilitadorDisponibleDia(conexion.conexion, time.fecha_curso, Cofa.id_facilitador, formacion.id_curso);
                    conexion.cerrarconexion();
                    if (fa_disponible != 0)//si retorna algo es que este facilitador ya está ocupado para esa fecha
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
            else if ((formacion.duracion == "16") || (formacion.duracion == "8" && formacion.bloque_curso == "2"))
            {
                //validar ambas fechas
                if (conexion.abrirconexion() == true)
                {
                    int fa_disponible = Clases.Facilitadores.CoFacilitadorDisponibleDia(conexion.conexion, time.fecha_curso, Cofa.id_facilitador, formacion.id_curso);
                    conexion.cerrarconexion();
                    if (fa_disponible != 0)//si retorna algo es que este facilitador ya está ocupado para esa fecha
                    {
                        errorProviderPresentacion.SetError(cmbxCoFa, "El Co-facilitador está ocupado en esta fecha : " + dtpFechaCurso.ToString() + ".");
                        cmbxCoFa.SelectedIndex = -1;
                        Cofa.id_facilitador = 0;
                        cmbxCoFa.Focus();
                    }
                    else
                    {
                        errorProviderPresentacion.SetError(cmbxCoFa, "");
                        if (conexion.abrirconexion() == true)
                        {
                            int fa_disponibleDia2 = Clases.Facilitadores.CoFacilitadorDisponibleDia2(conexion.conexion, time.fechaDos_curso, Cofa.id_facilitador, formacion.id_curso);
                            conexion.cerrarconexion();
                            if (fa_disponibleDia2 != 0)//si retorna algo es que este facilitador ya está ocupado para esa fecha
                            {
                                errorProviderPresentacion.SetError(cmbxCoFa, "El Co-facilitador está ocupado en la fecha : " + dtpSegundaFecha.ToString() + ".");
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

        private void dtpFechaCurso_ValueChanged(object sender, EventArgs e)
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
                if ((formacion.duracion == "8" && formacion.bloque_curso == "2") || (formacion.duracion == "16"))
                {
                    gpbSegundaFecha.Enabled = true;
                    dtpSegundaFecha.Enabled = true;
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
                time.fechaDos_curso = dtpSegundaFecha.Value;
                gpbFacilitador.Enabled = true;
            }
        }

        private void dgvMediosDifusion_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            foreach(DataGridView row in dgvMediosDifusion.Rows)
            {
               // CheckBox check = row("chkSeleccion") as CheckBox;

                //if (check.Checked)
                //{
                //    //aqui la fila esta marcada
                //}
            }
            //Int32 selectedCellCount = dgvMediosDifusion.GetCellCount(DataGridViewElementStates.Selected);
            //if (selectedCellCount > 0)
            //{
            //    System.Text.StringBuilder indices = new System.Text.StringBuilder();
            //    for (int i = 0; i < selectedCellCount; i++)
            //    {
            //        indices.Append("Fila: ");
            //        indices.Append(dgvMediosDifusion.SelectedCells[i]).ToString();
            //        indices.Append(", Columna: ");
            //        indices.Append(dgvMediosDifusion.SelectedCells[i].ColumnIndex.ToString());
            //        indices.Append(Environment.NewLine);
            //    }
            //    indices.Append("Total: " + selectedCellCount.ToString());
            //    MessageBox.Show(indices.ToString(), " de celdas seleecionnadas");
            //}

        }

        private void txtNombreFormacion_TextChanged(object sender, EventArgs e)
        {

        }

        private void cmbxFa_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void gpbFacilitador_Enter(object sender, EventArgs e)
        {

        }

        
        private void dgvMediosDifusion_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            //aporte de rafael
            if (dgvMediosDifusion.IsCurrentCellDirty)
            {
                dgvMediosDifusion.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void dgvMediosDifusion_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            //aporte de rafael
        }

        private void dtpFechaCurso_Validating(object sender, CancelEventArgs e)
        {
            if(dtpFechaCurso.Value <= DateTime.Today)
            {
                errorProviderFecha.SetError(dtpFechaCurso, "La fecha seleccionada es inválida.");
                dtpFechaCurso.Focus();
            }else
            {
                errorProviderFecha.SetError(dtpFechaCurso, "");
                time.fecha_curso = dtpFechaCurso.Value;
                if ((formacion.duracion == "8" && formacion.bloque_curso == "2")|| (formacion.duracion == "16"))
                {
                    gpbSegundaFecha.Enabled = true;
                    dtpSegundaFecha.Focus();
                }else if ((formacion.duracion == "4") || (formacion.duracion == "8" && formacion.bloque_curso == "1"))
                {
                    gpbFacilitador.Enabled = true;
                }
                

            }
        }
       

        

        

        //    esto va dentro del boton guardar.
        //    FechaFinalForm = DateTime.Now;
        //    if (primeravez == 0)
        //    { 
        //        Duracion = new TimeSpan(FechaFinalForm.Ticks - FechaInicioForm.Ticks) + Duracion;
        //        MessageBox.Show("el tiempo que ha transcurrido es: " + Duracion.ToString());
        //    }
        //    else
        //    {
        //        Duracion1 = new TimeSpan(FechaFinalForm.Ticks - FechaPausaForm.Ticks) + Duracion;
        //        MessageBox.Show("el tiempo que ha transcurrido es duracion 1 : " + Duracion1.ToString());
        //    }
    }
}
