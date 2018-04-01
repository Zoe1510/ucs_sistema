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
        public Notificaciones()
        {
            InitializeComponent();
        }

        private void Notificaciones_Load(object sender, EventArgs e)
        {
            this.Location = new Point(-5, 0);

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
        }

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
    }
}
