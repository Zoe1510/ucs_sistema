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

namespace UCS_NODO_FGC
{
    public partial class Detalle_reporte : Form
    {
        public Detalle_reporte()
        {
            InitializeComponent();
        }

        private void Detalle_reporte_Load(object sender, EventArgs e)
        {
            txtCreador.Text = Reporte_Seleccionado.creador;
            txtFecha.Text = Reporte_Seleccionado.fecha_emision;
            txtNombre.Text = Reporte_Seleccionado.nombre_reporte;
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
