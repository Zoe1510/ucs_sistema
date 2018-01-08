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
using System.Windows.Forms;


namespace UCS_NODO_FGC
{
    public partial class Vista_Formacion : Form
    {
        TimeSpan Duracion;
        public Vista_Formacion()
        {
            InitializeComponent();
            //int id_user1 = Cursos.id_user13;
            //MySqlDataReader consulta = Conexion.ConsultarBD("select fecha_creacion, fecha_mod_curso from cursos cur inner join user_gestionan_cursos ugc on cur.id_cursos = ugc.cursos_id_cursos where cur.id_usuario1 ='" + id_user1 + "' ");
            //if (consulta.Read())
            //{
            //    DateTime fecha_Mod = Convert.ToDateTime(consulta["fecha_mod_curso"]);
            //    DateTime fecha_creacion = Convert.ToDateTime(consulta["fecha_creacion"]);
            //    Duracion = new TimeSpan(fecha_Mod.Ticks - fecha_creacion.Ticks);
            //}
            

           


        }

        private void Vista_Formacion_Load(object sender, EventArgs e)
        {
            txtNombre.Text = Cursos.nombre_formacion13;
            txtTipo.Text = Cursos.tipo_formacion13;
            txtEstatus.Text = Cursos.estatus_formacion13;
            txtSolicitud.Text = Cursos.solicitud_formacion13;
            txtUsuario.Text = Cursos.nombreCreador_formacion13;
            if(Cursos.estatus_formacion13 == "En curso" || Cursos.estatus_formacion13 == "Reprogramado")
            {
                if (Cursos.etapa_formacion13 == 1)
                {
                    txtEstadoB.Text = "En proceso";
                    txtEstadoI.Text = "Sin iniciar";
                    txtEstadoA.Text = "Sin iniciar";

                }
                else if (Cursos.etapa_formacion13 == 2)
                {
                    txtEstadoB.Text = "Finalizada";
                    txtEstadoI.Text = "En proceso";
                    txtEstadoA.Text = "Sin iniciar";
                }
                else if (Cursos.etapa_formacion13 == 3)
                {
                    txtEstadoB.Text = "Finalizada";
                    txtEstadoI.Text = "Finalizada";
                    txtEstadoA.Text = "En proceso";
                }
            }else if (Cursos.estatus_formacion13 == "Suspendido" || Cursos.estatus_formacion13 == "Finalizado")
            {
                txtEstadoB.Text = "Finalizada";
                txtEstadoI.Text = "Finalizada";
                txtEstadoA.Text = "Finalizada";
            }

            //txtTiempoB.Text = " "+Duracion.ToString()+" horas";
            txtTiempoI.Text = "0 horas";
            txtTiempoA.Text = "0 horas";
        }
    }
}
