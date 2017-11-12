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
    public partial class Pagina_principal : Form
    {
        private Form _objForm;
        public Clases.conexion_bd conexion = new Clases.conexion_bd();

        public Pagina_principal()
        {
            InitializeComponent();
        }

        //funcionpara abrir un form, establecido por un btn del panel en el panel principal
        public void AddFormInPanel(object formHijo)
        {
            if (this.pnlPanelDisplay.Controls.Count > 0)
                this.pnlPanelDisplay.Controls.RemoveAt(0);
             Form fh = formHijo as Form;
            fh.TopLevel = false;
            fh.FormBorderStyle = FormBorderStyle.None;
            fh.Dock = DockStyle.None;
            this.pnlPanelDisplay.Controls.Add(fh);
            this.pnlPanelDisplay.Tag = fh;
            fh.Show();
            _objForm = fh;

        }

        private void Pagina_principal_Load(object sender, EventArgs e)
        {
            //permite que la imagen sea redonda
            System.Drawing.Drawing2D.GraphicsPath gp = new System.Drawing.Drawing2D.GraphicsPath();
            gp.AddEllipse(0, 0, picFotoUser.Width - 3, picFotoUser.Height - 3);
            Region rg = new Region(gp);
            picFotoUser.Region = rg;
            
            if(Clases.Usuario_logeado.imagen_usuario != null)
            {
                picFotoUser.Image =Clases.Helper.ByteArrayToImage(Clases.Usuario_logeado.imagen_usuario);
            }
            else
            {
                MessageBox.Show("No se ha encontrado ninguna foto en la base de datos");
            }
            //nombre del usuario
            lblLabelNombre_usuario.Text = Clases.Usuario_logeado.nombre_usuario;

            
            //si el usuario es nuevo:
            if (Clases.Usuario_logeado.password=="12345678")
            {
                MessageBox.Show("Debe actualizar sus datos");
                AddFormInPanel(new Perfil_profesional());
                
            }

            

           
            //funcion general: cerrar sesion, edital perfil (distinto del formuario principal para actualzar datos),
        }

        public void menudown()
        {
            //boton facilitadores
            if (pnlBtnFormaciones.Height == 48)
            {

                pnlBtnFacilitadores.Location = new Point(0, Convert.ToInt32(pnlBtnFormaciones.Location.Y) + 48);
            }
            else
            {

                pnlBtnFacilitadores.Location = new Point(0, Convert.ToInt32(pnlBtnFormaciones.Location.Y) + 318);
            }

            //boton clientes
            if (pnlBtnFacilitadores.Height == 48)
            {

                pnlBtnClientes.Location = new Point(0, Convert.ToInt32(pnlBtnFacilitadores.Location.Y) + 48);
            }
            else
            {

                pnlBtnClientes.Location = new Point(0, Convert.ToInt32(pnlBtnFacilitadores.Location.Y) + 163);
            }

            //boton participantes
            if (pnlBtnClientes.Height == 48)
            {

                pnlBtnParticipantes.Location = new Point(0, Convert.ToInt32(pnlBtnClientes.Location.Y) + 48);
            }
            else
            {

                pnlBtnParticipantes.Location = new Point(0, Convert.ToInt32(pnlBtnClientes.Location.Y) + 203);
            }

            //boton usuarios
            if (pnlBtnParticipantes.Height == 48)
            {

                pnlBtnUsuario.Location = new Point(0, Convert.ToInt32(pnlBtnParticipantes.Location.Y) + 48);
            }
            else
            {
                pnlBtnUsuario.Location = new Point(0, Convert.ToInt32(pnlBtnParticipantes.Location.Y) + 157);
            }

            //boton logistica
            if(pnlBtnUsuario.Height == 48)
            {
                pnlBtnLogistica.Location= new Point(0, Convert.ToInt32(pnlBtnUsuario.Location.Y) + 48);
            }
            else
            {
                pnlBtnLogistica.Location = new Point(0, Convert.ToInt32(pnlBtnUsuario.Location.Y) + 163);
            }
            //boton reportes
            if (pnlBtnLogistica.Height ==48)
            {

                pnlBtnReportes.Location = new Point(0, Convert.ToInt32(pnlBtnLogistica.Location.Y) + 48);
            }
            else
            {

                pnlBtnReportes.Location = new Point(0, Convert.ToInt32(pnlBtnLogistica.Location.Y) + 319);
            }
            //boton fromatos
            if (pnlBtnReportes.Height == 162)
            {
                pnlBtnFormatos.Location = new Point(0, Convert.ToInt32(pnlBtnReportes.Location.Y) + 162);
               
            }
            else
            {
                pnlBtnFormatos.Location = new Point(0, Convert.ToInt32(pnlBtnReportes.Location.Y) + 48);
               
            }
            //boton final
            if (pnlBtnFormatos.Height == 48)
            {
                pnlBtnFinal.Location = new Point(0, Convert.ToInt32(pnlBtnFormatos.Location.Y) + 48);
            }
            else
            {
                pnlBtnFinal.Location = new Point(0, Convert.ToInt32(pnlBtnFormatos.Location.Y) + 105);
            }
        }
        private void btn_cerrar_Click(object sender, EventArgs e)
        {
       
            Clases.Paneles.boton_cerrar();
        }

        
        private void btn_minimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        public void btnDespliegueMenu_Click(object sender, EventArgs e)
        {
            menu_size();
        }
        //falta hacer una validacion si no hay frm abiertos en paneldisplay
        public void menu_size()
        {
            Form obj = new Form();

            if (pnlPanelContenedorMenu.Width == 245)
            {
                pnlPanelContenedorMenu.Width = 44;
                btnDespliegueMenu.Left = 7;
                rsLineaSeparadora.Visible = false;
               if (_objForm!= null)
                {

                    ////_objForm.Dock = DockStyle.Left;
                    
                    obj.Width = _objForm.Width;
                   // _objForm.Width = pnlPanelDisplay.Width;
                   _objForm.Dock = DockStyle.Fill;
                }
                
                this.AutoScroll = false;

            }
            else
            {
                pnlPanelContenedorMenu.Width = 245;
                btnDespliegueMenu.Left = 204;
                rsLineaSeparadora.Visible = true;
                if (_objForm != null)
                {
                   // _objForm.Width = obj.Width;
                    _objForm.Dock = DockStyle.Right;
                   // _objForm.Width = pnlPanelDisplay.Width;
                   
                }
                   
            }
        }

        private void btnEditarPerfil_Click(object sender, EventArgs e)
        {
            AddFormInPanel(new Perfil_personal());
        }
        
        private void btnFormaciones_MouseClick(object sender, MouseEventArgs e)
        {

            if (pnlBtnFormaciones.Height == 48)
            {
                pnlBtnFormaciones.Height = 317;
                btnFormaciones.BackColor = Color.FromArgb(128, 128, 255);
                btnFacilitadores.BackColor = Color.FromArgb(128, 128, 255);
                menudown();
            }
            else
            {
                pnlBtnFormaciones.Height = 48;
                btnFormaciones.BackColor = Color.Transparent;
                btnFacilitadores.BackColor = Color.Transparent;
                menudown();

            }

        }

        private void btnFacilitadores_Click(object sender, EventArgs e)
        {
            if (pnlBtnFacilitadores.Height == 48)
            {
                pnlBtnFacilitadores.Height = 162;
                btnFacilitadores.BackColor = Color.FromArgb(128, 128, 255);
                btnClientes.BackColor = Color.FromArgb(128, 128, 255);
                menudown();
            }
            else
            {
                pnlBtnFacilitadores.Height = 48;
                btnFacilitadores.BackColor = Color.Transparent;
                btnClientes.BackColor = Color.Transparent;
                menudown();

            }
        }

        private void btnClientes_Click(object sender, EventArgs e)
        {
            if (pnlBtnClientes.Height == 48)
            {
                pnlBtnClientes.Height = 200;
                btnClientes.BackColor = Color.FromArgb(128, 128, 255);
                btnParticipante.BackColor = Color.FromArgb(128, 128, 255);
                menudown();
            }
            else
            {
                pnlBtnClientes.Height = 48;
                btnClientes.BackColor = Color.Transparent;
                btnParticipante.BackColor = Color.Transparent;
                menudown();

            }

        }

        private void btnParticipante_Click(object sender, EventArgs e)
        {
            if (pnlBtnParticipantes.Height == 48)
            {
                pnlBtnParticipantes.Height = 156;
                btnParticipante.BackColor = Color.FromArgb(128, 128, 255);
                btnUsuario.BackColor = Color.FromArgb(128, 128, 255);
                menudown();
            }
            else
            {
                pnlBtnParticipantes.Height = 48;
                btnParticipante.BackColor = Color.Transparent;
                btnUsuario.BackColor = Color.Transparent;
                menudown();
            }

        }

        private void btnUsuario_Click(object sender, EventArgs e)
        {
            if (pnlBtnUsuario.Height == 48)
            {
                pnlBtnUsuario.Height = 162;
                btnUsuario.BackColor = Color.FromArgb(128, 128, 255);
                btnLogistica.BackColor = Color.FromArgb(128, 128, 255);
                menudown();
            }
            else
            {
                pnlBtnUsuario.Height = 48;
                btnUsuario.BackColor = Color.Transparent;
                btnLogistica.BackColor = Color.Transparent;
                menudown();
            }
        }
        private void btnLogistica_Click(object sender, EventArgs e)
        {
            if (pnlBtnLogistica.Height == 48)
            {
                pnlBtnLogistica.Height = 318;
                btnLogistica.BackColor = Color.FromArgb(128, 128, 255);
                btnReportes.BackColor = Color.FromArgb(128, 128, 255);
                menudown();
            }
            else
            {
                pnlBtnLogistica.Height = 48;
                btnReportes.BackColor = Color.Transparent;
                btnLogistica.BackColor = Color.Transparent;
                menudown();
            }
        }
        private void btnReportes_Click(object sender, EventArgs e)
        {
            if (pnlBtnReportes.Height == 48)
            {
                pnlBtnReportes.Height = 162;
                btnReportes.BackColor = Color.FromArgb(128, 128, 255);
                btnFormatos.BackColor = Color.FromArgb(128, 128, 255);
                menudown();
            }
            else
            {
                pnlBtnReportes.Height = 48;
                btnReportes.BackColor = Color.Transparent;
                btnFormatos.BackColor = Color.Transparent;
                menudown();
            }

        }

       
        private void btnFormatos_Click(object sender, EventArgs e)
        {
            if (pnlBtnFormatos.Height == 48)
            {
                pnlBtnFormatos.Height = 104;
                btnFormatos.BackColor = Color.FromArgb(128, 128, 255);
                btnFrmAyuda.BackColor = Color.FromArgb(128, 128, 255);
                menudown();
            }
            else
            {
                pnlBtnFormatos.Height = 48;
                btnFormatos.BackColor = Color.Transparent;
                btnFrmAyuda.BackColor = Color.Transparent;
                menudown();
            }
        }
        private void btnFrmAddUsuario_Click(object sender, EventArgs e)
        {
            AddFormInPanel(new Registrar_usuarios());
        }

        private void btnFrmAddFacilitador_Click(object sender, EventArgs e)
        {
          AddFormInPanel(new Registrar_facilitadores());
        }

        private void btnFrmVerUsuario_Click(object sender, EventArgs e)
        {
            AddFormInPanel(new Buscar_usuarios());
        }

        public void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Desea cerrar sesión?", "",
           MessageBoxButtons.YesNo, MessageBoxIcon.Question)
           == DialogResult.Yes)
            {
                this.Close();
                Inicio_Sesion ini = new Inicio_Sesion();

                ini.Show();
            }
        }

        private void btnFrmVerFacilitador_Click(object sender, EventArgs e)
        {
            AddFormInPanel(new Buscar_facilitadores());
        }

        private void btnFrmAddCliente_Click(object sender, EventArgs e)
        {
            AddFormInPanel(new Registrar_clientes());
        }

        private void btnFrmAddAreaEmpresa_Click(object sender, EventArgs e)
        {
            AddFormInPanel(new Registrar_area());
        }

        public void btnFrmVerCliente_Click(object sender, EventArgs e)
        {
            AddFormInPanel(new Buscar_cliente());
        }

        private void btnFrmAddFormacion_Click(object sender, EventArgs e)
        {
            Clases.Formaciones.creacion = true;
            AddFormInPanel(new Nueva_formacion_Abierto());
            Clases.Formaciones.creacion = false;
        }

        

        private void pnlPanelDisplay_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnFrmVerRefrigerios_Click(object sender, EventArgs e)
        {
            AddFormInPanel(new Ver_refrigerios());
        }

        private void btnFrmVerPublicidad_Click(object sender, EventArgs e)
        {
            AddFormInPanel(new Ver_publicidad());
        }

        private void btnFrmAyuda_Click(object sender, EventArgs e)
        {
            AddFormInPanel(new frmAyuda());
        }

        private void btnFormaciones_Click(object sender, EventArgs e)
        {

        }

        private void pnlBtnLogistica_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnFrmVerInsumos_Click(object sender, EventArgs e)
        {
            AddFormInPanel(new Ver_insumos());
        }

        private void btnFrmVerCursosInces_Click(object sender, EventArgs e)
        {
            AddFormInPanel(new Cursos_INCES());
        }

        private void btnFrmAddIncom_Click(object sender, EventArgs e)
        {
            Clases.Formaciones.creacion = true;
            AddFormInPanel(new Nueva_formacion_InCompany());
            Clases.Formaciones.creacion = false;
        }

        private void btnFrmAddINCES_Click(object sender, EventArgs e)
        {
            Clases.Formaciones.creacion = true;
            AddFormInPanel(new Nueva_formacion_INCES());
            Clases.Formaciones.creacion = false;
        }

        private void btnFrmAddFEE_Click(object sender, EventArgs e)
        {
            Clases.Formaciones.creacion = true;
            AddFormInPanel(new Nueva_formacion_FEE());
            Clases.Formaciones.creacion = false;
        }
    }
}
