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
using Microsoft.Reporting.WinForms;

namespace UCS_NODO_FGC
{
    public partial class Lista_participantes : Form
    {
        public List<R_Formacion_lista> form_encabezado = new List<R_Formacion_lista>();
        public List<R_Participantes_postulados> pp_detalle = new List<R_Participantes_postulados>();
        public Lista_participantes()
        {
            InitializeComponent();
        }
      
        private void Lista_participantes_Load(object sender, EventArgs e)
        {

            this.reportViewer1.RefreshReport();
            reportViewer1.LocalReport.DataSources.Clear();
            
            //Establezcamos la lista como Datasource del informe
            //
            reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("dsEncabezado", form_encabezado));
            reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("Detalle", pp_detalle));
            //
            reportViewer1.RefreshReport();
        }
    }
}
