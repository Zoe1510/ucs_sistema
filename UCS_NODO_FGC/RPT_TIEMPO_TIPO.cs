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
            DateTime emision = DateTime.Now;
            string fecha = emision.ToString("dd-MM-yyyy");
            
            string nombre_reporte = "Reporte ("+info[0].tipo_formacion+ ") " + fecha + " ";
            //nuevo:
            int cantidad = 0;
            string extension = ".pdf";
            string ruta = @"C:\\Users\\ZM\\Documents\\Last_repo\\ucs_sistema\\UCS_NODO_FGC\\Archivos\\Reportes_emitidos\\";  //cambia cuando se instale
            //todo lo que se haga aquí, es nuevo 
            string nuevonombre;
            //aqui, se modificaré el nombre del archivo, añadiendo una cuenta progresiva de acuerdo a los existentes en la carpeta contenedora
            string[] dirs = Directory.GetFiles(@"C:\\Users\\ZM\\Documents\\Last_repo\\ucs_sistema\\UCS_NODO_FGC\\Archivos\\Reportes_emitidos", nombre_reporte + cantidad.ToString() + extension);
            int retorno = dirs.Length;
            while (retorno != 0)
            {
                cantidad += 1;
                nuevonombre = nombre_reporte + cantidad.ToString() + extension;
                string[] check = Directory.GetFiles(@"C:\\Users\\ZM\\Documents\\Last_repo\\ucs_sistema\\UCS_NODO_FGC\\Archivos\\Reportes_emitidos", nuevonombre);
                retorno = check.Length;
            }
            nuevonombre = nombre_reporte + cantidad.ToString() + extension;

            string destino = Path.Combine(ruta, nuevonombre);

            reportViewer1.LocalReport.DisplayName = "tipo formacion";
            //Establezcamos la lista como Datasource del informe
            //
            reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("dsFormacion", info));

            File.WriteAllBytes(destino, reportViewer1.LocalReport.Render("PDF"));
            //
            MySqlDataReader insert = Conexion.ConsultarBD("insert into reportes (nombre_reporte, fecha_creacion,id_creador_usuario, ruta_reporte) values ('" + nuevonombre + "', '" + DateTime.Now + "', '" + Usuario_logeado.id_usuario + "', '" + destino + "')");
            insert.Close();
            reportViewer1.RefreshReport();
            //

            this.reportViewer1.RefreshReport();
           
        }
    }
}
