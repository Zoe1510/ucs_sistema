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
        List<string> lista_insumo = new List<string>();

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
        public Nueva_formacion_InCompany()
        {
            InitializeComponent();
        }

        private void Nueva_formacion_InCompany_Load(object sender, EventArgs e)
        {
            int i = 0;
            llenarcmbxEmpresas(i);
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



                //cargar los facilitadores del nivel_intermedio
                llenarcomboFacilitador();

                llenarcomboRefrigerio();

            } else //si viene referenciado de modificar formacion            
            {


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
                    evaluarAFI(); //esto es para llenar el combobox con los facilitadores que pueden dar la formacion
                }
                else if(Cursos.etapa_formacion13 == 2)
                {
                    btnRetomar.Enabled = false;
                    btnSiguienteEtapa.Enabled = true;
                    
                    CargarDatosEtapaUno();
                    
                    CargarDatosEtapaDos();
                    

                }else if (Cursos.etapa_formacion13 == 3)
                {
                    btnRetomar.Enabled = false;
                    btnSiguienteEtapa.Enabled = true;
                    CargarDatosEtapaUno();
                    CargarDatosEtapaDos();
                    CargarDatosEtapaTres();

                }



            }
                
        }
        /*---------------------- METODOS ------------------------*/
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

        private void CargarDatosEtapaUno()
        {

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
                    break;
                case "8 Horas":
                    cmbxDuracionFormacion.SelectedIndex = 1;
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

            if (Cursos.p_bitacora == "")
            {
                btnVerBitacora.Enabled = false;
            }
            else
            {
                bitacora = Cursos.p_bitacora;
                btnVerBitacora.Enabled = true;
            }

            if (Cursos.p_manual == "")
            {
                btnVerManual.Enabled = false;
            }
            else
            {
                manual = Cursos.p_manual;
                btnVerManual.Enabled = true;
            }

        }
        private void CargarDatosEtapaDos()
        {
            //llenar dato de ubicacion
            if(Cursos.ubicacion_ucs == "Si")
            {
                rdbInstalaciones.Checked = true;
            }else
            {
                rdbNoInstalaciones.Checked = true;
            }

            //llenar datos del refrigerio
            if(Cursos.tiene_ref == "Si")
            {
                rdbSiRef.Checked = true;
            }else
            {
                rdbNoRef.Checked = true;
            }
            //llenar fechas
            dtpFechaCurso.Value = Convert.ToDateTime(Cursos.fecha_uno13);
            if(Cursos.bloque_curso13 == "2")
            {
                dtpSegundaFecha.Value = Convert.ToDateTime(Cursos.fecha_dos13);
            }else
            {
                dtpSegundaFecha.Enabled = false;
            }
            evaluarAFI();
            cmbxFa.ValueMember = "id_faci";
            cmbxFa.DisplayMember = "nombreyapellido1";
            cmbxFa.DataSource = Paneles.LlenarCmbxfa_AFI(AFI.id_AFI);
            cmbxFa.SelectedIndex = -1;

            //seleccionar facilitador encargado del curso 
            int id_fa = 0, co_fa=0;
            string nombreyapellido1 = "";
            //buscar id_fa de acuerdo al id del curso
            MySqlDataReader leer = Conexion.ConsultarBD("SELECT * from cursos_tienen_fa where cursos_id_cursos = '" + Cursos.id_curso13 + "'");
            if (leer.Read())
            {
                id_fa = Convert.ToInt32(leer["facilitadores_id_fa"]);
                co_fa = Convert.ToInt32(leer["ctf_id_cofa"]);
            }
            leer.Close();
            Facilitador_todos f = new Facilitador_todos();
            //recoger informacion del facilitador
            MySqlDataReader nom = Conexion.ConsultarBD("select * from facilitadores where id_fa='" + id_fa + "'");
            while (nom.Read())
            {
                
                f.nombre_facilitador = nom["nombre_fa"].ToString();
                f.apellido_facilitador = nom["apellido_fa"].ToString();
                f.correo_facilitador = nom["correo_fa"].ToString();
                f.tlfn_facilitador = nom["tlfn_fa"].ToString();
                nombreyapellido1 = f.nombre_facilitador + " " + f.apellido_facilitador;
                MessageBox.Show(nombreyapellido1);
            }
            //recorremos el comboboxFa
            for (int i= 0; i<cmbxFa.Items.Count; i++)
            {
                if(cmbxFa.Items[i].ToString() == nombreyapellido1)
                {
                    cmbxFa.SelectedItem = cmbxFa.Items[i];
                    txtCorreoFa.Text = f.correo_facilitador;
                    txtTlfnFa.Text = f.tlfn_facilitador;
                }
            }
            //evaluar si esa formacion tiene co facilitador
            if(co_fa != 0)
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
                //recorremos el comboboxFa
                for (int i = 0; i < cmbxCoFa.Items.Count; i++)
                {
                    if (cmbxCoFa.Items[i].ToString() == cf.nombreyapellido1)
                    {
                        cmbxCoFa.SelectedItem = cmbxCoFa.Items[i];
                        txtCorreoFa.Text = cf.correo_facilitador;
                        txtTlfnFa.Text = cf.tlfn_facilitador;
                    }
                }
            }
        }
        private void CargarDatosEtapaTres()
        {

        }

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
                                    FinalE2 = DateTime.Now;
                                    String duracionE2 = Convert.ToString(FinalE2 - inicioE2);

                                    int id_curso = 0;
                                    //se obtiene el id del curso
                                    MySqlDataReader obtener_id_curso = Conexion.ConsultarBD("SELECT id_cursos FROM cursos WHERE nombre_curso = '" + txtNombreFormacion.Text + "' AND tipo_curso='InCompany'");
                                    if (obtener_id_curso.Read())
                                    {
                                        id_curso = int.Parse(obtener_id_curso["id_cursos"].ToString());
                                    }
                                    obtener_id_curso.Close();

                                    formacion.bloque_curso = "1";

                                    MySqlDataReader ActualizarCursoTieneRef = Conexion.ConsultarBD("UPDATE cursos SET tiene_ref='0' WHERE id_cursos='" + id_curso + "'");
                                    ActualizarCursoTieneRef.Close();

                                    //SI SE HACE EN LA UCS ENTONCES
                                    if (rdbInstalaciones.Checked == true)
                                    {
                                        MySqlDataReader update = Conexion.ConsultarBD("UPDATE cursos SET ubicacion_ucs='Si' WHERE id_cursos='" + id_curso + "'");
                                        update.Close();
                                    }
                                    else  //SI NO SE HACE EN LA UCS
                                    {
                                        MySqlDataReader update = Conexion.ConsultarBD("UPDATE cursos SET ubicacion_ucs='No' WHERE id_cursos='" + id_curso + "'");
                                        update.Close();
                                        gpbAula.Enabled = false;
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
                                        MySqlDataReader FacilitadorCurso = Conexion.ConsultarBD("INSERT INTO cursos_tienen_fa (cursos_id_cursos, facilitadores_id_fa, ctf_fecha, ctf_id_cofa) VALUES ('" + id_curso + "', '" + fa.id_facilitador + "', '" + time.fecha_curso + "', '0')");
                                        FacilitadorCurso.Close();
                                        guardar = true;
                                        MessageBox.Show("Los datos se han agregado correctamente.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.None);
                                    }

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
                                    gpbSeleccionRef.Enabled = false;
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
                                    FinalE2 = DateTime.Now;
                                    String duracionE2 = Convert.ToString(FinalE2 - inicioE2);

                                    int id_curso = 0;
                                    //se obtiene el id del curso
                                    MySqlDataReader obtener_id_curso = Conexion.ConsultarBD("SELECT id_cursos FROM cursos WHERE nombre_curso = '" + txtNombreFormacion.Text + "' AND tipo_curso='InCompany'");
                                    if (obtener_id_curso.Read())
                                    {
                                        id_curso = int.Parse(obtener_id_curso["id_cursos"].ToString());
                                    }
                                    obtener_id_curso.Close();

                                    formacion.bloque_curso = "2";
                                    gpbSeleccionRef.Enabled = false;
                                    rdbNoIgualHorario.Enabled = true;
                                    rdbSiIgualHorario.Enabled = true;
                                    MySqlDataReader ActualizarCursoTieneRef = Conexion.ConsultarBD("UPDATE cursos SET tiene_ref='0' WHERE id_cursos='" + id_curso + "'");
                                    ActualizarCursoTieneRef.Close();

                                    //SI SE HACE EN LA UCS ENTONCES
                                    if (rdbInstalaciones.Checked == true)
                                    {
                                        MySqlDataReader update = Conexion.ConsultarBD("UPDATE cursos SET ubicacion_ucs='Si' WHERE id_cursos='" + id_curso + "'");
                                        update.Close();
                                        gpbAula.Enabled = true;
                                    }
                                    else //SI NO SE HACE EN LA UCS
                                    {
                                        MySqlDataReader update = Conexion.ConsultarBD("UPDATE cursos SET ubicacion_ucs='No' WHERE id_cursos='" + id_curso + "'");
                                        update.Close();
                                        gpbAula.Enabled = false;
                                    }

                                    //se actualiza el curso con los datos obtenidos de la segunda etapa, como las fechas
                                    MySqlDataReader ActualizarCursoSegundaEtapa = Conexion.ConsultarBD("UPDATE cursos SET etapa_curso ='2', fecha_uno='" + time.fecha_curso + "',fecha_dos='" + time.fechaDos_curso + "', duracionE2='" + duracionE2 + "', bloque_curso= '" + formacion.bloque_curso + "' WHERE id_cursos='" + id_curso + "'");
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
                                        MySqlDataReader FacilitadorCurso = Conexion.ConsultarBD("INSERT INTO cursos_tienen_fa (cursos_id_cursos, facilitadores_id_fa, ctf_fecha, ctf_fecha2, ctf_id_cofa) VALUES ('" + id_curso + "', '" + fa.id_facilitador + "', '" + time.fecha_curso + "', '" + time.fechaDos_curso + "', '0')");
                                        FacilitadorCurso.Close();
                                        guardar = true;
                                        MessageBox.Show("Los datos se han agregado correctamente.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.None);
                                    }

                                    //aqui estaran contemplados los arreglos (visuales y de contenido) de la siguiente etapa
                                    ordenTerceraEtapa();
                                    gpbHorarioCurso.Height = 169;
                                    gpbSeleccionRef.Visible = false;
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
                                    FinalE2 = DateTime.Now;
                                    String duracionE2 = Convert.ToString(FinalE2 - inicioE2);

                                    int id_curso = 0;
                                    //se obtiene el id del curso
                                    MySqlDataReader obtener_id_curso = Conexion.ConsultarBD("SELECT id_cursos FROM cursos WHERE nombre_curso = '" + txtNombreFormacion.Text + "' AND tipo_curso='InCompany'");
                                    if (obtener_id_curso.Read())
                                    {
                                        id_curso = int.Parse(obtener_id_curso["id_cursos"].ToString());
                                    }
                                    obtener_id_curso.Close();

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
                                                MySqlDataReader FacilitadorCurso = Conexion.ConsultarBD("INSERT INTO cursos_tienen_fa (cursos_id_cursos, facilitadores_id_fa, ctf_fecha, ctf_id_cofa) VALUES ('" + id_curso + "', '" + fa.id_facilitador + "', '" + time.fecha_curso + "', '0')");
                                                FacilitadorCurso.Close();
                                                guardar = true;
                                                MessageBox.Show("Los datos se han agregado correctamente.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.None);
                                            }

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
                                        MySqlDataReader ActualizarCursoSegundaEtapa = Conexion.ConsultarBD("UPDATE cursos SET etapa_curso ='2', fecha_uno='" + time.fecha_curso + "', duracionE2='" + duracionE2 + "', bloque_curso= '" + formacion.bloque_curso + "' WHERE id_cursos='" + id_curso + "'");
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
                                            MySqlDataReader FacilitadorCurso = Conexion.ConsultarBD("INSERT INTO cursos_tienen_fa (cursos_id_cursos, facilitadores_id_fa, ctf_fecha, ctf_fecha2, ctf_id_cofa) VALUES ('" + id_curso + "', '" + fa.id_facilitador + "', '" + time.fecha_curso + "', '" + time.fechaDos_curso + "', '0')");
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
                                    FinalE2 = DateTime.Now;
                                    String duracionE2 = Convert.ToString(FinalE2 - inicioE2);

                                    int id_curso = 0;
                                    //se obtiene el id del curso
                                    MySqlDataReader obtener_id_curso = Conexion.ConsultarBD("SELECT id_cursos FROM cursos WHERE nombre_curso = '" + txtNombreFormacion.Text + "' AND tipo_curso='InCompany'");
                                    if (obtener_id_curso.Read())
                                    {
                                        id_curso = int.Parse(obtener_id_curso["id_cursos"].ToString());
                                    }
                                    obtener_id_curso.Close();

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
                                            MySqlDataReader ActualizarCursoSegundaEtapa = Conexion.ConsultarBD("UPDATE cursos SET etapa_curso ='2', fecha_uno='" + time.fecha_curso + "',fecha_dos='" + time.fechaDos_curso + "', duracionE2='" + duracionE2 + "', bloque_curso= '" + formacion.bloque_curso + "' WHERE id_cursos='" + id_curso + "'");
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
                                                MySqlDataReader FacilitadorCurso = Conexion.ConsultarBD("INSERT INTO cursos_tienen_fa (cursos_id_cursos, facilitadores_id_fa, ctf_fecha, ctf_fecha2, ctf_id_cofa) VALUES ('" + id_curso + "', '" + fa.id_facilitador + "', '" + time.fecha_curso + "', '" + time.fechaDos_curso + "', '0')");
                                                FacilitadorCurso.Close();
                                                guardar = true;
                                                MessageBox.Show("Los datos se han agregado correctamente.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.None);
                                            }

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
                                        MySqlDataReader ActualizarCursoSegundaEtapa = Conexion.ConsultarBD("UPDATE cursos SET etapa_curso ='2', fecha_uno='" + time.fecha_curso + "',fecha_dos='" + time.fechaDos_curso + "', duracionE2='" + duracionE2 + "', bloque_curso= '" + formacion.bloque_curso + "' WHERE id_cursos='" + id_curso + "'");
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
                                            MySqlDataReader FacilitadorCurso = Conexion.ConsultarBD("INSERT INTO cursos_tienen_fa (cursos_id_cursos, facilitadores_id_fa, ctf_fecha, ctf_fecha2, ctf_id_cofa) VALUES ('" + id_curso + "', '" + fa.id_facilitador + "', '" + time.fecha_curso + "', '" + time.fechaDos_curso + "', '0')");
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
                //se obtiene el id del curso
                MySqlDataReader obtener_id_curso = Conexion.ConsultarBD("SELECT id_cursos FROM cursos WHERE nombre_curso = '" + txtNombreFormacion.Text + "' AND tipo_curso='InCompany'");
                if (obtener_id_curso.Read())
                {
                    id_curso = int.Parse(obtener_id_curso["id_cursos"].ToString());
                }
                obtener_id_curso.Close();

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

                MessageBox.Show(ubicacion.ToString() + id_curso.ToString());

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
                        string aula = "";
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
                        }
                        else
                        {

                        }


                        if (lista_insumo.Count <= 0)
                        {
                            errorProviderContenido.SetError(dgvInsumos, "Debe seleccionar los insumos que se usarán durante la formación.");

                        }
                        else
                        {
                            errorProviderContenido.SetError(dgvInsumos, "");

                            DateTime FinalE3 = DateTime.Now;
                            String duracionE3 = Convert.ToString(FinalE3 - inicioE3);
                            

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
                            //se actualiza el curso con los datos obtenidos de la segunda etapa, como las fechas
                            MySqlDataReader ActualizarCursoTerceraEtapa = Conexion.ConsultarBD("UPDATE cursos SET etapa_curso ='3', duracionE3='" + duracionE3 + "', horario_uno='" + id_horario + "', aula_dia1='" + aula + "' WHERE id_cursos='" + id_curso + "'");
                            ActualizarCursoTerceraEtapa.Close();
                            MessageBox.Show("Los datos se han agregado correctamente.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.None);



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
                            DateTime FinalE3 = DateTime.Now;
                            String duracionE3 = Convert.ToString(FinalE3 - inicioE3);
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

                                

                            }
                            string aula = "";
                            string aula2 ="";
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
                                if (rdbSiMantenerAula.Checked==true)
                                {
                                    aula2 = aula;
                                }
                                else
                                {
                                    aula2 = txtSegundaAula.Text;
                                }
                                ////se actualiza la informacion del curso con los valores nuevos: 
                                //se actualiza el curso con los datos obtenidos de la segunda etapa, como las fechas
                                MySqlDataReader ActualizarCursoTerceraEtapa = Conexion.ConsultarBD("UPDATE cursos SET etapa_curso ='3', duracionE3='" + duracionE3 + "', horario_uno='" + id_horario + "', horario_dos='" + id_horario2 + "', aula_dia1='" + aula + "', aula_dia2='" + aula2 + "' WHERE id_cursos='" + id_curso + "'");
                                ActualizarCursoTerceraEtapa.Close();
                                MessageBox.Show("Los datos se han agregado correctamente.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.None);

                            }
                            else //NO SE DICTA EN UCS
                            {
                                MySqlDataReader ActualizarCursoTerceraEtapa = Conexion.ConsultarBD("UPDATE cursos SET etapa_curso ='3', duracionE3='" + duracionE3 + "', horario_uno='" + id_horario + "', horario_dos='" + id_horario2 + "' WHERE id_cursos='" + id_curso + "'");
                                ActualizarCursoTerceraEtapa.Close();
                                MessageBox.Show("Los datos se han agregado correctamente.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.None);

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
                        DateTime FinalE3 = DateTime.Now;
                        String duracionE3 = Convert.ToString(FinalE3 - inicioE3);
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
                            else//si no se dicta en la uics
                            {
                                //se actualiza el curso con los datos obtenidos de la segunda etapa, como las fechas
                                MySqlDataReader ActualizarCursoTerceraEtapa = Conexion.ConsultarBD("UPDATE cursos SET etapa_curso ='3', duracionE3='" + duracionE3 + "', horario_uno='" + id_horario + "', aula_dia1='" + aula + "' WHERE id_cursos='" + id_curso + "'");
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

                                DateTime FinalE3 = DateTime.Now;
                                String duracionE3 = Convert.ToString(FinalE3 - inicioE3);

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
                                                MySqlDataReader ActualizarCursoTerceraEtapa = Conexion.ConsultarBD("UPDATE cursos SET etapa_curso ='3', duracionE3='" + duracionE3 + "', horario_uno='" + id_horario + "', aula_dia1='" + aula + "', id_ref1='" + id_refrigerio + "' WHERE id_cursos='" + id_curso + "'");
                                                ActualizarCursoTerceraEtapa.Close();
                                            }
                                            else
                                            {
                                                MySqlDataReader ActualizarCursoTerceraEtapa = Conexion.ConsultarBD("UPDATE cursos SET etapa_curso ='3', duracionE3='" + duracionE3 + "', horario_uno='" + id_horario + "', aula_dia1='" + aula + "' WHERE id_cursos='" + id_curso + "'");
                                                ActualizarCursoTerceraEtapa.Close();
                                                MessageBox.Show("Los datos se han agregado correctamente.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.None);
                                            }

                                        }
                                    }
                                }
                                else
                                {
                                    MySqlDataReader ActualizarCursoTerceraEtapa = Conexion.ConsultarBD("UPDATE cursos SET etapa_curso ='3', duracionE3='" + duracionE3 + "', horario_uno='" + id_horario + "', horario_dos='" + id_horario2 + "' WHERE id_cursos='" + id_curso + "'");
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
        /*---------------------- Panel lateral derecho ------------------------*/
        private void btnSiguienteEtapa_Click(object sender, EventArgs e)
        {
            guardar = false;
            //cuando Siguiente etapa, quita el panel actual
            if (pnlNivel_basico.Visible == true)
            {
                if(Formaciones.creacion == true)
                {
                    LabelCabecera.Text = "Nuevo Incompany: Detalles técnicos";
                    LabelCabecera.Location = new Point(115, 31);
                }else
                {
                    LabelCabecera.Text = "" + Cursos.nombre_formacion13 + ": Información básica";
                    LabelCabecera.Location = new Point(115, 31);

                }
                

                lblEtapaSiguiente.Text = "Nivel Avanzado";
                lblEtapaSiguiente.Location = new Point(22, 529);
                lblEtapafinal.Text = "Añadir participantes";
                lblEtapafinal.Location = new Point(3, 570);

                pnlNivel_basico.Visible = false;

                //comportamiento del panel nivel_intermedio de acuerdo a la duracion del curso
                if (formacion.duracion == "4")
                {
                    Controles_nivel_intermedio_EstatusInicial();
                                         
                }
                else
                {
                    if (formacion.duracion == "8" && formacion.bloque_curso == "1")
                    {
                        Controles_nivel_intermedio_EstatusInicial();
                        
                    }
                    else if (formacion.duracion == "8" && formacion.bloque_curso == "2")
                    {
                        Controles_nivel_intermedio_EstatusInicial();
                        
                        if (Formaciones.creacion == true)
                        {
                            dtpSegundaFecha.Enabled = true;                          
                        }
                      
                    }
                    else
                    {
                        if (formacion.duracion == "16")
                        {
                            Controles_nivel_intermedio_EstatusInicial();                                                       
                            if (Formaciones.creacion == true)
                            {
                                dtpSegundaFecha.Enabled = true;
                            }
                            
                        }
                    }
                }

                pnlNivel_intermedio.Visible = true;

                if (Formaciones.creacion == true)
                    Load_Sig_Re();
                else
                {
                    btnGuardar.Enabled = false;                    
                    btnLimpiar.Enabled = false;
                    btnRetomar.Enabled = true;

                    if (Cursos.etapa_formacion13 == 1)
                    {
                        btnSiguienteEtapa.Enabled = false;
                        btnRetomar.Enabled = false;
                        btnPausar.Enabled = true;
                        btnModificar.Enabled = true;
                    }
                    else 
                    {
                        btnSiguienteEtapa.Enabled = true;
                        btnRetomar.Enabled = true;
                        btnPausar.Enabled = false;
                        btnModificar.Enabled = false;
                        //--campos desactivados (etapa intermedio)
                        deshabilitarControlesIntermedio();
                    }
                   
                }

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
                    if (Formaciones.creacion == true)
                        Load_Sig_Re();
                    else
                    {
                        LabelCabecera.Text = "Logística";
                        LabelCabecera.Location = new Point(250, 31);
                        btnGuardar.Enabled = false;
                        
                        btnLimpiar.Enabled = false;
                        btnRetomar.Enabled = true;

                        if (Cursos.etapa_formacion13 == 1)
                        {
                            btnSiguienteEtapa.Enabled = false;
                            btnRetomar.Enabled = false;
                            btnPausar.Enabled = true;
                            btnModificar.Enabled = true;
                        }
                        else
                        {
                            btnSiguienteEtapa.Enabled = true;
                            btnRetomar.Enabled = true;
                            btnPausar.Enabled = false;
                            btnModificar.Enabled = false;
                        }

                        if (Cursos.etapa_formacion13 == 3)
                        {
                            btnSiguienteEtapa.Enabled = false;
                        }
                        
                        
                    }

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
            if (pnlNivel_basico.Visible == true)
            {
                deshabiltarControlesBasico();
            }else if (pnlNivel_intermedio.Visible == true)
            {
                //se deshabilitará controles del nivel intermedio
                deshabilitarControlesIntermedio();
            }else if (pnlNivel_avanzado.Visible == true)
            {

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
                    btnVerContenido.Enabled= false;
                    btnVerManual.Enabled = false;
                    btnVerPresentacion.Enabled = false;
                    btnVerBitacora.Enabled = false;

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
                    habilitarIntermedio();

                    btnRetomar.Enabled = false;
                    btnPausar.Enabled = true;
                    btnModificar.Enabled = true;
                    
                    if (Cursos.etapa_formacion13 >= 2)
                        btnSiguienteEtapa.Enabled = true;
                    else
                        btnSiguienteEtapa.Enabled = false;

                }
                else if (pnlNivel_avanzado.Visible == true)
                {
                    btnSiguienteEtapa.Enabled = false;
                }
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
            btnGuardar.Enabled = true;
        }
        private void rdbInstalaciones_CheckedChanged(object sender, EventArgs e)
        {
            btnGuardar.Enabled = true;
            if(formacion.duracion == "8" && formacion.bloque_curso=="1")
            {
                gpbRefrigerio.Enabled = true;
            }else if(formacion.duracion == "16")
            {
                gpbRefrigerio.Enabled = true;
            }else
            {
                gpbRefrigerio.Enabled = false;
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
                cmbxCoFa.SelectedIndex = -1;
                txtTlfnCoFa.Clear();
                txtCorreoCoFa.Clear();
            }
        }
        private void cmbxFa_SelectionChangeCommitted(object sender, EventArgs e)
        {
            fa.id_facilitador = Convert.ToInt32(cmbxFa.SelectedValue);
            if (AFI.id_AFI == 0)
            {
                llenarComboCoFa(fa.id_facilitador);
            }
            else
            {
                llenarComboCOFA_AFI(AFI.id_AFI, fa.id_facilitador);
            }

        }

        private void cmbxFa_Validating(object sender, CancelEventArgs e)
        {
            //validar si este facilitador estará disponible para esa o esas fechas
            if ((formacion.duracion == "4") || (formacion.duracion == "8" && formacion.bloque_curso == "1"))
            {
                int fa_disponible = 0;
                //validar si estará disponible para la fecha del dia 1
                if (conexion.abrirconexion() == true)
                {
                    fa_disponible = Clases.Facilitadores.FacilitadorDisponibleDia(conexion.conexion, time.fecha_curso, fa.id_facilitador);
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
                        errorProviderPresentacion.SetError(cmbxFa, "");
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
            else if ((formacion.duracion == "16") || (formacion.duracion == "8" && formacion.bloque_curso == "2"))
            {
                int fa_disponible=0;
                //validar ambas fechas
                if (conexion.abrirconexion() == true)
                {
                    fa_disponible = Clases.Facilitadores.FacilitadorDisponibleDia(conexion.conexion, time.fecha_curso, fa.id_facilitador);
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
                        MessageBox.Show("El Co-facilitador está ocupado en la fecha seleccionada: " + time.fecha_curso + ".");
                        cmbxCoFa.SelectedIndex = -1;
                        Cofa.id_facilitador = 0;
                        cmbxCoFa.Focus();
                    }
                    else
                    {
                        errorProviderPresentacion.SetError(cmbxCoFa, "");
                        if(conexion.abrirconexion()==true)
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
           // cmbxHorario2.Items.Clear();
            cmbxHorario2.Text = cmbxHorarios.Text;
            cmbxHorario2.Enabled = false;
            id_horario2 = id_horario;
        }

        private void rdbNoIgualHorario_CheckedChanged(object sender, EventArgs e)
        {
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

        private void rdbSiMantenerAula_CheckedChanged(object sender, EventArgs e)
        {
            txtSegundaAula.Text = txtAula.Text;
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
            id_horario2 = Convert.ToInt32(cmbxHorario2.SelectedValue);
        }







    }
}
