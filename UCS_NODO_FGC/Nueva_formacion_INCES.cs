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

    public partial class Nueva_formacion_INCES : Form
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
        string contenido = "";
        string manual = "";
        string bitacora = "";
        string bloques = "";
        public Nueva_formacion_INCES()
        {

            InitializeComponent();
        }

        private void Nueva_formacion_INCES_Load(object sender, EventArgs e)
        {
            if (Clases.Formaciones.creacion == true)//si viene referenciado del boton de la pagina principal
            {//------------------------------------------todo hay que hacerlo aquí(un nuevo ingreso)
                this.Location = new Point(-5, 0);


                
                LabelCabecera.Text = "Nuevo INCES: Información básica";
                LabelCabecera.Location = new Point(150, 31);

                lblEtapaSiguiente.Text = "Nivel Intermedio";
                lblEtapaSiguiente.Location = new Point(17, 529);

                lblEtapafinal.Text = "Nivel Avanzado";
                lblEtapafinal.Location = new Point(22, 570);

                // como estarán los botones inicialmente para cada nivel
                Load_Sig_Re();



                btnVerPresentacion.Enabled = false;
                btnVerContenido.Enabled = false;
                btnRutaContenido.Enabled = false;
                btnRutaPresentacion.Enabled = false;
                btnRutaBitacora.Enabled = false;
                btnRutaManual.Enabled = false;
                btnVerBitacora.Enabled = false;
                btnVerManual.Enabled = false;

                

                //controles del nivel intermedio
                Controles_nivel_intermedio_EstatusInicial();

                //aqui de una vez se carga la publicidad existente para el Nivel_intermedio
                conexion.cerrarconexion();
                if (conexion.abrirconexion() == true)
                {
                   
                    string difu = "";
                    CargarDatosInsumos(conexion.conexion, difu);
                    dgvInsumos.ClearSelection();
                    conexion.cerrarconexion();
                }

               
                cmbxCursoInce.Items.Clear();
                cmbxSolicitadoPor.Items.Clear();
                MySqlDataReader CursosInces = Conexion.ConsultarBD("SELECT nombre_curso_ince FROM cursos_inces ");

                while (CursosInces.Read())
                {               
                    cmbxCursoInce.Items.Add(CursosInces["nombre_curso_ince"]);
                }
                CursosInces.Close();



                MySqlDataReader SolicitadoPor = Conexion.ConsultarBD("SELECT nombre_empresa FROM clientes");
                while (SolicitadoPor.Read())
                {
                    cmbxSolicitadoPor.Items.Add(SolicitadoPor["nombre_empresa"]);
                }
                SolicitadoPor.Close();
                

            }
        }
        /*----------------- METODOS ----------------*/
        private void Load_Sig_Re()
        {
            btnRetomar.Enabled = false;
            btnSiguienteEtapa.Enabled = false;
            btnModificar.Enabled = false;
            btnGuardar.Enabled = true;
            btnPausar.Enabled = true;
            btnLimpiar.Enabled = true;
        }

        private void Controles_nivel_intermedio_EstatusInicial()
        {
           
            gpbFechaHora.Enabled = true;
            
            gpbFacilitador.Enabled = false;
            gpbDatosFa.Enabled = false;
            chkbCoFacilitador.Enabled = false;
            gpbCoFa.Enabled = false;
            gpbDatosCoFa.Enabled = false;
           

           
        }
        
        
       private void CargarDatosPrimeraEtapa()
        {

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

       //falta llenar el cmbxFacilitadores con los facilitadores asignados al curso seleccionado en la etapa_basica
       


        /*------------------ Botones panel lateral derecho -------------------*/
        private void btnSiguienteEtapa_Click(object sender, EventArgs e)
        {
            btnSiguienteEtapa.Enabled = false;
            btnGuardar.Enabled = true;

            //cuando Siguiente etapa, quita el panel actual
            if (pnlNivel_basico.Visible == true)
            {
                LabelCabecera.Text = "Nuevo INCES: Detalles técnicos";
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
                    LabelCabecera.Text = "Nuevo INCES: Logística";
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







        /*-------------------- Controles del Nivel_basico ------------------------*/

        //falta cargar el cmbxSolicitadoPor: con los nombres de las empresas que están registradas
        //falta cargar en cmbxNombreFormacion: El nombre de las formaciones inces registradas previamente en el modulo cursos_INCES

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
                        manual = od.FileName;

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
                        bitacora = od.FileName;

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





        /*-------------------- Controles del Nivel_intermedio ------------------------*/
        private void chkbCoFacilitador_CheckedChanged(object sender, EventArgs e)
        {
            if (chkbCoFacilitador.Checked == true)
            {
                gpbCoFa.Enabled = true;
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
           
        }

        private void cmbxFa_Validating(object sender, CancelEventArgs e)
        {
            if ((formacion.duracion == "16") )
            {
                //validar ambas fechas
                if (conexion.abrirconexion() == true)
                {
                    int fa_disponible = Clases.Facilitadores.FacilitadorDisponibleDia1(conexion.conexion, time.fecha_curso);
                    conexion.cerrarconexion();
                    if (fa_disponible == fa.id_facilitador)//si el id que retorna es el del facilitador seleccionado: este ya está ocupado para esa fecha
                    {
                        errorProviderManual.SetError(cmbxFa, "El facilitador está ocupado en esta fecha : " + dtpFechaCurso.ToString() + ".");
                        cmbxFa.SelectedIndex = -1;
                        fa.id_facilitador = 0;
                        cmbxFa.Focus();
                    }
                    else
                    {
                        errorProviderManual.SetError(cmbxFa, "");
                        if (conexion.abrirconexion() == true)
                        {
                            int fa_disponibleDia2 = Clases.Facilitadores.FacilitadorDisponibleDia2(conexion.conexion, time.fechaDos_curso);
                            conexion.cerrarconexion();
                            if (fa_disponibleDia2 == fa.id_facilitador)//si el id que retorna es el del facilitador seleccionado: este ya está ocupado para esa fecha
                            {
                                errorProviderManual.SetError(cmbxFa, "El facilitador está ocupado en esta fecha : " + dtpSegundaFecha.ToString() + ".");
                                cmbxFa.SelectedIndex = -1;
                                fa.id_facilitador = 0;
                                cmbxFa.Focus();
                            }
                            else
                            {
                                errorProviderManual.SetError(cmbxFa, "");
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
            if ((formacion.duracion == "16") || (formacion.duracion == "8" && formacion.bloque_curso == "2"))
            {
                //validar ambas fechas
                if (conexion.abrirconexion() == true)
                {
                    int fa_disponible = Clases.Facilitadores.FacilitadorDisponibleDia1(conexion.conexion, time.fecha_curso);
                    conexion.cerrarconexion();
                    if (fa_disponible == Cofa.id_facilitador)//si el id que retorna es el del facilitador seleccionado: este ya está ocupado para esa fecha
                    {
                        errorProviderManual.SetError(cmbxCoFa, "El Co-facilitador está ocupado en esta fecha : " + dtpFechaCurso.ToString() + ".");
                        cmbxCoFa.SelectedIndex = -1;
                        Cofa.id_facilitador = 0;
                        cmbxCoFa.Focus();
                    }
                    else
                    {
                        errorProviderManual.SetError(cmbxCoFa, "");
                        if (conexion.abrirconexion() == true)
                        {
                            int fa_disponibleDia2 = Clases.Facilitadores.FacilitadorDisponibleDia2(conexion.conexion, time.fechaDos_curso);
                            conexion.cerrarconexion();
                            if (fa_disponibleDia2 == Cofa.id_facilitador)//si el id que retorna es el del facilitador seleccionado: este ya está ocupado para esa fecha
                            {
                                errorProviderManual.SetError(cmbxCoFa, "El Co-facilitador está ocupado en la fecha : " + dtpSegundaFecha.ToString() + ".");
                                cmbxCoFa.SelectedIndex = -1;
                                Cofa.id_facilitador = 0;
                                cmbxCoFa.Focus();
                            }
                            else
                            {
                                errorProviderManual.SetError(cmbxCoFa, "");
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
                if ( (formacion.duracion == "16"))
                {                   
                    dtpSegundaFecha.Focus();
                    gpbFacilitador.Enabled = true;
                }
                


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
                String id_curso="";
                MySqlDataReader obtener_id_curso = Conexion.ConsultarBD("SELECT id_curso_ince FROM cursos_inces WHERE nombre_curso_ince = '"+cmbxCursoInce.Text+"'");
                if (obtener_id_curso.Read())
                {
                    id_curso = obtener_id_curso["id_curso_ince"].ToString();
                }
                obtener_id_curso.Close();

                MySqlDataReader obtener_id_de_facilitadores = Conexion.ConsultarBD("SELECT id_fa_INCE FROM inces_tiene_facilitadores WHERE id_curso_INCE ='" + id_curso+"'");
                List<string> lista_facilitadores = new List<string>();

                while (obtener_id_de_facilitadores.Read())
                {
                    lista_facilitadores.Add(obtener_id_de_facilitadores["id_fa_INCE"].ToString());                   
                }
                obtener_id_de_facilitadores.Close();
                cmbxFa.Items.Clear();
                foreach (string id_facilitador in lista_facilitadores)
                {
                    MessageBox.Show("id de facilitador:" + id_facilitador);
                    MySqlDataReader obtener_facilitador = Conexion.ConsultarBD("SELECT * FROM facilitadores WHERE id_fa='" + id_facilitador + "'");
                    if (obtener_facilitador.Read())
                    {
                        cmbxFa.Items.Add(obtener_facilitador["nombre_fa"]);
                        obtener_facilitador.Close();
                    }
                    
                    
                }

            }
        }

        private void pnlNivel_basico_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cmbxCursoInce_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            btnRutaContenido.Enabled = true;
            btnRutaPresentacion.Enabled = true;
            btnRutaBitacora.Enabled = true;
            btnRutaManual.Enabled = true;
            
        }

        private void btnGuardar_Click(object sender, EventArgs e)
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
        }

        private void GuardarBasico()
        {
            if (cmbxCursoInce.SelectedIndex == -1)
            {
                errorProviderNombreF.SetError(cmbxCursoInce, "Debe proporcionar el nombre la formación.");
                cmbxCursoInce.Focus();
            }
            else
            {
                errorProviderNombreF.SetError(cmbxCursoInce, "");
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
                                if (manual == "")
                                {
                                    errorProviderManual.SetError(btnRutaManual, "Debe seleccionar un manual para la formación.");

                                }
                                else // si el manual existe
                                {
                                    //para evitar que mysql borre los "\" se sustituyen por "/" que funcionan igual
                                    manual = manual.Replace("\\", "/");
                                    bitacora = manual.Replace("\\", "/");
                                    presentacion = manual.Replace("\\", "/");
                                    contenido = manual.Replace("\\", "/");

                                    //se valida si el paquete ya existe
                                    int paquete_existe = 0;
                                    MySqlDataReader BuscarPaquete = Conexion.ConsultarBD("SELECT id_pinstruccional FROM p_instruccional WHERE  p_manual='" + manual + "' AND p_contenido='" + contenido + "'");
                                    if (BuscarPaquete.Read())
                                    {
                                        paquete_existe = int.Parse(BuscarPaquete["id_pinstruccional"].ToString());

                                    }
                                    BuscarPaquete.Close();
                                    if (paquete_existe == 0)
                                    {

                                        errorProviderManual.SetError(btnRutaManual, "");

                                        // si se pasan todas las validaciones de la primera etapa, se guarda el paquete instruccional seleccionado
                                       
                                        MySqlDataReader GuardarPaqueteInstruccional = Conexion.ConsultarBD("INSERT INTO p_instruccional (p_bitacora, p_manual, p_contenido, p_presentacion) VALUES ('" + bitacora + "', '" + manual + "', '" + contenido + "', '" + presentacion + "')");
                                        GuardarPaqueteInstruccional.Close();

                                        int id_paq = 0;
                                        //se busca el id del paquete guardado
                                        MySqlDataReader BuscarIdPaquete = Conexion.ConsultarBD("SELECT id_pinstruccional FROM p_instruccional WHERE  p_manual='" + manual + "' AND p_contenido='" + contenido + "'");
                                        if (BuscarIdPaquete.Read())
                                        {
                                            id_paq = int.Parse(BuscarIdPaquete["id_pinstruccional"].ToString());

                                        }
                                        BuscarIdPaquete.Close();
                                        // 
                                        if (id_paq != 0)
                                        {
                                            String FechaCreacion = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                                            //Se crea la formacion
                                            MySqlDataReader CrearCurso = Conexion.ConsultarBD("INSERT INTO cursos (estatus_curso, tipo_curso, duracion_curso, nombre_curso, fecha_creacion ,id_usuario1, id_p_inst, bloque_curso, solicitud_curso, etapa_curso) VALUES ('En curso', 'INCES', '" + 16 + "', '" + cmbxCursoInce.Text + "', '" + FechaCreacion + "' ,'" + Usuario_logeado.id_usuario + "','" + id_paq + "' ,'" + cmbxBloques.Text + "' ,'" + cmbxSolicitadoPor.Text + "',  '1' )");
                                            CrearCurso.Close();
                                            guardar = true;
                                            MessageBox.Show("La formación se ha agregado correctamente.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.None);

                                        }

                                    } else
                                    {

                                        VerPaqueteInst(paquete_existe);

                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void GuardarIntermedio()
        {

        }

        private void GuardarAvanzado()
        {

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
                        Clases.Paquete_instruccional.tipo_curso = "INCES";
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

        private void cmbxCursoInce_Validating(object sender, CancelEventArgs e)
        {
            if (cmbxCursoInce.SelectedIndex == -1)
            {
                errorProviderNombreF.SetError(cmbxCursoInce, "Debe proporcionar el nombre la formación.");
                cmbxCursoInce.Focus();
            }
        }

        private void cmbxSolicitadoPor_Validating(object sender, CancelEventArgs e)
        {
            if (cmbxSolicitadoPor.SelectedIndex == -1)
            {
                errorProviderSolicitado.SetError(cmbxSolicitadoPor, "Debe proporcionar el nombre de quien solicita.");
                cmbxSolicitadoPor.Focus();
            }
        }

        private void cmbxDuracionFormacion_Validating(object sender, CancelEventArgs e)
        {
            if (cmbxDuracionFormacion.SelectedIndex == -1)
            {
                errorProviderDuracionF.SetError(cmbxDuracionFormacion, "Debe proporcionar la duración de la formación.");
                cmbxDuracionFormacion.Focus();
            }
        }

        private void cmbxBloques_Validating(object sender, CancelEventArgs e)
        {
            if (cmbxBloques.SelectedIndex == -1)
            {
                errorProviderBloque.SetError(cmbxBloques, "Debe proporcionar los bloques de la formación.");
                cmbxBloques.Focus();
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

        private void deshabiltarControlesBasico()
        {
            //para el boton pausar en el (nivel_basico)
            cmbxCursoInce.Enabled = false;
            cmbxSolicitadoPor.Enabled = false;
            cmbxDuracionFormacion.Enabled = false;
            cmbxBloques.Enabled = false;
            btnRutaContenido.Enabled = false;
            btnRutaPresentacion.Enabled = false;
            btnRutaManual.Enabled = false;
            btnRutaBitacora.Enabled = false;
        }

        private void btnRetomar_Click(object sender, EventArgs e)
        {

        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            vaciarFormacion();

        }

        private void vaciarFormacion()
        {
            cmbxCursoInce.SelectedIndex = -1;
            cmbxSolicitadoPor.SelectedIndex = -1;
            cmbxDuracionFormacion.SelectedIndex = -1;
            cmbxBloques.SelectedIndex = -1;
            btnRutaContenido.Enabled = false;
            btnRutaPresentacion.Enabled = false;
            btnRutaManual.Enabled = false;
            btnRutaBitacora.Enabled = false;
            bitacora = "";
            manual = "";
            contenido = "";
            presentacion = "";   

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
                dtpSegundaFecha.Enabled = true;
                dtpSegundaFecha.Focus();
                
                


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
    }
}
