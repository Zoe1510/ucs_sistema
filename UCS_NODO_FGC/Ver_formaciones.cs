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
        int resultado = 0;
        string nombre_user;
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
            refrescar();
           
            
        }

        private void llenarDGV(string nombre, string estatus)
        {
            
            dgvFormaciones.Rows.Clear();
            string query = "SELECT estatus_curso, solicitud_curso, tipo_curso, nombre_curso, duracion_curso, etapa_curso, nombre_user FROM cursos cur inner join user_gestionan_cursos ugc on cur.id_cursos = ugc.cursos_id_cursos inner join usuarios us on ugc.usuarios_id_user = us.id_user WHERE nombre_curso LIKE '%" + nombre + "%' AND estatus_curso LIKE '%" + estatus + "%' ";
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
                
                dgvFormaciones.Rows.Add(formaciones["nombre_curso"], formaciones["tipo_curso"],formaciones["solicitud_curso"],duracion , formaciones["estatus_curso"], etapa_actual, formaciones["nombre_user"]);
                if(formaciones["nombre_curso"].ToString() != "")
                {
                    resultado = 1;
                }
            }
            dgvFormaciones.ClearSelection();
            formaciones.Close();
            
        }
        private void refrescar()
        {
            string nombre = "";
            string estatus = "";
            llenarDGV(nombre, estatus);
            txtBuscarNombre.Clear();
            cmbxEstatus.SelectedIndex = -1;
        }

        private void btnRefrescar_Click(object sender, EventArgs e)
        {
            refrescar();
        }              

        private void cmbxEstatus_SelectionChangeCommitted(object sender, EventArgs e)
        {
            string estatus = Convert.ToString(cmbxEstatus.SelectedIndex);
            switch (estatus)
            {
                case "0":
                    estatus = "En curso";
                    break;
                case "1":
                    estatus = "Reprogramado";
                    break;
                case "2":
                    estatus = "Suspendido";
                    break;
                case "3":
                    estatus = "Finalizado";
                    break;
            }
            formaciones.estatus = estatus;
        }
       
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if(txtBuscarNombre.Text == "" && cmbxEstatus.SelectedIndex == -1)
            {
                errorProviderCmbxNombre.SetError(txtBuscarNombre, "Debe escribir el nombre de una formación.");
                errorProviderCmbxEstatus.SetError(cmbxEstatus, "Debe seleccionar un estatus.");
                txtBuscarNombre.Focus();
            }else
            {
                errorProviderCmbxNombre.SetError(txtBuscarNombre, "");
                errorProviderCmbxEstatus.SetError(cmbxEstatus, "");
                if (txtBuscarNombre.Text == "")
                {
                    errorProviderCmbxNombre.SetError(txtBuscarNombre, "Debe escribir el nombre de una formación.");
                    txtBuscarNombre.Focus();
                }
                else
                {
                    errorProviderCmbxNombre.SetError(txtBuscarNombre, "");
                    if (cmbxEstatus.SelectedIndex == -1)
                    {
                        errorProviderCmbxEstatus.SetError(cmbxEstatus, "Debe seleccionar un estatus.");
                        cmbxEstatus.Focus();
                    }else
                    {
                        resultado = 0;
                        errorProviderCmbxEstatus.SetError(cmbxEstatus, "");
                        string nombre = txtBuscarNombre.Text;
                        string estatus = formaciones.estatus;                                         
                        llenarDGV(nombre, estatus);
                        if(resultado == 1)
                        {
                            dgvFormaciones.CurrentRow.Selected = true;
                            resultado = 0;
                            txtBuscarNombre.Clear();
                            cmbxEstatus.SelectedIndex = -1;
                            
                        }
                        else
                        {
                            MessageBox.Show("La búsqueda no ha arrojado resultados.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            refrescar();
                        }
                    }
                }
            }
        }

       

        private void dgvFormaciones_MouseClick(object sender, MouseEventArgs e)
        {
            if (dgvFormaciones.SelectedRows.Count == 1)
            {
                formaciones.nombre_formacion=dgvFormaciones.SelectedRows[0].Cells[0].Value.ToString();
                formaciones.tipo_formacion=dgvFormaciones.SelectedRows[0].Cells[1].Value.ToString();
                formaciones.solicitado = dgvFormaciones.SelectedRows[0].Cells[2].Value.ToString();
                formaciones.duracion=dgvFormaciones.SelectedRows[0].Cells[3].Value.ToString();
                formaciones.estatus=dgvFormaciones.SelectedRows[0].Cells[4].Value.ToString();
                string etapa = dgvFormaciones.SelectedRows[0].Cells[5].Value.ToString();                
                switch (etapa)
                {
                    case "Nivel básico":
                        etapa = "1";
                        break;
                    case "Nivel intermedio":
                        etapa = "2";
                        break;
                    case "Nivel avanzado":
                        etapa = "3";
                        break;
                }
                formaciones.etapa_curso =Convert.ToInt32(etapa);
                nombre_user =dgvFormaciones.SelectedRows[0].Cells[6].Value.ToString();
                MySqlDataReader id = Conexion.ConsultarBD("SELECT id_user FROM usuarios WHERE nombre_user = '" + nombre_user + "'");
                if (id.Read())
                {
                    formaciones.id_user =Convert.ToInt32(id["id_user"]);
                }
                id.Close();

                MySqlDataReader id_curso = Conexion.ConsultarBD("SELECT id_cursos FROM cursos WHERE nombre_curso='" + formaciones.nombre_formacion + "' AND estatus_curso='" + formaciones.estatus + "' AND tipo_curso='" + formaciones.tipo_formacion + "'");
                if (id_curso.Read())
                {
                    formaciones.id_curso = Convert.ToInt32(id_curso["id_cursos"]);
                    //MessageBox.Show(formaciones.id_curso.ToString());
                }
                

            }
        }

        private void btnCambiarStatus_Click(object sender, EventArgs e)
        {
            if (dgvFormaciones.SelectedRows.Count == 1)
            {
                if(formaciones.tipo_formacion == "INCES")
                {
                    MessageBox.Show("No puede cambiarle el estatus a este tipo de formación.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }else
                {
                    if (formaciones.estatus == "Finalizado" || formaciones.estatus == "Suspendido")
                    {
                        MessageBox.Show("Ya no es posible cambiar el estatus de esta formación.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }else
                    {
                        Cursos.nombre_formacion13 = formaciones.nombre_formacion;
                        Cursos.estatus_formacion13 = formaciones.estatus;
                        Cursos.id_curso13 = formaciones.id_curso;
                        Cursos.solicitud_formacion13 = formaciones.solicitado;
                        Cambiar_Estatus_Curso cec = new Cambiar_Estatus_Curso();
                        cec.ShowDialog();
                        refrescar();

                    }
                }
            }
            else
            {
                MessageBox.Show("Debe seleccionar una formación de la lista.", "AVISO", MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
            }
        }

        private void btnVerFormacion_Click(object sender, EventArgs e)
        {
            if (dgvFormaciones.SelectedRows.Count == 1)
            {
                Cursos.nombre_formacion13 = formaciones.nombre_formacion;
                Cursos.estatus_formacion13 = formaciones.estatus;
                Cursos.id_curso13 = formaciones.id_curso;
                Cursos.solicitud_formacion13 = formaciones.solicitado;
                Cursos.nombreCreador_formacion13 = nombre_user;
                Cursos.tipo_formacion13 = formaciones.tipo_formacion;
                Cursos.etapa_formacion13 = formaciones.etapa_curso;
                Cursos.id_user13 = formaciones.id_user;
                Vista_Formacion verf = new Vista_Formacion();
                verf.ShowDialog();
            }
            else
            {
                MessageBox.Show("Debe seleccionar una formación de la lista.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (dgvFormaciones.SelectedRows.Count == 1)
            {
                Cursos.nombre_formacion13 = formaciones.nombre_formacion;
                Cursos.estatus_formacion13 = formaciones.estatus;
                Cursos.id_curso13 = formaciones.id_curso;
                Cursos.solicitud_formacion13 = formaciones.solicitado;
                Cursos.nombreCreador_formacion13 = nombre_user;
                Cursos.tipo_formacion13 = formaciones.tipo_formacion;
                Cursos.etapa_formacion13 = formaciones.etapa_curso;
                Cursos.id_user13 = formaciones.id_user;
                Formaciones.creacion = false;
                if(Cursos.tipo_formacion13 == "Abierto")
                {
                    Nueva_formacion_Abierto abierto = new Nueva_formacion_Abierto();
                    abierto.ShowDialog();
                }else if(Cursos.tipo_formacion13 == "INCES")
                {
                    Nueva_formacion_INCES inces = new Nueva_formacion_INCES();
                    inces.ShowDialog();
                }else if(Cursos.tipo_formacion13 == "InCompany")
                {
                    Nueva_formacion_InCompany incomp = new Nueva_formacion_InCompany();
                    incomp.ShowDialog();

                }else if(Cursos.tipo_formacion13 == "FEE")
                {
                    Nueva_formacion_FEE fee = new Nueva_formacion_FEE();
                    fee.ShowDialog();
                }
            }
            else
            {
                MessageBox.Show("Debe seleccionar una formación de la lista.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}
