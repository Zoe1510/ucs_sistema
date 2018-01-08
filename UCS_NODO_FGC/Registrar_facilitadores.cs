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
    public partial class Registrar_facilitadores : Form
    {
        public Clases.Facilitadores facilitadores = new Clases.Facilitadores();
        public Clases.conexion_bd conexion = new Clases.conexion_bd();

        public Registrar_facilitadores()
        {
            InitializeComponent();
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

       
        private void txtCedulaFa_KeyPress(object sender, KeyPressEventArgs e)
        {
            Clases.Paneles.solonumeros(e);
        }
        private void txtCedulaFa_KeyDown(object sender, KeyEventArgs e)
        {
            while (txtCedulaFa.Text == "                     Cédula")
            {
                txtCedulaFa.Text = "";

            }
        }
        private void txtCedulaFa_Leave(object sender, EventArgs e)
        {
            if (txtCedulaFa.Text == "")
            {
                txtCedulaFa.Text = "                     Cédula";
            }
        }
        private void txtCedulaFa_MouseClick(object sender, MouseEventArgs e)
        {
            if (txtCedulaFa.Text != "")
            {
                
              
            }
            else
            {
                txtCedulaFa.Text = "";
            }
        }

        private void txtNombreFa_KeyPress(object sender, KeyPressEventArgs e)
        {
            Clases.Paneles.sololetras(e);
            if (txtNombreFa.Text.Length == 0)
            {
                e.KeyChar = e.KeyChar.ToString().ToUpper().ToCharArray()[0];
            }
            else if (txtNombreFa.Text.Length > 0)
            {
                e.KeyChar = e.KeyChar.ToString().ToLower().ToCharArray()[0];
            }
        }
        private void txtNombreFa_KeyDown(object sender, KeyEventArgs e)
        {
            while (txtNombreFa.Text == "Nombre")
            {
                txtNombreFa.Text = "";

            }
        }
        private void txtNombreFa_Leave(object sender, EventArgs e)
        {
            if (txtNombreFa.Text == "")
            {
                txtNombreFa.Text = "Nombre";
            }
        }
        private void txtNombreFa_Click(object sender, EventArgs e)
        {

            if (txtNombreFa.Text != "")
            {
               
            }
            else
            {
                txtNombreFa.Text = "";
            }
        }

        private void txtApellidoFa_KeyPress(object sender, KeyPressEventArgs e)
        {
            Clases.Paneles.sololetras(e);
            if (txtApellidoFa.Text.Length == 0)
            {
                e.KeyChar = e.KeyChar.ToString().ToUpper().ToCharArray()[0];
            }
            else if (txtApellidoFa.Text.Length > 0)
            {
                e.KeyChar = e.KeyChar.ToString().ToLower().ToCharArray()[0];
            }
        }
        private void txtApellidoFa_KeyDown(object sender, KeyEventArgs e)
        {
            while (txtApellidoFa.Text == "Apellido")
            {
                txtApellidoFa.Text = "";

            }
        }
        private void txtApellidoFa_Leave(object sender, EventArgs e)
        {
            if (txtApellidoFa.Text == "")
            {
                txtApellidoFa.Text = "Apellido";
            }
        }
        private void txtApellidoFa_Click(object sender, EventArgs e)
        {

            if (txtApellidoFa.Text != "")
            {
                txtApellidoFa.SelectionStart = txtApellidoFa.Text.Length;
            }
            else
            {
                txtApellidoFa.Text = "";
            }
        }

        
        private void txtCorreoFa_Leave(object sender, EventArgs e)
        {
            if(txtCorreoFa.Text == "")
            {
                txtCorreoFa.Text = "correo@ejemplo.com";
            }
        }
        private void txtCorreoFa_KeyDown(object sender, KeyEventArgs e)
        {
            while (txtCorreoFa.Text == "correo@ejemplo.com")
            {
                txtCorreoFa.Text = "";
            }
        }
        private void txtCorreoFa_Click(object sender, EventArgs e)
        {

            if (txtCorreoFa.Text != "")
            {
                txtCorreoFa.SelectionStart = txtCorreoFa.Text.Length;
            }
            else
            {
                txtCorreoFa.Text = "";
            }
        }
        
     
        private void txtTelefonoFa_KeyDown(object sender, KeyEventArgs e)
        {
            while (txtTelefonoFa.Text == "Teléfono o celular")
            {
                txtTelefonoFa.Text = "";
            }
        }
        private void txtTelefonoFa_KeyPress(object sender, KeyPressEventArgs e)
        {
            Clases.Paneles.solonumeros(e);
        }
        private void txtTelefonoFa_Leave(object sender, EventArgs e)
        {
            if (txtTelefonoFa.Text == "")
            {
                txtTelefonoFa.Text = "Teléfono o celular";
            }
        }
        private void txtTelefonoFa_Click(object sender, EventArgs e)
        {

            if (txtTelefonoFa.Text != "")
            {
                txtTelefonoFa.SelectionStart = txtTelefonoFa.Text.Length;
            }
            else
            {
                txtTelefonoFa.Text = "";
            }
        }

        private void txtEspecialidadFa_KeyPress(object sender, KeyPressEventArgs e)
        {
            Clases.Paneles.sololetras(e);
            if (txtEspecialidadFa.Text.Length == 0)
            {
                e.KeyChar = e.KeyChar.ToString().ToUpper().ToCharArray()[0];
            }
            else if (txtEspecialidadFa.Text.Length > 0)
            {
                e.KeyChar = e.KeyChar.ToString().ToLower().ToCharArray()[0];
            }
        }
        private void txtEspecialidadFa_Leave(object sender, EventArgs e)
        {
            if (txtEspecialidadFa.Text == "")
            {
                txtEspecialidadFa.Text = "Especialidad";
            }
        }
        private void txtEspecialidadFa_KeyDown(object sender, KeyEventArgs e)
        {
            while (txtEspecialidadFa.Text == "Especialidad")
            {
                txtEspecialidadFa.Text = "";
            }
        }
        private void txtEspecialidadFa_Click(object sender, EventArgs e)
        {

            if (txtEspecialidadFa.Text != "")
            {
                txtEspecialidadFa.SelectionStart = txtEspecialidadFa.Text.Length;
            }
            else
            {
                txtEspecialidadFa.Text = "";
            }
        }

        private void seleccionarEdo(string edo)
        {
            
            switch (edo)
            {
                case "0":
                    edo = "Amazonas";
                    break;
                case "1":
                    edo = "Anzoátegui";
                    break;
                case "2":
                    edo = "Apure";
                    break;
                case "3":
                    edo = "Aragua";
                    break;
                case "4":
                    edo = "Barinas";
                    break;
                case "5":
                    edo = "Bolívar";
                    break;
                case "6":
                    edo = "Carabobo";
                    break;
                case "7":
                    edo = "Cojedes";
                    break;
                case "8":
                    edo = "Delta Amacuro";
                    break;
                case "9":
                    edo = "Distrito Capital";
                    break;
                case "10":
                    edo = "Falcón";
                    break;
                case "11":
                    edo = "Guárico";
                    break;
                case "12":
                    edo = "Lara";
                    break;
                case "13":
                    edo = "Mérida";
                    break;
                case "14":
                    edo = "Miranda";
                    break;
                case "15":
                    edo = "Monagas";
                    break;
                case "16":
                    edo = "Nueva Esparta";
                    break;
                case "17":
                    edo = "Portuguesa";
                    break;
                case "18":
                    edo = "Sucre";
                    break;
                case "19":
                    edo = "Táchira";
                    break;
                case "20":
                    edo = "Trujillo";
                    break;
                case "21":
                    edo = "Vargas";
                    break;
                case "22":
                    edo = "Yaracuy";
                    break;
                case "23":
                    edo = "Zulia";
                    break;
            }
            facilitadores.ubicacion_facilitador = edo;
        }

        private void RegistrarFa()
        {
            string nacionalidad;
            int ince;
            try
            {
                conexion.cerrarconexion();
                if (conexion.abrirconexion() == true)
                {
                    if (txtCedulaFa.Text == "                     Cédula" || txtCedulaFa.Text == "" || txtCedulaFa.Text.Length < 7)
                    {
                        errorProviderCI.SetError(txtCedulaFa, "Debe proporcionar un número de cédula válido.");
                        txtCedulaFa.Focus();
                    } else if (txtNombreFa.Text == "Nombre" || txtNombreFa.Text == "")
                    {
                        errorProviderCI.SetError(txtCedulaFa, "");


                        errorProviderNombre.SetError(txtNombreFa, "Debe proporcionar un nombre válido.");
                        txtNombreFa.Focus();
                    } else if (txtApellidoFa.Text == "Apellido" || txtApellidoFa.Text == "")
                    {
                        errorProviderNombre.SetError(txtNombreFa, "");

                        errorProviderApellido.SetError(txtApellidoFa, "Debe proporcionar un apellido válido.");
                        txtApellidoFa.Focus();
                    } else if (txtCorreoFa.Text == "correo@ejemplo.com" || txtCorreoFa.Text == "")
                    {
                        errorProviderApellido.SetError(txtApellidoFa, "");

                        errorProviderCorreo.SetError(txtCorreoFa, "Debe proporcionar un correo válido");
                        txtCorreoFa.Focus();
                    } else if (txtTelefonoFa.Text == "")
                    {
                        errorProviderCorreo.SetError(txtCorreoFa, "");

                        errorProviderTlfn.SetError(txtTelefonoFa, "Debe proporcionar un tel+efono válido");
                        txtTelefonoFa.Focus();
                    }
                    else if (txtEspecialidadFa.Text == "Especialidad" || txtEspecialidadFa.Text == "")
                    {

                        errorProviderTlfn.SetError(txtTelefonoFa, "");
                        errorProviderEspec.SetError(txtEspecialidadFa, "Debe proporcionar la especialidad del facilitador.");
                        txtEspecialidadFa.Focus();
                    } else if (cmbxINCE.SelectedIndex == -1)
                    {
                        errorProviderEspec.SetError(txtEspecialidadFa, "");

                        errorProviderCmbINCE.SetError(cmbxINCE, "Debe seleccionar una de las opciones.");

                    } else if (cmbUbicacionEdo.SelectedIndex == -1)
                    {
                        errorProviderCmbINCE.SetError(cmbxINCE, "");
                        errorProvider8CmbUbi.SetError(cmbUbicacionEdo, "Debe seleccionar la ubicación del facilitador.");
                    } else
                    {
                        errorProvider8CmbUbi.SetError(cmbUbicacionEdo, "");


                        facilitadores.ci_facilitador = txtCedulaFa.Text;
                        facilitadores.nombre_facilitador = txtNombreFa.Text;
                        facilitadores.apellido_facilitador = txtApellidoFa.Text;
                        facilitadores.correo_facilitador = txtCorreoFa.Text;
                        facilitadores.tlfn_facilitador = txtTelefonoFa.Text;
                        string edo = Convert.ToString(cmbUbicacionEdo.SelectedIndex);
                        seleccionarEdo(edo);
                        nacionalidad = Convert.ToString(cmbNacionalidad.SelectedIndex);
                        switch (nacionalidad)
                        {
                            case "0":
                                nacionalidad = "V";
                                break;
                            case "1":
                                nacionalidad = "E";
                                break;
                        }
                        facilitadores.nacionalidad_fa = nacionalidad;
                        facilitadores.especialidad_facilitador = txtEspecialidadFa.Text;
                        ince = cmbxINCE.SelectedIndex;
                        switch (ince)
                        {
                            case 0:
                                ince = 1;//avalado para dar cursos inces
                                break;
                            case 1:
                                ince = 0;//no puede dar cursos inces
                                break;
                        }
                        facilitadores.requerimiento_ince = ince;
                        int resultado, resultado2;
                        resultado = Clases.Facilitadores.FacilitadorExiste(conexion.conexion, facilitadores.ci_facilitador);
                        conexion.cerrarconexion();
                        if (conexion.abrirconexion() == true)
                        {
                            resultado2 = Clases.Facilitadores.AgregarFacilitador(conexion.conexion, facilitadores);
                            conexion.cerrarconexion();

                            if (conexion.abrirconexion() == true)
                            {
                                if (resultado != 1)
                                {
                                    if (resultado2 != 0)
                                    {
                                        if (MessageBox.Show("El usuario fue creado con éxito. ¿Desea añadir más facilitadores?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                                        {
                                            this.Close();
                                        }
                                        else
                                        {
                                            cmbNacionalidad.SelectedIndex = -1;
                                            txtCedulaFa.Text = "                     Cédula";
                                            txtNombreFa.Text = "Nombre";
                                            txtApellidoFa.Text = "Apellido";
                                            txtCorreoFa.Text = "correo@ejemplo.com";
                                            txtTelefonoFa.Text = "Teléfono o celular";
                                            txtEspecialidadFa.Text = "Especialidad";
                                            cmbUbicacionEdo.Text = "                              Ubicacion";
                                            cmbUbicacionEdo.SelectedIndex = -1;
                                        }

                                    }
                                    else
                                    {
                                        MessageBox.Show("Ha ocurrido un error en la base de datos", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }

                                }
                                else
                                {
                                    MessageBox.Show("Ya existe un facilitador registrado con ese número de cédula en la base de datos. Inténtelo de nuevo", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    cmbNacionalidad.SelectedIndex = -1;
                                    txtCedulaFa.Text = "";
                                    txtNombreFa.Text = "";
                                    txtApellidoFa.Text = "";
                                    txtCorreoFa.Text = "";
                                    txtTelefonoFa.Text = "";
                                    txtEspecialidadFa.Text = "";
                                    cmbUbicacionEdo.SelectedIndex = -1;

                                }

                                conexion.cerrarconexion();
                            }
                            conexion.cerrarconexion();
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
        private void btnGuardarFacilitador_Click(object sender, EventArgs e)
        {
            RegistrarFa();
        }

        private void txtTelefonoFa_Validating(object sender, CancelEventArgs e)
        {
            if(Clases.Paneles.comprobarFormatoTlfn(e, txtTelefonoFa.Text) == false)
            {
                errorProviderTlfn.SetError(txtTelefonoFa, "Debe proporcionar un número de teléfono válido.");
                txtTelefonoFa.Focus();
            }else
            {
                errorProviderTlfn.SetError(txtTelefonoFa, "");
            }
        }

        private void Registrar_facilitadores_Load(object sender, EventArgs e)
        {
            this.Location = new Point(-5, 0);
            cmbUbicacionEdo.SelectedIndex = 16;
        }

        private void txtCedulaFa_Validating(object sender, CancelEventArgs e)
        {
            if(Clases.Paneles.comprobarCedula(txtCedulaFa.Text) == true)
            {
                cmbNacionalidad.SelectedIndex = 1;
            }else
            {
                cmbNacionalidad.SelectedIndex = 0;
            }
        }

        private void txtCorreoFa_Validating(object sender, CancelEventArgs e)
        {
            if (Clases.Paneles.ComprobarFormatoEmail(txtCorreoFa.Text) == false)
            {
                errorProviderCorreo.SetError(txtCorreoFa, "Debe proporcionar un correo válido");
                txtCorreoFa.Focus();
            }else
            {
                errorProviderCorreo.SetError(txtCorreoFa, "");
            }
        }

        private void cmbNacionalidad_Validating(object sender, CancelEventArgs e)
        {
            if (Clases.Paneles.comprobarCedula(txtCedulaFa.Text) == true)
            {
                cmbNacionalidad.SelectedIndex = 1;
            }
            else
            {
                cmbNacionalidad.SelectedIndex = 0;
            }
        }
    }
}
