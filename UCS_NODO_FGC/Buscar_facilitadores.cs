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
    public partial class Buscar_facilitadores : Form
    {
     
        public Clases.Facilitadores facilitador = new Clases.Facilitadores();
        public Clases.Facilitadores fa { get; set; }
        public Clases.conexion_bd conexion = new Clases.conexion_bd();
        int retorno = 0;
        string txtbuscar="";
        public Buscar_facilitadores()
        {
            InitializeComponent();
            dgvFa.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            //PARA SELECCIONAR
            dgvFa.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvFa.MultiSelect = false;
            dgvFa.ClearSelection();
        }

        private void txtBuscarTodo_KeyPress(object sender, KeyPressEventArgs e)
        {
             //Clases.Paneles.letrasynumeros(e); //revisar metodo, algo falla
            if (txtBuscarTodo.Text.Length == 0)
            {
                e.KeyChar = e.KeyChar.ToString().ToUpper().ToCharArray()[0];
            }
            else if (txtBuscarTodo.Text.Length > 0)
            {
                e.KeyChar = e.KeyChar.ToString().ToLower().ToCharArray()[0];
            }
            if ((int)e.KeyChar == (int)Keys.Enter)
            {
                dgvFa.ReadOnly = true;
                try
                {
                    if (txtBuscarTodo.Text != "")
                    {
                        conexion.cerrarconexion();
                        if (conexion.abrirconexion() == true)
                        {
                            txtbuscar = txtBuscarTodo.Text;

                            buscar(conexion.conexion, txtbuscar);
                            txtBuscarTodo.Text = "Escriba aquí";
                            conexion.cerrarconexion();
                        }


                    }
                    else
                    {
                        MessageBox.Show("Debe introducir algun dato para buscar.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    }


                }
                catch (MySql.Data.MySqlClient.MySqlException ex)
                {
                    MessageBox.Show(ex.Message);
                    conexion.cerrarconexion();
                }
            }
        }
        private void txtBuscarTodo_KeyDown(object sender, KeyEventArgs e)
        {
            while (txtBuscarTodo.Text == "Escriba aquí")
            {
                txtBuscarTodo.Text = "";

            }
        }
        private void txtBuscarTodo_Leave(object sender, EventArgs e)
        {
            if (txtBuscarTodo.Text == "")
            {
                txtBuscarTodo.Text = "Escriba aquí";
            }
        }
        private void txtBuscarTodo_Click(object sender, EventArgs e)
        {

            if (txtBuscarTodo.Text != "")
            {

            }
            else
            {
                txtBuscarTodo.Text = "";
            }
        }

        private void Refrescar()
        {
            dgvFa.ReadOnly = true;
            try
            {
                conexion.cerrarconexion();
                if (conexion.abrirconexion() == true)
                {
                    txtbuscar = "";
                    txtBuscarTodo.Text = "Escriba aquí";
                    actualizarTabla(conexion.conexion, txtbuscar);
                    dgvFa.ClearSelection();
                    VaciarFa();
                    conexion.cerrarconexion();
                }
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                conexion.cerrarconexion();
            }
        }
        private void VaciarFa()
        {
            fa.id_facilitador = 0;
            fa.ci_facilitador = "0";
            fa.nacionalidad_fa = "";
            fa.nombreyapellido = "";
            fa.nombre_facilitador = "";
            fa.apellido_facilitador = "";
            fa.correo_facilitador = "";
            fa.especialidad_facilitador = "";
            fa.requerimiento_ince = 68768;
            fa.tlfn_facilitador = "";
            fa.ubicacion_facilitador = "";
            Clases.Facilitador_Seleccionado.id_facilitador = fa.id_facilitador;
            Clases.Facilitador_Seleccionado.ci_facilitador = fa.ci_facilitador;
            Clases.Facilitador_Seleccionado.nacionalidad_fa = fa.nacionalidad_fa;
            Clases.Facilitador_Seleccionado.nombre_facilitador = fa.nombre_facilitador;
            Clases.Facilitador_Seleccionado.apellido_facilitador = fa.apellido_facilitador;
            Clases.Facilitador_Seleccionado.tlfn_facilitador = fa.tlfn_facilitador;
            Clases.Facilitador_Seleccionado.correo_facilitador = fa.correo_facilitador;
            Clases.Facilitador_Seleccionado.ubicacion_facilitador = fa.ubicacion_facilitador;
            Clases.Facilitador_Seleccionado.especialidad_facilitador = fa.especialidad_facilitador;
            Clases.Facilitador_Seleccionado.requerimiento_ince = fa.requerimiento_ince;
            Facilitador_Seleccionado.VER =545647;
        }

        private void Buscar_facilitadores_Load(object sender, EventArgs e)
        {
            this.Location = new Point(-5, 0);
            try
            {
                conexion.cerrarconexion();
                if (conexion.abrirconexion() == true)
                {
                    //llenar el datagridview con los datos 
                    actualizarTabla(conexion.conexion, txtbuscar);
                    dgvFa.ClearSelection();
                    conexion.cerrarconexion();
                }
                //opciones segun el tipo de usuario
                if (Clases.Usuario_logeado.cargo_usuario == "Lider")
                {
                    grpbOpciones.Visible = true;
                    grpbOpciones.Height = 312;
                    btnEliminarFa.Visible = true;
                    btnEliminarFa.Enabled = true;
                    btnModificarFa.Enabled = true;
                    btnModificarFa.Visible = true;
                }
                else if(Clases.Usuario_logeado.cargo_usuario == "Coordinador")
                {
                    grpbOpciones.Visible = true;
                    grpbOpciones.Height = 238;
                    btnEliminarFa.Visible = false;
                    btnEliminarFa.Enabled = false;
                    btnModificarFa.Enabled = true;
                    btnModificarFa.Visible = true;
                }
                else if (Clases.Usuario_logeado.cargo_usuario == "Asistente")
                {
                    grpbOpciones.Visible = true;
                    grpbOpciones.Height = 173;
                    btnEliminarFa.Visible = false;
                    btnEliminarFa.Enabled = false;
                    btnModificarFa.Enabled = false;
                    btnModificarFa.Visible = false;

                }

            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                conexion.cerrarconexion();
            }
        }

       
        public void actualizarTabla(MySqlConnection conexion, string buscar)
        {

            try
            {

                MySqlCommand comando = new MySqlCommand(String.Format("SELECT id_fa, cedula_fa, nacionalidad_fa, nombre_fa, apellido_fa, tlfn_fa, correo_fa, ubicacion_fa, especialidad_fa, requerimiento_inces FROM facilitadores WHERE cedula_fa LIKE ('%{0}%') OR nacionalidad_fa LIKE ('%{0}%') OR nombre_fa LIKE ('%{0}%') OR apellido_fa LIKE ('%{0}%') OR tlfn_fa LIKE ('%{0}%') OR correo_fa LIKE ('%{0}%') OR ubicacion_fa LIKE ('%{0}%') OR especialidad_fa LIKE ('%{0}%')", buscar ), conexion);
                MySqlDataReader reader = comando.ExecuteReader();

                dgvFa.Rows.Clear();
                while (reader.Read())
                {
                    facilitador.id_facilitador = reader.GetInt32(0);
                    facilitador.ci_facilitador = reader.GetString(1);
                    facilitador.nacionalidad_fa = reader.GetString(2);
                    facilitador.nombre_facilitador = reader.GetString(3);
                    facilitador.apellido_facilitador = reader.GetString(4);
                    facilitador.tlfn_facilitador  = reader.GetString(5);
                    facilitador.correo_facilitador = reader.GetString(6);
                    facilitador.ubicacion_facilitador = reader.GetString(7);
                    facilitador.especialidad_facilitador = reader.GetString(8);
                    facilitador.requerimiento_ince = reader.GetInt32(9);
                    string INCES = facilitador.requerimiento_ince.ToString(); ;
                    
                    switch (INCES)
                    {
                        case "1":
                            INCES = "Sí";
                            break;
                        case "0":
                            INCES = "No";
                            break;
                    }
                    dgvFa.Rows.Add(facilitador.nacionalidad_fa, facilitador.ci_facilitador, facilitador.nombre_facilitador, facilitador.apellido_facilitador, facilitador.especialidad_facilitador, facilitador.ubicacion_facilitador);
                    retorno = 1;
                }


            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                

            }

        }

        private void buscar(MySqlConnection conexion, string buscar)
        {
            int resultado;
            actualizarTabla(conexion, buscar);
            
            resultado = retorno;
            if (resultado == 1)
            {

                //aqui, si se encuentra un resultado compatible, se debe seleccionar la fila que corresponda con el match
                dgvFa.CurrentRow.Selected = true;
                retorno = 0;
            }
            else
            {
                MessageBox.Show("No se ha encontrado ningun facilitador con los datos introducidos", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnEliminarFa_Click(object sender, EventArgs e)
        {
            dgvFa.ReadOnly = true;
            try
            {
                if(dgvFa.SelectedRows.Count == 1)
                {
                    if (MessageBox.Show("¿Está seguro de eliminar al facilitador " + facilitador.nombre_facilitador + "?", "AVISO", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        MySqlDataReader idf = Conexion.ConsultarBD("SELECT * FROM cursos_tienen_fa where facilitadores_id_fa='" + facilitador.id_facilitador + "'");
                        if (idf.Read())
                        {
                            //si está en cursos_tienen_fa, hay que evaluar las fechas asignadas antes de eliminar
                            int idprovicional = Convert.ToInt32(idf["facilitadores_id_fa"]);
                            string f2_provicional = "";
                            //MessageBox.Show(DateTime.Today.ToString("yyyy-MM-dd"));
                            MySqlDataReader f2 = Conexion.ConsultarBD("select * from cursos_tienen_fa where facilitadores_id_fa='" + idprovicional+ "' and ctf_fecha2 >= '" + DateTime.Today.ToString("yyyy-MM-dd") + "'");
                            if (f2.Read())
                            {
                                DateTime d2;
                                d2 = Convert.ToDateTime(f2["ctf_fecha2"]);
                                f2_provicional = d2.ToString("dd-MM-yyyy");
                                MessageBox.Show("No puede eliminar al facilitador " + facilitador.nombre_facilitador + " porque se encuentra asignado a una formación hasta la fecha: "+f2_provicional+" ", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else
                            {
                                MessageBox.Show("No hay fecha2");
                            }
                        }
                        else
                        {
                            // MessageBox.Show("No está");
                            //eliminar primero de afi e inces!!
                            int resultado=0;
                            MySqlDataReader del = Conexion.ConsultarBD("delete from inces_tiene_facilitadores where id_fa_INCE='" + facilitador.id_facilitador + "'");
                            del.Close();
                            MySqlDataReader delA = Conexion.ConsultarBD("delete from afi_tiene_facilitadores id_fa='" + facilitador.id_facilitador + "'");
                            delA.Close();


                            conexion.cerrarconexion();
                            if (conexion.abrirconexion() == true)
                            {

                                
                                resultado = Clases.Facilitadores.EliminarFa(conexion.conexion, facilitador.ci_facilitador);

                                if (resultado > 0)
                                {
                                    Refrescar();
                                }
                                conexion.cerrarconexion();
                            }
                        }
                        idf.Close();
                       

                    }//fin comprobacion eliminar usuario
                }
                else
                {
                    MessageBox.Show("Debe seleccionar un facilitador.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                conexion.cerrarconexion();

            }
        }

        private void btnBuscarFa_Click(object sender, EventArgs e)
        {
            dgvFa.ReadOnly = true;
            try
            {
                if (txtBuscarTodo.Text != "")
                {
                    conexion.cerrarconexion();
                    if (conexion.abrirconexion() == true)
                    {
                        txtbuscar = txtBuscarTodo.Text;

                        buscar(conexion.conexion, txtbuscar);
                        txtBuscarTodo.Text = "Escriba aquí";
                        conexion.cerrarconexion();
                    }
                    

                }else
                {
                    MessageBox.Show("Debe introducir algun dato para buscar.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                }


            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                conexion.cerrarconexion();
            }




        }

        private void dgvFa_MouseClick(object sender, MouseEventArgs e)
        {
            dgvFa.ReadOnly = true;
            if (dgvFa.SelectedRows.Count == 1)
            {
                facilitador.nacionalidad_fa = dgvFa.SelectedRows[0].Cells[0].Value.ToString();
                facilitador.ci_facilitador = dgvFa.SelectedRows[0].Cells[1].Value.ToString();
                facilitador.nombre_facilitador = dgvFa.SelectedRows[0].Cells[2].Value.ToString();
                facilitador.apellido_facilitador = dgvFa.SelectedRows[0].Cells[3].Value.ToString();
               // facilitador.correo_facilitador = dgvFa.SelectedRows[0].Cells[4].Value.ToString();
                facilitador.especialidad_facilitador = dgvFa.SelectedRows[0].Cells[4].Value.ToString();
                //facilitador.tlfn_facilitador = dgvFa.SelectedRows[0].Cells[6].Value.ToString();
                facilitador.ubicacion_facilitador = dgvFa.SelectedRows[0].Cells[5].Value.ToString();

                conexion.cerrarconexion();
                if(conexion.abrirconexion()==true)
                    fa = Clases.Facilitadores.SeleccionarFa(conexion.conexion, facilitador);
                conexion.cerrarconexion();
                facilitador.id_facilitador = fa.id_facilitador;
               // MessageBox.Show(facilitador.id_facilitador.ToString());
            }
        }

        private void btnRefrescar_Click(object sender, EventArgs e)
        {
            Refrescar();
        }

        private void btnModificarFa_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvFa.SelectedRows.Count == 1)
                {
                    if (fa.id_facilitador != 0)
                    {
                        conexion.cerrarconexion();
                        if (conexion.abrirconexion() == true)
                        {
                            Facilitador_Seleccionado.VER = 0;
                           
                            Clases.Facilitador_Seleccionado.id_facilitador = fa.id_facilitador;
                            Clases.Facilitador_Seleccionado.ci_facilitador = fa.ci_facilitador;
                            Clases.Facilitador_Seleccionado.nacionalidad_fa = fa.nacionalidad_fa;
                            Clases.Facilitador_Seleccionado.nombre_facilitador = fa.nombre_facilitador;
                            Clases.Facilitador_Seleccionado.apellido_facilitador = fa.apellido_facilitador;
                            Clases.Facilitador_Seleccionado.tlfn_facilitador = fa.tlfn_facilitador;
                            Clases.Facilitador_Seleccionado.correo_facilitador = fa.correo_facilitador;
                            Clases.Facilitador_Seleccionado.ubicacion_facilitador = fa.ubicacion_facilitador;
                            Clases.Facilitador_Seleccionado.especialidad_facilitador = fa.especialidad_facilitador;
                            Clases.Facilitador_Seleccionado.requerimiento_ince = fa.requerimiento_ince;
                            conexion.cerrarconexion();

                            Modificar_facilitadores mod = new Modificar_facilitadores();
                            mod.ShowDialog();
                            VaciarFa();
                            Refrescar();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Debe seleccionar un registro.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                                        
                }
                else
                {
                    MessageBox.Show("Debe seleccionar un registro.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                conexion.cerrarconexion();
            }
           
        }

        private void btnVerFacilitador_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvFa.SelectedRows.Count == 1)
                {
                    if (fa.id_facilitador != 0)
                    {
                        Facilitador_Seleccionado.VER = 1;
                        // fa = Clases.Facilitadores.SeleccionarFa(conexion.conexion, facilitador);
                        Clases.Facilitador_Seleccionado.id_facilitador = fa.id_facilitador;
                        Clases.Facilitador_Seleccionado.ci_facilitador = fa.ci_facilitador;
                        Clases.Facilitador_Seleccionado.nacionalidad_fa = fa.nacionalidad_fa;
                        Clases.Facilitador_Seleccionado.nombre_facilitador = fa.nombre_facilitador;
                        Clases.Facilitador_Seleccionado.apellido_facilitador = fa.apellido_facilitador;
                        Clases.Facilitador_Seleccionado.tlfn_facilitador = fa.tlfn_facilitador;
                        Clases.Facilitador_Seleccionado.correo_facilitador = fa.correo_facilitador;
                        Clases.Facilitador_Seleccionado.ubicacion_facilitador = fa.ubicacion_facilitador;
                        Clases.Facilitador_Seleccionado.especialidad_facilitador = fa.especialidad_facilitador;
                        Clases.Facilitador_Seleccionado.requerimiento_ince = fa.requerimiento_ince;


                        Modificar_facilitadores mod = new Modificar_facilitadores();
                        mod.ShowDialog();
                        VaciarFa();
                        Refrescar();

                    }
                    else
                    {
                        MessageBox.Show("Debe seleccionar un registro.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }

                }
                else
                {
                    MessageBox.Show("Debe seleccionar un registro.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                conexion.cerrarconexion();
            }
        }
    }
}
