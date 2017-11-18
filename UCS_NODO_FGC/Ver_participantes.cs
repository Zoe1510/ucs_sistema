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
    public partial class Ver_participantes : Form
    {
        public Ver_participantes()
        {
            InitializeComponent();
        }

        private void Ver_participantes_Load(object sender, EventArgs e)
        {
            this.Location = new Point(-5, 0);
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            Modificar_participante par = new Modificar_participante();
            par.ShowDialog();
        }
    }
}
