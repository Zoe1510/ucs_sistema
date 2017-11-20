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
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;

namespace UCS_NODO_FGC
{
    public partial class Cambiar_Estatus_Curso : Form
    {
        public Cambiar_Estatus_Curso()
        {
            InitializeComponent();
        }

        private void Cambiar_Estatus_Curso_Load(object sender, EventArgs e)
        {
            txtNombre.Text = Cursos.nombre_formacion13;
            string estatus = Cursos.estatus_formacion13;
            switch (estatus)
            {
                case "En curso":
                    cmbxEstatus.SelectedIndex = 0;
                    break;
                case "Reprogramado":
                    cmbxEstatus.SelectedIndex = 1;
                    break;
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Desea cancelar la operación?", "",
        MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            string status =Convert.ToString(cmbxEstatus.SelectedIndex);
            switch (status)
            {
                case "0":
                    status = "En curso";
                    break;
                case "1":
                    status = "Reprogramado";
                    break;
                case "2":
                    status = "Suspendido";
                    break;

            }

            if(status == Cursos.estatus_formacion13)
            {
                MessageBox.Show("No se han detectado cambios.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }else
            {
                if(status == "Suspendido")
                {
                    if(MessageBox.Show("Si cambia el estatus del curso a 'Suspendido' no podrá cambiarlo otra vez. ¿Desea continuar?.", "AVISO", MessageBoxButtons.YesNo, MessageBoxIcon.Information)==DialogResult.Yes)
                    {
                        MySqlDataReader cambio = Conexion.ConsultarBD("UPDATE cursos SET estatus_curso='" + status + "' WHERE id_cursos = '" + Cursos.id_curso13 + "'");
                        MessageBox.Show("Actualización exitosa.");
                        this.Close();
                        
                    }
                }
                else
                {
                    MySqlDataReader cambio = Conexion.ConsultarBD("UPDATE cursos SET estatus_curso='" + status + "' WHERE id_cursos = '" + Cursos.id_curso13 + "'");
                    MessageBox.Show("Actualización exitosa.");
                    this.Close();
                }
            }
        }
    }
}
