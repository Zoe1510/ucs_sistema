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
    public partial class Registrar_area : Form
    {
        public Clases.conexion_bd conexion = new Clases.conexion_bd();
        public Clases.Clientes cliente = new Clases.Clientes();
        public Clases.Clientes clienteexiste = new Clases.Clientes();
        public Clases.Clientes areaE = new Clases.Clientes();
        public Registrar_area()
        {
            InitializeComponent();
        }

        int id_cli = 0;
        //LOAD DEL FORM
        private void Registrar_area_Load(object sender, EventArgs e)
        {
            
            if (Clases.Cliente_seleccionado.id_cliente != 0)
            {
                
                txtNombreEmpresa.Visible = true;
                txtNombreEmpresa.Text = Clases.Cliente_seleccionado.nombre_empresa;
                txtNombreEmpresa.Enabled = false;
                txtNombreArea.Text = Clases.Cliente_seleccionado.nombre_areaEmpresa;
                txtNombreContactoArea.Text = Clases.Cliente_seleccionado.nombre_contacto;
                txtTelefonoCliArea.Text = Clases.Cliente_seleccionado.tlfn_cliente;
                txtCorreoCliArea.Text = Clases.Cliente_seleccionado.correo_cliente;

                cliente.id_cliente = Clases.Cliente_seleccionado.id_cliente;
                Clases.Cliente_seleccionado.id_cliente = 0;
                Clases.Cliente_seleccionado.id_area = 0;
                Clases.Cliente_seleccionado.nombre_empresa = "";
                Clases.Cliente_seleccionado.nombre_areaEmpresa = "";
                Clases.Cliente_seleccionado.nombre_contacto = "";
                Clases.Cliente_seleccionado.tlfn_cliente = "";
                Clases.Cliente_seleccionado.correo_cliente = "";
            }else
            {
                this.Location = new Point(-5, 0);
            }
            string nombre = "";
            llenarcombo(nombre);
        }

        //METODOS
        private void llenarcombo(string nombre)
        {
            //llenar el combobox con las empresas registradas:
            cmbxEmpresa.ValueMember = "id_clientes";
            cmbxEmpresa.DisplayMember = "nombre_empresa";
            cmbxEmpresa.DataSource = Clases.Paneles.LlenarCombobox(nombre);
            cmbxEmpresa.SelectedIndex = -1;
            cmbxEmpresa.Text = "    Seleccione";
        } //metodo para llenar el combobox con las empresas existentes

        
        private void GuardarArea()
        {
            try
            {
                conexion.cerrarconexion();
                if(txtNombreEmpresa.Visible == false)//es decir: si no se viene referenciado de Registro clientes
                {
                    if (cmbxEmpresa.SelectedIndex == -1)
                    {
                        errorProviderCMBxNCli.SetError(cmbxEmpresa, "Debe seleccionar una empresa para continuar con el registro.");

                    }
                    else if (txtNombreArea.Text == "" || txtNombreArea.Text == "Nombre del área")
                    {
                        errorProviderCMBxNCli.SetError(cmbxEmpresa, "");
                        errorProviderNomArea.SetError(txtNombreArea, "Debe proporcionar un nombre válido.");
                        txtNombreArea.Focus();
                    }
                    else if (txtNombreContactoArea.Text == "" || txtNombreContactoArea.Text == "Persona contacto del área")
                    {
                        errorProviderNomArea.SetError(txtNombreArea, "");
                        errorProviderPersContacto.SetError(txtNombreContactoArea, "Debe proporcionar el nombre del contacto dentro del área.");
                        txtNombreContactoArea.Focus();
                    } else if (txtTelefonoCliArea.Text == "" || txtTelefonoCliArea.TextLength < 11)
                    {
                        errorProviderPersContacto.SetError(txtNombreContactoArea, "");
                        errorProviderTxtNCli.SetError(txtTelefonoCliArea, "Debe proporcionar un número de teléfono válido.");
                        txtTelefonoCliArea.Focus();
                    }
                    else if (txtCorreoCliArea.Text == "correo@ejemplo.com")
                    {
                        errorProviderTxtNCli.SetError(txtTelefonoCliArea, "");
                        errorProviderCorreo.SetError(txtCorreoCliArea, "Debe proporcionar un correo electrónico válido.");
                        txtCorreoCliArea.Focus();
                    }
                    else
                    {
                        errorProviderCorreo.SetError(txtCorreoCliArea, "");

                        conexion.cerrarconexion();
                        if (conexion.abrirconexion() == true)
                        {
                            if (cliente.id_cliente > 0)
                            {
                                string nombre_e = Clases.Clientes.seleccionarNombreEmpresa(conexion.conexion, id_cli);
                                conexion.cerrarconexion();

                                if (nombre_e != "")//si el id arroja un Nombre de Empresa
                                {
                                    //cuando todas las comprobaciones sean hechas, se guardarán los datos en "cliente"
                                    conexion.cerrarconexion();
                                    if (conexion.abrirconexion() == true)
                                    {
                                        cliente.nombre_areaEmpresa = txtNombreArea.Text;
                                        cliente.nombre_contacto = txtNombreContactoArea.Text;
                                        cliente.tlfn_cliente = txtTelefonoCliArea.Text;
                                        cliente.correo_cliente = txtCorreoCliArea.Text;
                                        cliente.nombre_empresa = nombre_e;

                                        areaE = Clases.Clientes.AreaExiste(conexion.conexion, cliente); //se le manda el id_cliente y el nombre del area para la comprobación de existencia.
                                        conexion.cerrarconexion();
                                        if (areaE.id_area == 0)//si retorna 0 es que el area no existe
                                        {
                                            conexion.cerrarconexion();
                                            if (conexion.abrirconexion() == true)
                                            {
                                                int addAreas = 0;
                                                addAreas = Clases.Clientes.AgregarArea(conexion.conexion, cliente);
                                                conexion.cerrarconexion();

                                                if (addAreas > 0)
                                                {

                                                    if (MessageBox.Show("El área fue registrada con éxito. ¿Desea añadir otra área?", "AVISO", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                                                    {
                                                        Clases.Cliente_seleccionado.id_cliente = 0;
                                                        this.Close();
                                                    }
                                                    else //en el caso de que se quiera añadir otra area (se quita el textbox)
                                                    {
                                                        Clases.Cliente_seleccionado.id_cliente = 0;
                                                        txtNombreEmpresa.Text = "Nombre empresa";
                                                        txtNombreEmpresa.Visible = false;
                                                        string nombre = "";
                                                        llenarcombo(nombre);
                                                        refrescar();
                                                    }



                                                }
                                                else
                                                {
                                                    MessageBox.Show("No se pudo realizar el registro.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                }
                                                conexion.cerrarconexion();

                                            }

                                        }
                                        else if (areaE.id_area > 0)
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
                                                refrescar();

                                            }
                                            else
                                            {

                                                refrescar();
                                                conexion.cerrarconexion();
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }

                }
                else if (txtNombreEmpresa.Visible == true)//si viene referenciado de REGISTRAR CLIENTES
                {
                    if (txtNombreArea.Text == "" || txtNombreArea.Text == "Nombre del área")
                    {
                        errorProviderCMBxNCli.SetError(cmbxEmpresa, "");
                        errorProviderNomArea.SetError(txtNombreArea, "Debe proporcionar un nombre válido.");
                        txtNombreArea.Focus();
                    }
                    else if (txtNombreContactoArea.Text == "" || txtNombreContactoArea.Text == "Persona contacto del área")
                    {
                        errorProviderNomArea.SetError(txtNombreArea, "");
                        errorProviderPersContacto.SetError(txtNombreContactoArea, "Debe proporcionar el nombre del contacto dentro del área");
                        txtNombreContactoArea.Focus();
                    }
                    else if (txtTelefonoCliArea.Text == "" || txtTelefonoCliArea.TextLength < 11)
                    {
                        errorProviderPersContacto.SetError(txtNombreContactoArea, "");
                        errorProviderTxtNCli.SetError(txtTelefonoCliArea, "Debe proporcionar un número de teléfono válido.");
                        txtTelefonoCliArea.Focus();
                    }
                    else if (txtCorreoCliArea.Text == "correo@ejemplo.com")
                    {
                        errorProviderTxtNCli.SetError(txtTelefonoCliArea, "");
                        errorProviderCorreo.SetError(txtCorreoCliArea, "Debe proporcionar un correo electrónico válido.");
                        txtCorreoCliArea.Focus();
                    }
                    else
                    {
                        errorProviderCorreo.SetError(txtCorreoCliArea, "");

                        conexion.cerrarconexion();
                        if (conexion.abrirconexion() == true)
                        {
                            if (cliente.id_cliente > 0)
                            {
                                conexion.cerrarconexion();
                                //cuando todas las comprobaciones sean hechas, se guardarán los datos en "cliente"
                                if (conexion.abrirconexion() == true)
                                {

                                    cliente.nombre_areaEmpresa = txtNombreArea.Text;
                                    cliente.nombre_contacto = txtNombreContactoArea.Text;
                                    cliente.tlfn_cliente = txtTelefonoCliArea.Text;
                                    cliente.correo_cliente = txtCorreoCliArea.Text;
                                    cliente.nombre_empresa = txtNombreEmpresa.Text;

                                    areaE = Clases.Clientes.AreaExiste(conexion.conexion, cliente); //se le manda el id_cliente y el nombre del area para la comprobación de existencia.
                                    conexion.cerrarconexion();
                                    if (areaE.id_area == 0)//si retorna 0 es que el area no existe
                                    {
                                        conexion.cerrarconexion();
                                        if (conexion.abrirconexion() == true)
                                        {
                                            int addAreas = 0;
                                            addAreas = Clases.Clientes.AgregarArea(conexion.conexion, cliente);
                                            conexion.cerrarconexion();

                                            if (addAreas > 0)
                                            {

                                                if (MessageBox.Show("El área fue registrada con éxito. ¿Desea añadir otra área?", "AVISO", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                                                {
                                                    Clases.Cliente_seleccionado.id_cliente = 0;
                                                    this.Close();
                                                    Panel_cabecera.Visible = true;
                                                }
                                                else //en el caso de que se quiera añadir otra area (se quita el textbox)
                                                {
                                                    Clases.Cliente_seleccionado.id_cliente = 0;
                                                    txtNombreEmpresa.Text = "Nombre empresa";
                                                    txtNombreEmpresa.Visible = false;
                                                    string nombre = "";
                                                    llenarcombo(nombre);
                                                    refrescar();

                                                }



                                            }
                                            else
                                            {
                                                MessageBox.Show("No se pudo realizar el registro.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                            }
                                            conexion.cerrarconexion();

                                        }
                                    }
                                    else if (areaE.id_area > 0)
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
                                            refrescar();

                                        }
                                        else
                                        {
                                            refrescar();
                                            conexion.cerrarconexion();
                                        }
                                    }
                                }

                            }
                        }

                    }//final TODO OK

                }//FINAL (txtNombreEmpresa.Visible == true)

            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void refrescar()
        {
            txtNombreArea.Text = "Nombre del área";
            txtNombreContactoArea.Text = "Persona contacto del área";
            txtTelefonoCliArea.Text = "Teléfono o celular";
            txtCorreoCliArea.Text = "correo@ejemplo.com";
            errorProviderCMBxNCli.SetError(cmbxEmpresa, "");
            errorProviderNomArea.SetError(txtNombreArea, "");
            errorProviderPersContacto.SetError(txtNombreContactoArea, "");
            errorProviderTxtNCli.SetError(txtTelefonoCliArea, "");
            errorProviderCorreo.SetError(txtCorreoCliArea, "");
        }
        //END METODOS

        //EVENTOS
        private void cmbxEmpresa_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (Clases.Cliente_seleccionado.id_cliente == 0)
            {
                id_cli = Convert.ToInt32(cmbxEmpresa.SelectedValue);
                cliente.id_cliente = id_cli;//aqui, cargo el id_cliente de acuerdo a la seleccion del usuario en el combo
            }
            
        }
        //END EVENTOS

        //TEXTBOX
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
                errorProviderTlfn.SetError(txtTelefonoCliArea, "");
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
                errorProviderCorreo.SetError(txtCorreoCliArea, "Debe proporcionar un correo válido para continuar con el registro.");
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
                GuardarArea();
            }
        }
        //END TEXTBOX

        //BOTONES
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Desea cancelar la operación?", "",
       MessageBoxButtons.YesNo, MessageBoxIcon.Question)
       == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void btnGuardarArea_Click(object sender, EventArgs e)
        {
            GuardarArea();
        }
        //END BOTONES
    }
}
