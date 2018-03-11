using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UCS_NODO_FGC
{
    public partial class Modificar_empresa : Form
    {
        public Clases.conexion_bd conexion = new Clases.conexion_bd();
        public Clases.Clientes cliente = new Clases.Clientes();
        public Clases.Clientes clienteexiste = new Clases.Clientes();
        public Clases.Empresa empresa = new Clases.Empresa();
        public Modificar_empresa()
        {
            InitializeComponent();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Desea cancelar la operación?", "",
       MessageBoxButtons.YesNo, MessageBoxIcon.Question)
       == DialogResult.Yes)
            {
                Clases.Paneles.VaciarClienteSeleccionado();
                this.Close();
            }
        }

        private void Modificar_empresa_Load(object sender, EventArgs e)
        {
          
            if(Clases.Cliente_seleccionado.id_cliente != 0)
            {
               
                cliente.id_cliente = Clases.Cliente_seleccionado.id_cliente;
                txtNombreEmpresa.Text = Clases.Cliente_seleccionado.nombre_empresa;
                if(Clases.Cliente_seleccionado.fee_empresa == 0)//no posee fee
                {
                    cmbxFee.SelectedIndex = 1;
                }else if(Clases.Cliente_seleccionado.fee_empresa == 1)//si posee fee
                {
                    cmbxFee.SelectedIndex = 0;
                }
                
            }else
            {
                txtNombreEmpresa.Text = "Nombre empresa";
                cmbxFee.SelectedIndex = -1;
            }
        }

        private void txtNombreEmpresa_Validating(object sender, CancelEventArgs e)
        {
            if (txtNombreEmpresa.Text == "" || txtNombreEmpresa.Text == "Nombre empresa")
            {
                errorProviderEmpresa.SetError(txtNombreEmpresa, "Debe proporcionar un nombre válido.");
                txtNombreEmpresa.Focus();
            }
            else
            {
                errorProviderEmpresa.SetError(txtNombreEmpresa, "");
            }
        }

        private void cmbxFee_Validating(object sender, CancelEventArgs e)
        {
            if (cmbxFee.SelectedIndex == -1)
            {
                errorProviderFEE.SetError(cmbxFee, "Debe seleccionar una de las opciones.");
                cmbxFee.Focus();
            }else
            {
                errorProviderFEE.SetError(cmbxFee, "");
            }
        }

        private void Modificar()
        {
            try
            {
                empresa.id_clientes = cliente.id_cliente;
                empresa.nombre_empresa = cliente.nombre_empresa;
                empresa.fee = cliente.fee_empresa;
                conexion.cerrarconexion();
                if (conexion.abrirconexion() == true)
                {
                    int modificacion = Clases.Paneles.ModificarEmpresa(conexion.conexion, empresa);
                    if (modificacion > 0)
                    {
                        MessageBox.Show("Los datos han sido actualizados correctamente.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtNombreEmpresa.Text = "Nombre empresa";
                        cmbxFee.SelectedIndex = -1;
                        Clases.Paneles.VaciarClienteSeleccionado();
                        this.Close();
                    }
                }
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
        private void btnModificarEmpresa_Click(object sender, EventArgs e)
        {
            
            cliente.nombre_empresa = txtNombreEmpresa.Text;
            
            try
            {
                if(txtNombreEmpresa.Text == Clases.Cliente_seleccionado.nombre_empresa && cliente.fee_empresa == Clases.Cliente_seleccionado.fee_empresa)
                {
                    MessageBox.Show("No hay modificaciones.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtNombreEmpresa.Text = "Nombre empresa";
                    cmbxFee.SelectedIndex = -1;
                    Clases.Paneles.VaciarClienteSeleccionado();
                    this.Close();
                }else
                {
                    conexion.cerrarconexion();
                    if (conexion.abrirconexion() == true)
                    {
                        clienteexiste = Clases.Clientes.ClienteExiste(conexion.conexion, cliente);
                        conexion.cerrarconexion();
                    }
                        
                    if (txtNombreEmpresa.Text == Clases.Cliente_seleccionado.nombre_empresa && cliente.fee_empresa != Clases.Cliente_seleccionado.fee_empresa)
                    {
                        Modificar();
                    }else if (txtNombreEmpresa.Text != Clases.Cliente_seleccionado.nombre_empresa && cliente.fee_empresa == Clases.Cliente_seleccionado.fee_empresa)
                    {
                        if (cliente.nombre_empresa == clienteexiste.nombre_empresa)//si es iGUAL AL txt, no hay cambios en el nombre. 
                        {
                            MessageBox.Show("Ya existe una empresa registrada con este nombre.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            txtNombreEmpresa.Text = Clases.Cliente_seleccionado.nombre_empresa;
                            txtNombreEmpresa.Focus();

                        }else
                        {
                            Modificar();
                        }
                        
                    }
                    else if (txtNombreEmpresa.Text != Clases.Cliente_seleccionado.nombre_empresa && cliente.fee_empresa != Clases.Cliente_seleccionado.fee_empresa)
                    {
                        if (cliente.nombre_empresa == clienteexiste.nombre_empresa)//si es iGUAL AL txt, no hay cambios en el nombre. 
                        {
                            MessageBox.Show("Ya existe una empresa registrada con este nombre.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            txtNombreEmpresa.Text = Clases.Cliente_seleccionado.nombre_empresa;
                            txtNombreEmpresa.Focus();

                        }
                        else
                        {
                            Modificar();
                        }
                    }
                   
                    //if (clienteexiste.nombre_empresa == null)//no se retornó nada, se puede aceptar modificacion
                    //{
                    //    Modificar();

                    //}
                    //else if (cliente.nombre_empresa == clienteexiste.nombre_empresa && clienteexiste.fee_empresa == Clases.Cliente_seleccionado.fee_empresa)//si es iGUAL AL txt, no hay cambios en el nombre. 
                    //{
                    //    MessageBox.Show("No hay modificaciones.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //    txtNombreEmpresa.Text = "Nombre empresa";
                    //    cmbxFee.SelectedIndex = -1;
                    //    Clases.Paneles.VaciarClienteSeleccionado();
                    //    this.Close();


                    //}
                    //else if (Clases.Cliente_seleccionado.nombre_empresa == clienteexiste.nombre_empresa && clienteexiste.fee_empresa != Clases.Cliente_seleccionado.fee_empresa)
                    //{
                    //    Modificar();
                    //}
                    //else if (Clases.Cliente_seleccionado.nombre_empresa != clienteexiste.nombre_empresa)//si es distinto del txt o del cmbx, es que una empresa ya tiene ese nombre
                    //{

                    //    MessageBox.Show("Ya existe una empresa registrada con este nombre.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //    txtNombreEmpresa.Text = Clases.Cliente_seleccionado.nombre_empresa;
                    //    txtNombreEmpresa.Focus();
                    //}



                }
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cmbxFee_SelectionChangeCommitted(object sender, EventArgs e)
        {
           int fee = cmbxFee.SelectedIndex;
            switch (fee)
            {
                case 0:
                    fee = 1; //si 1, la empresa está con FEE
                    break;
                case 1:
                    fee = 0; //La empresa no posee FEE
                    break;
            }
            cliente.fee_empresa = fee;
            
        }

        private void txtNombreEmpresa_KeyPress(object sender, KeyPressEventArgs e)
        {
            Clases.Paneles.sololetras(e);
            if (txtNombreEmpresa.Text.Length == 0)
            {
                e.KeyChar = e.KeyChar.ToString().ToUpper().ToCharArray()[0];
            }
            else if (txtNombreEmpresa.Text.Length > 0)
            {
                e.KeyChar = e.KeyChar.ToString().ToLower().ToCharArray()[0];
            }
        }
    }
}
