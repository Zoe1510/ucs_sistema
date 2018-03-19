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
        List<string> lista_insumo = new List<string>();
        List<string> lista_insumo_cargada = new List<string>(); //esta es la lista de la base de datos
        bool ExisteFormacion;
        string presentacion = "";
        string contenido = "";
        string manual = "";
        string bitacora = "";

        int id_refrigerio, id_refrigerio2, id_horario, id_horario2;
        //string horario, horario2;

        DateTime fecha_creacion, inicioE2, inicioE3, FinalE1, FinalE2, FinalE3;
        Facilitador_todos fcombo = new Facilitador_todos();
        Facilitador_todos cf = new Facilitador_todos();
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
            //----------------------------EN GENERAL------------------------------------------------

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

            //estado inicial de los bloques de esta formación
            cmbxBloques.SelectedIndex = 0;
            cmbxBloques.Enabled = false;

            //estado inicial de la duracion de esta formación
            cmbxDuracionFormacion.SelectedIndex = 0;
            cmbxDuracionFormacion.Enabled = false;

            int i = 0;
            llenarcmbxEmpresas(i);//solicitado por

            llenarcomboRefrigerio();//refrigerio

            string tipo = "A";
            llenarComboHorario(tipo);

            formacion.bloque_curso = "2";
            formacion.etapa_curso = 1;
            formacion.tipo_formacion = "INCES";
            formacion.id_user = Usuario_logeado.id_usuario;
            formacion.duracion = "16";

            //-------FIN DE EN GENERAL-------

            if (Clases.Formaciones.creacion == true)//si viene referenciado del boton de la pagina principal
            {//------------------------------------------todo hay que hacerlo aquí(un nuevo ingreso)
                this.Location = new Point(-5, 0);

                fecha_creacion = DateTime.Now;
                
                LabelCabecera.Text = "Nuevo INCES: Información básica";
                LabelCabecera.Location = new Point(150, 31);

                lblEtapaSiguiente.Text = "Nivel Intermedio";
                lblEtapaSiguiente.Location = new Point(17, 529);

                lblEtapafinal.Text = "Nivel Avanzado";
                lblEtapafinal.Location = new Point(22, 570);

                // como estarán los botones inicialmente para cada nivel
                Load_Sig_Re();

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



                //controles del nivel intermedio
                Controles_nivel_intermedio_EstatusInicial();
                                           

               
            }else
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
                    

                }
                else if (Cursos.etapa_formacion13 == 2)
                {
                    gpbCorreos.Enabled = true;
                    
                    btnRetomar.Enabled = false;
                    btnSiguienteEtapa.Enabled = true;

                    CargarDatosEtapaUno();
                    CargarDatosEtapaDos();


                }
                else if (Cursos.etapa_formacion13 == 3)
                {
                    btnRetomar.Enabled = false;
                    btnSiguienteEtapa.Enabled = true;
                    CargarDatosEtapaUno();

                    CargarDatosEtapaDos();                    

                    CargarDatosEtapaTres();

                    
                }

            }
        }


        /*----------------- METODOS ----------------*/
        private void vaciarFormacion()
        {
            if (pnlNivel_basico.Visible == true)
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
                cmbxSegundoHorario.SelectedIndex = -1;
                rdbNoIgual.Checked = false;
                rdbNoMantenerAula.Checked = false;
                rdbNoMantenerRef.Checked = false;
                rdbNoRef.Checked = false;
                rdbSiIgual.Checked = false;
                rdbMantenerAula.Checked = false;
                rdbSiRef.Checked = false;
                cmbxTipoRefrigerio.SelectedIndex = -1;
                txtAula.Clear();
                txtSegundaAula.Clear();

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
        private void ordenTerceraEtapa()
        {
            gpbHorarioCurso.Location = new Point(254, 47);
            gpbHorarioCurso.Height = 73;

            gpbSeleccionRef.Location = new Point(254, 132);
            gpbSeleccionRef.Enabled = false;


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
        private static List<Facilitador_todos> listaCOFA(int id_curs, int idFA)
        {
            List<Facilitador_todos> lista = new List<Facilitador_todos>();
            MySqlDataReader cof = Conexion.ConsultarBD("select nombre_fa, apellido_fa from facilitadores fa inner join inces_tiene_facilitadores itf on itf.id_fa_INCE=fa.id_fa where itf.id_fa_INCE != '" + idFA + "' AND itf.id_curso_INCE='" + id_curs + "'");
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

        private void llenarcomboRefrigerio()
        {
            //llenar el combobox de la tercera etapa con los tipos de refrigerios
            cmbxTipoRefrigerio.ValueMember = "id_ref";
            cmbxTipoRefrigerio.DisplayMember = "nombre";
            cmbxTipoRefrigerio.DataSource = Paneles.llenarcmbxRef();
            if (Formaciones.creacion == true)
                cmbxTipoRefrigerio.SelectedIndex = -1;
        }
        private void llenarcombo2Refrigerio(int id_ref)
        {
            //llenar el combobox de la tercera etapa con los tipos de refrigerios
            cmbxSegundoRefrigerio.ValueMember = "id_ref";
            cmbxSegundoRefrigerio.DisplayMember = "nombre";
            cmbxSegundoRefrigerio.DataSource = Paneles.llenarcmbx2Ref(id_ref);
            if (Formaciones.creacion == true)
                cmbxSegundoRefrigerio.SelectedIndex = -1;
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
            cmbxSegundoHorario.ValueMember = "id_horario";
            cmbxSegundoHorario.DisplayMember = "contenido_horario";
            cmbxSegundoHorario.DataSource = Horarios.llenarcmbxHorario2(tipo, id);
            cmbxSegundoHorario.SelectedIndex = -1;
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
        private void deshabilitarControlesIntermedio()
        {           
            //para el boton pausar en el (nivel_intermedio)
            gpbRefrigerio.Enabled = false;
            dtpFechaCurso.Enabled = false;
            dtpSegundaFecha.Enabled = false;
            gpbFacilitador.Enabled = false;
            chkbCoFacilitador.Enabled = false;
            gpbCoFa.Enabled = false;

            //
        }
        private void deshabilitarControlesAvanzado()
        {
            //para el boton pausar en el (nivel_avanzado)
            gpbHorarioCurso.Enabled = false;
            gpbSeleccionRef.Enabled = false;
            gpbAulas.Enabled = false;
            gpbInsumos.Enabled = false;
        }

        private void habilitarIntermedio()
        {
            dtpFechaCurso.Enabled = true;

            if (Cursos.bloque_curso13 == "2")
            {
                dtpSegundaFecha.Enabled = true;
            }
            
            
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
            gpbAulas.Enabled = true;
            gpbInsumos.Enabled = true;
        }






        private void CargarDatosEtapaUno()
        {
            fa.id_facilitador = 0;

            formacion.ubicacion_ucs = "Si"; //ubicacion
            formacion.bloque_curso = "2";//bloque
            formacion.duracion = "16";

            //obtener id_curso_INCES
            MySqlDataReader IdCurso = Conexion.ConsultarBD("SELECT id_curso_ince FROM cursos_inces WHERE nombre_curso_ince = '" + cmbxCursoInce.Text + "'");
            int id_curso = 0;
            if (IdCurso.Read())
            {
                id_curso = int.Parse(IdCurso["id_curso_ince"].ToString());
            }
            IdCurso.Close();

            //buscar facilitadores asignados a esa formacion con ese nombre
            MySqlDataReader obtener_id_de_facilitadores = Conexion.ConsultarBD("SELECT id_fa_INCE FROM inces_tiene_facilitadores WHERE id_curso_INCE ='" + id_curso + "'");
            List<string> lista_facilitadores = new List<string>();

            while (obtener_id_de_facilitadores.Read())
            {
                lista_facilitadores.Add(obtener_id_de_facilitadores["id_fa_INCE"].ToString());
            }
            obtener_id_de_facilitadores.Close();
            cmbxFa.Items.Clear();

            //llenar el combobox de facilitadores
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

            //cargar nombre formacion
            for(int i=0; i< cmbxCursoInce.Items.Count; i++)
            {
                if (cmbxCursoInce.Items[i].ToString() == Cursos.nombre_formacion13)
                {
                    cmbxCursoInce.SelectedItem = cmbxCursoInce.Items[i];
                    formacion.nombre_formacion = Cursos.nombre_formacion13; //nombre
                }
            }
            //carga quien solicita
            for (int a = 0; a < cmbxSolicitadoPor.Items.Count; a++)
            {
                if (cmbxSolicitadoPor.Items[a].ToString() == Cursos.solicitud_formacion13)
                {
                    cmbxSolicitadoPor.SelectedItem = cmbxSolicitadoPor.Items[a];
                    formacion.solicitado = Cursos.solicitud_formacion13; //solicitud
                }
            }

            //carga duración y bloques
            switch (Cursos.duracion_formacion13)
            {
               
                case "16 Horas":
                    cmbxDuracionFormacion.SelectedIndex = 0;
                    formacion.duracion = "16";
                    formacion.bloque_curso = "2";
                    cmbxBloques.Items.Clear();
                    cmbxBloques.Items.Add("2");                    
                    if (Cursos.bloque_curso13 == "2")                   
                    {
                        cmbxBloques.SelectedIndex = 0;
                    }
                    break;
                
            }
            Cursos.duracion_formacion13 = formacion.duracion;

            //CARGAR DATOS DE PAQUETE INSTRUCCIONAL

            contenido = Cursos.p_contenido;
            btnVerContenido.Enabled = true;
            manual = Cursos.p_manual;
            btnVerManual.Enabled = true;

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
            //obtener id_curso_INCES
            MySqlDataReader IdCurso = Conexion.ConsultarBD("SELECT id_curso_ince FROM cursos_inces WHERE nombre_curso_ince = '" + cmbxCursoInce.Text + "'");
            int id_curs = 0;
            if (IdCurso.Read())
            {
                id_curs = int.Parse(IdCurso["id_curso_ince"].ToString());
            }
            IdCurso.Close();
            //llenar datos del refrigerio
            if (Cursos.tiene_ref == "Si")
            {
                rdbSiRef.Checked = true;
                formacion.tiene_ref = "Si"; //tiene_ref
            }
            else
            {
                rdbNoRef.Checked = true;
                formacion.tiene_ref = "No";
            }
            //llenar fechas
            dtpFechaCurso.Value = Convert.ToDateTime(Cursos.fecha_uno13);
            time.fecha_curso = Cursos.fecha_uno13;           //fecha1
            if (Cursos.bloque_curso13 == "2")
            {
                dtpSegundaFecha.Value = Convert.ToDateTime(Cursos.fecha_dos13);

                time.fechaDos_curso = Cursos.fecha_dos13; //fecha2
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

            
            fa.id_facilitador = id_fa;   //id_facilitador

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
            lista_cf = listaCOFA(id_curs, id_fa);
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
                chkbCoFacilitador.Checked = true;

                Cofa.id_facilitador = co_fa;    //id_cofacilitador
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
                    cmbxSegundoRefrigerio.Text = Cursos.tipo_ref2;
                    formacion.refri2 = Cursos.tipo_ref2;
                    MySqlDataReader nombre2 = Conexion.ConsultarBD("SELECT id_ref from refrigerios where ref_nombre='" + cmbxSegundoRefrigerio.Text + "'");
                    if (nombre2.Read())
                    {
                      //  MessageBox.Show(nombre2["id_ref"].ToString());
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
                cmbxSegundoRefrigerio.SelectedIndex = -1;
                formacion.refri1 = "";
                formacion.refri2 = "";
            }

            //buscar el id del horario1:
            MySqlDataReader h1 = Conexion.ConsultarBD("SELECT idhorarios from horarios where horario='" + Cursos.horario1 + "'");
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
                    rdbSiIgual.Checked = true;
                    cmbxSegundoHorario.Text = Cursos.horario2;
                    formacion.horario2 = Cursos.horario2;
                }
                else
                {
                    cmbxHorarios.Text = Cursos.horario1;
                    rdbNoIgual.Checked = true;
                    cmbxSegundoHorario.Text = Cursos.horario2;
                    formacion.horario2 = Cursos.horario2;
                }
                //si el aula2 es igual al aula1
                if (Cursos.aula2 == Cursos.aula1)
                {
                    txtSegundaAula.Text = Cursos.aula2;
                    rdbMantenerAula.Checked = true;
                }
                else
                {
                    txtSegundaAula.Text = Cursos.aula2;
                    rdbNoMantenerAula.Checked = true;
                }
                formacion.aula1 = Cursos.aula1;
                formacion.aula2 = Cursos.aula2;
            }
            else
            {
                formacion.horario1 = Cursos.horario1;

                cmbxSegundoHorario.SelectedIndex = -1;
                txtSegundaAula.Text = Cursos.aula2;
                rdbSiIgual.Checked = false;
                rdbNoIgual.Checked = false;
                rdbMantenerAula.Checked = false;
                rdbNoMantenerAula.Checked = false;
            }


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


        private void GuardarBasico()
        {
            formacion.tiene_ref = "0"; // no tiene por estar en la etapa 1 o nivel básico (esto se actualiza en la etapa 2)
            formacion.ubicacion_ucs = "Si"; //siempre es si

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
                                                formacion.fecha_inicial = fecha_creacion;                                                
                                                formacion.TiempoEtapa = Convert.ToString(FechaFinal - fecha_creacion);
                                                formacion.nombre_formacion = cmbxCursoInce.Text;
                                                formacion.estatus = "En curso";
                                                formacion.duracion = "16";
                                                formacion.pq_inst = id_paq;
                                                formacion.solicitado = cmbxSolicitadoPor.Text;
                                                //Se crea la formacion con un paquete nuevo
                                                int resultado;

                                                if (conexion.abrirconexion() == true)
                                                    resultado = Clases.Formaciones.AgregarNuevaFormacion(conexion.conexion, formacion);

                                                conexion.cerrarconexion();

                                               
                                                MySqlDataReader IdCurso = Conexion.ConsultarBD("SELECT id_cursos FROM cursos WHERE nombre_curso = '" + cmbxCursoInce.Text + "' AND solicitud_curso='" + cmbxSolicitadoPor.Text + "' AND estatus_curso='En curso'");
                                                int id_curso = 0;
                                                if (IdCurso.Read())
                                                {
                                                    id_curso = int.Parse(IdCurso["id_cursos"].ToString());
                                                }
                                                IdCurso.Close();                                                
                                                //MySqlDataReader usuarios_gestionan_cursos = Conexion.ConsultarBD("INSERT INTO user_gestionan_cursos (cursos_id_cursos, usuarios_id_user, fecha_mod_inicio, fecha_mod_final) VALUES ('" + id_curso + "', '" + Usuario_logeado.id_usuario + "', '" + fecha_creacion + "' , '" + FechaFinal + "')");
                                                //usuarios_gestionan_cursos.Close();
                                                if (conexion.abrirconexion() == true)
                                                {
                                                    int agregarUGC = Clases.Formaciones.Agregar_U_g_C(conexion.conexion, id_curso, Usuario_logeado.id_usuario, fecha_creacion, FechaFinal);
                                                    conexion.cerrarconexion();
                                                   
                                                }
                                                MySqlDataReader IdCliente = Conexion.ConsultarBD("SELECT id_clientes FROM clientes WHERE nombre_empresa='" + cmbxSolicitadoPor.Text + "'");
                                                int id_cliente = 0;
                                                if (IdCliente.Read())
                                                {
                                                    id_cliente = int.Parse(IdCliente["id_clientes"].ToString());
                                                }

                                                IdCliente.Close();
                                                MySqlDataReader idince = Conexion.ConsultarBD("SELECT id_curso_ince FROM cursos_inces WHERE nombre_curso_ince='" + cmbxCursoInce.Text + "'");
                                                int id_ince = 0;
                                                if (idince.Read())
                                                {
                                                    id_ince = Convert.ToInt32(idince["id_curso_ince"]);
                                                }
                                                idince.Close();

                                                MySqlDataReader clientes_solicitan_cursos = Conexion.ConsultarBD("INSERT INTO clientes_solicitan_cursos (id_cliente1, id_curso1, id_cursoInce) VALUES ('" + id_cliente + "', '" + id_curso + "', '" + id_ince + "')");
                                                clientes_solicitan_cursos.Close();

                                                guardar = true;
                                                MessageBox.Show("La formación se ha agregado correctamente.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.None);

                                            }
                                        }
                                        else
                                        {
                                            DateTime FechaFinal = DateTime.Now;
                                            formacion.TiempoEtapa = Convert.ToString(FechaFinal - fecha_creacion);
                                            formacion.fecha_inicial = fecha_creacion;                                            
                                            formacion.nombre_formacion = cmbxCursoInce.Text;
                                            formacion.estatus = "En curso";
                                            formacion.duracion = "16";
                                           
                                            formacion.solicitado = cmbxSolicitadoPor.Text;
                                            //Se crea la formacion con un paquete nuevo
                                            int resultado;

                                            if (conexion.abrirconexion() == true)
                                                resultado = Clases.Formaciones.AgregarNuevaFormacion(conexion.conexion, formacion);

                                            conexion.cerrarconexion();
                                            //Se crea la formacion con un paquete ya existente
                                             
                                            MySqlDataReader IdCurso = Conexion.ConsultarBD("SELECT id_cursos FROM cursos WHERE nombre_curso = '" + cmbxCursoInce.Text + "' AND solicitud_curso='" + cmbxSolicitadoPor.Text + "' AND estatus_curso='En curso'");
                                            int id_curso = 0;
                                            if (IdCurso.Read())
                                            {
                                                id_curso = int.Parse(IdCurso["id_cursos"].ToString());
                                            }
                                            IdCurso.Close();

                                            MySqlDataReader idince = Conexion.ConsultarBD("SELECT id_curso_ince FROM cursos_inces WHERE nombre_curso_ince='" + cmbxCursoInce.Text + "'");
                                            int id_ince = 0;
                                            if (idince.Read())
                                            {
                                                id_ince = Convert.ToInt32(idince["id_curso_ince"]);
                                            }
                                            idince.Close();

                                            if (conexion.abrirconexion() == true)
                                            {
                                                int agregarUGC = Clases.Formaciones.Agregar_U_g_C(conexion.conexion, id_curso, Usuario_logeado.id_usuario, fecha_creacion, FechaFinal);
                                                conexion.cerrarconexion();

                                            }
                                            //esto no guardaba bien las fechas
                                            //MySqlDataReader usuarios_gestionan_cursos = Conexion.ConsultarBD("INSERT INTO user_gestionan_cursos (cursos_id_cursos, usuarios_id_user, fecha_mod_inicio, fecha_mod_final) VALUES ('" + id_curso + "', '" + Usuario_logeado.id_usuario + "', '" + fecha_creacion + "', '" + FechaFinal + "')");
                                            //usuarios_gestionan_cursos.Close();

                                            MySqlDataReader IdCliente = Conexion.ConsultarBD("SELECT id_clientes FROM clientes WHERE nombre_empresa='" + cmbxSolicitadoPor.Text + "'");
                                            int id_cliente = 0;
                                            if (IdCliente.Read())
                                            {
                                                id_cliente = int.Parse(IdCliente["id_clientes"].ToString());
                                            }

                                            IdCliente.Close();

                                            MySqlDataReader clientes_solicitan_cursos = Conexion.ConsultarBD("INSERT INTO clientes_solicitan_cursos (id_cliente1, id_curso1, id_cursoInce) VALUES ('" + id_cliente + "', '" + id_curso + "', '"+id_ince+"')");
                                            clientes_solicitan_cursos.Close();

                                            guardar = true;
                                            MessageBox.Show("La formación se ha agregado correctamente.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information);

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
            int id_curso = 0;
            if (Formaciones.creacion == true)
            {
                //se obtiene el id del curso
                MySqlDataReader obtener_id_curso = Conexion.ConsultarBD("SELECT id_cursos FROM cursos WHERE nombre_curso = '" + cmbxCursoInce.Text + "' AND tipo_curso='INCES' AND solicitud_curso='" + cmbxSolicitadoPor.Text + "'");
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
                               
                                
                                //se actualiza el curso con los datos obtenidos de la segunda etapa, como las fechas
                                MySqlDataReader ActualizarCursoSegundaEtapa = Conexion.ConsultarBD("UPDATE cursos SET etapa_curso ='2', fecha_uno='" + dtpFechaCurso.Value.ToString("yyyy-MM-dd HH:mm:ss") + "', fecha_dos='" + dtpSegundaFecha.Value.ToString("yyyy-MM-dd HH:mm:ss") + "' WHERE id_cursos='" + id_curso + "'");
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
                                    MySqlDataReader FacilitadorCurso = Conexion.ConsultarBD("INSERT INTO cursos_tienen_fa (cursos_id_cursos, facilitadores_id_fa, ctf_fecha, ctf_fecha2) VALUES ('" + id_curso + "', '" + (cmbxFa.SelectedItem as ComboboxItem).Value + "', '" + dtpFechaCurso.Value.ToString("yyyy-MM-dd HH:mm:ss") + "', '" + dtpSegundaFecha.Value.ToString("yyyy-MM-dd HH:mm:ss") + "')");
                                    FacilitadorCurso.Close();
                                    guardar = true;
                                }
                                MessageBox.Show("Los datos se han guardado correctamente.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.None);

                                //aqui estaran contemplados los arreglos (visuales y de contenido) de la siguiente etapa
                                if (rdbSiRef.Checked == false)
                                {
                                    ordenTerceraEtapa();
                                    gpbSeleccionRef.Enabled = false;
                                    gpbSeleccionRef.Visible = false;
                                    gpbHorarioCurso.Height = 169;

                                 
                                    MySqlDataReader ActualizarCursoTieneRef = Conexion.ConsultarBD("UPDATE cursos SET tiene_ref='0' WHERE id_cursos='" + id_curso + "'");
                                    ActualizarCursoTieneRef.Close();
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

                                   
                                    MySqlDataReader ActualizarCursoTieneRef = Conexion.ConsultarBD("UPDATE cursos SET tiene_ref='1' WHERE id_cursos='" + id_curso + "'");
                                    ActualizarCursoTieneRef.Close();
                                }
                              

                                
                                //para la logistica:
                                rdbMantenerAula.Enabled = true;
                                rdbNoMantenerAula.Enabled = true;
                                txtSegundaAula.Enabled = false;
                            }

                        }
                    }
                }

            }


        }
        
        private void GuardarAvanzado()
        {
            guardar = false;
            int id_curso = 0;
            if (Formaciones.creacion == true)
            {
                //se obtiene el id del curso
                MySqlDataReader obtener_id_curso = Conexion.ConsultarBD("SELECT id_cursos FROM cursos WHERE nombre_curso = '" + cmbxCursoInce.Text + "' AND tipo_curso='INCES' AND solicitud_curso='" + cmbxSolicitadoPor.Text + "'");
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

            if (cmbxHorarios.SelectedIndex == -1)
            {
                errorProviderHora.SetError(cmbxHorarios, "Debe seleccionar uno de los horarios disponibles.");
                cmbxHorarios.Focus();
            }
            else
            {
                errorProviderHora.SetError(cmbxHorarios, "");
                if (rdbNoIgual.Checked && cmbxSegundoHorario.SelectedIndex == -1)
                {
                    errorProviderBloque.SetError(cmbxSegundoHorario, "Debe seleccionar uno de los horarios disponibles.");
                    cmbxSegundoHorario.Focus();

                }
                else
                {
                    errorProviderBloque.SetError(cmbxSegundoHorario, "");
                    if (gpbSeleccionRef.Enabled == true)
                    {
                        if (cmbxTipoRefrigerio.SelectedIndex == -1)
                        {
                            errorProviderFecha.SetError(cmbxTipoRefrigerio, "Debe seleccionar un tipo de refrigerio para la formación.");
                            cmbxTipoRefrigerio.Focus();

                        }
                        else if (rdbNoMantenerRef.Checked == true && cmbxSegundoRefrigerio.SelectedIndex == -1)
                        {
                            errorProviderBloque.SetError(cmbxSegundoRefrigerio, "Debe seleccionar un tipo de refrigerio para la formación.");
                            cmbxSegundoRefrigerio.Focus();
                        }
                    }
                    errorProviderFecha.SetError(cmbxTipoRefrigerio, "");
                    errorProviderBloque.SetError(cmbxSegundoRefrigerio, "");
                    if (txtAula.Text == "")
                    {
                        errorProviderNombreF.SetError(txtAula, "Debe proporcionar el aula en el que se dictará la formación.");
                        txtAula.Focus();
                    }
                    else
                    {
                        errorProviderNombreF.SetError(txtAula, "");
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

                               
                                string aula = txtAula.Text;
                                string aula2;
                                if (rdbMantenerAula.Checked)
                                {
                                    aula2 = aula;
                                }
                                else
                                {
                                    aula2 = txtSegundaAula.Text;
                                }

                                

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
                                if (gpbSeleccionRef.Enabled==true)
                                {
                                    MySqlDataReader ActualizarCursoTerceraEtapa = Conexion.ConsultarBD("UPDATE cursos SET etapa_curso ='3', horario_uno='" + id_horario + "', horario_dos='" + id_horario2 + "', aula_dia1='" + aula + "', aula_dia2='" + aula2 + "', id_ref1='" + id_refrigerio + "',id_ref2='" + id_refrigerio2 + "' WHERE id_cursos='" + id_curso + "'");
                                    ActualizarCursoTerceraEtapa.Close();
                                }
                                else
                                {
                                    MySqlDataReader ActualizarCursoTerceraEtapa = Conexion.ConsultarBD("UPDATE cursos SET etapa_curso ='3', horario_uno='" + id_horario + "', horario_dos='" + id_horario2 + "', aula_dia1='" + aula + "', aula_dia2='" + aula2 + "' WHERE id_cursos='" + id_curso + "'");
                                    ActualizarCursoTerceraEtapa.Close();
                                }
                                //se actualiza el curso con los datos obtenidos de la segunda etapa, como las fechas

                                MessageBox.Show("Los datos se han agregado correctamente.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.None);
                                guardar = true;

                            }
                        }

                    }

                }

            }
        }


        private void Modificar_intermedio()
        {
            
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



        /*------------------ Botones panel lateral derecho -------------------*/
        private void btnSiguienteEtapa_Click(object sender, EventArgs e)
        {
            guardar = false;

            if (Formaciones.creacion == true)
            {
                if (pnlNivel_basico.Visible == true)
                {
                    LabelCabecera.Text = "Nuevo INCES: Detalles técnicos";
                    LabelCabecera.Location = new Point(115, 31);

                    pnlNivel_basico.Visible = false;
                    pnlNivel_intermedio.Visible = true;

                    Load_Sig_Re();

                    //comportamiento del panel nivel_intermedio de acuerdo a la duracion del curso
                    if (formacion.duracion == "16")
                    {
                        Controles_nivel_intermedio_EstatusInicial();
                        dtpSegundaFecha.Enabled = true;

                    }
                    
                }
                else if (pnlNivel_intermedio.Visible == true)
                {
                    LabelCabecera.Text = "Nuevo INCES: Logística";
                    LabelCabecera.Location = new Point(200, 31);


                    lblEtapaSiguiente.Location = new Point(3, 529);
                    lblEtapaSiguiente.Text = "Añadir participantes";

                    lblEtapafinal.Location = new Point(3, 570);
                    lblEtapafinal.Text = "Día de la formación";

                    pnlNivel_intermedio.Visible = false;
                    pnlNivel_avanzado.Visible = true;
                    Load_Sig_Re();

                }

            }else
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

                        inicioE2 = DateTime.Now;
                        
                    }
                    else if (Cursos.etapa_formacion13 == 2)
                    {
                        cmbxTipoRefrigerio.SelectedIndex = -1;
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

                    LabelCabecera.Text = "Logística";
                    LabelCabecera.Location = new Point(250, 31);

                    btnGuardar.Enabled = false;

                    btnLimpiar.Enabled = false;
                    btnRetomar.Enabled = true;

                    //comportamiento del panel nivel_AVANZADO de acuerdo a la duracion del curso Y SI SE DICTA EN UCS O NO

                    //ARREGLOS VISUALES PARA LA ETAPA3
                    if (Cursos.ubicacion_ucs == "Si")
                    {

                        //para arreglos visuales en el form
                        if (Cursos.duracion_formacion13 == "16")
                        {
                            //para la logistica:
                            rdbMantenerAula.Enabled = true;
                            rdbNoMantenerAula.Enabled = true;
                            txtSegundaAula.Enabled = false;
                            gpbAulas.Enabled = true;
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
                    }


                    if (Cursos.etapa_formacion13 == 1)
                    {
                        habilitarIntermedio();
                        btnGuardar.Enabled = false;
                        btnSiguienteEtapa.Enabled = false;
                        btnRetomar.Enabled = false;
                        btnPausar.Enabled = true;
                        btnModificar.Enabled = true;
                    }
                    else if (Cursos.etapa_formacion13 == 2)
                    {
                        deshabilitarControlesIntermedio();
                        habilitarAvanzado();
                        btnGuardar.Enabled = false;
                        btnSiguienteEtapa.Enabled = false;
                        btnRetomar.Enabled = false;
                        btnPausar.Enabled = true;
                        btnModificar.Enabled = true;

                        inicioE3 = DateTime.Now;
                    }
                    else if (Cursos.etapa_formacion13 == 3)
                    {
                        deshabilitarControlesAvanzado();
                        btnSiguienteEtapa.Enabled = false;
                        btnRetomar.Enabled = true;
                        btnPausar.Enabled = false;
                        btnModificar.Enabled = false;

                    }
                    //FIN COMPORTAMIENTO


                    pnlNivel_intermedio.Visible = false;
                    pnlNivel_avanzado.Visible = true;

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
                    if (guardar == false)
                    {
                        btnGuardar.Enabled = true;
                    }
                    else
                    {
                        btnGuardar.Enabled = false;
                    }
                    cmbxCursoInce.Enabled = true;
                    cmbxCursoInce.Focus();
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
                    cmbxCursoInce.Enabled = true;
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
                        gpbCorreos.Enabled = false; // se deshabilita hasta que modifique en algo
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
                    if (Cursos.bloque_curso13 == "1")
                    {
                        txtSegundaAula.Enabled = false;
                        rdbNoMantenerAula.Enabled = false;
                        rdbMantenerAula.Enabled = false;
                    }
                    else
                    {
                        rdbNoMantenerAula.Enabled = true;
                        rdbMantenerAula.Enabled = true;
                    }
                    if (Cursos.tiene_ref == "No")
                    {
                        gpbSeleccionRef.Enabled = false;
                    }
                   
                }
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
                DateTime FinalE2 = DateTime.Now;
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
                    gpbCorreos.Enabled = true;
                    //para saber si las opciones de refrigerio estarán disponibles o no
                    if (rdbNoRef.Checked == true)
                    {
                        gpbSeleccionRef.Enabled = false;
                    }
                    else
                    {
                        gpbSeleccionRef.Enabled = true;
                        //se llena el combobox con los refrigerios
                        llenarcomboRefrigerio();

                    }
                }

            }
            else if (pnlNivel_avanzado.Visible == true)
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
            if(guardar==true)
                btnSiguienteEtapa.Enabled = false;
            btnModificar.Enabled = false;
            btnGuardar.Enabled = false;
            btnPausar.Enabled = false;
            btnLimpiar.Enabled = false;
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (pnlNivel_basico.Visible == true)
            {
                if (Cursos.etapa_formacion13 == 1)
                {
                    if (cmbxCursoInce.Text != Cursos.nombre_formacion13 || cmbxSolicitadoPor.Text != Cursos.solicitud_formacion13 || formacion.duracion != Cursos.duracion_formacion13 || formacion.bloque_curso != Cursos.bloque_curso13)
                    {
                        formacion.tiene_ref = "0";
                        formacion.ubicacion_ucs = "Si"; //es SI hasta que se actualice en la etapa dos o nivel intermedio
                        if (cmbxCursoInce.Text == "")
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

                                            formacion.nombre_formacion = cmbxCursoInce.Text;
                                            formacion.tipo_formacion = "INCES";
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
                                          //  MessageBox.Show(formacion.TiempoEtapa + " tiempoEtapa");
                                            formacion.id_user = Clases.Usuario_logeado.id_usuario;
                                            formacion.etapa_curso = 1;//representa la etapa actual: nivel_basico (cambiará para cada panel)
                                            formacion.solicitado = cmbxSolicitadoPor.Text;
                                            MySqlDataReader e1 = Conexion.ConsultarBD("SELECT duracionE1 from cursos where id_cursos='" + Cursos.id_curso13 + "'");
                                            if (e1.Read())
                                            {
                                                string duracion = Convert.ToString(e1["duracionE1"]);

                                                TimeSpan et1;
                                                et1 = TimeSpan.Parse(duracion);
                                              //  MessageBox.Show(et1.ToString() + "Lo que se extrajo de la BD");
                                                TimeSpan tt = TimeSpan.Parse(formacion.TiempoEtapa);

                                                formacion.TiempoEtapa = (tt + et1).ToString();
                                                //formacion.TiempoEtapa= Convert.ToString();
                                                //MessageBox.Show(formacion.TiempoEtapa + "dizque la suma de lo9s timespan");
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
                    MySqlDataReader update = Conexion.ConsultarBD("UPDATE cursos SET duracionE2='" + formacion.TiempoEtapa + "' where id_cursos='" + Cursos.id_curso13 + "'");
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
                    if (Cursos.fecha_dos13 != "No aplica")
                    {
                        if (formacion.ubicacion_ucs != Cursos.ubicacion_ucs || formacion.tiene_ref != Cursos.tiene_ref || dtpFechaCurso.Value.ToString("yyyy-MM-dd") != Cursos.fecha_uno13 || dtpSegundaFecha.Value.ToString("yyyy-MM-dd") != Cursos.fecha_dos13 || cmbxFa.Text != fcombo.nombreyapellido1 || cmbxCoFa.Text != cf.nombreyapellido1)
                        {
                            Modificar_intermedio();
                            
                        }
                    }
                    
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
                            if (formacion.bloque_curso == "2")
                            {
                                if (lista_insumo_cargada != lista_insumo || formacion.horario1 != Cursos.horario1 || formacion.horario2 != Cursos.horario2 || txtAula.Text != formacion.aula1 || txtSegundaAula.Text != formacion.aula2 || formacion.refri1 != Cursos.tipo_ref1 || formacion.refri2 != Cursos.tipo_ref2)
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
                            
                        }


                    }// SI NO se hace en ucs (
                    

                }
            }
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
                    formacion.estatus = "En curso"; //predeterminado en esta etapa
                    formacion.tipo_formacion = "INCES"; //predeterminado para este form
                    formacion.nombre_formacion = cmbxCursoInce.Text;

                    int existeEjecucion = 0;

                    if (conexion.abrirconexion() == true)
                        existeEjecucion = Clases.Formaciones.CursoEjecucionExiste(conexion.conexion, formacion);

                    conexion.cerrarconexion();

                    if (existeEjecucion != 0)
                    {
                        //errorProviderNombreF.SetError(cmbxCursoInce, "Ya existe una formacion con este nombre en estado 'En curso'");

                        formacion.bloque_curso = "2";
                        formacion.etapa_curso = 1;
                        formacion.tipo_formacion = "INCES";
                        formacion.id_user = Usuario_logeado.id_usuario;
                        //estado inicial de los bloques de esta formación
                        cmbxBloques.SelectedIndex = 0;
                        cmbxBloques.Enabled = false;

                        //estado inicial de la duracion de esta formación
                        cmbxDuracionFormacion.SelectedIndex = 0;
                        cmbxDuracionFormacion.Enabled = false;
                        MySqlDataReader idince = Conexion.ConsultarBD("SELECT id_curso_ince FROM cursos_inces WHERE nombre_curso_ince='" + cmbxCursoInce.Text + "'");
                        int id_ince = 0;
                        if (idince.Read())
                        {
                            id_ince = Convert.ToInt32(idince["id_curso_ince"]);
                        }
                        idince.Close();

                       
                        List<string> nombre = new List<string>();
                        List<int> lista_id = new List<int>();
                        MySqlDataReader nombreSolicitud = Conexion.ConsultarBD("SELECT id_cliente1 FROM clientes_solicitan_cursos WHERE id_cursoInce= '" + id_ince+ "'");
                        while (nombreSolicitud.Read())
                        {
                           int n = Convert.ToInt32(nombreSolicitud["id_cliente1"]);                          
                           lista_id.Add(n);
                           
                        }
                        
                        nombreSolicitud.Close();

                        for(int x=0; x<lista_id.Count; x++)
                        {
                            MySqlDataReader Ncliente = Conexion.ConsultarBD("SELECT nombre_empresa FROM clientes WHERE id_clientes='" + lista_id[x] + "'");
                            while (Ncliente.Read())
                            {
                                string a = Convert.ToString(Ncliente["nombre_empresa"]);
                                nombre.Add(a);
                            }
                            Ncliente.Close();
                        }

                        for (int i=0; i < nombre.Count; i++)
                        {
                            for (int a=0; a< cmbxSolicitadoPor.Items.Count; a++)
                            {
                                if (cmbxSolicitadoPor.Items[a].ToString() == nombre[i])
                                {
                                    cmbxSolicitadoPor.Items.RemoveAt(a);
                                }
                            }
                            
                        }


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
                    else
                    {
                        errorProviderNombreF.SetError(cmbxCursoInce, "");                       

                        ExisteFormacion = false;//significa que no existe, el usuario podrá establecer su propio paquete instruccional
                        btnRutaContenido.Enabled = true;
                        btnRutaPresentacion.Enabled = true;
                        btnRutaManual.Enabled = true;
                        btnRutaBitacora.Enabled = true;



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
                fa.id_facilitador = Convert.ToInt32(cmbxFa.SelectedValue);
                
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
            if ((formacion.duracion == "16"))
            {
                //validar ambas fechas
                if (conexion.abrirconexion() == true)
                {
                    int fa_disponible = Clases.Facilitadores.FacilitadorDisponibleDia(conexion.conexion, time.fecha_curso, fa.id_facilitador);
                    conexion.cerrarconexion();
                    if (fa_disponible > 0)//si retorna algo: este ya está ocupado para esa fecha
                    {
                        MessageBox.Show("El facilitador está ocupado en esta fecha : " + time.fecha_curso + ".");
                        cmbxFa.SelectedIndex = -1;
                        fa.id_facilitador = 0;
                        //cmbxFa.Focus();
                        dtpFechaCurso.Focus();
                    }
                    else
                    {
                        errorProviderManual.SetError(cmbxFa, "");
                        if (conexion.abrirconexion() == true)
                        {
                            int fa_disponibleDia2 = Clases.Facilitadores.FacilitadorDisponibleDia2(conexion.conexion, time.fechaDos_curso, fa.id_facilitador);
                            conexion.cerrarconexion();
                            if (fa_disponibleDia2 > 0 )//si retorna algo: este ya está ocupado para esa fecha
                            {
                                MessageBox.Show("El facilitador está ocupado en esta fecha : " + time.fechaDos_curso + ".");
                                cmbxFa.SelectedIndex = -1;
                                fa.id_facilitador = 0;
                                //cmbxFa.Focus();
                                dtpSegundaFecha.Focus();
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
            MySqlDataReader cnom = Conexion.ConsultarBD("select * from facilitadores where nombre_apellido='" + cmbxCoFa.Text + "'");
            if (cnom.Read())
            {
                Cofa.id_facilitador = Convert.ToInt32(cnom["id_fa"]);
            }
            cnom.Close();
        }

        private void cmbxCoFa_Validating(object sender, CancelEventArgs e)
        {
            if ((formacion.duracion == "16"))
            {
                if(Cofa.id_facilitador != 0)
                {
                    //validar ambas fechas
                    if (conexion.abrirconexion() == true)
                    {
                        int fa_disponible = Clases.Facilitadores.FacilitadorDisponibleDia(conexion.conexion, time.fecha_curso, Cofa.id_facilitador);
                        conexion.cerrarconexion();
                        if (fa_disponible > 0)//si el id que retorna es el del facilitador seleccionado: este ya está ocupado para esa fecha
                        {
                            MessageBox.Show("El Co-facilitador está ocupado en esta fecha : " + time.fecha_curso + ".");
                            cmbxCoFa.SelectedIndex = -1;
                            Cofa.id_facilitador = 0;

                        }
                        else
                        {
                            errorProviderManual.SetError(cmbxCoFa, "");
                            if (conexion.abrirconexion() == true)
                            {
                                int fa_disponibleDia2 = Clases.Facilitadores.FacilitadorDisponibleDia2(conexion.conexion, time.fechaDos_curso, Cofa.id_facilitador);
                                conexion.cerrarconexion();
                                if (fa_disponibleDia2 > 0)//si el id que retorna es el del facilitador seleccionado: este ya está ocupado para esa fecha
                                {
                                    MessageBox.Show("El Co-facilitador está ocupado en la fecha : " + time.fechaDos_curso + ".");
                                    cmbxCoFa.SelectedIndex = -1;
                                    Cofa.id_facilitador = 0;

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
                dtpSegundaFecha.Enabled = true;
                dtpSegundaFecha.Focus();
             
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

        private void pnlNivel_basico_Paint(object sender, PaintEventArgs e)
        {

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
                time.fecha_curso = dtpFechaCurso.Value.ToString("yyyy-MM-dd");
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
                time.fechaDos_curso = dtpSegundaFecha.Value.ToString("yyyy-MM-dd"); ;
                gpbFacilitador.Enabled = true;
                

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
             else 
            {
                string t = "A";
                llenarComboHorario2(t, id_horario);
                cmbxSegundoHorario.SelectedIndex = 0;
                id_horario2 = id_horario;
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
            else
            {
                string t = "A";
                llenarComboHorario2(t, id_horario);
                cmbxSegundoHorario.Focus();
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
               
            }
            else
            {
                //llenar el combobox de la tercera etapa con los tipos de refrigerios
                cmbxSegundoRefrigerio.ValueMember = "id_ref";
                cmbxSegundoRefrigerio.DisplayMember = "nombre";
                cmbxSegundoRefrigerio.DataSource = Paneles.llenarcmbxRef();
                cmbxSegundoRefrigerio.SelectedIndex = -1;
                //como se quiere seleccionar el mismo refrigerio, se desactiva el combobox y automaticamente se selecciona
                errorProviderFecha.SetError(cmbxTipoRefrigerio, "");

                cmbxSegundoRefrigerio.SelectedIndex = cmbxTipoRefrigerio.SelectedIndex;               
                cmbxSegundoRefrigerio.Enabled = false;
                id_refrigerio2 = id_refrigerio;
            }
        }

        private void rdbNoMantenerRef_CheckedChanged(object sender, EventArgs e)
        {
            
            if (cmbxTipoRefrigerio.SelectedIndex == -1)
            {
                errorProviderFecha.SetError(cmbxTipoRefrigerio, "Debe seleccionar un tipo de refrigerio.");
                cmbxTipoRefrigerio.Focus();                
                cmbxSegundoRefrigerio.Enabled = false;
            }
            else
            {
                errorProviderFecha.SetError(cmbxTipoRefrigerio, "");
                //llenar el combobox de la tercera etapa con los tipos de refrigerios
                cmbxSegundoRefrigerio.ValueMember = "id_ref";
                cmbxSegundoRefrigerio.DisplayMember = "nombre";
                cmbxSegundoRefrigerio.DataSource = Paneles.llenarcmbx2Ref(id_refrigerio);
                cmbxSegundoRefrigerio.SelectedIndex = -1;
                // se habilita el combo box
                cmbxSegundoRefrigerio.Enabled = true;
                cmbxSegundoRefrigerio.Focus();
               
            }
            
        }

        private void cmbxHorarios_Validating(object sender, CancelEventArgs e)
        {
          
            if (cmbxHorarios.SelectedIndex == -1)
            {
                errorProviderFecha.SetError(cmbxHorarios, "Debe seleccionar un horario para la formación.");
                
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
            if(Formaciones.creacion==true)
                inicioE3 = DateTime.Now;

            if (cmbxHorarios.SelectedIndex == -1)
            {
                errorProviderFecha.SetError(cmbxHorarios, "Debe seleccionar un horario para la formación.");

                rdbSiIgual.Enabled = false;
                rdbNoIgual.Enabled = false;

            }
            else
            {
                errorProviderFecha.SetError(cmbxHorarios, "");
                rdbSiIgual.Enabled = true;
                rdbNoIgual.Enabled = true;
                id_horario = Convert.ToInt32(cmbxHorarios.SelectedValue);
                formacion.horario1 = cmbxHorarios.Text;
            }
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
                   // MessageBox.Show(nombre["id_ref"].ToString());
                    id_refrigerio = Convert.ToInt32(nombre["id_ref"]);
                }
            }
            formacion.refri1 = cmbxTipoRefrigerio.Text;
        }

        private void cmbxSegundoRefrigerio_SelectionChangeCommitted(object sender, EventArgs e)
        {
            id_refrigerio2 = Convert.ToInt32(cmbxSegundoRefrigerio.SelectedValue);
            if (Formaciones.creacion == true)
            {
                id_refrigerio2 = Convert.ToInt32(cmbxSegundoRefrigerio.SelectedValue);
              //  MessageBox.Show(id_refrigerio2.ToString() + "id del refreigerio2 en creacion");


                MySqlDataReader nombre = Conexion.ConsultarBD("SELECT id_ref from refrigerios where ref_nombre='" + cmbxSegundoRefrigerio.Text + "'");
                if (nombre.Read())
                {
                   // MessageBox.Show(nombre["id_ref"].ToString());
                    id_refrigerio2 = Convert.ToInt32(nombre["id_ref"]);
                  //  MessageBox.Show(id_refrigerio2.ToString() + "id del refreigerio2 en creacion 2");
                }

            }
            else
            {
                MySqlDataReader nombre = Conexion.ConsultarBD("SELECT id_ref from refrigerios where ref_nombre='" + cmbxSegundoRefrigerio.Text + "'");
                if (nombre.Read())
                {
                   // MessageBox.Show(nombre["id_ref"].ToString());
                    id_refrigerio2 = Convert.ToInt32(nombre["id_ref"]);
                    formacion.refri2 = cmbxSegundoRefrigerio.Text;
                }
            }
            formacion.refri2 = cmbxSegundoRefrigerio.Text;
        }
        
        private void txtAula_TextChanged(object sender, EventArgs e)
        {

        }
        private void txtAula_Leave(object sender, EventArgs e)
        {
            formacion.aula1 = txtAula.Text;
        }

        private void txtSegundaAula_Leave(object sender, EventArgs e)
        {
            formacion.aula2 = txtSegundaAula.Text;
        }

        private void rdbSiRef_CheckedChanged(object sender, EventArgs e)
        {
            if (Formaciones.creacion == true)
                inicioE2 = DateTime.Now;

            formacion.tiene_ref = "Si";
        }

        private void rdbNoRef_CheckedChanged(object sender, EventArgs e)
        {
            if (Formaciones.creacion == true)
                inicioE2 = DateTime.Now;

            formacion.tiene_ref = "No";
        }

        private void cmbxHorarios_SelectionChangeCommitted(object sender, EventArgs e)
        {
            id_horario = Convert.ToInt32(cmbxHorarios.SelectedIndex);
        }

        private void rdbMantenerAula_CheckedChanged(object sender, EventArgs e)
        {
            txtSegundaAula.Text = txtAula.Text;
            txtSegundaAula.Enabled = false;
            formacion.aula2 = txtSegundaAula.Text;
        }
        private void rdbNoMantenerAula_CheckedChanged(object sender, EventArgs e)
        {
            txtSegundaAula.Text = "";
            txtSegundaAula.Focus();
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
                    //row.DefaultCellStyle.BackColor = Color.Red;

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

        private void dgvInsumos_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            //aporte de rafael
            if (dgvInsumos.IsCurrentCellDirty)
            {
                dgvInsumos.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }
    }
}
