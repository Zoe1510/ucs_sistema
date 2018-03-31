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

namespace UCS_NODO_FGC
{
    public partial class Ver_publicidad : Form
    {
        public Clases.conexion_bd conexion = new Clases.conexion_bd();
        public Clases.Difusion dif = new Clases.Difusion();
        int retorno = 0;
        string txtbuscar = "";
        public Ver_publicidad()
        {
            InitializeComponent();
            dgvDif.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            //PARA SELECCIONAR
            dgvDif.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDif.MultiSelect = false;
            dgvDif.ClearSelection();

        }

        private void refrescar()
        {
            dgvDif.ReadOnly = true;
            try
            {
                conexion.cerrarconexion();
                if (conexion.abrirconexion() == true)
                {
                    txtbuscar = "";
                    txtBuscarTodo.Text = "Escriba aquí";
                    ActualizarTabla(conexion.conexion, txtbuscar);
                    dgvDif.ClearSelection();

                    conexion.cerrarconexion();
                }
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                conexion.cerrarconexion();
            }
        }
        private void ActualizarTabla(MySqlConnection conexion, string buscar)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand(String.Format("SELECT id_difusion, dif_contenido FROM difusion WHERE dif_contenido LIKE ('%{0}%')", buscar), conexion);
                MySqlDataReader reader = cmd.ExecuteReader();

                dgvDif.Rows.Clear();
                while (reader.Read())
                {
                    dif.id_dif = reader.GetInt32(0);
                    dif.contenido_dif = reader.GetString(1);
                    

                    dgvDif.Rows.Add(dif.contenido_dif);
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
            ActualizarTabla(conexion, buscar);

            resultado = retorno;
            if (resultado == 1)
            {

                //aqui, si se encuentra un resultado compatible, se debe seleccionar la fila que corresponda con el match
                dgvDif.CurrentRow.Selected = true;
                retorno = 0;
            }
            else
            {
                MessageBox.Show("No se ha encontrado ninguna concordancia con los datos introducidos", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        private void Ver_publicidad_Load(object sender, EventArgs e)
        {
            this.Location = new Point(-5, 0);
            try
            {
                conexion.cerrarconexion();
                if (conexion.abrirconexion() == true)
                {
                    ActualizarTabla(conexion.conexion, txtbuscar);
                    conexion.cerrarconexion();
                    dgvDif.ClearSelection();
                }
                if (Clases.Usuario_logeado.cargo_usuario == "Lider" || Clases.Usuario_logeado.cargo_usuario == "Coordinador")
                {
                    grpbOpciones.Height = 363;
                }
                else if (Clases.Usuario_logeado.cargo_usuario == "Asistente")
                {
                    grpbOpciones.Height = 126;
                }
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                conexion.cerrarconexion();
            }
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
                dgvDif.ReadOnly = true;
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

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            dgvDif.ReadOnly = true;
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

        private void dgvRef_MouseClick(object sender, MouseEventArgs e)
        {
            dgvDif.ReadOnly = true;
            if (dgvDif.SelectedRows.Count == 1)
            {
                dif.contenido_dif = dgvDif.SelectedRows[0].Cells[0].Value.ToString();
                
            }
        }

        private void btnRefrescar_Click(object sender, EventArgs e)
        {
            refrescar();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            Registrar_publicidad reg = new Registrar_publicidad();
            reg.ShowDialog();
            refrescar();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            dgvDif.ReadOnly = true;
            try
            {
                if (dgvDif.SelectedRows.Count == 1)
                {
                    conexion.cerrarconexion();
                    if (conexion.abrirconexion() == true)
                    {
                        int iddif = Clases.Difusion.ExisteDif(conexion.conexion, dif);
                        conexion.cerrarconexion();
                       dif.id_dif = iddif;
                        conexion.cerrarconexion();
                        if (conexion.abrirconexion() == true)
                        {
                            if (MessageBox.Show("¿Está seguro de eliminar esta opción?", "AVISO", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                            {
                                int resultado = Clases.Difusion.EliminarDif(conexion.conexion, dif);
                                conexion.cerrarconexion();
                                if (resultado > 0)
                                {
                                      refrescar();
                                    
                                }
                            }
                        }
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

        private void btnModificar_Click(object sender, EventArgs e)
        {
            //dgvDif.ReadOnly = true;
            try
            {
               
                if (dgvDif.SelectedRows.Count !=0)
                {
                    conexion.cerrarconexion();
                    if (conexion.abrirconexion() == true)
                    {
                        int iddif = Clases.Difusion.ExisteDif(conexion.conexion, dif);
                        conexion.cerrarconexion();
                        dif.id_dif = iddif;

                        Clases.Difusion.idD = dif.id_dif;
                        Clases.Difusion.contenido = dif.contenido_dif;


                        Modificar_difusion modf = new Modificar_difusion();
                        modf.ShowDialog();
                        refrescar();
                        Clases.Difusion.idD =0;
                        Clases.Difusion.contenido ="";
                    }
                }else
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
