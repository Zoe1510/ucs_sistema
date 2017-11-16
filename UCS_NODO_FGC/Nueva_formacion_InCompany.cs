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

namespace UCS_NODO_FGC
{
    public partial class Nueva_formacion_InCompany : Form
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
        string bloques = "";
        string bitacora = "";
        string manual = "";
        public Nueva_formacion_InCompany()
        {
            InitializeComponent();
        }

        private void Nueva_formacion_InCompany_Load(object sender, EventArgs e)
        {
            if (Clases.Formaciones.creacion == true)//si viene referenciado del boton de la pagina principal
            {//------------------------------------------todo hay que hacerlo aquí(un nuevo ingreso)
                this.Location = new Point(-5, 0);
                LabelCabecera.Text = "Nuevo InCompany: Información básica";
                LabelCabecera.Location = new Point(115, 31);

                lblEtapaSiguiente.Text = "Nivel Intermedio";
                lblEtapaSiguiente.Location = new Point(17, 529);

                lblEtapafinal.Text = "Nivel Avanzado";
                lblEtapafinal.Location = new Point(22, 570);

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
            }
                /* TIENE QUE SABER: Se puede llegar a este form de dos maneras:

                    Opcion 1)Por el botón del panel principal (Nuevo Incompany) aquí, se mostrará como parte de la ventana principal
                            -Crear un nuevo curso de Incompany
                                -se trabaja una fecha de creación, guardada en la tabla Cursos de la base de datos
                                -Y se guarda una fecha de modificacion final al momento de guardar. (Pueden verlo en Nueva_formacion_Abierta)

                    Opcion 2)Por el boton (Seguir) en "Ver formaciones", aquí se desplegará como una ventana distinta:
                            -Modificar o continuar un curso que esté registrado.
                                -Se trabaja con una fecha de modificación (al momento de guardar cambios) en la tabla Users_gestionan_cursos
                                -Esto representaria cargar los datos que estén llenos (siempre estarán llenos al menos los del primer panel) en el form
                                -Afectaria al estado de los botones y los controles (textbox y demás)

                 * ESPECIFICACIONES PANEL_NIVEL_BASICO (opción 1): 
                   1)Cuando se introduzca el NOMBRE se verifica (Yo usé el evento Leave):
                        -Que no exista otro curso de ese tipo y con ese mismo nombre de estatus "en curso" : De ser así, mostrar mensaje de error
                        -Que no exista otro curso de ese tipo y con ese mismo nombre de estatus "Reprogramado" : De ser así, mostrar mensaje de error

                        -Si se introduce el nombre de un curso que está Suspendido o Finalizado: Mostrar el paquete instruccional que existe de ese curso. (ver en Nueva_formacion_Abierto, "void VerPaqueteInst"

                    2)Para el combobox(cmbx) SOLICITADO POR:
                        -Cargar ahí todas las empresas registradas, sin excepción.

                    3)Para el cmbx DURACIÖN FORMACIÖN:
                        -tiene las mismas opciones que en Abierto: 4, 8 o 16 hrs.
                        -Pueden ver esa parte en Nueva_formacion_Abierto: "void cmbxDuracionFormacion_Validating"
                        -El punto anterior les ayuda en los bloques de la formacion:
                            \Si es 4hrs: 1 bloque siempre (1 solo día)
                            \Si es 8hrs: 1 o 2 bloques (si se hace en un día o dos)
                            \Si es 16hrs: 2 bloques siempre (2 dias de 8 horas)

                        Este punto ayudará para luego (En la Etapa 2 y 3. O sea, el panel Nivel_Intermedio, y avanzado) seleccionar fechas y los horarios de cada bloque (en el caso de 8 o 16 hrs)
                        Y para seleccionar el refrigerio de esos dias (en caso de ser 8 o 16)
                        Nota: los cursos de 4 hrs, no tienen refrigerio.

                    4)Para el PAQUETE INSTRUCCIONAL:
                        -Es obligatorio el contenido.
                        -revisen los cambios de estado de los botones "ver" en Nueva_formacion_Abierto (abajo de la foto del formato)

                    LOS BOTONES DEL PANEL DERECHO:
                        -load: 
                            Enable: Guardar, Pausar, Limpiar.
                            Disable: Retomar, Siguiente etapa, Modificar

                        *Si se presiona Guardar: Todos los campos deben estar llenos y si es así:
                            Enable: Pausar, Siguiente etapa, Limpiar
                            Disable: Guardar, Retomar, Modificar
                            Nota: Si se está en el ultimo Panel (NivelAvanzado), el boton Siguiente Etapa estará Disable

                        *Si se presiona Pausar: Disable todos los textbox y combobox; Para los botones (Esto no significa que se guarde algo, sólo pausamos la edición):
                            Enable: Retomar
                            Disable: Guardar, Pausar, Siguiente etapa, Modificar limpiar

                        *Si se presiona Retomar: Se habilitan los controles y el estado de los botones es igual que en el load.

                        *Si se presiona Siguiente Etapa (Para poder presionar ese boton, se debió guardar antes), así que: 
                        oculta el panel que se esté trabajando (NivelBasico, NivelIntermedio, NivelAvanzado) y el estado de los botones es igual que en el Load

                        *Si se presiona Limpiar: Borrará el contenido de todos los controles y variables (como en el selección del paquete instruccional)

                    NOTA: 
                    PARA LOS ERRORES DE CAMPOS VACIOS: 
                    USO EL VALIDATING EN TODOS LOS CONTROLES Y ADEMÁS HAY QUE VALIDARLOS AL MOMENTO DE GUARDAR
                    LOS ERRORES LOS MUESTRO POR EL ERRORPROVAIDER QUE ESTÁN AHÍ
                 */
            }
        /*---------------------- METODOS ------------------------*/
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




        /*---------------------- Controles nivel_basico------------------------*/

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



        /*---------------------- Controles nivel_intermedio------------------------*/


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
                    int fa_disponible = Clases.Facilitadores.FacilitadorDisponibleDia1(conexion.conexion, time.fecha_curso);
                    conexion.cerrarconexion();
                    if (fa_disponible == fa.id_facilitador)//si el id que retorna es el del facilitador seleccionado: este ya está ocupado para esa fecha
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
                    int fa_disponible = Clases.Facilitadores.FacilitadorDisponibleDia1(conexion.conexion, time.fecha_curso);
                    conexion.cerrarconexion();
                    if (fa_disponible == fa.id_facilitador)//si el id que retorna es el del facilitador seleccionado: este ya está ocupado para esa fecha
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
                            int fa_disponibleDia2 = Clases.Facilitadores.FacilitadorDisponibleDia2(conexion.conexion, time.fechaDos_curso);
                            conexion.cerrarconexion();
                            if (fa_disponibleDia2 == fa.id_facilitador)//si el id que retorna es el del facilitador seleccionado: este ya está ocupado para esa fecha
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
                    int fa_disponible = Clases.Facilitadores.FacilitadorDisponibleDia1(conexion.conexion, time.fecha_curso);
                    conexion.cerrarconexion();
                    if (fa_disponible == Cofa.id_facilitador)//si el id que retorna es el del facilitador seleccionado: este ya está ocupado para esa fecha
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
                    int fa_disponible = Clases.Facilitadores.FacilitadorDisponibleDia1(conexion.conexion, time.fecha_curso);
                    conexion.cerrarconexion();
                    if (fa_disponible == Cofa.id_facilitador)//si el id que retorna es el del facilitador seleccionado: este ya está ocupado para esa fecha
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
                            int fa_disponibleDia2 = Clases.Facilitadores.FacilitadorDisponibleDia2(conexion.conexion, time.fechaDos_curso);
                            conexion.cerrarconexion();
                            if (fa_disponibleDia2 == Cofa.id_facilitador)//si el id que retorna es el del facilitador seleccionado: este ya está ocupado para esa fecha
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
    }
}
