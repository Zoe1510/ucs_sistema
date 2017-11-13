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
    public partial class Buscar_cliente : Form
    {
        public Clases.Clientes cliente = new Clases.Clientes();
        public Clases.conexion_bd conexion = new Clases.conexion_bd();
        public Clases.Clientes areaE = new Clases.Clientes();
       

        public Clases.Clientes cli { get; set; }
      
        public Buscar_cliente()
        {
            InitializeComponent();
            dgvAreasEmpresa.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            //PARA SELECCIONAR
            dgvAreasEmpresa.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvAreasEmpresa.MultiSelect = false;
            dgvAreasEmpresa.ClearSelection();
        }

        private void Buscar_cliente_Load(object sender, EventArgs e)
        {
            string nombreempresa = "";
            //this.Location = new Point(-150, 0);
            try
            {
                conexion.cerrarconexion();
                if (conexion.abrirconexion() == true)
                {
                    LlenarDGV(conexion.conexion, nombreempresa);
                    conexion.cerrarconexion();
                    //ninguna seleccion por defecto del datagridview
                    dgvAreasEmpresa.ClearSelection();
                }
                //llenar el combobox con las empresas registradas
                string nombre = "";
                llenarcombo(nombre);

                //opciones segun el tipo de usuario
                if (Clases.Usuario_logeado.cargo_usuario == "Lider")
                {
                    grpbOpciones.Visible = true;
                    grpbOpciones.Height = 387;
                }
                else if (Clases.Usuario_logeado.cargo_usuario == "Coordinador")
                {
                    grpbOpciones.Visible = true;
                    grpbOpciones.Height = 270;
                }
                else if (Clases.Usuario_logeado.cargo_usuario == "Asistente")
                {
                    grpbOpciones.Visible = true;
                    grpbOpciones.Height = 97;
                }

            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                conexion.cerrarconexion();
            }
           
        }

        private void dgvAreasEmpresa_MouseClick(object sender, MouseEventArgs e)
        {
            if (dgvAreasEmpresa.SelectedRows.Count == 1)
            {
                cliente.nombre_empresa = dgvAreasEmpresa.SelectedRows[0].Cells[0].Value.ToString();
                cliente.nombre_areaEmpresa = dgvAreasEmpresa.SelectedRows[0].Cells[1].Value.ToString();
                cliente.nombre_contacto = dgvAreasEmpresa.SelectedRows[0].Cells[2].Value.ToString();
                cliente.tlfn_cliente = dgvAreasEmpresa.SelectedRows[0].Cells[3].Value.ToString();
                cliente.correo_cliente = dgvAreasEmpresa.SelectedRows[0].Cells[4].Value.ToString();
               
            }
        }
        private void llenarcombo(string nombre)
        {
            //llenar el combobox con las empresas registradas:
            cmbxEmpresa.ValueMember = "id_clientes";
            cmbxEmpresa.DisplayMember = "nombre_empresa";
            cmbxEmpresa.DataSource = Clases.Paneles.LlenarCombobox(nombre);
            cmbxEmpresa.SelectedIndex = -1;
            cmbxEmpresa.Text = "    Seleccione";
            

        }

        private void LlenarDGV(MySqlConnection conexion, string nombre_empresa)
        {
            try
            {
                
                MySqlCommand comando = new MySqlCommand(String.Format("SELECT id_clientes, id_area, nombre_empresa, nombre_area, nombre_contacto, tlfn_contacto, correo_contacto, fee_empresa FROM clientes inner join areas on clientes.id_clientes=areas.id_cliente1 WHERE nombre_empresa LIKE ('%{0}%')",nombre_empresa), conexion);
                MySqlDataReader reader = comando.ExecuteReader();

                dgvAreasEmpresa.Rows.Clear();
                while (reader.Read())
                {
                    cliente.id_cliente = reader.GetInt32(0);
                    cliente.id_area = reader.GetInt32(1);
                    cliente.nombre_empresa = reader.GetString(2);
                    cliente.nombre_areaEmpresa = reader.GetString(3);
                    cliente.nombre_contacto = reader.GetString(4);
                    cliente.tlfn_cliente = reader.GetString(5);
                    cliente.correo_cliente = reader.GetString(6);
                    cliente.fee_empresa = reader.GetInt32(7);
                    string FEE = cliente.fee_empresa.ToString(); ;

                    switch (FEE)
                    {
                        case "1":
                            FEE = "Sí";
                            break;
                        case "0":
                            FEE = "No";
                            break;
                    }

                    dgvAreasEmpresa.Rows.Add(cliente.nombre_empresa, cliente.nombre_areaEmpresa, cliente.nombre_contacto, cliente.tlfn_cliente, cliente.correo_cliente, FEE);
                    
                }
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void abrirFormModificar()
        {
            try
            {
                if (dgvAreasEmpresa.SelectedRows.Count == 1)
                {
                    conexion.cerrarconexion();
                    if (conexion.abrirconexion() == true)
                    {
                        cli = Clases.Clientes.SeleccionarCliente(conexion.conexion, cliente);
                        Clases.Cliente_seleccionado.id_cliente = cli.id_cliente;
                        Clases.Cliente_seleccionado.id_area = cli.id_area;
                        Clases.Cliente_seleccionado.nombre_empresa = cli.nombre_empresa;
                        Clases.Cliente_seleccionado.nombre_areaEmpresa = cli.nombre_areaEmpresa;
                        Clases.Cliente_seleccionado.nombre_contacto = cli.nombre_contacto;
                        Clases.Cliente_seleccionado.tlfn_cliente = cli.tlfn_cliente;
                        Clases.Cliente_seleccionado.correo_cliente = cli.correo_cliente;
                        conexion.cerrarconexion();
                        Modificar_contacto_area mod_contacto = new Modificar_contacto_area();
                        mod_contacto.ShowDialog();

                        refrescar();
                        Clases.Paneles.VaciarClienteSeleccionado();
                        dgvAreasEmpresa.ClearSelection();
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
            }
        }
        private void btnModificarContacto_Click(object sender, EventArgs e)
        {
            Clases.Empresa.ModificarArea = 0;
            abrirFormModificar();
        }

        private void refrescar()
        {
            string nombre_empresa = "";
            try
            {
                conexion.cerrarconexion();
                if (conexion.abrirconexion() == true)
                {
                    LlenarDGV(conexion.conexion, nombre_empresa);
                    conexion.cerrarconexion();
                    //ninguna seleccion por defecto del datagridview
                    dgvAreasEmpresa.ClearSelection();
                }
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnVerTodo_Click(object sender, EventArgs e)
        {
            refrescar();
        }

        private void btnBuscarAreas_Click(object sender, EventArgs e)
        {
            try
            {
                if(cmbxEmpresa.SelectedIndex != -1)
                {
                    errorProviderSelecEmpresa.SetError(cmbxEmpresa, "");
                    conexion.cerrarconexion();
                    if (conexion.abrirconexion() == true)
                    {
                        if(id_cli > 0)
                        {
                            string nombre_e = Clases.Clientes.seleccionarNombreEmpresa(conexion.conexion, id_cli);
                            conexion.cerrarconexion();

                            if (nombre_e != "")
                            {
                                if (conexion.abrirconexion() == true)
                                {
                                   
                                    areaE = Clases.Paneles.AreaExiste(conexion.conexion, cliente);
                                    conexion.cerrarconexion();
                                    if (areaE.id_area > 0)//si existe areas por mostrar ( no importa cuál), llena el datagridview
                                    {
                                        if (conexion.abrirconexion() == true)
                                        {
                                            LlenarDGV(conexion.conexion, nombre_e);
                                            dgvAreasEmpresa.CurrentRow.Selected = true;

                                        }
                                        
                                    }
                                    else//si no existen areas de una empresa, refrescar, mensaje y quitar seleccion del combobox
                                    {
                                        MessageBox.Show("Esta empresa no posee ningún área", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        refrescar();
                                        if (conexion.abrirconexion() == true)
                                        {
                                            string nombreempresa = "";
                                            llenarcombo(nombreempresa);
                                            conexion.cerrarconexion();
                                            //ninguna seleccion por defecto del datagridview
                                            dgvAreasEmpresa.ClearSelection();
                                        }
                                    }
                                   
                                }

                            }

                        }


                    }
                }
                else
                {
                    errorProviderSelecEmpresa.SetError(cmbxEmpresa, "Debe seleccionar una empresa.");
                    cmbxEmpresa.Focus();
                }

               

            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        int id_cli = 0;
        private void cmbxEmpresa_SelectionChangeCommitted(object sender, EventArgs e)
        {
            id_cli = Convert.ToInt32(cmbxEmpresa.SelectedValue);
            cliente.id_cliente = id_cli;
            
        }

        private void seleccionarIdArea()
        {
            try
            {
                conexion.cerrarconexion();
                if (conexion.abrirconexion() == true)
                {
                    Clases.Clientes areae = new Clases.Clientes();
                    areae = Clases.Clientes.SeleccionarCliente(conexion.conexion, cliente);
                    areae.id_area = cliente.id_area;
                    areae.id_cliente = cliente.id_cliente;
                    conexion.cerrarconexion();
                }
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnEliminarArea_Click(object sender, EventArgs e)
        {
            try
            {
                
                if(dgvAreasEmpresa.SelectedRows.Count == 1)
                {
                    seleccionarIdArea();
                    conexion.cerrarconexion();
                    if (conexion.abrirconexion() == true)
                    {
                        
                        int resultado = Clases.Clientes.EliminarArea(conexion.conexion, cliente);
                        conexion.cerrarconexion();
                        refrescar();
                       
                   }

                    

                }
                else
                {
                    MessageBox.Show("Debe seleccionar un área de la lista.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

                conexion.cerrarconexion();
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnEliminarEmpresa_Click(object sender, EventArgs e)
        {
            try
            {
                conexion.cerrarconexion();
                if (cmbxEmpresa.SelectedIndex != -1)
                {
                    errorProviderSelecEmpresa.SetError(cmbxEmpresa, "");
                    if (MessageBox.Show("Si elimina esta empresa perderá todos sus contactos. ¿Desea continuar?", "ALERTA", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        if (conexion.abrirconexion() == true)
                        {
                            int resultado = Clases.Clientes.EliminarEmpresa(conexion.conexion, cliente);
                            conexion.cerrarconexion();
                            refrescar();
                            if (conexion.abrirconexion() == true)
                            {
                                string nombreempresa = "";
                                llenarcombo(nombreempresa);
                                conexion.cerrarconexion();
                            //ninguna seleccion por defecto del datagridview
                                dgvAreasEmpresa.ClearSelection();
                            }
                        }
                    }
                    else
                    {
                        dgvAreasEmpresa.ClearSelection();
                    }

                }
                else
                {
                    errorProviderSelecEmpresa.SetError(cmbxEmpresa, "Debe seleccionar una empresa.");
                    cmbxEmpresa.Focus();
                }

                conexion.cerrarconexion();
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cmbxEmpresa_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnModificarEmpresa_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbxEmpresa.SelectedIndex == -1)
                {
                    errorProviderSelecEmpresa.SetError(cmbxEmpresa, "Debe seleccionar una empresa.");
                    cmbxEmpresa.Focus();

                }else
                {
                    errorProviderSelecEmpresa.SetError(cmbxEmpresa, "");
                    conexion.cerrarconexion();
                    if (conexion.abrirconexion() == true)
                    {
                        string nombre_e = Clases.Clientes.seleccionarNombreEmpresa(conexion.conexion, cliente.id_cliente);
                        conexion.cerrarconexion();
                        if (conexion.abrirconexion() == true)
                        {
                            int fee= Clases.Clientes.seleccionarFeeEmpresa(conexion.conexion, cliente.id_cliente);
                            conexion.cerrarconexion();
                            Clases.Cliente_seleccionado.id_cliente = cliente.id_cliente;
                            Clases.Cliente_seleccionado.fee_empresa = fee;
                            Clases.Cliente_seleccionado.nombre_empresa = nombre_e;
                            Modificar_empresa ModE = new Modificar_empresa();
                            ModE.ShowDialog();
                            cmbxEmpresa.SelectedIndex = -1;
                            refrescar();
                            Clases.Paneles.VaciarClienteSeleccionado();
                            dgvAreasEmpresa.ClearSelection();
                        }
                        
                        //cli2.id_cliente = 0;
                        //cli2.fee_empresa = 2;
                        //cli2.nombre_empresa="";
                        
                    }
                    
                }

            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnModificarArea_Click(object sender, EventArgs e)
        {
            Clases.Empresa.ModificarArea = 1;
            abrirFormModificar();
           
        }
    }
}
