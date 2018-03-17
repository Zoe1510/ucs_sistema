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
        conexion_bd conexion = new conexion_bd();
        DateTime fecha_uno, fecha_dos;
        Tiempos_curso tiempo = new Tiempos_curso();
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
            string query = "SELECT estatus_curso, solicitud_curso, tipo_curso, nombre_curso, duracion_curso, etapa_curso, nombre_user  FROM cursos cur inner join user_gestionan_cursos ugc on cur.id_cursos = ugc.cursos_id_cursos inner join usuarios us on ugc.usuarios_id_user = us.id_user WHERE nombre_curso LIKE '%" + nombre + "%' AND estatus_curso LIKE '%" + estatus + "%' ";
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

        private void txtNombrePart_KeyPress(object sender, KeyPressEventArgs e)
        {
            Clases.Paneles.sololetras(e);
            if (txtBuscarNombre.Text.Length == 0)
            {
                e.KeyChar = e.KeyChar.ToString().ToUpper().ToCharArray()[0];
            }
            else if (txtBuscarNombre.Text.Length > 0)
            {
                e.KeyChar = e.KeyChar.ToString().ToLower().ToCharArray()[0];
            }
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
                formaciones.nombre_formacion = dgvFormaciones.SelectedRows[0].Cells[0].Value.ToString();
                formaciones.tipo_formacion = dgvFormaciones.SelectedRows[0].Cells[1].Value.ToString();
                formaciones.solicitado = dgvFormaciones.SelectedRows[0].Cells[2].Value.ToString();
                formaciones.duracion = dgvFormaciones.SelectedRows[0].Cells[3].Value.ToString();
                formaciones.estatus = dgvFormaciones.SelectedRows[0].Cells[4].Value.ToString();
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
                formaciones.etapa_curso = Convert.ToInt32(etapa);
                nombre_user = dgvFormaciones.SelectedRows[0].Cells[6].Value.ToString();
                MySqlDataReader id = Conexion.ConsultarBD("SELECT id_user FROM usuarios WHERE nombre_user = '" + nombre_user + "'");
                if (id.Read())
                {
                    formaciones.id_user = Convert.ToInt32(id["id_user"]);
                }
                id.Close();

                MySqlDataReader id_curso = Conexion.ConsultarBD("SELECT id_cursos FROM cursos WHERE nombre_curso='" + formaciones.nombre_formacion + "' AND estatus_curso='" + formaciones.estatus + "' AND tipo_curso='" + formaciones.tipo_formacion + "'");
                if (id_curso.Read())
                {
                    formaciones.id_curso = Convert.ToInt32(id_curso["id_cursos"]);
                    //MessageBox.Show(formaciones.id_curso.ToString());
                }
                id_curso.Close();

                MySqlDataReader leer = Conexion.ConsultarBD("SELECT bloque_curso, tiene_ref, ubicacion_ucs, id_p_inst from cursos where id_cursos='" + formaciones.id_curso + "'");
                if (leer.Read())
                {
                    formaciones.bloque_curso = Convert.ToString(leer["bloque_curso"]);
                    formaciones.tiene_ref = Convert.ToString(leer["tiene_ref"]);
                    formaciones.ubicacion_ucs = Convert.ToString(leer["ubicacion_ucs"]);

                    Cursos.id_pinst = Convert.ToInt32(leer["id_p_inst"]);

                }
                leer.Close();

                switch (formaciones.tiene_ref)
                {
                    case "0":
                        formaciones.tiene_ref = "No";
                        break;
                    case "1":
                        formaciones.tiene_ref = "Si";
                        break;
                }

                if(formaciones.etapa_curso >1)
                {
                    if (formaciones.bloque_curso == "1")
                    {
                        MySqlDataReader leer2 = Conexion.ConsultarBD("SELECT fecha_uno from cursos where id_cursos='" + formaciones.id_curso + "'");
                        if (leer2.Read())
                        {
                            fecha_uno = Convert.ToDateTime(leer2["fecha_uno"]);

                        }
                        leer2.Close();
                    }
                    else
                    {
                        MySqlDataReader leer2 = Conexion.ConsultarBD("SELECT fecha_uno, fecha_dos from cursos where id_cursos='" + formaciones.id_curso + "'");
                        if (leer2.Read())
                        {
                            fecha_uno = Convert.ToDateTime(leer2["fecha_uno"]);
                            fecha_dos = Convert.ToDateTime(leer2["fecha_dos"]);

                        }
                        leer2.Close();
                    }
                    int id_ctp = 0;
                    MySqlDataReader leer3 = Conexion.ConsultarBD("SELECT id_ctf from cursos_tienen_participantes where ctp_id_curso='" + formaciones.id_curso + "'");
                    if (leer3.Read())
                    {
                        id_ctp = Convert.ToInt32(leer3["id_ctf"]);
                    }
                    if (id_ctp == 0)
                    {
                        btnListaP.Enabled = false;
                    }
                    else
                    {
                        btnListaP.Enabled = true;
                    }
                }
                
            
            }
        }
       
        private void btnCambiarStatus_Click(object sender, EventArgs e)
        {
            if (dgvFormaciones.SelectedRows.Count == 1)
            {
                //if(formaciones.tipo_formacion == "INCES")
                //{
                //    MessageBox.Show("No puede cambiarle el estatus a este tipo de formación.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //}else
                //{
                
                if (formaciones.estatus == "Finalizado" || formaciones.estatus == "Suspendido")
                {
                    MessageBox.Show("Ya no es posible cambiar el estatus de esta formación.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else if (formaciones.estatus == "Reprogramado")
                {
                    if(formaciones.etapa_curso == 1) //si el curso está en un estado básico
                    {
                        Cursos.nombre_formacion13 = formaciones.nombre_formacion;
                        Cursos.estatus_formacion13 = formaciones.estatus;
                        Cursos.id_curso13 = formaciones.id_curso;
                        Cursos.solicitud_formacion13 = formaciones.solicitado;
                        Cambiar_Estatus_Curso cec = new Cambiar_Estatus_Curso();
                        cec.ShowDialog();
                        refrescar();
                    }
                    else //si por el contrario, se encuentra en nivel intermedio o avanzado
                    {
                        tiempo.fecha_curso = fecha_uno.ToString("dd-MM-yyyy");
                        if (formaciones.bloque_curso == "1" || formaciones.bloque_curso == "2")
                        {
                            
                            if (fecha_uno <= DateTime.Now)
                            {
                                MessageBox.Show("Antes de cambiar el estatus, debe proporcionar una fecha válida (Modificar fecha).", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
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
                }
                else
                {
                    Cursos.nombre_formacion13 = formaciones.nombre_formacion;
                    Cursos.estatus_formacion13 = formaciones.estatus;
                    Cursos.id_curso13 = formaciones.id_curso;
                    Cursos.solicitud_formacion13 = formaciones.solicitado;
                    Cambiar_Estatus_Curso cec = new Cambiar_Estatus_Curso();
                    cec.ShowDialog();
                    refrescar();

                }
                //}
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

                llenardatos();
                Vista_Formacion verf = new Vista_Formacion();
                verf.ShowDialog();
                vaciardatos();
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
                if (formaciones.estatus == "Finalizado" || formaciones.estatus == "Suspendido")
                {
                    MessageBox.Show("Ya no es posible modificar esta formación.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }else
                {
                    llenardatos();
                    Formaciones.creacion = false;
                    if (Cursos.tipo_formacion13 == "Abierto")
                    {
                        Nueva_formacion_Abierto abierto = new Nueva_formacion_Abierto();
                        abierto.ShowDialog();
                    }
                    else if (Cursos.tipo_formacion13 == "INCES")
                    {
                        Nueva_formacion_INCES inces = new Nueva_formacion_INCES();
                        inces.ShowDialog();
                    }
                    else if (Cursos.tipo_formacion13 == "InCompany")
                    {
                        Nueva_formacion_InCompany incomp = new Nueva_formacion_InCompany();
                        incomp.ShowDialog();

                    }
                    else if (Cursos.tipo_formacion13 == "FEE")
                    {
                        Nueva_formacion_FEE fee = new Nueva_formacion_FEE();
                        fee.ShowDialog();
                    }
                    vaciardatos();
                }
                
            }
            else
            {
                MessageBox.Show("Debe seleccionar una formación de la lista.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void llenardatos()
        {
            Cursos.nombre_formacion13 = formaciones.nombre_formacion;
            Cursos.estatus_formacion13 = formaciones.estatus;
            Cursos.id_curso13 = formaciones.id_curso;
            Cursos.solicitud_formacion13 = formaciones.solicitado;
            Cursos.nombreCreador_formacion13 = nombre_user;
            Cursos.tipo_formacion13 = formaciones.tipo_formacion;
            Cursos.etapa_formacion13 = formaciones.etapa_curso;
            Cursos.id_user13 = formaciones.id_user;
            Cursos.duracion_formacion13 = formaciones.duracion;
            Cursos.bloque_curso13 = formaciones.bloque_curso;
            Cursos.tiene_ref = formaciones.tiene_ref;
            Cursos.ubicacion_ucs = formaciones.ubicacion_ucs;
            Paquete_instruccional pq = new Paquete_instruccional();
            conexion.cerrarconexion();
            if (conexion.abrirconexion()==true)
                pq = Clases.Formaciones.obtenerTodoPq(conexion.conexion, Cursos.id_pinst);
            conexion.cerrarconexion();

            Cursos.p_contenido = pq.contenido;
            Cursos.p_presentacion = pq.presentacion;
            Cursos.p_manual = pq.manual;
            Cursos.p_bitacora = pq.bitacora;

            if (formaciones.etapa_curso > 1)
            {
                if (formaciones.etapa_curso == 3)
                {
                    //si está en esta etapa, se podrá recoger aulas, horarios, insumos y tipos de refrigerios, si es que aplica
                    if (formaciones.tiene_ref == "No")
                    {
                        if (formaciones.ubicacion_ucs == "No") //Si la formacion NO se realiza en la ucs
                        {
                            if (formaciones.bloque_curso == "1")
                            {
                                Cursos.aula1 = "No aplica";
                                Cursos.aula2 = "No aplica";
                                Cursos.tipo_ref1 = "No aplica";
                                Cursos.tipo_ref2 = "No aplica";
                                Cursos.horario2 = "No aplica";
                                //lo que se puede buscar en este caso: horario 1                             

                            }
                            else if (formaciones.bloque_curso == "2")
                            {
                                Cursos.aula1 = "No aplica";
                                Cursos.aula2 = "No aplica";
                                Cursos.tipo_ref1 = "No aplica";
                                Cursos.tipo_ref2 = "No aplica";
                                MySqlDataReader h2 = Conexion.ConsultarBD("select horario from horarios h inner join cursos c on c.horario_dos=h.idhorarios where c.id_cursos='" + formaciones.id_curso + "'");
                                if (h2.Read())
                                {
                                    Cursos.horario2 = Convert.ToString(h2["horario"]);
                                }
                                h2.Close();
                                //lo que se puede buscar en este caso: horario 1 y horario2
                            }


                        }
                        else //si la formacion se realiza en la UCS BUSCAR:
                        {
                            if (formaciones.bloque_curso == "1")
                            {                               
                                Cursos.aula2 = "No aplica";
                                Cursos.tipo_ref1 = "No aplica";
                                Cursos.tipo_ref2 = "No aplica";
                                Cursos.horario2 = "No aplica";

                                MySqlDataReader aulas = Conexion.ConsultarBD("select aula_dia1, aula_dia2 from cursos where id_cursos='" + formaciones.id_curso + "'");
                                if (aulas.Read())
                                {
                                    Cursos.aula1 = Convert.ToString(aulas["aula_dia1"]);                                  
                                }
                                aulas.Close();
                                //lo que se puede buscar en este caso: horario 1 y aula 1
                            }
                            else if (formaciones.bloque_curso == "2")
                            {
                               
                                Cursos.tipo_ref1 = "No aplica";
                                Cursos.tipo_ref2 = "No aplica";

                                //lo que se puede buscar en este caso: horario 1 y 2, aula 1 y 2

                                MySqlDataReader h2 = Conexion.ConsultarBD("select horario from horarios h inner join cursos c on c.horario_dos=h.idhorarios where c.id_cursos='" + formaciones.id_curso + "'");
                                if (h2.Read())
                                {
                                    Cursos.horario2 = Convert.ToString(h2["horario"]);
                                }
                                h2.Close();

                                MySqlDataReader aulas = Conexion.ConsultarBD("select aula_dia1, aula_dia2 from cursos where id_cursos='" + formaciones.id_curso + "'");
                                if (aulas.Read())
                                {
                                    Cursos.aula1 = Convert.ToString(aulas["aula_dia1"]);
                                    Cursos.aula2= Convert.ToString(aulas["aula_dia2"]);

                                }
                                aulas.Close();
                            }

                          
                        }
                        
                    }
                    else //Si la formacion tiene refrigerio, de lógica es porque se va a realizar en la UCS
                    {
                        if (formaciones.bloque_curso == "1")
                        {                            
                            Cursos.aula2 = "No aplica";                            
                            Cursos.tipo_ref2 = "No aplica";
                            Cursos.horario2 = "No aplica";
                            //lo que se puede buscar en este caso: horario1, aula1, id_ref1

                            MySqlDataReader ar = Conexion.ConsultarBD("select aula_dia1, ref_nombre from cursos c inner join refrigerios r on r.id_ref=c.id_ref1  where c.id_cursos='" + formaciones.id_curso + "'");
                            if (ar.Read())
                            {
                                Cursos.aula1 = Convert.ToString(ar["aula_dia1"]);
                                Cursos.tipo_ref1 = Convert.ToString(ar["ref_nombre"]);
                            }
                            ar.Close();
                        }
                        else if (formaciones.bloque_curso == "2")
                        {
                            int idr1 = 0, idr2 = 0;
                            //lo que se puede buscar en este caso: TODO

                            MySqlDataReader h2 = Conexion.ConsultarBD("select horario from horarios h inner join cursos c on c.horario_dos=h.idhorarios where c.id_cursos='" + formaciones.id_curso + "'");
                            if (h2.Read())
                            {
                                Cursos.horario2 = Convert.ToString(h2["horario"]);
                            }
                            h2.Close();

                            MySqlDataReader aulas = Conexion.ConsultarBD("select aula_dia1, aula_dia2, id_ref1, id_ref2 from cursos where id_cursos='" + formaciones.id_curso + "'");
                            if (aulas.Read())
                            {
                                Cursos.aula1 = Convert.ToString(aulas["aula_dia1"]);
                                Cursos.aula2 = Convert.ToString(aulas["aula_dia2"]);
                                idr1 = Convert.ToInt32(aulas["id_ref1"]);
                                idr2 = Convert.ToInt32(aulas["id_ref2"]);
                            }
                           
                            aulas.Close();

                            MySqlDataReader r1 = Conexion.ConsultarBD("select ref_nombre from refrigerios where id_ref='"+idr1+"'");
                            if (r1.Read())
                            {
                                Cursos.tipo_ref1 = Convert.ToString(r1["ref_nombre"]);
                            }
                            r1.Close();

                            MySqlDataReader r2 = Conexion.ConsultarBD("select ref_nombre from refrigerios where id_ref='"+idr2+"'");
                            if (r2.Read())
                            {
                                Cursos.tipo_ref2 = Convert.ToString(r2["ref_nombre"]);
                            }
                            r2.Close();
                        }
                    }
                }

                if (formaciones.bloque_curso == "1")
                {
                    Cursos.fecha_uno13 = fecha_uno.ToString("dd-MM-yyyy");
                    Cursos.fecha_dos13 = "No aplica";
                }
                else
                {
                    Cursos.fecha_uno13 = fecha_uno.ToString("dd-MM-yyyy");
                    Cursos.fecha_dos13 = fecha_dos.ToString("dd-MM-yyyy");
                }

                //todos los casos se busca el horario 1 como minimo:

                MySqlDataReader leer = Conexion.ConsultarBD("select horario from horarios h inner join cursos c on c.horario_uno=h.idhorarios where c.id_cursos='" + formaciones.id_curso + "'");
                if (leer.Read())
                {
                    Cursos.horario1 = Convert.ToString(leer["horario"]);
                }
                leer.Close();

            }
           MessageBox.Show(Cursos.ubicacion_ucs + Cursos.tiene_ref + Cursos.horario1 + Cursos.horario2 + Cursos.tipo_ref1  + Cursos.tipo_ref2  + Cursos.aula1  + Cursos.aula2 );
        }

        private void btnListaP_Click(object sender, EventArgs e)
        {
            llenardatos();

            if (Cursos.etapa_formacion13 > 1)
            {
                Ver_postulados vp = new Ver_postulados();
                vp.ShowDialog();
                vaciardatos();
            }
        }

        private void vaciardatos()
        {
            Cursos.nombre_formacion13 = "";
            Cursos.estatus_formacion13 = "";
            Cursos.id_curso13 =0;
            Cursos.solicitud_formacion13 = "";
            Cursos.nombreCreador_formacion13 ="";
            Cursos.tipo_formacion13 = "";
            Cursos.etapa_formacion13 = 0;
            Cursos.id_user13 = 0;
            Cursos.duracion_formacion13 = "";
            Cursos.bloque_curso13 = "";
            Cursos.tiene_ref = "";
            Cursos.ubicacion_ucs ="";
            Cursos.p_contenido = "";
            Cursos.p_presentacion ="";
            Cursos.p_manual ="";
            Cursos.p_bitacora ="";
            Cursos.aula1 = "";
            Cursos.aula2 = "";
            Cursos.tipo_ref1 = "";
            Cursos.tipo_ref2 = "";
            Cursos.horario1 = "";
            Cursos.horario2 = "";
            Cursos.fecha_uno13 = "";
            Cursos.fecha_dos13 = "";
        }
    }
}
