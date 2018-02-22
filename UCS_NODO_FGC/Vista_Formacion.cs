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
        string duracion, duracion2, duracion3;
        public Vista_Formacion()
        {
            InitializeComponent();
            int id_user1 = Cursos.id_user13;
            MySqlDataReader consulta = Conexion.ConsultarBD("SELECT duracionE1, duracionE2, duracionE3 FROM cursos WHERE id_cursos='"+ Cursos.id_curso13+"'");
            if (consulta.Read())
            {
                duracion=Convert.ToString(consulta["duracionE1"]);
                duracion2=Convert.ToString(consulta["duracionE2"]);
                duracion3 = Convert.ToString(consulta["duracionE3"]);
            }
            
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
                    txtTiempoB.Text = " " + duracion + " min.";
                    txtTiempoI.Text = "0 min.";
                    txtTiempoA.Text = "0 min.";

                }
                else if (Cursos.etapa_formacion13 == 2)
                {
                    txtEstadoB.Text = "Finalizada";
                    txtEstadoI.Text = "En proceso";
                    txtEstadoA.Text = "Sin iniciar";
                    txtTiempoB.Text = " " + duracion + " min.";
                    txtTiempoI.Text = " " + duracion2 + " min.";
                    txtTiempoA.Text = "0 min.";
                }
                else if (Cursos.etapa_formacion13 == 3)
                {
                    txtEstadoB.Text = "Finalizada";
                    txtEstadoI.Text = "Finalizada";
                    txtEstadoA.Text = "En proceso";
                    txtTiempoB.Text = " " + duracion + " min.";
                    txtTiempoI.Text = " " + duracion2 + " min.";
                    txtTiempoA.Text = " " + duracion3 + " min.";
                }
            }else if (Cursos.estatus_formacion13 == "Suspendido" || Cursos.estatus_formacion13 == "Finalizado")
            {
                txtEstadoB.Text = "Finalizada";
                txtEstadoI.Text = "Finalizada";
                txtEstadoA.Text = "Finalizada";
                txtTiempoB.Text = " " + duracion + " min.";
                txtTiempoI.Text = " " + duracion2 + " min.";
                txtTiempoA.Text = " " + duracion3 + " min.";
            }

        
        }
    }
}
