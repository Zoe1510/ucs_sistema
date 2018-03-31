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
    public partial class Ver_insumos : Form
    {
        Clases.conexion_bd conexion = new Clases.conexion_bd();
        Clases.Insumos insumo = new Clases.Insumos();
        int retorno = 0;
        string buscar = "";
        public Ver_insumos()
        {
            InitializeComponent();
            dgvInsumos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            //PARA SELECCIONAR
            dgvInsumos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvInsumos.MultiSelect = false;
            dgvInsumos.ClearSelection();
        }

        private void Ver_insumos_Load(object sender, EventArgs e)
        {
            this.Location = new Point(-5, 0);

            try
            {
                conexion.cerrarconexion();
                if (conexion.abrirconexion() == true)
                {
                    CargarDatosTabla(conexion.conexion, buscar);
                    conexion.cerrarconexion();
                    dgvInsumos.ClearSelection();
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

        private void CargarDatosTabla(MySqlConnection conexion, string buscar)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand(String.Format("SELECT ins_contenido FROM insumos WHERE ins_contenido LIKE ('%{0}%')", buscar), conexion);
                MySqlDataReader reader = cmd.ExecuteReader();

                dgvInsumos.Rows.Clear();
                while (reader.Read())
                {
                    Clases.Insumos ins = new Clases.Insumos();
                    ins.contenido_insumo = reader.GetString(0);

                    dgvInsumos.Rows.Add(ins.contenido_insumo);
                    retorno = 1;
                }
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show(ex.Message);


            }

        }

        private void refrescar()
        {
            dgvInsumos.ReadOnly = true;
            try
            {
                conexion.cerrarconexion();
                if (conexion.abrirconexion() == true)
                {
                    buscar = "";
                    txtBuscarTodo.Text = "Escriba aquí";
                    CargarDatosTabla(conexion.conexion, buscar);
                    dgvInsumos.ClearSelection();

                    conexion.cerrarconexion();
                }
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                conexion.cerrarconexion();
            }
        }

        private void BuscarMatch(MySqlConnection conexion, string buscar)
        {
            int resultado;
            CargarDatosTabla(conexion, buscar);

            resultado = retorno;
            if (resultado == 1)
            {

                //aqui, si se encuentra un resultado compatible, se debe seleccionar la fila que corresponda con el match
                dgvInsumos.CurrentRow.Selected = true;
                retorno = 0;
            }
            else
            {
                MessageBox.Show("No se ha encontrado ninguna concordancia con los datos introducidos", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
                dgvInsumos.ReadOnly = true;
                try
                {
                    if (txtBuscarTodo.Text != "")
                    {
                        conexion.cerrarconexion();
                        if (conexion.abrirconexion() == true)
                        {
                            buscar = txtBuscarTodo.Text;

                            BuscarMatch(conexion.conexion, buscar);
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
            dgvInsumos.ReadOnly = true;
            try
            {
                if (txtBuscarTodo.Text != "")
                {
                    conexion.cerrarconexion();
                    if (conexion.abrirconexion() == true)
                    {
                        buscar = txtBuscarTodo.Text;

                        BuscarMatch(conexion.conexion,buscar);
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
            dgvInsumos.ReadOnly = true;
            if (dgvInsumos.SelectedRows.Count == 1)
            {
                insumo.contenido_insumo = dgvInsumos.SelectedRows[0].Cells[0].Value.ToString();

            }
        }

        private void btnRefrescar_Click(object sender, EventArgs e)
        {
            refrescar();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            Registrar_insumo reg = new Registrar_insumo();
            reg.ShowDialog();
            refrescar();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            dgvInsumos.ReadOnly = true;
            try
            {
                if (dgvInsumos.SelectedRows.Count == 1)
                {
                    conexion.cerrarconexion();
                    if (conexion.abrirconexion() == true)
                    {
                        insumo.id_insumos = Clases.Insumos.ExisteInsumo(conexion.conexion, insumo);
                        conexion.cerrarconexion();
                        if (conexion.abrirconexion() == true)
                        {
                            if (MessageBox.Show("¿Está seguro de eliminar esta opción?", "AVISO", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                            {
                                int resultado = Clases.Insumos.EliminarInsumo(conexion.conexion,insumo);
                                conexion.cerrarconexion();
                                if (resultado > 0)
                                {
                                    refrescar();

                                }else
                                {
                                    MessageBox.Show("Ha ocurrido un error al eliminar.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                if (dgvInsumos.SelectedRows.Count != 0)
                {
                    conexion.cerrarconexion();
                    if (conexion.abrirconexion() == true)
                    {
                        insumo.id_insumos = Clases.Insumos.ExisteInsumo(conexion.conexion, insumo);
                        conexion.cerrarconexion();
                        
                        Clases.Insumos.id_insu = insumo.id_insumos;
                        Clases.Insumos.contenido_insu = insumo.contenido_insumo;


                        Modificar_insumo modI = new Modificar_insumo();
                        modI.ShowDialog();
                        refrescar();
                        Clases.Insumos.id_insu = 0;
                        Clases.Insumos.contenido_insu = "";
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
