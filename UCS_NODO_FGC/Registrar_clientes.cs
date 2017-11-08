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
    public partial class Registrar_clientes : Form
    {
        public Clases.conexion_bd conexion = new Clases.conexion_bd();
        public Clases.Clientes cliente = new Clases.Clientes();
        public Clases.Clientes clienteexiste = new Clases.Clientes();
        public Clases.Clientes areaE = new Clases.Clientes();
        public Registrar_clientes()
        {
            InitializeComponent();
        }

        private void Registrar_clientes_Load(object sender, EventArgs e)
        {
            
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
                errorProviderTlfn.SetError(txtTelefonoCliArea, "Debe proporcionar un número de teléfono válido");
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

        private void txtNombreArea_KeyPress(object sender, KeyPressEventArgs e)
        {
            Clases.Paneles.sololetras(e);
            if (txtNombreArea.Text.Length == 0)
            {
                e.KeyChar = e.KeyChar.ToString().ToUpper().ToCharArray()[0];
            }
            else if (txtNombreArea.Text.Length > 0)
            {
                e.KeyChar = e.KeyChar.ToString().ToLower().ToCharArray()[0];
            }

                
        }
        private void txtNombreArea_KeyDown(object sender, KeyEventArgs e)
        {
            while (txtNombreArea.Text == "Nombre del área")
            {
                txtNombreArea.Text = "";

            }
        }
        private void txtNombreArea_Leave(object sender, EventArgs e)
        {
            if (txtNombreArea.Text == "")
            {
                txtNombreArea.Text = "Nombre del área";
            }
        }
        private void txtNombreArea_Click(object sender, EventArgs e)
        {

            if (txtNombreArea.Text != "")
            {

            }
            else
            {
                txtNombreArea.Text = "";
            }
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
        private void txtNombreEmpresa_KeyDown(object sender, KeyEventArgs e)
        {
            while (txtNombreEmpresa.Text == "Nombre empresa")
            {
                txtNombreEmpresa.Text = "";

            }
        }
        private void txtNombreEmpresa_Leave(object sender, EventArgs e)
        {
            if (txtNombreEmpresa.Text == "")
            {
                txtNombreEmpresa.Text = "Nombre empresa";
            }
        }
        private void txtNombreEmpresa_Click(object sender, EventArgs e)
        {

            if (txtNombreEmpresa.Text == "Nombre empresa")
            {
                txtNombreEmpresa.Text = "";
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
        private void txtCorreoCliArea_Validating(object sender, CancelEventArgs e)
        {
            if (Clases.Paneles.ComprobarFormatoEmail(txtCorreoCliArea.Text) == false)
            {
                errorProviderCorreo.SetError(txtCorreoCliArea, "Debe proporcionar un correo válido.");
                txtCorreoCliArea.Focus();
            }else
            {
                errorProviderCorreo.SetError(txtCorreoCliArea, "");
            }
        }
        private void txtCorreoCliArea_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar == (int)Keys.Enter)
            {
                Guardar();
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

        private void btnGuardarCliente_Click(object sender, EventArgs e)
        {
            //una empresa puede tener mas de un contacto? es necesario verificar que el nombre de la 
            //empresa no esté repetido? 
            Guardar();

        }

        private void Refrescar()
        {
            txtNombreEmpresa.Text = "Nombre empresa";
            txtNombreArea.Text = "Nombre del área";
            cmbxFee.SelectedIndex = -1;
            txtNombreContactoArea.Text = "Persona contacto del área";
            txtTelefonoCliArea.Text = "Teléfono o celular";
            txtCorreoCliArea.Text = "correo@ejemplo.com";
        }
        
        private void Guardar()
        {
            try
            {
                int fee;
                conexion.cerrarconexion();
                if (conexion.abrirconexion() == true)
                {
                    if (txtNombreEmpresa.Text == "Nombre empresa" || txtNombreEmpresa.Text == "")
                    {
                        errorProviderEmpresa.SetError(txtNombreEmpresa, "Debe proporcionar un nombre de empresa válido.");
                        txtNombreEmpresa.Focus();
                    }
                    else if (cmbxFee.SelectedIndex == -1)
                    {
                        errorProviderEmpresa.SetError(txtNombreEmpresa, "");
                        errorProviderFee.SetError(cmbxFee, "Debe seleccionar una de las opciones.");
                        cmbxFee.Focus();
                    }
                    else if (txtNombreArea.Text == "Nombre del área" || txtNombreArea.Text == "")
                    {
                        errorProviderFee.SetError(cmbxFee, "");
                        errorProviderNArea.SetError(txtNombreArea, "Debe proporcionar un nombre de área válido.");
                        txtNombreArea.Focus();
                    }
                    else if (txtNombreContactoArea.Text == "Persona contacto del área" || txtNombreContactoArea.Text == "")
                    {
                        errorProviderNArea.SetError(txtNombreArea, "");
                        errorProviderContacto.SetError(txtNombreContactoArea, "Debe proporcionar un contacto de área válido");
                        txtNombreContactoArea.Focus();
                    }
                    else //si todo ok, procede a registrarlo
                    {
                        errorProviderContacto.SetError(txtNombreContactoArea, "");


                        cliente.nombre_empresa = txtNombreEmpresa.Text;
                        cliente.nombre_areaEmpresa = txtNombreArea.Text;
                        cliente.nombre_contacto = txtNombreContactoArea.Text;
                        cliente.tlfn_cliente = txtTelefonoCliArea.Text;
                        cliente.correo_cliente = txtCorreoCliArea.Text;
                        fee = cmbxFee.SelectedIndex;
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
                        clienteexiste = Clases.Clientes.ClienteExiste(conexion.conexion, cliente);
                        conexion.cerrarconexion();

                        if (clienteexiste.id_cliente == 0)//si resultado es 0, el cliente no existe (se puede hacer el registro)
                        {
                            if (conexion.abrirconexion() == true)
                            {

                                int agregar = 0;
                                agregar = Clases.Clientes.AgregarCliente(conexion.conexion, cliente);
                                conexion.cerrarconexion();
                                if (conexion.abrirconexion() == true)
                                {
                                    clienteexiste = Clases.Clientes.ClienteExiste(conexion.conexion, cliente);
                                    conexion.cerrarconexion();
                                    cliente.id_cliente = clienteexiste.id_cliente;//aqui se toma el valor del id del cliente agregado
                                    if (conexion.abrirconexion() == true)
                                    {
                                        int addAreas = 0;
                                        addAreas = Clases.Clientes.AgregarArea(conexion.conexion, cliente);
                                        conexion.cerrarconexion();

                                        if (agregar > 0 && addAreas > 0)
                                        {

                                            if (MessageBox.Show("El cliente fue creado con exito. ¿Desea añadir otro cliente?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                                            {
                                                this.Close();
                                            }
                                            else
                                            {
                                                Refrescar();

                                            }



                                        }
                                        else
                                        {
                                            MessageBox.Show("No se pudo realizar el registro.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        }
                                        conexion.cerrarconexion();

                                    }
                                    else
                                    {
                                        MessageBox.Show("No se pudo realizar el registro.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                    conexion.cerrarconexion();
                                }




                            }


                        }
                        else if (clienteexiste.id_cliente != 0)//si la empresa existe
                        {
                            cliente.id_cliente = clienteexiste.id_cliente;
                            if (conexion.abrirconexion() == true)
                            {
                                areaE = Clases.Clientes.AreaExiste(conexion.conexion, cliente); //verificar que el area exista o no
                                conexion.cerrarconexion();
                                if (areaE.id_area > 0)//si el id_area existe, mandar a nuevo form para modificar el contacto de dicha area existente
                                {
                                    if (MessageBox.Show("Ya existe un contacto relacionado con el área de esta empresa. ¿Desea modificar el contacto del área?", "AVISO", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                                    {
                                        conexion.cerrarconexion();
                                        //lo ideal seria cerrar este form, que se dispare el 
                                        Clases.Cliente_seleccionado.id_cliente = cliente.id_cliente;
                                        Clases.Cliente_seleccionado.id_area = areaE.id_area;
                                        Clases.Cliente_seleccionado.nombre_empresa = cliente.nombre_empresa;
                                        Clases.Cliente_seleccionado.nombre_areaEmpresa = cliente.nombre_areaEmpresa;
                                        Clases.Cliente_seleccionado.nombre_contacto = cliente.nombre_contacto;
                                        Clases.Cliente_seleccionado.tlfn_cliente = cliente.tlfn_cliente;
                                        Clases.Cliente_seleccionado.correo_cliente = cliente.correo_cliente;

                                        Modificar_contacto_area mod_contacto = new Modificar_contacto_area();
                                        mod_contacto.ShowDialog();


                                    }
                                    else
                                    {
                                        Refrescar();
                                        conexion.cerrarconexion();
                                    }



                                }
                                else if (areaE.id_area == 0)//no existe area de la empresa ya registrada. Mandar a añadir nueva area de la empresa
                                {
                                    if (MessageBox.Show("Ya existe una empresa registrada con ese nombre en la base de datos. ¿Desea añadir una nueva área de la empresa?", "AVISO", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                                    {
                                        //aqui debe redirigir a añadir nueva area (nuevo boton)
                                        conexion.cerrarconexion();

                                        Clases.Cliente_seleccionado.id_cliente = cliente.id_cliente;
                                        Clases.Cliente_seleccionado.fee_empresa = cliente.fee_empresa;
                                        Clases.Cliente_seleccionado.nombre_empresa = cliente.nombre_empresa;
                                        Clases.Cliente_seleccionado.nombre_areaEmpresa = cliente.nombre_areaEmpresa;
                                        Clases.Cliente_seleccionado.nombre_contacto = cliente.nombre_contacto;
                                        Clases.Cliente_seleccionado.tlfn_cliente = cliente.tlfn_cliente;
                                        Clases.Cliente_seleccionado.correo_cliente = cliente.correo_cliente;

                                        Registrar_area registroArea = new Registrar_area();
                                        registroArea.ShowDialog();

                                        Refrescar();
                                        conexion.cerrarconexion();
                                        int fe = 3;
                                        Clases.Cliente_seleccionado.id_cliente = 0;
                                        Clases.Cliente_seleccionado.fee_empresa = fe;
                                        Clases.Cliente_seleccionado.id_area = 0;
                                        Clases.Cliente_seleccionado.nombre_empresa = "";
                                        Clases.Cliente_seleccionado.nombre_areaEmpresa = "";
                                        Clases.Cliente_seleccionado.nombre_contacto = "";
                                        Clases.Cliente_seleccionado.tlfn_cliente = "";
                                        Clases.Cliente_seleccionado.correo_cliente = "";

                                    }
                                    else
                                    {
                                        Refrescar();

                                    }


                                }
                            }

                        }

                        conexion.cerrarconexion();
                    }
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
