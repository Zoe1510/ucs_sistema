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
    public partial class Ver_refrigerios : Form
    {
        public Clases.conexion_bd conexion = new Clases.conexion_bd();
        public Clases.Refrigerios refri = new Clases.Refrigerios();
        int retorno = 0;
        string txtbuscar = "";
        public Ver_refrigerios()
        {
            InitializeComponent();
            dgvRef.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            //PARA SELECCIONAR
            dgvRef.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvRef.MultiSelect = false;
            dgvRef.ClearSelection();
        }

        private void ActualizarTabla(MySqlConnection conexion, string buscar)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand(String.Format("SELECT id_ref, ref_nombre, ref_contenido FROM refrigerios WHERE ref_nombre LIKE ('%{0}%') OR ref_contenido LIKE ('%{0}%')", buscar), conexion);
                MySqlDataReader reader = cmd.ExecuteReader();

                dgvRef.Rows.Clear();
                while (reader.Read())
                {
                    refri.id_ref = reader.GetInt32(0);
                    refri.nombre = reader.GetString(1);
                    refri.contenido_ref = reader.GetString(2);
                    
                    dgvRef.Rows.Add(refri.nombre, refri.contenido_ref);
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
                dgvRef.CurrentRow.Selected = true;
                retorno = 0;
            }
            else
            {
                MessageBox.Show("No se ha encontrado ninguna concordancia con los datos introducidos", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        private void Ver_refrigerios_Load(object sender, EventArgs e) 
        {
            this.Location = new Point(-5, 0);
            try
            {
                if (conexion.abrirconexion() == true)
                {
                    ActualizarTabla(conexion.conexion, txtbuscar);
                    conexion.cerrarconexion();
                    dgvRef.ClearSelection();
                }
                if (Clases.Usuario_logeado.cargo_usuario == "Lider" || Clases.Usuario_logeado.cargo_usuario == "Coordinador")
                {
                    grpbOpciones.Height = 363;
                }else if (Clases.Usuario_logeado.cargo_usuario == "Asistente")
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
                dgvRef.ReadOnly = true;
                try
                {
                    if (txtBuscarTodo.Text != "")
                    {
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

        private void btnBuscarFa_Click(object sender, EventArgs e)
        {
            dgvRef.ReadOnly = true;
            try
            {
                if (txtBuscarTodo.Text != "")
                {
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
            dgvRef.ReadOnly = true;
            if (dgvRef.SelectedRows.Count == 1)
            {
                refri.nombre = dgvRef.SelectedRows[0].Cells[0].Value.ToString();
                refri.contenido_ref= dgvRef.SelectedRows[0].Cells[1].Value.ToString();
            }
        }

        private void btnModificarFa_Click(object sender, EventArgs e)
        {
            dgvRef.ReadOnly = true;
            try
            {
                if (dgvRef.SelectedRows.Count == 1)
                {
                    if (conexion.abrirconexion() == true)
                    {
                        int idRef = Clases.Refrigerios.ExisteRef(conexion.conexion, refri);
                        conexion.cerrarconexion();
                        refri.id_ref = idRef;

                        Clases.Refrigerios.idR = refri.id_ref;
                        Clases.Refrigerios.nombreR = refri.nombre;
                        Clases.Refrigerios.contenidoR = refri.contenido_ref;

                        Modificar_Refrigerio modr = new Modificar_Refrigerio();
                        modr.ShowDialog();
                        if (conexion.abrirconexion() == true)
                        {
                            txtbuscar = "";
                            txtBuscarTodo.Text = "Escriba aquí";
                            ActualizarTabla(conexion.conexion, txtbuscar);
                            dgvRef.ClearSelection();

                            conexion.cerrarconexion();
                            Clases.Refrigerios.idR = 0;
                            Clases.Refrigerios.nombreR = "";
                            Clases.Refrigerios.contenidoR = "";
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



        private void btnEliminarFa_Click(object sender, EventArgs e)
        {
            dgvRef.ReadOnly = true;
            try
            {
                if (dgvRef.SelectedRows.Count == 1)
                {
                    if (conexion.abrirconexion() == true)
                    {
                        int idRef = Clases.Refrigerios.ExisteRef(conexion.conexion, refri);
                        conexion.cerrarconexion();
                        refri.id_ref = idRef;
                        if (conexion.abrirconexion() == true)
                        {
                            if (MessageBox.Show("¿Está seguro de eliminar esta opción?", "AVISO", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                            {
                                int resultado = Clases.Refrigerios.EliminarRef(conexion.conexion, refri);
                                conexion.cerrarconexion();
                                if (resultado > 0)
                                {
                                   
                                    if (conexion.abrirconexion() == true)
                                    {
                                        txtBuscarTodo.Text = "Escriba aquí";
                                        ActualizarTabla(conexion.conexion, txtbuscar);
                                        conexion.cerrarconexion();
                                        dgvRef.ClearSelection();
                                    }
                                   
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

        private void btnRefrescar_Click(object sender, EventArgs e)
        {
            dgvRef.ReadOnly = true;
            try
            {
                if (conexion.abrirconexion() == true)
                {
                    txtbuscar = "";
                    txtBuscarTodo.Text = "Escriba aquí";
                    ActualizarTabla(conexion.conexion, txtbuscar);
                    dgvRef.ClearSelection();

                    conexion.cerrarconexion();
                }
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                conexion.cerrarconexion();
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            Registrar_refrigerio reg = new Registrar_refrigerio();
            reg.ShowDialog();
            if (conexion.abrirconexion() == true)
            {
                txtBuscarTodo.Text = "Escriba aquí";
                ActualizarTabla(conexion.conexion, txtbuscar);
                conexion.cerrarconexion();
                dgvRef.ClearSelection();
            }
        }
    }
}
