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
    public partial class RPT_HORAS_DEDICADAS : Form
    {
        public List<R_Formacion_DatosGenerales> InfoGeneral = new List<R_Formacion_DatosGenerales>();
        public List<R_EtapaFormacion> EtapasF = new List<R_EtapaFormacion>();
        public RPT_HORAS_DEDICADAS()
        {
            InitializeComponent();
        }

        private void RPT_HORAS_DEDICADAS_Load(object sender, EventArgs e)
        {
            this.reportViewer1.RefreshReport();

            reportViewer1.LocalReport.DataSources.Clear();

            //Establezcamos la lista como Datasource del informe
            //
            reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("dsGeneral", InfoGeneral));
            reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("dsEtapas", EtapasF));
            //
            reportViewer1.RefreshReport();
        }
    }
}
