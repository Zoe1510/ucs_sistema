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
using System.IO;
using System.Diagnostics;

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
        //esto se cambia cuando vaya a instalarlo en ucs
       
        private void Lista_participantes_Load(object sender, EventArgs e)
        {
           this.reportViewer1.RefreshReport();
            reportViewer1.LocalReport.DataSources.Clear();

            string fecha = DateTime.Today.ToString("dd-MM-yyyy");
            string nombre_reporte ="Control asistencia_"+fecha+".pdf";
            string ruta = @"C:\Users\ZM\Documents\Last_repo\ucs_sistema\UCS_NODO_FGC\Archivos\Reportes_emitidos";  //cambia cuando se instale

            string destino = Path.Combine(ruta, nombre_reporte);
            //MessageBox.Show(destino);
            //Establezcamos la lista como Datasource del informe
            //
            reportViewer1.LocalReport.DisplayName = "Control de asistencia";
            
            reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("dsEncabezado", form_encabezado));
            reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("Detalle", pp_detalle));
            File.WriteAllBytes(destino, reportViewer1.LocalReport.Render("PDF"));
            //
            MySqlDataReader insert = Conexion.ConsultarBD("insert into reportes (nombre_reporte, fecha_creacion,id_creador_usuario, ruta_reporte) values ('" + nombre_reporte + "', '" + DateTime.Today + "', '"+Usuario_logeado.id_usuario+"', '"+destino+"')");
            insert.Close();
            reportViewer1.RefreshReport();
        }
    }
}
