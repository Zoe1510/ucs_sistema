using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UCS_NODO_FGC
{
    public partial class Opciones_recuperacion : Form
    {
        public Opciones_recuperacion()
        {
            InitializeComponent();
        }

        private void btn_cerrar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Desea salir?", "",
         MessageBoxButtons.YesNo, MessageBoxIcon.Question)
         == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void btn_minimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnFormPreguntasSeguridad_Click(object sender, EventArgs e)
        {
            Verificacion_recuperacion verify = new Verificacion_recuperacion();
            Clases.Recuperacion_contraseña.Opcion = 1;
            verify.ShowDialog();
            if (Clases.Recuperacion_contraseña.Opcion == 0)
            {
                this.Close();
            }
        }

        private void btnFormCorreoElectronico_Click(object sender, EventArgs e)
        {
            Verificacion_recuperacion verify = new Verificacion_recuperacion();
            Clases.Recuperacion_contraseña.Opcion = 2;
            verify.ShowDialog();
            if (Clases.Recuperacion_contraseña.Opcion == 0)
            {
                this.Close();
            }
        }
    }
}
