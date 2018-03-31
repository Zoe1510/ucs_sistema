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
using System.IO;
using System.Drawing.Imaging;
using System.Reflection;
using System.Diagnostics;
using System.Net.Mail;
using System.Net;

namespace UCS_NODO_FGC
{
    public partial class Ver_postulados : Form
    {
        Participantes part = new Participantes();
        R_Formacion_lista LF = new R_Formacion_lista();
        string ruta = @"C:\Users\ZM\Documents\Last_repo\ucs_sistema\UCS_NODO_FGC\Resources\logo ucs.png";
        Image pic;
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

            Datos_envio_correo.idcurso = Cursos.id_curso13;
            Datos_envio_correo.nombreformacion = Cursos.nombre_formacion13;

            if (Cursos.estatus_formacion13 != "En curso")
            {
                btnEliminar.Enabled = false;
                //btnImprimir.Enabled = false;
                btnImprimir.Enabled = true;
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
                resultado = 0;
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


                LF.duracion_formacion = Cursos.duracion_formacion13;
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
                    string nombreyapellido =Convert.ToString( nom["nombre_fa"] + " " + nom["apellido_fa"]);
                    LF.nombre_facilitador =nombreyapellido;

                }
                nom.Close();

                MySqlDataReader b = Conexion.ConsultarBD("SELECT nombreE, nombre_par, apellido_par, cedula_par, correo_par, tlfn_par FROM participantes p inner join cursos_tienen_participantes ctp on ctp_id_participante = p.id_participante  where  ctp.ctp_id_curso='" + f + "'");
                while (b.Read())
                {
                    R_Participantes_postulados pp = new R_Participantes_postulados();
                    //MessageBox.Show("entré a la busqueda");
                    resultado += 1;
                    dgvParticipantes.Rows.Add(b["cedula_par"], b["nombre_par"], b["apellido_par"], b["correo_par"], b["tlfn_par"], b["nombreE"]);
                    string nombreyapellido = (b["apellido_par"] + " " + b["nombre_par"]).ToString();
                    MessageBox.Show(nombreyapellido);
                    pp.nombre_participante = nombreyapellido;
                    pp.apellido_participante = resultado.ToString() ;
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
            if (dgvParticipantes.SelectedRows.Count == 1)
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
        private byte[] GetBytes(Image imageIn)
        {
            //
            //Usamos la clase MemoryStream para contener los bytes que compone la imagen
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, ImageFormat.Png);
            //
            //Retornamos el arreglo de bytes
            return ms.ToArray();
        }
        private void btnImprimir_Click(object sender, EventArgs e)
        {
            Lista_participantes fmr = new Lista_participantes();
            
            pic = Image.FromFile(ruta);
            string nombreyapellido = Usuario_logeado.nombre_usuario +" "+ Usuario_logeado.apellido_usuario;
            LF.nombreyapellido = nombreyapellido;
            LF.Logo = GetBytes(pic);
            fmr.form_encabezado.Add(LF);
            fmr.pp_detalle = LF.lista_participantes;
            fmr.ShowDialog();
        }

        private void btnCorreoPart_Click(object sender, EventArgs e)
        {
            //para acá recorrer el dgv y buscar correos, añadirlos a una lista de string
            foreach(DataGridViewRow row in dgvParticipantes.Rows)
            {
                string correo = Convert.ToString(row.Cells["correo_par"].Value);
                MessageBox.Show(correo);
                Datos_envio_correo.correos_participantes.Add(correo);
            }
            Datos_envio_correo.opcion = 1;
            if (AccesoInternet())
            {
                Datos_envio_correo.idcurso = Cursos.id_curso13;
                Correo_cuerpo cc = new Correo_cuerpo();
                cc.ShowDialog();
            }
            else
            {
                MessageBox.Show("No es posible enviar el correo en estos momentos (Verifique su conexión a internet).", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            Datos_envio_correo.opcion = 0;
        }

        private void btnCorreoFacilitadores_Click(object sender, EventArgs e)
        {
            //deberia llegar por medio estatico, el id del curso o el id del facilitador, para buscar el correo 
            //EL ID_CURSO SE TOMA EN EL LOAD DE ESTE FORM:
            Datos_envio_correo.opcion = 2;
            if (AccesoInternet())
            {
                Datos_envio_correo.idcurso = Cursos.id_curso13;
                Correo_cuerpo cc = new Correo_cuerpo();
                cc.ShowDialog();
            }
            else
            {
                MessageBox.Show("No es posible enviar el correo en estos momentos (Verifique su conexión a internet).", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            Datos_envio_correo.opcion = 0;

        }

        private void btnCorreoCliente_Click(object sender, EventArgs e)
        {
            //deberia llegar por medio estatico, el id del curso o el id del CLIENTE, para buscar el correo (EN CASO DE SER 
            //UNA FORMACION DE TIPO INCOMPANY O INCES.

            //EL ID_CURSO SE TOMA EN EL LOAD DE ESTE FORM:
            Datos_envio_correo.opcion = 3;
            if (AccesoInternet())
            {
                Datos_envio_correo.idcurso = Cursos.id_curso13;
                Correo_cuerpo cc = new Correo_cuerpo();
                cc.ShowDialog();
            }else
            {
                MessageBox.Show("No es posible enviar el correo en estos momentos (Verifique su conexión a internet).", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
            Datos_envio_correo.opcion = 0;
        }
        private bool AccesoInternet()
        {

            try
            {
                System.Net.IPHostEntry host = System.Net.Dns.GetHostEntry("www.google.com");
                return true;

            }
            catch (Exception es)
            {

                return false;
            }

        }
   

    }
}
