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
using Microsoft.Reporting.WinForms;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;

namespace UCS_NODO_FGC
{
    public partial class RPT_TIEMPO_TIPO : Form
    {
        public List<R_TiempoTipoFormacion> info = new List<R_TiempoTipoFormacion>();
        public RPT_TIEMPO_TIPO()
        {
            InitializeComponent();
        }

        private void RPT_TIEMPO_TIPO_Load(object sender, EventArgs e)
        {

            this.reportViewer1.RefreshReport();
            reportViewer1.LocalReport.DataSources.Clear();

            string fecha = DateTime.Today.ToString("dd-MM-yyyy");

            string nombre_reporte = "tiempo_tipo_" + fecha + ".pdf";
            string ruta = @"C:\Users\ZM\Documents\Last_repo\ucs_sistema\UCS_NODO_FGC\Archivos\Reportes_emitidos";  //cambia cuando se instale

            string destino = Path.Combine(ruta, nombre_reporte);
            reportViewer1.LocalReport.DisplayName = "Tiempo_tipo";
            //Establezcamos la lista como Datasource del informe
            //
            reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("dsFormacion", info));

            File.WriteAllBytes(destino, reportViewer1.LocalReport.Render("PDF"));
            //
            MySqlDataReader insert = Conexion.ConsultarBD("insert into reportes (nombre_reporte, fecha_creacion,id_creador_usuario, ruta_reporte) values ('" + nombre_reporte + "', '" + DateTime.Today + "', '" + Usuario_logeado.id_usuario + "', '" + destino + "')");
            insert.Close();
            reportViewer1.RefreshReport();
            //
            
        }
    }
}
