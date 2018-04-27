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
    public partial class Ver_participantes : Form
    {

        Formaciones formaciones = new Formaciones();
        Participantes part = new Participantes();
       // int resultado = 0;
        
        public Ver_participantes()
        {
            InitializeComponent();

            dgvParticipantes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            //PARA SELECCIONAR
            dgvParticipantes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvParticipantes.MultiSelect = false;
            dgvParticipantes.ClearSelection();
        }

        private void Ver_participantes_Load(object sender, EventArgs e)
        {
            this.Location = new Point(-5, 0);
            llenarDGV();
           
        }
        /*----------------------METODOS------------------------*/

        private void llenarDGV()
        {
           
           try
            {
                MySqlDataReader participantes = Conexion.ConsultarBD("SELECT nombre_par, cedula_par, apellido_par, correo_par, id_cli1, nombreE, tlfn_par FROM participantes order by nombre_par, apellido_par");
                while (participantes.Read())
                {

                    part.nombreE = Convert.ToString(participantes["nombreE"]);
                    part.ci_participante = Convert.ToInt32(participantes["cedula_par"]);
                    part.id_cli1 = Convert.ToInt32(participantes["id_cli1"]);
                    part.nombreP = Convert.ToString(participantes["nombre_par"]);
                    part.apellidoP = Convert.ToString(participantes["apellido_par"]);
                    part.correoP = Convert.ToString(participantes["correo_par"]);
                    part.tlfn_participante= Convert.ToString(participantes["tlfn_par"]);
                    dgvParticipantes.Rows.Add(part.ci_participante, part.nombreP, part.apellidoP, part.correoP,part.tlfn_participante, part.nombreE);
                    
                }
                dgvParticipantes.ClearSelection();
                participantes.Close();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void refrescar()
        {
            dgvParticipantes.Rows.Clear();
            llenarDGV();
            
        } 

        private void Busqueda(string nombreC, string e, string f)
        {
            try
            {
                dgvParticipantes.Rows.Clear();
                
                //"SELECT nombreE, nombre_par, apellido_par, cedula_par, correo_par FROM cursos_tienen_participantes ctp inner join cursos cur on cur.id_cursos = ctp.ctp_id_curso inner join participantes p on ctp.ctp_id_participante = p.id_participante where cur.nombre_curso like 'Prueba abierto' and cur.estatus_curso like 'En curso' and cur.fecha_uno like '2018-03-01'"
                
                MySqlDataReader b = Conexion.ConsultarBD("SELECT nombreE, nombre_par, apellido_par, cedula_par, correo_par, tlfn_par FROM participantes p inner join cursos_tienen_participantes ctp on ctp_id_participante = p.id_participante inner join cursos cur on ctp.ctp_id_curso = cur.id_cursos where cur.nombre_curso like '" + nombreC + "' and cur.estatus_curso like '" + e + "' and cur.fecha_uno like '"+f+"'");
                while (b.Read())
                {                   
                   
                    dgvParticipantes.Rows.Add(b["cedula_par"], b["nombre_par"], b["apellido_par"], b["correo_par"],b["tlfn_par"], b["nombreE"]);                    
                }
                b.Close();
                
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /*----------------------EVENTOS-------------------*/
        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (dgvParticipantes.SelectedRows.Count == 1)
            {
                Participante_seleccionado.id_participante = part.id_participante;
                Participante_seleccionado.ci_participante = part.ci_participante;
                Participante_seleccionado.nacionalidad = part.nacionalidad;
                Participante_seleccionado.nombreP=part.nombreP;
                Participante_seleccionado.apellidoP = part.apellidoP;
                Participante_seleccionado.correoP = part.correoP;
                Participante_seleccionado.id_cli1 = part.id_cli1;
                Participante_seleccionado.nivelE = part.nivelE;
                Participante_seleccionado.cargoE = part.cargoE;
                Participante_seleccionado.nombreE = part.nombreE;
                Participante_seleccionado.tlfn_par = part.tlfn_participante;
               
                Modificar_participante par = new Modificar_participante();
                par.ShowDialog();
                refrescar();
            }
            else
            {
                MessageBox.Show("Debe seleccionar un participante de la lista.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }

        //private void btnBuscar_Click(object sender, EventArgs e)
        //{
        //    if (txtBuscarNombre.Text == "" && cmbxEstatus.SelectedIndex == -1)
        //    {
        //        errorProviderCmbxNombre.SetError(txtBuscarNombre, "Debe escribir el nombre de una formación.");
        //        errorProviderCmbxEstatus.SetError(cmbxEstatus, "Debe seleccionar un estatus.");
        //        txtBuscarNombre.Focus();
        //    }
        //    else
        //    {
        //        errorProviderCmbxNombre.SetError(txtBuscarNombre, "");
        //        errorProviderCmbxEstatus.SetError(cmbxEstatus, "");
        //        if (txtBuscarNombre.Text == "")
        //        {
        //            errorProviderCmbxNombre.SetError(txtBuscarNombre, "Debe escribir el nombre de una formación.");
        //            txtBuscarNombre.Focus();
        //        }
        //        else
        //        {
        //            errorProviderCmbxNombre.SetError(txtBuscarNombre, "");
        //            if (cmbxEstatus.SelectedIndex == -1)
        //            {
        //                errorProviderCmbxEstatus.SetError(cmbxEstatus, "Debe seleccionar un estatus.");
        //                cmbxEstatus.Focus();
        //            }
        //            else
        //            {
                        
        //                errorProviderCmbxEstatus.SetError(cmbxEstatus, "");
        //                string nombre = txtBuscarNombre.Text;
                        
        //                string fecha = dtpFechaCurso.Value.ToString("yyyy-MM-dd");
        //                DateTime today = DateTime.Today;
        //                Busqueda(nombre, formaciones.estatus, fecha);
                        
        //                //MessageBox.Show(""+nombre+","+estatus+", "+fecha+"");

        //                //aqui debe estar el metodo que permita llenar el dgv con los parametros de busqueda
        //                if (resultado == 1)
        //                {
        //                   // dgvParticipantes.CurrentRow.Selected = true;

        //                    if (today < DateTime.Parse(fecha) && formaciones.estatus == "En curso")
        //                    {
        //                        btnEliminar.Enabled = true;
        //                        btnImprimir.Enabled = true;
        //                    }
        //                    else
        //                    {
        //                        btnEliminar.Enabled = false;
        //                        btnImprimir.Enabled = false;
        //                    }


        //                }
        //                else
        //                {
        //                    MessageBox.Show("La búsqueda no ha arrojado resultados.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //                    refrescar();
        //                }
        //            }
        //        }
        //    }
        //}

        private void btnRefrescar_Click(object sender, EventArgs e)
        {
            refrescar();
        }

        //private void cmbxEstatus_SelectionChangeCommitted(object sender, EventArgs e)
        //{
        //    string estatus = Convert.ToString(cmbxEstatus.SelectedIndex);
        //    switch (estatus)
        //    {
        //        case "0":
        //            estatus = "En curso";
        //            break;
        //        case "1":
        //            estatus = "Reprogramado";
        //            break;
        //        case "2":
        //            estatus = "Suspendido";
        //            break;
        //        case "3":
        //            estatus = "Finalizado";
        //            break;
        //    }
        //    formaciones.estatus = estatus;
        //}

        //private void txtNombrePart_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    Clases.Paneles.sololetras(e);
        //    if (txtBuscarNombre.Text.Length == 0)
        //    {
        //        e.KeyChar = e.KeyChar.ToString().ToUpper().ToCharArray()[0];
        //    }
        //    else if (txtBuscarNombre.Text.Length > 0)
        //    {
        //        e.KeyChar = e.KeyChar.ToString().ToLower().ToCharArray()[0];
        //    }
        //}

        private void dgvParticipantes_MouseClick(object sender, MouseEventArgs e)
        {
            if(dgvParticipantes.SelectedRows.Count == 1)
            {
                part.ci_participante=Convert.ToInt32(dgvParticipantes.SelectedRows[0].Cells[0].Value.ToString());
                part.nombreP=dgvParticipantes.SelectedRows[0].Cells[1].Value.ToString();
                part.apellidoP=dgvParticipantes.SelectedRows[0].Cells[2].Value.ToString();
                part.correoP=dgvParticipantes.SelectedRows[0].Cells[3].Value.ToString();
                part.nombreE=dgvParticipantes.SelectedRows[0].Cells[4].Value.ToString();

                MySqlDataReader buscarId = Conexion.ConsultarBD("SELECT id_participante, id_cli1, nivelE, cargoE, nacionalidad FROM participantes WHERE cedula_par='" + part.ci_participante + "'");
                if (buscarId.Read())
                {
                    part.id_participante = Convert.ToInt32(buscarId["id_participante"]);
                    part.id_cli1 = Convert.ToInt32(buscarId["id_cli1"]);
                    part.nivelE = Convert.ToString(buscarId["nivelE"]);
                    part.cargoE = Convert.ToString(buscarId["cargoE"]);
                    part.nacionalidad = Convert.ToString(buscarId["nacionalidad"]);
                }
                buscarId.Close();

                
            }
        }

        //private void btnEliminar_Click(object sender, EventArgs e)
        //{
        //    if (dgvParticipantes.SelectedRows.Count == 1)
        //    {
        //        string nombre = txtBuscarNombre.Text;
        //        string estatus = formaciones.estatus;
        //        string fecha = dtpFechaCurso.Value.ToString("yyyy-MM-dd");
        //        int id_curso = 0;
        //        if (MessageBox.Show("¿Está seguro de eliminar al postulado " + part.nombreP + "?", "AVISO", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
        //        {
        //            MySqlDataReader id = Conexion.ConsultarBD("SELECT id_cursos from cursos WHERE nombre_curso='" + nombre + "' AND fecha_uno='" + fecha + "' AND estatus_curso='" + estatus + "'");
        //            if (id.Read())
        //            {
        //                id_curso = Convert.ToInt32(id["id_cursos"]);
        //            }
        //            id.Close();
        //            MySqlDataReader del = Conexion.ConsultarBD("DELETE FROM cursos_tienen_participantes WHERE ctp_id_curso='" + id_curso + "' AND ctp_id_participante='"+ part.id_participante + "'");
        //            del.Close();
                    
        //            Busqueda(nombre, estatus, fecha);
        //        }
        //    }
        //    else
        //    {
        //        MessageBox.Show("Debe seleccionar un participante de la lista.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        //    }
        //}
    }
}
