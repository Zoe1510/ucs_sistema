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
    public partial class Ver_postulados : Form
    {
        Participantes part = new Participantes();
        R_Formacion_lista LF = new R_Formacion_lista();

        public Ver_postulados()
        {
            InitializeComponent();

            dgvParticipantes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            //PARA SELECCIONAR
            dgvParticipantes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvParticipantes.MultiSelect = false;
            dgvParticipantes.ClearSelection();
        }
        int resultado = 0;
        private void Ver_postulados_Load(object sender, EventArgs e)
        {
            Busqueda(Cursos.nombre_formacion13, Cursos.estatus_formacion13, Cursos.id_curso13);

            if (Cursos.estatus_formacion13 != "En curso")
            {
                btnEliminar.Enabled = false;
                btnImprimir.Enabled = false;
                if (Cursos.estatus_formacion13 == "Finalizado")
                {
                    btnCorreoPart.Enabled = true;
                    btnCorreoFacilitadores.Enabled = true;
                    if (Cursos.tipo_formacion13 == "InCompany")
                    {
                        btnCorreoCliente.Enabled = true;
                    }
                    else if (Cursos.tipo_formacion13 == "INCES")
                    {
                        btnCorreoCliente.Enabled = true;
                    }
                }
            }
            else
            {
                btnEliminar.Enabled = true;
                btnImprimir.Enabled = true;

                btnCorreoPart.Enabled = false;
                btnCorreoFacilitadores.Enabled = false;
                btnCorreoCliente.Enabled = false;
            }
        }
        private void Busqueda(string nombreC, string e, int f)
        {
            try
            {
                dgvParticipantes.Rows.Clear();

                LF.tipo_formacion = Cursos.tipo_formacion13;
                LF.nombre_formacion = Cursos.nombre_formacion13;
                LF.fecha_inicio = Cursos.fecha_uno13;
                if (Cursos.fecha_dos13 == "No aplica")
                    LF.fecha_culminacion = LF.fecha_inicio;
                else
                    LF.fecha_culminacion = Cursos.fecha_dos13;

                int id_fa = 0;
                //MessageBox.Show(Cursos.id_curso13.ToString());
                //buscar id_fa de acuerdo al id del curso
                MySqlDataReader leer = Conexion.ConsultarBD("SELECT * from cursos_tienen_fa where cursos_id_cursos = '" + Cursos.id_curso13 + "'");
                if (leer.Read())
                {
                    id_fa = Convert.ToInt32(leer["facilitadores_id_fa"]);

                }
                leer.Close();

                switch (Cursos.duracion_formacion13)
                {
                    case "4":
                        LF.duracion_formacion = "4 Horas";
                        break;
                    case "8":
                        LF.duracion_formacion = "8 Horas";
                        break;
                    case "16":
                        LF.duracion_formacion = "16 Horas";
                        break;

                }
                MySqlDataReader nom = Conexion.ConsultarBD("select * from facilitadores where id_fa='" + id_fa + "'");
                while (nom.Read())
                {

                    LF.nombre_facilitador = nom["nombre_fa"].ToString();

                }
                nom.Close();

                MySqlDataReader b = Conexion.ConsultarBD("SELECT nombreE, nombre_par, apellido_par, cedula_par, correo_par, tlfn_par FROM participantes p inner join cursos_tienen_participantes ctp on ctp_id_participante = p.id_participante  where  ctp.ctp_id_curso='" + f + "'");
                while (b.Read())
                {
                    R_Participantes_postulados pp = new R_Participantes_postulados();
                    //MessageBox.Show("entré a la busqueda");
                    resultado += 1;
                    dgvParticipantes.Rows.Add(b["cedula_par"], b["nombre_par"], b["apellido_par"], b["correo_par"], b["tlfn_par"], b["nombreE"]);

                    pp.nombre_participante = b["apellido_par"].ToString() + " " + b["nombre_par"].ToString();
                    pp.apellido_participante = "";
                    pp.cedula_participante = b["cedula_par"].ToString();
                    pp.organizacion_part = b["nombreE"].ToString();
                    pp.tlfn_participante = b["tlfn_par"].ToString();
                    pp.correo_participante = b["correo_par"].ToString();
                    LF.cantidad_participantes = resultado.ToString();
                    LF.lista_participantes.Add(pp);
                }
                b.Close();

            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvParticipantes.SelectedRows.Count == 1)
            {


                if (MessageBox.Show("¿Está seguro de eliminar al postulado " + part.nombreP + "?", "AVISO", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {

                    MySqlDataReader del = Conexion.ConsultarBD("DELETE FROM cursos_tienen_participantes WHERE ctp_id_curso='" + Cursos.id_curso13 + "' AND ctp_id_participante='" + part.id_participante + "'");
                    del.Close();
                    Busqueda(Cursos.nombre_formacion13, Cursos.estatus_formacion13, Cursos.id_curso13);

                }
            }
            else
            {
                MessageBox.Show("Debe seleccionar un participante de la lista.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void dgvParticipantes_MouseClick(object sender, MouseEventArgs e)
        {
            {
                part.ci_participante = Convert.ToInt32(dgvParticipantes.SelectedRows[0].Cells[0].Value.ToString());
                part.nombreP = dgvParticipantes.SelectedRows[0].Cells[1].Value.ToString();
                part.apellidoP = dgvParticipantes.SelectedRows[0].Cells[2].Value.ToString();
                part.correoP = dgvParticipantes.SelectedRows[0].Cells[3].Value.ToString();
                part.nombreE = dgvParticipantes.SelectedRows[0].Cells[4].Value.ToString();

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
    }
}
