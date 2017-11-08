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
    public partial class Modificar_facilitadores : Form
    {
        public Clases.Facilitadores facilitadores = new Clases.Facilitadores();
        public Clases.conexion_bd conexion = new Clases.conexion_bd();
        public Modificar_facilitadores()
        {
            InitializeComponent();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

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
        private void btnGuardarFa_Click(object sender, EventArgs e)
        {
            Actualizar();
        }

        private void txtCedulaFa_KeyPress(object sender, KeyPressEventArgs e)
        {
            Clases.Paneles.solonumeros(e);
        }
        private void txtCedulaFa_KeyDown(object sender, KeyEventArgs e)
        {
            while (txtCedulaFa.Text == "Cédula")
            {
                txtCedulaFa.Text = "";

            }
        }
        private void txtCedulaFa_Leave(object sender, EventArgs e)
        {
            if (txtCedulaFa.Text == "")
            {
                txtCedulaFa.Text = "Cédula";
            }
        }
        private void txtCedulaFa_MouseClick(object sender, MouseEventArgs e)
        {
            if (txtCedulaFa.Text != "Cédula")
            {


            }
            else
            {
                txtCedulaFa.Text = "";
            }
        }
        private void txtCedulaFa_Validating(object sender, CancelEventArgs e)
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

            if (txtNombreFa.Text != "Nombre")
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

            if (txtApellidoFa.Text != "Apellido")
            {
                
            }
            else
            {
                txtApellidoFa.Text = "";
            }
        }


        private void txtCorreoFa_Leave(object sender, EventArgs e)
        {
            if (txtCorreoFa.Text == "")
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

            if (txtCorreoFa.Text != "correo@ejemplo.com")
            {
               
            }
            else
            {
                txtCorreoFa.Text = "";
            }
        }
        private void txtCorreoFa_Validating(object sender, CancelEventArgs e)
        {
            if (Clases.Paneles.ComprobarFormatoEmail(txtCorreoFa.Text) == false)
            {
                errorProviderCorreo.SetError(txtCorreoFa, "Debe proporcionar un correo válido.");
                txtCorreoFa.Focus();
            }else
            {
                errorProviderCorreo.SetError(txtCorreoFa, "");
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

            if (txtTelefonoFa.Text != "Téléfono o celular")
            {
                
            }
            else
            {
                txtTelefonoFa.Text = "";
            }
        }
        private void txtTelefonoFa_Validating(object sender, CancelEventArgs e)
        {
            if(Clases.Paneles.comprobarFormatoTlfn(e, txtTelefonoFa.Text) == false)
            {
                errorProviderTlfn.SetError(txtTelefonoFa, "Debe proporcionar un teléfono válido.");
                txtTelefonoFa.Focus();
            }else
            {
                errorProviderTlfn.SetError(txtTelefonoFa, "");
                facilitadores.tlfn_facilitador = txtTelefonoFa.Text;
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

            if (txtEspecialidadFa.Text != "Especialidad")
            {
                
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
                    edo = "Bolíar";
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
        
        private void CargarEdo(string edo)
        {

            switch (edo)
            {
                case "Amazonas":
                    cmbUbicacionEdo.SelectedIndex = 0;
                    break;
                case "Anzoátegui":
                    cmbUbicacionEdo.SelectedIndex = 1;
                    break;
                case "Apure":
                    cmbUbicacionEdo.SelectedIndex = 2;
                    break;
                case "Aragua":
                    cmbUbicacionEdo.SelectedIndex = 3;
                    break;
                case "Barinas":
                    cmbUbicacionEdo.SelectedIndex = 4;
                    break;
                case "Bolívar":
                    cmbUbicacionEdo.SelectedIndex = 5;
                    break;
                case "Carabobo":
                    cmbUbicacionEdo.SelectedIndex = 6;
                    break;
                case "Cojedes":
                    cmbUbicacionEdo.SelectedIndex = 7;
                    break;
                case "Delta Amacuro":
                    cmbUbicacionEdo.SelectedIndex = 8;
                    break;
                case "Distrito Capital":
                    cmbUbicacionEdo.SelectedIndex = 9;
                    break;
                case "Falcón":
                    cmbUbicacionEdo.SelectedIndex = 10;
                    break;
                case "Guárico":
                    cmbUbicacionEdo.SelectedIndex = 11;
                    break;
                case "Lara":
                    cmbUbicacionEdo.SelectedIndex = 12;
                    break;
                case "Mérida":
                    cmbUbicacionEdo.SelectedIndex = 13;
                    break;
                case "Miranda":
                    cmbUbicacionEdo.SelectedIndex = 14;
                    break;
                case "Monagas":
                    cmbUbicacionEdo.SelectedIndex = 15;
                    break;
                case "Nueva Esparta":
                    cmbUbicacionEdo.SelectedIndex = 16;
                    break;
                case "Portuguesa":
                    cmbUbicacionEdo.SelectedIndex = 17;
                    break;
                case "Sucre":
                    cmbUbicacionEdo.SelectedIndex = 18;
                    break;
                case "Táchira":
                    cmbUbicacionEdo.SelectedIndex = 19;
                    break;
                case "Trujillo":
                    cmbUbicacionEdo.SelectedIndex = 20;
                    break;
                case "Vargas":
                    cmbUbicacionEdo.SelectedIndex = 21;
                    break;
                case "Yaracuy":
                    cmbUbicacionEdo.SelectedIndex = 22;
                    break;
                case "Zulia":
                    cmbUbicacionEdo.SelectedIndex = 23;
                    break;
            }
          
        }

        private void CargarNacionalidad(string na)
        {
            switch(na)
            {
                case "V":
                    cmbNacionalidad.SelectedIndex = 0;
                    break;
                case "E":
                    cmbNacionalidad.SelectedIndex = 1;
                    break;
            }
        }

        private void Modificar_facilitadores_Load(object sender, EventArgs e)
        {
            string na = Clases.Facilitador_Seleccionado.nacionalidad_fa;
            CargarNacionalidad(na);
            txtCedulaFa.Text = Clases.Facilitador_Seleccionado.ci_facilitador;
            txtNombreFa.Text = Clases.Facilitador_Seleccionado.nombre_facilitador;
            txtApellidoFa.Text = Clases.Facilitador_Seleccionado.apellido_facilitador;
            txtCorreoFa.Text = Clases.Facilitador_Seleccionado.correo_facilitador;
            txtTelefonoFa.Text = Clases.Facilitador_Seleccionado.tlfn_facilitador;
            txtEspecialidadFa.Text = Clases.Facilitador_Seleccionado.especialidad_facilitador;
            string Edo = Clases.Facilitador_Seleccionado.ubicacion_facilitador;
            CargarEdo(Edo);
            int ince = Clases.Facilitador_Seleccionado.requerimiento_ince;
            switch (ince)
            {
                case 1:
                    cmbxINCE.SelectedIndex = 0;
                    break;
                case 0:
                    cmbxINCE.SelectedIndex = 1;
                    break;
            }
            

        }
        private void actualizarfa()
        {
            int resultado;
            try
            {
                conexion.cerrarconexion();
                if (conexion.abrirconexion() == true)
                {
                    resultado = Clases.Facilitadores.ActualizarFa(conexion.conexion, facilitadores);
                    conexion.cerrarconexion();

                    if (resultado > 0)
                    {
                        MessageBox.Show("Los datos han sido modificados exitosamente.", "", MessageBoxButtons.OK);
                        this.Close();
                        //Pagina_principal pag_pri = new Pagina_principal();
                        //pag_pri.AddFormInPanel(new Buscar_facilitadores());
                    }
                    else
                    {
                        MessageBox.Show("Ha ocurrido un error en la base de datos", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                conexion.cerrarconexion();
            }
        }
        private void Actualizar()
        {
            string nacionalidad;
            int ince;
            try
            {
                conexion.cerrarconexion();
                if (conexion.abrirconexion() == true)
                {
                    if (txtCedulaFa.Text == "" || txtCedulaFa.Text.Length < 7)
                    {
                        errorProviderCI.SetError(txtCedulaFa, "Debe proporcionar un número de cédula válido.");
                        txtCedulaFa.Focus();
                    }
                    else if (txtNombreFa.Text == "Nombre" || txtNombreFa.Text == "")
                    {
                        errorProviderCI.SetError(txtCedulaFa, "");
                        errorProviderNombre.SetError(txtNombreFa, "Debe proporcionar un nombre válido.");
                        txtNombreFa.Focus();
                    }
                    else if (txtApellidoFa.Text == "" || txtApellidoFa.Text == "Apellido")
                    {
                        errorProviderNombre.SetError(txtNombreFa, "");
                        errorProviderApellido.SetError(txtApellidoFa, "Debe proporcionar un apelllido válido.");
                        txtApellidoFa.Focus();
                    }
                    else if(txtTelefonoFa.TextLength < 11)
                    {
                        errorProviderTlfn.SetError(txtTelefonoFa, "Debe proporcionar un teléfono válido.");
                        txtTelefonoFa.Focus();
                        errorProviderApellido.SetError(txtApellidoFa, "");
                    }
                    else if (txtEspecialidadFa.Text == "Especialidad" || txtEspecialidadFa.Text == "")
                    {
                        errorProviderTlfn.SetError(txtTelefonoFa, "");
                        errorProviderEspec.SetError(txtEspecialidadFa, "Debe proporcionar una especilidad.");
                        txtEspecialidadFa.Focus();
                    }
                    else if (cmbxINCE.SelectedIndex == -1)
                    {
                        errorProviderEspec.SetError(txtEspecialidadFa, "");

                        errorProviderCmbINCE.SetError(cmbxINCE, "Debe seleccionar una de las opciones.");

                    }
                    else if (cmbUbicacionEdo.SelectedIndex == -1)
                    {
                        errorProviderCmbINCE.SetError(cmbxINCE, "");

                        
                        errorProviderUbicacion.SetError(cmbUbicacionEdo, "Debe seleccionar la ubicación del facilitador.");
                    }
                    else //si todo Ok
                    {
                        errorProviderUbicacion.SetError(cmbUbicacionEdo, "");

                        facilitadores.id_facilitador = Clases.Facilitador_Seleccionado.id_facilitador;
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
                        

                        //ANTES DE ACTUALIZAR, VERIFICAR QUE NO EXISTAN DOS FACILITADORES CON LA MISMA CEDULA. FALTA VALIDACION!
                        if(txtCedulaFa.Text != Clases.Facilitador_Seleccionado.ci_facilitador)//si ha habido cambios entre lo que se cargó y lo que está en el txt
                        {
                            int existefa = Clases.Facilitadores.FacilitadorExiste(conexion.conexion, facilitadores.ci_facilitador, facilitadores.nacionalidad_fa);
                            conexion.cerrarconexion();
                            if (existefa == 0)
                            {
                                actualizarfa();
                            }
                            else
                            {
                                MessageBox.Show("Ya exite un facilitador con este número de cédula", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                txtCedulaFa.Text = Clases.Facilitador_Seleccionado.ci_facilitador.ToString();
                                if (Clases.Paneles.comprobarCedula(txtCedulaFa.Text) == true)
                                {
                                    cmbNacionalidad.SelectedIndex = 1;
                                }
                                else
                                {
                                    cmbNacionalidad.SelectedIndex = 0;
                                }
                            }
                        }else
                        {
                            actualizarfa();
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

        private void panel8_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
