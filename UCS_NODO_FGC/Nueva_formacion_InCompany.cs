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
using System.IO;
using System.Diagnostics;
using iTextSharp;
using System.Drawing.Imaging;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Net.Mail;
using System.Net;

namespace UCS_NODO_FGC
{
    public partial class Nueva_formacion_InCompany : Form
    {
        #region VARIABLES
        Formaciones formacion = new Clases.Formaciones();
        Empresa cliente = new Empresa();
        conexion_bd conexion = new Clases.conexion_bd();
        Tiempos_curso time = new Clases.Tiempos_curso();
        Facilitadores fa = new Clases.Facilitadores();
        Facilitadores Cofa = new Clases.Facilitadores();
        Facilitadores faDatos = new Clases.Facilitadores();
        Paquete_instruccional p_inst = new Clases.Paquete_instruccional();
        List<Paquete_instruccional> pLista = new List<Clases.Paquete_instruccional>();
        List<string> lista_insumo = new List<string>(); //esta es la que se llena cuando se selecciona en el datagridview
        List<string> lista_insumo_cargada = new List<string>(); //esta es la lista de la base de datos
        List<int> lista_id = new List<int>();
        Curso_AFI AFI = new Curso_AFI();
        List<Facilitador_todos> lista = new List<Facilitador_todos>();

        bool guardar = false;
        string duracion = "";
        bool ExisteFormacion;
        string presentacion = "";
        string contenido = "";
        string bloques = "";
        string bitacora = "";
        string manual = "";
        int id_refrigerio, id_refrigerio2, id_horario, id_horario2;
        DateTime fecha_creacion, FinalE1, FinalE2, FinalE3, inicioE2, inicioE3;
        Facilitador_todos fcombo = new Facilitador_todos();
        Facilitador_todos cf = new Facilitador_todos();

        #endregion
        public Nueva_formacion_InCompany()
        {
            InitializeComponent();
            btnGuardar.Enabled = false;
        }

        private void Nueva_formacion_InCompany_Load(object sender, EventArgs e)
        {
            int i = 0;
            llenarcmbxEmpresas(i);
            conexion.cerrarconexion();
            if (conexion.abrirconexion() == true)
            {
                string difu = "";
                CargarDatosInsumos(conexion.conexion, difu);
                dgvInsumos.ClearSelection();
                conexion.cerrarconexion();
            }

            llenarcomboRefrigerio();

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

                
                //cargar los facilitadores del nivel_intermedio
                llenarcomboFacilitador();

                

            } else //si viene referenciado de modificar formacion            
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
                    if (AFI.id_AFI != 0) //si el curso está registrado: 
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
                else if(Cursos.etapa_formacion13 == 2)
                {
                    gpbCorreos.Enabled = true;
                    btnCorreoAdministracion.Enabled = true;
                    btnCorreoFacilitadores.Enabled = true;
                    btnRetomar.Enabled = false;
                    btnSiguienteEtapa.Enabled = true;
                    
                    CargarDatosEtapaUno();
                    
                    

                    if (Cursos.duracion_formacion13 == "16" || (Cursos.duracion_formacion13 == "8" && Cursos.bloque_curso13 == "1"))
                    {
                        string tipo = "A";
                        llenarComboHorario(tipo);
                    }
                    else if (Cursos.duracion_formacion13 == "4" || (Cursos.duracion_formacion13 == "8" && Cursos.bloque_curso13 == "2"))
                    {
                        string tipo = "B";
                        llenarComboHorario(tipo);
                    }

                    CargarDatosEtapaDos();

                }
                else if (Cursos.etapa_formacion13 == 3)
                {
                    btnRetomar.Enabled = false;
                    btnSiguienteEtapa.Enabled = true;
                    CargarDatosEtapaUno();

                    CargarDatosEtapaDos();
                    if ((Cursos.duracion_formacion13 == "16") || (Cursos.duracion_formacion13 == "8" && Cursos.bloque_curso13 == "1"))
                    {
                        string tipo = "A";
                        llenarComboHorario(tipo);
                    }
                    else if ((Cursos.duracion_formacion13 == "4") || (Cursos.duracion_formacion13 == "8" && Cursos.bloque_curso13 == "2"))
                    {
                        string tipo = "B";
                        llenarComboHorario(tipo);
                    }

                    CargarDatosEtapaTres();

                    
                    

                }
                //evaluarAFI(); //esto es para llenar el combobox con los facilitadores que pueden dar la formacion


            }
                
        }
        /*---------------------- METODOS ------------------------*/

        #region METODOS AUXILIARES
        private void llenarcomboRefrigerio()
        {
            //llenar el combobox de la tercera etapa con los tipos de refrigerios
            cmbxTipoRefrigerio.ValueMember = "id_ref";
            cmbxTipoRefrigerio.DisplayMember = "nombre";
            cmbxTipoRefrigerio.DataSource = Paneles.llenarcmbxRef();
            if(Formaciones.creacion==true)
                cmbxTipoRefrigerio.SelectedIndex = -1;
        }
        private void llenarcombo2Refrigerio(int id_ref)
        {
            //llenar el combobox de la tercera etapa con los tipos de refrigerios
            cmbxTipoRefrigerio2.ValueMember = "id_ref";
            cmbxTipoRefrigerio2.DisplayMember = "nombre";
            cmbxTipoRefrigerio2.DataSource = Paneles.llenarcmbx2Ref(id_ref);
            if (Formaciones.creacion == true)
                cmbxTipoRefrigerio2.SelectedIndex = -1;
        }        
        private void llenarComboHorario(string tipo)
        {
            //llenar el combobox con los horarios registrados en la BD
            cmbxHorarios.ValueMember = "id_horario";
            cmbxHorarios.DisplayMember = "contenido_horario";
            cmbxHorarios.DataSource = Horarios.llenarcmbxHorario(tipo);
            //cmbxHorarios.SelectedIndex = -1;
        }
        private void llenarComboHorario2(string tipo, int id)
        {
            //llenar el combobox con los horarios registrados en la BD
            cmbxHorario2.ValueMember = "id_horario";
            cmbxHorario2.DisplayMember = "contenido_horario";
            cmbxHorario2.DataSource = Horarios.llenarcmbxHorario2(tipo, id);
            cmbxHorario2.SelectedIndex = -1;
        }
        private void llenarcmbxEmpresas(int id)
        {
            MySqlDataReader SolicitadoPor = Conexion.ConsultarBD("SELECT nombre_empresa FROM clientes WHERE id_clientes !='" + id + "'");
            while (SolicitadoPor.Read())
            {
                cmbxSolicitadoPor.Items.Add(SolicitadoPor["nombre_empresa"]);
            }
            SolicitadoPor.Close();
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
        private void llenarComboCOFA_AFI(int idC, int idFa)
        {
            cmbxCoFa.ValueMember = "id_faci";
            cmbxCoFa.DisplayMember = "nombreyapellido1";
            cmbxCoFa.DataSource = listaCOFA_AFI(idC, idFa);
            cmbxCoFa.SelectedIndex = -1;
        }

        private static List<Facilitador_todos> listaCOFA_AFI(int id_curs, int idFA)
        {
            List<Facilitador_todos> lista = new List<Facilitador_todos>();
            MySqlDataReader cof = Conexion.ConsultarBD("select nombre_fa, apellido_fa from facilitadores fa inner join afi_tiene_facilitadores atf on atf.id_fa=fa.id_fa where atf.id_fa!= '" + idFA + "' AND atf.id_cursos_afi='" + id_curs + "'");
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
            gpbRefrigerio.Enabled = true;
            rdbNoRef.Checked = true;
            gpbFecha.Enabled = true;          
            gpbFacilitador.Enabled = false;
            gpbDatosFa.Enabled = false;
            chkbCoFacilitador.Enabled = false;
            gpbCoFa.Enabled = false;
            gpbDatosCoFa.Enabled = false;
            gpbInstalacion.Enabled = true;
        }
        private void ordenTerceraEtapa()
        {
            gpbHorarioCurso.Location = new Point(254, 47);
            gpbHorarioCurso.Height = 73;

            gpbSeleccionRef.Location = new Point(254, 132);
            gpbSeleccionRef.Enabled = false;

            gpbAula.Location = new Point(254, 215);
            gpbAula.Height = 155;
            gpbAula.Width = 419;

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

        private void deshabiltarControlesBasico()
        {
            //estado de los campos del nivel básico:
            txtNombreFormacion.Enabled = false;
            cmbxSolicitadoPor.Enabled = false;
            cmbxDuracionFormacion.Enabled = false;
            cmbxBloques.Enabled = false;

            btnRutaContenido.Enabled = false;
            btnRutaPresentacion.Enabled = false;
            btnRutaBitacora.Enabled = false;
            btnRutaManual.Enabled = false;

        }
        private void deshabilitarControlesIntermedio()
        {
            gpbInstalacion.Enabled = false;
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
            gpbAula.Enabled = false;
            gpbInsumos.Enabled = false;
        }
        private void habilitarIntermedio()
        {
            dtpFechaCurso.Enabled = true;
            
            if(Cursos.bloque_curso13 == "2")
            {
                dtpSegundaFecha.Enabled = true;
            }
            else
            {
                dtpSegundaFecha.Enabled = false;
            }
            gpbInstalacion.Enabled = true;
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
            gpbAula.Enabled = true;
            gpbInsumos.Enabled = true;
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
                cof.Close();
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

        #endregion

        #region CARGAR DATOS
        private void CargarDatosEtapaUno()
        {
            fa.id_facilitador = 0;
            formacion.nombre_formacion = Cursos.nombre_formacion13;
            formacion.solicitado = Cursos.solicitud_formacion13;
            formacion.id_curso = Cursos.id_curso13;
            formacion.bloque_curso = Cursos.bloque_curso13;

            deshabiltarControlesBasico();
            //carga el nombre
            txtNombreFormacion.Text = Cursos.nombre_formacion13;
            //carga quien solicita
            for (int a = 0; a < cmbxSolicitadoPor.Items.Count; a++)
            {
                if (cmbxSolicitadoPor.Items[a].ToString() == Cursos.solicitud_formacion13)
                {
                    cmbxSolicitadoPor.SelectedItem = cmbxSolicitadoPor.Items[a];
                }
            }
            //carga duración y bloques
            switch (Cursos.duracion_formacion13)
            {
                case "4 Horas":
                    cmbxDuracionFormacion.SelectedIndex = 0;
                    cmbxBloques.Items.Clear();
                    cmbxBloques.Items.Add("1");
                    cmbxBloques.SelectedIndex = 0;
                    formacion.duracion ="4";
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
            Cursos.duracion_formacion13 = formacion.duracion;

            //CARGAR DATOS DE PAQUETE INSTRUCCIONAL

            contenido = Cursos.p_contenido;
            p_inst.contenido = contenido;
            btnVerContenido.Enabled = true;
            if (Cursos.p_presentacion == "")
            {
                presentacion = Cursos.p_presentacion;
                p_inst.presentacion = presentacion;
                btnVerPresentacion.Enabled = false;
            }
            else
            {
                presentacion = Cursos.p_presentacion;
                p_inst.presentacion = presentacion;
                btnVerPresentacion.Enabled = true;
            }

            if (Cursos.p_bitacora == "")
            {
                bitacora = Cursos.p_bitacora;
                p_inst.bitacora = bitacora;
                btnVerBitacora.Enabled = false;
            }
            else
            {
                bitacora = Cursos.p_bitacora;
                p_inst.bitacora = bitacora;
                btnVerBitacora.Enabled = true;
            }

            if (Cursos.p_manual == "")
            {
                manual = Cursos.p_manual;
                p_inst.manual = manual;
                btnVerManual.Enabled = false;
            }
            else
            {
                manual = Cursos.p_manual;
                p_inst.manual = manual;
                btnVerManual.Enabled = true;
            }

            
        }
        private void CargarDatosEtapaDos()
        {
            fa.id_facilitador = 0;
            //llenar dato de ubicacion
            if (Cursos.ubicacion_ucs == "Si")
            {                
                rdbInstalaciones.Checked = true;
                formacion.ubicacion_ucs = "Si";
            } else
            {
                rdbNoInstalaciones.Checked = true;
                formacion.ubicacion_ucs = "No";
            }
            
            //llenar datos del refrigerio
            if (Cursos.tiene_ref == "Si")
            {
                rdbSiRef.Checked = true;
                formacion.tiene_ref = "Si";
            } else
            {
                rdbNoRef.Checked = true;
                formacion.tiene_ref = "No";
            }
            //llenar fechas
            dtpFechaCurso.Value = Convert.ToDateTime(Cursos.fecha_uno13);
            time.fecha_curso = Cursos.fecha_uno13;
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
            
            //buscar id_fa de acuerdo al id del curso
            MySqlDataReader leer = Conexion.ConsultarBD("SELECT * from cursos_tienen_fa where cursos_id_cursos = '" + Cursos.id_curso13 + "'");
            if (leer.Read())
            {
                id_fa = Convert.ToInt32(leer["facilitadores_id_fa"]);
                co_fa = Convert.ToInt32(leer["ctf_id_cofa"]);
            }
            leer.Close();

             time.fecha_curso = Cursos.fecha_uno13; 
            fa.id_facilitador = id_fa;
            
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

                //MessageBox.Show(fcombo.nombreyapellido1);
                //cmbxFa.Text = fcombo.nombreyapellido1;
                //txtCorreoFa.Text = fcombo.correo_facilitador;
                //txtTlfnFa.Text = fcombo.tlfn_facilitador;
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

            List<Facilitador_todos> lista_cf = new List<Facilitador_todos>();
            lista_cf = listaCOFA_AFI(id_curs, id_fa);
            for (int x = 0; x < lista_cf.Count; x++)
            {
                cmbxCoFa.Items.Add(lista_cf[x].nombreyapellido1);
            }

            //evaluar si esa formacion tiene co facilitador
            if (co_fa != 0)
            {
                
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
                //cmbxCoFa.Text = cf.nombreyapellido1;
                txtCorreoCoFa.Text = cf.correo_facilitador;
                txtTlfnCoFa.Text = cf.tlfn_facilitador;
                chkbCoFacilitador.Checked=true;

                Cofa.id_facilitador = co_fa;
                //recorremos el comboboxCOFa
                for (int i = 0; i < cmbxCoFa.Items.Count; i++)
                {
                    if (cmbxCoFa.Items[i].ToString() == cf.nombreyapellido1)
                    {
                        cmbxCoFa.SelectedItem = cmbxCoFa.Items[i];
                        txtCorreoCoFa.Text = cf.correo_facilitador;
                        txtTlfnCoFa.Text = cf.tlfn_facilitador;
                    }
                }
            }
            else
            {
                chkbCoFacilitador.Checked = false;
                //cf.nombreyapellido1 = "";
                //cmbxCoFa.Text = "";
            }


            time.fecha_curso = Cursos.fecha_uno13;
            fa.id_facilitador = id_fa;




        }
        private void CargarDatosEtapaTres()
        {
                     
           
            cmbxHorarios.Text = Cursos.horario1;
            deshabilitarControlesAvanzado();
            
            
            txtAula.Text = Cursos.aula1;
           
            if (Cursos.tiene_ref == "Si")
            {
                
                cmbxTipoRefrigerio.Text = Cursos.tipo_ref1;
                formacion.refri1 = Cursos.tipo_ref1;
                MySqlDataReader nombre = Conexion.ConsultarBD("SELECT id_ref from refrigerios where ref_nombre='" + cmbxTipoRefrigerio.Text + "'");
                if (nombre.Read())
                {
                  //  MessageBox.Show(nombre["id_ref"].ToString());
                    id_refrigerio = Convert.ToInt32(nombre["id_ref"]);
                }
                nombre.Close();

                if (Cursos.tipo_ref2 != "No aplica")
                {
                    cmbxTipoRefrigerio2.Text = Cursos.tipo_ref2;
                    formacion.refri2 = Cursos.tipo_ref2;
                    MySqlDataReader nombre2 = Conexion.ConsultarBD("SELECT id_ref from refrigerios where ref_nombre='" + cmbxTipoRefrigerio2.Text + "'");
                    if (nombre2.Read())
                    {
                        //MessageBox.Show(nombre2["id_ref"].ToString());
                        id_refrigerio2 = Convert.ToInt32(nombre2["id_ref"]);
                    }
                    nombre2.Close();
                }
                else
                {
                    formacion.refri2 = "";
                }
               

            }
            else
            {
                cmbxTipoRefrigerio.SelectedIndex = -1;
                cmbxTipoRefrigerio2.SelectedIndex = -1;
                formacion.refri1 = "";
                formacion.refri2 = "";
            }

            //buscar el id del horario1:
            MySqlDataReader h1 = Conexion.ConsultarBD("SELECT idhorarios from horarios where horario='"+ Cursos.horario1 + "'");
            if (h1.Read())
            {
                id_horario = Convert.ToInt32(h1["idhorarios"]);
            }
            h1.Close();
            //si la formacion tiene dos bloques
            if (Cursos.bloque_curso13 == "2")
            {
                //buscar el id del horario2:
                MySqlDataReader h2 = Conexion.ConsultarBD("SELECT idhorarios from horarios where horario='" + Cursos.horario2 + "'");
                if (h2.Read())
                {
                    id_horario2 = Convert.ToInt32(h2["idhorarios"]);
                }
                h2.Close();

                cmbxHorarios.Text = Cursos.horario1;
                formacion.horario1 = Cursos.horario1;
                //si el horario2 es igual al horario1
                if (Cursos.horario2 == Cursos.horario1)
                {
                    rdbSiIgualHorario.Checked = true;
                    cmbxHorario2.Text = Cursos.horario2;
                    formacion.horario2 = Cursos.horario2;
                }else
                {
                    cmbxHorarios.Text = Cursos.horario1;
                    rdbNoIgualHorario.Checked = true;
                    cmbxHorario2.Text = Cursos.horario2;
                    formacion.horario2 = Cursos.horario2;
                }
                //si el aula2 es igual al aula1
                if(Cursos.aula2 == Cursos.aula1)
                {
                    txtSegundaAula.Text = Cursos.aula2;
                    rdbSiMantenerAula.Checked = true;
                }else
                {
                    txtSegundaAula.Text = Cursos.aula2;
                    rdbNoMantenerAula.Checked = true;
                }
                formacion.aula1 = Cursos.aula1;
                formacion.aula2 = Cursos.aula2;
            }else
            {
                formacion.horario1 = Cursos.horario1;
                
                cmbxHorario2.SelectedIndex = -1;
                txtSegundaAula.Text = Cursos.aula2;
                rdbSiIgualHorario.Checked = false;
                rdbNoIgualHorario.Checked = false;
                rdbSiMantenerAula.Checked = false;
                rdbNoMantenerAula.Checked = false;
            }
            

          //  MessageBox.Show(cmbxHorarios.Text + " :Horario1" + txtAula.Text + " :aula ");

            //buscar todos los insumos registrados para esa formacion
            MySqlDataReader ins = Conexion.ConsultarBD("SELECT * FROM insumos i inner join cursos_tienen_insumos cti on cti.cti_id_insumo = i.id_insumos where cti.cti_id_curso = '" + Cursos.id_curso13 + "'");
            while (ins.Read())
            {
                string insumo = Convert.ToString(ins["ins_contenido"]);
                lista_insumo_cargada.Add(insumo);
            }
            ins.Close();
            //recorrer el datagridview y buscar igualdades para luego marcar el check
            for (int i = 0; i < lista_insumo_cargada.Count; i++) 
            {
                foreach (DataGridViewRow row in dgvInsumos.Rows)
                {
                    string celda = Convert.ToString(row.Cells["insumo"].Value);
                    if (celda == lista_insumo_cargada[i])
                    {
                        row.Cells["seleccion_opcion"].Value = true;
                        lista_insumo.Add(celda);
                    }
                }
            }
           // lista_insumo = lista_insumo_cargada; //se igualan las listas
        }

        #endregion

        #region GUARDAR
        private void GuardarBasico()
        {
            try
            {
                formacion.tiene_ref = "0";
                formacion.ubicacion_ucs = "Si"; //es SI hasta que se actualice en la etapa dos o nivel intermedio
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
                                    string ruta = @"C:\Users\ZM\Documents\Last_repo\ucs_sistema\UCS_NODO_FGC\Archivos\Paquete_instruccional\";

                                    //manual = manual.Replace("\\", "/");
                                    //bitacora = bitacora.Replace("\\", "/");
                                    //presentacion = presentacion.Replace("\\", "/");
                                    //contenido = contenido.Replace("\\", "/");

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
                                        conexion.cerrarconexion();
                                        if (conexion.abrirconexion() == true)
                                            p_inst.id_pinstruccional = Clases.Formaciones.ObtenerIdPaquete(conexion.conexion, p_inst);
                                        conexion.cerrarconexion();

                                        if (p_inst.id_pinstruccional == 0)
                                        {

                                            int resultado2 = 0;

                                            contenido = p_inst.contenido;
                                            string nombrearchivo = Path.GetFileName(p_inst.contenido);
                                            p_inst.contenido = Path.Combine(ruta, nombrearchivo);  //actualizando ruta del archivo
                                            File.WriteAllBytes(p_inst.contenido, Helper.DocToByteArray(contenido)); //escribiendo el archivo en la carpeta de respaldo

                                            
                                            if (p_inst.presentacion != "")
                                            {
                                                presentacion = p_inst.presentacion;
                                                nombrearchivo = Path.GetFileName(presentacion);
                                                p_inst.presentacion = Path.Combine(ruta, nombrearchivo); //actualizando ruta del archivo
                                                File.WriteAllBytes(p_inst.presentacion, Helper.DocToByteArray(presentacion)); //escribiendo el archivo en la carpeta de respaldo
                                            }

                                            if(p_inst.manual != "")
                                            {
                                                manual = p_inst.manual;
                                                nombrearchivo = Path.GetFileName(manual);
                                                p_inst.manual = Path.Combine(ruta, nombrearchivo); //actualizando ruta del archivo
                                                File.WriteAllBytes(p_inst.manual, Helper.DocToByteArray(manual)); //escribiendo el archivo en la carpeta de respaldo

                                            }

                                            if(p_inst.bitacora != "")
                                            {
                                                bitacora = p_inst.bitacora;
                                                nombrearchivo = Path.GetFileName(bitacora);
                                                p_inst.bitacora = Path.Combine(ruta, nombrearchivo); //actualizando ruta del archivo
                                                File.WriteAllBytes(p_inst.bitacora, Helper.DocToByteArray(bitacora)); //escribiendo el archivo en la carpeta de respaldo

                                            }

                                            conexion.cerrarconexion();
                                            if (conexion.abrirconexion() == true)
                                            {

                                                resultado2 = Formaciones.GuardarPaqueteInstruccional(conexion.conexion, p_inst);
                                                conexion.cerrarconexion();
                                            }
                                            int id_paquete = 0;


                                            if (resultado2 != 0)//si se guardó con éxito: recoger el id de ese paquete.
                                            {
                                                conexion.cerrarconexion();
                                                if (conexion.abrirconexion() == true)
                                                    id_paquete = Clases.Formaciones.ObtenerIdPaquete(conexion.conexion, p_inst);

                                                conexion.cerrarconexion();

                                                if (id_paquete != 0)//se obtiene un id_intruccional cooncordante con el archivo subido
                                                {
                                                    formacion.pq_inst = id_paquete;
                                                    int resultado = 0;
                                                    conexion.cerrarconexion();
                                                    if (conexion.abrirconexion() == true)
                                                    {

                                                        resultado = Clases.Formaciones.AgregarNuevaFormacion(conexion.conexion, formacion);

                                                        conexion.cerrarconexion();
                                                    }


                                                    if (resultado > 0 && resultado2 > 0)
                                                    {
                                                        int id_curso = 0;
                                                        conexion.cerrarconexion();
                                                        if (conexion.abrirconexion() == true)
                                                        {
                                                            //esto es para recoger el id del curso que se acaba de agregar (ignorar el nombre del método)
                                                            id_curso = Clases.Formaciones.CursoEjecucionExiste(conexion.conexion, formacion);
                                                            conexion.cerrarconexion();
                                                        }

                                                        if (id_curso != 0)
                                                        {
                                                            formacion.id_curso = id_curso;
                                                            conexion.cerrarconexion();
                                                            if (conexion.abrirconexion() == true)
                                                            {
                                                                int agregarUGC = Clases.Formaciones.Agregar_U_g_C(conexion.conexion, id_curso, formacion.id_user, fecha_creacion, FinalE1);
                                                                conexion.cerrarconexion();
                                                                MySqlDataReader clientes_solicitan_cursos = Conexion.ConsultarBD("INSERT INTO clientes_solicitan_cursos (id_cliente1, id_curso1) VALUES ('" + cliente.id_clientes + "', '" + id_curso + "')");
                                                                clientes_solicitan_cursos.Close();
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
                                        else
                                        {
                                            VerPaqueteInst(p_inst.id_pinstruccional);
                                            int resultado = 0;
                                            conexion.cerrarconexion();
                                            if (conexion.abrirconexion() == true)
                                            {

                                                resultado = Clases.Formaciones.AgregarNuevaFormacion(conexion.conexion, formacion);

                                                conexion.cerrarconexion();
                                            }

                                            if (resultado > 0)
                                            {
                                                int id_curso = 0;
                                                conexion.cerrarconexion();
                                                if (conexion.abrirconexion() == true)
                                                {
                                                    //esto es para recoger el id del curso que se acaba de agregar (ignorar el nombre del método)
                                                    id_curso = Clases.Formaciones.CursoEjecucionExiste(conexion.conexion, formacion);
                                                    conexion.cerrarconexion();
                                                }

                                                if (id_curso != 0)
                                                {
                                                    formacion.id_curso = id_curso;
                                                    conexion.cerrarconexion();
                                                    if (conexion.abrirconexion() == true)
                                                    {
                                                        int agregarUGC = Clases.Formaciones.Agregar_U_g_C(conexion.conexion, id_curso, formacion.id_user, fecha_creacion, FinalE1);
                                                        conexion.cerrarconexion();

                                                        MySqlDataReader clientes_solicitan_cursos = Conexion.ConsultarBD("INSERT INTO clientes_solicitan_cursos (id_cliente1, id_curso1) VALUES ('" + cliente.id_clientes + "', '" + id_curso + "')");
                                                        clientes_solicitan_cursos.Close();
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
                                    else //if formacionExiste==true
                                    {
                                        int resultado = 0;
                                        conexion.cerrarconexion();
                                        if (conexion.abrirconexion() == true)
                                        {

                                            resultado = Clases.Formaciones.AgregarNuevaFormacion(conexion.conexion, formacion);

                                            conexion.cerrarconexion();
                                        }

                                        if (resultado > 0)
                                        {
                                            int id_curso = 0;
                                            conexion.cerrarconexion();
                                            if (conexion.abrirconexion() == true)
                                            {
                                                //esto es para recoger el id del curso que se acaba de agregar (ignorar el nombre del método)
                                                id_curso = Clases.Formaciones.CursoEjecucionExiste(conexion.conexion, formacion);
                                                conexion.cerrarconexion();
                                            }

                                            if (id_curso != 0)
                                            {
                                                formacion.id_curso = id_curso;
                                                conexion.cerrarconexion();
                                                if (conexion.abrirconexion() == true)
                                                {
                                                    int agregarUGC = Clases.Formaciones.Agregar_U_g_C(conexion.conexion, id_curso, formacion.id_user, fecha_creacion, FinalE1);
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
                int id_curso = 0;
                if (Formaciones.creacion == true)
                {
                    //se obtiene el id del curso
                    MySqlDataReader obtener_id_curso = Conexion.ConsultarBD("SELECT id_cursos FROM cursos WHERE nombre_curso = '" + txtNombreFormacion.Text + "' AND tipo_curso='InCompany'  AND estatus_curso='En curso' AND duracion_curso=" + formacion.duracion + " AND id_usuario1='" + formacion.id_user + "' and solicitud_curso='" + cmbxSolicitadoPor.Text + "'");
                    if (obtener_id_curso.Read())
                    {
                        id_curso = int.Parse(obtener_id_curso["id_cursos"].ToString());
                    }
                    obtener_id_curso.Close();

                }
                else
                {
                    id_curso = Cursos.id_curso13;
                    
                }

                formacion.id_curso = id_curso;

                if (formacion.duracion == "4")
                {
                    if (rdbInstalaciones.Checked == false && rdbNoInstalaciones.Checked == false)
                    {
                        errorProviderBloque.SetError(gpbInstalacion, "Debe especificar dónde será la formación.");
                    }
                    else
                    {
                        errorProviderBloque.SetError(gpbInstalacion, "");

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
                                    
                                    time.fecha_curso = dtpFechaCurso.Value.ToString("yyyy-MM-dd");

                                    formacion.bloque_curso = "1";

                                    if (rdbSiRef.Checked == false)
                                    {
                                        gpbSeleccionRef.Enabled = false;
                                        gpbSeleccionRef.Visible = true;
                                        MySqlDataReader ActualizarCursoTieneRef = Conexion.ConsultarBD("UPDATE cursos SET tiene_ref='0' WHERE id_cursos='" + id_curso + "'");
                                        ActualizarCursoTieneRef.Close();
                                    }
                                    else
                                    {


                                        gpbSeleccionRef.Enabled = true;
                                        gpbSeleccionRef.Visible = true;
                                        MySqlDataReader ActualizarCursoTieneRef = Conexion.ConsultarBD("UPDATE cursos SET tiene_ref='1' WHERE id_cursos='" + id_curso + "'");
                                        ActualizarCursoTieneRef.Close();
                                    }

                                    //SI SE HACE EN LA UCS ENTONCES
                                    if (rdbInstalaciones.Checked == true)
                                    {
                                        MySqlDataReader update = Conexion.ConsultarBD("UPDATE cursos SET ubicacion_ucs='Si' WHERE id_cursos='" + id_curso + "'");
                                        update.Close();
                                        gpbSeleccionRef.Enabled = true;
                                    }
                                    else  //SI NO SE HACE EN LA UCS
                                    {
                                        MySqlDataReader update = Conexion.ConsultarBD("UPDATE cursos SET ubicacion_ucs='No' WHERE id_cursos='" + id_curso + "'");
                                        update.Close();
                                        gpbAula.Enabled = false;
                                        gpbSeleccionRef.Enabled = false;
                                    }

                                    //se actualiza el curso con los datos obtenidos de la segunda etapa, como las fechas
                                    MySqlDataReader ActualizarCursoSegundaEtapa = Conexion.ConsultarBD("UPDATE cursos SET etapa_curso ='2', fecha_uno='" + time.fecha_curso + "', bloque_curso= '" + formacion.bloque_curso + "' WHERE id_cursos='" + id_curso + "'");
                                    ActualizarCursoSegundaEtapa.Close();

                                    GuardarAfiE2();

                                    //la parte de añadir los cursos_tienen_fa está en el evento de guardar en etpa2
                                    MessageBox.Show("Los datos se han agregado correctamente.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                    //aqui estaran contemplados los arreglos (visuales y de contenido) de la siguiente etapa
                                    ordenTerceraEtapa();
                                    //llenar el cmbx de horarios:
                                   
                                    string tipo = "B";
                                    llenarComboHorario(tipo);

                                    //para la logistica:
                                    rdbSiMantenerAula.Enabled = false;
                                    rdbNoMantenerAula.Enabled = false;
                                    txtSegundaAula.Enabled = false;
                                    rdbNoIgualHorario.Enabled = false;
                                    rdbSiIgualHorario.Enabled = false;
                                    gpbSeleccionRef.Enabled = true;
                                }
                            }
                        }
                    }
                    
                }
                else if (formacion.duracion == "8" && formacion.bloque_curso == "2")
                {
                    if (rdbInstalaciones.Checked == false && rdbNoInstalaciones.Checked == false)
                    {
                        errorProviderBloque.SetError(gpbInstalacion, "Debe especificar dónde será la formación.");
                    }else
                    {
                        errorProviderBloque.SetError(gpbInstalacion, "");
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
                            }
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

                                    time.fecha_curso = dtpFechaCurso.Value.ToString("yyyy-MM-dd");
                                    time.fechaDos_curso = dtpSegundaFecha.Value.ToString("yyyy-MM-dd");
                                                                        

                                    formacion.bloque_curso = "2";
                                    
                                    rdbNoIgualHorario.Enabled = true;
                                    rdbSiIgualHorario.Enabled = true;

                                    if (rdbSiRef.Checked == false)
                                    {
                                        gpbSeleccionRef.Enabled = false;
                                        gpbSeleccionRef.Visible = true;
                                        MySqlDataReader ActualizarCursoTieneRef = Conexion.ConsultarBD("UPDATE cursos SET tiene_ref='0' WHERE id_cursos='" + id_curso + "'");
                                        ActualizarCursoTieneRef.Close();
                                    }
                                    else
                                    {


                                        gpbSeleccionRef.Enabled = true;
                                        gpbSeleccionRef.Visible = true;
                                        MySqlDataReader ActualizarCursoTieneRef = Conexion.ConsultarBD("UPDATE cursos SET tiene_ref='1' WHERE id_cursos='" + id_curso + "'");
                                        ActualizarCursoTieneRef.Close();
                                    }

                                    //SI SE HACE EN LA UCS ENTONCES
                                    if (rdbInstalaciones.Checked == true)
                                    {
                                        MySqlDataReader update = Conexion.ConsultarBD("UPDATE cursos SET ubicacion_ucs='Si' WHERE id_cursos='" + id_curso + "'");
                                        update.Close();
                                        gpbAula.Enabled = true;
                                        gpbSeleccionRef.Enabled = true;
                                    }
                                    else //SI NO SE HACE EN LA UCS
                                    {
                                        MySqlDataReader update = Conexion.ConsultarBD("UPDATE cursos SET ubicacion_ucs='No' WHERE id_cursos='" + id_curso + "'");
                                        update.Close();
                                        gpbAula.Enabled = false;
                                        gpbSeleccionRef.Enabled = false;
                                    }

                                    //se actualiza el curso con los datos obtenidos de la segunda etapa, como las fechas
                                    MySqlDataReader ActualizarCursoSegundaEtapa = Conexion.ConsultarBD("UPDATE cursos SET etapa_curso ='2', fecha_uno='" + time.fecha_curso + "',fecha_dos='" + time.fechaDos_curso + "', bloque_curso= '" + formacion.bloque_curso + "' WHERE id_cursos='" + id_curso + "'");
                                    ActualizarCursoSegundaEtapa.Close();

                                    GuardarAfiE2();

                                    //la parte de añadir los cursos_tienen_fa está en el evento de guardar en etpa2
                                    MessageBox.Show("Los datos se han agregado correctamente.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    //aqui estaran contemplados los arreglos (visuales y de contenido) de la siguiente etapa
                                    ordenTerceraEtapa();
                                    gpbHorarioCurso.Height = 169;
                                    gpbSeleccionRef.Visible = true;
                                    //llenar el cmbx de horarios:
                                   // cmbxHorarios.Items.Clear();
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
                else if (formacion.duracion == "8" && formacion.bloque_curso == "1")
                {
                    if (rdbInstalaciones.Checked == false && rdbNoInstalaciones.Checked==false)
                    {
                        errorProviderBloque.SetError(gpbInstalacion, "Debe especificar dónde será la formación.");
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

                                    time.fecha_curso = dtpFechaCurso.Value.ToString("yyyy-MM-dd");                                    
                                   

                                   

                                    formacion.bloque_curso = "1";

                                    //llenar el cmbx de horarios:
                                    //cmbxHorarios.Items.Clear();
                                    string tipo = "A";
                                    llenarComboHorario(tipo);

                                    if (rdbInstalaciones.Checked == true)
                                    {
                                        MySqlDataReader update = Conexion.ConsultarBD("UPDATE cursos SET ubicacion_ucs='Si' WHERE id_cursos='" + id_curso + "'");
                                        update.Close();
                                        //para la logistica:
                                        rdbSiMantenerAula.Enabled = false;
                                        rdbNoMantenerAula.Enabled = false;
                                        txtSegundaAula.Enabled = false;

                                        rdbSiMantenerAula.Visible = true;
                                        rdbNoMantenerAula.Visible= true;

                                        rdbNoIgualHorario.Visible = true;
                                        rdbSiIgualHorario.Visible = true;
                                        rdbNoIgualHorario.Enabled = false;
                                        rdbSiIgualHorario.Enabled = false;
                                        if (rdbSiRef.Checked == false && rdbNoRef.Checked == false)
                                        {
                                            errorProviderBloque.SetError(gpbRefrigerio, "Debe especificar si la formación poseerá refrigerio.");
                                        }
                                        else
                                        {
                                            if (rdbSiRef.Checked == false)
                                            {
                                                gpbSeleccionRef.Enabled = false;
                                                gpbSeleccionRef.Visible = true;
                                                MySqlDataReader ActualizarCursoTieneRef = Conexion.ConsultarBD("UPDATE cursos SET tiene_ref='0' WHERE id_cursos='" + id_curso + "'");
                                                ActualizarCursoTieneRef.Close();
                                            }
                                            else
                                            {
                                                

                                                gpbSeleccionRef.Enabled = true;
                                                gpbSeleccionRef.Visible = true;
                                                MySqlDataReader ActualizarCursoTieneRef = Conexion.ConsultarBD("UPDATE cursos SET tiene_ref='1' WHERE id_cursos='" + id_curso + "'");
                                                ActualizarCursoTieneRef.Close();
                                            }

                                            //se actualiza el curso con los datos obtenidos de la segunda etapa, como las fechas
                                            MySqlDataReader ActualizarCursoSegundaEtapa = Conexion.ConsultarBD("UPDATE cursos SET etapa_curso ='2', fecha_uno='" + time.fecha_curso + "', bloque_curso= '" + formacion.bloque_curso + "' WHERE id_cursos='" + id_curso + "'");
                                            ActualizarCursoSegundaEtapa.Close();

                                            GuardarAfiE2();

                                            //la parte de añadir los cursos_tienen_fa está en el evento de guardar en etpa2
                                            MessageBox.Show("Los datos se han agregado correctamente.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                            //aqui estaran contemplados los arreglos (visuales y de contenido) de la siguiente etapa
                                            ordenTerceraEtapa();


                                        }
                                    }
                                    else //SI LA FORMACION NO SE DICTARÁ EN LA UCS
                                    {
                                        MySqlDataReader update = Conexion.ConsultarBD("UPDATE cursos SET ubicacion_ucs='No' WHERE id_cursos='" + id_curso + "'");
                                        update.Close();
                                        ordenTerceraEtapa();
                                        gpbSeleccionRef.Enabled = false;
                                        gpbAula.Enabled = false;
                                        gpbHorarioCurso.Height = 169;
                                        MySqlDataReader ActualizarCursoTieneRef = Conexion.ConsultarBD("UPDATE cursos SET tiene_ref='0' WHERE id_cursos='" + id_curso + "'");
                                        ActualizarCursoTieneRef.Close();
                                        //se actualiza el curso con los datos obtenidos de la segunda etapa, como las fechas
                                        MySqlDataReader ActualizarCursoSegundaEtapa = Conexion.ConsultarBD("UPDATE cursos SET etapa_curso ='2', fecha_uno='" + time.fecha_curso + "', bloque_curso= '" + formacion.bloque_curso + "' WHERE id_cursos='" + id_curso + "'");
                                        ActualizarCursoSegundaEtapa.Close();

                                        GuardarAfiE2();

                                        //la parte de añadir los cursos_tienen_fa está en el evento de guardar en etpa2
                                        MessageBox.Show("Los datos se han agregado correctamente.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                    }
                                }
                           }
                        }
                    }
                }
                else if (formacion.duracion == "16")
                {
                    if (rdbInstalaciones.Checked == false && rdbNoInstalaciones.Checked==false)
                    {
                        errorProviderBloque.SetError(gpbInstalacion, "Debe especificar dónde será la formación.");
                    }else
                    {
                        errorProviderBloque.SetError(gpbInstalacion, "");
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
                            }
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

                                    time.fecha_curso = dtpFechaCurso.Value.ToString("yyyy-MM-dd");
                                    time.fechaDos_curso = dtpSegundaFecha.Value.ToString("yyyy-MM-dd");
                                    
                                    

                                    formacion.bloque_curso = "2";

                                    //llenar el cmbx de horarios:
                                    //cmbxHorarios.Items.Clear();
                                    string tipo = "A";
                                    llenarComboHorario(tipo);


                                    if (rdbInstalaciones.Checked == true)
                                    {
                                        MySqlDataReader update = Conexion.ConsultarBD("UPDATE cursos SET ubicacion_ucs='Si' WHERE id_cursos='" + id_curso + "'");
                                        update.Close();
                                        //para la logistica:
                                        rdbSiMantenerAula.Enabled = true;
                                        rdbNoMantenerAula.Enabled = true;
                                        txtSegundaAula.Enabled = false;
                                        gpbAula.Enabled = true;
                                        rdbMantenerRef.Enabled = true;
                                        rdbNoMantenerRef.Enabled = true;

                                        if (rdbSiRef.Checked == false && rdbNoRef.Checked == false)
                                        {
                                            errorProviderBloque.SetError(gpbRefrigerio, "Debe especificar si la formación poseerá refrigerio.");
                                        }else
                                        {
                                            if (rdbSiRef.Checked == false)
                                            {
                                                gpbSeleccionRef.Enabled = false;
                                                gpbSeleccionRef.Visible = true;
                                                MySqlDataReader ActualizarCursoTieneRef = Conexion.ConsultarBD("UPDATE cursos SET tiene_ref='0' WHERE id_cursos='" + id_curso + "'");
                                                ActualizarCursoTieneRef.Close();
                                            }
                                            else
                                            {
                                               
                                                gpbSeleccionRef.Enabled = true;
                                                gpbSeleccionRef.Visible = true;
                                                MySqlDataReader ActualizarCursoTieneRef = Conexion.ConsultarBD("UPDATE cursos SET tiene_ref='1' WHERE id_cursos='" + id_curso + "'");
                                                ActualizarCursoTieneRef.Close();
                                            }

                                            ////se actualiza la informacion del curso con los valores nuevos: 
                                            //se actualiza el curso con los datos obtenidos de la segunda etapa, como las fechas
                                            MySqlDataReader ActualizarCursoSegundaEtapa = Conexion.ConsultarBD("UPDATE cursos SET etapa_curso ='2', fecha_uno='" + time.fecha_curso + "',fecha_dos='" + time.fechaDos_curso + "', bloque_curso= '" + formacion.bloque_curso + "' WHERE id_cursos='" + id_curso + "'");
                                            ActualizarCursoSegundaEtapa.Close();

                                            GuardarAfiE2();

                                            //la parte de añadir los cursos_tienen_fa está en el evento de guardar en etpa2
                                            MessageBox.Show("Los datos se han agregado correctamente.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                            if (rdbSiRef.Checked == false)
                                            {
                                                ordenTerceraEtapa();
                                                gpbSeleccionRef.Enabled = false;
                                               
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
                                                rdbMantenerRef.Visible = true;
                                                rdbNoMantenerRef.Enabled = true;
                                                rdbMantenerRef.Enabled = true;
                                            }
                                          

                                          

                                        }
                                    }
                                    else //SI LA FORMACION NO SE DICTARÁ EN LA UCS
                                    {
                                        MySqlDataReader update = Conexion.ConsultarBD("UPDATE cursos SET ubicacion_ucs='No' WHERE id_cursos='" + id_curso + "'");
                                        update.Close();
                                        ordenTerceraEtapa();
                                        gpbSeleccionRef.Enabled = false;
                                        rdbNoIgualHorario.Enabled = true;
                                        rdbSiIgualHorario.Enabled = true;
                                        gpbAula.Enabled = false;                                  
                                        gpbHorarioCurso.Height = 169;
                                        MySqlDataReader ActualizarCursoTieneRef = Conexion.ConsultarBD("UPDATE cursos SET tiene_ref='0' WHERE id_cursos='" + id_curso + "'");
                                        ActualizarCursoTieneRef.Close();
                                        //se actualiza el curso con los datos obtenidos de la segunda etapa, como las fechas
                                        MySqlDataReader ActualizarCursoSegundaEtapa = Conexion.ConsultarBD("UPDATE cursos SET etapa_curso ='2', fecha_uno='" + time.fecha_curso + "',fecha_dos='" + time.fechaDos_curso + "', bloque_curso= '" + formacion.bloque_curso + "' WHERE id_cursos='" + id_curso + "'");
                                        ActualizarCursoSegundaEtapa.Close();

                                        GuardarAfiE2();

                                        //la parte de añadir los cursos_tienen_fa está en el evento de guardar en etpa2
                                        MessageBox.Show("Los datos se han agregado correctamente.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information);

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
                int id_curso = 0;
                if (Formaciones.creacion == true)
                {
                    //se obtiene el id del curso
                    MySqlDataReader obtener_id_curso = Conexion.ConsultarBD("SELECT id_cursos FROM cursos WHERE nombre_curso = '" + txtNombreFormacion.Text + "' AND tipo_curso='InCompany'  AND estatus_curso='En curso' AND duracion_curso=" + formacion.duracion + " AND id_usuario1='" + formacion.id_user + "' and solicitud_curso='" + cmbxSolicitadoPor.Text + "'");
                    if (obtener_id_curso.Read())
                    {
                        id_curso = int.Parse(obtener_id_curso["id_cursos"].ToString());
                    }
                    obtener_id_curso.Close();

                }
                else
                {
                    id_curso = Cursos.id_curso13;
                   
                }

                formacion.id_curso = id_curso;

                bool ubicacion=true;

                MySqlDataReader ub = Conexion.ConsultarBD("SELECT ubicacion_ucs FROM cursos WHERE id_cursos='" + id_curso + "'");
                if (ub.Read())
                {
                    string a = ub.GetString(0);
                    switch (a)
                    {
                        case "Si":
                            ubicacion = true;
                            break;
                        case "No":
                            ubicacion = false;
                            break;
                    }
                }
                ub.Close();

               

                if (formacion.duracion == "4")
                {
                    if (cmbxHorarios.SelectedIndex == -1)
                    {
                        errorProviderHora.SetError(cmbxHorarios, "Debe seleccionar uno de los horarios disponibles.");
                        cmbxHorarios.Focus();
                    }
                    else
                    {
                        errorProviderHora.SetError(cmbxHorarios, "");
                        
                        if (lista_insumo.Count <= 0)
                        {
                            errorProviderContenido.SetError(dgvInsumos, "Debe seleccionar los insumos que se usarán durante la formación.");

                        }
                        else
                        {
                            errorProviderContenido.SetError(dgvInsumos, "");

                                                     

                            List<Insumos> insumos_curso = new List<Insumos>();
                            
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

                            string aula = "";
                            if (ubicacion == true) //SI SE DICTA EN UCS
                            {
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
                                    aula = txtAula.Text;
                                }
                                if (id_refrigerio > 0)
                                {
                                    MySqlDataReader ActualizarCursoTerceraEtapa = Conexion.ConsultarBD("UPDATE cursos SET etapa_curso ='3', horario_uno='" + id_horario + "', aula_dia1='" + aula + "', id_ref1='" + id_refrigerio + "' WHERE id_cursos='" + id_curso + "'");
                                    ActualizarCursoTerceraEtapa.Close();

                                    MySqlDataReader refri = Conexion.ConsultarBD("insert into cursos_tienen_refrigerios (cursos_id_cursos, refrigerios_id_ref) values ('" + id_curso + "', '" + id_refrigerio + "')");
                                    refri.Close();
                                }
                                else
                                {
                                    MySqlDataReader ActualizarCursoTerceraEtapa = Conexion.ConsultarBD("UPDATE cursos SET etapa_curso ='3',  horario_uno='" + id_horario + "', aula_dia1='" + aula + "' WHERE id_cursos='" + id_curso + "'");
                                    ActualizarCursoTerceraEtapa.Close();
                                }

                                MessageBox.Show("Los datos se han agregado correctamente.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.None);
                            }
                            else//si no se dicta en la uics
                            {
                                //se actualiza el curso con los datos obtenidos de la segunda etapa, como las fechas
                                MySqlDataReader ActualizarCursoTerceraEtapa = Conexion.ConsultarBD("UPDATE cursos SET etapa_curso ='3', horario_uno='" + id_horario + "' WHERE id_cursos='" + id_curso + "'");
                                ActualizarCursoTerceraEtapa.Close();
                                MessageBox.Show("Los datos se han agregado correctamente.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.None);

                            }

                        }
                    }
                }
                else if (formacion.duracion == "8" && formacion.bloque_curso == "2")
                {
                    if (cmbxHorarios.SelectedIndex == -1)
                    {
                        errorProviderHora.SetError(cmbxHorarios, "Debe seleccionar uno de los horarios disponibles.");
                        cmbxHorarios.Focus();
                    }
                    else
                    {
                        errorProviderHora.SetError(cmbxHorarios, "");
                        if (rdbNoIgualHorario.Checked && cmbxHorario2.SelectedIndex == -1)
                        {
                            errorProviderBloque.SetError(cmbxHorario2, "Debe seleccionar uno de los horarios disponibles.");
                            cmbxHorario2.Focus();

                        }
                        else
                        {
                            errorProviderBloque.SetError(cmbxHorario2, "");
                           

                            if (lista_insumo.Count <= 0)
                            {
                                errorProviderContenido.SetError(dgvInsumos, "Debe seleccionar los insumos que se usarán durante la formación.");

                            }
                            else
                            {
                                errorProviderContenido.SetError(dgvInsumos, "");

                               
                                List<Insumos> insumos_curso = new List<Insumos>();

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

                                string aula = "";
                                string aula2 = "";
                                if (ubicacion == true) //SI SE DICTA EN UCS
                                {
                                    if (txtAula.Text == "")
                                    {
                                        errorProviderNombreF.SetError(txtAula, "Debe proporcionar el aula en el que se dictará la formación.");
                                        txtAula.Focus();
                                    }
                                    else
                                    {
                                        errorProviderNombreF.SetError(txtAula, "");
                                        aula = txtAula.Text;
                                    }

                                    if (rdbNoMantenerAula.Checked == true && txtSegundaAula.Text == "")
                                    {
                                        errorProviderContenido.SetError(txtSegundaAula, "Debe proporcionar el aula en el que se dictará la formación.");
                                        txtSegundaAula.Focus();
                                    }
                                    else
                                    {
                                        errorProviderContenido.SetError(txtSegundaAula, "");
                                        aula2 = txtSegundaAula.Text;

                                    }
                                    if (rdbSiMantenerAula.Checked == true)
                                    {
                                        aula2 = aula;
                                    }
                                    else
                                    {
                                        aula2 = txtSegundaAula.Text;
                                    }
                                    ////se actualiza la informacion del curso con los valores nuevos: 
                                    if (id_refrigerio > 0)
                                    {
                                        MySqlDataReader ActualizarCursoTerceraEtapa = Conexion.ConsultarBD("UPDATE cursos SET etapa_curso ='3', horario_uno='" + id_horario + "', horario_dos='" + id_horario2 + "', aula_dia1='" + aula + "', aula_dia2='" + aula2 + "', id_ref1='" + id_refrigerio + "',id_ref2='" + id_refrigerio2 + "' WHERE id_cursos='" + id_curso + "'");
                                        ActualizarCursoTerceraEtapa.Close();

                                        MySqlDataReader refri = Conexion.ConsultarBD("insert into cursos_tienen_refrigerios (cursos_id_cursos, refrigerios_id_ref) values ('" + id_curso + "', '" + id_refrigerio + "')");
                                        refri.Close();
                                        MySqlDataReader refri2 = Conexion.ConsultarBD("insert into cursos_tienen_refrigerios (cursos_id_cursos, refrigerios_id_ref) values ('" + id_curso + "', '" + id_refrigerio2 + "')");
                                        refri2.Close();
                                    }
                                    else
                                    {
                                        MySqlDataReader ActualizarCursoTerceraEtapa = Conexion.ConsultarBD("UPDATE cursos SET etapa_curso ='3', horario_uno='" + id_horario + "', horario_dos='" + id_horario2 + "', aula_dia1='" + aula + "', aula_dia2='" + aula2 + "' WHERE id_cursos='" + id_curso + "'");
                                        ActualizarCursoTerceraEtapa.Close();
                                    }
                                    MessageBox.Show("Los datos se han agregado correctamente.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.None);

                                }
                                else //NO SE DICTA EN UCS
                                {
                                    MySqlDataReader ActualizarCursoTerceraEtapa = Conexion.ConsultarBD("UPDATE cursos SET etapa_curso ='3', horario_uno='" + id_horario + "', horario_dos='" + id_horario2 + "' WHERE id_cursos='" + id_curso + "'");
                                    ActualizarCursoTerceraEtapa.Close();
                                    MessageBox.Show("Los datos se han agregado correctamente.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.None);

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
                       
                        if (lista_insumo.Count <= 0)
                        {
                            errorProviderContenido.SetError(dgvInsumos, "Debe seleccionar los insumos que se usarán durante la formación.");

                        }
                        else
                        {
                            errorProviderContenido.SetError(dgvInsumos, "");
                            List<Insumos> insumos_curso = new List<Insumos>();
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

                            string aula = "";
                            if (ubicacion == true) //SI SE DICTA EN UCS
                            {
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
                                    aula = txtAula.Text;
                                }
                                if (id_refrigerio > 0)
                                {
                                    MySqlDataReader ActualizarCursoTerceraEtapa = Conexion.ConsultarBD("UPDATE cursos SET etapa_curso ='3', horario_uno='" + id_horario + "', aula_dia1='" + aula + "', id_ref1='" + id_refrigerio + "' WHERE id_cursos='" + id_curso + "'");
                                    ActualizarCursoTerceraEtapa.Close();
                                    MySqlDataReader refri = Conexion.ConsultarBD("insert into cursos_tienen_refrigerios (cursos_id_cursos, refrigerios_id_ref) values ('" + id_curso + "', '" + id_refrigerio + "')");
                                    refri.Close();
                                }
                                else
                                {
                                    MySqlDataReader ActualizarCursoTerceraEtapa = Conexion.ConsultarBD("UPDATE cursos SET etapa_curso ='3',  horario_uno='" + id_horario + "', aula_dia1='" + aula + "' WHERE id_cursos='" + id_curso + "'");
                                    ActualizarCursoTerceraEtapa.Close();
                                }

                                MessageBox.Show("Los datos se han agregado correctamente.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.None);
                            }
                            else//si no se dicta en la uics
                            {
                                //se actualiza el curso con los datos obtenidos de la segunda etapa, como las fechas
                                MySqlDataReader ActualizarCursoTerceraEtapa = Conexion.ConsultarBD("UPDATE cursos SET etapa_curso ='3', horario_uno='" + id_horario + "' WHERE id_cursos='" + id_curso + "'");
                                ActualizarCursoTerceraEtapa.Close();
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
                            if (lista_insumo.Count <= 0)
                            {
                                errorProviderContenido.SetError(dgvInsumos, "Debe seleccionar los insumos que se usarán durante la formación.");

                            }
                            else
                            {
                                errorProviderContenido.SetError(dgvInsumos, "");

                               
                                List<Insumos> insumos_curso = new List<Insumos>();

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


                                string aula = "";
                                string aula2 = "";
                                if (ubicacion == true) //SI SE DICTA EN UCS
                                {
                                    if (gpbSeleccionRef.Enabled == true)
                                    {
                                        if (cmbxTipoRefrigerio.SelectedIndex == -1)
                                        {
                                            errorProviderFecha.SetError(cmbxTipoRefrigerio, "Debe seleccionar un tipo de refrigerio para la formación.");
                                            cmbxTipoRefrigerio.Focus();

                                        }
                                        else if (rdbNoMantenerRef.Checked == true && cmbxTipoRefrigerio2.SelectedIndex == -1)
                                        {
                                            errorProviderBloque.SetError(cmbxTipoRefrigerio2, "Debe seleccionar un tipo de refrigerio para la formación.");
                                            cmbxTipoRefrigerio2.Focus();
                                        }
                                    }
                                    errorProviderFecha.SetError(cmbxTipoRefrigerio, "");
                                    errorProviderBloque.SetError(cmbxTipoRefrigerio2, "");

                                    if (txtAula.Text == "")
                                    {
                                        errorProviderNombreF.SetError(txtAula, "Debe proporcionar el aula en el que se dictará la formación.");
                                        txtAula.Focus();
                                    }
                                    else
                                    {
                                        errorProviderNombreF.SetError(txtAula, "");
                                        aula = txtAula.Text;
                                        if (rdbNoMantenerAula.Checked == true && txtSegundaAula.Text == "")
                                        {
                                            errorProviderContenido.SetError(txtSegundaAula, "Debe proporcionar el aula en el que se dictará la formación.");
                                            txtSegundaAula.Focus();
                                        }
                                        else
                                        {
                                            errorProviderContenido.SetError(txtSegundaAula, "");

                                            if (rdbSiMantenerAula.Checked)
                                            {
                                                aula2 = aula;
                                            }
                                            else
                                            {
                                                aula2 = txtSegundaAula.Text;
                                            }

                                            if (id_refrigerio > 0)
                                            {
                                                MySqlDataReader ActualizarCursoTerceraEtapa = Conexion.ConsultarBD("UPDATE cursos SET etapa_curso ='3', horario_uno='" + id_horario + "', horario_dos='" + id_horario2 + "', aula_dia1='" + aula + "', aula_dia2='" + aula2 + "', id_ref1='" + id_refrigerio + "',id_ref2='" + id_refrigerio2 + "' WHERE id_cursos='" + id_curso + "'");
                                                ActualizarCursoTerceraEtapa.Close();                                                
                                                MySqlDataReader refri = Conexion.ConsultarBD("insert into cursos_tienen_refrigerios (cursos_id_cursos, refrigerios_id_ref) values ('" + id_curso + "', '" + id_refrigerio + "')");
                                                refri.Close();
                                                MySqlDataReader refri2 = Conexion.ConsultarBD("insert into cursos_tienen_refrigerios (cursos_id_cursos, refrigerios_id_ref) values ('" + id_curso + "', '" + id_refrigerio2 + "')");
                                                refri2.Close();
                                            }
                                            else
                                            {
                                                MySqlDataReader ActualizarCursoTerceraEtapa = Conexion.ConsultarBD("UPDATE cursos SET etapa_curso ='3', horario_uno='" + id_horario + "', horario_dos='" + id_horario2 + "', aula_dia1='" + aula + "', aula_dia2='" + aula2 + "' WHERE id_cursos='" + id_curso + "'");
                                                ActualizarCursoTerceraEtapa.Close();
                                            }
                                            MessageBox.Show("Los datos se han agregado correctamente.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.None);
                                        }
                                    }
                                }
                                else
                                {
                                    MySqlDataReader ActualizarCursoTerceraEtapa = Conexion.ConsultarBD("UPDATE cursos SET etapa_curso ='3', horario_uno='" + id_horario + "', horario_dos='" + id_horario2 + "' WHERE id_cursos='" + id_curso + "'");
                                    ActualizarCursoTerceraEtapa.Close();
                                    MessageBox.Show("Los datos se han agregado correctamente.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.None);

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

        #endregion

        #region MODIFICAR
        private void Modificar_intermedio()
        {
            
            GuardarIntermedio();

            if (formacion.bloque_curso == "1")
            {
                // si el checkbox esta seleccionado es que tiene co-facilitador
                if (chkbCoFacilitador.Checked == true && cmbxCoFa.SelectedIndex != -1)
                {
                    MySqlDataReader FacilitadorCurso = Conexion.ConsultarBD("INSERT INTO cursos_tienen_fa (cursos_id_cursos, facilitadores_id_fa, ctf_id_cofa, ctf_fecha) VALUES ('" + Cursos.id_curso13 + "', '" + fa.id_facilitador + "', '" + Cofa.id_facilitador + "', '" + time.fecha_curso + "')");
                    FacilitadorCurso.Close();
                    guardar = true;

                }
                else // sino, solo guarda al facilitador
                {
                    MySqlDataReader FacilitadorCurso = Conexion.ConsultarBD("INSERT INTO cursos_tienen_fa (cursos_id_cursos, facilitadores_id_fa, ctf_fecha, ctf_id_cofa) VALUES ('" + Cursos.id_curso13 + "', '" + fa.id_facilitador + "', '" + time.fecha_curso + "', '0')");
                    FacilitadorCurso.Close();
                    guardar = true;

                }
            }
            else
            {
                // si el checkbox esta seleccionado es que tiene co-facilitador
                if (chkbCoFacilitador.Checked == true && cmbxCoFa.SelectedIndex != -1)
                {
                    MySqlDataReader FacilitadorCurso = Conexion.ConsultarBD("INSERT INTO cursos_tienen_fa (cursos_id_cursos, facilitadores_id_fa, ctf_id_cofa, ctf_fecha, ctf_fecha2) VALUES ('" + Cursos.id_curso13 + "', '" + fa.id_facilitador + "', '" + Cofa.id_facilitador + "', '" + time.fecha_curso + "', '" + time.fechaDos_curso + "')");
                    FacilitadorCurso.Close();
                    guardar = true;

                }
                else // sino, solo guarda al facilitador
                {
                    MySqlDataReader FacilitadorCurso = Conexion.ConsultarBD("INSERT INTO cursos_tienen_fa (cursos_id_cursos, facilitadores_id_fa, ctf_fecha, ctf_fecha2, ctf_id_cofa) VALUES ('" + Cursos.id_curso13 + "', '" + fa.id_facilitador + "', '" + time.fecha_curso + "', '" + time.fechaDos_curso + "', '0')");
                    FacilitadorCurso.Close();
                    guardar = true;

                }
            }

            if (guardar == true)
            {
                FinalE2 = DateTime.Now;

                formacion.TiempoEtapa = Convert.ToString(FinalE2 - inicioE2);

                MySqlDataReader e2 = Conexion.ConsultarBD("SELECT duracionE2 from cursos where id_cursos='" + Cursos.id_curso13 + "'");
                if (e2.Read())
                {
                    string duracion = Convert.ToString(e2["duracionE2"]);

                    TimeSpan et2;
                    et2 = TimeSpan.Parse(duracion);

                    TimeSpan tt = TimeSpan.Parse(formacion.TiempoEtapa);

                    formacion.TiempoEtapa = (tt + et2).ToString();
                    //formacion.TiempoEtapa= Convert.ToString();

                }
                e2.Close();

                //se actualiza el tiempo en el que se trabajó en la formacion
                MySqlDataReader update = Conexion.ConsultarBD("UPDATE cursos SET duracionE2='" + formacion.TiempoEtapa + "' where id_cursos='" + Cursos.id_curso13 + "'");
                update.Close();

                //se agrega la modificacion en la tabla
                conexion.cerrarconexion();
                if (conexion.abrirconexion() == true)
                {
                    int agregarUGC = Clases.Formaciones.Agregar_U_MOD_C(conexion.conexion, Cursos.id_curso13, Usuario_logeado.id_usuario, inicioE2, FinalE2);
                    conexion.cerrarconexion();
                    btnModificar.Enabled = false;

                }
                btnModificar.Enabled = false;
                btnPausar.Enabled = false;
                btnSiguienteEtapa.Enabled = true;
                btnRetomar.Enabled = true;
                deshabilitarControlesIntermedio();
                btnCorreoAdministracion.Enabled = true;
                btnCorreoFacilitadores.Enabled = true;
            }
            
        }
        private void Modificar_avanzado()
        {
            //eliminará los insumos existentes y más abajo los añadirá para evitrar dublicados
            MySqlDataReader del = Conexion.ConsultarBD("delete from cursos_tienen_insumos where cti_id_curso='" + Cursos.id_curso13 + "'");
            del.Close();

            MySqlDataReader del1 = Conexion.ConsultarBD("delete from cursos_tienen_refrigerios where cursos_id_cursos ='" + Cursos.id_curso13 + "'");
            del1.Close();
            GuardarAvanzado();
            if (guardar == true)
            {
                FinalE3 = DateTime.Now;
                formacion.TiempoEtapa = Convert.ToString(FinalE3 - inicioE3);

                MySqlDataReader e3 = Conexion.ConsultarBD("SELECT duracionE3 from cursos where id_cursos='" + Cursos.id_curso13 + "'");
                if (e3.Read())
                {
                    string duracion = Convert.ToString(e3["duracionE3"]);

                    TimeSpan et3;
                    et3 = TimeSpan.Parse(duracion);

                    TimeSpan tt = TimeSpan.Parse(formacion.TiempoEtapa);

                    formacion.TiempoEtapa = (tt + et3).ToString();
                    //formacion.TiempoEtapa= Convert.ToString();

                }
                e3.Close();

                //se actualiza el tiempo en el que se trabajó en la formacion
                MySqlDataReader update = Conexion.ConsultarBD("UPDATE cursos SET duracionE3='" + formacion.TiempoEtapa + "' where id_cursos='" + Cursos.id_curso13 + "'");
                update.Close();

                //se agrega la modificacion en la tabla
                conexion.cerrarconexion();
                if (conexion.abrirconexion() == true)
                {
                    int agregarUGC = Clases.Formaciones.Agregar_U_MOD_C(conexion.conexion, Cursos.id_curso13, Usuario_logeado.id_usuario, inicioE3, FinalE3);
                    conexion.cerrarconexion();
                    btnModificar.Enabled = false;

                }
                btnSiguienteEtapa.Enabled = false;
                btnPausar.Enabled = false;
                btnRetomar.Enabled = true;
                deshabilitarControlesAvanzado();
            }
                      
            
        }

        #endregion

        /*---------------------- Panel lateral derecho ------------------------*/

        #region PANEL LATERAL
        private void btnSiguienteEtapa_Click(object sender, EventArgs e)
        {
            guardar = false;

            if (Formaciones.creacion == true)
            {               
                if (pnlNivel_basico.Visible == true)
                {
                    LabelCabecera.Text = "Nuevo Incompany: Detalles técnicos";
                    LabelCabecera.Location = new Point(115, 31);

                    pnlNivel_basico.Visible = false;
                    pnlNivel_intermedio.Visible = true;

                    Load_Sig_Re();

                    //comportamiento del panel nivel_intermedio de acuerdo a la duracion del curso
                    if (formacion.duracion == "4")
                    {
                        Controles_nivel_intermedio_EstatusInicial();
                        
                        dtpSegundaFecha.Enabled = false;
                        errorProviderFecha.SetError(dtpSegundaFecha, "");

                    }
                    else if (formacion.duracion == "8" && formacion.bloque_curso == "1")
                    {
                        Controles_nivel_intermedio_EstatusInicial();
                        
                        dtpSegundaFecha.Enabled = false;
                        errorProviderFecha.SetError(dtpSegundaFecha, "");
                    }
                    else if (formacion.duracion == "8" && formacion.bloque_curso == "2")
                    {
                        Controles_nivel_intermedio_EstatusInicial();                      
                        dtpSegundaFecha.Enabled = true;
                        

                    }
                    else
                    {
                        if (formacion.duracion == "16")
                        {
                            Controles_nivel_intermedio_EstatusInicial();
                            dtpSegundaFecha.Enabled = true;
                            
                        }
                    }

                    if(AFI.id_AFI==0)
                        MessageBox.Show("Ningun facilitador ha colaborado antes en esta formación.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                }
                else if (pnlNivel_intermedio.Visible == true)
                {
                    LabelCabecera.Text = "Nuevo InCompany: Logística";
                    LabelCabecera.Location = new Point(200, 31);
                                      

                    lblEtapaSiguiente.Location = new Point(3, 529);
                    lblEtapaSiguiente.Text = "Añadir participantes";

                    lblEtapafinal.Location = new Point(3, 570);
                    lblEtapafinal.Text = "Día de la formación";

                    pnlNivel_intermedio.Visible = false;
                    pnlNivel_avanzado.Visible = true;
                    Load_Sig_Re();

                }

            }
            else
            {
                btnGuardar.Enabled = false;
                if (pnlNivel_basico.Visible == true)
                {
                    LabelCabecera.Text = "      InCompany: Información básica";
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

                        inicioE2 = DateTime.Now;
                        if (AFI.id_AFI == 0)
                        {
                            llenarcomboFacilitador();
                            if (AFI.id_AFI == 0)
                                MessageBox.Show("Ningun facilitador ha colaborado antes en esta formación.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                    else if(Cursos.etapa_formacion13 == 2)
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
                        errorProviderFecha.SetError(dtpFechaCurso, "");
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
                   
                    LabelCabecera.Text = "      InCompany: Logística";
                    LabelCabecera.Location = new Point(200, 31);

                    btnGuardar.Enabled = false;

                    btnLimpiar.Enabled = false;
                    btnRetomar.Enabled = true;

                    
                    //comportamiento del panel nivel_AVANZADO de acuerdo a la duracion del curso Y SI SE DICTA EN UCS O NO
                   
                    //ARREGLOS VISUALES PARA LA ETAPA3
                    if (Cursos.ubicacion_ucs == "Si")
                    {
                        
                        //para arreglos visuales en el form
                        if (Cursos.duracion_formacion13 == "4")
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
                        else if (Cursos.duracion_formacion13 == "8" && Cursos.bloque_curso13 == "2")
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
                        else if (Cursos.duracion_formacion13 == "8")
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
                        else if (Cursos.duracion_formacion13 == "16")
                        {
                            //para la logistica:
                            rdbSiMantenerAula.Enabled = true;
                            rdbNoMantenerAula.Enabled = true;
                            txtSegundaAula.Enabled = false;
                            gpbAula.Enabled = true;
                            rdbMantenerRef.Enabled = true;
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
                                rdbMantenerRef.Visible = true;
                                rdbNoMantenerRef.Enabled = true;
                                rdbMantenerRef.Enabled = true;
                            }

                        }
                        if (Cursos.etapa_formacion13 == 1)
                        {
                            btnGuardar.Enabled = false;
                            btnSiguienteEtapa.Enabled = false;
                            btnRetomar.Enabled = false;
                            btnPausar.Enabled = true;
                            btnModificar.Enabled = true;
                            habilitarIntermedio();
                        }
                        else if (Cursos.etapa_formacion13 == 2)
                        {
                            cmbxTipoRefrigerio.SelectedIndex = -1;
                            btnGuardar.Enabled = false;
                            btnSiguienteEtapa.Enabled = false;
                            btnRetomar.Enabled = false;
                            btnPausar.Enabled = true;
                            btnModificar.Enabled = true;
                            habilitarAvanzado();
                            deshabilitarControlesIntermedio();
                            inicioE3 = DateTime.Now;
                        }
                        else if (Cursos.etapa_formacion13 == 3)
                        {
                            btnSiguienteEtapa.Enabled = false;
                            btnRetomar.Enabled = true;
                            btnPausar.Enabled = false;
                            btnModificar.Enabled = false;
                            deshabilitarControlesAvanzado();

                        }
                    }
                    else
                    {
                       
                        //para arreglos visuales en el form
                        if (Cursos.duracion_formacion13 == "4")
                        {
                            
                            gpbAula.Enabled = false;
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
                        else if (Cursos.duracion_formacion13 == "8" && Cursos.bloque_curso13 == "2")
                        {
                            
                            gpbSeleccionRef.Enabled = false;
                            rdbNoIgualHorario.Enabled = true;
                            rdbSiIgualHorario.Enabled = true;
                            gpbAula.Enabled = false;
                            //aqui estaran contemplados los arreglos (visuales y de contenido) de la siguiente etapa
                            ordenTerceraEtapa();
                            gpbHorarioCurso.Height = 169;
                            gpbSeleccionRef.Visible = true;

                            rdbSiMantenerAula.Enabled = true;
                            rdbNoMantenerAula.Enabled = true;
                            txtSegundaAula.Enabled = false;

                        }
                        else if (Cursos.duracion_formacion13 == "8")
                        {
                            ordenTerceraEtapa();
                            gpbSeleccionRef.Enabled = false;
                            gpbAula.Enabled = false;
                            gpbHorarioCurso.Height = 169;
                        }
                        else if (Cursos.duracion_formacion13 == "16")
                        {
                            ordenTerceraEtapa();
                            gpbSeleccionRef.Enabled = false;
                            rdbNoIgualHorario.Enabled = true;
                            rdbSiIgualHorario.Enabled = true;
                            gpbAula.Enabled = false;
                            gpbHorarioCurso.Height = 169;
                        }
                    }

                    //FIN COMPORTAMIENTO

                 
                    pnlNivel_intermedio.Visible = false;
                    pnlNivel_avanzado.Visible = true;

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
                        
                        DateTime FinalE2 = DateTime.Now;
                        String duracionE2 = Convert.ToString(FinalE2 - inicioE2);
                        //se actualiza el tiempo en el que se trabajó en la formacion
                        MySqlDataReader update = Conexion.ConsultarBD("UPDATE cursos SET duracionE2='" + duracionE2 + "' where id_cursos='" + formacion.id_curso + "'");
                        update.Close();

                        if (formacion.bloque_curso == "1")
                        {
                            // si el checkbox esta seleccionado es que tiene co-facilitador
                            if (chkbCoFacilitador.Checked == true && cmbxCoFa.SelectedIndex != -1)
                            {
                                MySqlDataReader FacilitadorCurso = Conexion.ConsultarBD("INSERT INTO cursos_tienen_fa (cursos_id_cursos, facilitadores_id_fa, ctf_id_cofa, ctf_fecha) VALUES ('" + formacion.id_curso + "', '" + fa.id_facilitador + "', '" + Cofa.id_facilitador + "', '" + time.fecha_curso + "')");
                                FacilitadorCurso.Close();
                                guardar = true;

                            }
                            else // sino, solo guarda al facilitador
                            {
                                MySqlDataReader FacilitadorCurso = Conexion.ConsultarBD("INSERT INTO cursos_tienen_fa (cursos_id_cursos, facilitadores_id_fa, ctf_fecha, ctf_id_cofa) VALUES ('" + formacion.id_curso + "', '" + fa.id_facilitador + "', '" + time.fecha_curso + "', '0')");
                                FacilitadorCurso.Close();
                                guardar = true;

                            }
                        }
                        else
                        {
                            // si el checkbox esta seleccionado es que tiene co-facilitador
                            if (chkbCoFacilitador.Checked == true && cmbxCoFa.SelectedIndex != -1)
                            {
                                MySqlDataReader FacilitadorCurso = Conexion.ConsultarBD("INSERT INTO cursos_tienen_fa (cursos_id_cursos, facilitadores_id_fa, ctf_id_cofa, ctf_fecha, ctf_fecha2) VALUES ('" + formacion.id_curso + "', '" + fa.id_facilitador + "', '" + Cofa.id_facilitador + "', '" + time.fecha_curso + "', '" + time.fechaDos_curso + "')");
                                FacilitadorCurso.Close();
                                guardar = true;

                            }
                            else // sino, solo guarda al facilitador
                            {
                                MySqlDataReader FacilitadorCurso = Conexion.ConsultarBD("INSERT INTO cursos_tienen_fa (cursos_id_cursos, facilitadores_id_fa, ctf_fecha, ctf_fecha2, ctf_id_cofa) VALUES ('" + formacion.id_curso + "', '" + fa.id_facilitador + "', '" + time.fecha_curso + "', '" + time.fechaDos_curso + "', '0')");
                                FacilitadorCurso.Close();
                                guardar = true;

                            }
                        }

                        if (guardar == true)
                        {
                            btnSiguienteEtapa.Enabled = true;
                            btnPausar.Enabled = true;
                            btnLimpiar.Enabled = true;

                            btnModificar.Enabled = false;
                            btnRetomar.Enabled = false;
                            btnGuardar.Enabled = false;
                            btnCorreoAdministracion.Enabled = true;
                            btnCorreoFacilitadores.Enabled = true;
                        }
                    }
                    else
                    {
                        if (pnlNivel_avanzado.Visible == true)
                        {
                            GuardarAvanzado();

                            DateTime FinalE3 = DateTime.Now;
                            String duracionE3 = Convert.ToString(FinalE3 - inicioE3);
                            //se actualiza el tiempo en el que se trabajó en la formacion
                            MySqlDataReader update = Conexion.ConsultarBD("UPDATE cursos SET duracionE3='" + duracionE3 + "' where id_cursos='" + formacion.id_curso + "'");
                            update.Close();

                            if (guardar == true)
                            {
                                deshabilitarControlesAvanzado();
                                btnSiguienteEtapa.Enabled = false;
                                btnPausar.Enabled = false;
                                btnLimpiar.Enabled = false;

                                btnModificar.Enabled = false;
                                btnRetomar.Enabled = false;
                                btnGuardar.Enabled = false;
                            }
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
            }else if (pnlNivel_intermedio.Visible == true)
            {
                //se deshabilitará controles del nivel intermedio
                deshabilitarControlesIntermedio();
            }else if (pnlNivel_avanzado.Visible == true)
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
            if(Formaciones.creacion == true)
            {
                //cuando retomar = habilitar los controles
                if (pnlNivel_basico.Visible == true)
                {
                    if (guardar == false)
                    {
                        btnGuardar.Enabled = true;
                    }
                    else
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
                    btnRutaManual.Enabled = true;
                    btnRutaBitacora.Enabled = true;
                    //Y lo deja igual que el load
                }
                else if (pnlNivel_intermedio.Visible == true)
                {
                    if (guardar == false)
                    {
                        btnGuardar.Enabled = true;
                    }
                    else
                    {
                        btnGuardar.Enabled = false;
                    }
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
                    btnGuardar.Enabled = false;
                }else
                {
                    btnGuardar.Enabled = true;
                }

            }
            else //si va a hacer modificaciones
            {
                if (pnlNivel_basico.Visible == true)
                {
                    //si retomar, se habilitan todos los campos
                    txtNombreFormacion.Enabled = true;
                    cmbxSolicitadoPor.Enabled = true;
                    cmbxDuracionFormacion.Enabled = true;
                    cmbxBloques.Enabled = true;

                    btnRutaContenido.Enabled = true;
                    btnRutaPresentacion.Enabled = true;
                    btnRutaBitacora.Enabled = true;
                    btnRutaManual.Enabled = true;
                    btnVerContenido.Enabled = false;
                    btnVerManual.Enabled = false;
                    btnVerPresentacion.Enabled = false;
                    btnVerBitacora.Enabled = false;

                    //comportamiento del panel lateral:
                    btnGuardar.Enabled = false;
                    btnLimpiar.Enabled = false;
                    btnRetomar.Enabled = false;

                    btnPausar.Enabled = true;
                    btnModificar.Enabled = true;
                    

                    fecha_creacion = DateTime.Now;

                    if (Cursos.etapa_formacion13 == 1)
                    {
                        btnSiguienteEtapa.Enabled = true;
                        if(AFI.id_AFI == 0)
                        {
                            llenarcomboFacilitador();
                        }

                    }
                   

                }
                else if (pnlNivel_intermedio.Visible == true)
                {

                    inicioE2 = DateTime.Now; //se guarda hora exacta en la que se dio click en ejecutar

                    btnGuardar.Enabled = false;
                    btnRetomar.Enabled = false;
                    btnPausar.Enabled = true;
                    btnModificar.Enabled = true;

                    if (Cursos.etapa_formacion13 == 1)
                    {
                        btnSiguienteEtapa.Enabled = false;

                    } else if (Cursos.etapa_formacion13 == 2)
                    {
                        btnSiguienteEtapa.Enabled = true;
                        habilitarIntermedio();
                        gpbCorreos.Enabled = false; // se deshabilita hasta que modifique en algo
                    }
                    else
                    {
                        btnSiguienteEtapa.Enabled =true;
                    }
                       
                        

                }
                else if (pnlNivel_avanzado.Visible == true)
                {
                    btnSiguienteEtapa.Enabled = false;
                    inicioE3 = DateTime.Now;//se guarda el momento en el que dio click en retomar
                    btnRetomar.Enabled = false;
                    btnPausar.Enabled = true;
                    btnModificar.Enabled = true;
                    habilitarAvanzado();
                    if (Cursos.bloque_curso13 == "1")
                    {
                        txtSegundaAula.Enabled = false;
                        rdbNoMantenerAula.Enabled = false;
                        rdbSiMantenerAula.Enabled = false;
                    }
                    else
                    {
                        rdbNoMantenerAula.Enabled = true;
                        rdbSiMantenerAula.Enabled = true;
                    }
                    if(Cursos.tiene_ref== "No")
                    {
                        gpbSeleccionRef.Enabled = false;
                    }
                    if (Cursos.ubicacion_ucs == "No")
                    {
                        gpbSeleccionRef.Enabled = false;
                        gpbAula.Enabled = false;

                    }
                }
            }
            
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (pnlNivel_basico.Visible == true)
            {
                if(Cursos.etapa_formacion13 == 1)
                {
                    if(txtNombreFormacion.Text!=Cursos.nombre_formacion13 || cmbxSolicitadoPor.Text != Cursos.solicitud_formacion13 || formacion.duracion != Cursos.duracion_formacion13 || formacion.bloque_curso!= Cursos.bloque_curso13)
                    {
                        formacion.tiene_ref = "0";
                        formacion.ubicacion_ucs = "Si"; //es SI hasta que se actualice en la etapa dos o nivel intermedio
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
                                            formacion.solicitado = cmbxSolicitadoPor.Text;
                                           // MessageBox.Show(formacion.solicitado);
                                            int id_solicitud = 0;
                                            MySqlDataReader idS = Conexion.ConsultarBD("select id_clientes from clientes where nombre_empresa='" + formacion.solicitado + "'");
                                            if (idS.Read())
                                            {
                                                id_solicitud = Convert.ToInt32(idS["id_clientes"]);
                                            }
                                            idS.Close();
                                           // MessageBox.Show(id_solicitud.ToString());
                                            FinalE1 = DateTime.Now; //agregar en fecha_mod_final en UGC
                                            formacion.fecha_inicial = fecha_creacion;// agregar fecha_mod_inicio
                                            formacion.TiempoEtapa = Convert.ToString(FinalE1 - fecha_creacion); //para añadir a la duracionE1
                                           // MessageBox.Show(formacion.TiempoEtapa+" tiempoEtapa");
                                            formacion.id_user = Clases.Usuario_logeado.id_usuario;
                                            formacion.etapa_curso = 1;//representa la etapa actual: nivel_basico (cambiará para cada panel)
                                            formacion.solicitado = cmbxSolicitadoPor.Text;
                                            MySqlDataReader e1 = Conexion.ConsultarBD("SELECT duracionE1 from cursos where id_cursos='" + Cursos.id_curso13 + "'");
                                            if (e1.Read())
                                            {
                                                string duracion = Convert.ToString(e1["duracionE1"]);

                                                TimeSpan et1;
                                                et1 = TimeSpan.Parse(duracion);
                                                MessageBox.Show(et1.ToString() + "Lo que se extrajo de la BD");
                                                TimeSpan tt = TimeSpan.Parse(formacion.TiempoEtapa);
                                                
                                                formacion.TiempoEtapa = (tt+et1).ToString();
                                                //formacion.TiempoEtapa= Convert.ToString();
                                               // MessageBox.Show(formacion.TiempoEtapa +"dizque la suma de lo9s timespan");
                                            }
                                            e1.Close();
                                            MySqlDataReader update = Conexion.ConsultarBD("UPDATE cursos SET nombre_curso='" + formacion.nombre_formacion + "', duracion_curso='" + formacion.duracion + "', bloque_curso='" + formacion.bloque_curso + "', solicitud_curso='" + formacion.solicitado + "', duracionE1='" + formacion.TiempoEtapa + "' where id_cursos='" + Cursos.id_curso13 + "'");
                                            update.Close();
                                            MySqlDataReader clientes_solicitan_cursos = Conexion.ConsultarBD("update clientes_solicitan_cursos set id_cliente1='"+id_solicitud+"' where id_curso1='" + Cursos.id_curso13 + "'");
                                            clientes_solicitan_cursos.Close();
                                            conexion.cerrarconexion();
                                            if (conexion.abrirconexion() == true)
                                            {
                                                int agregarUGC = Clases.Formaciones.Agregar_U_MOD_C(conexion.conexion, Cursos.id_curso13, formacion.id_user, fecha_creacion, FinalE1);
                                                conexion.cerrarconexion();
                                                if (agregarUGC > 0)
                                                {
                                                    evaluarAFI();
                                                    guardar = true;
                                                    MessageBox.Show("La formación se ha modificado correctamente.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.None);
                                                    btnModificar.Enabled = false;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }else
                    {
                        MessageBox.Show("No se han detectado cambios.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            else if (pnlNivel_intermedio.Visible == true)
            {
                if(Cursos.etapa_formacion13 == 1)
                {
                    GuardarIntermedio();


                    if (formacion.bloque_curso == "1")
                    {
                        // si el checkbox esta seleccionado es que tiene co-facilitador
                        if (chkbCoFacilitador.Checked == true && cmbxCoFa.SelectedIndex != -1)
                        {
                            MySqlDataReader FacilitadorCurso = Conexion.ConsultarBD("INSERT INTO cursos_tienen_fa (cursos_id_cursos, facilitadores_id_fa, ctf_id_cofa, ctf_fecha) VALUES ('" + Cursos.id_curso13 + "', '" + fa.id_facilitador + "', '" + Cofa.id_facilitador + "', '" + time.fecha_curso + "')");
                            FacilitadorCurso.Close();
                            guardar = true;

                        }
                        else // sino, solo guarda al facilitador
                        {
                            MySqlDataReader FacilitadorCurso = Conexion.ConsultarBD("INSERT INTO cursos_tienen_fa (cursos_id_cursos, facilitadores_id_fa, ctf_fecha, ctf_id_cofa) VALUES ('" + Cursos.id_curso13 + "', '" + fa.id_facilitador + "', '" + time.fecha_curso + "', '0')");
                            FacilitadorCurso.Close();
                            guardar = true;

                        }
                    }
                    else
                    {
                        // si el checkbox esta seleccionado es que tiene co-facilitador
                        if (chkbCoFacilitador.Checked == true && cmbxCoFa.SelectedIndex != -1)
                        {
                            MySqlDataReader FacilitadorCurso = Conexion.ConsultarBD("INSERT INTO cursos_tienen_fa (cursos_id_cursos, facilitadores_id_fa, ctf_id_cofa, ctf_fecha, ctf_fecha2) VALUES ('" + Cursos.id_curso13 + "', '" + fa.id_facilitador + "', '" + Cofa.id_facilitador + "', '" + time.fecha_curso + "', '" + time.fechaDos_curso + "')");
                            FacilitadorCurso.Close();
                            guardar = true;

                        }
                        else // sino, solo guarda al facilitador
                        {
                            MySqlDataReader FacilitadorCurso = Conexion.ConsultarBD("INSERT INTO cursos_tienen_fa (cursos_id_cursos, facilitadores_id_fa, ctf_fecha, ctf_fecha2, ctf_id_cofa) VALUES ('" + Cursos.id_curso13 + "', '" + fa.id_facilitador + "', '" + time.fecha_curso + "', '" + time.fechaDos_curso + "', '0')");
                            FacilitadorCurso.Close();
                            guardar = true;

                        }
                    }

                    if (guardar == true)
                    {
                        FinalE2 = DateTime.Now;

                        formacion.TiempoEtapa = Convert.ToString(FinalE2 - inicioE2);
                        //se actualiza el tiempo en el que se trabajó en la formacion
                        MySqlDataReader update = Conexion.ConsultarBD("UPDATE cursos SET duracionE2='" + formacion.TiempoEtapa + "' where id_cursos='" + Cursos.id_curso13 + "'");
                        update.Close();

                        //se agrega la modificacion en la tabla
                        conexion.cerrarconexion();
                        if (conexion.abrirconexion() == true)
                        {
                            int agregarUGC = Clases.Formaciones.Agregar_U_MOD_C(conexion.conexion, Cursos.id_curso13, Usuario_logeado.id_usuario, inicioE3, FinalE3);
                            conexion.cerrarconexion();
                            btnModificar.Enabled = false;

                        }

                        btnModificar.Enabled = false;
                        btnPausar.Enabled = false;

                        btnSiguienteEtapa.Enabled = true;
                        btnRetomar.Enabled = true;
                        deshabilitarControlesIntermedio();


                        Cursos.etapa_formacion13 = 2;
                        btnCorreoFacilitadores.Enabled = true;
                        btnCorreoAdministracion.Enabled = true;
                    }
                    

                }
                else if (Cursos.etapa_formacion13 == 2)
                {
                    if(Cursos.fecha_dos13 != "No aplica")
                    {
                        if (formacion.ubicacion_ucs != Cursos.ubicacion_ucs || formacion.tiene_ref != Cursos.tiene_ref || dtpFechaCurso.Value.ToString("yyyy-MM-dd") != Cursos.fecha_uno13 || dtpSegundaFecha.Value.ToString("yyyy-MM-dd") != Cursos.fecha_dos13 || cmbxFa.Text!=fcombo.nombreyapellido1 || cmbxCoFa.Text !=cf.nombreyapellido1)
                        {
                            Modificar_intermedio();
                            
                        }
                    }else
                    {
                        if (formacion.ubicacion_ucs != Cursos.ubicacion_ucs || formacion.tiene_ref != Cursos.tiene_ref || dtpFechaCurso.Value.ToString("yyyy-MM-dd") != Cursos.fecha_uno13 || cmbxFa.Text != fcombo.nombreyapellido1 || cmbxCoFa.Text != cf.nombreyapellido1)
                        {
                            Modificar_intermedio();
                           
                        }
                    }
                   
                }
                deshabilitarControlesIntermedio();
            }else if (pnlNivel_avanzado.Visible == true)
            {
                if (Cursos.etapa_formacion13 == 2)
                {
                    GuardarAvanzado();
                    if (guardar == true)
                    {
                        FinalE3 = DateTime.Now;
                        formacion.TiempoEtapa = Convert.ToString(FinalE3 - inicioE3);
                        //se actualiza el tiempo en el que se trabajó en la formacion
                        MySqlDataReader update = Conexion.ConsultarBD("UPDATE cursos SET duracionE3='" + formacion.TiempoEtapa + "' where id_cursos='" + Cursos.id_curso13 + "'");
                        update.Close();

                        //se agrega la modificacion en la tabla
                        conexion.cerrarconexion();
                        if (conexion.abrirconexion() == true)
                        {
                            int agregarUGC = Clases.Formaciones.Agregar_U_MOD_C(conexion.conexion, Cursos.id_curso13, Usuario_logeado.id_usuario, inicioE3, FinalE3);
                            conexion.cerrarconexion();
                            btnModificar.Enabled = false;

                        }
                        btnSiguienteEtapa.Enabled = false;
                        btnPausar.Enabled = false;
                        btnRetomar.Enabled = true;
                        deshabilitarControlesAvanzado();
                    }
                    
                }
                else if (Cursos.etapa_formacion13 == 3)
                {

                    if (formacion.ubicacion_ucs == "Si")
                    {
                        if (formacion.tiene_ref == "Si")
                        {
                            if (formacion.bloque_curso == "2")
                            {
                                if (lista_insumo_cargada != lista_insumo || formacion.horario1 != Cursos.horario1 || formacion.horario2 != Cursos.horario2 || txtAula.Text != formacion.aula1 || txtSegundaAula.Text != formacion.aula2 || formacion.refri1 != Cursos.tipo_ref1 || formacion.refri2 != Cursos.tipo_ref2)
                                {
                                    Modificar_avanzado();
                                }
                            }
                            else
                            {
                                if (lista_insumo_cargada != lista_insumo || formacion.horario1 != Cursos.horario1 || txtAula.Text != formacion.aula1 || formacion.refri1!=Cursos.tipo_ref1)
                                {
                                    Modificar_avanzado();
                                }
                            }
                        }
                        else //si No tiene refrigerio
                        {
                            if (formacion.bloque_curso == "2")
                            {
                                if (lista_insumo_cargada != lista_insumo || formacion.horario1 != Cursos.horario1 || formacion.horario2 != Cursos.horario2 || txtAula.Text != formacion.aula1 || txtSegundaAula.Text != formacion.aula2)
                                {
                                    Modificar_avanzado();
                                }
                            }
                            else
                            {
                                if (lista_insumo_cargada != lista_insumo || formacion.horario1 != Cursos.horario1)
                                {
                                    Modificar_avanzado();
                                }
                            }
                        }


                    }// SI NO se hace en ucs (
                    else
                    {
                        if (formacion.bloque_curso == "2")
                        {
                            if (lista_insumo_cargada != lista_insumo || formacion.horario1 != Cursos.horario1 || formacion.horario2 != Cursos.horario2)
                            {
                                Modificar_avanzado();
                            }
                        }
                        else
                        {
                            if (lista_insumo_cargada != lista_insumo || formacion.horario1 != Cursos.horario1)
                            {
                                Modificar_avanzado();
                            }
                        }

                    }



                }
            }
        }

        #endregion

        /*---------------------- Controles nivel_basico------------------------*/
        #region EVENTOS NIVEL BASICO
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

                //List<Formaciones> lista = new List<Formaciones>();
                //List<string> nombre = new List<string>();
                //MySqlDataReader leer = Conexion.ConsultarBD("SELECT id_cursos, solicitud_curso  FROM cursos WHERE nombre_curso = '" + formacion.nombre_formacion+ "' AND tipo_curso='InCompany' AND estatus_curso LIKE 'En curso'");
                //while (leer.Read())
                //{
                //    Formaciones f = new Formaciones();
                //    f.id_curso = Convert.ToInt32(leer["id_cursos"]);
                //    string n = Convert.ToString(leer["solicitud_curso"]);
                //    f.estatus = "En curso";
                //    lista.Add(f);
                //    nombre.Add(n);
                //}
                //leer.Close();



                ////si está: buscar todos los clientes que lo hayan solicitado (en la tabla clientes solicitan cursos) 
                //for (int i = 0; i < nombre.Count; i++)
                //{
                //    for (int a = 0; a < cmbxSolicitadoPor.Items.Count; a++)
                //    {
                //        if (cmbxSolicitadoPor.Items[a].ToString() == nombre[i])
                //        {
                //            cmbxSolicitadoPor.Items.RemoveAt(a);
                //        }
                //    }

                //}

                if (formacion.pq_inst == 0)
                {
                    conexion.cerrarconexion();
                    if (conexion.abrirconexion() == true)
                    {
                        List<Clases.Paquete_instruccional> paqueteExiste = new List<Clases.Paquete_instruccional>();
                        paqueteExiste = Clases.Formaciones.ObtenerPaqueteCursoDistinto(conexion.conexion, formacion);
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

            ////validar si la empresa seleccionada ha solicitado un curso del mismo nombre.
            //MySqlDataReader leer = Conexion.ConsultarBD("SELECT id_cursos from cursos where nombre_curso='" + formacion.nombre_formacion + "' AND solicitud_curso='" + formacion.solicitado + "' AND tipo_curso='Incompany'");
            //if (leer.Read())
            //{
            //    errorProviderNombreF.SetError(txtNombreFormacion, "El cliente seleccionado ya ha solicitado esta formación.");
            //    txtNombreFormacion.Focus();
            //}
            //else
            //{
            //    errorProviderNombreF.SetError(txtNombreFormacion, "");
            //}
            //leer.Close();
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

        #endregion

        /*---------------------- Controles nivel_intermedio------------------------*/

        #region EVENTOS NIVEL INTERMEDIO
        private void rdbNoInstalaciones_CheckedChanged(object sender, EventArgs e)
        {
            rdbSiRef.Checked = false;
            rdbNoRef.Checked = false;
            gpbRefrigerio.Enabled = false;
            formacion.ubicacion_ucs = "No";
        }
        private void rdbInstalaciones_CheckedChanged(object sender, EventArgs e)
        {
            if(Formaciones.creacion==true)
                btnGuardar.Enabled = true;
           
            
           gpbRefrigerio.Enabled = true;
            formacion.ubicacion_ucs = "Si";
            
        }
        
        private void dtpFechaCurso_ValueChanged(object sender, EventArgs e)
        {
            if(Formaciones.creacion==true)
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
                time.fechaDos_curso = dtpSegundaFecha.Value.ToString("yyyy-MM-dd"); ;
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
                cmbxCoFa.SelectedIndex = -1;
                txtTlfnCoFa.Clear();
                txtCorreoCoFa.Clear();
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
                //MessageBox.Show(fa.id_facilitador + " idFa y idafi"+AFI.id_AFI);
                

            }


        }

        private void cmbxFa_Validating(object sender, CancelEventArgs e)
        {
            //validar si este facilitador estará disponible para esa o esas fechas
            if ((formacion.duracion == "4") || (formacion.duracion == "8" && formacion.bloque_curso == "1"))
            {
                int fa_disponible = 0;
                //validar si estará disponible para la fecha del dia 1
                conexion.cerrarconexion();
                if (conexion.abrirconexion() == true)
                {
                    fa_disponible = Clases.Facilitadores.FacilitadorDisponibleDia(conexion.conexion, time.fecha_curso, fa.id_facilitador);
                    conexion.cerrarconexion();
                    if (fa_disponible != 0)//si retorna algo es que este facilitador ya está ocupado para esa fecha
                    {
                        MessageBox.Show("El facilitador está ocupado en la fecha seleccionada: " + time.fecha_curso + ".");
                        cmbxFa.SelectedIndex = -1;
                        fa.id_facilitador = 0;
                        //cmbxFa.Focus();
                    }
                    else
                    {
                        errorProviderPresentacion.SetError(cmbxFa, "");
                        if(fa.id_facilitador != 0)
                        {
                            conexion.cerrarconexion();
                            if (conexion.abrirconexion() == true)
                                fa_disponible = Facilitadores.CoFacilitadorDisponibleDia(conexion.conexion, time.fecha_curso, fa.id_facilitador);

                            conexion.cerrarconexion();
                            if (fa_disponible != 0)//si retorna algo es que este facilitador ya está ocupado para esa fecha
                            {
                                MessageBox.Show("El facilitador está ocupado en la fecha seleccionada: " + time.fecha_curso + ".");
                                cmbxFa.SelectedIndex = -1;
                                fa.id_facilitador = 0;
                                //cmbxFa.Focus();
                            }
                            else
                            {
                                //si todo bien, cargar los datos en el gpbDatosFa
                                gpbDatosFa.Enabled = true;
                                chkbCoFacilitador.Enabled = true;
                                conexion.cerrarconexion();
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
                int fa_disponible=0;
                //validar ambas fechas
                conexion.cerrarconexion();
                if (conexion.abrirconexion() == true)
                {
                    fa_disponible = Clases.Facilitadores.FacilitadorDisponibleDia(conexion.conexion, time.fecha_curso, fa.id_facilitador);
                    conexion.cerrarconexion();
                    if (fa_disponible > 0)//si retorna algo es que este facilitador ya está ocupado para esa fecha
                    {
                        MessageBox.Show("El facilitador está ocupado en esta fecha: " + time.fecha_curso + ".");
                        cmbxFa.SelectedIndex = -1;
                        fa.id_facilitador = 0;
                        //cmbxFa.Focus();
                    }
                    else
                    {
                       
                        errorProviderPresentacion.SetError(cmbxFa, "");
                        if(fa.id_facilitador != 0)
                        {
                            conexion.cerrarconexion();
                            if (conexion.abrirconexion() == true)
                                fa_disponible = Facilitadores.CoFacilitadorDisponibleDia(conexion.conexion, time.fecha_curso, fa.id_facilitador);

                            conexion.cerrarconexion();
                            if (fa_disponible != 0)//si retorna algo es que este facilitador ya está ocupado para esa fecha
                            {
                                MessageBox.Show("El facilitador está ocupado en la fecha seleccionada: " + time.fecha_curso + ".");
                                cmbxFa.SelectedIndex = -1;
                                fa.id_facilitador = 0;
                                //cmbxFa.Focus();
                            }
                            else
                            {
                                int fa_disponibleDia2 = 0;
                                conexion.cerrarconexion();
                                if (conexion.abrirconexion() == true)
                                {
                                    fa_disponibleDia2 = Clases.Facilitadores.FacilitadorDisponibleDia2(conexion.conexion, time.fechaDos_curso, fa.id_facilitador);
                                    conexion.cerrarconexion();
                                    if (fa_disponibleDia2 > 0)//si retorna algo es que este facilitador ya está ocupado para esa fecha
                                    {
                                        MessageBox.Show("El facilitador está ocupado en esta fecha: " + time.fechaDos_curso + ".");
                                        cmbxFa.SelectedIndex = -1;
                                        fa.id_facilitador = 0;
                                        //cmbxFa.Focus();
                                    }
                                    else
                                    {
                                        errorProviderPresentacion.SetError(cmbxFa, "");
                                        conexion.cerrarconexion();
                                        if (conexion.abrirconexion() == true)
                                            fa_disponible = Facilitadores.CoFacilitadorDisponibleDia2(conexion.conexion, time.fechaDos_curso, fa.id_facilitador);

                                        conexion.cerrarconexion();
                                        if (fa_disponible != 0)//si retorna algo es que este facilitador ya está ocupado para esa fecha
                                        {
                                            MessageBox.Show("El facilitador está ocupado en la fecha seleccionada: " + time.fechaDos_curso + ".");
                                            cmbxFa.SelectedIndex = -1;
                                            fa.id_facilitador = 0;
                                            //cmbxFa.Focus();
                                        }
                                        else
                                        {
                                            //si todo bien, cargar los datos en el gpbDatosFa
                                            gpbDatosFa.Enabled = true;
                                            chkbCoFacilitador.Enabled = true;
                                            conexion.cerrarconexion();
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
            MySqlDataReader cnom = Conexion.ConsultarBD("select * from facilitadores where nombre_apellido='" + cmbxCoFa.Text + "'");
            if (cnom.Read())
            {
                Cofa.id_facilitador = Convert.ToInt32(cnom["id_fa"]);
            }
            cnom.Close();
        }

        private void cmbxCoFa_Validating(object sender, CancelEventArgs e)
        {
            //validar si este facilitador estará disponible para esa o esas fechas
            if ((formacion.duracion == "4") || (formacion.duracion == "8" && formacion.bloque_curso == "1"))
            {
                if (Cofa.id_facilitador != 0)
                {
                    //validar si estará disponible para la fecha del dia 1
                    conexion.cerrarconexion();
                    if (conexion.abrirconexion() == true)
                    {
                        int fa_disponible = Clases.Facilitadores.CoFacilitadorDisponibleDia(conexion.conexion, time.fecha_curso, Cofa.id_facilitador);
                        conexion.cerrarconexion();
                        if (fa_disponible != 0)//si retorna algo es que este facilitador ya está ocupado para esa fecha
                        {
                            MessageBox.Show("El Co-facilitador está ocupado en la fecha seleccionada: " + time.fecha_curso + ".");
                            cmbxCoFa.SelectedIndex = -1;
                            Cofa.id_facilitador = 0;
                            //cmbxCoFa.Focus();
                        }
                        else
                        {
                            errorProviderPresentacion.SetError(cmbxCoFa, "");
                            conexion.cerrarconexion();
                            if (conexion.abrirconexion() == true)
                                fa_disponible = Clases.Facilitadores.FacilitadorDisponibleDia(conexion.conexion, time.fecha_curso, Cofa.id_facilitador);

                            conexion.cerrarconexion();
                            if (fa_disponible != 0)//si retorna algo es que este facilitador ya está ocupado para esa fecha
                            {
                                MessageBox.Show("El Co-facilitador está ocupado en la fecha seleccionada: " + time.fecha_curso + ".");
                                cmbxCoFa.SelectedIndex = -1;
                                Cofa.id_facilitador = 0;
                                //cmbxCoFa.Focus();
                            }
                            else
                            {
                                //si todo bien, cargar los datos en el gpbDatosCoFa
                                gpbDatosCoFa.Enabled = true;
                                conexion.cerrarconexion();
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
            else if ((formacion.duracion == "16") || (formacion.duracion == "8" && formacion.bloque_curso == "2"))
            {
                if (Cofa.id_facilitador != 0)
                {
                    //validar ambas fechas
                    conexion.cerrarconexion();
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
                            conexion.cerrarconexion();
                            if (conexion.abrirconexion() == true)
                                fa_disponible = Clases.Facilitadores.FacilitadorDisponibleDia(conexion.conexion, time.fecha_curso, Cofa.id_facilitador);

                            conexion.cerrarconexion();
                            if (fa_disponible != 0)//si retorna algo es que este facilitador ya está ocupado para esa fecha
                            {
                                MessageBox.Show("El Co-facilitador está ocupado en la fecha seleccionada: " + time.fecha_curso + ".");
                                cmbxCoFa.SelectedIndex = -1;
                                Cofa.id_facilitador = 0;
                                cmbxCoFa.Focus();
                            }
                            else
                            {

                            }
                            conexion.cerrarconexion();
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
                                    conexion.cerrarconexion();
                                    if (conexion.abrirconexion() == true)
                                        fa_disponible = Clases.Facilitadores.FacilitadorDisponibleDia2(conexion.conexion, time.fechaDos_curso, Cofa.id_facilitador);

                                    conexion.cerrarconexion();
                                    if (fa_disponible != 0)//si retorna algo es que este facilitador ya está ocupado para esa fecha
                                    {
                                        MessageBox.Show("El Co-facilitador está ocupado en la fecha seleccionada: " + time.fechaDos_curso + ".");
                                        cmbxCoFa.SelectedIndex = -1;
                                        Cofa.id_facilitador = 0;
                                        cmbxCoFa.Focus();
                                    }
                                    else
                                    {
                                        //si todo bien, cargar los datos en el gpbDatosFa
                                        gpbDatosCoFa.Enabled = true;

                                        conexion.cerrarconexion();
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

        #endregion
        /*---------------------------Tercera etapa------------------------------*/
        #region EVENTOS NIVEL AVANZADO
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
            if (cmbxHorarios.SelectedIndex == -1)
            {
                errorProviderFecha.SetError(cmbxHorarios, "Debe seleccionar un horario primero.");
                cmbxHorarios.Focus();
                rdbSiIgualHorario.Checked = false;
            }else
            {
                errorProviderFecha.SetError(cmbxHorarios, "");
                // cmbxHorario2.Items.Clear();
                cmbxHorario2.Text = cmbxHorarios.Text;
                cmbxHorario2.Enabled = false;
                id_horario2 = id_horario;
            }
           
        }

        private void rdbNoIgualHorario_CheckedChanged(object sender, EventArgs e)
        {
            if (cmbxHorarios.SelectedIndex == -1)
            {
                errorProviderFecha.SetError(cmbxHorarios, "Debe seleccionar un horario primero.");
                cmbxHorarios.Focus();
                rdbNoIgualHorario.Checked = false;
            }else
            {
                errorProviderFecha.SetError(cmbxHorarios, "");
                string t;
                if (formacion.duracion == "8" && formacion.bloque_curso == "2")
                {
                    t = "B";
                    llenarComboHorario2(t, id_horario);

                }
                else if (formacion.duracion == "16")
                {
                    t = "A";
                    llenarComboHorario2(t, id_horario);

                }
                cmbxHorario2.Enabled = true;
                cmbxHorario2.Focus();
            }
           
        }

        private void rdbSiRef_CheckedChanged(object sender, EventArgs e)
        {

            formacion.tiene_ref = "Si";
        }

        private void rdbNoRef_CheckedChanged(object sender, EventArgs e)
        {
            formacion.tiene_ref ="No";
        }

        private void txtAula_Leave(object sender, EventArgs e)
        {
            formacion.aula1 = txtAula.Text;
        }

        private void txtSegundaAula_Leave(object sender, EventArgs e)
        {
            formacion.aula2 = txtSegundaAula.Text;
        }

        private void rdbSiMantenerAula_CheckedChanged(object sender, EventArgs e)
        {
            txtSegundaAula.Text = txtAula.Text;
            formacion.aula2 = txtSegundaAula.Text;
            txtSegundaAula.Enabled = false;
        }

        private void rdbNoMantenerAula_CheckedChanged(object sender, EventArgs e)
        {
            txtSegundaAula.Enabled = true;
            txtSegundaAula.Focus();
        }

        private void rdbSiMantenerRef_CheckedChanged(object sender, EventArgs e)
        {
            if (cmbxTipoRefrigerio.SelectedIndex == -1)
            {
                errorProviderFecha.SetError(cmbxTipoRefrigerio, "Debe seleccionar un tipo de refrigerio.");
                cmbxTipoRefrigerio.Focus();
                rdbMantenerRef.Checked = false;

            }else
            {
                cmbxTipoRefrigerio2.Enabled = false;
                cmbxTipoRefrigerio2.SelectedIndex = cmbxTipoRefrigerio.SelectedIndex;
                cmbxTipoRefrigerio2.Text = cmbxTipoRefrigerio.Text;
                id_refrigerio2 = id_refrigerio;

                errorProviderFecha.SetError(cmbxTipoRefrigerio, "");
            }
           
        }

        private void rdbNoMantenerRef_CheckedChanged(object sender, EventArgs e)
        {
            if (cmbxTipoRefrigerio.SelectedIndex == -1)
            {
                errorProviderFecha.SetError(cmbxTipoRefrigerio, "Debe seleccionar un tipo de refrigerio.");
                cmbxTipoRefrigerio.Focus();
                cmbxTipoRefrigerio2.Enabled = false;
            }else
            {
                errorProviderFecha.SetError(cmbxTipoRefrigerio, "");
                cmbxTipoRefrigerio2.Enabled = true;
                cmbxTipoRefrigerio2.Focus();
            }
           
        }

        private void cmbxTipoRefrigerio2_SelectionChangeCommitted(object sender, EventArgs e)
        {
            id_refrigerio2 = Convert.ToInt32(cmbxTipoRefrigerio2.SelectedValue);
            if (Formaciones.creacion == true)
            {
                id_refrigerio2 = Convert.ToInt32(cmbxTipoRefrigerio2.SelectedValue);
             //   MessageBox.Show(id_refrigerio2.ToString() + "id del refreigerio2 en creacion");


                MySqlDataReader nombre = Conexion.ConsultarBD("SELECT id_ref from refrigerios where ref_nombre='" + cmbxTipoRefrigerio2.Text + "'");
                if (nombre.Read())
                {
                 //   MessageBox.Show(nombre["id_ref"].ToString());
                    id_refrigerio2 = Convert.ToInt32(nombre["id_ref"]);
                //    MessageBox.Show(id_refrigerio2.ToString() + "id del refreigerio2 en creacion 2");
                }

            }
            else
            {
                MySqlDataReader nombre = Conexion.ConsultarBD("SELECT id_ref from refrigerios where ref_nombre='" + cmbxTipoRefrigerio2.Text + "'");
                if (nombre.Read())
                {
                    //MessageBox.Show(nombre["id_ref"].ToString());
                    id_refrigerio2 = Convert.ToInt32(nombre["id_ref"]);
                    formacion.refri2 = cmbxTipoRefrigerio2.Text;
                }
            }
            formacion.refri2 = cmbxTipoRefrigerio2.Text;
        }

        private void cmbxTipoRefrigerio_SelectionChangeCommitted(object sender, EventArgs e)
        {
            
            if (Formaciones.creacion == true)
            {
                id_refrigerio = Convert.ToInt32(cmbxTipoRefrigerio.SelectedValue);
                llenarcombo2Refrigerio(id_refrigerio);
                MySqlDataReader nombre = Conexion.ConsultarBD("SELECT id_ref from refrigerios where ref_nombre='" + cmbxTipoRefrigerio.Text + "'");
                if (nombre.Read())
                {
                  //  MessageBox.Show(nombre["id_ref"].ToString());
                    id_refrigerio = Convert.ToInt32(nombre["id_ref"]);
                }

            }
            else
            {
                MySqlDataReader nombre = Conexion.ConsultarBD("SELECT id_ref from refrigerios where ref_nombre='" + cmbxTipoRefrigerio.Text + "'");
                if (nombre.Read())
                {
                //    MessageBox.Show(nombre["id_ref"].ToString());
                    id_refrigerio = Convert.ToInt32(nombre["id_ref"]);
                }
            }
            
            formacion.refri1 = cmbxTipoRefrigerio.Text;
        }


        private void cmbxHorarios_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if(Formaciones.creacion==true)
                inicioE3 = DateTime.Now;

            id_horario = Convert.ToInt32(cmbxHorarios.SelectedValue);
            formacion.horario1 = cmbxHorarios.Text;
        }
        private void cmbxHorario2_SelectionChangeCommitted(object sender, EventArgs e)
        {
            id_horario2 = Convert.ToInt32(cmbxHorario2.SelectedValue);
            formacion.horario2 = cmbxHorario2.Text;
        }

        #endregion

        /*-----------------------------CORREOS-------------------------------------*/
        #region GenerarPDF
        private void generarpdfAdmin()
        {
            string fecha = DateTime.Now.ToString("dd-MM-yyyy");
            int cantidad = 0;
            string nombre_reporte = "Formación (" + formacion.nombre_formacion + ") " + fecha + " ";
            string extension = ".pdf";
            string ruta = @"C:\Users\ZM\Documents\Last_repo\ucs_sistema\UCS_NODO_FGC\Archivos\Reportes_emitidos\";
            //aqui, se modificaré el nombre del archivo, añadiendo una cuenta progresiva de acuerdo a los existentes en la carpeta contenedora
            string[] dirs = Directory.GetFiles(@"C:\\Users\\ZM\\Documents\\Last_repo\\ucs_sistema\\UCS_NODO_FGC\\Archivos\\Reportes_emitidos", nombre_reporte + cantidad.ToString() + extension);
            int retorno = dirs.Length;
            string nuevonombre;
            while (retorno != 0)
            {
                cantidad = cantidad + 1;
                nuevonombre = nombre_reporte + cantidad.ToString() + extension;
                string[] check = Directory.GetFiles(@"C:\\Users\\ZM\\Documents\\Last_repo\\ucs_sistema\\UCS_NODO_FGC\\Archivos\\Reportes_emitidos", nuevonombre);
                retorno = check.Length;
            }
            nuevonombre = nombre_reporte + cantidad.ToString() + extension;
            string fileName = Path.Combine(ruta, nuevonombre);

            // aqui se le pasa la ruta completa a nodos para usarla en otro form
            Nodos.ruta_PDF = fileName;
           
            byte[] bytesImagen =
            new System.Drawing.ImageConverter().ConvertTo(Properties.Resources.logo_ucs, typeof(byte[])) as byte[];
            iTextSharp.text.Image imagen = iTextSharp.text.Image.GetInstance(bytesImagen);
            imagen.Alignment = Element.ALIGN_LEFT;
            imagen.ScaleToFit(125f, 60F);

            //tabla y celda
            PdfPTable t, tablaFa, tablaCf;
            PdfPCell c, c2;
            //documento

            Document document = new Document(PageSize.LETTER, 50, 50, 50, 50);
            PdfWriter.GetInstance(document, new FileStream(fileName, FileMode.Create));

            document.Open();
            //document.Add(imagen);

            t = new PdfPTable(2);
            t.SetWidthPercentage(new float[] { 300, 300 }, PageSize.LETTER);

            c = new PdfPCell(imagen);
            c.Border = 0;
            c.VerticalAlignment = Element.ALIGN_TOP;
            c.HorizontalAlignment = Element.ALIGN_LEFT;
            t.AddCell(c);

            var fecha_encabezado = new Paragraph(DateTime.Today.ToShortDateString());
            c2 = new PdfPCell(fecha_encabezado);
            c2.Border = 0;
            c2.VerticalAlignment = Element.ALIGN_MIDDLE;
            c2.HorizontalAlignment = Element.ALIGN_RIGHT;
            t.AddCell(c2);


            var titulo = new Paragraph("INFORMACIÓN A NODO ADMINISTRACIÓN");
            titulo.Alignment = 1;//0-Left, 1 middle,2 Right

            document.Add(t);
            document.Add(titulo);
            document.Add(Chunk.NEWLINE);
            document.Add(Chunk.NEWLINE);
            document.Add(Chunk.NEWLINE);


            var Nombre_formacion = new Paragraph("     Nombre de la formación: " + formacion.nombre_formacion + ".");

            document.Add(Nombre_formacion);
            document.Add(Chunk.NEWLINE);

            var fechainicio = new Paragraph("     Fecha de inicio de la formacion: " + time.fecha_curso + ".");

            document.Add(fechainicio);
            document.Add(Chunk.NEWLINE);

            var fechaFin = new Paragraph();
            //comprobacion rapida de fecha2
            if (formacion.bloque_curso == "2")
            {
                fechaFin.Add("     Fecha de culminación de la formación: " + time.fechaDos_curso + ".");
            }
            else if (formacion.bloque_curso == "1")
            {
                fechaFin.Add("     Fecha de culminación de la formación: " + time.fecha_curso + ".");
            }

            document.Add(fechaFin);
            document.Add(Chunk.NEWLINE);

            var facilitador = new Paragraph("     Facilitador");

            document.Add(facilitador);
            document.Add(Chunk.NEWLINE);

            //tabla con datos de facilitador:
            tablaFa = new PdfPTable(1);
            tablaFa.WidthPercentage = 100;

            var nombreFa = new Paragraph("             - Nombre completo: " + cmbxFa.Text + ".");
            PdfPCell c1 = new PdfPCell(nombreFa);
            c1.Border = 0;
            tablaFa.AddCell(c1);

            var tlfnFa = new Paragraph("             - Teléfono: " + txtTlfnFa.Text + ".");
            c1 = new PdfPCell(tlfnFa);
            c1.Border = 0;
            tablaFa.AddCell(c1);

            var correoFa = new Paragraph("             - Correo electrónico: " + txtCorreoFa.Text);
            c1 = new PdfPCell(correoFa);
            c1.Border = 0;
            tablaFa.AddCell(c1);

            //obtener la ubicacion del facilitador:
            string ubicacion = "";
            MySqlDataReader datos = Conexion.ConsultarBD("select * from facilitadores where id_fa='" + fa.id_facilitador + "'");
            if (datos.Read())
            {
                ubicacion = Convert.ToString(datos["ubicacion_fa"]);
            }
            datos.Close();

            var ubicacionFa = new Paragraph("             - Ubicación: " + ubicacion + ".");
            c1 = new PdfPCell(ubicacionFa);
            c1.Border = 0;
            tablaFa.AddCell(c1);

            document.Add(tablaFa);


            var cofa = new Paragraph();
            //comprobacion rápida de cofa:
            if (chkbCoFacilitador.Checked == false)
            {
                cofa.Add("     Co-Facilitador: No aplica.");
            }
            else
            {
                cofa.Add("     Co-Facilitador: " + cmbxCoFa.Text + ".");
                document.Add(Chunk.NEWLINE);
                document.Add(cofa);
                document.Add(Chunk.NEWLINE);
                //tabla con datos de COfacilitador:
                tablaCf = new PdfPTable(1);
                tablaCf.WidthPercentage = 100;
                var nombrecf = new Paragraph("             - Nombre completo: " + cmbxFa.Text + ".");
                PdfPCell cel = new PdfPCell(nombrecf);
                cel.Border = 0;
                tablaCf.AddCell(cel);

                var tlfncf = new Paragraph("             - Teléfono: " + txtTlfnFa.Text + ".");
                cel = new PdfPCell(tlfncf);
                cel.Border = 0;
                tablaCf.AddCell(cel);

                var correocf = new Paragraph("             - Correo electrónico: " + txtCorreoFa.Text);
                cel = new PdfPCell(correocf);
                cel.Border = 0;
                tablaCf.AddCell(cel);

                //obtener la ubicacion del facilitador:
                string ubicacionc = "";
                MySqlDataReader datosc = Conexion.ConsultarBD("select * from facilitadores where id_fa='" + Cofa.id_facilitador + "'");
                if (datosc.Read())
                {
                    ubicacionc = Convert.ToString(datosc["ubicacion_fa"]);
                }
                datosc.Close();

                var ubicacioncf = new Paragraph("             - Ubicación: " + ubicacionc + ".");
                cel = new PdfPCell(ubicacioncf);
                cel.Border = 0;
                tablaCf.AddCell(cel); ;

                document.Add(tablaCf);

            }


            var emitido = new Paragraph("Emitido por: " + Usuario_logeado.nombre_usuario + " " + Usuario_logeado.apellido_usuario + ", " + Usuario_logeado.cargo_usuario + " del Nodo de Formación.");


            document.Add(Chunk.NEWLINE);
            document.Add(Chunk.NEWLINE);
            document.Add(Chunk.NEWLINE);
            document.Add(emitido);



            document.Close();
            ////opcional, para mostrar luego de hacerse
            //Process prc = new System.Diagnostics.Process();
            //prc.StartInfo.FileName = fileName;
            //prc.Start();
        }

        private void GenerarPDFFacilitador()
        {
            string fecha = DateTime.Now.ToString("dd-MM-yyyy");
            int cantidad = 0;
            string nombre_reporte = "Formación (" + formacion.nombre_formacion + ") " + fecha + " ";
            string extension = ".pdf";
            string ruta = @"C:\Users\ZM\Documents\Last_repo\ucs_sistema\UCS_NODO_FGC\Archivos\Reportes_emitidos\";
            //aqui, se modificaré el nombre del archivo, añadiendo una cuenta progresiva de acuerdo a los existentes en la carpeta contenedora
            string[] dirs = Directory.GetFiles(@"C:\\Users\\ZM\\Documents\\Last_repo\\ucs_sistema\\UCS_NODO_FGC\\Archivos\\Reportes_emitidos", nombre_reporte + cantidad.ToString() + extension);
            int retorno = dirs.Length;
            string nuevonombre;
            while (retorno != 0)
            {
                cantidad = cantidad + 1;
                nuevonombre = nombre_reporte + cantidad.ToString() + extension;
                string[] check = Directory.GetFiles(@"C:\\Users\\ZM\\Documents\\Last_repo\\ucs_sistema\\UCS_NODO_FGC\\Archivos\\Reportes_emitidos", nuevonombre);
                retorno = check.Length;
            }
            nuevonombre = nombre_reporte + cantidad.ToString() + extension;
            string fileName = Path.Combine(ruta, nuevonombre);

            // aqui se le pasa la ruta completa a nodos para usarla en otro form
            Nodos.ruta_PDF = fileName;

            //imagen en encabezado
            byte[] bytesImagen =
            new System.Drawing.ImageConverter().ConvertTo(Properties.Resources.logo_ucs, typeof(byte[])) as byte[];
            iTextSharp.text.Image imagen = iTextSharp.text.Image.GetInstance(bytesImagen);
            imagen.Alignment = Element.ALIGN_LEFT;
            imagen.ScaleToFit(125f, 60F);

            //tabla y celda
            PdfPTable t;
            PdfPCell c, c2;
            //documento

            Document document = new Document(PageSize.LETTER, 50, 50, 50, 50);
            PdfWriter.GetInstance(document, new FileStream(fileName, FileMode.Create));
            document.Open();

            t = new PdfPTable(2);
            t.SetWidthPercentage(new float[] { 300, 300 }, PageSize.LETTER);

            c = new PdfPCell(imagen);
            c.Border = 0;
            c.VerticalAlignment = Element.ALIGN_TOP;
            c.HorizontalAlignment = Element.ALIGN_LEFT;
            t.AddCell(c);

            var fecha_encabezado = new Paragraph(DateTime.Today.ToShortDateString());
            c2 = new PdfPCell(fecha_encabezado);
            c2.Border = 0;
            c2.VerticalAlignment = Element.ALIGN_MIDDLE;
            c2.HorizontalAlignment = Element.ALIGN_RIGHT;
            t.AddCell(c2);



            var parrafo3 = new Paragraph("INFORMACIÓN DEL CURSO: " + txtNombreFormacion.Text);
            parrafo3.Alignment = 1;//0-Left, 1 middle,2 Right

            var Nombre_formacion = new Paragraph("     Nombre de la formación: " + formacion.nombre_formacion + ".");
            var solicitante = new Paragraph("     Empresa solicitante: " + formacion.solicitado + ".");
            var fechainicio = new Paragraph("     Fecha de inicio de la formacion: " + time.fecha_curso + ".");

            var fechaFin = new Paragraph();
            //comprobacion rapida de fecha2
            if (formacion.bloque_curso == "2")
            {
                fechaFin.Add("     Fecha de culminación de la formación: " + time.fechaDos_curso + ".");
            }

            var facilitador = new Paragraph("     Facilitador a cargo: " + cmbxFa.Text + ".");

            document.Add(t);
            document.Add(parrafo3);
            document.Add(Chunk.NEWLINE);
            document.Add(Chunk.NEWLINE);
            document.Add(Chunk.NEWLINE);
            document.Add(Chunk.NEWLINE);

            document.Add(Nombre_formacion);
            document.Add(Chunk.NEWLINE);
            document.Add(solicitante);
            document.Add(Chunk.NEWLINE);
            document.Add(fechainicio);
            document.Add(Chunk.NEWLINE);
            document.Add(fechaFin);
            document.Add(Chunk.NEWLINE);
            document.Add(facilitador);

            var cofa = new Paragraph();
            //comprobacion rápida de cofa:
            if (chkbCoFacilitador.Checked == false)
            {
                cofa.Add("     Co-Facilitador: No aplica.");
                document.Add(Chunk.NEWLINE);
                document.Add(cofa);
            }
            else
            {
                cofa.Add("     Co-Facilitador: " + cmbxCoFa.Text + ".");
                document.Add(Chunk.NEWLINE);
                document.Add(cofa);
                document.Add(Chunk.NEWLINE);
            }

            var emitido = new Paragraph("Emitido por: " + Usuario_logeado.nombre_usuario + " " + Usuario_logeado.apellido_usuario + ", " + Usuario_logeado.cargo_usuario + " del Nodo de Formación.");


            document.Add(Chunk.NEWLINE);
            document.Add(Chunk.NEWLINE);
            document.Add(Chunk.NEWLINE);
            document.Add(emitido);



            document.Close();
            ////opcional, para mostrar luego de hacerse
            //Process prc = new System.Diagnostics.Process();
            //prc.StartInfo.FileName = fileName;
            //prc.Start();
        }
        #endregion


        #region envio correo
        private bool AccesoInternet()
        {

            try
            {
                System.Net.IPHostEntry host = System.Net.Dns.GetHostEntry("www.google.com");
                return true;

            }
            catch (Exception es)
            {

                return false;
            }

        }
        public Stream GetStreamFile(string filePath)
        {
            using (FileStream fileStream = File.OpenRead(filePath))
            {
                MemoryStream memStream = new MemoryStream();
                memStream.SetLength(fileStream.Length);
                fileStream.Read(memStream.GetBuffer(), 0, (int)fileStream.Length);

                return memStream;
            }
        }
        private void EnviarCorreo(string correo, string asuntos, string ruta)
        {
            //consiste basicamente en obtener el pdf creado (desde la ruta ya establecida) y anexarlo a un formato de correo simple:

            String asunto = asuntos;
            String mensaje = "Saludos cordiales. Se adjunta información pertinente al curso de formación: " + formacion.nombre_formacion + ".";
            String destintario = correo;
            String remitente = "soporteucs@gmail.com";
            MailMessage msg = new MailMessage();
            SmtpClient smtp = new SmtpClient();
            msg.To.Add(destintario);

            msg.Attachments.Add(new Attachment(GetStreamFile(ruta), Path.GetFileName(ruta), "application/pdf"));

            msg.From = new MailAddress(remitente, "Nodo de Formación", System.Text.Encoding.UTF8);
            msg.Subject = asunto;
            msg.SubjectEncoding = System.Text.Encoding.UTF8;
            msg.Body = mensaje;
            msg.BodyEncoding = System.Text.Encoding.UTF8;
            msg.IsBodyHtml = false;

            smtp.Credentials = new System.Net.NetworkCredential(remitente, "ucs.29933526"); //entre comillas va el password de ese correo electronico
            smtp.Port = 587;
            smtp.Host = "smtp.gmail.com";
            smtp.EnableSsl = true;
            try
            {
                Task.Run(() =>
                {
                    smtp.Send(msg);
                    msg.Dispose();
                    MessageBox.Show("Correo enviado.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                );

                MessageBox.Show("Esta tarea puede tardar algunos minutos, por favor espere.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al enviar el correo electrónico: " + ex.Message + " Intentelo más tarde.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }

        private void EnviarCorreoFacilitador(string correo)
        {
            //int id_paquete = 0;
            //conexion.cerrarconexion();
            //if (conexion.abrirconexion() == true)
            //    id_paquete = Clases.Formaciones.ObtenerIdPaquete(conexion.conexion, p_inst);
            //conexion.cerrarconexion();
            //consiste basicamente en obtener el pdf creado (desde la ruta ya establecida) y anexarlo a un formato de correo simple:

            String asunto = "Información y formatos del curso " + formacion.nombre_formacion + ".";
            String mensaje = "Saludos cordiales. Se adjunta información pertinente al curso de formación: " + formacion.nombre_formacion + ".";
            String destintario = correo;
            String remitente = "soporteucs@gmail.com";
            MailMessage msg = new MailMessage();
            SmtpClient smtp = new SmtpClient();
            msg.To.Add(destintario);

            //primero se añade el pdf creado con la informacion general
            msg.Attachments.Add(new Attachment(GetStreamFile(Nodos.ruta_PDF), Path.GetFileName(Nodos.ruta_PDF), "application/pdf"));

            //luego el contenido
            msg.Attachments.Add(new Attachment(GetStreamFile(p_inst.contenido), Path.GetFileName(p_inst.contenido), "application/pdf"));
            if (p_inst.manual != "")
            {
                msg.Attachments.Add(new Attachment(GetStreamFile(p_inst.manual), Path.GetFileName(p_inst.manual), "application/pdf"));
            }
            
            if (p_inst.presentacion != "")
            {
                msg.Attachments.Add(new Attachment(GetStreamFile(p_inst.presentacion), Path.GetFileName(p_inst.presentacion), "application/pdf"));
            }
            if (p_inst.bitacora != "")
            {
                msg.Attachments.Add(new Attachment(GetStreamFile(p_inst.bitacora), Path.GetFileName(p_inst.bitacora), "application/pdf"));
            }
            msg.From = new MailAddress(remitente, "Nodo de Formación", System.Text.Encoding.UTF8);
            msg.Subject = asunto;
            msg.SubjectEncoding = System.Text.Encoding.UTF8;
            msg.Body = mensaje;
            msg.BodyEncoding = System.Text.Encoding.UTF8;
            msg.IsBodyHtml = false;

            smtp.Credentials = new System.Net.NetworkCredential(remitente, "ucs.29933526"); //entre comillas va el password de ese correo electronico
            smtp.Port = 587;
            smtp.Host = "smtp.gmail.com";
            smtp.EnableSsl = true;
            try
            {
                Task.Run(() =>
                {
                    smtp.Send(msg);
                    msg.Dispose();
                    MessageBox.Show("Correo enviado.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                );

                MessageBox.Show("Esta tarea puede tardar algunos minutos, por favor espere.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al enviar el correo electrónico: " + ex.Message + " Intentelo más tarde.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
        #endregion

        #region BTN CORREOS
        private void btnCorreoAdministrador_Click(object sender, EventArgs e)
        {
            Nodos.nombre_nodo = "Administración";

            generarpdfAdmin();
            MessageBox.Show("Se ha generado un PDF con información del curso.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information);

            MySqlDataReader mail = Conexion.ConsultarBD("select * from nodos where nombre_nodo='Administración'");
            if (mail.Read())
            {
                int mantiene = Convert.ToInt32(mail["mantener"]);
                if (mantiene == 1)
                {
                    Nodos.correo_ndoo = mail["correo_nodo"].ToString();
                    string asunt = "Datos de la formación " + formacion.nombre_formacion + " y facilitadores.";
                    //si hay correo predeterminado, se envia automaticamente y mensaje de generacion de informe!
                    if (AccesoInternet())
                    {
                        EnviarCorreo(Nodos.correo_ndoo, asunt, Nodos.ruta_PDF);
                    }
                    else
                    {
                        MessageBox.Show("No es posible enviar el correo en estos momentos (Verifique su conexión a internet).", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    //Hay correo, pero no se mantiene, abrir el form donde pregunta el destinatario
                    Nodos.mantener = 0;
                    Nodos.formacion_nombre = formacion.nombre_formacion; //aqui se le pasa el nombre para usarlo en el form correo_destinatario
                    Nodos.correo_ndoo = mail["correo_nodo"].ToString();
                    Correo_destinatario destinatario = new Correo_destinatario();
                    destinatario.ShowDialog();
                }
            }
            else
            {
                //SI NO SE ENCUENTRA CORREO ALGUNO, MOSTRAR EL FORM DE DESTINATARIO
                Nodos.mantener = 0;
                Nodos.formacion_nombre = formacion.nombre_formacion; //aqui se le pasa el nombre para usarlo en el form correo_destinatario
                Nodos.correo_ndoo = "NO HAY CORREO";
                Correo_destinatario destinatario = new Correo_destinatario();
                destinatario.ShowDialog();
            }
            mail.Close();
        }

        private void btnCorreoFacilitador_Click(object sender, EventArgs e)
        {
            GenerarPDFFacilitador();
            MessageBox.Show("Se ha generado un PDF con información del curso.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //si hay correo predeterminado, se envia automaticamente y mensaje de generacion de informe!
            if (AccesoInternet())
            {

                EnviarCorreoFacilitador(txtCorreoFa.Text);
            }
            else
            {
                MessageBox.Show("No es posible enviar el correo en estos momentos (Verifique su conexión a internet).", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion
    }
}
