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
    public partial class Ver_formatos : Form
    {
        public Ver_formatos()
        {
            InitializeComponent();
            dgvFormatos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            //PARA SELECCIONAR
            dgvFormatos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvFormatos.MultiSelect = false;
            dgvFormatos.ClearSelection();
        }

        private void Ver_formatos_Load(object sender, EventArgs e)
        {

        }
    }
}
