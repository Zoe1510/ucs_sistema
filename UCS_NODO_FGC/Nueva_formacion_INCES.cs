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
        
        bool ExisteFormacion;
        string presentacion = "";
        string contenido = "";
        string manual = "";
        string bitacora = "";
        
        DateTime FechaCreacion, inicioE2;
        public Nueva_formacion_INCES()
        {

            InitializeComponent();
        }

        public class ComboboxItem
        {
            public string Text { get; set; }
            public object Value { get; set; }

            public override string ToString()
            {
                return Text;
            }

        }



        private void Nueva_formacion_INCES_Load(object sender, EventArgs e)
        {
            if (Clases.Formaciones.creacion == true)//si viene referenciado del boton de la pagina principal
            {//------------------------------------------todo hay que hacerlo aquí(un nuevo ingreso)
                this.Location = new Point(-5, 0);

                FechaCreacion = DateTime.Now;

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
            cmbxFa.Enabled = false;
            gpbDatosFa.Enabled = false;
            chkbCoFacilitador.Enabled = false;
            gpbCoFa.Enabled = false;
            gpbDatosCoFa.Enabled = false;
            button1.Enabled = false;
            button2.Enabled = false;



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
                                    bitacora = bitacora.Replace("\\", "/");
                                    presentacion = presentacion.Replace("\\", "/");
                                    contenido = contenido.Replace("\\", "/");

                                    //se valida si el paquete ya existe
                                    int paquete_existe = 0;

                                    if (formacion.pq_inst == 0)
                                    {
                                        MySqlDataReader BuscarPaquete = Conexion.ConsultarBD("SELECT id_pinstruccional FROM p_instruccional WHERE  p_manual='" + manual + "' AND p_contenido='" + contenido + "'");
                                        if (BuscarPaquete.Read())
                                        {
                                            paquete_existe = int.Parse(BuscarPaquete["id_pinstruccional"].ToString());

                                        }
                                        BuscarPaquete.Close();
                                    }


                                    if (paquete_existe == 0)
                                    {

                                        errorProviderManual.SetError(btnRutaManual, "");

                                        if (formacion.pq_inst == 0)
                                        {
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
                                                DateTime FechaFinal = DateTime.Now;
                                                String TiempoEtapa = Convert.ToString(FechaFinal - FechaCreacion);
                                                //Se crea la formacion con un paquete nuevo
                                                MySqlDataReader CrearCurso = Conexion.ConsultarBD("INSERT INTO cursos (estatus_curso, tipo_curso, duracion_curso, nombre_curso, fecha_creacion ,id_usuario1, id_p_inst, bloque_curso, solicitud_curso, etapa_curso,  duracionE1) VALUES ('En curso', 'INCES', '" + 16 + "', '" + cmbxCursoInce.Text + "', '" + FechaCreacion + "' ,'" + Usuario_logeado.id_usuario + "','" + id_paq + "' ,'" + cmbxBloques.Text + "' ,'" + cmbxSolicitadoPor.Text + "',  '1','" + TiempoEtapa + "')");
                                                CrearCurso.Close();
                                                guardar = true;
                                                MessageBox.Show("La formación se ha agregado correctamente.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.None);

                                                MySqlDataReader IdCurso = Conexion.ConsultarBD("SELECT id_cursos FROM cursos WHERE nombre_curso = '" + cmbxCursoInce.Text + "' AND solicitud_curso='" + cmbxSolicitadoPor.Text + "' AND estatus_curso='En curso'");
                                                int id_curso = 0;
                                                if (IdCurso.Read())
                                                {
                                                    id_curso = int.Parse(IdCurso["id_cursos"].ToString());
                                                }
                                                IdCurso.Close();


                                                MySqlDataReader usuarios_gestionan_cursos = Conexion.ConsultarBD("INSERT INTO user_gestionan_cursos (cursos_id_cursos, usuarios_id_user, fecha_mod_inicio, fecha_mod_final) VALUES ('" + id_curso + "', '" + Usuario_logeado.id_usuario + "', '" + FechaCreacion + "' , '" + FechaFinal + "')");
                                                usuarios_gestionan_cursos.Close();

                                                MySqlDataReader IdCliente = Conexion.ConsultarBD("SELECT id_clientes FROM clientes WHERE nombre_empresa='" + cmbxSolicitadoPor.Text + "'");
                                                int id_cliente = 0;
                                                if (IdCliente.Read())
                                                {
                                                    id_cliente = int.Parse(IdCliente["id_clientes"].ToString());
                                                }

                                                IdCliente.Close();

                                                MySqlDataReader clientes_solicitan_cursos = Conexion.ConsultarBD("INSERT INTO clientes_solicitan_cursos (id_cliente1, id_curso1) VALUES ('" + id_cliente + "', '" + id_curso + "')");
                                                clientes_solicitan_cursos.Close();

                                            }
                                        }
                                        else
                                        {
                                            DateTime FechaFinal = DateTime.Now;
                                            String TiempoEtapa = Convert.ToString(FechaFinal - FechaCreacion);
                                            //Se crea la formacion con un paquete ya existente
                                            MySqlDataReader CrearCurso = Conexion.ConsultarBD("INSERT INTO cursos (estatus_curso, tipo_curso, duracion_curso, nombre_curso, fecha_creacion ,id_usuario1, id_p_inst, bloque_curso, solicitud_curso, etapa_curso, duracionE1) VALUES ('En curso', 'INCES', '" + 16 + "', '" + cmbxCursoInce.Text + "', '" + FechaCreacion + "' ,'" + Usuario_logeado.id_usuario + "','" + formacion.pq_inst + "' ,'" + cmbxBloques.Text + "' ,'" + cmbxSolicitadoPor.Text + "',  '1', '" + TiempoEtapa + "')");
                                            CrearCurso.Close();

                                            MessageBox.Show("La formación se ha agregado correctamente.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.None);
                                            MySqlDataReader IdCurso = Conexion.ConsultarBD("SELECT id_cursos FROM cursos WHERE nombre_curso = '" + cmbxCursoInce.Text + "' AND solicitud_curso='" + cmbxSolicitadoPor.Text + "' AND estatus_curso='En curso'");
                                            int id_curso = 0;
                                            if (IdCurso.Read())
                                            {
                                                id_curso = int.Parse(IdCurso["id_cursos"].ToString());
                                            }
                                            IdCurso.Close();


                                            MySqlDataReader usuarios_gestionan_cursos = Conexion.ConsultarBD("INSERT INTO user_gestionan_cursos (cursos_id_cursos, usuarios_id_user, fecha_mod_inicio, fecha_mod_final) VALUES ('" + id_curso + "', '" + Usuario_logeado.id_usuario + "', '" + FechaCreacion + "', '" + FechaFinal + "')");
                                            usuarios_gestionan_cursos.Close();

                                            MySqlDataReader IdCliente = Conexion.ConsultarBD("SELECT id_clientes FROM clientes WHERE nombre_empresa='" + cmbxSolicitadoPor.Text + "'");
                                            int id_cliente = 0;
                                            if (IdCliente.Read())
                                            {
                                                id_cliente = int.Parse(IdCliente["id_clientes"].ToString());
                                            }

                                            IdCliente.Close();

                                            MySqlDataReader clientes_solicitan_cursos = Conexion.ConsultarBD("INSERT INTO clientes_solicitan_cursos (id_cliente1, id_curso1) VALUES ('" + id_cliente + "', '" + id_curso + "')");
                                            clientes_solicitan_cursos.Close();
                                            guardar = true;

                                        }


                                    }
                                    else
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
            guardar = false;

            if (rdbNoRef.Checked == false && rdbSiRef.Checked == false)
            {
                errorProviderRefrigerio.SetError(gpbRefrigerio, "Debe seleccionar una opción.");
                gpbRefrigerio.Focus();
            }
            else
            {
                errorProviderRefrigerio.SetError(gpbRefrigerio, "");
                if (dtpFechaCurso.Value <= DateTime.Today)
                {
                    errorProviderContenido.SetError(dtpFechaCurso, "La fecha seleccionada es inválida.");
                    dtpFechaCurso.Focus();
                }
                else
                {
                    errorProviderContenido.SetError(dtpFechaCurso, "");
                    if (dtpSegundaFecha.Value <= dtpFechaCurso.Value || dtpSegundaFecha.Value <= DateTime.Today)
                    {
                        errorProviderContenido.SetError(dtpSegundaFecha, "La fecha seleccionada es inválida.");
                        dtpSegundaFecha.Focus();
                    }
                    else
                    {
                        errorProviderContenido.SetError(dtpFechaCurso, "");
                        if (cmbxFa.SelectedIndex == -1)
                        {
                            errorProviderContenido.SetError(cmbxFa, "Debe seleccionar un facilitador.");
                            cmbxFa.Focus();
                        }
                        else
                        {
                            errorProviderContenido.SetError(cmbxFa, "");
                            if (chkbCoFacilitador.Checked == true && cmbxCoFa.SelectedIndex == -1)
                            {
                                errorProviderContenido.SetError(cmbxCoFa, "No ha seleccionado ningún co-facilitador.");
                                cmbxCoFa.Focus();
                            }
                            else
                            {
                                errorProviderContenido.SetError(cmbxCoFa, "");
                                DateTime FinalE2 = DateTime.Now;
                                String duracionE2 = Convert.ToString(FinalE2 - inicioE2);
                                int id_curso = 0;
                                //se obtiene el id del curso
                                MySqlDataReader obtener_id_curso = Conexion.ConsultarBD("SELECT id_cursos FROM cursos WHERE nombre_curso = '" + cmbxCursoInce.Text + "' AND tipo_curso='INCES' AND estatus_curso='En curso'");
                                if (obtener_id_curso.Read())
                                {
                                    id_curso = int.Parse(obtener_id_curso["id_cursos"].ToString());
                                }
                                obtener_id_curso.Close();
                                //se actualiza el curso con los datos obtenidos de la segunda etapa, como las fechas
                                MySqlDataReader ActualizarCursoSegundaEtapa = Conexion.ConsultarBD("UPDATE cursos SET etapa_curso ='2', fecha_uno='" + dtpFechaCurso.Value.ToString("yyyy-MM-dd HH:mm:ss") + "', fecha_dos='" + dtpSegundaFecha.Value.ToString("yyyy-MM-dd HH:mm:ss") + "', duracionE2='"+duracionE2+"' WHERE id_cursos='" + id_curso + "'");
                                ActualizarCursoSegundaEtapa.Close();

                                // si el checkbox esta seleccionado es que tiene co-facilitador
                                if (chkbCoFacilitador.Checked == true && cmbxCoFa.SelectedIndex != -1)
                                {
                                    MySqlDataReader FacilitadorCurso = Conexion.ConsultarBD("INSERT INTO cursos_tienen_fa (cursos_id_cursos, facilitadores_id_fa, ctf_id_cofa, ctf_fecha, ctf_fecha2) VALUES ('" + id_curso + "', '" + (cmbxFa.SelectedItem as ComboboxItem).Value + "', '" + (cmbxCoFa.SelectedItem as ComboboxItem).Value + "', '" + dtpFechaCurso.Value.ToString("yyyy-MM-dd HH:mm:ss") + "', '" + dtpSegundaFecha.Value.ToString("yyyy-MM-dd HH:mm:ss") + "')");
                                    FacilitadorCurso.Close();
                                    guardar = true;
                                }
                                else // sino, solo guarda al facilitador
                                {
                                    MySqlDataReader FacilitadorCurso = Conexion.ConsultarBD("INSERT INTO cursos_tienen_fa (cursos_id_cursos, facilitadores_id_fa, ctf_fecha) VALUES ('" + id_curso + "', '" + (cmbxFa.SelectedItem as ComboboxItem).Value + "', '" + dtpFechaCurso.Value.ToString("yyyy-MM-dd HH:mm:ss") + "')");
                                    FacilitadorCurso.Close();
                                    guardar = true;
                                }



                            }

                        }
                    }
                }

            }


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

            //para el boton pausar en el (nivel_intermedio)
            gpbRefrigerio.Enabled = false;
            dtpFechaCurso.Enabled = false;
            dtpSegundaFecha.Enabled = false;
            gpbFacilitador.Enabled = false;
            chkbCoFacilitador.Enabled = false;
            gpbCoFa.Enabled = false;

            //

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
            formacion.pq_inst = 0;

        }
       



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
                                
            } else if (pnlNivel_intermedio.Visible == true)
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

        private void btnRetomar_Click(object sender, EventArgs e)
        {
            //cuando retomar = habilitar los controles
            if (pnlNivel_basico.Visible == true)
            {
                cmbxCursoInce.Enabled = true;
                cmbxSolicitadoPor.Enabled = true;
                cmbxDuracionFormacion.Enabled = true;
                cmbxBloques.Enabled = true;
                btnRutaContenido.Enabled = true;
                btnRutaPresentacion.Enabled = true;
                btnRutaManual.Enabled = true;
                btnRutaBitacora.Enabled = true;
                dtpFechaCurso.Enabled = true;
                dtpSegundaFecha.Enabled = true;
                //Y lo deja igual que el load
            }
            else if (pnlNivel_intermedio.Visible == true)
            {
                gpbRefrigerio.Enabled = true;
                dtpFechaCurso.Enabled = true;
                dtpSegundaFecha.Enabled = true;


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
            else if (pnlNivel_intermedio.Visible == true)
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

                    //para saber si las opciones de refrigerio estarán disponibles o no
                    if (rdbNoRef.Checked == true)
                    {
                        gpbSeleccionRef.Enabled = false;
                    }
                    else
                    {
                        gpbSeleccionRef.Enabled = true;
                        //se llena el combobox con los refrigerios
                        MySqlDataReader obtener_refrigerios = Conexion.ConsultarBD("SELECT * FROM refrigerios");
                        while (obtener_refrigerios.Read())
                        {

                            ComboboxItem item = new ComboboxItem();
                            item.Text = obtener_refrigerios["ref_nombre"].ToString();
                            item.Value = int.Parse(obtener_refrigerios["id_ref"].ToString());
                            cmbxTipoRefrigerio.Items.Add(item);

                        }
                        obtener_refrigerios.Close();

                    }
                }

            }
            else if (pnlNivel_avanzado.Visible == true)
            {

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



        /*-------------------- Controles del Nivel_basico ------------------------*/


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

        private void cmbxCursoInce_SelectedIndexChanged(object sender, EventArgs e)
        {




            //validar si existe un curso de cualquier estatus en la base de datos con el mismo nombre
            //para recuperar el paquete instruccional o dejar que el usuario lo escoja
            try
            {
                conexion.cerrarconexion();
                if (cmbxCursoInce.Text != "")//siempre y cuando el txt contenga algo, se valida
                {
                    if (conexion.abrirconexion() == true)
                    {
                        formacion.estatus = "En curso"; //predeterminado en esta etapa
                        formacion.tipo_formacion = "INCES"; //predeterminado para este form
                        formacion.nombre_formacion = cmbxCursoInce.Text;
                        conexion.cerrarconexion();
                        if (conexion.abrirconexion() == true)
                        {
                            int existeEjecucion = Clases.Formaciones.CursoEjecucionExiste(conexion.conexion, formacion);
                            conexion.cerrarconexion();

                            if (existeEjecucion != 0)
                            {
                                errorProviderNombreF.SetError(cmbxCursoInce, "Ya existe una formacion con este nombre en estado 'En curso'");
                                cmbxCursoInce.Focus();
                                vaciarFormacion();
                                ExisteFormacion = false;//significa que no existe, el usuario podrá establecer su propio paquete instruccional
                                btnRutaContenido.Enabled = true;
                                btnRutaPresentacion.Enabled = true;
                                btnRutaBitacora.Enabled = true;
                                btnRutaManual.Enabled = true;
                            }
                            else
                            {
                                errorProviderNombreF.SetError(cmbxCursoInce, "");
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
                                            btnRutaContenido.Enabled = true;
                                            btnRutaPresentacion.Enabled = true;
                                            btnRutaBitacora.Enabled = true;
                                            btnRutaManual.Enabled = true;
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




        /*-------------------- Controles del Nivel_intermedio ------------------------*/

        private void cmbxCursoInce_Leave(object sender, EventArgs e)
        {

        }

        private void cmbxFa_SelectedIndexChanged(object sender, EventArgs e)
        {


        }

        private void cmbxCoFa_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbxCoFa.Text != "")
            {
                //ComboboxItem item = new ComboboxItem();
                //para llenar el combobox con los co-facilitadores
                MySqlDataReader datos_co_facilitador = Conexion.ConsultarBD("SELECT * FROM facilitadores where id_fa='" + (cmbxCoFa.SelectedItem as ComboboxItem).Value + "'");
                if (datos_co_facilitador.Read())
                {
                    txtTlfnCoFa.Text = datos_co_facilitador["tlfn_fa"].ToString();
                    txtCorreoCoFa.Text = datos_co_facilitador["correo_fa"].ToString();
                }
                datos_co_facilitador.Close();
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

            if (cmbxFa.Text != "")
            {
                ComboboxItem item = new ComboboxItem();

                MySqlDataReader datos_facilitador = Conexion.ConsultarBD("SELECT * FROM facilitadores where id_fa='" + (cmbxFa.SelectedItem as ComboboxItem).Value + "'");
                if (datos_facilitador.Read())
                {
                    //al seleccionar un elemento del combobox, se llenan los datos del co-facilitador
                    txtTlfnFa.Text = datos_facilitador["tlfn_fa"].ToString();
                    txtCorreoFa.Text = datos_facilitador["correo_fa"].ToString();
                }
                datos_facilitador.Close();

                MySqlDataReader IdCurso = Conexion.ConsultarBD("SELECT id_curso_ince FROM cursos_inces WHERE nombre_curso_ince = '" + cmbxCursoInce.Text + "'");
                int id_curso = 0;
                if (IdCurso.Read())
                {
                    id_curso = int.Parse(IdCurso["id_curso_ince"].ToString());
                }
                IdCurso.Close();
               

                //para luego buscar todos los facilitadores relacionados con el curso

                MySqlDataReader obtener_id_de_facilitadores = Conexion.ConsultarBD("SELECT id_fa_INCE FROM inces_tiene_facilitadores WHERE id_curso_INCE ='" + id_curso + "'");
                List<string> lista_facilitadores = new List<string>();

                while (obtener_id_de_facilitadores.Read())
                {
                    //se hace una lista con todos esos facilitadores
                    lista_facilitadores.Add(obtener_id_de_facilitadores["id_fa_INCE"].ToString());
                }
                obtener_id_de_facilitadores.Close();
                cmbxCoFa.Items.Clear();
                foreach (string id_co_facilitador in lista_facilitadores)
                {
                    //se seleccionan los datos de los facilitadores de la lista
                    MySqlDataReader obtener_co_facilitador = Conexion.ConsultarBD("SELECT * FROM facilitadores WHERE id_fa='" + id_co_facilitador + "'");
                    if (obtener_co_facilitador.Read())
                    {
                        //si el id del facilitador es diferente al primero, pasa a ser un co-facilitador
                        if (obtener_co_facilitador["id_fa"].ToString() != (cmbxFa.SelectedItem as ComboboxItem).Value.ToString())
                        {

                            item.Text = obtener_co_facilitador["nombre_fa"].ToString() + " " + obtener_co_facilitador["apellido_fa"].ToString();
                            item.Value = int.Parse(obtener_co_facilitador["id_fa"].ToString());
                            cmbxCoFa.Items.Add(item);
                            obtener_co_facilitador.Close();
                            chkbCoFacilitador.Enabled = true;

                        }

                    }


                }

            } else
            {
                MessageBox.Show("Hola");
            }

        }

        private void cmbxFa_Validating(object sender, CancelEventArgs e)
        {
            /*if ((formacion.duracion == "16"))
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
            } */

        }

        private void cmbxCoFa_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Cofa.id_facilitador = Convert.ToInt32(cmbxCoFa.SelectedValue);
        }

        private void cmbxCoFa_Validating(object sender, CancelEventArgs e)
        {
            if ((formacion.duracion == "16"))
            {
                ////validar ambas fechas
                //if (conexion.abrirconexion() == true)
                //{
                //    int fa_disponible = Clases.Facilitadores.FacilitadorDisponibleDia1(conexion.conexion, time.fecha_curso);
                //    conexion.cerrarconexion();
                //    if (fa_disponible == Cofa.id_facilitador)//si el id que retorna es el del facilitador seleccionado: este ya está ocupado para esa fecha
                //    {
                //        errorProviderManual.SetError(cmbxCoFa, "El Co-facilitador está ocupado en esta fecha : " + dtpFechaCurso.ToString() + ".");
                //        cmbxCoFa.SelectedIndex = -1;
                //        Cofa.id_facilitador = 0;
                //        cmbxCoFa.Focus();
                //    }
                //    else
                //    {
                //        errorProviderManual.SetError(cmbxCoFa, "");
                //        if (conexion.abrirconexion() == true)
                //        {
                //            int fa_disponibleDia2 = Clases.Facilitadores.FacilitadorDisponibleDia2(conexion.conexion, time.fechaDos_curso);
                //            conexion.cerrarconexion();
                //            if (fa_disponibleDia2 == Cofa.id_facilitador)//si el id que retorna es el del facilitador seleccionado: este ya está ocupado para esa fecha
                //            {
                //                errorProviderManual.SetError(cmbxCoFa, "El Co-facilitador está ocupado en la fecha : " + dtpSegundaFecha.ToString() + ".");
                //                cmbxCoFa.SelectedIndex = -1;
                //                Cofa.id_facilitador = 0;
                //                cmbxCoFa.Focus();
                //            }
                //            else
                //            {
                //                errorProviderManual.SetError(cmbxCoFa, "");
                //                //si todo bien, cargar los datos en el gpbDatosFa
                //                gpbDatosCoFa.Enabled = true;

                //                if (conexion.abrirconexion() == true)
                //                {
                //                    faDatos = Clases.Facilitadores.SeleccionarFaPorID(conexion.conexion, Cofa.id_facilitador);

                //                    conexion.cerrarconexion();
                //                    txtTlfnCoFa.Text = faDatos.tlfn_facilitador;
                //                    txtCorreoCoFa.Text = faDatos.correo_facilitador;
                //                }
                //            }
                //        }
                //    }
                //}
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
                dtpSegundaFecha.Focus();
             
            }
        }
        private void dtpSegundaFecha_Validating(object sender, CancelEventArgs e)
        {
           
        }

        private void pnlNivel_basico_Paint(object sender, PaintEventArgs e)
        {

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
                cmbxFa.Enabled = true;
                cmbxFa.Focus();

                MySqlDataReader IdCurso = Conexion.ConsultarBD("SELECT id_curso_ince FROM cursos_inces WHERE nombre_curso_ince = '" + cmbxCursoInce.Text + "'");
                int id_curso = 0;
                if (IdCurso.Read())
                {
                    id_curso = int.Parse(IdCurso["id_curso_ince"].ToString());
                }
                IdCurso.Close();

                MySqlDataReader obtener_id_de_facilitadores = Conexion.ConsultarBD("SELECT id_fa_INCE FROM inces_tiene_facilitadores WHERE id_curso_INCE ='" + id_curso + "'");
                List<string> lista_facilitadores = new List<string>();

                while (obtener_id_de_facilitadores.Read())
                {
                    lista_facilitadores.Add(obtener_id_de_facilitadores["id_fa_INCE"].ToString());
                }
                obtener_id_de_facilitadores.Close();
                cmbxFa.Items.Clear();
                foreach (string id_facilitador in lista_facilitadores)
                {

                    MySqlDataReader obtener_facilitador = Conexion.ConsultarBD("SELECT * FROM facilitadores WHERE id_fa='" + id_facilitador + "'");
                    if (obtener_facilitador.Read())
                    {
                        ComboboxItem item = new ComboboxItem();
                        item.Text = obtener_facilitador["nombre_fa"].ToString() + " " + obtener_facilitador["apellido_fa"].ToString();
                        item.Value = int.Parse(obtener_facilitador["id_fa"].ToString());
                        cmbxFa.Items.Add(item);

                    }
                    obtener_facilitador.Close();
                }
                gpbFacilitador.Enabled = true;
                cmbxFa.Enabled = true;
                cmbxFa.Focus();

                

                

            }
        }




        /*-------------------- Controles del Nivel_Avanzado ------------------------*/
        private void rdbSiIgual_CheckedChanged(object sender, EventArgs e)
        {
            if (cmbxHorarios.SelectedIndex == -1)
            {
                errorProviderFecha.SetError(cmbxHorarios, "Debe seleccionar un horario primero.");
                cmbxHorarios.Focus();
                rdbSiIgual.Checked = false;
            }
             else if (cmbxHorarios.SelectedIndex == 0)
            {
                cmbxSegundoHorario.Text = "08:00 am - 05:00pm";
                errorProviderFecha.SetError(cmbxHorarios, "");
            }
             else if (cmbxHorarios.SelectedIndex == 1)
            {
                cmbxSegundoHorario.Text = "09:00 am - 06:00pm";
                errorProviderFecha.SetError(cmbxHorarios, "");
            }
            
        }

        private void rdbNoIgual_CheckedChanged(object sender, EventArgs e)
        {
            if (cmbxHorarios.SelectedIndex == -1)
            {
                errorProviderFecha.SetError(cmbxHorarios, "Debe seleccionar un horario primero.");
                cmbxHorarios.Focus();
                rdbNoIgual.Checked = false;
            }
            else if (cmbxHorarios.SelectedIndex == 0)
            {
                cmbxSegundoHorario.Text = "09:00 am - 06:00pm";
                errorProviderFecha.SetError(cmbxHorarios, "");
            }
            else if (cmbxHorarios.SelectedIndex == 1)
            {
                cmbxSegundoHorario.Text = "08:00 am - 05:00pm";
                errorProviderFecha.SetError(cmbxHorarios, "");
            }
        }

        private void rdbMantenerRef_CheckedChanged(object sender, EventArgs e)
        { 
            if (cmbxTipoRefrigerio.SelectedIndex == -1)
            {
                errorProviderFecha.SetError(cmbxTipoRefrigerio, "Debe seleccionar un tipo de refrigerio.");
                cmbxTipoRefrigerio.Focus();
                rdbMantenerRef.Checked = false;
                cmbxSegundoRefrigerio.Enabled = false;
            }
            else
            {
                //como se quiere seleccionar el mismo refrigerio, se desactiva el combobox y automaticamente se selecciona
                errorProviderFecha.SetError(cmbxTipoRefrigerio, "");
                cmbxSegundoRefrigerio.Enabled = false;
                cmbxSegundoRefrigerio.Text = cmbxTipoRefrigerio.Text;
            }
        }

        private void rdbNoMantenerRef_CheckedChanged(object sender, EventArgs e)
        {
            
            if (cmbxTipoRefrigerio.SelectedIndex == -1)
            {
                errorProviderFecha.SetError(cmbxTipoRefrigerio, "Debe seleccionar un tipo de refrigerio");
                cmbxTipoRefrigerio.Focus();
                rdbNoMantenerRef.Checked = false;
                cmbxSegundoRefrigerio.Enabled = false;
            }
            else
            {
                // se habilita el combo box
                cmbxSegundoRefrigerio.Enabled = true;

                //se piden todos los refrigerios
                MySqlDataReader obtener_refrigerios = Conexion.ConsultarBD("SELECT * FROM refrigerios");
                while (obtener_refrigerios.Read())
                {
                    //se llena el combobox de segundo  refrigerio omitiendo el que esta en el primer refrigerio
                    if (obtener_refrigerios["ref_nombre"].ToString() != cmbxTipoRefrigerio.Text)
                    {
                        ComboboxItem item = new ComboboxItem();
                        item.Text = obtener_refrigerios["ref_nombre"].ToString();
                        item.Value = int.Parse(obtener_refrigerios["id_ref"].ToString());
                        cmbxTipoRefrigerio.Items.Add(item);
                    }
                }
            }
            
        }

        private void cmbxTipoRefrigerio_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmbxHorarios_Validating(object sender, CancelEventArgs e)
        { 
            rdbSiIgual.Checked = false;
            rdbNoIgual.Checked = false;

            if (cmbxHorarios.SelectedIndex == -1)
            {
                errorProviderFecha.SetError(cmbxHorarios, "Debe seleccionar una fecha primero.");
                
                rdbSiIgual.Enabled = false;
                rdbNoIgual.Enabled = false;

            } else
            {
                errorProviderFecha.SetError(cmbxHorarios, "");
                rdbSiIgual.Enabled = true;
                rdbNoIgual.Enabled = true;
            }
        }

        private void cmbxHorarios_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtAula_TextChanged(object sender, EventArgs e)
        {

        }

        private void rdbMantenerAula_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
