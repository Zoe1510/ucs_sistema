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

namespace UCS_NODO_FGC
{
    public partial class Notificaciones : Form
    {
        public Notificaciones()
        {
            InitializeComponent();
        }

        private void Notificaciones_Load(object sender, EventArgs e)
        {
            this.Location = new Point(-5, 0);
            int nroF = 0;
            MySqlDataReader leer = Conexion.ConsultarBD("select * from cursos where id_usuario1='" + Usuario_logeado.id_usuario + "'");
            while (leer.Read())
            {
                nroF += 1;
            }
            txtNroCursos.Text = nroF.ToString();

        }
    }
}
