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
    public partial class Modificar_contacto_area : Form
    {
        public Clases.Clientes cliente = new Clases.Clientes();
        public Clases.conexion_bd conexion = new Clases.conexion_bd();
        public Modificar_contacto_area()
        {
            InitializeComponent();
        }

        private void Modificar_contacto_area_Load(object sender, EventArgs e)
        {
           
            txtNombreEmpresa.Text = Clases.Cliente_seleccionado.nombre_empresa;
            txtNombreArea.Text = Clases.Cliente_seleccionado.nombre_areaEmpresa;
            txtNombreContactoArea.Text = Clases.Cliente_seleccionado.nombre_contacto;
            txtTelefonoCliArea.Text = Clases.Cliente_seleccionado.tlfn_cliente;
            txtCorreoCliArea.Text = Clases.Cliente_seleccionado.correo_cliente;
            if (Clases.Empresa.ModificarArea == 1)
            {
                txtNombreArea.Enabled = true;
                txtNombreArea.Cursor = Cursors.IBeam;
                groupBox2.Enabled = false;
                this.Text = "Modificar área";
                txtNombreContactoArea.ReadOnly = false;
                txtTelefonoCliArea.ReadOnly = false;
                txtCorreoCliArea.ReadOnly = false;
                btnCancelar.Enabled = true;
                btnModificarContacto.Text = "Actualizar";

            }
            else if (Clases.Empresa.ModificarArea == 0)
            {
                txtNombreArea.Enabled = false;
                txtNombreArea.Cursor = Cursors.No;
                groupBox2.Enabled = true;
                this.Text = "Modificar contacto de área";
                txtNombreContactoArea.ReadOnly =false;
                txtTelefonoCliArea.ReadOnly = false;
                txtCorreoCliArea.ReadOnly = false;
                btnCancelar.Enabled = true;
                btnModificarContacto.Text = "Actualizar";
            }
            else if (Clases.Empresa.ModificarArea == 2) //esto es para el asistente y VER INFORMACION DE CONTACTO
            {
                this.Text = "Ver información de contacto";
                groupBox2.Enabled = true;
                txtNombreArea.Enabled = false;
                txtNombreArea.Cursor = Cursors.No;
                txtNombreContactoArea.ReadOnly = true;
                txtTelefonoCliArea.ReadOnly = true;
                txtCorreoCliArea.ReadOnly = true;

                btnCancelar.Enabled = false;
                btnModificarContacto.Text = "Aceptar";
            }
        }

        private void txtTelefonoCli_KeyDown(object sender, KeyEventArgs e)
        {
            while (txtTelefonoCliArea.Text == "Teléfono o celular")
            {
                txtTelefonoCliArea.Text = "";
            }
        }
        private void txtTelefonoCli_KeyPress(object sender, KeyPressEventArgs e)
        {
            Clases.Paneles.solonumeros(e);
        }
        private void txtTelefonoCli_Leave(object sender, EventArgs e)
        {
            if (txtTelefonoCliArea.Text == "")
            {
                txtTelefonoCliArea.Text = "Teléfono o celular";
            }
        }
        private void txtTelefonoCli_Click(object sender, EventArgs e)
        {

            if (txtTelefonoCliArea.Text != "Teléfono o celular")
            {

            }
            else
            {
                txtTelefonoCliArea.Text = "";
            }
        }
        private void txtTelefonoCliArea_Validating(object sender, CancelEventArgs e)
        {
            if(Clases.Paneles.comprobarFormatoTlfn(e, txtTelefonoCliArea.Text) == false)
            {
                errorProviderTlfn.SetError(txtTelefonoCliArea, "Debe proporcionar un número válido.");
                txtTelefonoCliArea.Focus();
            }else
            {
                errorProviderTlfn.SetError(txtTelefonoCliArea, "");
            }
            
        }

        private void txtNombreContacto_KeyPress(object sender, KeyPressEventArgs e)
        {
            Clases.Paneles.sololetras(e);
            if (txtNombreContactoArea.Text.Length == 0)
            {
                e.KeyChar = e.KeyChar.ToString().ToUpper().ToCharArray()[0];
            }
            else if (txtNombreContactoArea.Text.Length > 0)
            {
                e.KeyChar = e.KeyChar.ToString().ToLower().ToCharArray()[0];
            }
        }
        private void txtNombreContacto_KeyDown(object sender, KeyEventArgs e)
        {
            while (txtNombreContactoArea.Text == "Persona contacto del área")
            {
                txtNombreContactoArea.Text = "";

            }
        }
        private void txtNombreContacto_Leave(object sender, EventArgs e)
        {
            if (txtNombreContactoArea.Text == "")
            {
                txtNombreContactoArea.Text = "Persona contacto del área";
            }
        }
        private void txtNombreContacto_Click(object sender, EventArgs e)
        {

            if (txtNombreContactoArea.Text != "")
            {

            }
            else
            {
                txtNombreContactoArea.Text = "";
            }
        }


        private void txtCorreoCliArea_Validating(object sender, CancelEventArgs e)
        {
            if (Clases.Paneles.ComprobarFormatoEmail(txtCorreoCliArea.Text) == false)
            {
                errorProviderCorreo.SetError(txtCorreoCliArea, "Debe proporcionar un correo válido.");
                txtCorreoCliArea.Focus();
            }
        }
        private void txtCorreoCliArea_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Clases.Empresa.ModificarArea == 0)
            {
                if ((int)e.KeyChar == (int)Keys.Enter)
                {
                    Actualizar();
                }
            }
            
        }
        private void txtCorreoCli_Leave(object sender, EventArgs e)
        {
            if (txtCorreoCliArea.Text == "")
            {
                txtCorreoCliArea.Text = "correo@ejemplo.com";
            }
        }
        private void txtCorreoCli_KeyDown(object sender, KeyEventArgs e)
        {
            while (txtCorreoCliArea.Text == "correo@ejemplo.com")
            {
                txtCorreoCliArea.Text = "";
            }
        }
        private void txtCorreoCli_Click(object sender, EventArgs e)
        {

            if (txtCorreoCliArea.Text != "")
            {

            }
            else
            {
                txtCorreoCliArea.Text = "";
            }
        }


        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Desea cancelar la operación?", "",
       MessageBoxButtons.YesNo, MessageBoxIcon.Question)
       == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void btnModificarContacto_Click(object sender, EventArgs e)
        {
         
            if(Clases.Empresa.ModificarArea == 1)
            {
                try
                {
                    if (txtNombreEmpresa.Text == "" || txtNombreEmpresa.Text == "Nombre empresa")
                    {
                        errorProviderNCliente.SetError(txtNombreEmpresa, "Debe proporcionar el nombre de la empresa.");
                        txtNombreEmpresa.Focus();

                    }
                    else if (txtNombreArea.Text == "" || txtNombreArea.Text == "Nombre del área")
                    {
                        errorProviderNCliente.SetError(txtNombreEmpresa, "");
                        errorProviderNArea.SetError(txtNombreArea, "Debe propocionar el nombre del área.");
                        txtNombreArea.Focus();

                    }
                    else if (txtNombreContactoArea.Text == "" || txtNombreContactoArea.Text == "Persona contacto del área")
                    {
                        errorProviderNArea.SetError(txtNombreArea, "");
                        errorProviderNContacto.SetError(txtNombreContactoArea, "Debe proporcionar un contácto en el área.");
                        txtNombreContactoArea.Focus();
                    }
                    else
                    {
                        errorProviderNContacto.SetError(txtNombreContactoArea, "");
                        conexion.cerrarconexion();
                        if (conexion.abrirconexion() == true)
                        {
                            cliente.id_area = Clases.Cliente_seleccionado.id_area; 
                            cliente.id_cliente = Clases.Cliente_seleccionado.id_cliente;
                            cliente.nombre_empresa = txtNombreEmpresa.Text;
                            cliente.nombre_areaEmpresa = txtNombreArea.Text;
                            cliente.nombre_contacto = txtNombreContactoArea.Text;
                            cliente.tlfn_cliente = txtTelefonoCliArea.Text;
                            cliente.correo_cliente = txtCorreoCliArea.Text;

                            int resultado;
                            resultado = Clases.Clientes.ActualizarNombreArea(conexion.conexion, cliente);
                            conexion.cerrarconexion();

                            if (resultado != 0)
                            {
                                MessageBox.Show("Los datos han sido modificados exitosamente.", "AVISO", MessageBoxButtons.OK);
                                Clases.Paneles.VaciarClienteSeleccionado();
                                Clases.Empresa.ModificarArea = 0;
                                this.Close();
                            }
                            else
                            {
                                MessageBox.Show("Ha ocurrido un error en la base de datos", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                Clases.Paneles.VaciarClienteSeleccionado();
                            }
                        }
                    }

                }
                catch (MySql.Data.MySqlClient.MySqlException ex)
                {
                    MessageBox.Show(ex.Message);
                    conexion.cerrarconexion();
                }

            }else if(Clases.Empresa.ModificarArea == 0)
            {
                Actualizar();

            }else if (Clases.Empresa.ModificarArea == 2)
            {
                this.Close();
            }
        }

        private void Actualizar()
        {
            try
            {
                if (txtNombreEmpresa.Text == "" || txtNombreEmpresa.Text == "Nombre empresa")
                {
                    errorProviderNCliente.SetError(txtNombreEmpresa, "Debe proporcionar el nombre de la empresa.");
                    txtNombreEmpresa.Focus();

                }
                else if (txtNombreArea.Text == "" || txtNombreArea.Text == "Nombre del área")
                {
                    errorProviderNCliente.SetError(txtNombreEmpresa, "");
                    errorProviderNArea.SetError(txtNombreArea, "Debe propocionar el nombre del área.");
                    txtNombreArea.Focus();

                }
                else if (txtNombreContactoArea.Text == "" || txtNombreContactoArea.Text == "Persona contacto del área")
                {
                    errorProviderNArea.SetError(txtNombreArea, "");
                    errorProviderNContacto.SetError(txtNombreContactoArea, "Debe proporcionar un contácto en el área.");
                    txtNombreContactoArea.Focus();
                }
                else if (txtTelefonoCliArea.Text == "" || txtTelefonoCliArea.TextLength < 11)
                {
                    errorProviderNContacto.SetError(txtNombreContactoArea, "");
                    errorProviderTlfn.SetError(txtTelefonoCliArea, "Debe proporcionar un número de teléfono válido.");
                    txtTelefonoCliArea.Focus();
                }
                else if (txtCorreoCliArea.Text == "correo@ejemplo.com")
                {
                    errorProviderTlfn.SetError(txtTelefonoCliArea, "");
                    errorProviderCorreo.SetError(txtCorreoCliArea, "Debe proporcionar un correo electrónico válido.");
                    txtCorreoCliArea.Focus();
                }
                else
                {
                    errorProviderCorreo.SetError(txtCorreoCliArea, "");
                    conexion.cerrarconexion();
                    if (conexion.abrirconexion() == true)
                    {
                        cliente.id_cliente = Clases.Cliente_seleccionado.id_cliente;
                        cliente.nombre_empresa = txtNombreEmpresa.Text;
                        cliente.nombre_areaEmpresa = txtNombreArea.Text;
                        cliente.nombre_contacto = txtNombreContactoArea.Text;
                        cliente.tlfn_cliente = txtTelefonoCliArea.Text;
                        cliente.correo_cliente = txtCorreoCliArea.Text;

                        int resultado;
                        resultado = Clases.Clientes.ActualizarContactoArea(conexion.conexion, cliente);
                        conexion.cerrarconexion();

                        if (resultado != 0)
                        {
                            MessageBox.Show("Los datos han sido modificados exitosamente.", "AVISO", MessageBoxButtons.OK);
                            Clases.Paneles.VaciarClienteSeleccionado();
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Ha ocurrido un error en la base de datos", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Clases.Paneles.VaciarClienteSeleccionado();
                        }
                    }
                }

            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                conexion.cerrarconexion();
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
