using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UCS_NODO_FGC.Clases;
using MySql.Data.MySqlClient;

namespace UCS_NODO_FGC
{
    public partial class Modificar_participante : Form
    {
        Participantes participante = new Participantes();
        public Modificar_participante()
        {
            InitializeComponent();
        }

        private void Modificar_participante_Load(object sender, EventArgs e)
        {
            llenarcmbxEmpresaInce();
            
            if (Participante_seleccionado.nacionalidad == "V")
            {
                cmbNacionalidad.SelectedIndex = 0;
            }else
            {
                cmbNacionalidad.SelectedIndex = 1;
            }
            txtCedulaPart.Text=Participante_seleccionado.ci_participante.ToString();           
            txtNombrePart.Text = Participante_seleccionado.nombreP;
            txtApellidoPart.Text = Participante_seleccionado.apellidoP;
            txtCorreoPart.Text = Participante_seleccionado.correoP;
            if(Participante_seleccionado.id_cli1 != 0)
            {
                txtCargoEnEmpresa.Text = Participante_seleccionado.cargoE;
                switch (Participante_seleccionado.nivelE)
                {
                    case "A":
                        cmbxNivelEmpresa.SelectedIndex = 0;
                        break;
                    case "B":
                        cmbxNivelEmpresa.SelectedIndex = 1;
                        break;
                    case "C":
                        cmbxNivelEmpresa.SelectedIndex = 2;
                        break;
                    case "D":
                        cmbxNivelEmpresa.SelectedIndex = 3;
                        break;
                }

                cmbxEmpresa.Text = Participante_seleccionado.nombreE;
                gpbDatosEmpresa.Enabled = true;
                cmbxEmpresa.Enabled = true;
                cmbxNivelEmpresa.Enabled = true;
                txtCargoEnEmpresa.Enabled = true;
            }else
            {
               
                gpbDatosEmpresa.Enabled = false;
                cmbxEmpresa.SelectedIndex = -1;
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
        private void txtCedulaPart_KeyPress(object sender, KeyPressEventArgs e)
        {
            Paneles.solonumeros(e);
        }
        private void txtCedulaPart_Validating(object sender, CancelEventArgs e)
        {
            if (Clases.Paneles.comprobarCedula(txtCedulaPart.Text) == true)
            {
                cmbNacionalidad.SelectedIndex = 1;

            }
            else
            {
                cmbNacionalidad.SelectedIndex = 0;
            }
            if (txtCedulaPart.Text != "")
            {
                errorProviderCI.SetError(txtCedulaPart, "");
                participante.ci_participante = Convert.ToInt32(txtCedulaPart.Text);

                MySqlDataReader leer = Conexion.ConsultarBD("SELECT * FROM participantes WHERE cedula_par='" + participante.ci_participante + "'");
                if (leer.Read())
                {
                    if(participante.ci_participante != Participante_seleccionado.ci_participante)
                    {
                        errorProviderCI.SetError(txtCedulaPart, "Este número de cédula ya se encuentra registrada.");
                        txtCedulaPart.Focus();
                    }else
                    {
                        errorProviderCI.SetError(txtCedulaPart, "");
                    }
                    
                  
                }
               
            }
            else
            {
                errorProviderCI.SetError(txtCedulaPart, "Debe proporcionar un número de cédula válido");
                txtCedulaPart.Focus();
            }

        }

        private void txtNombrePart_KeyPress(object sender, KeyPressEventArgs e)
        {
            Clases.Paneles.sololetras(e);
            if (txtNombrePart.Text.Length == 0)
            {
                e.KeyChar = e.KeyChar.ToString().ToUpper().ToCharArray()[0];
            }
            else if (txtNombrePart.Text.Length > 0)
            {
                e.KeyChar = e.KeyChar.ToString().ToLower().ToCharArray()[0];
            }
        }
        private void txtNombrePart_Validating(object sender, CancelEventArgs e)
        {
            if (txtNombrePart.Text != "")
            {
                errorProviderNombre.SetError(txtNombrePart, "");
            }
            else
            {
                errorProviderNombre.SetError(txtNombrePart, "Debe proporcionar un nombre válido.");
                txtNombrePart.Focus();
            }
        }

        private void txtApellidoPart_KeyPress(object sender, KeyPressEventArgs e)
        {

        }
        private void txtApellidoPart_Validating(object sender, CancelEventArgs e)
        {
            if (txtApellidoPart.Text != "")
            {
                errorProviderApellido.SetError(txtApellidoPart, "");
            }
            else
            {
                errorProviderApellido.SetError(txtApellidoPart, "Debe proporcionar un apellido válido.");
                txtApellidoPart.Focus();
            }
        }

        private void txtCorreo_Leave(object sender, EventArgs e)
        {
            if (txtCorreoPart.Text == "")
            {
                txtCorreoPart.Text = "correo@ejemplo.com";
            }
        }
        private void txtCorreo_KeyDown(object sender, KeyEventArgs e)
        {
            while (txtCorreoPart.Text == "correo@ejemplo.com")
            {
                txtCorreoPart.Text = "";
            }
        }
        private void txtCorreo_Click(object sender, EventArgs e)
        {

            if (txtCorreoPart.Text != "correo@ejemplo.com")
            {

            }
            else
            {
                txtCorreoPart.Text = "";
            }
        }
        private void txtCorreo_Validating(object sender, CancelEventArgs e)
        {
            if (Paneles.ComprobarFormatoEmail(txtCorreoPart.Text) == false || txtCorreoPart.Text == "correo@ejemplo.com")
            {
                errorProviderCorreo.SetError(txtCorreoPart, "Debe proporcionar un correo válido.");
                txtCorreoPart.Focus();
            }
            else
            {
                errorProviderCorreo.SetError(txtCorreoPart, "");
            }
        }

        private void cmbxEmpresaConInce_SelectionChangeCommitted(object sender, EventArgs e)
        {

            participante.id_cli1 = Convert.ToInt32(cmbxEmpresa.SelectedValue);
            participante.nombreE = Convert.ToString(cmbxEmpresa.Text);
            //recoger el id del cliente y el nombre
            
        }

        private void cmbxNivelEmpresa_SelectionChangeCommitted(object sender, EventArgs e)
        {
            
            switch (cmbxNivelEmpresa.SelectedIndex)
            {
                case 0:
                    participante.nivelE = "A";
                    break;
                case 1:
                    participante.nivelE = "B";
                    break;
                case 2:
                    participante.nivelE = "C";
                    break;
                case 3:
                    participante.nivelE = "D";
                    break;

            }
        }

        private void cmbxNivelEmpresa_Validating(object sender, CancelEventArgs e)
        {
            if (cmbxNivelEmpresa.SelectedIndex == -1)
            {
                errorProvider2.SetError(cmbxNivelEmpresa, "Debe seleccionar un nivel válido.");
                cmbxNivelEmpresa.Focus();
            }
            else
            {
                errorProvider2.SetError(cmbxNivelEmpresa, "");
            }
        }

        private void txtCargoEnEmpresa_KeyPress(object sender, KeyPressEventArgs e)
        {
            Clases.Paneles.sololetras(e);
            if (txtNombrePart.Text.Length == 0)
            {
                e.KeyChar = e.KeyChar.ToString().ToUpper().ToCharArray()[0];
            }
            else if (txtNombrePart.Text.Length > 0)
            {
                e.KeyChar = e.KeyChar.ToString().ToLower().ToCharArray()[0];
            }
        }
        private void txtCargoEnEmpresa_Validating(object sender, CancelEventArgs e)
        {
            if (txtCargoEnEmpresa.Text == "")
            {
                errorProvider1.SetError(txtCargoEnEmpresa, "Debe proporcionar un cargo válido.");
                txtCargoEnEmpresa.Focus();
            }
            else
            {
                errorProvider1.SetError(txtCargoEnEmpresa, "");
            }
        }
        private List<Empresa> SeleccionarInce()
        {

            List<Empresa> lista = new List<Empresa>();
            MySqlDataReader leer = Conexion.ConsultarBD("SELECT id_clientes, nombre_empresa FROM clientes");
            while (leer.Read())
            {

                Empresa e = new Empresa();
                e.id_clientes = Convert.ToInt32(leer["id_clientes"]);
                e.nombre_empresa = Convert.ToString(leer["nombre_empresa"]);
                lista.Add(e);
            }
            return lista;
        }

        private void llenarcmbxEmpresaInce()
        {
            cmbxEmpresa.ValueMember = "id_clientes";
            cmbxEmpresa.DisplayMember = "nombre_empresa";
            cmbxEmpresa.DataSource = SeleccionarInce();
            //cmbxEmpresa.SelectedIndex = -1;

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                //la validacion del cmbxempresa va luego para cada quien, se prosigue con los demás campos
                if (txtCedulaPart.Text == "")
                {
                    errorProviderCI.SetError(txtCedulaPart, "Debe proporcionar un número de cédula válido");
                    txtCedulaPart.Focus();
                }
                else
                {
                    errorProviderCI.SetError(txtCedulaPart, "");
                    if (txtNombrePart.Text == "")
                    {
                        errorProviderNombre.SetError(txtNombrePart, "Debe proporcionar un nombre válido.");
                        txtNombrePart.Focus();
                    }
                    else
                    {
                        errorProviderNombre.SetError(txtNombrePart, "");
                        if (txtApellidoPart.Text == "")
                        {
                            errorProviderApellido.SetError(txtApellidoPart, "Debe proporcionar un apellido válido.");
                            txtApellidoPart.Focus();
                        }
                        else
                        {
                            errorProviderApellido.SetError(txtApellidoPart, "");
                            if (txtCorreoPart.Text == "" || txtCorreoPart.Text == "correo@ejemplo.com")
                            {
                                errorProviderCorreo.SetError(txtCorreoPart, "Debe proporcionar un correo válido.");
                                txtCorreoPart.Focus();
                            }
                            else
                            {
                                errorProviderCorreo.SetError(txtCorreoPart, "");
                                if (Participante_seleccionado.id_cli1 != 0)
                                {
                                    if (cmbxEmpresa.Text != Participante_seleccionado.nombreE)
                                    {
                                        if (cmbxEmpresa.SelectedIndex == -1)
                                        {
                                            errorProviderTipoF.SetError(cmbxEmpresa, "Debe seleccionar una empresa para asociar.");
                                            cmbxEmpresa.Focus();
                                        }
                                        else
                                        {
                                            errorProviderTipoF.SetError(cmbxEmpresa, "");
                                            participante.nombreE = cmbxEmpresa.Text;
                                           
                                        }
                                        
                                    }
                                    else
                                    {
                                        participante.nombreE = Participante_seleccionado.nombreE;
                                        participante.id_cli1 = Participante_seleccionado.id_cli1;
                                    }
                                    
                                    if (cmbxNivelEmpresa.Text != Participante_seleccionado.nivelE)
                                    {
                                       
                                        if (cmbxNivelEmpresa.SelectedIndex == -1)
                                        {
                                            errorProvider2.SetError(cmbxNivelEmpresa, "Debe seleccionar un nivel.");
                                            cmbxNivelEmpresa.Focus();
                                        }
                                        else
                                        {
                                            errorProvider2.SetError(cmbxNivelEmpresa, "");
                                            participante.nivelE = cmbxNivelEmpresa.Text;
                                        }
                                    }
                                    else
                                    {
                                        participante.nivelE = Participante_seleccionado.nivelE;
                                    }

                                    if (txtCargoEnEmpresa.Text != Participante_seleccionado.cargoE)
                                    {
                                        
                                        if (txtCargoEnEmpresa.Text == "")
                                        {
                                            errorProvider1.SetError(txtCargoEnEmpresa, "Debe proporcionar el cargo del participante.");
                                            txtCargoEnEmpresa.Focus();
                                        }
                                        else
                                        {
                                            errorProvider1.SetError(txtCargoEnEmpresa, "");
                                            participante.cargoE = txtCargoEnEmpresa.Text;
                                        }
                                    }
                                    else
                                    {
                                        participante.cargoE = Participante_seleccionado.cargoE;
                                    }
                                    
                                    
                                }
                                else//se validó si es que el participante está asociado
                                {
                                    participante.id_cli1 = 0;
                                    participante.nombreE = "No asociado";
                                    participante.nivelE = "";
                                    participante.cargoE = "";
                                }
                                
                                participante.ci_participante = Convert.ToInt32(txtCedulaPart.Text);
                                participante.nombreP = txtNombrePart.Text;
                                participante.apellidoP = txtApellidoPart.Text;
                                participante.correoP = txtCorreoPart.Text;
                                string n = "";
                                switch (cmbNacionalidad.SelectedIndex)
                                {
                                    case 0:
                                        n = "V";
                                        break;
                                    case 1:
                                        n = "E";
                                        break;
                                }
                                participante.nacionalidad = n;
                                //procede a actualizar:
                                if (txtCedulaPart.Text != Participante_seleccionado.ci_participante.ToString() || txtNombrePart.Text != Participante_seleccionado.nombreP || txtApellidoPart.Text!= Participante_seleccionado.apellidoP || txtCorreoPart.Text != Participante_seleccionado.correoP || cmbxEmpresa.Text != Participante_seleccionado.nombreE || txtCargoEnEmpresa.Text != Participante_seleccionado.cargoE || cmbxNivelEmpresa.Text != Participante_seleccionado.nivelE)
                                {
                                    //si el participante ya se encuentra regiustrado en el curso
                                    MySqlDataReader existe = Conexion.ConsultarBD("select id_participante from participantes where cedula_par='" + participante.ci_participante + "'");
                                    if (existe.Read())
                                    {
                                        errorProviderCI.SetError(txtCedulaPart, "Este número de cédula ya se encuentra registrada.");                                
                                        txtCedulaPart.Focus();
                                    }
                                    else
                                    {
                                        errorProviderCI.SetError(txtCedulaPart, "");
                                        MySqlDataReader update = Conexion.ConsultarBD("UPDATE participantes SET cedula_par='" + participante.ci_participante + "', nacionalidad='" + participante.nacionalidad + "', nombre_par='" + participante.nombreP + "', apellido_par='" + participante.apellidoP + "', correo_par='" + participante.correoP + "', nivelE='" + participante.nivelE + "', cargoE='" + participante.cargoE + "', nombreE='" + participante.nombreE + "', id_cli1='" + participante.id_cli1 + "' WHERE id_participante='" + Participante_seleccionado.id_participante + "'");
                                        update.Close();
                                        if (MessageBox.Show("Los datos fueron actualizados correctamente.", "", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
                                        {
                                            this.Close();
                                            cmbxEmpresa.SelectedIndex = -1;
                                        }
                                    }
                                   

                                }
                                else
                                {
                                    MessageBox.Show("No se ha detectado cambios.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                }

                            }
                        }
                    }
                }
            
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        //private void calculo()
        //{
        //    /* Recibiendo 4 variables (a,b,c,d) , para cada par, hay tres casos disponibles:
        //     * 1) a < b  |  c < d
        //     * 2) a > b  |  c > d
        //     * 3) a = b  |  c = d */
        //    int a=0, b=0, c=0, d=0;
        //    int i=0, j=0, k=0, m=0; //estos serán variables auxiliares

        //    if(a <= b) // si a es menor o igual a b
        //    {
        //        i = a;
        //        m = b;
        //    }else // caso contrario si b es menor
        //    {
        //        i = b;
        //        m = a;
        //    }

        //    if (c <= b) //si c es menor o igual a d
        //    {
        //        j = c;
        //        k = d;
        //    }else  // caso contraio si d es menor
        //    {
        //        j = d;
        //        k = c;
        //    }

        //    for(int num =i; num<=m; num++)
        //    {
        //        for(int n2=j; n2<=k; n2++)
        //        {
        //            MessageBox.Show("'" + num + "' x '" + n2 + "' = '" + num * n2 + "'");
        //            MessageBox.Show(num.ToString());
        //            /* Mostrar 'num' x 'n2'= 'num*n2' */
        //        }
        //    }
        //}
    }
}
