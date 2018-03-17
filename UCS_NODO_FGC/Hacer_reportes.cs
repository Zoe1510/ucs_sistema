using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using UCS_NODO_FGC.Clases;
using System.IO;
using System.Diagnostics;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Reflection;
using System.Windows.Forms;

namespace UCS_NODO_FGC
{
    public partial class Hacer_reportes : Form
    {
        conexion_bd conexion = new conexion_bd();
        DateTime fecha_uno, fecha_dos;
        Tiempos_curso tiempo = new Tiempos_curso();
        Formaciones formaciones = new Formaciones();
        //objeto de la clase r_datosgenerales
        R_Formacion_DatosGenerales fdg = new R_Formacion_DatosGenerales();
        R_Formacion_DatosGenerales datosF = new R_Formacion_DatosGenerales();
        //objeto de etapas de la formacion= new 
        R_EtapaFormacion etapaf = new R_EtapaFormacion();
        R_EtapaFormacion etapaf2 = new R_EtapaFormacion();
        R_EtapaFormacion etapaf3 = new R_EtapaFormacion();
        string fecha_actual, fecha_vieja;
        int resultado = 0;
        string nombre_user;
        bool reporte2;
        string duracion, duracion2, duracion3;
        string ruta = @"C:\Users\ZM\Documents\Last_repo\ucs_sistema\UCS_NODO_FGC\Resources\logo ucs.png";
        Image pic;
        public Hacer_reportes()
        {
            InitializeComponent();
            dgvFormaciones.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            //PARA SELECCIONAR
            dgvFormaciones.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvFormaciones.MultiSelect = false;
            dgvFormaciones.ClearSelection();
        }
        
        private void Hacer_reportes_Load(object sender, EventArgs e)
        {
            this.Location = new Point(-5, 0);
            refrescar();
        }
        private byte[] GetBytes(Image imageIn)
        {
            //
            //Usamos la clase MemoryStream para contener los bytes que compone la imagen
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, ImageFormat.Png);
            //
            //Retornamos el arreglo de bytes
            return ms.ToArray();
        }
        private void llenarDGV(string nombre, string estatus)
        {

            dgvFormaciones.Rows.Clear();
            string query = "SELECT estatus_curso, solicitud_curso, tipo_curso, nombre_curso, duracion_curso, nombre_user  FROM cursos cur inner join user_gestionan_cursos ugc on cur.id_cursos = ugc.cursos_id_cursos inner join usuarios us on ugc.usuarios_id_user = us.id_user WHERE nombre_curso LIKE '%" + nombre + "%' AND estatus_curso LIKE '%" + estatus + "%' ";
            MySqlDataReader formaciones = Conexion.ConsultarBD(query);
            while (formaciones.Read())
            {
                string duracion = Convert.ToString(formaciones["duracion_curso"]);
                switch (duracion)
                {
                    case "4":
                        duracion = "4 Horas";
                        break;
                    case "8":
                        duracion = "8 Horas";
                        break;
                    case "16":
                        duracion = "16 Horas";
                        break;

                }
                //string etapa_actual = Convert.ToString(formaciones["etapa_curso"]);
                //switch (etapa_actual)
                //{
                //    case "1":
                //        etapa_actual = "Nivel básico";
                //        break;
                //    case "2":
                //        etapa_actual = "Nivel intermedio";
                //        break;
                //    case "3":
                //        etapa_actual = "Nivel avanzado";
                //        break;
                //}

                dgvFormaciones.Rows.Add(formaciones["nombre_curso"], formaciones["tipo_curso"], formaciones["solicitud_curso"], duracion, formaciones["estatus_curso"], formaciones["nombre_user"]);
                if (formaciones["nombre_curso"].ToString() != "")
                {
                    resultado = 1;
                }
            }
            dgvFormaciones.ClearSelection();
            formaciones.Close();

        }
        private void refrescar()
        {
            string nombre = "";
            string estatus = "";
            llenarDGV(nombre, estatus);
            txtBuscarNombre.Clear();
            cmbxEstatus.SelectedIndex = -1;
        }
        private void llenardatos()
        {
            switch (formaciones.duracion)
            {
                case "4":
                    formaciones.duracion = "4 Horas";
                    break;
                case "8":
                    formaciones.duracion = "8 Horas";
                    break;
                case "16":
                    formaciones.duracion = "16 Horas";
                    break;

            }
            Cursos.nombre_formacion13 = formaciones.nombre_formacion;
            Cursos.estatus_formacion13 = formaciones.estatus;
            Cursos.id_curso13 = formaciones.id_curso;
            Cursos.solicitud_formacion13 = formaciones.solicitado;
            Cursos.nombreCreador_formacion13 = nombre_user;
            Cursos.tipo_formacion13 = formaciones.tipo_formacion;
            Cursos.etapa_formacion13 = formaciones.etapa_curso;
            Cursos.id_user13 = formaciones.id_user;
            Cursos.duracion_formacion13 = formaciones.duracion;
            Cursos.bloque_curso13 = formaciones.bloque_curso;
            Cursos.tiene_ref = formaciones.tiene_ref;
            Cursos.ubicacion_ucs = formaciones.ubicacion_ucs;
            Paquete_instruccional pq = new Paquete_instruccional();
            conexion.cerrarconexion();
            if (conexion.abrirconexion() == true)
                pq = Clases.Formaciones.obtenerTodoPq(conexion.conexion, Cursos.id_pinst);
            conexion.cerrarconexion();

            Cursos.p_contenido = pq.contenido;
            Cursos.p_presentacion = pq.presentacion;
            Cursos.p_manual = pq.manual;
            Cursos.p_bitacora = pq.bitacora;

            if (formaciones.etapa_curso > 1)
            {
                if (formaciones.etapa_curso == 3)
                {
                    //si está en esta etapa, se podrá recoger aulas, horarios, insumos y tipos de refrigerios, si es que aplica
                    if (formaciones.tiene_ref == "No")
                    {
                        if (formaciones.ubicacion_ucs == "No") //Si la formacion NO se realiza en la ucs
                        {
                            if (formaciones.bloque_curso == "1")
                            {
                                Cursos.aula1 = "No aplica";
                                Cursos.aula2 = "No aplica";
                                Cursos.tipo_ref1 = "No aplica";
                                Cursos.tipo_ref2 = "No aplica";
                                Cursos.horario2 = "No aplica";
                                //lo que se puede buscar en este caso: horario 1                             

                            }
                            else if (formaciones.bloque_curso == "2")
                            {
                                Cursos.aula1 = "No aplica";
                                Cursos.aula2 = "No aplica";
                                Cursos.tipo_ref1 = "No aplica";
                                Cursos.tipo_ref2 = "No aplica";
                                MySqlDataReader h2 = Conexion.ConsultarBD("select horario from horarios h inner join cursos c on c.horario_dos=h.idhorarios where c.id_cursos='" + formaciones.id_curso + "'");
                                if (h2.Read())
                                {
                                    Cursos.horario2 = Convert.ToString(h2["horario"]);
                                }
                                h2.Close();
                                //lo que se puede buscar en este caso: horario 1 y horario2
                            }


                        }
                        else //si la formacion se realiza en la UCS BUSCAR:
                        {
                            if (formaciones.bloque_curso == "1")
                            {
                                Cursos.aula2 = "No aplica";
                                Cursos.tipo_ref1 = "No aplica";
                                Cursos.tipo_ref2 = "No aplica";
                                Cursos.horario2 = "No aplica";

                                MySqlDataReader aulas = Conexion.ConsultarBD("select aula_dia1, aula_dia2 from cursos where id_cursos='" + formaciones.id_curso + "'");
                                if (aulas.Read())
                                {
                                    Cursos.aula1 = Convert.ToString(aulas["aula_dia1"]);
                                }
                                aulas.Close();
                                //lo que se puede buscar en este caso: horario 1 y aula 1
                            }
                            else if (formaciones.bloque_curso == "2")
                            {

                                Cursos.tipo_ref1 = "No aplica";
                                Cursos.tipo_ref2 = "No aplica";

                                //lo que se puede buscar en este caso: horario 1 y 2, aula 1 y 2

                                MySqlDataReader h2 = Conexion.ConsultarBD("select horario from horarios h inner join cursos c on c.horario_dos=h.idhorarios where c.id_cursos='" + formaciones.id_curso + "'");
                                if (h2.Read())
                                {
                                    Cursos.horario2 = Convert.ToString(h2["horario"]);
                                }
                                h2.Close();

                                MySqlDataReader aulas = Conexion.ConsultarBD("select aula_dia1, aula_dia2 from cursos where id_cursos='" + formaciones.id_curso + "'");
                                if (aulas.Read())
                                {
                                    Cursos.aula1 = Convert.ToString(aulas["aula_dia1"]);
                                    Cursos.aula2 = Convert.ToString(aulas["aula_dia2"]);

                                }
                                aulas.Close();
                            }


                        }

                    }
                    else //Si la formacion tiene refrigerio, de lógica es porque se va a realizar en la UCS
                    {
                        if (formaciones.bloque_curso == "1")
                        {
                            Cursos.aula2 = "No aplica";
                            Cursos.tipo_ref2 = "No aplica";
                            Cursos.horario2 = "No aplica";
                            //lo que se puede buscar en este caso: horario1, aula1, id_ref1

                            MySqlDataReader ar = Conexion.ConsultarBD("select aula_dia1, ref_nombre from cursos c inner join refrigerios r on r.id_ref=c.id_ref1  where c.id_cursos='" + formaciones.id_curso + "'");
                            if (ar.Read())
                            {
                                Cursos.aula1 = Convert.ToString(ar["aula_dia1"]);
                                Cursos.tipo_ref1 = Convert.ToString(ar["ref_nombre"]);
                            }
                            ar.Close();
                        }
                        else if (formaciones.bloque_curso == "2")
                        {
                            int idr1 = 0, idr2 = 0;
                            //lo que se puede buscar en este caso: TODO

                            MySqlDataReader h2 = Conexion.ConsultarBD("select horario from horarios h inner join cursos c on c.horario_dos=h.idhorarios where c.id_cursos='" + formaciones.id_curso + "'");
                            if (h2.Read())
                            {
                                Cursos.horario2 = Convert.ToString(h2["horario"]);
                            }
                            h2.Close();

                            MySqlDataReader aulas = Conexion.ConsultarBD("select aula_dia1, aula_dia2, id_ref1, id_ref2 from cursos where id_cursos='" + formaciones.id_curso + "'");
                            if (aulas.Read())
                            {
                                Cursos.aula1 = Convert.ToString(aulas["aula_dia1"]);
                                Cursos.aula2 = Convert.ToString(aulas["aula_dia2"]);
                                idr1 = Convert.ToInt32(aulas["id_ref1"]);
                                idr2 = Convert.ToInt32(aulas["id_ref2"]);
                            }

                            aulas.Close();

                            MySqlDataReader r1 = Conexion.ConsultarBD("select ref_nombre from refrigerios where id_ref='" + idr1 + "'");
                            if (r1.Read())
                            {
                                Cursos.tipo_ref1 = Convert.ToString(r1["ref_nombre"]);
                            }
                            r1.Close();

                            MySqlDataReader r2 = Conexion.ConsultarBD("select ref_nombre from refrigerios where id_ref='" + idr2 + "'");
                            if (r2.Read())
                            {
                                Cursos.tipo_ref2 = Convert.ToString(r2["ref_nombre"]);
                            }
                            r2.Close();
                        }
                    }
                }

                if (formaciones.bloque_curso == "1")
                {
                    Cursos.fecha_uno13 = fecha_uno.ToString("dd-MM-yyyy");
                    Cursos.fecha_dos13 = "No aplica";
                }
                else
                {
                    Cursos.fecha_uno13 = fecha_uno.ToString("dd-MM-yyyy");
                    Cursos.fecha_dos13 = fecha_dos.ToString("dd-MM-yyyy");
                }

                //todos los casos se busca el horario 1 como minimo:

                MySqlDataReader leer = Conexion.ConsultarBD("select horario from horarios h inner join cursos c on c.horario_uno=h.idhorarios where c.id_cursos='" + formaciones.id_curso + "'");
                if (leer.Read())
                {
                    Cursos.horario1 = Convert.ToString(leer["horario"]);
                }
                leer.Close();

            }
            MessageBox.Show(Cursos.ubicacion_ucs + Cursos.tiene_ref + Cursos.horario1 + Cursos.horario2 + Cursos.tipo_ref1 + Cursos.tipo_ref2 + Cursos.aula1 + Cursos.aula2);
        }

        private void vaciardatos()
        {
            Cursos.nombre_formacion13 = "";
            Cursos.estatus_formacion13 = "";
            Cursos.id_curso13 = 0;
            Cursos.solicitud_formacion13 = "";
            Cursos.nombreCreador_formacion13 = "";
            Cursos.tipo_formacion13 = "";
            Cursos.etapa_formacion13 = 0;
            Cursos.id_user13 = 0;
            Cursos.duracion_formacion13 = "";
            Cursos.bloque_curso13 = "";
            Cursos.tiene_ref = "";
            Cursos.ubicacion_ucs = "";
            Cursos.p_contenido = "";
            Cursos.p_presentacion = "";
            Cursos.p_manual = "";
            Cursos.p_bitacora = "";
            Cursos.aula1 = "";
            Cursos.aula2 = "";
            Cursos.tipo_ref1 = "";
            Cursos.tipo_ref2 = "";
            Cursos.horario1 = "";
            Cursos.horario2 = "";
            Cursos.fecha_uno13 = "";
            Cursos.fecha_dos13 = "";
        }
        private void llenarReporte1()
        {
            int id_user1 = Cursos.id_user13;
            MySqlDataReader consulta = Conexion.ConsultarBD("SELECT duracionE1, duracionE2, duracionE3 FROM cursos WHERE id_cursos='" + Cursos.id_curso13 + "'");
            if (consulta.Read())
            {
                duracion = Convert.ToString(consulta["duracionE1"]);
                duracion2 = Convert.ToString(consulta["duracionE2"]);
                duracion3 = Convert.ToString(consulta["duracionE3"]);
            }
            fdg.nombre_formacion = Cursos.nombre_formacion13;
            fdg.tipo_formacion = Cursos.tipo_formacion13;
            fdg.estatus_formacion = Cursos.estatus_formacion13;
            //txtSolicitud.Text = Cursos.solicitud_formacion13;
            fdg.nombre_creador = Cursos.nombreCreador_formacion13;
            fdg.duracion_formacion = Cursos.duracion_formacion13;
            fdg.listaEtapa.Clear();

            if (Cursos.estatus_formacion13 == "En curso" || Cursos.estatus_formacion13 == "Reprogramado")
            {
                if (Cursos.etapa_formacion13 == 1)
                {
                    fdg.fecha_inicio = "Sin asignar";
                    fdg.fecha_culminacion = "Sin asignar";

                    etapaf.etapa_formacion = "Nivel básico";
                    etapaf.estatus_etapa = "En proceso";
                    etapaf.tiempo_etapa = " " + duracion + " min.";
                    fdg.tiempo_total = duracion; //en esta etapa, el tiempo total es el unico que hay
                    fdg.listaEtapa.Add(etapaf); //agrega la primera etapa de la formacion

                    etapaf2.etapa_formacion = "Nivel intermedio";
                    etapaf2.estatus_etapa = "Sin iniciar";
                    etapaf2.tiempo_etapa = "0 min.";
                    fdg.listaEtapa.Add(etapaf2); //agrega la segunda etapa de la formacion

                    etapaf3.etapa_formacion = "Nivel avanzado";
                    etapaf3.estatus_etapa = "Sin iniciar";
                    etapaf3.tiempo_etapa = "0 min.";
                    fdg.listaEtapa.Add(etapaf3); //agrega la tercera etapa de la formacion
                    

                }
                else if (Cursos.etapa_formacion13 == 2)
                {
                    TimeSpan t1 = TimeSpan.Parse(duracion);
                    TimeSpan tt = TimeSpan.Parse(duracion2) + t1;
                    fdg.tiempo_total = tt.ToString(); 
                    fdg.fecha_inicio = Cursos.fecha_uno13; 
                    fdg.fecha_culminacion = Cursos.fecha_dos13;

                    etapaf.etapa_formacion = "Nivel básico";
                    etapaf.estatus_etapa = "Finalizada";
                    etapaf.tiempo_etapa = " " + duracion + " min.";
                    fdg.listaEtapa.Add(etapaf); //agrega la primera etapa de la formacion

                    etapaf2.etapa_formacion = "Nivel intermedio";
                    etapaf2.estatus_etapa = "En proceso";
                    etapaf2.tiempo_etapa = " " + duracion2 + " min.";
                    fdg.listaEtapa.Add(etapaf2); //agrega la segunda etapa de la formacion

                    etapaf3.etapa_formacion = "Nivel avanzado";
                    etapaf3.estatus_etapa = "Sin iniciar";
                    etapaf3.tiempo_etapa = "0 min.";
                    fdg.listaEtapa.Add(etapaf3); //agrega la tercera etapa de la formacion

                }
                else if (Cursos.etapa_formacion13 == 3)
                {
                    TimeSpan t1 = TimeSpan.Parse(duracion);
                    TimeSpan t2 = TimeSpan.Parse(duracion2);
                    TimeSpan tt = TimeSpan.Parse(duracion3) + t1 + t2;
                    fdg.tiempo_total = tt.ToString();
                    fdg.fecha_inicio = Cursos.fecha_uno13;
                    fdg.fecha_culminacion = Cursos.fecha_dos13;

                    etapaf.etapa_formacion = "Nivel básico";
                    etapaf.estatus_etapa = "Finalizada";
                    etapaf.tiempo_etapa = " " + duracion + " min.";
                    fdg.listaEtapa.Add(etapaf); //agrega la primera etapa de la formacion

                    etapaf2.etapa_formacion = "Nivel intermedio";
                    etapaf2.estatus_etapa = "Finalizada";
                    etapaf2.tiempo_etapa = " " + duracion2 + " min.";
                    fdg.listaEtapa.Add(etapaf2); //agrega la segunda etapa de la formacion

                    etapaf3.etapa_formacion = "Nivel avanzado";
                    etapaf3.estatus_etapa = "En proceso";
                    etapaf3.tiempo_etapa = " " + duracion3 + " min.";
                    fdg.listaEtapa.Add(etapaf3); //agrega la tercera etapa de la formacion
                   
                }
            }
            else if (Cursos.estatus_formacion13 == "Suspendido" || Cursos.estatus_formacion13 == "Finalizado")
            {
                TimeSpan t1 = TimeSpan.Parse(duracion);
                TimeSpan t2 = TimeSpan.Parse(duracion2);
                TimeSpan tt = TimeSpan.Parse(duracion3) + t1 + t2;
                fdg.tiempo_total = tt.ToString();
                fdg.fecha_inicio = Cursos.fecha_uno13;
                fdg.fecha_culminacion = Cursos.fecha_dos13;

                etapaf.etapa_formacion = "Nivel básico";
                etapaf.estatus_etapa = "Finalizada";
                etapaf.tiempo_etapa = " " + duracion + " min.";
                fdg.listaEtapa.Add(etapaf); //agrega la primera etapa de la formacion

                etapaf2.etapa_formacion = "Nivel intermedio";
                etapaf2.estatus_etapa = "Finalizada";
                etapaf2.tiempo_etapa = " " + duracion2 + " min.";
                fdg.listaEtapa.Add(etapaf2); //agrega la segunda etapa de la formacion

                etapaf3.etapa_formacion = "Nivel avanzado";
                etapaf3.estatus_etapa = "Finalizada";
                etapaf3.tiempo_etapa = " " + duracion3 + " min.";
                fdg.listaEtapa.Add(etapaf3); //agrega la tercera etapa de la formacion

            }

        }
        private void btnReporte1_Click(object sender, EventArgs e)
        {
            if (dgvFormaciones.SelectedRows.Count == 1)
            {
                //en este reporte solo es necesario que seleccione un registro del dgvformaciones
                //para crear el reporte
                llenardatos(); //aqui se llenan los datos de la formacion
                llenarReporte1();
                pic = Image.FromFile(ruta);
                fdg.Logo = GetBytes(pic);
                fdg.fecha_actual = DateTime.Today.ToString("dd-MM-yyyy");
                RPT_HORAS_DEDICADAS r1 = new RPT_HORAS_DEDICADAS();
                r1.InfoGeneral.Add(fdg);
                r1.EtapasF = fdg.listaEtapa;
                r1.ShowDialog();
                vaciardatos();
                
            }
            else
            {
                MessageBox.Show("Debe seleccionar una formación de la lista.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }




        }
        private void llenarReporteTiempoTipos()
        {
            llenardatos();
            string tipo = Cursos.tipo_formacion13;
            fecha_actual = DateTime.Today.ToString("yyyy-MM-dd");
            DateTime trampa = Convert.ToDateTime(fecha_vieja);
            //MessageBox.Show(fecha_vieja + " la segunda se pasa por parametro " + trampa.ToString());
            datosF.fecha_inicio =fecha_vieja;
            datosF.tipo_formacion = tipo;
            
            int nroFormaciones = 0;
            //hará select de los cursos de tipo y fechas comprendidas
            MySqlDataReader leer = Conexion.ConsultarBD("select * from cursos where tipo_curso='"+tipo+"' and fecha_creacion between '"+fecha_vieja+"' and '"+fecha_actual+"'");
            while (leer.Read())
            {
                nroFormaciones += 1;
            }
            datosF.tiempo_total = nroFormaciones.ToString(); //ignorar nombre de variable, aqui se guardan cuantas formaciones de ese tipo se han realizado en ese tiempo
        }
        private void btnReporte2_Click(object sender, EventArgs e) //relacion tiempo-tipo de formaciones
        {
            if (dgvFormaciones.SelectedRows.Count == 1)
            {
                if (cmbxPeriodoTiempo.SelectedIndex == -1)
                {
                    errorProviderPeriodo.SetError(cmbxPeriodoTiempo, "Debe seleccionar una de las opciones.");

                }
                else
                {
                    errorProviderPeriodo.SetError(cmbxPeriodoTiempo, "");
                    //si todo ok, carga el reporte, puede  crear reporte
                    llenarReporteTiempoTipos();
                    datosF.fecha_actual = DateTime.Today.ToString("dd-MM-yyyy");
                    pic = Image.FromFile(ruta);
                    datosF.Logo = GetBytes(pic);
                    RPT_TIEMPO_TIPO rtt = new RPT_TIEMPO_TIPO();
                    rtt.info.Add(datosF);
                    rtt.ShowDialog();
                    vaciardatos();
                }
               
            }else
            {
                MessageBox.Show("Debe seleccionar una formación de la lista.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                
            }               
          
           
        }      
        private void btnRpt_TimeDur_Click(object sender, EventArgs e)
        {
            if (dgvFormaciones.SelectedRows.Count == 1)
            {
                if (cmbxPeriodoTiempo.SelectedIndex == -1)
                {
                    errorProviderPeriodo.SetError(cmbxPeriodoTiempo, "Debe seleccionar una de las opciones.");

                }
                else
                {
                    errorProviderPeriodo.SetError(cmbxPeriodoTiempo, "");
                    //si todo ok, carga el reporte, puede  crear reporte

                }

            }
            else
            {
                MessageBox.Show("Debe seleccionar una formación de la lista.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }
        }
        private void btnRpt_TipoDur_Click(object sender, EventArgs e)
        {
            if (dgvFormaciones.SelectedRows.Count == 1)
            {
                if (cmbxPeriodoTiempo.SelectedIndex == -1)
                {
                    errorProviderPeriodo.SetError(cmbxPeriodoTiempo, "Debe seleccionar una de las opciones.");

                }
                else
                {
                    errorProviderPeriodo.SetError(cmbxPeriodoTiempo, "");
                    //si todo ok, carga el reporte, puede  crear reporte

                }

            }
            else
            {
                MessageBox.Show("Debe seleccionar una formación de la lista.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }
        }
        private void btnRefrescar_Click(object sender, EventArgs e)
        {
            refrescar();
        }

        private void txtNombrePart_KeyPress(object sender, KeyPressEventArgs e)
        {
            Clases.Paneles.sololetras(e);
            if (txtBuscarNombre.Text.Length == 0)
            {
                e.KeyChar = e.KeyChar.ToString().ToUpper().ToCharArray()[0];
            }
            else if (txtBuscarNombre.Text.Length > 0)
            {
                e.KeyChar = e.KeyChar.ToString().ToLower().ToCharArray()[0];
            }
        }
        private void cmbxEstatus_SelectionChangeCommitted(object sender, EventArgs e)
        {
            string estatus = Convert.ToString(cmbxEstatus.SelectedIndex);
            switch (estatus)
            {
                case "0":
                    estatus = "En curso";
                    break;
                case "1":
                    estatus = "Reprogramado";
                    break;
                case "2":
                    estatus = "Suspendido";
                    break;
                case "3":
                    estatus = "Finalizado";
                    break;
            }
            formaciones.estatus = estatus;
        }

       

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (txtBuscarNombre.Text == "" && cmbxEstatus.SelectedIndex == -1)
            {
                errorProviderCmbxNombre.SetError(txtBuscarNombre, "Debe escribir el nombre de una formación.");
                errorProviderCmbxEstatus.SetError(cmbxEstatus, "Debe seleccionar un estatus.");
                txtBuscarNombre.Focus();
            }
            else
            {
                errorProviderCmbxNombre.SetError(txtBuscarNombre, "");
                errorProviderCmbxEstatus.SetError(cmbxEstatus, "");
                if (txtBuscarNombre.Text == "")
                {
                    errorProviderCmbxNombre.SetError(txtBuscarNombre, "Debe escribir el nombre de una formación.");
                    txtBuscarNombre.Focus();
                }
                else
                {
                    errorProviderCmbxNombre.SetError(txtBuscarNombre, "");
                    if (cmbxEstatus.SelectedIndex == -1)
                    {
                        errorProviderCmbxEstatus.SetError(cmbxEstatus, "Debe seleccionar un estatus.");
                        cmbxEstatus.Focus();
                    }
                    else
                    {
                        resultado = 0;
                        errorProviderCmbxEstatus.SetError(cmbxEstatus, "");
                        string nombre = txtBuscarNombre.Text;
                        string estatus = formaciones.estatus;
                        llenarDGV(nombre, estatus);
                        if (resultado == 1)
                        {
                            dgvFormaciones.CurrentRow.Selected = true;
                            resultado = 0;
                            txtBuscarNombre.Clear();
                            cmbxEstatus.SelectedIndex = -1;

                        }
                        else
                        {
                            MessageBox.Show("La búsqueda no ha arrojado resultados.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            refrescar();
                        }
                    }
                }
            }
        }

        private void dgvFormaciones_MouseClick(object sender, MouseEventArgs e)
        {
            if (dgvFormaciones.SelectedRows.Count == 1)
            {
                formaciones.nombre_formacion = dgvFormaciones.SelectedRows[0].Cells[0].Value.ToString();
                formaciones.tipo_formacion = dgvFormaciones.SelectedRows[0].Cells[1].Value.ToString();
                formaciones.solicitado = dgvFormaciones.SelectedRows[0].Cells[2].Value.ToString();
                formaciones.duracion = dgvFormaciones.SelectedRows[0].Cells[3].Value.ToString();
                formaciones.estatus = dgvFormaciones.SelectedRows[0].Cells[4].Value.ToString();

                //string etapa = dgvFormaciones.SelectedRows[0].Cells[5].Value.ToString();
                //switch (etapa)
                //{
                //    case "Nivel básico":
                //        etapa = "1";
                //        break;
                //    case "Nivel intermedio":
                //        etapa = "2";
                //        break;
                //    case "Nivel avanzado":
                //        etapa = "3";
                //        break;
                //}

                switch (formaciones.duracion)
                {
                    case "4 Horas":
                        formaciones.duracion = "4";
                        break;
                    case "8 Horas":
                        formaciones.duracion = "8";
                        break;
                    case "16 Horas":
                        formaciones.duracion = "16";
                        break;

                }
                
                nombre_user = dgvFormaciones.SelectedRows[0].Cells[5].Value.ToString();
                MySqlDataReader id = Conexion.ConsultarBD("SELECT id_user FROM usuarios WHERE nombre_user = '" + nombre_user + "'");
                if (id.Read())
                {
                    formaciones.id_user = Convert.ToInt32(id["id_user"]);
                }
                id.Close();

                MySqlDataReader id_curso = Conexion.ConsultarBD("SELECT id_cursos FROM cursos WHERE nombre_curso='" + formaciones.nombre_formacion + "' AND estatus_curso='" + formaciones.estatus + "' AND tipo_curso='" + formaciones.tipo_formacion + "' AND duracion_curso='" + formaciones.duracion + "' AND id_usuario1=" + formaciones.id_user + " and solicitud_curso='" + formaciones.solicitado + "' ");
                if (id_curso.Read())
                {
                    formaciones.id_curso = Convert.ToInt32(id_curso["id_cursos"]);
                    MessageBox.Show(formaciones.id_curso.ToString());
                }
                id_curso.Close();

                MySqlDataReader leer = Conexion.ConsultarBD("SELECT bloque_curso, tiene_ref, ubicacion_ucs, id_p_inst, etapa_curso from cursos where id_cursos='" + formaciones.id_curso + "'");
                if (leer.Read())
                {
                    formaciones.bloque_curso = Convert.ToString(leer["bloque_curso"]);
                    formaciones.tiene_ref = Convert.ToString(leer["tiene_ref"]);
                    formaciones.ubicacion_ucs = Convert.ToString(leer["ubicacion_ucs"]);
                    formaciones.etapa_curso = Convert.ToInt32(leer["etapa_curso"]);
                    Cursos.id_pinst = Convert.ToInt32(leer["id_p_inst"]);

                }
                leer.Close();

                switch (formaciones.tiene_ref)
                {
                    case "0":
                        formaciones.tiene_ref = "No";
                        break;
                    case "1":
                        formaciones.tiene_ref = "Si";
                        break;
                }

                if (formaciones.etapa_curso > 1)
                {
                    if (formaciones.bloque_curso == "1")
                    {
                        MySqlDataReader leer2 = Conexion.ConsultarBD("SELECT fecha_uno from cursos where id_cursos='" + formaciones.id_curso + "'");
                        if (leer2.Read())
                        {
                            fecha_uno = Convert.ToDateTime(leer2["fecha_uno"]);

                        }
                        leer2.Close();
                    }
                    else
                    {
                        MySqlDataReader leer2 = Conexion.ConsultarBD("SELECT fecha_uno, fecha_dos from cursos where id_cursos='" + formaciones.id_curso + "'");
                        if (leer2.Read())
                        {
                            fecha_uno = Convert.ToDateTime(leer2["fecha_uno"]);
                            fecha_dos = Convert.ToDateTime(leer2["fecha_dos"]);

                        }
                        leer2.Close();
                    }
                    int id_ctp = 0;
                    MySqlDataReader leer3 = Conexion.ConsultarBD("SELECT id_ctf from cursos_tienen_participantes where ctp_id_curso='" + formaciones.id_curso + "'");
                    if (leer3.Read())
                    {
                        id_ctp = Convert.ToInt32(leer3["id_ctf"]);
                    }
                    if (id_ctp == 0)
                    {
                       //es que no tiene participantes
                    }
                    else
                    {
                       //aqui si tiene participantes
                    }
                }


            }
        }
        private void cmbxPeriodoTiempo_SelectionChangeCommitted(object sender, EventArgs e)
        {
            //se deberá guardar lo que seleccione el usuario para la creacion del reporte
            if (cmbxPeriodoTiempo.SelectedIndex == 0)
            {
                //si seleccionó ultimo mes, a la fecha actual se le resta 30 dias, y se buscará en base a esas dos fechas y el tipo que haya escogido en el dgv
                DateTime fa = DateTime.Today.AddMonths(-1);
                fecha_vieja = fa.ToString("yyyy-MM-dd");
            }else
            {
                //si selecciona ultimos tres meses a la fecha actual se le restan 3 meses
                DateTime fv = DateTime.Today.AddMonths(-3);
                fecha_vieja = fv.ToString("yyyy-MM-dd");
            }
        }


        //private List<Formaciones> Lista_filtrada()
        //{
        //    List<Formaciones> lista = new List<Formaciones>();
        //    MySqlDataReader leer =  Conexion.ConsultarBD("select ")
        //    return lista;
        //}

        //private void llenarFormaciones()
        //{

        //}
        //private void cmbxTipoFormacion_SelectionChangeCommitted(object sender, EventArgs e)
        //{
        //    //si selecciona algo de aqui, se activa el combobox de duración y se llena en base al tipo
        //    string tipo = Convert.ToString(cmbxTipoFormacion.SelectedIndex);
        //    switch (tipo)
        //    {
        //        case "0":
        //            tipo = "Abierto";
        //            cmbxDuracionFormacion.Items.Clear();
        //            cmbxDuracionFormacion.Items.Add("4 horas");
        //            cmbxDuracionFormacion.Items.Add("8 horas y 1 bloque");
        //            cmbxDuracionFormacion.Items.Add("8 horas y 2 bloques");
        //            cmbxDuracionFormacion.Items.Add("16 horas");
        //            break;
        //        case "1":
        //            tipo = "FEE";
        //            cmbxDuracionFormacion.Items.Clear();
        //            cmbxDuracionFormacion.Items.Add("8 horas");
        //            break;
        //        case "2":
        //            tipo = "INCES";
        //            cmbxDuracionFormacion.Items.Clear();
        //            cmbxDuracionFormacion.Items.Add("16 horas");
        //            break;
        //        case "3":
        //            tipo = "InCompany";
        //            cmbxDuracionFormacion.Items.Clear();
        //            cmbxDuracionFormacion.Items.Add("4 horas");
        //            cmbxDuracionFormacion.Items.Add("8 horas y 1 bloque");
        //            cmbxDuracionFormacion.Items.Add("8 horas y 2 bloques");
        //            cmbxDuracionFormacion.Items.Add("16 horas");
        //            break;
        //            //falta recoger el tipo en una variable global para hacer el reporte

        //    }
        //    f.tipo_formacion = tipo;
        //    cmbxDuracionFormacion.Enabled = true;
        //    cmbxDuracionFormacion.Focus();
        //    //add evento validating tipo, duracion, todos
        //}

        //private void cmbxDuracionFormacion_SelectionChangeCommitted(object sender, EventArgs e)
        //{
        //    //si se selecciona algo de aquí, se habilita el siguiente combo (estatus)

        //    string duracion = Convert.ToString(cmbxDuracionFormacion.SelectedIndex);
        //    //falta: recoger lo que se seleccione aqui en una variable global para hacer el reporte
        //    if (f.tipo_formacion == "Abierto" || f.tipo_formacion == "InCompany")
        //    {
        //        switch (duracion)
        //        {
        //            case "0":
        //                f.duracion = "4";
        //                f.bloque_curso = "1";
        //                break;
        //            case "1":
        //                f.duracion = "8";
        //                f.bloque_curso = "1";
        //                break;
        //            case "2":
        //                f.duracion = "8";
        //                f.bloque_curso = "2";
        //                break;
        //            case "3":
        //                f.duracion = "16";
        //                f.bloque_curso = "2";
        //                break;
        //        }

        //    } else if (f.tipo_formacion == "FEE")
        //    {
        //        switch (duracion)
        //        {
        //            case "0":
        //                f.duracion = "8";
        //                f.bloque_curso = "1";
        //                break;

        //        }

        //    }
        //    else if (f.tipo_formacion == "INCES")
        //    {
        //        switch (duracion)
        //        {
        //            case "0":
        //                f.duracion = "16";
        //                f.bloque_curso = "2";
        //                break;

        //        }
        //    }
        //    cmbxEstatusFormación.Enabled = true;
        //    cmbxEstatusFormación.Focus();
        //}

        //private void cmbxEstatusFormación_SelectionChangeCommitted(object sender, EventArgs e)
        //{
        //    //si se selecciona algo aqui, se habilita el otro cmbx y se cargan los datos dependiendo
        //    //de la informacion contenida en todos los anteriores a el
        //    string estatus = Convert.ToString(cmbxEstatusFormación.SelectedIndex);
        //    switch (estatus)
        //    {
        //        case "0":
        //            estatus = "En curso";
        //            cmbxNombreFormacion.Items.Clear();

        //            break;
        //        case "1":
        //            estatus = "Reprogramado";
        //            cmbxNombreFormacion.Items.Clear();

        //            break;
        //        case "2":
        //            estatus = "Suspendido";
        //            cmbxNombreFormacion.Items.Clear();

        //            break;
        //        case "3":
        //            estatus = "Finalizado";
        //            cmbxNombreFormacion.Items.Clear();

        //            break;
        //            //falta recoger el estatus en una variable global para hacer el reporte
        //            //y para llenar el cmbx nombresFormacion

        //    }
        //    f.estatus = estatus;
        //    cmbxNombreFormacion.Enabled = false;
        //    //cmbxNombreFormacion.Items.Add("No existe formación con esas especificaciones");

        //}

        //private void cmbxNombreFormacion_SelectionChangeCommitted(object sender, EventArgs e)
        //{
        //    //aqui se deberá recoger el id del curso seleccionado para trabajarlo de manera global en el reporte
        //    //se verifica si viene referenciado desde el reporte2 para habilitar el periodo.
        //    if (cmbxNombreFormacion.SelectedItem.ToString() != "No existe formación con esas especificaciones")
        //    {
        //        btnCrearReporte.Enabled = true;
        //        if (reporte2 == true)
        //        {
        //            cmbxPeriodoTiempo.Enabled = true;
        //        }
        //    }
        //    else
        //    {
        //        btnCrearReporte.Enabled = false;
        //    }

        //}

    }
}
