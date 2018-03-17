using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UCS_NODO_FGC.Clases;
using Microsoft.Reporting.WinForms;

namespace UCS_NODO_FGC
{
    public partial class RPT_TIEMPO_TIPO : Form
    {
        public List<R_Formacion_DatosGenerales> info = new List<R_Formacion_DatosGenerales>();
        public RPT_TIEMPO_TIPO()
        {
            InitializeComponent();
        }

        private void RPT_TIEMPO_TIPO_Load(object sender, EventArgs e)
        {

            this.reportViewer1.RefreshReport();
            reportViewer1.LocalReport.DataSources.Clear();

            //Establezcamos la lista como Datasource del informe
            //
            reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("dsTodo", info));
            
            //
            reportViewer1.RefreshReport();
        }
    }
}
