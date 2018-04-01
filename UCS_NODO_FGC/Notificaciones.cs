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

namespace UCS_NODO_FGC
{
    public partial class Notificaciones : Form
    {
        string activo = "";
        List<Formaciones> ListaE2SiP = new List<Formaciones>();
        List<Formaciones> ListaE2NoP = new List<Formaciones>();
        List<Formaciones> ListaE3SiP = new List<Formaciones>();
        List<Formaciones> ListaE3NoP = new List<Formaciones>();
        List<Formaciones> ListaE2HOY = new List<Formaciones>();
        
        public Notificaciones()
        {
            InitializeComponent();
            Comprobaciones(); //al iniciar el formulario, se hacen las comprobaciones
            Generar_avisos();
        }

        private void Notificaciones_Load(object sender, EventArgs e)
        {
            this.Location = new Point(-5, 0);
            #region cursosRealizados
            //calcular cuantas formaciones ha creado el usuario
            int nroF = 0;
            MySqlDataReader leer = Conexion.ConsultarBD("select * from cursos where id_usuario1='" + Usuario_logeado.id_usuario + "'");
            while (leer.Read())
            {
                nroF += 1;
            }
            txtNroCursos.Text = nroF.ToString();
            if (Usuario_logeado.cargo_usuario == "Asistente")
            {
                gpbCursosRealizados.Visible = false;
            } else
            {
                gpbCursosRealizados.Visible = true;
            }
            #endregion
            pnlLeyenda.Height = 30;
            txtEspacio.Text = "";
            txtEspacio.Height = 18;
        }
        private void Comprobaciones()
        {
            #region ETAPA2
            List<Formaciones> lista_reprogramados = new List<Formaciones>();

            //llenar todo si la etapa sigue siendo 2 y está en curso
            MySqlDataReader leerR = Conexion.ConsultarBD("SELECT * FROM cursos WHERE etapa_curso='2' AND estatus_curso='En curso' AND ubicacion_ucs='Si'");
            while (leerR.Read())
            {
                Formaciones f = new Formaciones();
                f.id_curso = Convert.ToInt32(leerR["id_cursos"]);
                f.dia1 = Convert.ToDateTime(leerR["fecha_uno"]);
                f.tipo_formacion = Convert.ToString(leerR["tipo_curso"]);
                f.nombre_formacion = Convert.ToString(leerR["nombre_curso"]);
                lista_reprogramados.Add(f);
            }
            leerR.Close();

            //lista de reprogramados
            for (int i = 0; i < lista_reprogramados.Count; i++)
            {
                string dia_anterior = lista_reprogramados[i].dia1.AddDays(-1).ToShortDateString();
                if (dia_anterior == DateTime.Today.ToShortDateString())
                {
                    MySqlDataReader tieneP = Conexion.ConsultarBD("select * from cursos_tienen_participantes where ctp_id_curso='" + lista_reprogramados[i].id_curso + "'");
                    if (tieneP.Read())
                    {
                        Formaciones f = new Formaciones();
                        f.id_curso = lista_reprogramados[i].id_curso;
                        f.nombre_formacion = lista_reprogramados[i].nombre_formacion;
                        f.tipo_formacion = lista_reprogramados[i].tipo_formacion;
                        f.tiene_ref = "Si"; //ignorar el nombre, tiene_ref se está usando como sustituto a un "tiene_participante"
                        ListaE2SiP.Add(f);
                        //AVISAR QUE DEBE SELECCIONAR AULA Y TODO DE LA ETAPA 3
                        //CONFIRMAR ASISTENCIA DE FACILITADOR
                        //AVISAR QUE DEBE VERIFICAR INMOBILIARIO Y ACCESO A INTERNET  
                    }
                    else
                    {
                        //si no devuelve nada, es que no hay participantes añadidos a este curso. ENTONCES se avisa que el dia de mañana se cambiará el estatus
                        Formaciones f = new Formaciones();
                        f.id_curso = lista_reprogramados[i].id_curso;
                        f.nombre_formacion = lista_reprogramados[i].nombre_formacion;
                        f.tipo_formacion = lista_reprogramados[i].tipo_formacion;
                        f.tiene_ref = "No"; //ignorar el nombre, tiene_ref se está usando como sustituto a un "tiene_participante"
                        ListaE2NoP.Add(f);
                    }
                    tieneP.Close();
                }
                else if (lista_reprogramados[i].dia1.ToShortDateString() == DateTime.Today.ToShortDateString())
                {
                    MySqlDataReader tieneP = Conexion.ConsultarBD("select * from cursos_tienen_participantes where ctp_id_curso='" + lista_reprogramados[i].id_curso + "'");
                    if (tieneP.Read())
                    {
                        //AVISAR QUE HOY SE DA LA FORMACION Y NO HA SELECCIONADO AULA NI HORARIO NI NADA DE LA ETAPA 3
                        Formaciones f = new Formaciones();
                        f.id_curso = lista_reprogramados[i].id_curso;
                        f.nombre_formacion = lista_reprogramados[i].nombre_formacion;
                        f.tipo_formacion = lista_reprogramados[i].tipo_formacion;
                        f.tiene_ref = "Si"; //ignorar el nombre, tiene_ref se está usando como sustituto a un "tiene_participante"
                        ListaE2HOY.Add(f);
                    }
                    else
                    {
                        MySqlDataReader cambiar = Conexion.ConsultarBD("UPDATE cursos SET estatus_curso='Reprogramado' WHERE id_cursos='" + lista_reprogramados[i].id_curso + "'");
                        cambiar.Close();
                    }
                }else if(lista_reprogramados[i].dia1 < DateTime.Today)
                {
                    MySqlDataReader cambiar = Conexion.ConsultarBD("UPDATE cursos SET estatus_curso='Reprogramado' WHERE id_cursos='" + lista_reprogramados[i].id_curso + "'");
                    cambiar.Close();
                }
                else
                {
                    //si lo anterior no ocurre, comprobar todas aquellas formaciones que se dan el lunes,
                    // y ver si el dia actual es viernes, para mostrar los avisos (del facilitador y lo demás)
                    DayOfWeek prueba = lista_reprogramados[i].dia1.DayOfWeek;
                    DayOfWeek pruebaV = DateTime.Today.DayOfWeek;
                    //string lunes = "Lunes";
                    if (prueba == DayOfWeek.Monday && pruebaV == DayOfWeek.Friday)
                    {
                        MySqlDataReader tieneP = Conexion.ConsultarBD("select * from cursos_tienen_participantes where ctp_id_curso='" + lista_reprogramados[i].id_curso + "'");
                        if (tieneP.Read())
                        {
                            Formaciones f = new Formaciones();
                            f.id_curso = lista_reprogramados[i].id_curso;
                            f.nombre_formacion = lista_reprogramados[i].nombre_formacion;
                            f.tipo_formacion = lista_reprogramados[i].tipo_formacion;
                            f.tiene_ref = "Si"; //ignorar el nombre, tiene_ref se está usando como sustituto a un "tiene_participante"
                            ListaE2SiP.Add(f);
                            //AVISAR QUE DEBE SELECCIONAR AULA Y TODO DE LA ETAPA 3
                            //CONFIRMAR ASISTENCIA DE FACILITADOR
                            //AVISAR QUE DEBE VERIFICAR INMOBILIARIO Y ACCESO A INTERNET  
                        }
                        else
                        {
                            //si no devuelve nada, es que no hay participantes añadidos a este curso. ENTONCES se avisa que el dia de mañana se cambiará el estatus
                            Formaciones f = new Formaciones();
                            f.id_curso = lista_reprogramados[i].id_curso;
                            f.nombre_formacion = lista_reprogramados[i].nombre_formacion;
                            f.tipo_formacion = lista_reprogramados[i].tipo_formacion;
                            f.tiene_ref = "No"; //ignorar el nombre, tiene_ref se está usando como sustituto a un "tiene_participante"
                            ListaE2NoP.Add(f);
                        }
                        tieneP.Close();
                    }
                }


            }
            lista_reprogramados.Clear(); //limpiar lista
            
            #endregion

            #region ETAPA3 En curso
            List<Formaciones> lista = new List<Formaciones>();
            //también se evaluará 1 dia si es el día anterior a una formación (para la validación con el facilitador) --> mostrar aviso
            MySqlDataReader leer2 = Conexion.ConsultarBD("SELECT * FROM cursos WHERE etapa_curso='3' AND estatus_curso='En curso' AND ubicacion_ucs='Si'");
            while (leer2.Read())
            {
                Formaciones f = new Formaciones();
                f.id_curso = Convert.ToInt32(leer2["id_cursos"]);
                f.dia1 = DateTime.Parse(leer2["fecha_uno"].ToString());
                f.tipo_formacion = Convert.ToString(leer2["tipo_curso"]);
                f.nombre_formacion = Convert.ToString(leer2["nombre_curso"]);
                
                lista.Add(f);
            }
            leer2.Close();

            DateTime dia = DateTime.Today;
            for (int i = 0; i < lista.Count; i++)
            {
                DateTime diaA = lista[i].dia1;
                if (diaA.AddDays(-1) == dia)
                {
                    //si esta condicion se cumple, es que el dia de hoy es un dia anterior al de una formación.
                    MySqlDataReader tieneP = Conexion.ConsultarBD("select * from cursos_tienen_participantes where ctp_id_curso='" + lista[i].id_curso + "'");
                    if (tieneP.Read())
                    {
                        Formaciones f = new Formaciones();
                        f.id_curso = lista[i].id_curso;
                        f.nombre_formacion = lista[i].nombre_formacion;
                        f.tipo_formacion = lista[i].tipo_formacion;
                        f.tiene_ref = "Si"; //ignorar el nombre, tiene_ref se está usando como sustituto a un "tiene_participante"
                        ListaE3SiP.Add(f);
                        //AVISAR QUE DEBE SELECCIONAR AULA Y TODO DE LA ETAPA 3
                        //CONFIRMAR ASISTENCIA DE FACILITADOR
                        //AVISAR QUE DEBE VERIFICAR INMOBILIARIO Y ACCESO A INTERNET  
                    }
                    else
                    {
                        //si no devuelve nada, es que no hay participantes añadidos a este curso. ENTONCES se avisa que el dia de mañana se cambiará el estatus
                        Formaciones f = new Formaciones();
                        f.id_curso = lista[i].id_curso;
                        f.nombre_formacion = lista[i].nombre_formacion;
                        f.tipo_formacion = lista[i].tipo_formacion;
                        f.tiene_ref = "No"; //ignorar el nombre, tiene_ref se está usando como sustituto a un "tiene_participante"
                        ListaE3NoP.Add(f);
                    }
                    tieneP.Close();

                }
                else if (lista[i].dia1 < DateTime.Today)
                {
                    MySqlDataReader cambiar = Conexion.ConsultarBD("UPDATE cursos SET estatus_curso='Reprogramado' WHERE id_cursos='" + lista[i].id_curso + "'");
                    cambiar.Close();
                }
                else
                {
                    //si lo anterior no ocurre, comprobar todas aquellas formaciones que se dan el lunes,
                    // y ver si el dia actual es viernes, para mostrar los avisos (del facilitador y lo demás)
                    DayOfWeek prueba = lista[i].dia1.DayOfWeek;
                    DayOfWeek pruebaV = DateTime.Today.DayOfWeek;
                    //string lunes = "Lunes";
                    if (prueba == DayOfWeek.Monday && pruebaV == DayOfWeek.Friday)
                    {
                        // si esta condicion se cumple, es que el dia de hoy es un dia anterior al de una formación.
                     MySqlDataReader tieneP = Conexion.ConsultarBD("select * from cursos_tienen_participantes where ctp_id_curso='" + lista[i].id_curso + "'");
                        if (tieneP.Read())
                        {
                            Formaciones f = new Formaciones();
                            f.id_curso = lista[i].id_curso;
                            f.nombre_formacion = lista[i].nombre_formacion;
                            f.tipo_formacion = lista[i].tipo_formacion;
                            f.tiene_ref = "Si"; //ignorar el nombre, tiene_ref se está usando como sustituto a un "tiene_participante"
                            ListaE3SiP.Add(f);
                            //AVISAR QUE DEBE SELECCIONAR AULA Y TODO DE LA ETAPA 3
                            //CONFIRMAR ASISTENCIA DE FACILITADOR
                            //AVISAR QUE DEBE VERIFICAR INMOBILIARIO Y ACCESO A INTERNET  
                        }
                        else
                        {
                            //si no devuelve nada, es que no hay participantes añadidos a este curso. ENTONCES se avisa que el dia de mañana se cambiará el estatus
                            Formaciones f = new Formaciones();
                            f.id_curso = lista[i].id_curso;
                            f.nombre_formacion = lista[i].nombre_formacion;
                            f.tipo_formacion = lista[i].tipo_formacion;
                            f.tiene_ref = "No"; //ignorar el nombre, tiene_ref se está usando como sustituto a un "tiene_participante"
                            ListaE3NoP.Add(f);
                        }
                        tieneP.Close();
                    }
                }
            }
            //aviso para dia antes también de la conexion a internet en el aula correspondiente (si aplica)
            //aviso un dia antes de mobiliario y sonido
            #endregion
        }

        private void Generar_avisos()
        {
            int width = 276;
            int height = 28;
            int nueva_locacion = 77;
            // MessageBox.Show(ListaE2NoP.Count.ToString());
            for (int i = 0; i < ListaE2NoP.Count; i++)
            {
                #region generar

                TextBox tipof = new TextBox();
                tipof.Name = "txt" + i.ToString();
                tipof.Text = ListaE2NoP[i].tipo_formacion;
                tipof.ReadOnly = true;
                tipof.Multiline = true;
                tipof.Width = width;
                tipof.Height = height;
                tipof.Location = new Point(0, nueva_locacion);
                tipof.TextAlign = HorizontalAlignment.Center;
                tipof.BorderStyle = BorderStyle.Fixed3D;
                tipof.Font = new Font("Microsoft Sans Serif", 12, FontStyle.Bold);
                tipof.ForeColor = Color.White;

                TextBox nombref = new TextBox();
                nombref.Name = "txt" + i.ToString();
                nombref.Text = ListaE2NoP[i].nombre_formacion;
                nombref.ReadOnly = true;
                nombref.Multiline = true;
                nombref.Width = width;
                nombref.Height = height;
                nombref.Location = new Point(0, Convert.ToInt32(tipof.Location.Y) + height);
                nombref.TextAlign = HorizontalAlignment.Center;
                nombref.BorderStyle = BorderStyle.Fixed3D;
                nombref.Font = new Font("Microsoft Sans Serif", 12, FontStyle.Bold);
                nombref.ForeColor = Color.Black;

                TextBox accion = new TextBox();
                accion.Name = "txt" + i.ToString();
                accion.Text = "Mañana se reprogramará el curso. No hay participantes";
                accion.ReadOnly = true;
                accion.Multiline = true;
                accion.Width = width;
                accion.Height = 39;
                accion.Location = new Point(0, Convert.ToInt32(nombref.Location.Y) + height);
                accion.TextAlign = HorizontalAlignment.Center;
                accion.BorderStyle = BorderStyle.Fixed3D;
                accion.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Bold);
                accion.ForeColor = Color.Black;
                accion.BackColor = Color.FromArgb(207, 214, 221);

                nueva_locacion = Convert.ToInt32(accion.Location.Y) + 40;

                if (ListaE2NoP[i].tipo_formacion == "FEE")
                {
                    tipof.BackColor = Color.FromArgb(115, 156, 98);
                    nombref.BackColor = Color.FromArgb(194, 218, 158);
                }
                else if (ListaE2NoP[i].tipo_formacion == "Abierto")
                {
                    tipof.BackColor = Color.FromArgb(106, 150, 148);
                    nombref.BackColor = Color.FromArgb(163, 186, 185);
                }
                else if (ListaE2NoP[i].tipo_formacion == "INCES")
                {
                    tipof.BackColor = Color.FromArgb(156, 98, 103);
                    nombref.BackColor = Color.FromArgb(180, 148, 150);
                }
                else if (ListaE2NoP[i].tipo_formacion == "InCompany")
                {
                    tipof.BackColor = Color.FromArgb(129, 115, 151);
                    nombref.BackColor = Color.FromArgb(187, 174, 197);
                }
                panelActividades.Controls.Add(tipof);
                panelActividades.Controls.Add(nombref);
                panelActividades.Controls.Add(accion);
                #endregion
            }

            for (int i = 0; i < ListaE2SiP.Count; i++)
            {
                #region generar

                TextBox tipof = new TextBox();
                tipof.Name = "txt" + i.ToString();
                tipof.Text = ListaE2SiP[i].tipo_formacion;
                tipof.ReadOnly = true;
                tipof.Multiline = true;
                tipof.Width = width;
                tipof.Height = height;
                tipof.Location = new Point(0, nueva_locacion);
                tipof.TextAlign = HorizontalAlignment.Center;
                tipof.BorderStyle = BorderStyle.Fixed3D;
                tipof.Font = new Font("Microsoft Sans Serif", 12, FontStyle.Bold);
                tipof.ForeColor = Color.White;

                TextBox nombref = new TextBox();
                nombref.Name = "txt" + i.ToString();
                nombref.Text = ListaE2SiP[i].nombre_formacion;
                nombref.ReadOnly = true;
                nombref.Multiline = true;
                nombref.Width = width;
                nombref.Height = height;
                nombref.Location = new Point(0, Convert.ToInt32(tipof.Location.Y) + height);
                nombref.TextAlign = HorizontalAlignment.Center;
                nombref.BorderStyle = BorderStyle.Fixed3D;
                nombref.Font = new Font("Microsoft Sans Serif", 12, FontStyle.Bold);
                nombref.ForeColor = Color.Black;

                TextBox accion = new TextBox();
                accion.Name = "txt" + i.ToString();
                accion.Text = "     - Llamar facilitador (recordatorio).            - Falta logística.                                          -  Verificar acceso a internet.              - Verificar mobiliario y sonido     ";
                accion.ReadOnly = true;
                accion.Multiline = true;
                accion.Width = width;
                accion.Height = 71;
                accion.Location = new Point(0, Convert.ToInt32(nombref.Location.Y) + height);
                accion.TextAlign = HorizontalAlignment.Center;
                accion.BorderStyle = BorderStyle.Fixed3D;
                accion.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Bold);
                accion.ForeColor = Color.Black;
                accion.BackColor = Color.FromArgb(207, 214, 221);

                nueva_locacion = Convert.ToInt32(accion.Location.Y) + 72;

                if (ListaE2SiP[i].tipo_formacion == "FEE")
                {
                    tipof.BackColor = Color.FromArgb(115, 156, 98);
                    nombref.BackColor = Color.FromArgb(194, 218, 158);
                }
                else if (ListaE2SiP[i].tipo_formacion == "Abierto")
                {
                    tipof.BackColor = Color.FromArgb(106, 150, 148);
                    nombref.BackColor = Color.FromArgb(163, 186, 185);
                }
                else if (ListaE2SiP[i].tipo_formacion == "INCES")
                {
                    tipof.BackColor = Color.FromArgb(156, 98, 103);
                    nombref.BackColor = Color.FromArgb(180, 148, 150);
                }
                else if (ListaE2SiP[i].tipo_formacion == "InCompany")
                {
                    tipof.BackColor = Color.FromArgb(129, 115, 151);
                    nombref.BackColor = Color.FromArgb(187, 174, 197);
                }
                panelActividades.Controls.Add(tipof);
                panelActividades.Controls.Add(nombref);
                panelActividades.Controls.Add(accion);
                #endregion
            }

            for (int i=0; i<ListaE2HOY.Count; i++)
            {
                //aqui hay participantes y no posee logistica
                #region generar

                TextBox tipof = new TextBox();
                tipof.Name = "txt" + i.ToString();
                tipof.Text = ListaE2HOY[i].tipo_formacion;
                tipof.ReadOnly = true;
                tipof.Multiline = true;
                tipof.Width = width;
                tipof.Height = height;
                tipof.Location = new Point(0, nueva_locacion);
                tipof.TextAlign = HorizontalAlignment.Center;
                tipof.BorderStyle = BorderStyle.Fixed3D;
                tipof.Font = new Font("Microsoft Sans Serif", 12, FontStyle.Bold);
                tipof.ForeColor = Color.White;

                TextBox nombref = new TextBox();
                nombref.Name = "txt" + i.ToString();
                nombref.Text = ListaE2HOY[i].nombre_formacion;
                nombref.ReadOnly = true;
                nombref.Multiline = true;
                nombref.Width = width;
                nombref.Height = height;
                nombref.Location = new Point(0, Convert.ToInt32(tipof.Location.Y) + height);
                nombref.TextAlign = HorizontalAlignment.Center;
                nombref.BorderStyle = BorderStyle.Fixed3D;
                nombref.Font = new Font("Microsoft Sans Serif", 12, FontStyle.Bold);
                nombref.ForeColor = Color.Black;

                TextBox accion = new TextBox();
                accion.Name = "txt" + i.ToString();
                accion.Text = "     Día de formación. De no proporcionar la logísitca HOY, se reprogramará el curso.";
                accion.ReadOnly = true;
                accion.Multiline = true;
                accion.Width = width;
                accion.Height = 57;
                accion.Location = new Point(0, Convert.ToInt32(nombref.Location.Y) + height);
                accion.TextAlign = HorizontalAlignment.Center;
                accion.BorderStyle = BorderStyle.Fixed3D;
                accion.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Bold);
                accion.ForeColor = Color.Black;
                accion.BackColor = Color.FromArgb(207, 214, 221);

                nueva_locacion = Convert.ToInt32(accion.Location.Y) + 58;

                if (ListaE2HOY[i].tipo_formacion == "FEE")
                {
                    tipof.BackColor = Color.FromArgb(115, 156, 98);
                    nombref.BackColor = Color.FromArgb(194, 218, 158);
                }
                else if (ListaE2HOY[i].tipo_formacion == "Abierto")
                {
                    tipof.BackColor = Color.FromArgb(106, 150, 148);
                    nombref.BackColor = Color.FromArgb(163, 186, 185);
                }
                else if (ListaE2HOY[i].tipo_formacion == "INCES")
                {
                    tipof.BackColor = Color.FromArgb(156, 98, 103);
                    nombref.BackColor = Color.FromArgb(180, 148, 150);
                }
                else if (ListaE2HOY[i].tipo_formacion == "InCompany")
                {
                    tipof.BackColor = Color.FromArgb(129, 115, 151);
                    nombref.BackColor = Color.FromArgb(187, 174, 197);
                }
                panelActividades.Controls.Add(tipof);
                panelActividades.Controls.Add(nombref);
                panelActividades.Controls.Add(accion);
                #endregion
            }

            for(int i=0; i < ListaE3NoP.Count; i++)
            {
                #region generar

                TextBox tipof = new TextBox();
                tipof.Name = "txt" + i.ToString();
                tipof.Text = ListaE3NoP[i].tipo_formacion;
                tipof.ReadOnly = true;
                tipof.Multiline = true;
                tipof.Width = width;
                tipof.Height = height;
                tipof.Location = new Point(0, nueva_locacion);
                tipof.TextAlign = HorizontalAlignment.Center;
                tipof.BorderStyle = BorderStyle.Fixed3D;
                tipof.Font = new Font("Microsoft Sans Serif", 12, FontStyle.Bold);
                tipof.ForeColor = Color.White;

                TextBox nombref = new TextBox();
                nombref.Name = "txt" + i.ToString();
                nombref.Text = ListaE3NoP[i].nombre_formacion;
                nombref.ReadOnly = true;
                nombref.Multiline = true;
                nombref.Width = width;
                nombref.Height = height;
                nombref.Location = new Point(0, Convert.ToInt32(tipof.Location.Y) + height);
                nombref.TextAlign = HorizontalAlignment.Center;
                nombref.BorderStyle = BorderStyle.Fixed3D;
                nombref.Font = new Font("Microsoft Sans Serif", 12, FontStyle.Bold);
                nombref.ForeColor = Color.Black;

                TextBox accion = new TextBox();
                accion.Name = "txt" + i.ToString();
                accion.Text = "Mañana se reprogramará el curso. No hay participantes";
                accion.ReadOnly = true;
                accion.Multiline = true;
                accion.Width = width;
                accion.Height = 39;
                accion.Location = new Point(0, Convert.ToInt32(nombref.Location.Y) + height);
                accion.TextAlign = HorizontalAlignment.Center;
                accion.BorderStyle = BorderStyle.Fixed3D;
                accion.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Bold);
                accion.ForeColor = Color.Black;
                accion.BackColor = Color.FromArgb(207, 214, 221);

                nueva_locacion = Convert.ToInt32(accion.Location.Y) + 40;

                if (ListaE3NoP[i].tipo_formacion == "FEE")
                {
                    tipof.BackColor = Color.FromArgb(115, 156, 98);
                    nombref.BackColor = Color.FromArgb(194, 218, 158);
                }
                else if (ListaE3NoP[i].tipo_formacion == "Abierto")
                {
                    tipof.BackColor = Color.FromArgb(106, 150, 148);
                    nombref.BackColor = Color.FromArgb(163, 186, 185);
                }
                else if (ListaE3NoP[i].tipo_formacion == "INCES")
                {
                    tipof.BackColor = Color.FromArgb(156, 98, 103);
                    nombref.BackColor = Color.FromArgb(180, 148, 150);
                }
                else if (ListaE3NoP[i].tipo_formacion == "InCompany")
                {
                    tipof.BackColor = Color.FromArgb(129, 115, 151);
                    nombref.BackColor = Color.FromArgb(187, 174, 197);
                }
                panelActividades.Controls.Add(tipof);
                panelActividades.Controls.Add(nombref);
                panelActividades.Controls.Add(accion);
                #endregion
            }

            for(int i=0; i < ListaE3SiP.Count; i++)
            {
                #region generar

                TextBox tipof = new TextBox();
                tipof.Name = "txt" + i.ToString();
                tipof.Text = ListaE3SiP[i].tipo_formacion;
                tipof.ReadOnly = true;
                tipof.Multiline = true;
                tipof.Width = width;
                tipof.Height = height;
                tipof.Location = new Point(0, nueva_locacion);
                tipof.TextAlign = HorizontalAlignment.Center;
                tipof.BorderStyle = BorderStyle.Fixed3D;
                tipof.Font = new Font("Microsoft Sans Serif", 12, FontStyle.Bold);
                tipof.ForeColor = Color.White;

                TextBox nombref = new TextBox();
                nombref.Name = "txt" + i.ToString();
                nombref.Text = ListaE3SiP[i].nombre_formacion;
                nombref.ReadOnly = true;
                nombref.Multiline = true;
                nombref.Width = width;
                nombref.Height = height;
                nombref.Location = new Point(0, Convert.ToInt32(tipof.Location.Y) + height);
                nombref.TextAlign = HorizontalAlignment.Center;
                nombref.BorderStyle = BorderStyle.Fixed3D;
                nombref.Font = new Font("Microsoft Sans Serif", 12, FontStyle.Bold);
                nombref.ForeColor = Color.Black;

                TextBox accion = new TextBox();
                accion.Name = "txt" + i.ToString();
                accion.Text = "       - Llamar facilitador (recordatorio).            -  Verificar acceso a internet.               - Verificar mobiliario y sonido     ";
                accion.ReadOnly = true;
                accion.Multiline = true;
                accion.Width = width;
                accion.Height = 54;
                accion.Location = new Point(0, Convert.ToInt32(nombref.Location.Y) + height);
                accion.TextAlign = HorizontalAlignment.Center;
                accion.BorderStyle = BorderStyle.Fixed3D;
                accion.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Bold);
                accion.ForeColor = Color.Black;
                accion.BackColor = Color.FromArgb(207, 214, 221);

                nueva_locacion = Convert.ToInt32(accion.Location.Y) + 55;

                if (ListaE3SiP[i].tipo_formacion == "FEE")
                {
                    tipof.BackColor = Color.FromArgb(115, 156, 98);
                    nombref.BackColor = Color.FromArgb(194, 218, 158);
                }
                else if (ListaE3SiP[i].tipo_formacion == "Abierto")
                {
                    tipof.BackColor = Color.FromArgb(106, 150, 148);
                    nombref.BackColor = Color.FromArgb(163, 186, 185);
                }
                else if (ListaE3SiP[i].tipo_formacion == "INCES")
                {
                    tipof.BackColor = Color.FromArgb(156, 98, 103);
                    nombref.BackColor = Color.FromArgb(180, 148, 150);
                }
                else if (ListaE3SiP[i].tipo_formacion == "InCompany")
                {
                    tipof.BackColor = Color.FromArgb(129, 115, 151);
                    nombref.BackColor = Color.FromArgb(187, 174, 197);
                }
                panelActividades.Controls.Add(tipof);
                panelActividades.Controls.Add(nombref);
                panelActividades.Controls.Add(accion);
                #endregion
            }
        }
        #region cargar datos
        private void CargarDatosFEE()
        {
            int encurso = 0;
            MySqlDataReader ec = Conexion.ConsultarBD("select * from cursos where tipo_curso='FEE' and estatus_curso='En curso'");
            while (ec.Read())
            {
                encurso += 1;                
            }
            ec.Close();
            int rep = 0;
            MySqlDataReader r = Conexion.ConsultarBD("select * from cursos where tipo_curso='FEE' and estatus_curso='Reprogramado'");
            while (r.Read())
            {
                rep += 1;
            }
            r.Close();
            int fin = 0;
            MySqlDataReader f = Conexion.ConsultarBD("select * from cursos where tipo_curso='FEE' and estatus_curso='Finalizado'");
            while (f.Read())
            {
                fin += 1;
            }
            f.Close();
            int sus = 0;
            MySqlDataReader s = Conexion.ConsultarBD("select * from cursos where tipo_curso='FEE' and estatus_curso='Suspendido'");
            while (s.Read())
            {
                sus += 1;
            }
            s.Close();
            int total = 0;
            MySqlDataReader leer = Conexion.ConsultarBD("select * from cursos where tipo_curso='FEE'");
            while (leer.Read())
            {
                total += 1;                
            }
            leer.Close();
            //cargar los labels:
            labelEnCurso.Text = encurso.ToString();
            labelReprogramado.Text = rep.ToString();
            labelFinalizado.Text = fin.ToString();
            labelSuspendido.Text = sus.ToString();
            labelTotalRealizado.Text = total.ToString();
        }
        private void CargarDatosABIERTO()
        {
            int encurso = 0;
            MySqlDataReader ec = Conexion.ConsultarBD("select * from cursos where tipo_curso='Abierto' and estatus_curso='En curso'");
            while (ec.Read())
            {
                encurso += 1;
            }
            ec.Close();
            int rep = 0;
            MySqlDataReader r = Conexion.ConsultarBD("select * from cursos where tipo_curso='Abierto' and estatus_curso='Reprogramado'");
            while (r.Read())
            {
                rep += 1;
            }
            r.Close();
            int fin = 0;
            MySqlDataReader f = Conexion.ConsultarBD("select * from cursos where tipo_curso='Abierto' and estatus_curso='Finalizado'");
            while (f.Read())
            {
                fin += 1;
            }
            f.Close();
            int sus = 0;
            MySqlDataReader s = Conexion.ConsultarBD("select * from cursos where tipo_curso='Abierto' and estatus_curso='Suspendido'");
            while (s.Read())
            {
                sus += 1;
            }
            s.Close();
            int total = 0;
            MySqlDataReader leer = Conexion.ConsultarBD("select * from cursos where tipo_curso='Abierto'");
            while (leer.Read())
            {
                total += 1;
            }
            leer.Close();
            //cargar los labels:
            labelEnCurso.Text = encurso.ToString();
            labelReprogramado.Text = rep.ToString();
            labelFinalizado.Text = fin.ToString();
            labelSuspendido.Text = sus.ToString();
            labelTotalRealizado.Text = total.ToString();
        }
        private void CargarDatosINCOM()
        {
            int encurso = 0;
            MySqlDataReader ec = Conexion.ConsultarBD("select * from cursos where tipo_curso='InCompany' and estatus_curso='En curso'");
            while (ec.Read())
            {
                encurso += 1;
            }
            ec.Close();
            int rep = 0;
            MySqlDataReader r = Conexion.ConsultarBD("select * from cursos where tipo_curso='InCompany' and estatus_curso='Reprogramado'");
            while (r.Read())
            {
                rep += 1;
            }
            r.Close();
            int fin = 0;
            MySqlDataReader f = Conexion.ConsultarBD("select * from cursos where tipo_curso='InCompany' and estatus_curso='Finalizado'");
            while (f.Read())
            {
                fin += 1;
            }
            f.Close();
            int sus = 0;
            MySqlDataReader s = Conexion.ConsultarBD("select * from cursos where tipo_curso='InCompany' and estatus_curso='Suspendido'");
            while (s.Read())
            {
                sus += 1;
            }
            s.Close();
            int total = 0;
            MySqlDataReader leer = Conexion.ConsultarBD("select * from cursos where tipo_curso='InCompany'");
            while (leer.Read())
            {
                total += 1;
            }
            leer.Close();
            //cargar los labels:
            labelEnCurso.Text = encurso.ToString();
            labelReprogramado.Text = rep.ToString();
            labelFinalizado.Text = fin.ToString();
            labelSuspendido.Text = sus.ToString();
            labelTotalRealizado.Text = total.ToString();
        }
        private void CargarDatosINCES()
        {
            int encurso = 0;
            MySqlDataReader ec = Conexion.ConsultarBD("select * from cursos where tipo_curso='INCES' and estatus_curso='En curso'");
            while (ec.Read())
            {
                encurso += 1;
            }
            ec.Close();
            int rep = 0;
            MySqlDataReader r = Conexion.ConsultarBD("select * from cursos where tipo_curso='INCES' and estatus_curso='Reprogramado'");
            while (r.Read())
            {
                rep += 1;
            }
            r.Close();
            int fin = 0;
            MySqlDataReader f = Conexion.ConsultarBD("select * from cursos where tipo_curso='INCES' and estatus_curso='Finalizado'");
            while (f.Read())
            {
                fin += 1;
            }
            f.Close();
            int sus = 0;
            MySqlDataReader s = Conexion.ConsultarBD("select * from cursos where tipo_curso='INCES' and estatus_curso='Suspendido'");
            while (s.Read())
            {
                sus += 1;
            }
            s.Close();
            int total = 0;
            MySqlDataReader leer = Conexion.ConsultarBD("select * from cursos where tipo_curso='INCES'");
            while (leer.Read())
            {
                total += 1;
            }
            leer.Close();
            //cargar los labels:
            labelEnCurso.Text = encurso.ToString();
            labelReprogramado.Text = rep.ToString();
            labelFinalizado.Text = fin.ToString();
            labelSuspendido.Text = sus.ToString();
            labelTotalRealizado.Text = total.ToString();
        }
        #endregion
        #region eventos paneles
        private void pnlLeyenda_MouseClick(object sender, MouseEventArgs e)
        {
            if (pnlLeyenda.Height == 30)
            {
                pnlLeyenda.Height = 104;
            }else
            {
                pnlLeyenda.Height = 30;
            }
        }

        private void labelLeyenda_MouseClick(object sender, MouseEventArgs e)
        {
            if (pnlLeyenda.Height == 30)
            {
                pnlLeyenda.Height = 104;
            }
            else
            {
                pnlLeyenda.Height = 30;
            }
        }


        private void labelINCES_MouseClick(object sender, MouseEventArgs e)
        {
            if (pnlInformacion.Visible == false)
            {
                CargarDatosINCES();
                activo = "INCES";
                pnlInformacion.Visible = true;
            }
            else if (pnlInformacion.Visible == true && activo != "INCES")
            {
                CargarDatosINCES();
                activo = "INCES";
                pnlInformacion.Visible = true;
            }
            else
            {
                activo = "";
                pnlInformacion.Visible = false;
            }
        }

        private void labelINCOM_MouseClick(object sender, MouseEventArgs e)
        {
             if (pnlInformacion.Visible == false)
            {
                CargarDatosINCOM();
                activo = "INCOM";
                pnlInformacion.Visible = true;
            }
             else if (pnlInformacion.Visible == true && activo != "INCOM")
            {
                CargarDatosINCOM();
                activo = "INCOM";
                pnlInformacion.Visible = true;
            }
            else
            {
                activo = "";
                pnlInformacion.Visible = false;
            }
        }

        private void panelFEE_Click(object sender, EventArgs e)
        {
            if (pnlInformacion.Visible == false)
            {
                CargarDatosFEE();
                activo = "FEE";
                pnlInformacion.Visible = true;
            }
            else if (pnlInformacion.Visible == true && activo != "FEE")
            {
                CargarDatosFEE();
                activo = "FEE";
                pnlInformacion.Visible = true;
            }
            else
            {
                activo = "";
                pnlInformacion.Visible = false;
            }
        }

        private void panelABIERTO_Click(object sender, EventArgs e)
        {
            if (pnlInformacion.Visible == false)
            {
                CargarDatosABIERTO();
                activo = "Abierto";
                pnlInformacion.Visible = true;
            }
            else if (pnlInformacion.Visible == true && activo != "Abierto")
            {
                CargarDatosABIERTO();
                activo = "Abierto";
                pnlInformacion.Visible = true;
            }
            else
            {
                activo = "";
                pnlInformacion.Visible = false;
            }
        }
        #endregion
    }
}
