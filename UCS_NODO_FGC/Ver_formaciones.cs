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
    public partial class Ver_formaciones : Form
    {
        Formaciones formaciones = new Formaciones();
        public Ver_formaciones()
        {
            InitializeComponent();

            dgvFormaciones.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            //PARA SELECCIONAR
            dgvFormaciones.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvFormaciones.MultiSelect = false;
            dgvFormaciones.ClearSelection();
        }

        private void Ver_formaciones_Load(object sender, EventArgs e)
        {
            this.Location = new Point(-5, 0);
            string nombre = "";
            string estatus="";
            llenarDGV(nombre, estatus);
            cargarCursos();
        }

        private void cargarCursos()
        {
            //llenar el combobox con las empresas registradas:
            cmbxNombre.ValueMember = "id_curso12";
            cmbxNombre.DisplayMember = "nombre_formacion12";
            cmbxNombre.DataSource = Clases.Paneles.llenarCmbxCursos();
            cmbxNombre.SelectedIndex = -1;
           
        }
        private void llenarDGV(string nombre, string estatus)
        {
            dgvFormaciones.Rows.Clear();
            string query = "SELECT estatus_curso, tipo_curso, nombre_curso, duracion_curso, etapa_curso, nombre_user FROM cursos cur inner join user_gestionan_cursos ugc on cur.id_cursos = ugc.cursos_id_cursos inner join usuarios us on ugc.usuarios_id_user = us.id_user WHERE nombre_curso LIKE '%" + nombre + "%' AND estatus_curso LIKE '%" + estatus + "%' ";
            MySqlDataReader formaciones = Conexion.ConsultarBD(query);
            while (formaciones.Read())
            {
                string duracion =Convert.ToString(formaciones["duracion_curso"]);
                switch (duracion)
                {
                    case "4":
                        duracion = "4 Horas";
                        break;
                    case "8":
                        duracion = "8 Horas";
                        break;
                    case "16":
                        duracion = "16 Horas";
                        break;

                }
                string etapa_actual =Convert.ToString(formaciones["etapa_curso"]);
                switch (etapa_actual)
                {
                    case "1":
                        etapa_actual = "Nivel básico";
                        break;
                    case "2":
                        etapa_actual = "Nivel intermedio";
                        break;
                    case "3":
                        etapa_actual = "Nivel avanzado";
                        break;
                }
                dgvFormaciones.Rows.Add(formaciones["nombre_curso"], formaciones["tipo_curso"],duracion , formaciones["estatus_curso"], etapa_actual, formaciones["nombre_user"]);
            }
            dgvFormaciones.ClearSelection();
            formaciones.Close();
        }

        private void btnRefrescar_Click(object sender, EventArgs e)
        {
            string nombre = "";
            string estatus = "";
            llenarDGV(nombre, estatus);
        }

        private void cmbxNombre_SelectionChangeCommitted(object sender, EventArgs e)
        {
            formaciones.id_curso = Convert.ToInt32(cmbxNombre.SelectedValue);
        }
    }
}
