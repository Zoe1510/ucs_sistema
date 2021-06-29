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
        TimeSpan dur, dur2, dur3;

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public Vista_Formacion()
        {
            InitializeComponent();
            int id_user1 = Cursos.id_user13;
            MySqlDataReader consulta = Conexion.ConsultarBD("SELECT duracionE1, duracionE2, duracionE3 FROM cursos WHERE id_cursos='"+ Cursos.id_curso13+"'");
            if (consulta.Read())
            {
                duracion=consulta["duracionE1"].ToString();
                duracion2= consulta["duracionE2"].ToString();
                duracion3 =consulta["duracionE3"].ToString();
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
                    if (Cursos.tipo_formacion13 == "Abierto" || Cursos.tipo_formacion13 == "InCompany" || Cursos.tipo_formacion13=="FEE")
                    {
                        if((Cursos.duracion_formacion13 == "4") || (Cursos.duracion_formacion13 == "8" && Cursos.bloque_curso13 == "1"))
                        {
                            txtFechauno.Text = "Sin asignar";
                            txtFechados.Text = "No aplica";
                        }else
                        {
                            txtFechauno.Text = "Sin asignar";
                            txtFechados.Text = "Sin asignar";
                        }
                    }else
                    {
                        txtFechauno.Text = "Sin asignar";
                        txtFechados.Text = "Sin asignar";
                    }
                    txtEstadoB.Text = "En proceso";
                    txtEstadoI.Text = "Sin iniciar";
                    txtEstadoA.Text = "Sin iniciar";
                    dur = TimeSpan.Parse(duracion);
                    txtTiempoB.Text = dur.RelativeTimes();
                    txtTiempoI.Text = "0 Horas";
                    txtTiempoA.Text = "0 Horas";

                }
                else if (Cursos.etapa_formacion13 == 2)
                {
                    txtFechados.Text = Cursos.fecha_dos13;
                    txtFechauno.Text = Cursos.fecha_uno13;
                    txtEstadoB.Text = "Finalizada";
                    txtEstadoI.Text = "En proceso";
                    dur = TimeSpan.Parse(duracion);
                    dur2 = TimeSpan.Parse(duracion2);
                    txtEstadoA.Text = "Sin iniciar";
                    txtTiempoB.Text = dur.RelativeTimes();
                    txtTiempoI.Text = dur2.RelativeTimes();
                    txtTiempoA.Text = "0 Horas";
                }
                else if (Cursos.etapa_formacion13 == 3)
                {
                    txtFechados.Text = Cursos.fecha_dos13;
                    txtFechauno.Text = Cursos.fecha_uno13;
                    txtEstadoB.Text = "Finalizada";
                    txtEstadoI.Text = "Finalizada";
                    txtEstadoA.Text = "En proceso";
                    dur = TimeSpan.Parse(duracion);
                    dur2 = TimeSpan.Parse(duracion2);
                    dur3 = TimeSpan.Parse(duracion3);
                    txtTiempoB.Text = dur.RelativeTimes();
                    txtTiempoI.Text = dur2.RelativeTimes();
                    txtTiempoA.Text = dur3.RelativeTimes();
                }
            }else if (Cursos.estatus_formacion13 == "Suspendido" || Cursos.estatus_formacion13 == "Finalizado")
            {
                txtFechados.Text = Cursos.fecha_dos13;
                txtFechauno.Text = Cursos.fecha_uno13;
                txtEstadoB.Text = "Finalizada";
                txtEstadoI.Text = "Finalizada";
                txtEstadoA.Text = "Finalizada";
                dur = TimeSpan.Parse(duracion);
                dur2 = TimeSpan.Parse(duracion2);
                dur3 = TimeSpan.Parse(duracion3);
                txtTiempoB.Text = dur.RelativeTimes();
                txtTiempoI.Text = dur2.RelativeTimes();
                txtTiempoA.Text = dur3.RelativeTimes();
            }

           
        }
    }
}
