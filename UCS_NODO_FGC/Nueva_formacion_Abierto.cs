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
        List<int> lista_id = new List<int>();
        Curso_AFI AFI = new Curso_AFI();
        List<Facilitador_todos> lista = new List<Facilitador_todos>();
        List<string> nombre_publicidad = new List<string>();
        List<string> lista_insumo = new List<string>();

        bool guardar = false;
        string duracion = "";
        bool ExisteFormacion;
        string presentacion = "";
        string contenido="";
        string bloques = "";
        int id_refrigerio, id_refrigerio2, id_horario, id_horario2;

        Facilitador_todos fcombo = new Facilitador_todos();
        DateTime fecha_creacion, fecha_modifinal, inicioE2, inicioE3;
       
        
        public Nueva_formacion_Abierto()
        {
            InitializeComponent();
        }
        
        //Load
        private void Nueva_formacion_Load(object sender, EventArgs e)
        {
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

            llenarcomboRefrigerio();

            //lo de arriba, aplica para modificacion o creacion 

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
                dtpFechaCurso.Value = DateTime.Today;
                dtpSegundaFecha.Value = DateTime.Today;

                btnVerPresentacion.Enabled = false;
                btnVerContenido.Enabled = false;
                btnRutaContenido.Enabled = false;
                btnRutaPresentacion.Enabled = false;

                //como estarán los botones inicialmente para cada nivel
                Load_Sig_Re();

                //btnSiguienteEtapa.Enabled = true; //Solo para tomar ss


                //controles del nivel intermedio
                Controles_nivel_intermedio_EstatusInicial();
                

                
                //cargar los facilitadores del nivel_intermedio
                llenarcomboFacilitador();

                
            }else //si viene referenciado desde el modulo ver formaciones, donde se cargaran los datos ingresados previamente para su modificacion
            {

                dtpFechaCurso.Value = DateTime.Today;
                dtpSegundaFecha.Value = DateTime.Today;

                LabelCabecera.Text = "" + Cursos.nombre_formacion13 + ": Información básica";
                LabelCabecera.Location = new Point(115, 31);

                lblEtapaSiguiente.Text = "Nivel Intermedio";
                lblEtapaSiguiente.Location = new Point(17, 529);

                lblEtapafinal.Text = "Nivel Avanzado";
                lblEtapafinal.Location = new Point(22, 570);

                //botones laterales
                btnGuardar.Enabled = false;
                btnPausar.Enabled = false;
                btnLimpiar.Enabled = false;
                btnModificar.Enabled = false;
                btnRetomar.Enabled = true;
                btnSiguienteEtapa.Enabled = true;

                if (Cursos.etapa_formacion13 == 1)
                {
                    CargarDatosEtapaUno();
                    //para cargar el cmbxfa
                    AFI.id_AFI = 0;
                    formacion.nombre_formacion = txtNombreFormacion.Text;
                    //verificar si el curso etá registrado en cursos_afi para seleccionar a los facilitadores que pueden realizar ese curso.
                    MySqlDataReader id = Conexion.ConsultarBD("SELECT id_curso_afi from cursos_afi where nombre_curso_afi='" + formacion.nombre_formacion + "'");
                    if (id.Read())
                    {
                        AFI.id_AFI = Convert.ToInt32(id["id_curso_afi"]);
                    }
                    id.Close();
                    if (AFI.id_AFI != 0) //si el curso está registrado: se seleccionan los id de facilitadores y se quitan del comboboxFa los que no estén asignados a esa formación.
                    {

                        MySqlDataReader cof = Conexion.ConsultarBD("select nombre_fa, apellido_fa from facilitadores fa inner join afi_tiene_facilitadores atf on atf.id_fa=fa.id_fa where atf.id_cursos_afi='" + AFI.id_AFI + "'");
                        while (cof.Read())
                        {
                            Facilitador_todos f = new Facilitador_todos();
                            f.nombre_facilitador = cof["nombre_fa"].ToString();
                            f.apellido_facilitador = cof["apellido_fa"].ToString();
                            f.nombreyapellido1 = f.nombre_facilitador + " " + f.apellido_facilitador;

                            cmbxFa.Items.Add(f.nombreyapellido1);

                        }
                        cof.Close();

                    }

                }
                else if (Cursos.etapa_formacion13 == 2)
                {
                    
                    btnCorreoComercializacion.Enabled = true;
                    btnCorreoFacilitador.Enabled = true;
                    btnRetomar.Enabled = false;
                    btnSiguienteEtapa.Enabled = true;

                    CargarDatosEtapaUno();

                    CargarDatosEtapaDos();

                    if (Cursos.duracion_formacion13 == "16 Horas" || (Cursos.duracion_formacion13 == "8 Horas" && Cursos.bloque_curso13 == "1"))
                    {
                        string tipo = "A";
                        llenarComboHorario(tipo);
                    }
                    else if (Cursos.duracion_formacion13 == "4 Horas" || (Cursos.duracion_formacion13 == "8 Horas" && Cursos.bloque_curso13 == "2"))
                    {
                        string tipo = "B";
                        llenarComboHorario(tipo);
                    }


                }
                else if (Cursos.etapa_formacion13 == 3)
                {
                    btnRetomar.Enabled = false;
                    btnSiguienteEtapa.Enabled = true;

                    if ((Cursos.duracion_formacion13 == "16 Horas") || (Cursos.duracion_formacion13 == "8 Horas" && Cursos.bloque_curso13 == "1"))
                    {
                        string tipo = "A";
                        llenarComboHorario(tipo);
                    }
                    else if ((Cursos.duracion_formacion13 == "4 Horas") || (Cursos.duracion_formacion13 == "8 Horas" && Cursos.bloque_curso13 == "2"))
                    {
                        string tipo = "B";
                        llenarComboHorario(tipo);
                    }

                    CargarDatosEtapaTres();

                    if (Cursos.ubicacion_ucs == "Si")
                    {
                        //para arreglos visuales en el form
                        if (Cursos.duracion_formacion13 == "4 Horas")
                        {
                            //aqui estaran contemplados los arreglos (visuales y de contenido) de la siguiente etapa
                            ordenTerceraEtapa();

                            //para la logistica:
                            rdbSiMantenerAula.Enabled = false;
                            rdbNoMantenerAula.Enabled = false;
                            txtSegundaAula.Enabled = false;
                            rdbNoIgualHorario.Enabled = false;
                            rdbSiIgualHorario.Enabled = false;
                            gpbSeleccionRef.Enabled = true;
                        }
                        else if (Cursos.duracion_formacion13 == "8 Horas" && Cursos.bloque_curso13 == "2")
                        {
                            //aqui estaran contemplados los arreglos (visuales y de contenido) de la siguiente etapa
                            ordenTerceraEtapa();
                            gpbHorarioCurso.Height = 169;
                            gpbSeleccionRef.Visible = true;

                            //para la logistica:
                            rdbSiMantenerAula.Enabled = true;
                            rdbNoMantenerAula.Enabled = true;
                            txtSegundaAula.Enabled = false;

                        }
                        else if (Cursos.duracion_formacion13 == "8 Horas")
                        {
                            //para la logistica:
                            rdbSiMantenerAula.Enabled = false;
                            rdbNoMantenerAula.Enabled = false;
                            txtSegundaAula.Enabled = false;

                            rdbSiMantenerAula.Visible = true;
                            rdbNoMantenerAula.Visible = true;

                            rdbNoIgualHorario.Visible = true;
                            rdbSiIgualHorario.Visible = true;
                            rdbNoIgualHorario.Enabled = false;
                            rdbSiIgualHorario.Enabled = false;

                            if (rdbSiRef.Checked == false)
                            {
                                gpbSeleccionRef.Enabled = false;
                                gpbSeleccionRef.Visible = true;
                            }
                            else
                            {
                                gpbSeleccionRef.Enabled = true;
                                gpbSeleccionRef.Visible = true;
                            }

                        }
                        else if (Cursos.duracion_formacion13 == "16 Horas")
                        {
                            //para la logistica:
                            rdbSiMantenerAula.Enabled = true;
                            rdbNoMantenerAula.Enabled = true;
                            txtSegundaAula.Enabled = false;
                            gpbLogistica.Enabled = true;
                            rdbSiMantenerRef.Enabled = true;
                            rdbNoMantenerRef.Enabled = true;

                            if (rdbSiRef.Checked == false)
                            {

                                ordenTerceraEtapa();
                                gpbSeleccionRef.Enabled = false;
                                gpbSeleccionRef.Visible = true;
                                gpbHorarioCurso.Height = 169;
                            }
                            else
                            {
                                ordenTerceraEtapa();
                                gpbHorarioCurso.Location = new Point(33, 47);
                                gpbHorarioCurso.Height = 169;
                                gpbSeleccionRef.Location = new Point(464, 47);
                                gpbSeleccionRef.Height = 169;
                                gpbSeleccionRef.Enabled = true;
                                gpbSeleccionRef.Visible = true;
                                rdbNoMantenerRef.Visible = true;
                                rdbSiMantenerRef.Visible = true;
                                rdbNoMantenerRef.Enabled = true;
                                rdbSiMantenerRef.Enabled = true;
                            }

                        }
                    }
                    else
                    {
                        //para arreglos visuales en el form
                        if (Cursos.duracion_formacion13 == "4 Horas")
                        {
                            gpbLogistica.Enabled = false;
                            gpbSeleccionRef.Enabled = false;
                            //aqui estaran contemplados los arreglos (visuales y de contenido) de la siguiente etapa
                            ordenTerceraEtapa();

                            //para la logistica:
                            rdbSiMantenerAula.Enabled = false;
                            rdbNoMantenerAula.Enabled = false;
                            txtSegundaAula.Enabled = false;
                            rdbNoIgualHorario.Enabled = false;
                            rdbSiIgualHorario.Enabled = false;

                        }
                        else if (Cursos.duracion_formacion13 == "8 Horas" && Cursos.bloque_curso13 == "2")
                        {
                            gpbSeleccionRef.Enabled = false;
                            rdbNoIgualHorario.Enabled = true;
                            rdbSiIgualHorario.Enabled = true;
                            gpbLogistica.Enabled = false;
                            //aqui estaran contemplados los arreglos (visuales y de contenido) de la siguiente etapa
                            ordenTerceraEtapa();
                            gpbHorarioCurso.Height = 169;
                            gpbSeleccionRef.Visible = true;

                            rdbSiMantenerAula.Enabled = true;
                            rdbNoMantenerAula.Enabled = true;
                            txtSegundaAula.Enabled = false;

                        }
                        else if (Cursos.duracion_formacion13 == "8 Horas")
                        {
                            ordenTerceraEtapa();
                            gpbSeleccionRef.Enabled = false;
                            gpbLogistica.Enabled = false;
                            gpbHorarioCurso.Height = 169;
                        }
                        else if (Cursos.duracion_formacion13 == "16 Horas")
                        {
                            ordenTerceraEtapa();
                            gpbSeleccionRef.Enabled = false;
                            rdbNoIgualHorario.Enabled = true;
                            rdbSiIgualHorario.Enabled = true;
                            gpbLogistica.Enabled = false;
                            gpbHorarioCurso.Height = 169;
                        }
                    }


                }
                


            }


        }


        /* ------------- Metodos -------------------*/
        private void Controles_nivel_intermedio_EstatusInicial()
        {
            gpbRefrigerio.Enabled = true;
            gpbFecha.Enabled = true;
            gpbSegundaFecha.Enabled = false;
            gpbDifusion.Enabled = true;
            gpbFacilitador.Enabled = false;
            gpbDatosFa.Enabled = false;
            chkbCoFacilitador.Enabled =true;
            gpbCoFa.Enabled = false;
            gpbDatosCoFa.Enabled = false;
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
                txtAulaSeleccionada.Clear();

            }

        }

        private void ordenTerceraEtapa()
        {
            gpbHorarioCurso.Location = new Point(254, 47);
            gpbHorarioCurso.Height = 73;

            gpbSeleccionRef.Location = new Point(254, 132);
            gpbSeleccionRef.Enabled = false;

            gpbLogistica.Location = new Point(254, 215);
            gpbLogistica.Height = 155;
            gpbLogistica.Width = 419;

            gpbInsumos.Location = new Point(254, 377);
            gpbInsumos.Height = 229;
            gpbInsumos.Width = 419;
           
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
                        ExisteFormacion = true;
                        Paquete_instruccional pq = new Clases.Paquete_instruccional();
                        pq = Formaciones.obtenerTodoPq(conexion.conexion, id_pq);
                        conexion.cerrarconexion();
                        Paquete_instruccional._bitacora = pq.bitacora;
                        Paquete_instruccional._contenido = pq.contenido;
                        
                        Paquete_instruccional._manual = pq.manual;
                        Paquete_instruccional._presentacion = pq.presentacion;
                        Paquete_instruccional.id_pin = id_pq;
                        Paquete_instruccional.tipo_curso = formacion.tipo_formacion;
                        Ver_paqueteInstruccional verp = new Ver_paqueteInstruccional();
                        verp.ShowDialog();
                        formacion.pq_inst = id_pq;
                       
                        
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
        private void deshabilitarControlesIntermedio()
        {
            
            gpbRefrigerio.Enabled = false;

            gpbFecha.Enabled = false; //aunque el groupbox esté deshabilitado, los dtp dentro no

            gpbFacilitador.Enabled = false;
            gpbDatosFa.Enabled = false;
            chkbCoFacilitador.Enabled = false;
            gpbDatosCoFa.Enabled = false;
            gpbCoFa.Enabled = false;
        }
        private void deshabilitarControlesAvanzado()
        {
            gpbHorarioCurso.Enabled = false;
            gpbSeleccionRef.Enabled = false;
            gpbLogistica.Enabled = false;
            gpbInsumos.Enabled = false;
        }
        private void habilitarIntermedio()
        {
            dtpFechaCurso.Enabled = true;

            if (Cursos.bloque_curso13 == "2")
            {
                dtpSegundaFecha.Enabled = true;
            }
            else
            {
                dtpSegundaFecha.Enabled = false;
            }
           
            gpbRefrigerio.Enabled = true;
            gpbFecha.Enabled = true;
            gpbFacilitador.Enabled = true;
            gpbDatosFa.Enabled = true;
            chkbCoFacilitador.Enabled = true;
            gpbCoFa.Enabled = true;
            gpbDatosCoFa.Enabled = true;
        }
        private void habilitarAvanzado()
        {
            gpbHorarioCurso.Enabled = true;
            gpbSeleccionRef.Enabled = true;
            gpbLogistica.Enabled = true;
            gpbInsumos.Enabled = true;
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
       
        private void llenarcomboFacilitador()
        {
            //llenar el combobox con las empresas registradas:
            cmbxFa.ValueMember = "id_faci";
            cmbxFa.DisplayMember = "nombreyapellido1";
            cmbxFa.DataSource = Clases.Paneles.LlenarCmbxFaTodos();
            cmbxFa.SelectedIndex = -1;
        }
        private void llenarcomboRefrigerio()
        {
            //llenar el combobox de la tercera etapa con los tipos de refrigerios
            cmbxTipoRefrigerio.ValueMember = "id_ref";
            cmbxTipoRefrigerio.DisplayMember = "nombre";
            cmbxTipoRefrigerio.DataSource = Paneles.llenarcmbxRef();
            cmbxTipoRefrigerio.SelectedIndex = -1;
        }
        private void llenarcombo2Refrigerio(int id_ref)
        {
            //llenar el combobox de la tercera etapa con los tipos de refrigerios
            cmbxTipoRefrigerio2.ValueMember = "id_ref";
            cmbxTipoRefrigerio2.DisplayMember = "nombre";
            cmbxTipoRefrigerio2.DataSource = Paneles.llenarcmbx2Ref(id_ref);
            cmbxTipoRefrigerio2.SelectedIndex = -1;
        }
        private void llenarComboCoFa(int id_facilitador)
        {
            //llenar el combobox con las empresas registradas:
            cmbxCoFa.ValueMember = "id_faci";
            cmbxCoFa.DisplayMember = "nombreyapellido1";
            cmbxCoFa.DataSource = Clases.Paneles.LlenarCmbxCoFa(id_facilitador);
            cmbxCoFa.SelectedIndex = -1;
        }

        private void llenarComboHorario(string tipo)
        {
            //llenar el combobox con los horarios registrados en la BD
            cmbxHorarios.ValueMember = "id_horario";
            cmbxHorarios.DisplayMember = "contenido_horario";
            cmbxHorarios.DataSource = Horarios.llenarcmbxHorario(tipo);
            cmbxHorarios.SelectedIndex = -1;
        }

        private void llenarComboHorario2(string tipo, int id)
        {
            //llenar el combobox con los horarios registrados en la BD
            cmbxHorario2.ValueMember = "id_horario";
            cmbxHorario2.DisplayMember = "contenido_horario";
            cmbxHorario2.DataSource = Horarios.llenarcmbxHorario2(tipo, id);
            cmbxHorario2.SelectedIndex = -1;
        }

        private static List<Facilitador_todos> listaCOFA_AFI(int id_curs, int idFA)
        {
            List<Facilitador_todos> lista = new List<Facilitador_todos>();
            MySqlDataReader cof = Conexion.ConsultarBD("select nombre_fa, apellido_fa from facilitadores fa inner join afi_tiene_facilitadores atf on atf.id_fa=fa.id_fa where atf.id_fa!= '"+idFA+"' AND atf.id_cursos_afi='"+id_curs+"'");
            while (cof.Read())
            {
                Facilitador_todos f = new Facilitador_todos();
                f.nombre_facilitador = cof["nombre_fa"].ToString();
                f.apellido_facilitador = cof["apellido_fa"].ToString();
                f.nombreyapellido1 = f.nombre_facilitador + " " + f.apellido_facilitador;
                lista.Add(f);

            }
            return lista;
        }
        private void llenarComboCOFA_AFI(int idC, int idFa)
        {
            cmbxCoFa.ValueMember = "id_faci";
            cmbxCoFa.DisplayMember = "nombreyapellido1";
            cmbxCoFa.DataSource = listaCOFA_AFI(idC,idFa);
            cmbxCoFa.SelectedIndex = -1;
        }
        private void evaluarAFI()
        {
            AFI.id_AFI = 0;
            formacion.nombre_formacion = txtNombreFormacion.Text;
            //verificar si el curso etá registrado en cursos_afi para seleccionar a los facilitadores que pueden realizar ese curso.
            MySqlDataReader id = Conexion.ConsultarBD("SELECT id_curso_afi from cursos_afi where nombre_curso_afi='" + formacion.nombre_formacion + "'");
            if (id.Read())
            {
                AFI.id_AFI = Convert.ToInt32(id["id_curso_afi"]);
            }
            id.Close();
            if (AFI.id_AFI != 0) //si el curso está registrado: se seleccionan los id de facilitadores y se quitan del comboboxFa los que no estén asignados a esa formación.
            {
               
                MySqlDataReader cof = Conexion.ConsultarBD("select nombre_fa, apellido_fa from facilitadores fa inner join afi_tiene_facilitadores atf on atf.id_fa=fa.id_fa where atf.id_cursos_afi='" + AFI.id_AFI + "'");
                while (cof.Read())
                {
                    Facilitador_todos f = new Facilitador_todos();
                    f.nombre_facilitador = cof["nombre_fa"].ToString();
                    f.apellido_facilitador = cof["apellido_fa"].ToString();
                    f.nombreyapellido1 = f.nombre_facilitador + " " + f.apellido_facilitador;
                    lista.Add(f);

                }
                
                cmbxFa.ValueMember = "id_faci";
                cmbxFa.DisplayMember = "nombreyapellido1";
                cmbxFa.DataSource = lista;
                cmbxFa.SelectedIndex = -1;
            }
        }       
        private void GuardarAfiE2()
        {
            AFI.id_AFI = 0;
            formacion.nombre_formacion = txtNombreFormacion.Text;
            //verificar si el curso etá registrado en cursos_afi para seleccionar a los facilitadores que pueden realizar ese curso.
            MySqlDataReader id = Conexion.ConsultarBD("SELECT id_curso_afi from cursos_afi where nombre_curso_afi='" + formacion.nombre_formacion + "'");
            if (id.Read())
            {
                AFI.id_AFI = Convert.ToInt32(id["id_curso_afi"]);
            }
            id.Close();

            if (AFI.id_AFI == 0) //si la formacion no está registrada en afi --> se registra la formacion y la asignacion de facilitador
            {
                MySqlDataReader add = Conexion.ConsultarBD("INSERT INTO cursos_afi (nombre_curso_afi) VALUES ('" + formacion.nombre_formacion + "')");
                add.Close();
                MySqlDataReader idc = Conexion.ConsultarBD("SELECT id_curso_afi from cursos_afi where nombre_curso_afi='" + formacion.nombre_formacion + "'");
                if (idc.Read())
                {
                    AFI.id_AFI = Convert.ToInt32(idc["id_curso_afi"]);
                }
                idc.Close();
                MySqlDataReader leer = Conexion.ConsultarBD("INSERT INTO afi_tiene_facilitadores (id_cursos_afi, id_fa) VALUES ('" + AFI.id_AFI + "','" + fa.id_facilitador + "' ) ");
                leer.Close();
                // si el checkbox esta seleccionado es que tiene co-facilitador
                if (chkbCoFacilitador.Checked == true && cmbxCoFa.SelectedIndex != -1)
                {
                    MySqlDataReader leer2 = Conexion.ConsultarBD("INSERT INTO afi_tiene_facilitadores (id_cursos_afi, id_fa) VALUES ('" + AFI.id_AFI + "','" + Cofa.id_facilitador + "' ) ");
                    leer2.Close();
                }
            }

        }

        private void CargarDatosEtapaUno()
        {
            fa.id_facilitador = 0;
            formacion.nombre_formacion = Cursos.nombre_formacion13;
            formacion.solicitado = Cursos.solicitud_formacion13;

            formacion.bloque_curso = Cursos.bloque_curso13;

            deshabiltarControlesBasico();
            //carga el nombre
            txtNombreFormacion.Text = Cursos.nombre_formacion13;
            //carga quien solicita
            txtSolicitadoPor.Text=Cursos.solicitud_formacion13;
            //carga duración y bloques
            switch (Cursos.duracion_formacion13)
            {
                case "4 Horas":
                    cmbxDuracionFormacion.SelectedIndex = 0;
                    cmbxBloques.Items.Clear();
                    cmbxBloques.Items.Add("1");
                    cmbxBloques.SelectedIndex = 0;
                    formacion.duracion = "4";
                    break;
                case "8 Horas":
                    cmbxDuracionFormacion.SelectedIndex = 1;
                    formacion.duracion = "8";
                    cmbxBloques.Items.Clear();
                    cmbxBloques.Items.Add("1");
                    cmbxBloques.Items.Add("2");
                    if (Cursos.bloque_curso13 == "2")
                    {
                        cmbxBloques.SelectedIndex = 1;
                    }
                    else
                    {
                        cmbxBloques.SelectedIndex = 0;
                    }
                    break;
                case "16 Horas":
                    cmbxDuracionFormacion.SelectedIndex = 2;
                    formacion.duracion = "16";
                    cmbxBloques.Items.Clear();
                    cmbxBloques.Items.Add("2");
                    cmbxBloques.SelectedIndex = 0;
                    break;
            }

            //CARGAR DATOS DE PAQUETE INSTRUCCIONAL

            contenido = Cursos.p_contenido;
            btnVerContenido.Enabled = true;
            if (Cursos.p_presentacion == "")
            {
                btnVerPresentacion.Enabled = false;
            }
            else
            {
                presentacion = Cursos.p_presentacion;
                btnVerPresentacion.Enabled = true;
            }

            


        }
        private void CargarDatosEtapaDos()
        {
            fa.id_facilitador = 0;
           
            //llenar datos del refrigerio
            if (Cursos.tiene_ref == "Si")
            {
                rdbSiRef.Checked = true;
            }
            else
            {
                rdbNoRef.Checked = true;
            }
            //llenar fechas
            dtpFechaCurso.Value = Convert.ToDateTime(Cursos.fecha_uno13);
            if (Cursos.bloque_curso13 == "2")
            {
                dtpSegundaFecha.Value = Convert.ToDateTime(Cursos.fecha_dos13);

                time.fechaDos_curso = Cursos.fecha_dos13;
            }
            else
            {
                dtpSegundaFecha.Enabled = false;
            }

            int id_curs = 0;
            string nombre_curso = txtNombreFormacion.Text;
            MySqlDataReader id = Conexion.ConsultarBD("SELECT id_curso_afi from cursos_afi where nombre_curso_afi='" + nombre_curso + "'");
            if (id.Read())
            {
                id_curs = Convert.ToInt32(id["id_curso_afi"]);
            }
            id.Close();

            List<Facilitador_todos> lf = new List<Facilitador_todos>();
            lf = Paneles.LlenarCmbxfa_AFI(id_curs);
            for (int i = 0; i < lf.Count; i++)
            {
                cmbxFa.Items.Add(lf[i].nombreyapellido1);
            }



            //seleccionar facilitador encargado del curso 
            int id_fa = 0, co_fa = 0;
            MessageBox.Show(Cursos.id_curso13.ToString());
            //buscar id_fa de acuerdo al id del curso
            MySqlDataReader leer = Conexion.ConsultarBD("SELECT * from cursos_tienen_fa where cursos_id_cursos = '" + Cursos.id_curso13 + "'");
            if (leer.Read())
            {
                id_fa = Convert.ToInt32(leer["facilitadores_id_fa"]);
                co_fa = Convert.ToInt32(leer["ctf_id_cofa"]);
            }
            leer.Close();
            MessageBox.Show(id_fa.ToString());
            //recoger informacion del facilitador
            MySqlDataReader nom = Conexion.ConsultarBD("select * from facilitadores where id_fa='" + id_fa + "'");
            while (nom.Read())
            {
                fcombo.id_faci = id_fa;
                fcombo.nombre_facilitador = nom["nombre_fa"].ToString();
                fcombo.apellido_facilitador = nom["apellido_fa"].ToString();
                fcombo.correo_facilitador = nom["correo_fa"].ToString();
                fcombo.tlfn_facilitador = nom["tlfn_fa"].ToString();
                fcombo.nombreyapellido1 = fcombo.nombre_facilitador + " " + fcombo.apellido_facilitador;

                MessageBox.Show(fcombo.nombreyapellido1);
                cmbxFa.Text = fcombo.nombreyapellido1;
                txtCorreoFa.Text = fcombo.correo_facilitador;
                txtTlfnFa.Text = fcombo.tlfn_facilitador;
            }
            nom.Close();
            //recorremos el comboboxFa
            for (int i = 0; i < cmbxFa.Items.Count; i++)
            {
                if (cmbxFa.Items[i].ToString() == fcombo.nombreyapellido1)
                {
                    cmbxFa.SelectedItem = cmbxFa.Items[i];
                    txtCorreoFa.Text = fcombo.correo_facilitador;
                    txtTlfnFa.Text = fcombo.tlfn_facilitador;
                }
            }


            //evaluar si esa formacion tiene co facilitador
            if (co_fa != 0)
            {
                Facilitador_todos cf = new Facilitador_todos();
                //recoger informacion del facilitador
                MySqlDataReader cnom = Conexion.ConsultarBD("select * from facilitadores where id_fa='" + co_fa + "'");
                while (cnom.Read())
                {

                    cf.nombre_facilitador = cnom["nombre_fa"].ToString();
                    cf.apellido_facilitador = cnom["apellido_fa"].ToString();
                    cf.correo_facilitador = cnom["correo_fa"].ToString();
                    cf.tlfn_facilitador = cnom["tlfn_fa"].ToString();
                    cf.nombreyapellido1 = cf.nombre_facilitador + " " + cf.apellido_facilitador;
                }
                cnom.Close();
                cmbxCoFa.Text = cf.nombreyapellido1;
                txtCorreoCoFa.Text = cf.correo_facilitador;
                txtTlfnCoFa.Text = cf.tlfn_facilitador;
                chkbCoFacilitador.Checked = true;

                Cofa.id_facilitador = co_fa;
            }
            else
            {
                chkbCoFacilitador.Checked = false;
            }

            formacion.ubicacion_ucs = Cursos.ubicacion_ucs;
            formacion.tiene_ref = Cursos.tiene_ref;
            time.fecha_curso = Cursos.fecha_uno13;
            fa.id_facilitador = id_fa;


        }
        private void CargarDatosEtapaTres()
        {
            CargarDatosEtapaUno();

            CargarDatosEtapaDos();

            deshabilitarControlesAvanzado();
            time.horario_curso1 = Cursos.horario1;

            txtAulaSeleccionada.Text = Cursos.aula1;

            if (Cursos.tiene_ref == "Si")
            {
                cmbxTipoRefrigerio.Text = Cursos.tipo_ref1;
                if (Cursos.tipo_ref2 != "No aplica")
                {
                    cmbxTipoRefrigerio2.Text = Cursos.tipo_ref2;
                }
            }
            else
            {
                cmbxTipoRefrigerio.SelectedIndex = -1;
                cmbxTipoRefrigerio2.SelectedIndex = -1;
            }
            //si la formacion tiene dos bloques
            if (Cursos.bloque_curso13 == "2")
            {
                cmbxHorarios.Text = time.horario_curso1;
                //si el horario2 es igual al horario1
                if (Cursos.horario2 == Cursos.horario1)
                {
                    rdbSiIgualHorario.Checked = true;
                    cmbxHorario2.Text = Cursos.horario2;
                }
                else
                {
                    rdbNoIgualHorario.Checked = true;
                    cmbxHorario2.Text = Cursos.horario2;
                }
                //si el aula2 es igual al aula1
                if (Cursos.aula2 == Cursos.aula1)
                {
                    txtSegundaAula.Text = Cursos.aula2;
                    rdbSiMantenerAula.Checked = true;
                }
                else
                {
                    txtSegundaAula.Text = Cursos.aula2;
                    rdbNoMantenerAula.Checked = true;
                }
            }
            else
            {
                cmbxHorarios.Text = time.horario_curso1;
                cmbxHorario2.SelectedIndex = -1;
                txtSegundaAula.Text = Cursos.aula2;
                rdbSiIgualHorario.Checked = false;
                rdbNoIgualHorario.Checked = false;
                rdbSiMantenerAula.Checked = false;
                rdbNoMantenerAula.Checked = false;
            }
            List<string> lista_insumo = new List<string>();

            MessageBox.Show(cmbxHorarios.Text + " :Horario1" + txtAulaSeleccionada.Text + " :aula ");

            //buscar todos los insumos registrados para esa formacion
            MySqlDataReader ins = Conexion.ConsultarBD("SELECT * FROM insumos i inner join cursos_tienen_insumos cti on cti.cti_id_insumo = i.id_insumos where cti.cti_id_curso = '" + Cursos.id_curso13 + "'");
            while (ins.Read())
            {
                string insumo = Convert.ToString(ins["ins_contenido"]);
                lista_insumo.Add(insumo);
            }
            ins.Close();
            //recorrer el datagridview y buscar igualdades para luego marcar el check
            for (int i = 0; i < lista_insumo.Count; i++)
            {
                foreach (DataGridViewRow row in dgvInsumos.Rows)
                {
                    string celda = Convert.ToString(row.Cells["insumno"].Value);
                    if (celda == lista_insumo[i])
                    {
                        row.Cells["seleccion_opcion"].Value = true;
                    }
                }
            }
        }

        public void GuardarBasico()
        {
            //el estatus del curso en esta etapa siempre será "En curso"
            // los posibles estatus son: En curso, Reprogramado, Suspendido

            try
            {
                formacion.tiene_ref = "0"; // no tiene por estar en la etapa 1 o nivel básico (esto se actualiza en la etapa 2)
                formacion.ubicacion_ucs = "Si"; //siempre es si
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
                                        p_inst.bitacora = "";
                                        p_inst.manual = "";


                                        if (btnVerPresentacion.Enabled == false)
                                        {
                                            p_inst.presentacion = "";                                            
                                        }

                                        if (conexion.abrirconexion() == true)
                                            p_inst.id_pinstruccional = Clases.Formaciones.ObtenerIdPaquete(conexion.conexion, p_inst);
                                        conexion.cerrarconexion();

                                        formacion.fecha_inicial = fecha_creacion;
                                        formacion.TiempoEtapa = Convert.ToString(fecha_modifinal - fecha_creacion);
                                        //MessageBox.Show(formacion.TiempoEtapa);
                                        formacion.id_user = Clases.Usuario_logeado.id_usuario;
                                        formacion.etapa_curso = 1;//representa la etapa actual: nivel_basico (cambiará para cada panel)


                                        if (p_inst.id_pinstruccional == 0)//si no arroja coincidencias, no existe un paquete con el mismo contenido-- PROCEDE A GUARDAR
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
                                                                int agregarUGC = Clases.Formaciones.Agregar_U_g_C(conexion.conexion, id_curso, formacion.id_user, fecha_creacion, fecha_modifinal);
                                                                conexion.cerrarconexion();
                                                                if (agregarUGC > 0)
                                                                {
                                                                    evaluarAFI();
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
                                                        int agregarUGC = Clases.Formaciones.Agregar_U_g_C(conexion.conexion, id_curso, formacion.id_user, fecha_creacion, fecha_modifinal);
                                                        conexion.cerrarconexion();
                                                        if (agregarUGC > 0)
                                                        {
                                                            evaluarAFI();
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
                                    formacion.nombre_formacion = txtNombreFormacion.Text;
                                    p_inst.id_pinstruccional = formacion.pq_inst;
                                    conexion.cerrarconexion();
                                    if (conexion.abrirconexion() == true)
                                    {
                                        int cursoexiste = Clases.Formaciones.CursoEjecucionExiste(conexion.conexion, formacion);
                                        conexion.cerrarconexion();

                                        //if (cursoexiste == 0)
                                        //{
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
                                                                evaluarAFI();
                                                                    guardar = true;
                                                                    MessageBox.Show("La formación se ha agregado correctamente.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.None);
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

                                        //}
                                        //else
                                        //{
                                        //    MessageBox.Show("No se puede agregar esta formación. Existe una con el mismo nombre en estatus 'En curso'", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        //    vaciarFormacion();
                                        //}


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
        private void GuardarIntermedio()
        {
            try
            {
                //primer caso: cuando la formacion es de 4 hrs
                
                if (formacion.duracion == "4")
                {
                    
                    if (dtpFechaCurso.Value <= DateTime.Today)
                    {
                        errorProviderFecha.SetError(dtpFechaCurso, "La fecha seleccionada es inválida.");
                        dtpFechaCurso.Focus();
                    }
                    else
                    {
                        errorProviderFecha.SetError(dtpFechaCurso, "");
                       
                        if (cmbxFa.SelectedIndex == -1)
                        {
                            errorProviderSolicitado.SetError(cmbxFa, "Debe escoger un facilitador.");
                            cmbxFa.Focus();
                        }else
                        {
                            errorProviderSolicitado.SetError(cmbxFa, "");
                            
                            if(chkbCoFacilitador.Checked==true && cmbxCoFa.SelectedIndex == -1)
                            {
                                errorProviderContenido.SetError(cmbxCoFa, "No ha seleccionado ningún co-facilitador.");
                                cmbxCoFa.Focus();
                            }else
                            {
                                errorProviderContenido.SetError(cmbxCoFa, "");

                                if (nombre_publicidad.Count <= 0)
                                {
                                    errorProviderBloque.SetError(dgvMediosDifusion, "Debe seleccionar al menos 1 medio de difusión.");
                                }
                                else
                                {
                                    
                                    time.fecha_curso = dtpFechaCurso.Value.ToString("yyyy-MM-dd");
                                    DateTime FinalE2 = DateTime.Now;
                                    String duracionE2 = Convert.ToString(FinalE2 - inicioE2);
                                    List<Difusion> medios_publicitarios = new List<Difusion>();
                                    int id_curso = 0;
                                    //se obtiene el id del curso
                                    MySqlDataReader obtener_id_curso = Conexion.ConsultarBD("SELECT id_cursos FROM cursos WHERE nombre_curso = '" + txtNombreFormacion.Text + "' AND tipo_curso='Abierto'");
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
                                            //error en el codigo
                                        }

                                        seleccionId.Close();
                                        MySqlDataReader guardarRelacionCursoPublicidad = Conexion.ConsultarBD("INSERT INTO cursos_tiene_publicidad (ctp_id_curso, ctp_id_difusion, ctp_contenido_dif) VALUES('" + id_curso + "', '" + medios_publicitarios[i].id_dif + "', '" + medios_publicitarios[i].contenido_dif + "')");
                                       
                                        guardarRelacionCursoPublicidad.Close();
                                    }

                                    MySqlDataReader ActualizarCursoTieneRef = Conexion.ConsultarBD("UPDATE cursos SET tiene_ref='0' ");
                                    ActualizarCursoTieneRef.Close();

                                    //se actualiza el curso con los datos obtenidos de la segunda etapa, como las fechas
                                    MySqlDataReader ActualizarCursoSegundaEtapa = Conexion.ConsultarBD("UPDATE cursos SET etapa_curso ='2', fecha_uno='" + time.fecha_curso + "', duracionE2='" + duracionE2 + "', bloque_curso= '" + formacion.bloque_curso + "' WHERE id_cursos='" + id_curso + "'");
                                    ActualizarCursoSegundaEtapa.Close();

                                    GuardarAfiE2();

                                    // si el checkbox esta seleccionado es que tiene co-facilitador
                                    if (chkbCoFacilitador.Checked == true && cmbxCoFa.SelectedIndex != -1)
                                    {
                                        MySqlDataReader FacilitadorCurso = Conexion.ConsultarBD("INSERT INTO cursos_tienen_fa (cursos_id_cursos, facilitadores_id_fa, ctf_id_cofa, ctf_fecha) VALUES ('" + id_curso + "', '" + fa.id_facilitador + "', '" + Cofa.id_facilitador + "', '" + time.fecha_curso + "')");
                                        FacilitadorCurso.Close();
                                        guardar = true;
                                        MessageBox.Show("Los datos se han agregado correctamente.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.None);
                                    }
                                    else // sino, solo guarda al facilitador
                                    {
                                        MySqlDataReader FacilitadorCurso = Conexion.ConsultarBD("INSERT INTO cursos_tienen_fa (cursos_id_cursos, facilitadores_id_fa, ctf_fecha) VALUES ('" + id_curso + "', '" + fa.id_facilitador + "', '" + time.fecha_curso + "')");
                                        FacilitadorCurso.Close();
                                        guardar = true;
                                        MessageBox.Show("Los datos se han agregado correctamente.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.None);
                                    }

                                    //aqui estaran contemplados los arreglos (visuales y de contenido) de la siguiente etapa
                                    ordenTerceraEtapa();
                                    //llenar el cmbx de horarios:
                                    cmbxHorarios.Items.Clear();
                                    string tipo = "B";
                                    llenarComboHorario(tipo);

                                    //para la logistica:
                                    rdbSiMantenerAula.Enabled = false;
                                    rdbNoMantenerAula.Enabled = false;
                                    txtSegundaAula.Enabled = false;

                                }

                            }
                        }
     
                    }
                }
                else
                {
                    //segundo caso: cuando la formaicon es de 8 horas y tiene dos bloques, o sea, dos diías de 4 hrs
                    if(formacion.duracion=="8" && formacion.bloque_curso == "2")
                    {
                        
                        if (dtpFechaCurso.Value <= DateTime.Today)
                        {
                            errorProviderFecha.SetError(dtpFechaCurso, "La fecha seleccionada es inválida.");
                            dtpFechaCurso.Focus();
                        }
                        else
                        {
                            errorProviderFecha.SetError(dtpFechaCurso, "");
                            if (dtpSegundaFecha.Value <= dtpFechaCurso.Value || dtpSegundaFecha.Value <= DateTime.Today)
                            {
                                errorProviderContenido.SetError(dtpSegundaFecha, "La fecha seleccionada es inválida.");
                                dtpSegundaFecha.Focus();
                            }
                            else
                            {
                                errorProviderContenido.SetError(dtpSegundaFecha, "");
                                if (cmbxFa.SelectedIndex == -1)
                                {
                                    errorProviderSolicitado.SetError(cmbxFa, "Debe escoger un facilitador.");
                                    cmbxFa.Focus();
                                }
                                else
                                {
                                    errorProviderSolicitado.SetError(cmbxFa, "");

                                    if (chkbCoFacilitador.Checked == true && cmbxCoFa.SelectedIndex == -1)
                                    {
                                        errorProviderContenido.SetError(cmbxCoFa, "No ha seleccionado ningún co-facilitador.");
                                        cmbxCoFa.Focus();
                                    }
                                    else
                                    {
                                        errorProviderContenido.SetError(cmbxCoFa, "");

                                        if (nombre_publicidad.Count <= 0)
                                        {
                                            errorProviderBloque.SetError(dgvMediosDifusion, "Debe seleccionar al menos 1 medio de difusión.");
                                        }
                                        else
                                        {
                                            time.fecha_curso = dtpFechaCurso.Value.ToString("yyyy-MM-dd"); 
                                            time.fechaDos_curso = dtpSegundaFecha.Value.ToString("yyyy-MM-dd");
                                            DateTime FinalE2 = DateTime.Now;
                                            String duracionE2 = Convert.ToString(FinalE2 - inicioE2);
                                            List<Difusion> medios_publicitarios = new List<Difusion>();
                                            int id_curso = 0;
                                            //se obtiene el id del curso
                                            MySqlDataReader obtener_id_curso = Conexion.ConsultarBD("SELECT id_cursos FROM cursos WHERE nombre_curso = '" + txtNombreFormacion.Text + "' AND tipo_curso='Abierto'");
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
                                                    //error en el codigo
                                                }

                                                seleccionId.Close();
                                                MySqlDataReader guardarRelacionCursoPublicidad = Conexion.ConsultarBD("INSERT INTO cursos_tiene_publicidad (ctp_id_curso, ctp_id_difusion, ctp_contenido_dif) VALUES('" + id_curso + "', '" + medios_publicitarios[i].id_dif + "', '" + medios_publicitarios[i].contenido_dif + "')");
                                              
                                                guardarRelacionCursoPublicidad.Close();
                                            }

                                            MySqlDataReader ActualizarCursoTieneRef = Conexion.ConsultarBD("UPDATE cursos SET tiene_ref='0' ");
                                            ActualizarCursoTieneRef.Close();
                                            GuardarAfiE2();
                                            ////se actualiza la informacion del curso con los valores nuevos: 
                                            //se actualiza el curso con los datos obtenidos de la segunda etapa, como las fechas
                                            MySqlDataReader ActualizarCursoSegundaEtapa = Conexion.ConsultarBD("UPDATE cursos SET etapa_curso ='2', fecha_uno='" +time.fecha_curso+ "',fecha_dos='" + time.fechaDos_curso + "', duracionE2='" + duracionE2 + "', bloque_curso= '" + formacion.bloque_curso + "' WHERE id_cursos='" + id_curso + "'");
                                            ActualizarCursoSegundaEtapa.Close();
                                            // si el checkbox esta seleccionado es que tiene co-facilitador
                                            if (chkbCoFacilitador.Checked == true && cmbxCoFa.SelectedIndex != -1)
                                            {
                                                MySqlDataReader FacilitadorCurso = Conexion.ConsultarBD("INSERT INTO cursos_tienen_fa (cursos_id_cursos, facilitadores_id_fa, ctf_id_cofa, ctf_fecha, ctf_fecha2) VALUES ('" + id_curso + "', '" + fa.id_facilitador + "', '" + Cofa.id_facilitador + "', '" + time.fecha_curso + "', '" + time.fechaDos_curso + "')");
                                                FacilitadorCurso.Close();
                                                guardar = true;
                                                MessageBox.Show("Los datos se han agregado correctamente.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.None);
                                            }
                                            else // sino, solo guarda al facilitador
                                            {
                                                MySqlDataReader FacilitadorCurso = Conexion.ConsultarBD("INSERT INTO cursos_tienen_fa (cursos_id_cursos, facilitadores_id_fa, ctf_fecha, ctf_fecha2) VALUES ('" + id_curso + "', '" + fa.id_facilitador + "', '" + time.fecha_curso + "', '" + time.fechaDos_curso + "')");
                                                FacilitadorCurso.Close();
                                                guardar = true;
                                                MessageBox.Show("Los datos se han agregado correctamente.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.None);
                                            }

                                            //aqui estaran contemplados los arreglos (visuales y de contenido) de la siguiente etapa
                                            ordenTerceraEtapa();                                            
                                            gpbHorarioCurso.Height = 169;                                           
                                            gpbSeleccionRef.Visible = false;
                                            //llenar el cmbx de horarios:
                                            cmbxHorarios.Items.Clear();
                                            string tipo = "B";
                                            llenarComboHorario(tipo);
                                            //para la logistica:
                                            rdbSiMantenerAula.Enabled = true;
                                            rdbNoMantenerAula.Enabled = true;
                                            txtSegundaAula.Enabled = false;

                                        }

                                    }
                                }
                            }
                           
                        }
                    }
                    else
                    {
                        //tercer caso: cuando la formacion es de 8 horas y tiene 1 bloque, o sea, se hace en un día
                        if(formacion.duracion == "8" && formacion.bloque_curso == "1")
                        {
                            if (rdbSiRef.Checked == false && rdbNoRef.Checked == false)
                            {
                                errorProviderBloque.SetError(gpbRefrigerio, "Debe especificar si la formación poseerá refrigerio.");
                            }
                            else
                            {
                                if (dtpFechaCurso.Value <= DateTime.Today)
                                {
                                    errorProviderFecha.SetError(dtpFechaCurso, "La fecha seleccionada es inválida.");
                                    dtpFechaCurso.Focus();
                                }
                                else
                                {
                                    errorProviderFecha.SetError(dtpFechaCurso, "");

                                    if (cmbxFa.SelectedIndex == -1)
                                    {
                                        errorProviderSolicitado.SetError(cmbxFa, "Debe escoger un facilitador.");
                                        cmbxFa.Focus();
                                    }
                                    else
                                    {
                                        errorProviderSolicitado.SetError(cmbxFa, "");

                                        if (chkbCoFacilitador.Checked == true && cmbxCoFa.SelectedIndex == -1)
                                        {
                                            errorProviderContenido.SetError(cmbxCoFa, "No ha seleccionado ningún co-facilitador.");
                                            cmbxCoFa.Focus();
                                        }
                                        else
                                        {
                                            errorProviderContenido.SetError(cmbxCoFa, "");

                                            if (nombre_publicidad.Count <= 0)
                                            {
                                                errorProviderBloque.SetError(dgvMediosDifusion, "Debe seleccionar al menos 1 medio de difusión.");
                                            }
                                            else
                                            {
                                                time.fecha_curso = dtpFechaCurso.Value.ToString("yyyy-MM-dd");
                                                DateTime FinalE2 = DateTime.Now;
                                                String duracionE2 = Convert.ToString(FinalE2 - inicioE2);
                                                List<Difusion> medios_publicitarios = new List<Difusion>();
                                                int id_curso = 0;

                                                //se obtiene el id del curso
                                                MySqlDataReader obtener_id_curso = Conexion.ConsultarBD("SELECT id_cursos FROM cursos WHERE nombre_curso = '" + txtNombreFormacion.Text + "' AND tipo_curso='Abierto'");
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
                                                    gpbSeleccionRef.Enabled = true;
                                                    gpbSeleccionRef.Visible = true;
                                                    MySqlDataReader ActualizarCursoTieneRef = Conexion.ConsultarBD("UPDATE cursos SET tiene_ref='1' ");
                                                    ActualizarCursoTieneRef.Close();
                                                }

                                                //se actualiza el curso con los datos obtenidos de la segunda etapa, como las fechas
                                                MySqlDataReader ActualizarCursoSegundaEtapa = Conexion.ConsultarBD("UPDATE cursos SET etapa_curso ='2', fecha_uno='" + time.fecha_curso + "', duracionE2='" + duracionE2 + "', bloque_curso= '" + formacion.bloque_curso + "' WHERE id_cursos='" + id_curso + "'");
                                                ActualizarCursoSegundaEtapa.Close();

                                                GuardarAfiE2();

                                                // si el checkbox esta seleccionado es que tiene co-facilitador
                                                if (chkbCoFacilitador.Checked == true && cmbxCoFa.SelectedIndex != -1)
                                                {
                                                    MySqlDataReader FacilitadorCurso = Conexion.ConsultarBD("INSERT INTO cursos_tienen_fa (cursos_id_cursos, facilitadores_id_fa, ctf_id_cofa, ctf_fecha) VALUES ('" + id_curso + "', '" + fa.id_facilitador + "', '" + Cofa.id_facilitador + "', '" + time.fecha_curso + "')");
                                                    FacilitadorCurso.Close();
                                                    guardar = true;
                                                    MessageBox.Show("Los datos se han agregado correctamente.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.None);
                                                }
                                                else // sino, solo guarda al facilitador
                                                {
                                                    MySqlDataReader FacilitadorCurso = Conexion.ConsultarBD("INSERT INTO cursos_tienen_fa (cursos_id_cursos, facilitadores_id_fa, ctf_fecha) VALUES ('" + id_curso + "', '" + fa.id_facilitador + "', '" + time.fecha_curso + "')");
                                                    FacilitadorCurso.Close();
                                                    guardar = true;
                                                    MessageBox.Show("Los datos se han agregado correctamente.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.None);
                                                }

                                                //aqui estaran contemplados los arreglos (visuales y de contenido) de la siguiente etapa
                                                ordenTerceraEtapa();

                                                //llenar el cmbx de horarios:
                                                cmbxHorarios.Items.Clear();
                                                string tipo = "A";
                                                llenarComboHorario(tipo);

                                                //para la logistica:
                                                rdbSiMantenerAula.Enabled = false;
                                                rdbNoMantenerAula.Enabled = false;
                                                txtSegundaAula.Enabled = false;


                                            }

                                        }
                                    }



                                }
                            }
                            
                        }
                        else
                        {
                            //ultimo caso: cuando la formacion es de 16 horas, siempre tendrá dos bloques, o sea, dos dias
                            if(formacion.duracion == "16")
                            {
                                if (rdbSiRef.Checked == false && rdbNoRef.Checked==false)
                                {
                                    errorProviderBloque.SetError(gpbRefrigerio, "Debe especificar si la formación poseerá refrigerio.");
                                }
                                else
                                {

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
                                        string tipo = "A";
                                        llenarComboHorario(tipo);

                                        gpbSeleccionRef.Enabled = true;
                                        gpbSeleccionRef.Visible = true;
                                        MySqlDataReader ActualizarCursoTieneRef = Conexion.ConsultarBD("UPDATE cursos SET tiene_ref='1' ");
                                        ActualizarCursoTieneRef.Close();
                                    }



                                    if (dtpFechaCurso.Value <= DateTime.Today)
                                    {
                                        errorProviderFecha.SetError(dtpFechaCurso, "La fecha seleccionada es inválida.");
                                        dtpFechaCurso.Focus();
                                    }
                                    else
                                    {
                                        errorProviderFecha.SetError(dtpFechaCurso, "");
                                        if (dtpSegundaFecha.Value <= dtpFechaCurso.Value || dtpSegundaFecha.Value <= DateTime.Today)
                                        {
                                            errorProviderContenido.SetError(dtpSegundaFecha, "La fecha seleccionada es inválida.");
                                            dtpSegundaFecha.Focus();
                                        }
                                        else
                                        {
                                            errorProviderContenido.SetError(dtpSegundaFecha, "");
                                            if (cmbxFa.SelectedIndex == -1)
                                            {
                                                errorProviderSolicitado.SetError(cmbxFa, "Debe escoger un facilitador.");
                                                cmbxFa.Focus();
                                            }
                                            else
                                            {
                                                errorProviderSolicitado.SetError(cmbxFa, "");

                                                if (chkbCoFacilitador.Checked == true && cmbxCoFa.SelectedIndex == -1)
                                                {
                                                    errorProviderContenido.SetError(cmbxCoFa, "No ha seleccionado ningún co-facilitador.");
                                                    cmbxCoFa.Focus();
                                                }
                                                else
                                                {
                                                    errorProviderContenido.SetError(cmbxCoFa, "");

                                                    if (nombre_publicidad.Count <=0)
                                                    {
                                                        errorProviderBloque.SetError(dgvMediosDifusion, "Debe seleccionar al menos 1 medio de difusión.");
                                                    }
                                                    else
                                                    {
                                                        time.fecha_curso = dtpFechaCurso.Value.ToString("yyyy-MM-dd"); 
                                                        time.fechaDos_curso=dtpSegundaFecha.Value.ToString("yyyy-MM-dd");
                                                        DateTime FinalE2 = DateTime.Now;
                                                        String duracionE2 = Convert.ToString(FinalE2 - inicioE2);
                                                        List<Difusion> medios_publicitarios = new List<Difusion>();
                                                        int id_curso = 0;
                                                        //se obtiene el id del curso
                                                        MySqlDataReader obtener_id_curso = Conexion.ConsultarBD("SELECT id_cursos FROM cursos WHERE nombre_curso = '" + txtNombreFormacion.Text + "' AND tipo_curso='Abierto'");
                                                        if (obtener_id_curso.Read())
                                                        {
                                                            id_curso = int.Parse(obtener_id_curso["id_cursos"].ToString());
                                                        }
                                                        obtener_id_curso.Close();
                                                        Difusion d = new Difusion();
                                                        //crear una lista donde se agreguen los nombres de cada fila para luego buscar el id de ellos (los seleccionado) y guardarlo en la bd
                                                        for (int i = 0; i < nombre_publicidad.Count; i++)
                                                        {
                                                           string publi=nombre_publicidad[i].ToString();
                                                            MySqlDataReader seleccionId = Conexion.ConsultarBD("SELECT id_difusion FROM difusion WHERE dif_contenido = '" + publi + "' LIMIT 1");
                                                            while (seleccionId.Read())
                                                            {                                                               
                                                                d.id_dif = int.Parse(seleccionId["id_difusion"].ToString());
                                                                d.contenido_dif =publi;
                                                                medios_publicitarios.Add(d);
                                                              
                                                            }

                                                            seleccionId.Close();
                                                            MySqlDataReader guardarRelacionCursoPublicidad = Conexion.ConsultarBD("INSERT INTO cursos_tiene_publicidad (ctp_id_curso, ctp_id_difusion, ctp_contenido_dif) VALUES('" + id_curso + "', '" + medios_publicitarios[i].id_dif + "', '" + medios_publicitarios[i].contenido_dif + "')");
                                                           
                                                            guardarRelacionCursoPublicidad.Close();
                                                        }

                                                        ////se actualiza la informacion del curso con los valores nuevos: 
                                                        //se actualiza el curso con los datos obtenidos de la segunda etapa, como las fechas
                                                        MySqlDataReader ActualizarCursoSegundaEtapa = Conexion.ConsultarBD("UPDATE cursos SET etapa_curso ='2', fecha_uno='" +time.fecha_curso + "',fecha_dos='" + time.fechaDos_curso + "', duracionE2='" + duracionE2 + "', bloque_curso= '" + formacion.bloque_curso + "' WHERE id_cursos='" + id_curso + "'");
                                                        ActualizarCursoSegundaEtapa.Close();

                                                        GuardarAfiE2();

                                                        // si el checkbox esta seleccionado es que tiene co-facilitador
                                                        if (chkbCoFacilitador.Checked == true && cmbxCoFa.SelectedIndex != -1)
                                                        {
                                                            MySqlDataReader FacilitadorCurso = Conexion.ConsultarBD("INSERT INTO cursos_tienen_fa (cursos_id_cursos, facilitadores_id_fa, ctf_id_cofa, ctf_fecha, ctf_fecha2) VALUES ('" + id_curso + "', '" + fa.id_facilitador + "', '" + Cofa.id_facilitador + "', '" + time.fecha_curso + "', '" + time.fechaDos_curso + "')");
                                                            FacilitadorCurso.Close();
                                                            guardar = true;
                                                            MessageBox.Show("Los datos se han agregado correctamente.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.None);
                                                         }
                                                         else // sino, solo guarda al facilitador
                                                         {
                                                            MySqlDataReader FacilitadorCurso = Conexion.ConsultarBD("INSERT INTO cursos_tienen_fa (cursos_id_cursos, facilitadores_id_fa, ctf_fecha, ctf_fecha2) VALUES ('" + id_curso + "', '" + fa.id_facilitador + "', '" + time.fecha_curso + "', '" + time.fechaDos_curso + "')");
                                                            FacilitadorCurso.Close();
                                                            guardar = true;
                                                            MessageBox.Show("Los datos se han agregado correctamente.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.None);
                                                         }

                                                        //aqui estaran contemplados los arreglos (visuales y de contenido) de la siguiente etapa


                                                        if (rdbSiRef.Checked == false)
                                                        {
                                                            ordenTerceraEtapa();
                                                            gpbSeleccionRef.Enabled = false;
                                                            gpbSeleccionRef.Visible = false;
                                                            gpbHorarioCurso.Height = 169;
                                                           

                                                        }
                                                        else
                                                        {
                                                            ordenTerceraEtapa();
                                                            gpbHorarioCurso.Location = new Point(33, 47);
                                                            gpbHorarioCurso.Height = 169;
                                                            gpbSeleccionRef.Location = new Point(464, 47);
                                                            gpbSeleccionRef.Height = 169;
                                                            gpbSeleccionRef.Enabled = true;
                                                            gpbSeleccionRef.Visible = true;
                                                            rdbNoMantenerRef.Visible = true;
                                                            rdbSiMantenerRef.Visible = true;
                                                            rdbNoMantenerRef.Enabled = true;
                                                            rdbSiMantenerRef.Enabled = true;
                                                        }
                                                        //llenar el cmbx de horarios:
                                                        cmbxHorarios.Items.Clear();
                                                        string tipo = "A";
                                                        llenarComboHorario(tipo);

                                                        //para la logistica:
                                                        rdbSiMantenerAula.Enabled = true;
                                                        rdbNoMantenerAula.Enabled = true;
                                                        txtSegundaAula.Enabled = false;

                                                    }

                                                }
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
        private void GuardarAvanzado()
        {
            try
            {
                if (formacion.duracion == "4")
                {
                    if (cmbxHorarios.SelectedIndex == -1)
                    {
                        errorProviderHora.SetError(cmbxHorarios, "Debe seleccionar uno de los horarios disponibles.");
                        cmbxHorarios.Focus();
                    }else
                    {
                        errorProviderHora.SetError(cmbxHorarios, "");
                        if(txtAulaSeleccionada.Text == "")
                        {
                            errorProviderNombreF.SetError(txtAulaSeleccionada, "Debe proporcionar el aula en el que se dictará la formación.");
                            txtAulaSeleccionada.Focus();
                        }else
                        {
                            errorProviderNombreF.SetError(txtAulaSeleccionada, "");
                            if(lista_insumo.Count <= 0)
                            {
                                errorProviderContenido.SetError(dgvInsumos, "Debe seleccionar los insumos que se usarán durante la formación.");

                            }else
                            {
                                errorProviderContenido.SetError(dgvInsumos, "");
                                
                                DateTime FinalE3 = DateTime.Now;
                                String duracionE3 = Convert.ToString(FinalE3 - inicioE3);
                                string aula = txtAulaSeleccionada.Text;
                              
                                List<Insumos> insumos_curso = new List<Insumos>();
                               
                                int id_curso = 0;
                                //se obtiene el id del curso
                                MySqlDataReader obtener_id_curso = Conexion.ConsultarBD("SELECT id_cursos FROM cursos WHERE nombre_curso = '" + txtNombreFormacion.Text + "' AND tipo_curso='Abierto'");
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
                                //se actualiza el curso con los datos obtenidos de la segunda etapa, como las fechas
                                MySqlDataReader ActualizarCursoTerceraEtapa = Conexion.ConsultarBD("UPDATE cursos SET etapa_curso ='3', duracionE3='" + duracionE3 + "', horario_uno='"+ id_horario +"', aula_dia1='"+ aula +"' WHERE id_cursos='" + id_curso + "'");
                                ActualizarCursoTerceraEtapa.Close();
                                MessageBox.Show("Los datos se han agregado correctamente.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.None);
                               

                            }
                        }
                    }
                }else if(formacion.duracion == "8" && formacion.bloque_curso == "2")
                {
                    if (cmbxHorarios.SelectedIndex == -1)
                    {
                        errorProviderHora.SetError(cmbxHorarios, "Debe seleccionar uno de los horarios disponibles.");
                        cmbxHorarios.Focus();
                    }
                    else
                    {
                        errorProviderHora.SetError(cmbxHorarios, "");
                        if(rdbNoIgualHorario.Checked==true && cmbxHorario2.SelectedIndex == -1)
                        {
                            errorProviderBloque.SetError(cmbxHorario2, "Debe seleccionar uno de los horarios disponibles.");
                            cmbxHorario2.Focus();

                        }else
                        {
                            errorProviderBloque.SetError(cmbxHorario2, "");
                            if (txtAulaSeleccionada.Text == "")
                            {
                                errorProviderNombreF.SetError(txtAulaSeleccionada, "Debe proporcionar el aula en el que se dictará la formación.");
                                txtAulaSeleccionada.Focus();
                            }
                            else
                            {
                                errorProviderNombreF.SetError(txtAulaSeleccionada, "");
                                if (rdbNoMantenerAula.Checked==true && txtSegundaAula.Text == "")
                                {
                                    errorProviderContenido.SetError(txtSegundaAula, "Debe proporcionar el aula en el que se dictará la formación.");
                                    txtSegundaAula.Focus();
                                }
                                else
                                {
                                    errorProviderContenido.SetError(txtSegundaAula, "");

                                    if (lista_insumo.Count <= 0)
                                    {
                                        errorProviderContenido.SetError(dgvInsumos, "Debe seleccionar los insumos que se usarán durante la formación.");

                                    }
                                    else
                                    {
                                        errorProviderContenido.SetError(dgvInsumos, "");

                                        DateTime FinalE3 = DateTime.Now;
                                        String duracionE3 = Convert.ToString(FinalE3 - inicioE3);
                                        string aula = txtAulaSeleccionada.Text;
                                        string aula2;
                                        if (rdbSiMantenerAula.Checked)
                                        {
                                            aula2 = aula;
                                        }else
                                        {
                                            aula2 = txtSegundaAula.Text;
                                        }
                                                                              
                                        
                                        List<Insumos> insumos_curso = new List<Insumos>();
                                        
                                        int id_curso = 0;
                                        //se obtiene el id del curso
                                        MySqlDataReader obtener_id_curso = Conexion.ConsultarBD("SELECT id_cursos FROM cursos WHERE nombre_curso = '" + txtNombreFormacion.Text + "' AND tipo_curso='Abierto'");
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
                                        //se actualiza el curso con los datos obtenidos de la segunda etapa, como las fechas
                                        MySqlDataReader ActualizarCursoTerceraEtapa = Conexion.ConsultarBD("UPDATE cursos SET etapa_curso ='3', duracionE3='" + duracionE3 + "', horario_uno='" + id_horario + "', horario_dos='" + id_horario2 + "', aula_dia1='" + aula + "', aula_dia2='" + aula2 + "' WHERE id_cursos='" + id_curso + "'");
                                        ActualizarCursoTerceraEtapa.Close();
                                        MessageBox.Show("Los datos se han agregado correctamente.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.None);


                                    }
                                }

                            }

                        }
                        
                    }
                }
                else if (formacion.duracion == "8" && formacion.bloque_curso == "1")
                {
                    
                    if (cmbxHorarios.SelectedIndex == -1)
                    {
                        errorProviderHora.SetError(cmbxHorarios, "Debe seleccionar uno de los horarios disponibles.");
                        cmbxHorarios.Focus();
                    }
                    else
                    {
                        errorProviderHora.SetError(cmbxHorarios, "");
                        if (gpbSeleccionRef.Enabled == true )
                        {
                            if(cmbxTipoRefrigerio.SelectedIndex == -1)
                            {
                                errorProviderFecha.SetError(cmbxTipoRefrigerio, "Debe seleccionar un tipo de refrigerio para la formación.");
                                cmbxTipoRefrigerio.Focus();

                            }
                        }
                        errorProviderFecha.SetError(cmbxTipoRefrigerio, "");
                       
                        if (txtAulaSeleccionada.Text == "")
                        {
                            errorProviderNombreF.SetError(txtAulaSeleccionada, "Debe proporcionar el aula en el que se dictará la formación.");
                            txtAulaSeleccionada.Focus();
                        }
                        else
                        {
                            errorProviderNombreF.SetError(txtAulaSeleccionada, "");
                            if (lista_insumo.Count <= 0)
                            {
                                errorProviderContenido.SetError(dgvInsumos, "Debe seleccionar los insumos que se usarán durante la formación.");

                            }
                            else
                            {
                                errorProviderContenido.SetError(dgvInsumos, "");
                                                             
                             
                                DateTime FinalE3 = DateTime.Now;
                                String duracionE3 = Convert.ToString(FinalE3 - inicioE3);
                                string aula = txtAulaSeleccionada.Text;                             

                                List<Insumos> insumos_curso = new List<Insumos>();
                               
                                int id_curso = 0;

                                //se obtiene el id del curso
                                MySqlDataReader obtener_id_curso = Conexion.ConsultarBD("SELECT id_cursos FROM cursos WHERE nombre_curso = '" + txtNombreFormacion.Text + "' AND tipo_curso='Abierto'");
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
                                    MySqlDataReader ActualizarCursoTerceraEtapa = Conexion.ConsultarBD("UPDATE cursos SET etapa_curso ='3', duracionE3='" + duracionE3 + "', horario_uno='" + id_horario + "', aula_dia1='" + aula + "', id_ref1='"+id_refrigerio+"' WHERE id_cursos='" + id_curso + "'");
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
                else if (formacion.duracion == "16")
                {
                    if (cmbxHorarios.SelectedIndex == -1)
                    {
                        errorProviderHora.SetError(cmbxHorarios, "Debe seleccionar uno de los horarios disponibles.");
                        cmbxHorarios.Focus();
                    }
                    else
                    {
                        errorProviderHora.SetError(cmbxHorarios, "");
                        if (rdbNoIgualHorario.Checked==true && cmbxHorario2.SelectedIndex == -1)
                        {
                            errorProviderBloque.SetError(cmbxHorario2, "Debe seleccionar uno de los horarios disponibles.");
                            cmbxHorario2.Focus();

                        }
                        else
                        {
                            errorProviderBloque.SetError(cmbxHorario2, "");
                            if (gpbSeleccionRef.Enabled == true)
                            {
                                if (cmbxTipoRefrigerio.SelectedIndex == -1)
                                {
                                    errorProviderFecha.SetError(cmbxTipoRefrigerio, "Debe seleccionar un tipo de refrigerio para la formación.");
                                    cmbxTipoRefrigerio.Focus();

                                }else if (rdbNoMantenerRef.Checked==true && cmbxTipoRefrigerio2.SelectedIndex == -1)
                                {
                                    errorProviderBloque.SetError(cmbxTipoRefrigerio2, "Debe seleccionar un tipo de refrigerio para la formación.");
                                    cmbxTipoRefrigerio2.Focus();
                                }
                            }
                            errorProviderFecha.SetError(cmbxTipoRefrigerio, "");
                            errorProviderBloque.SetError(cmbxTipoRefrigerio2, "");
                            if (txtAulaSeleccionada.Text == "")
                            {
                                errorProviderNombreF.SetError(txtAulaSeleccionada, "Debe proporcionar el aula en el que se dictará la formación.");
                                txtAulaSeleccionada.Focus();
                            }
                            else
                            {
                                errorProviderNombreF.SetError(txtAulaSeleccionada, "");
                                if (rdbNoMantenerAula.Checked && txtSegundaAula.Text == "")
                                {
                                    errorProviderContenido.SetError(txtSegundaAula, "Debe proporcionar el aula en el que se dictará la formación.");
                                    txtSegundaAula.Focus();
                                }
                                else
                                {
                                    errorProviderContenido.SetError(txtSegundaAula, "");

                                    if (lista_insumo.Count <= 0)
                                    {
                                        errorProviderContenido.SetError(dgvInsumos, "Debe seleccionar los insumos que se usarán durante la formación.");

                                    }
                                    else
                                    {
                                        errorProviderContenido.SetError(dgvInsumos, "");

                                        DateTime FinalE3 = DateTime.Now;
                                        String duracionE3 = Convert.ToString(FinalE3 - inicioE3);
                                        string aula = txtAulaSeleccionada.Text;
                                        string aula2;
                                        if (rdbSiMantenerAula.Checked)
                                        {
                                            aula2 = aula;
                                        }
                                        else
                                        {
                                            aula2 = txtSegundaAula.Text;
                                        }
                                      
                                       
                                        List<Insumos> insumos_curso = new List<Insumos>();
                                       
                                        int id_curso = 0;
                                        //se obtiene el id del curso
                                        MySqlDataReader obtener_id_curso = Conexion.ConsultarBD("SELECT id_cursos FROM cursos WHERE nombre_curso = '" + txtNombreFormacion.Text + "' AND tipo_curso='Abierto'");
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
                                            MySqlDataReader ActualizarCursoTerceraEtapa = Conexion.ConsultarBD("UPDATE cursos SET etapa_curso ='3', duracionE3='" + duracionE3 + "', horario_uno='" + id_horario + "', horario_dos='" + id_horario2 + "', aula_dia1='" + aula + "', aula_dia2='" + aula2 + "', id_ref1='"+id_refrigerio+ "',id_ref2='" + id_refrigerio2 + "' WHERE id_cursos='" + id_curso + "'");
                                            ActualizarCursoTerceraEtapa.Close();
                                        }else
                                        {
                                            MySqlDataReader ActualizarCursoTerceraEtapa = Conexion.ConsultarBD("UPDATE cursos SET etapa_curso ='3', duracionE3='" + duracionE3 + "', horario_uno='" + id_horario + "', horario_dos='" + id_horario2 + "', aula_dia1='" + aula + "', aula_dia2='" + aula2 + "' WHERE id_cursos='" + id_curso + "'");
                                            ActualizarCursoTerceraEtapa.Close();
                                        }
                                            //se actualiza el curso con los datos obtenidos de la segunda etapa, como las fechas
                                       
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
                    formacion.estatus = "En curso"; //predeterminado en esta etapa
                    formacion.tipo_formacion = "Abierto"; //predeterminado para este form
                    formacion.nombre_formacion = txtNombreFormacion.Text;

                    errorProviderNombreF.SetError(txtNombreFormacion, "");
                    
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
                        //para evitar que mysql borre los "\" se sustituyen por "/" que funcionan igual
                        contenido = contenido.Replace("\\", "/");
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
            if (pnlNivel_basico.Visible == true)
            {
                deshabiltarControlesBasico();
            }
            else if (pnlNivel_intermedio.Visible == true)
            {
                //se deshabilitará controles del nivel intermedio
                deshabilitarControlesIntermedio();
            }
            else if (pnlNivel_avanzado.Visible == true)
            {
                deshabilitarControlesAvanzado();
            }

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
            if (Formaciones.creacion == true)
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
                    dtpSegundaFecha.Enabled = true;
                    chkbCoFacilitador.Enabled = true;
                    cmbxFa.Enabled = true;
                    gpbDifusion.Enabled = true;

                }
                else if (pnlNivel_avanzado.Visible == true)
                {
                    cmbxHorarios.Enabled = true;
                    cmbxTipoRefrigerio.Enabled = true;
                    txtAulaSeleccionada.Enabled = true;
                    gpbInsumos.Enabled = true;
                }

                Load_Sig_Re();
                if (guardar == true)
                {
                    btnSiguienteEtapa.Enabled = true;
                }

            }
            else
            {
                if (pnlNivel_basico.Visible == true)
                {
                    //si retomar, se habilitan todos los campos
                    txtNombreFormacion.Enabled = true;
                    txtSolicitadoPor.Enabled = true;
                    cmbxDuracionFormacion.Enabled = true;
                    cmbxBloques.Enabled = true;

                    btnRutaContenido.Enabled = true;
                    btnRutaPresentacion.Enabled = true;
                    
                    btnVerContenido.Enabled = false;
                    
                    btnVerPresentacion.Enabled = false;
                    

                    //comportamiento del panel lateral:
                    btnGuardar.Enabled = false;
                    btnLimpiar.Enabled = false;
                    btnRetomar.Enabled = false;

                    btnPausar.Enabled = true;
                    btnModificar.Enabled = true;
                    btnSiguienteEtapa.Enabled = true;


                }
                else if (pnlNivel_intermedio.Visible == true)
                {


                    btnGuardar.Enabled = false;
                    btnRetomar.Enabled = false;
                    btnPausar.Enabled = true;
                    btnModificar.Enabled = true;

                    if (Cursos.etapa_formacion13 == 1)
                    {
                        btnSiguienteEtapa.Enabled = false;

                    }
                    else if (Cursos.etapa_formacion13 == 2)
                    {
                        btnSiguienteEtapa.Enabled = false;
                        habilitarIntermedio();
                        btnCorreoComercializacion.Enabled = false; // se deshabilita hasta que modifique en algo
                        btnCorreoFacilitador.Enabled = false; // se deshabilita hasta que modifique en algo
                    }
                    else
                    {
                        btnSiguienteEtapa.Enabled = true;
                    }



                }
                else if (pnlNivel_avanzado.Visible == true)
                {
                    btnSiguienteEtapa.Enabled = false;

                    btnRetomar.Enabled = false;
                    btnPausar.Enabled = true;
                    btnModificar.Enabled = true;
                    habilitarAvanzado();
                    if (Cursos.tiene_ref == "No")
                    {
                        gpbSeleccionRef.Enabled = false;
                    }
                    if (Cursos.ubicacion_ucs == "No")
                    {
                        gpbSeleccionRef.Enabled = false;
                        gpbLogistica.Enabled = false;
                    }
                }
            }
            
        }

        private void btnSiguienteEtapa_Click(object sender, EventArgs e)
        {
            guardar = false;
            if (Formaciones.creacion == true)
            {
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
                        dtpSegundaFecha.Enabled = false;
                    }
                    else
                    {
                        if (formacion.duracion == "8" && formacion.bloque_curso == "1")
                        {
                            Controles_nivel_intermedio_EstatusInicial();
                            dtpSegundaFecha.Enabled = false;
                            rdbNoRef.Checked = true;
                        }
                        else if (formacion.duracion == "8" && formacion.bloque_curso == "2")
                        {
                            Controles_nivel_intermedio_EstatusInicial();
                            dtpSegundaFecha.Enabled = true;
                            dtpFechaCurso.Focus();
                        }
                        else
                        {
                            if (formacion.duracion == "16")
                            {
                                Controles_nivel_intermedio_EstatusInicial();
                                dtpSegundaFecha.Enabled = true;
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
                if (AFI.id_AFI == 0)
                    MessageBox.Show("Ningun facilitador ha colaborado antes en esta formación.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }
            else //si viene referenciado desde modificar
            {
                btnGuardar.Enabled = false;
                if (pnlNivel_basico.Visible == true)
                {
                    LabelCabecera.Text = "" + Cursos.nombre_formacion13 + ": Información básica";
                    LabelCabecera.Location = new Point(115, 31);

                    pnlNivel_basico.Visible = false;
                    pnlNivel_intermedio.Visible = true;

                    btnGuardar.Enabled = false;
                    btnLimpiar.Enabled = false;
                    btnRetomar.Enabled = true;

                    //comportamiento del panel nivel_intermedio de acuerdo a la duracion del curso
                    if (formacion.duracion == "4")
                    {
                        Controles_nivel_intermedio_EstatusInicial();
                        gpbRefrigerio.Enabled = false;
                        dtpSegundaFecha.Enabled = false;
                        errorProviderFecha.SetError(dtpSegundaFecha, "");

                    }
                    else if (formacion.duracion == "8" && formacion.bloque_curso == "1")
                    {
                        Controles_nivel_intermedio_EstatusInicial();
                        gpbRefrigerio.Enabled = false;
                        dtpSegundaFecha.Enabled = false;
                        errorProviderFecha.SetError(dtpSegundaFecha, "");
                    }
                    else if (formacion.duracion == "8" && formacion.bloque_curso == "2")
                    {
                        Controles_nivel_intermedio_EstatusInicial();
                        gpbRefrigerio.Enabled = false;
                    }
                    else if (formacion.duracion == "16")
                    {
                        Controles_nivel_intermedio_EstatusInicial();
                        gpbRefrigerio.Enabled = false;
                    }

                    if (Cursos.etapa_formacion13 == 1)
                    {
                        btnSiguienteEtapa.Enabled = false;
                        btnRetomar.Enabled = false;
                        btnPausar.Enabled = true;
                        btnModificar.Enabled = true;
                    }
                    else if (Cursos.etapa_formacion13 == 2)
                    {
                        btnSiguienteEtapa.Enabled = true;
                        btnRetomar.Enabled = true;
                        btnPausar.Enabled = false;
                        btnModificar.Enabled = false;
                        //--campos desactivados (etapa intermedio)
                        deshabilitarControlesIntermedio();
                    }
                    else if (Cursos.etapa_formacion13 == 3)
                    {
                        deshabilitarControlesIntermedio();
                        btnSiguienteEtapa.Enabled = true;
                        btnRetomar.Enabled = false;
                        btnPausar.Enabled = false;
                        btnModificar.Enabled = false;
                    }
                }
                else if (pnlNivel_intermedio.Visible == true)
                {


                    lblEtapaSiguiente.Location = new Point(3, 529);
                    lblEtapaSiguiente.Text = "Añadir participantes";

                    lblEtapafinal.Location = new Point(3, 570);
                    lblEtapafinal.Text = "Día de la formación";

                    LabelCabecera.Text = "Logística";
                    LabelCabecera.Location = new Point(250, 31);

                    btnGuardar.Enabled = false;

                    btnLimpiar.Enabled = false;
                    btnRetomar.Enabled = true;

                    if (Cursos.etapa_formacion13 == 1)
                    {
                        btnGuardar.Enabled = false;
                        btnSiguienteEtapa.Enabled = false;
                        btnRetomar.Enabled = false;
                        btnPausar.Enabled = true;
                        btnModificar.Enabled = true;
                    }
                    else if (Cursos.etapa_formacion13 == 2)
                    {
                        btnSiguienteEtapa.Enabled = true;
                        btnRetomar.Enabled = true;
                        btnPausar.Enabled = false;
                        btnModificar.Enabled = false;
                    }
                    else if (Cursos.etapa_formacion13 == 3)
                    {
                        btnSiguienteEtapa.Enabled = false;
                        btnRetomar.Enabled = true;
                        btnPausar.Enabled = false;
                        btnModificar.Enabled = false;

                    }
                    pnlNivel_intermedio.Visible = false;
                    pnlNivel_avanzado.Visible = true;

                }

            }


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
                time.fechaDos_curso = dtpSegundaFecha.Value.ToString("yyyy-MM-dd"); ;
                gpbFacilitador.Enabled = true;
                cmbxFa.Focus();
            }
        }
       
        private void cmbxFa_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (Formaciones.creacion == true)
            {
                MySqlDataReader cnom = Conexion.ConsultarBD("select * from facilitadores where nombre_apellido='" + cmbxFa.Text + "'");
                if (cnom.Read())
                {
                    fa.id_facilitador = Convert.ToInt32(cnom["id_fa"]);
                }
                cnom.Close();
                if (AFI.id_AFI == 0)
                {
                    llenarComboCoFa(fa.id_facilitador);
                }
                else
                {
                    llenarComboCOFA_AFI(AFI.id_AFI, fa.id_facilitador);
                }
                MessageBox.Show(fa.id_facilitador + " idFa true ");
            }
            else
            {
                AFI.id_AFI = 0;
                formacion.nombre_formacion = txtNombreFormacion.Text;
                //verificar si el curso etá registrado en cursos_afi para seleccionar a los facilitadores que pueden realizar ese curso.
                MySqlDataReader id = Conexion.ConsultarBD("SELECT id_curso_afi from cursos_afi where nombre_curso_afi='" + formacion.nombre_formacion + "'");
                if (id.Read())
                {
                    AFI.id_AFI = Convert.ToInt32(id["id_curso_afi"]);
                }
                id.Close();

                fa.id_facilitador = 0;
                MySqlDataReader cnom = Conexion.ConsultarBD("select * from facilitadores where nombre_apellido='" + cmbxFa.Text + "'");
                if (cnom.Read())
                {
                    fa.id_facilitador = Convert.ToInt32(cnom["id_fa"]);
                }
                cnom.Close();
                llenarComboCOFA_AFI(AFI.id_AFI, fa.id_facilitador);
                MessageBox.Show(fa.id_facilitador + " idFa y idafi" + AFI.id_AFI);


            }

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
                        if (fa.id_facilitador != 0)
                        {
                            if (conexion.abrirconexion() == true)
                                fa_disponible = Facilitadores.CoFacilitadorDisponibleDia(conexion.conexion, time.fecha_curso, fa.id_facilitador);

                            conexion.cerrarconexion();
                            if (fa_disponible != 0)//si retorna algo es que este facilitador ya está ocupado para esa fecha
                            {
                                MessageBox.Show("El facilitador está ocupado en la fecha seleccionada: " + time.fecha_curso + ".");
                                cmbxFa.SelectedIndex = -1;
                                fa.id_facilitador = 0;
                                cmbxFa.Focus();
                            }
                            else
                            {
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

                        errorProviderPresentacion.SetError(cmbxFa, "");
                        if (fa.id_facilitador != 0)
                        {
                            if (conexion.abrirconexion() == true)
                                fa_disponible = Facilitadores.CoFacilitadorDisponibleDia(conexion.conexion, time.fecha_curso, fa.id_facilitador);

                            conexion.cerrarconexion();
                            if (fa_disponible != 0)//si retorna algo es que este facilitador ya está ocupado para esa fecha
                            {
                                MessageBox.Show("El facilitador está ocupado en la fecha seleccionada: " + time.fecha_curso + ".");
                                cmbxFa.SelectedIndex = -1;
                                fa.id_facilitador = 0;
                                cmbxFa.Focus();
                            }
                            else
                            {
                                int fa_disponibleDia2 = 0;
                                if (conexion.abrirconexion() == true)
                                {
                                    fa_disponibleDia2 = Clases.Facilitadores.FacilitadorDisponibleDia2(conexion.conexion, time.fechaDos_curso, fa.id_facilitador);
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
                                        if (conexion.abrirconexion() == true)
                                            fa_disponible = Facilitadores.CoFacilitadorDisponibleDia2(conexion.conexion, time.fechaDos_curso, fa.id_facilitador);

                                        conexion.cerrarconexion();
                                        if (fa_disponible != 0)//si retorna algo es que este facilitador ya está ocupado para esa fecha
                                        {
                                            MessageBox.Show("El facilitador está ocupado en la fecha seleccionada: " + time.fechaDos_curso + ".");
                                            cmbxFa.SelectedIndex = -1;
                                            fa.id_facilitador = 0;
                                            cmbxFa.Focus();
                                        }
                                        else
                                        {
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
                    gpbSegundaFecha.Enabled = true;
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
        private void dtpFechaCurso_Validating(object sender, CancelEventArgs e)
        {
            if(dtpFechaCurso.Value <= DateTime.Today)
            {
                errorProviderFecha.SetError(dtpFechaCurso, "La fecha seleccionada es inválida.");
                dtpFechaCurso.Focus();
            }else
            {
                errorProviderFecha.SetError(dtpFechaCurso, "");
                time.fecha_curso = dtpFechaCurso.Value.ToString("yyyy-MM-dd"); ;
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
                                     
                    for (int i=0; i< nombre_publicidad.Count; i++)
                    {
                       
                        if (row.Cells["opcion_difusion"].Value.ToString()== nombre_publicidad[i].ToString())
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

       



        /*---------------------------Tercera etapa------------------------------*/
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
                    
                    string nombre = row.Cells["insumo"].Value.ToString();
                    lista_insumo.Add(nombre);

                   
                }
                else
                {


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


        private void rdbSiIgualHorario_CheckedChanged(object sender, EventArgs e)
        {
            //cmbxHorario2.SelectedIndex = -1;
            cmbxHorario2.Text = cmbxHorarios.Text;
            cmbxHorario2.Enabled = false;
            id_horario2 = id_horario;
        }

        private void rdbNoIgualHorario_CheckedChanged(object sender, EventArgs e)
        {
            string t;
            if (formacion.duracion=="8" && formacion.bloque_curso == "2")
            {
                t = "B";
                llenarComboHorario2(t, id_horario);
                               
            }else if(formacion.duracion == "16")
            {
                t = "A";
                llenarComboHorario2(t, id_horario);

            }
            cmbxHorario2.Enabled = true;
            cmbxHorario2.Focus();
        }

        private void rdbSiMantenerAula_CheckedChanged(object sender, EventArgs e)
        {
            txtSegundaAula.Text = txtAulaSeleccionada.Text;
            txtSegundaAula.Enabled = false;
        }

        private void rdbNoMantenerAula_CheckedChanged(object sender, EventArgs e)
        {
            txtSegundaAula.Enabled = true;
            txtSegundaAula.Focus();
        }

        private void rdbSiMantenerRef_CheckedChanged(object sender, EventArgs e)
        {
            cmbxTipoRefrigerio2.Enabled = false;
            cmbxTipoRefrigerio2.SelectedIndex = -1;
            cmbxTipoRefrigerio2.Text = cmbxTipoRefrigerio.Text;
            id_refrigerio2 = id_refrigerio;
        }

        private void rdbNoMantenerRef_CheckedChanged(object sender, EventArgs e)
        {
            cmbxTipoRefrigerio2.Enabled = true;
        }

        private void cmbxTipoRefrigerio2_SelectionChangeCommitted(object sender, EventArgs e)
        {
            id_refrigerio2 = Convert.ToInt32(cmbxTipoRefrigerio2.SelectedValue);
        }

        private void cmbxTipoRefrigerio_SelectionChangeCommitted(object sender, EventArgs e)
        {
            id_refrigerio = Convert.ToInt32(cmbxTipoRefrigerio.SelectedValue);
            llenarcombo2Refrigerio(id_refrigerio);
        }       

       
        private void cmbxHorarios_SelectionChangeCommitted(object sender, EventArgs e)
        {
            inicioE3 = DateTime.Now;
            id_horario = Convert.ToInt32(cmbxHorarios.SelectedValue);
        }
        private void cmbxHorario2_SelectionChangeCommitted(object sender, EventArgs e)
        {
            id_horario2= Convert.ToInt32(cmbxHorario2.SelectedValue);
        }

              
    }
}
