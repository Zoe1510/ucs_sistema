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
    public partial class Agregar_participante : Form
    {
        string tipo_formacion, nombre_formacion, empresa_solicitante;
        int id_curso, id_cliente, id_ince;
        List<string> nombre = new List<string>();
        List<int> lista_id = new List<int>();
        Participantes participante = new Participantes();
        public Agregar_participante()
        {
            InitializeComponent();
        }

        private void Agregar_participante_Load(object sender, EventArgs e)
        {
            this.Location = new Point(-5, 0);
            cmbxTiposFormaciones.Focus();
            cmbxFormaciones.Enabled = false;
            cmbxEmpresaConInce.Enabled = false;
            gpbDatosParticipantes.Enabled = false;
        }

        /*---------------------------------EVENTOS PARTICIPANTES-----------------------------------*/
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
            if(txtCedulaPart.Text != "")
            {
                errorProviderCI.SetError(txtCedulaPart, "");
                participante.ci_participante = Convert.ToInt32(txtCedulaPart.Text);

                MySqlDataReader leer = Conexion.ConsultarBD("SELECT * FROM participantes WHERE cedula_par='" + participante.ci_participante + "'");
                if (leer.Read())
                {
                    participante.nombreP = Convert.ToString(leer["nombre_par"]);
                    participante.apellidoP = Convert.ToString(leer["apellido_par"]);
                    participante.correoP = Convert.ToString(leer["correo_par"]);
                    participante.cargoE = Convert.ToString(leer["cargoE"]);
                    participante.nivelE = Convert.ToString(leer["nivelE"]);
                    participante.id_participante = Convert.ToInt32(leer["id_participante"]);
                    participante.id_cli1 = Convert.ToInt32(leer["id_cli1"]);

                    MySqlDataReader existe = Conexion.ConsultarBD("select id_ctf from cursos_tienen_participantes where ctp_id_participante='" + participante.id_participante + "' and ctp_id_curso='" + id_curso + "'");
                    if (existe.Read())
                    {
                        errorProviderCI.SetError(txtCedulaPart, "El participante ya se encuentra registrado en esta formación.");
                        txtCedulaPart.Clear();
                        txtApellidoPart.Clear();
                        txtNombrePart.Clear();
                        txtCorreoPart.Text = "correo@ejemplo.com";
                        txtCargoEnEmpresa.Clear();
                        cmbNacionalidad.SelectedIndex = -1;
                        cmbxNivelEmpresa.SelectedIndex = -1;
                        txtCedulaPart.Focus();
                    }
                    else
                    {
                        errorProviderCI.SetError(txtCedulaPart, "");

                        if (tipo_formacion != "Abierto")
                        {
                            if (participante.id_cli1 != id_cliente)
                            {
                                errorProviderCI.SetError(txtCedulaPart, "La cédula se encuentra asociado con una empresa distinta a la seleccionada.");
                                txtCedulaPart.Clear();
                                txtApellidoPart.Clear();
                                txtNombrePart.Clear();
                                txtCorreoPart.Text = "correo@ejemplo.com";
                                txtCargoEnEmpresa.Clear();
                                cmbNacionalidad.SelectedIndex = -1;
                                cmbxNivelEmpresa.SelectedIndex = -1;
                                txtNombrePart.Enabled = true;
                                txtApellidoPart.Enabled = true;
                                txtCorreoPart.Enabled = true;
                                txtCedulaPart.Focus();

                            }
                            else
                            {
                                errorProviderCI.SetError(txtCedulaPart, "");
                                encontrado();

                            }
                        }
                        else
                        {
                            encontrado();
                        }
                    }
                    
                                       

                }
                else
                {
                    txtNombrePart.Enabled = true;
                    txtNombrePart.Focus();
                    txtApellidoPart.Enabled = true;
                    txtCorreoPart.Enabled = true;
                }
            }else
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
            Clases.Paneles.sololetras(e);
            if (txtApellidoPart.Text.Length == 0)
            {
                e.KeyChar = e.KeyChar.ToString().ToUpper().ToCharArray()[0];
            }
            else if (txtApellidoPart.Text.Length > 0)
            {
                e.KeyChar = e.KeyChar.ToString().ToLower().ToCharArray()[0];
            }
        }
        private void txtApellidoPart_Validating(object sender, CancelEventArgs e)
        {
            if(txtApellidoPart.Text != "")
            {
                errorProviderApellido.SetError(txtApellidoPart, "");
            }else
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
            if (Paneles.ComprobarFormatoEmail(txtCorreoPart.Text) == false || txtCorreoPart.Text=="correo@ejemplo.com")
            {
                errorProviderCorreo.SetError(txtCorreoPart, "Debe proporcionar un correo válido.");
                txtCorreoPart.Focus();
            }
            else
            {
                errorProviderCorreo.SetError(txtCorreoPart, "");
            }
        }

        

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            guardarParticipante();
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
        /*---------------------------------EVENTOS TIPO DE FORMACION-----------------------------------*/

        private void cmbxTiposFormaciones_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cmbxTiposFormaciones.SelectedIndex !=-1)
            {
                errorProviderTipoF.SetError(cmbxTiposFormaciones, "");
                int x = cmbxTiposFormaciones.SelectedIndex;
                switch (x)
                {
                    case 0:
                        tipo_formacion = "Abierto";
                        cmbxEmpresaConInce.SelectedIndex = -1;
                        cmbxFormaciones.SelectedIndex = -1;
                        cmbxEmpresaConInce.Enabled = false;
                        gpbDatosEmpresa.Enabled = false;
                        break;
                    case 1:
                        tipo_formacion = "FEE";
                        cmbxEmpresaConInce.SelectedIndex = -1;
                        cmbxFormaciones.SelectedIndex = -1;
                        cmbxEmpresaConInce.Enabled = true;
                        gpbDatosEmpresa.Enabled = false;
                        break;
                    case 2:
                        tipo_formacion = "INCES";
                        cmbxEmpresaConInce.SelectedIndex = -1;
                        cmbxFormaciones.SelectedIndex = -1;
                        cmbxEmpresaConInce.Enabled = true;
                        gpbDatosEmpresa.Enabled = true;
                        break;
                    case 3:
                        tipo_formacion = "InCompany";
                        cmbxEmpresaConInce.SelectedIndex = -1;
                        cmbxFormaciones.SelectedIndex = -1;
                        cmbxEmpresaConInce.Enabled = true;
                        gpbDatosEmpresa.Enabled = false;
                        break;
                }

                llenarcmbxFormaciones();
                cmbxFormaciones.Enabled = true;
                cmbxFormaciones.Focus();
            }
            else
            {
                errorProviderTipoF.SetError(cmbxTiposFormaciones, "Debe seleccionar un tipo de formación.");
                cmbxTiposFormaciones.Focus();

                cmbxFormaciones.Items.Clear();
                cmbxFormaciones.Enabled = false;

                cmbxEmpresaConInce.Items.Clear();
                cmbxEmpresaConInce.Enabled = false;
            }
        }

        private void cmbxTiposFormaciones_Validating(object sender, CancelEventArgs e)
        {
            if (cmbxTiposFormaciones.SelectedIndex == -1)
            {
                errorProviderTipoF.SetError(cmbxTiposFormaciones, "Debe seleccionar un tipo de formación.");
                cmbxTiposFormaciones.Focus();

                cmbxFormaciones.Items.Clear();
                cmbxFormaciones.Enabled = false;

                cmbxEmpresaConInce.Items.Clear();
                cmbxEmpresaConInce.Enabled = false;
            }else
            {
                errorProviderTipoF.SetError(cmbxTiposFormaciones, "");
                cmbxFormaciones.Focus();
            }
        }

        private void cmbxFormaciones_SelectionChangeCommitted(object sender, EventArgs e)
        {
            id_curso = Convert.ToInt32(cmbxFormaciones.SelectedValue);
            nombre_formacion = Convert.ToString(cmbxFormaciones.Text);
           
            
            if (cmbxTiposFormaciones.SelectedIndex == 2)
            {
                MySqlDataReader idince = Conexion.ConsultarBD("SELECT id_curso_ince FROM cursos_inces WHERE nombre_curso_ince='" + nombre_formacion + "'");
               
                if (idince.Read())
                {
                    id_ince = Convert.ToInt32(idince["id_curso_ince"]);
                }
                idince.Close();
                llenarcmbxEmpresaInce(id_ince);

            }
            else if(tipo_formacion == "FEE")
            {
                llenarcmbxFEE();                
            }
            else if (tipo_formacion == "InCompany")
            {

            }
            else if (tipo_formacion == "Abierto") //si la formación es de tipo abierto
            {
                gpbDatosParticipantes.Enabled = true;
                cmbxEmpresaConInce.Enabled = false;
                txtCedulaPart.Enabled = true;
                txtCedulaPart.Focus();
            }
            cmbxEmpresaConInce.Focus();
        }
        private void cmbxEmpresaConInce_SelectionChangeCommitted(object sender, EventArgs e)
        {

            id_cliente = Convert.ToInt32(cmbxEmpresaConInce.SelectedValue);
            participante.nombreE = Convert.ToString(cmbxEmpresaConInce.Text);
            //recoger el id del cliente y el nombre
            gpbDatosParticipantes.Enabled = true;
            txtCedulaPart.Enabled = true;
            cmbNacionalidad.Enabled = true;
            participante.id_cli1 = id_cliente;
            txtCedulaPart.Focus();

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
            }else
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
            if(txtCargoEnEmpresa.Text == "")
            {
                errorProvider1.SetError(txtCargoEnEmpresa, "Debe proporcionar un cargo válido.");
                txtCargoEnEmpresa.Focus();
            }else
            {
                errorProvider1.SetError(txtCargoEnEmpresa, "");
            }
        }
        /*-------------------------------METODOS----------------------------------------*/

        private List<Empresa> SeleccionarInce(int id)
        {
            
            List<Empresa> lista = new List<Empresa>();
            MySqlDataReader leer = Conexion.ConsultarBD("SELECT id_cliente1, nombre_empresa FROM clientes_solicitan_cursos csc inner join clientes cli on csc.id_cliente1 = cli.id_clientes where csc.id_cursoInce = '" + id + "'");
            while (leer.Read())
            {
                
                Empresa e = new Empresa();
                e.id_clientes = Convert.ToInt32(leer["id_cliente1"]);
                e.nombre_empresa = Convert.ToString(leer["nombre_empresa"]);
                lista.Add(e);
            }
            return lista;
        }

        private void llenarcmbxEmpresaInce(int id)
        {
            cmbxEmpresaConInce.ValueMember = "id_clientes";
                cmbxEmpresaConInce.DisplayMember = "nombre_empresa";
                cmbxEmpresaConInce.DataSource = SeleccionarInce(id);
                cmbxEmpresaConInce.SelectedIndex = -1;
            
        }

        private List<Formaciones> SeleccionFormacion()
        {
            string tipo = tipo_formacion;
            int nivel = 1;
            List<Formaciones> lista = new List<Formaciones>();
            MySqlDataReader formacion = Conexion.ConsultarBD("SELECT * FROM cursos WHERE tipo_curso= '" + tipo + "' AND etapa_curso >'" + nivel + "'");
            while (formacion.Read())
            {
                Formaciones c = new Formaciones();
                c.id_curso = Convert.ToInt32(formacion["id_cursos"]);
                c.nombre_formacion = Convert.ToString(formacion["nombre_curso"]);
                lista.Add(c);
            }

            return lista;
        }        

        private void llenarcmbxFormaciones()
        {
            if (SeleccionFormacion().Count != 0)
            {
                errorProviderNombreF.SetError(cmbxFormaciones, "");
                cmbxFormaciones.ValueMember = "id_curso";
                cmbxFormaciones.DisplayMember = "nombre_formacion";
                cmbxFormaciones.DataSource = SeleccionFormacion();
                cmbxFormaciones.SelectedIndex = -1;
            }else
            {
                cmbxFormaciones.Enabled = false;
                errorProviderNombreF.SetError(cmbxFormaciones, "No hay formaciones de este tipo en el nivel intermedio.");
                cmbxTiposFormaciones.Focus();
            }
            
        }

       
        private List<Empresa> SeleccionarFEE()
        {
            List<Empresa> lista = new List<Empresa>();           

            MySqlDataReader leer = Conexion.ConsultarBD("SELECT * FROM clientes WHERE fee_empresa='" + 1 + "'");
            while (leer.Read())
            {
                Empresa ex = new Empresa();
                ex.id_clientes = Convert.ToInt32(leer["id_clientes"]);
                ex.nombre_empresa = Convert.ToString(leer["nombre_empresa"]);
                lista.Add(ex);
            }
                     

            return lista;
        }

        private void llenarcmbxFEE()
        {
            cmbxEmpresaConInce.ValueMember = "id_clientes";
            cmbxEmpresaConInce.DisplayMember = "nombre_empresa";
            cmbxEmpresaConInce.DataSource = SeleccionarFEE();
            cmbxEmpresaConInce.SelectedIndex = -1;

        }

        private void guardarParticipante()
        {
            try
            {
                if(tipo_formacion=="" || cmbxTiposFormaciones.SelectedIndex == -1)
                {
                    errorProviderTipoF.SetError(cmbxTiposFormaciones, "Debe seleccionar un tipo de formación.");
                    cmbxTiposFormaciones.Focus();
                }else
                {
                    errorProviderTipoF.SetError(cmbxTiposFormaciones, "");
                    if(nombre_formacion=="" || cmbxFormaciones.SelectedIndex == -1)
                    {
                        errorProviderNombreF.SetError(cmbxFormaciones, "Debe seleccionar una formación.");
                        cmbxFormaciones.Focus();
                    } else
                    {
                        errorProviderNombreF.SetError(cmbxFormaciones, "");
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
                                        if(participante.id_participante > 0)//lo que significa, que ya se encuentra registrado en el sistema
                                        {
                                            //falta una validacion, si el participante ya se encuentra regiustrado en el curso
                                            MySqlDataReader leer = Conexion.ConsultarBD("INSERT INTO cursos_tienen_participantes (ctp_id_participante, ctp_id_curso, ctp_id_cliente) VALUES ('"+participante.id_participante+"', '"+id_curso+"', '"+participante.id_cli1+"')");
                                            MessageBox.Show("Participante añadido correctamente.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            leer.Close();
                                        } else
                                        {
                                            participante.ci_participante = Convert.ToInt32(txtCedulaPart.Text);
                                            participante.nombreP = txtNombrePart.Text;
                                            participante.apellidoP = txtApellidoPart.Text;
                                            participante.correoP = txtCorreoPart.Text;
                                            string n="";
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


                                            if (tipo_formacion == "Abierto")
                                            {
                                                participante.cargoE = "";
                                                participante.nivelE = "";
                                                int id_cli = 0;
                                                participante.id_cli1 = 0;
                                                participante.nombreE = "No asociado";
                                                MySqlDataReader leer = Conexion.ConsultarBD("INSERT INTO participantes (nacionalidad, cedula_par, nombre_par, apellido_par, correo_par, cargoE, nivelE, id_cli1, nombreE) VALUES ('"+participante.nacionalidad+"','" + participante.ci_participante + "', '" + participante.nombreP + "', '" + participante.apellidoP + "', '" + participante.correoP + "', '" + participante.cargoE + "', '" + participante.nivelE + "', '" + id_cli + "', '"+participante.nombreE+"')");
                                                MessageBox.Show("Participante añadido correctamente.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                leer.Close();

                                                agregarP2();
                                            }
                                            else if (tipo_formacion == "FEE")
                                            {
                                                participante.cargoE = "";
                                                participante.nivelE = "";

                                                agregarP();

                                                agregarP2();
                                            }
                                            else if (tipo_formacion == "INCES")
                                            {
                                                if(cmbxEmpresaConInce.SelectedIndex != -1)
                                                {
                                                    errorProviderNombreE.SetError(cmbxEmpresaConInce, "");                                                    
                                                    participante.cargoE = txtCargoEnEmpresa.Text;
                                                    agregarP();
                                                    agregarP2();
                                                }
                                                else
                                                {
                                                    errorProviderNombreE.SetError(cmbxEmpresaConInce, "Debe seleccionar una empresa o cliente.");
                                                    cmbxEmpresaConInce.Focus();
                                                }
                                            }
                                            else if (tipo_formacion == "InCompany")
                                            {
                                                participante.cargoE = "";
                                                participante.nivelE = "";

                                                agregarP();
                                                agregarP2();
                                            }
                                            
                                        }
                                                                               

                                    }
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
        private void encontrado()
        {
            txtNombrePart.Text = participante.nombreP;
            txtApellidoPart.Text = participante.apellidoP;
            txtCorreoPart.Text = participante.correoP;
            txtNombrePart.Enabled = false;
            txtApellidoPart.Enabled = false;
            txtCorreoPart.Enabled = false;

            if (participante.cargoE != "")
            {
                txtCargoEnEmpresa.Text = participante.cargoE;
                txtCargoEnEmpresa.Enabled = false;
                switch (participante.nivelE)
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
                cmbxNivelEmpresa.Enabled = false;
            }
        }
        private void agregarP()
        {
            MySqlDataReader leer = Conexion.ConsultarBD("INSERT INTO participantes (nacionalidad, cedula_par, nombre_par, apellido_par, correo_par, cargoE, nivelE, id_cli1, nombreE) VALUES ('"+participante.nacionalidad+"','" + participante.ci_participante + "', '" + participante.nombreP + "', '" + participante.apellidoP + "', '" + participante.correoP + "', '" + participante.cargoE + "', '" + participante.nivelE + "', '" + id_cliente + "', '"+participante.nombreE+"')");
            MessageBox.Show("Participante añadido correctamente.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information);
            leer.Close();
        }
        private void agregarP2()
        {
            MySqlDataReader read = Conexion.ConsultarBD("SELECT id_participante FROM participantes WHERE cedula_par='" + participante.ci_participante + "'");
            if (read.Read())
            {
                participante.id_participante = Convert.ToInt32(read["id_participante"]);

            }
            read.Close();
            MySqlDataReader leer1 = Conexion.ConsultarBD("INSERT INTO cursos_tienen_participantes (ctp_id_participante, ctp_id_curso, ctp_id_cliente) VALUES ('" + participante.id_participante + "', '" + id_curso + "', '"+participante.id_cli1+"')");

            leer1.Close();
        }

        
    }
}
