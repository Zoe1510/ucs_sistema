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

            string fecha = DateTime.Today.ToString("dd-MM-yyyy");
            string nombre_reporte = "Horas dedicadas ("+InfoGeneral[0].nombre_formacion+") " + fecha+" ";
            //nuevo:
            string extension = ".pdf";
            string ruta = @"C:\\Users\\ZM\\Documents\\Last_repo\\ucs_sistema\\UCS_NODO_FGC\\Archivos\\Reportes_emitidos\\";  //cambia cuando se instale
            //todo lo que se haga aquí, es nuevo 
            //aqui, se modificaré el nombre del archivo, añadiendo una cuenta progresiva de acuerdo a los existentes en la carpeta contenedora
            string[] dirs = Directory.GetFiles(@"C:\\Users\\ZM\\Documents\\Last_repo\\ucs_sistema\\UCS_NODO_FGC\\Archivos\\Reportes_emitidos", nombre_reporte + extension);
            int cantidad = dirs.Length;
            //MessageBox.Show(cantidad.ToString());
            string nuevonombre = nombre_reporte + cantidad.ToString() + extension;
            string[] check = Directory.GetFiles(@"C:\\Users\\ZM\\Documents\\Last_repo\\ucs_sistema\\UCS_NODO_FGC\\Archivos\\Reportes_emitidos", nuevonombre);
            int hay = check.Length;
            if (hay != 0)
            {
                cantidad += 1;
                nuevonombre = nombre_reporte + cantidad.ToString() + extension;
                //   MessageBox.Show(nuevonombre);
            }
            //MessageBox.Show(nuevonombre);
            string destino = Path.Combine(ruta, nuevonombre);

            //MessageBox.Show(destino);
            //Establezcamos la lista como Datasource del informe
            //
            reportViewer1.LocalReport.DisplayName = "Horas dedicadas";
            //Establezcamos la lista como Datasource del informe
            //
            reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("dsGeneral", InfoGeneral));
            reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("dsEtapas", EtapasF));            
            //            
            File.WriteAllBytes(destino, reportViewer1.LocalReport.Render("PDF"));

            MySqlDataReader insert = Conexion.ConsultarBD("insert into reportes (nombre_reporte, fecha_creacion,id_creador_usuario, ruta_reporte) values ('" + nuevonombre + "', '" + DateTime.Now + "', '" + Usuario_logeado.id_usuario + "', '" + destino + "')");
            insert.Close();
            reportViewer1.RefreshReport();
        }
    }
}
