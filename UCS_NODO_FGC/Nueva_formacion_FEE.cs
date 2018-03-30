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
        #region variables
        Curso_AFI AFI = new Curso_AFI();
        Formaciones formacion = new Clases.Formaciones();
        conexion_bd conexion = new Clases.conexion_bd();
        Tiempos_curso time = new Clases.Tiempos_curso();
        Facilitadores fa = new Clases.Facilitadores();
        Facilitadores Cofa = new Clases.Facilitadores();
        Facilitadores faDatos = new Clases.Facilitadores();

        Paquete_instruccional p_inst = new Clases.Paquete_instruccional();
        List<Facilitador_todos> lista = new List<Facilitador_todos>();
        List<int> lista_id = new List<int>();

        bool guardar, ExisteFormacion;
        string presentacion = "";
        string contenido = "";
        string bitacora = "";
        int id_refrigerio, id_horario;
        Facilitador_todos fcombo = new Facilitador_todos();
        DateTime fecha_creacion, FinalE1, FinalE2, FinalE3, inicioE3, inicioE2;
        List<string> nombre_publicidad = new List<string>();
        List<string> lista_difusion_cargada = new List<string>();
        List<string> lista_insumo = new List<string>();
        List<string> lista_insumo_cargada = new List<string>();
        Facilitador_todos cf = new Facilitador_todos();

        #endregion
        public Nueva_formacion_FEE()
        {
            InitializeComponent();
        }

        private void Nueva_formacion_FEE_Load(object sender, EventArgs e)
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
                //cargar los facilitadores del nivel_intermedio

            }
            //lo de arriba, aplica para modificacion o creacion 


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


                

                llenarcomboFacilitador();
                llenarcomboRefrigerio();
            }else
            {

                dtpFechaCurso.Value = DateTime.Today;
                
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

                    btnCorreoAdministracion.Enabled = true;
                    btnCorreoComercializacion.Enabled = true;
                    btnRetomar.Enabled = false;
                    btnSiguienteEtapa.Enabled = true;

                    CargarDatosEtapaUno();

                    CargarDatosEtapaDos();

                    if (Cursos.duracion_formacion13 == "16" || (Cursos.duracion_formacion13 == "8" && Cursos.bloque_curso13 == "1")) //por ahora, siempre entra aqui
                    {
                        string tipo = "A";
                        llenarComboHorario(tipo);
                    }
                    else if (Cursos.duracion_formacion13 == "4" || (Cursos.duracion_formacion13 == "8" && Cursos.bloque_curso13 == "2"))
                    {
                        string tipo = "B";
                        llenarComboHorario(tipo);
                    }


                }
                else if (Cursos.etapa_formacion13 == 3)
                {
                    btnRetomar.Enabled = false;
                    btnSiguienteEtapa.Enabled = true;

                    CargarDatosEtapaUno();

                    CargarDatosEtapaDos();
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

                    CargarDatosEtapaTres();
                                        

                }


            }
        }

        /*------------------METODOS------------------------*/
        #region metodos auxiliares
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
        private void deshabilitarControlesIntermedio()
        {

            gpbRefrigerio.Enabled = false;

            gpbFechaHora.Enabled = false; //aunque el groupbox esté deshabilitado, los dtp dentro no

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

            gpbRefrigerio.Enabled = true;
            gpbFechaHora.Enabled = true;
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
        private void Controles_nivel_intermedio_EstatusInicial()
        {
            gpbRefrigerio.Enabled = true;
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
            if (Formaciones.creacion == true)
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

            if(Formaciones.creacion==true)
                cmbxTipoRefrigerio.SelectedIndex = -1;
        }
        private void llenarComboHorario(string tipo)
        {
            //llenar el combobox con los horarios registrados en la BD
            cmbxHorarios.ValueMember = "id_horario";
            cmbxHorarios.DisplayMember = "contenido_horario";
            cmbxHorarios.DataSource = Horarios.llenarcmbxHorario(tipo);
            if(Formaciones.creacion==true)
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
        private void llenarComboCOFA_AFI(int idC, int idFa)
        {
            cmbxCoFa.ValueMember = "id_faci";
            cmbxCoFa.DisplayMember = "nombreyapellido1";
            cmbxCoFa.DataSource = listaCOFA_AFI(idC, idFa);
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
        #endregion

        #region cargar datos
        private void CargarDatosEtapaUno()
        {
            fa.id_facilitador = 0;
            formacion.nombre_formacion = Cursos.nombre_formacion13;
            formacion.solicitado = Cursos.solicitud_formacion13;

            formacion.bloque_curso = Cursos.bloque_curso13;
            formacion.ubicacion_ucs = "Si";
            deshabiltarControlesBasico();
            //carga el nombre
            txtNombreFormacion.Text = Cursos.nombre_formacion13;
            //carga quien solicita
            txtSolicitadoPor.Text = Cursos.solicitud_formacion13;
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
                case "8 Horas": //por ahora siempre entra acá
                    cmbxDuracionFormacion.SelectedIndex =0;
                    formacion.duracion = "8";
                    cmbxBloques.Items.Clear();
                    cmbxBloques.Items.Add("1");
                    cmbxBloques.SelectedIndex = 0;
                    
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

            if (Cursos.p_bitacora == "")
            {
                btnVerBitacora.Enabled = false;
            }
            else
            {
                bitacora = Cursos.p_bitacora;
                btnVerBitacora.Enabled = true;
            }

            


        }
        private void CargarDatosEtapaDos()
        {
            fa.id_facilitador = 0;
           

            MySqlDataReader dif = Conexion.ConsultarBD("select * from difusion d inner join cursos_tiene_publicidad ctp on ctp.ctp_id_difusion=d.id_difusion where ctp.ctp_id_curso='" + Cursos.id_curso13 + "'");
            while (dif.Read())
            {
                string difusion = Convert.ToString(dif["dif_contenido"]);
                lista_difusion_cargada.Add(difusion);
            }
            for(int i=0; i<lista_difusion_cargada.Count; i++)
            {
                foreach(DataGridViewRow row in dgvMediosDifusion.Rows)
                {
                    string celda = Convert.ToString(row.Cells["opcion_difusion"].Value);
                    if (celda == lista_difusion_cargada[i])
                    {
                        row.Cells["seleccionar_opcion"].Value = true;
                        nombre_publicidad.Add(celda);
                    }
                }
            }
           
            //llenar datos del refrigerio
            if (Cursos.tiene_ref == "Si")
            {
                rdbSiRef.Checked = true;
                formacion.tiene_ref = "Si";
            }
            else
            {
                rdbNoRef.Checked = true;
                formacion.tiene_ref = "No";
            }

            //llenar fechas
            dtpFechaCurso.Value = Convert.ToDateTime(Cursos.fecha_uno13);
            

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
            //MessageBox.Show(Cursos.id_curso13.ToString());
            //buscar id_fa de acuerdo al id del curso
            MySqlDataReader leer = Conexion.ConsultarBD("SELECT * from cursos_tienen_fa where cursos_id_cursos = '" + Cursos.id_curso13 + "'");
            if (leer.Read())
            {
                id_fa = Convert.ToInt32(leer["facilitadores_id_fa"]);
                co_fa = Convert.ToInt32(leer["ctf_id_cofa"]);
            }
            leer.Close();
          //  MessageBox.Show(id_fa.ToString());
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
                ////cmbxCoFa.Text = cf.nombreyapellido1;
                //txtCorreoCoFa.Text = cf.correo_facilitador;
                //txtTlfnCoFa.Text = cf.tlfn_facilitador;
                chkbCoFacilitador.Checked = true;

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
            }

            
            formacion.tiene_ref = Cursos.tiene_ref;
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


            }
            else
            {
                cmbxTipoRefrigerio.SelectedIndex = -1;
               
                formacion.refri1 = "";
              
            }

            //buscar el id del horario1:
            MySqlDataReader h1 = Conexion.ConsultarBD("SELECT idhorarios from horarios where horario='" + Cursos.horario1 + "'");
            if (h1.Read())
            {
                id_horario = Convert.ToInt32(h1["idhorarios"]);
            }
            h1.Close();


            
           // MessageBox.Show(cmbxHorarios.Text + " :Horario1" + txtAula.Text + " :aula ");

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
            
        }
        #endregion

        #region Guardardatos
        private void GuardarBasico()
        {
           try
            {
                formacion.tiene_ref = "0"; // no tiene por estar en la etapa 1 o nivel básico (esto se actualiza en la etapa 2)
                formacion.ubicacion_ucs = "Si"; //siempre es si

                if ( ExisteFormacion == false)
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
                                                                        evaluarAFI(); //para ver si el curso está registrado en afi y modificar los facilitadores mostrados en E2
                                                                        
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
                                                        evaluarAFI(); //para ver si el curso está registrado en afi y modificar los facilitadores mostrados en E2

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
                                                                    evaluarAFI(); //para ver si el curso está registrado en afi y modificar los facilitadores mostrados en E2

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
                int id_curso = 0;

                if (Formaciones.creacion == true)
                {
                    //se obtiene el id del curso
                    MySqlDataReader obtener_id_curso = Conexion.ConsultarBD("SELECT id_cursos FROM cursos WHERE nombre_curso = '" + txtNombreFormacion.Text + "' AND tipo_curso='InCompany'  AND estatus_curso='En curso' AND duracion_curso=" + formacion.duracion + " AND id_usuario1='" + formacion.id_user + "' and solicitud_curso='" + txtSolicitadoPor.Text + "'");
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

                if (rdbSiRef.Checked==false && rdbNoRef.Checked == false)
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
                                    time.fecha_curso = dtpFechaCurso.Value.ToString("yyyy-MM-dd");
                                  
                                    
                                    List<Difusion> medios_publicitarios = new List<Difusion>();
                                    
                                    
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
                                        MySqlDataReader ActualizarCursoTieneRef = Conexion.ConsultarBD("UPDATE cursos SET tiene_ref='0'  WHERE id_cursos='" + id_curso + "'");
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
                                        MySqlDataReader ActualizarCursoTieneRef = Conexion.ConsultarBD("UPDATE cursos SET tiene_ref='1'  WHERE id_cursos='" + id_curso + "'");
                                        ActualizarCursoTieneRef.Close();
                                    }

                                    //se actualiza el curso con los datos obtenidos de la segunda etapa, como las fechas
                                    MySqlDataReader ActualizarCursoSegundaEtapa = Conexion.ConsultarBD("UPDATE cursos SET etapa_curso ='2', fecha_uno='" + dtpFechaCurso.Value.ToString("yyyy-MM-dd") + "',bloque_curso= '" + formacion.bloque_curso + "' WHERE id_cursos='" + id_curso + "'");
                                    ActualizarCursoSegundaEtapa.Close();

                                    GuardarAfiE2(); //evalua again si el curso está registrado en afi, en caso de que no: lo registra.

                                    // si el checkbox esta seleccionado es que tiene co-facilitador
                                    if (chkbCoFacilitador.Checked == true && cmbxCoFa.SelectedIndex != -1)
                                    {
                                        MySqlDataReader FacilitadorCurso = Conexion.ConsultarBD("INSERT INTO cursos_tienen_fa (cursos_id_cursos, facilitadores_id_fa, ctf_id_cofa, ctf_fecha) VALUES ('" + id_curso + "', '" + fa.id_facilitador + "', '" + Cofa.id_facilitador + "', '" + dtpFechaCurso.Value.ToString("yyyy-MM-dd") + "')");
                                        FacilitadorCurso.Close();
                                        guardar = true;
                                        MessageBox.Show("Los datos se han agregado correctamente.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.None);
                                    }
                                    else // sino, solo guarda al facilitador
                                    {
                                        MySqlDataReader FacilitadorCurso = Conexion.ConsultarBD("INSERT INTO cursos_tienen_fa (cursos_id_cursos, facilitadores_id_fa, ctf_fecha, ctf_id_cofa) VALUES ('" + id_curso + "', '" + fa.id_facilitador + "', '" + dtpFechaCurso.Value.ToString("yyyy-MM-dd") + "', '0')");
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
                int id_curso = 0;

                if (Formaciones.creacion == true)
                {
                    //se obtiene el id del curso
                    MySqlDataReader obtener_id_curso = Conexion.ConsultarBD("SELECT id_cursos FROM cursos WHERE nombre_curso = '" + txtNombreFormacion.Text + "' AND tipo_curso='InCompany'  AND estatus_curso='En curso' AND duracion_curso=" + formacion.duracion + " AND id_usuario1='" + formacion.id_user + "' and solicitud_curso='" + txtSolicitadoPor.Text + "'");
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
                                                        
                           
                            string aula = txtAula.Text;

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

                            ////se actualiza la informacion del curso con los valores nuevos: 

                            if (id_refrigerio > 0)
                            {
                                MySqlDataReader ActualizarCursoTerceraEtapa = Conexion.ConsultarBD("UPDATE cursos SET etapa_curso ='3', horario_uno='" + id_horario + "', aula_dia1='" + aula + "', id_ref1='" + id_refrigerio + "' WHERE id_cursos='" + id_curso + "'");
                                ActualizarCursoTerceraEtapa.Close();
                            }
                            else
                            {
                                MySqlDataReader ActualizarCursoTerceraEtapa = Conexion.ConsultarBD("UPDATE cursos SET etapa_curso ='3', horario_uno='" + id_horario + "', aula_dia1='" + aula + "' WHERE id_cursos='" + id_curso + "'");
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
        #endregion

        #region modificar
        private void Modificar_intermedio()
        {
            //eliminará la difusion existentes y más abajo los añadirá para evitrar dublicados
            MySqlDataReader del = Conexion.ConsultarBD("delete from cursos_tiene_publicidad where ctp_id_curso='" + Cursos.id_curso13 + "'");
            del.Close();

            GuardarIntermedio();

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
        }

        private void Modificar_avanzado()
        {
            //eliminará los insumos existentes y más abajo los añadirá para evitrar dublicados
            MySqlDataReader del = Conexion.ConsultarBD("delete from cursos_tienen_insumos where cti_id_curso='" + Cursos.id_curso13 + "'");
            del.Close();

            GuardarAvanzado();
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
        #endregion
        /*--------------------Botones del panel lateral derecho------------------------*/
        #region PANEL LATERAL
        private void btnSiguienteEtapa_Click(object sender, EventArgs e)
        {
            guardar = false;
            if (Formaciones.creacion == true)
            {
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

                    if (AFI.id_AFI == 0)
                        MessageBox.Show("Ningun facilitador ha colaborado antes en esta formación.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
                   if (formacion.duracion == "8" && formacion.bloque_curso == "1")
                    {
                        Controles_nivel_intermedio_EstatusInicial();
                        gpbRefrigerio.Enabled = true;
                        
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
                    else if (Cursos.etapa_formacion13 == 2)
                    {
                        btnSiguienteEtapa.Enabled = true;
                        btnRetomar.Enabled = true;
                        btnPausar.Enabled = false;
                        btnModificar.Enabled = false;
                        //--campos desactivados (etapa intermedio)
                        deshabilitarControlesIntermedio();
                        btnCorreoAdministracion.Enabled = true;
                        btnCorreoComercializacion.Enabled = true;
                    }
                    else if (Cursos.etapa_formacion13 == 3)
                    {
                        deshabilitarControlesIntermedio();
                        btnSiguienteEtapa.Enabled = true;
                        btnRetomar.Enabled = false;
                        btnPausar.Enabled = false;
                        btnModificar.Enabled = false;
                        btnCorreoAdministracion.Enabled = true;
                        btnCorreoComercializacion.Enabled = true;
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
                        habilitarIntermedio();
                    }
                    else if (Cursos.etapa_formacion13 == 2)
                    {
                        cmbxTipoRefrigerio.SelectedIndex = -1;
                        btnSiguienteEtapa.Enabled = true;
                        btnRetomar.Enabled = true;
                        btnPausar.Enabled = false;
                        btnModificar.Enabled = false;
                        inicioE3 = DateTime.Now;
                        deshabilitarControlesIntermedio();
                        habilitarAvanzado();
                    }
                    else if (Cursos.etapa_formacion13 == 3)
                    {
                        btnSiguienteEtapa.Enabled = false;
                        btnRetomar.Enabled = true;
                        btnPausar.Enabled = false;
                        btnModificar.Enabled = false;
                        deshabilitarControlesAvanzado();

                    }

                    if (Cursos.duracion_formacion13 == "8")
                    {
                        
                        if (rdbSiRef.Checked == false)
                        {
                            gpbSeleccionRef.Enabled = false;
                            gpbSeleccionRef.Visible = true;
                        }
                        else
                        {
                            gpbSeleccionRef.Enabled = false;
                            gpbSeleccionRef.Visible = true;
                        }

                    }
                    pnlNivel_intermedio.Visible = false;
                    pnlNivel_avanzado.Visible = true;

                }

            }

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
                        
                        String duracionE2 = Convert.ToString(FinalE2 - inicioE2);
                        //se actualiza el tiempo en el que se trabajó en la formacion
                        MySqlDataReader update = Conexion.ConsultarBD("UPDATE cursos SET duracionE2='" + duracionE2 + "' where id_cursos='" + formacion.id_curso + "'");
                        update.Close();
                        if (guardar == true)
                        {
                            btnSiguienteEtapa.Enabled = true;
                            btnPausar.Enabled = true;
                            btnLimpiar.Enabled = true;

                            btnModificar.Enabled = false;
                            btnRetomar.Enabled = false;
                            btnGuardar.Enabled = false;

                            btnCorreoAdministracion.Enabled = true;
                            btnCorreoComercializacion.Enabled = true;
                        }
                    }
                    else
                    {
                        if (pnlNivel_avanzado.Visible == true)
                        {
                            FinalE3 = DateTime.Now;
                            GuardarAvanzado();
                            
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
                    btnGuardar.Enabled = false;
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
                    btnRutaBitacora.Enabled = true;                   
                    btnVerContenido.Enabled = false;                    
                    btnVerPresentacion.Enabled = false;
                    btnVerBitacora.Enabled = false;

                    //comportamiento del panel lateral:
                    btnGuardar.Enabled = false;
                    btnLimpiar.Enabled = false;
                    btnRetomar.Enabled = false;

                    btnPausar.Enabled = true;
                    btnModificar.Enabled = true;
                    btnSiguienteEtapa.Enabled = true;

                    fecha_creacion = DateTime.Now;

                    if (Cursos.etapa_formacion13 == 1)
                    {
                        btnSiguienteEtapa.Enabled = true;
                        if (AFI.id_AFI == 0)
                        {
                            llenarcomboFacilitador();
                        }

                    }
                    else if (Cursos.etapa_formacion13 == 2)
                    {

                    }
                    else
                    {
                        btnSiguienteEtapa.Enabled = true;
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

                    }
                    else if (Cursos.etapa_formacion13 == 2)
                    {
                        btnSiguienteEtapa.Enabled = false;
                        habilitarIntermedio();
                        btnCorreoComercializacion.Enabled = false; // se deshabilita hasta que modifique en algo
                        btnCorreoAdministracion.Enabled = false; // se deshabilita hasta que modifique en algo
                    }
                    else
                    {
                        btnSiguienteEtapa.Enabled = true;
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
                    
                    if (Cursos.tiene_ref == "No")
                    {
                        gpbSeleccionRef.Enabled = false;
                    }else
                    {
                        gpbSeleccionRef.Enabled = true;
                    }
                    
                }
            }
            
        }
        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            vaciarFormacion();
            btnSiguienteEtapa.Enabled = false;
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (pnlNivel_basico.Visible == true)
            {
                if (Cursos.etapa_formacion13 == 1)
                {
                    if (txtNombreFormacion.Text != Cursos.nombre_formacion13 || txtSolicitadoPor.Text != Cursos.solicitud_formacion13 || formacion.duracion != Cursos.duracion_formacion13 || formacion.bloque_curso != Cursos.bloque_curso13)
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
                                            if (btnVerPresentacion.Enabled == false)
                                            {
                                                p_inst.presentacion = "";
                                            }
                                            if (btnVerBitacora.Enabled == false)
                                            {
                                                p_inst.bitacora = "";
                                            }
                                             p_inst.manual = "";
                                            

                                            formacion.nombre_formacion = txtNombreFormacion.Text;
                                            formacion.tipo_formacion = "FEE";
                                            formacion.solicitado = txtSolicitadoPor.Text;
                                            MessageBox.Show(formacion.solicitado);
                                            int id_solicitud = 0;
                                            MySqlDataReader idS = Conexion.ConsultarBD("select id_clientes from clientes where nombre_empresa='" + formacion.solicitado + "'");
                                            if (idS.Read())
                                            {
                                                id_solicitud = Convert.ToInt32(idS["id_clientes"]);
                                            }
                                            idS.Close();
                                            

                                            FinalE1 = DateTime.Now; //agregar en fecha_mod_final en UGC
                                            formacion.fecha_inicial = fecha_creacion;// agregar fecha_mod_inicio
                                            formacion.TiempoEtapa = Convert.ToString(FinalE1 - fecha_creacion); //para añadir a la duracionE1

                                            
                                            formacion.id_user = Clases.Usuario_logeado.id_usuario;
                                            formacion.etapa_curso = 1;//representa la etapa actual: nivel_basico (cambiará para cada panel)
                                            
                                            MySqlDataReader e1 = Conexion.ConsultarBD("SELECT duracionE1 from cursos where id_cursos='" + Cursos.id_curso13 + "'");
                                            if (e1.Read())
                                            {
                                                string duracion = Convert.ToString(e1["duracionE1"]);

                                                TimeSpan et1;
                                                et1 = TimeSpan.Parse(duracion);
                                                
                                                TimeSpan tt = TimeSpan.Parse(formacion.TiempoEtapa);

                                                formacion.TiempoEtapa = (tt + et1).ToString();
                                                //formacion.TiempoEtapa= Convert.ToString();
                                               
                                            }
                                            e1.Close();
                                            MySqlDataReader update = Conexion.ConsultarBD("UPDATE cursos SET nombre_curso='" + formacion.nombre_formacion + "', duracion_curso='" + formacion.duracion + "', bloque_curso='" + formacion.bloque_curso + "', solicitud_curso='" + formacion.solicitado + "', duracionE1='" + formacion.TiempoEtapa + "' where id_cursos='" + Cursos.id_curso13 + "'");
                                            update.Close();
                                            MySqlDataReader clientes_solicitan_cursos = Conexion.ConsultarBD("update clientes_solicitan_cursos set id_cliente1='" + id_solicitud + "' where id_curso1='" + Cursos.id_curso13 + "'");
                                            clientes_solicitan_cursos.Close();
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
                    }
                    else
                    {
                        MessageBox.Show("No se han detectado cambios.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            else if (pnlNivel_intermedio.Visible == true)
            {
                if (Cursos.etapa_formacion13 == 1)
                {
                    GuardarIntermedio();

                    FinalE2 = DateTime.Now;

                    formacion.TiempoEtapa = Convert.ToString(FinalE2 - inicioE2);
                    //se actualiza el tiempo en el que se trabajó en la formacion
                    MySqlDataReader update = Conexion.ConsultarBD("UPDATE cursos SET duracionE3='" + formacion.TiempoEtapa + "' where id_cursos='" + Cursos.id_curso13 + "'");
                    update.Close();

                    //se agrega la modificacion en la tabla
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

                }
                else if (Cursos.etapa_formacion13 == 2)
                {

                    if (formacion.ubicacion_ucs != Cursos.ubicacion_ucs || formacion.tiene_ref != Cursos.tiene_ref || dtpFechaCurso.Value.ToString("yyyy-MM-dd") != Cursos.fecha_uno13 || cmbxFa.Text != fcombo.nombreyapellido1 || cmbxCoFa.Text != cf.nombreyapellido1)
                    {
                        Modificar_intermedio();
                       
                    }

                }
                else if (pnlNivel_avanzado.Visible == true)
                {
                    if (Cursos.etapa_formacion13 == 2)
                    {
                        GuardarAvanzado();
                        FinalE3 = DateTime.Now;
                        formacion.TiempoEtapa = Convert.ToString(FinalE3 - inicioE3);
                        //se actualiza el tiempo en el que se trabajó en la formacion
                        MySqlDataReader update = Conexion.ConsultarBD("UPDATE cursos SET duracionE3='" + formacion.TiempoEtapa + "' where id_cursos='" + Cursos.id_curso13 + "'");
                        update.Close();

                        //se agrega la modificacion en la tabla
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
                    else if (Cursos.etapa_formacion13 == 3)
                    {

                        if (formacion.ubicacion_ucs == "Si")
                        {
                            if (formacion.tiene_ref == "Si")
                            {

                                if (lista_insumo_cargada != lista_insumo || formacion.horario1 != Cursos.horario1 || txtAula.Text != formacion.aula1 || formacion.refri1 != Cursos.tipo_ref1)
                                {
                                    Modificar_avanzado();
                                }


                            }
                            else //si No tiene refrigerio
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
        }

        #endregion
        /* ------------- Controles del nivel básico -------------------*/
        #region ETAPA BASICA EVENTOS
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
                    formacion.estatus = "En curso"; //predeterminado en esta etapa
                    formacion.tipo_formacion = "FEE"; //predeterminado para este form
                    formacion.nombre_formacion = txtNombreFormacion.Text;
                    conexion.cerrarconexion();
                    
                    //pero se busca cualquier concordancia de nombre en otros tipos de curso para el paquete instruccional
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
                            btnRutaBitacora.Enabled = true;
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
        #endregion
        /*-------------------- Controles del Nivel_intermedio ------------------------*/
        #region ETAPA INTERMEDIA EVENTOS
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
               // MessageBox.Show(fa.id_facilitador + " idFa true ");
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
               // MessageBox.Show(fa.id_facilitador + " idFa y idafi" + AFI.id_AFI);


            }
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
            if (Cofa.id_facilitador != 0)
            {
                //validar si estará disponible para la fecha del dia 1
                if (conexion.abrirconexion() == true)
                {
                    int fa_disponible = Clases.Facilitadores.FacilitadorDisponibleDia(conexion.conexion, time.fecha_curso, Cofa.id_facilitador);
                    conexion.cerrarconexion();
                    if (fa_disponible != 0)//si el id que retorna es el del facilitador seleccionado: este ya está ocupado para esa fecha
                    {
                        errorProviderPresentacion.SetError(cmbxCoFa, "El Co-facilitador está ocupado en la fecha seleccionada.");
                        cmbxCoFa.SelectedIndex = -1;
                        Cofa.id_facilitador = 0;
                        //cmbxCoFa.Focus();
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

        private void rdbNoRef_CheckedChanged(object sender, EventArgs e)
        {
            if (Formaciones.creacion == true)
                inicioE2 = DateTime.Now;

            formacion.tiene_ref = "No";
        }

        private void rdbSiRef_CheckedChanged(object sender, EventArgs e)
        {
            if (Formaciones.creacion == true)
                inicioE2 = DateTime.Now;
            formacion.tiene_ref = "Si";
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

        #endregion

        /*-----------------Controles Nivel Avanzado--------------------------------*/
        #region ETAPA AVANZADA EVENTOS
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

        private void cmbxHorarios_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if(Formaciones.creacion==true)
                inicioE3 = DateTime.Now;

            id_horario = Convert.ToInt32(cmbxHorarios.SelectedIndex);       
        }

        private void cmbxTipoRefrigerio_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (Formaciones.creacion == true)
            {
                id_refrigerio = Convert.ToInt32(cmbxTipoRefrigerio.SelectedValue);
                
                MySqlDataReader nombre = Conexion.ConsultarBD("SELECT id_ref from refrigerios where ref_nombre='" + cmbxTipoRefrigerio.Text + "'");
                if (nombre.Read())
                {
                    //MessageBox.Show(nombre["id_ref"].ToString());
                    id_refrigerio = Convert.ToInt32(nombre["id_ref"]);
                }

            }
            else
            {
                MySqlDataReader nombre = Conexion.ConsultarBD("SELECT id_ref from refrigerios where ref_nombre='" + cmbxTipoRefrigerio.Text + "'");
                if (nombre.Read())
                {
                    //MessageBox.Show(nombre["id_ref"].ToString());
                    id_refrigerio = Convert.ToInt32(nombre["id_ref"]);
                }
            }

            formacion.refri1 = cmbxTipoRefrigerio.Text;

        }
        #endregion
    }
}
