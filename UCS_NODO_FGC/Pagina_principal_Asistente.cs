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
    public partial class Pagina_principal_Asistente : Form
    {
        private Form _objForm;
        public Clases.conexion_bd conexion = new Clases.conexion_bd();
        public Pagina_principal_Asistente()
        {
            InitializeComponent();
        }

        //METODOS
        public void menudown()
        {
            //boton facilitadores
            if (pnlBtnFormaciones.Height == 48)
            {

                pnlBtnFacilitadores.Location = new Point(0, Convert.ToInt32(pnlBtnFormaciones.Location.Y) + 48);
            }
            else
            {

                pnlBtnFacilitadores.Location = new Point(0, Convert.ToInt32(pnlBtnFormaciones.Location.Y) + 97);
            }

            //boton clientes
            if (pnlBtnFacilitadores.Height == 48)
            {

                pnlBtnClientes.Location = new Point(0, Convert.ToInt32(pnlBtnFacilitadores.Location.Y) + 48);
            }
            else
            {

                pnlBtnClientes.Location = new Point(0, Convert.ToInt32(pnlBtnFacilitadores.Location.Y) + 97);
            }

            //boton participantes
            if (pnlBtnClientes.Height == 48)
            {

                pnlBtnParticipantes.Location = new Point(0, Convert.ToInt32(pnlBtnClientes.Location.Y) + 48);
            }
            else
            {

                pnlBtnParticipantes.Location = new Point(0, Convert.ToInt32(pnlBtnClientes.Location.Y) + 97);
            }

            //boton formatos
            if (pnlBtnParticipantes.Height == 48)
            {
                pnlBtnLogistica.Location = new Point(0, Convert.ToInt32(pnlBtnParticipantes.Location.Y) + 48);

            }
            else
            {
                pnlBtnLogistica.Location = new Point(0, Convert.ToInt32(pnlBtnParticipantes.Location.Y) +157);
            }

            //
            if (pnlBtnLogistica.Height == 48)
            {

                pnlBtnFormatos.Location = new Point(0, Convert.ToInt32(pnlBtnLogistica.Location.Y) + 48);
            }
            else
            {

                pnlBtnFormatos.Location = new Point(0, Convert.ToInt32(pnlBtnLogistica.Location.Y) + 316);
            }

            //boton final
            if (pnlBtnFormatos.Height == 48)
            {
                pnlBtnFinal.Location = new Point(0, Convert.ToInt32(pnlBtnFormatos.Location.Y) + 48);
            }
            else
            {
                pnlBtnFinal.Location = new Point(0, Convert.ToInt32(pnlBtnFormatos.Location.Y) + 97);
            }
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

        public void menu_size()
        {
            Form obj = new Form();

            if (pnlPanelContenedorMenu.Width == 245)
            {
                pnlPanelContenedorMenu.Width = 44;
                btnDespliegueMenu.Left = 7;
                rsLineaSeparadora.Visible = false;
                if (_objForm != null)
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

        private void Pagina_principal_Asistente_Load(object sender, EventArgs e)
        {
            //permite que la imagen sea redonda
            System.Drawing.Drawing2D.GraphicsPath gp = new System.Drawing.Drawing2D.GraphicsPath();
            gp.AddEllipse(0, 0, picFotoUser.Width - 3, picFotoUser.Height - 3);
            Region rg = new Region(gp);
            picFotoUser.Region = rg;

            if (Clases.Usuario_logeado.imagen_usuario != null)
            {
                picFotoUser.Image = Clases.Helper.ByteArrayToImage(Clases.Usuario_logeado.imagen_usuario);
            }
            else
            {
                MessageBox.Show("No se ha encontrado ninguna foto en la base de datos");
            }
            //nombre del usuario
            lblLabelNombre_usuario.Text = Clases.Usuario_logeado.nombre_usuario;


            //si el usuario es nuevo:
            if (Clases.Usuario_logeado.password == "12345678")
            {
                Clases.Recuperacion_contraseña.Opcion = 3;
                Clases.Recuperacion_contraseña.cedula = 1;
                MessageBox.Show("Debe actualizar su contraseña y establecer las preguntas de seguridad.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Cambio_contraseña c = new Cambio_contraseña();
                c.ShowDialog();

            }

            AddFormInPanel(new Notificaciones());

        }


        //BOTONES
        private void btn_cerrar_Click(object sender, EventArgs e)
        {
            Clases.Paneles.boton_cerrar();
        }

        private void btn_minimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
               

        private void btnEditarPerfil_Click(object sender, EventArgs e)
        {
            AddFormInPanel(new Perfil_personal());
        }

        private void btnFormaciones_MouseClick(object sender, MouseEventArgs e)
        {
            if (pnlBtnFormaciones.Height == 48)
            {
                pnlBtnFormaciones.Height = 108;
                btnFormaciones.BackColor = Color.FromArgb(128, 128, 255);
                btnFacilitadores.BackColor = Color.FromArgb(128, 128, 255);
                menudown();
            }
            else
            {
                pnlBtnFormaciones.Height = 48;
                btnFacilitadores.BackColor = Color.Transparent;
                btnFormaciones.BackColor = Color.Transparent;
                menudown();

            }
        }

        private void btnFacilitadores_Click(object sender, EventArgs e)
        {
            if (pnlBtnFacilitadores.Height == 48)
            {
                pnlBtnFacilitadores.Height = 96;
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
                pnlBtnClientes.Height = 96;
                btnParticipante.BackColor = Color.FromArgb(128, 128, 255);
                btnClientes.BackColor = Color.FromArgb(128, 128, 255);
                menudown();
            }
            else
            {
                pnlBtnClientes.Height = 48;
                btnParticipante.BackColor = Color.Transparent;
                btnClientes.BackColor = Color.Transparent;
                menudown();

            }

        }

        private void btnParticipante_Click(object sender, EventArgs e)
        {
            if (pnlBtnParticipantes.Height == 48)
            {
                pnlBtnParticipantes.Height = 156;
                btnLogistica.BackColor = Color.FromArgb(128, 128, 255);
                btnParticipante.BackColor = Color.FromArgb(128, 128, 255);
                menudown();
            }
            else
            {
                pnlBtnParticipantes.Height = 48;
                btnLogistica.BackColor = Color.Transparent;
                btnParticipante.BackColor = Color.Transparent;
                menudown();
            }

        }
        private void btnFormatos_Click(object sender, EventArgs e)
        {
            if (pnlBtnFormatos.Height == 48)
            {
                pnlBtnFormatos.Height = 96;
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

        private void btnLogistica_Click(object sender, EventArgs e)
        {
            if (pnlBtnLogistica.Height == 48)
            {
                pnlBtnLogistica.Height = 315;
                btnLogistica.BackColor = Color.FromArgb(128, 128, 255);
                btnFormatos.BackColor = Color.FromArgb(128, 128, 255);
                menudown();
            }
            else
            {
                pnlBtnLogistica.Height = 48;
                btnFormatos.BackColor = Color.Transparent;
                btnLogistica.BackColor = Color.Transparent;
                menudown();
            }
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

        public void btnFrmVerCliente_Click(object sender, EventArgs e)
        {
            AddFormInPanel(new Buscar_cliente());
        }

        private void btnDespliegueMenu_Click_1(object sender, EventArgs e)
        {
            AddFormInPanel(new Notificaciones());
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
        private void btnFrmVerInsumos_Click(object sender, EventArgs e)
        {
            AddFormInPanel(new Ver_insumos());
        }

        private void btnFrmVerCursosInces_Click(object sender, EventArgs e)
        {
            AddFormInPanel(new Cursos_INCES());
        }

        private void btnFrmVerParticipante_Click(object sender, EventArgs e)
        {
            AddFormInPanel(new Ver_participantes());
        }

        private void btnFrmAddParticipantes_Click(object sender, EventArgs e)
        {
            AddFormInPanel(new Agregar_participante());
        }

        private void btnFrmVerFormatos_Click(object sender, EventArgs e)
        {
            AddFormInPanel(new Ver_formatos());
        }

        private void btnFrmVerFormacion_Click(object sender, EventArgs e)
        {
            AddFormInPanel(new Ver_formaciones());
        }
        private void btnFrmVerCursosAFI_Click(object sender, EventArgs e)
        {
            AddFormInPanel(new Cursos_AFI());
        }

    }
}
