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
    public partial class Hacer_reportes : Form
    {
        Formaciones f = new Formaciones();
        public Hacer_reportes()
        {
            InitializeComponent();
        }
        bool reporte2;
        private void btnReporte1_Click(object sender, EventArgs e)
        {
            gpbItems.Enabled = true;
            cmbxTipoFormacion.SelectedIndex = -1;
            cmbxTipoFormacion.Enabled = true;

            cmbxDuracionFormacion.SelectedIndex = -1;
            cmbxEstatusFormación.SelectedIndex = -1;
            cmbxNombreFormacion.SelectedIndex = -1;
            cmbxPeriodoTiempo.SelectedIndex = -1;

            cmbxNombreFormacion.Enabled = false;
            cmbxDuracionFormacion.Enabled = false;
            cmbxEstatusFormación.Enabled = false;
            cmbxPeriodoTiempo.Enabled = false;

            lblObli1.Visible = true;
            lblObli2.Visible = true;
            lblObli3.Visible = true;
            lblObli4.Visible = true;

            lblCamposobli.Location = new Point(492, 579);
            lblCamposobli.Text = "Campos obligatorios (*)";

            lblCamposobli.Visible = true;
        }

        private void btnReporte2_Click(object sender, EventArgs e)
        {
            gpbItems.Enabled = true;
            cmbxTipoFormacion.SelectedIndex = -1;
            cmbxTipoFormacion.Enabled = true;

            cmbxDuracionFormacion.SelectedIndex = -1;
            cmbxEstatusFormación.SelectedIndex = -1;
            cmbxNombreFormacion.SelectedIndex = -1;
            cmbxPeriodoTiempo.SelectedIndex = -1;

            cmbxNombreFormacion.Enabled = false;
            cmbxDuracionFormacion.Enabled = false;
            cmbxEstatusFormación.Enabled = false;
            cmbxPeriodoTiempo.Enabled = false;

            reporte2 = true;
            lblObli1.Visible = false;
            lblObli2.Visible = false;
            lblObli3.Visible = false;
            lblObli4.Visible = false;
            lblCamposobli.Location = new Point(464, 579);
            lblCamposobli.Text = "Todos los campos son obligatorios.";
            lblCamposobli.Visible = true;
        }

        private void Hacer_reportes_Load(object sender, EventArgs e)
        {
            this.Location = new Point(-5, 0);
        }

        private void cmbxTipoFormacion_SelectionChangeCommitted(object sender, EventArgs e)
        {
            //si selecciona algo de aqui, se activa el combobox de duración y se llena en base al tipo
            string tipo =Convert.ToString(cmbxTipoFormacion.SelectedIndex);
            switch (tipo)
            {
                case "0":
                    tipo = "Abierto";
                    cmbxDuracionFormacion.Items.Clear();
                    cmbxDuracionFormacion.Items.Add("4 horas");
                    cmbxDuracionFormacion.Items.Add("8 horas y 1 bloque");
                    cmbxDuracionFormacion.Items.Add("8 horas y 2 bloques");
                    cmbxDuracionFormacion.Items.Add("16 horas");
                    break;
                case "1":
                    tipo = "FEE";
                    cmbxDuracionFormacion.Items.Clear();
                    cmbxDuracionFormacion.Items.Add("8 horas");                    
                    break;
                case "2":
                    tipo = "INCES";
                    cmbxDuracionFormacion.Items.Clear();
                    cmbxDuracionFormacion.Items.Add("16 horas");                    
                    break;
                case "3":
                    tipo = "InCompany";
                    cmbxDuracionFormacion.Items.Clear();
                    cmbxDuracionFormacion.Items.Add("4 horas");
                    cmbxDuracionFormacion.Items.Add("8 horas y 1 bloque");
                    cmbxDuracionFormacion.Items.Add("8 horas y 2 bloques");
                    cmbxDuracionFormacion.Items.Add("16 horas");
                    break;
                    //falta recoger el tipo en una variable global para hacer el reporte
                
            }
            f.tipo_formacion = tipo;
            cmbxDuracionFormacion.Enabled = true;
            cmbxDuracionFormacion.Focus();
            //add evento validating tipo, duracion, todos
        }

        private void cmbxDuracionFormacion_SelectionChangeCommitted(object sender, EventArgs e)
        {
            //si se selecciona algo de aquí, se habilita el siguiente combo (estatus)
           
            string duracion = Convert.ToString(cmbxDuracionFormacion.SelectedIndex);
            //falta: recoger lo que se seleccione aqui en una variable global para hacer el reporte
            if(f.tipo_formacion == "Abierto" || f.tipo_formacion == "InCompany")
            {
                switch (duracion)
                {
                    case "0":
                        f.duracion = "4";
                        f.bloque_curso = "1";
                        break;
                    case "1":
                        f.duracion = "8";
                        f.bloque_curso = "1";
                        break;
                    case "2":
                        f.duracion = "8";
                        f.bloque_curso = "2";
                        break;
                    case "3":
                        f.duracion = "16";
                        f.bloque_curso = "2";
                        break;
                }

            }else if(f.tipo_formacion == "FEE")
            {
                switch (duracion)
                {
                    case "0":
                        f.duracion = "8";
                        f.bloque_curso = "1";
                        break;
                    
                }

            }
            else if(f.tipo_formacion == "INCES")
            {
                switch (duracion)
                {
                    case "0":
                        f.duracion = "16";
                        f.bloque_curso = "2";
                        break;

                }
            }
            cmbxEstatusFormación.Enabled = true;
            cmbxEstatusFormación.Focus();
        }

        private void cmbxEstatusFormación_SelectionChangeCommitted(object sender, EventArgs e)
        {
            //si se selecciona algo aqui, se habilita el otro cmbx y se cargan los datos dependiendo
            //de la informacion contenida en todos los anteriores a el
            string estatus = Convert.ToString(cmbxEstatusFormación.SelectedIndex);
            switch (estatus)
            {
                case "0":
                    estatus = "En curso";
                    cmbxNombreFormacion.Items.Clear();
                    
                    break;
                case "1":
                    estatus = "Reprogramado";
                    cmbxNombreFormacion.Items.Clear();

                    break;
                case "2":
                    estatus = "Suspendido";
                    cmbxNombreFormacion.Items.Clear();

                    break;
                case "3":
                    estatus = "Finalizado";
                    cmbxNombreFormacion.Items.Clear();

                    break;
                    //falta recoger el estatus en una variable global para hacer el reporte
                    //y para llenar el cmbx nombresFormacion

            }
            f.estatus = estatus;
            cmbxNombreFormacion.Enabled = false;
            //cmbxNombreFormacion.Items.Add("No existe formación con esas especificaciones");
            
        }

        private void cmbxNombreFormacion_SelectionChangeCommitted(object sender, EventArgs e)
        {
            //aqui se deberá recoger el id del curso seleccionado para trabajarlo de manera global en el reporte
            //se verifica si viene referenciado desde el reporte2 para habilitar el periodo.
            if (cmbxNombreFormacion.SelectedItem.ToString() != "No existe formación con esas especificaciones")
            {
                btnCrearReporte.Enabled = true;
                if (reporte2 == true)
                {
                    cmbxPeriodoTiempo.Enabled = true;
                }
            }
            else
            {
                btnCrearReporte.Enabled = false;
            }
           
        }

        private void cmbxPeriodoTiempo_SelectionChangeCommitted(object sender, EventArgs e)
        {
            //se deberá guardar lo que seleccione el usuario para la creacion del reporte2
        }
    }
}
