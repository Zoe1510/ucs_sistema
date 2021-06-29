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
    public partial class Recuperacion_preguntas : Form
    {
        public Clases.conexion_bd conexion = new Clases.conexion_bd();
        public Clases.Preguntas pre, pre2, pre3 = new Clases.Preguntas();
        public int xClick = 0, yClick = 0;

        public Recuperacion_preguntas()
        {
            InitializeComponent();
        }
        
      

        private void Recuperacion_preguntas_Load(object sender, EventArgs e)
        {
           
            if (Clases.Recuperacion_contraseña.cedula != 0)
            {
                CargarPreguntas(Clases.Recuperacion_contraseña.id_usuario);
            }
        }

        private void CargarPreguntas(int id_usuario)
        {
            obtenerIDPreguntas(id_usuario);
            ObtenerPreguntas();
            txtPregunta1.Text = pre.pregunta;
            txtPregunta2.Text = pre2.pregunta;
            txtPregunta3.Text = pre3.pregunta;
            txtPregunta1.Focus();
        }

        private void txtRespuesta1_Validating(object sender, CancelEventArgs e)
        {
            if(txtRespuesta1.Text == "")
            {
                errorProviderR1.SetError(txtRespuesta1, "Debe proporcionar una respuesta válida.");
                txtRespuesta1.Focus();
            }else
            {
                errorProviderR1.SetError(txtRespuesta1, "");
            }
        }

        private void txtRespuesta2_Validating(object sender, CancelEventArgs e)
        {
            if (txtRespuesta2.Text == "")
            {
                errorProviderR2.SetError(txtRespuesta2, "Debe proporcionar una respuesta válida.");
                txtRespuesta2.Focus();
            }
            else
            {
                errorProviderR2.SetError(txtRespuesta2, "");
            }
        }

        private void txtRespuesta3_Validating(object sender, CancelEventArgs e)
        {
            if (txtRespuesta3.Text == "")
            {
                errorProviderR3.SetError(txtRespuesta3, "Debe proporcionar una respuesta válida.");
                txtRespuesta3.Focus();
            }
            else
            {
                errorProviderR3.SetError(txtRespuesta3, "");
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

        private void btn_minimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btn_cerrar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Desea salir?", "",
       MessageBoxButtons.YesNo, MessageBoxIcon.Question)
       == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void btnGuardarPDS_Click(object sender, EventArgs e)
        {
            Comprobacion();
        }

        private void Comprobacion()
        {
            if (txtRespuesta1.Text != pre.respuesta)
            {
                errorProviderR1.SetError(txtRespuesta1, "Respuesta inválida.");
                txtRespuesta1.Focus();
            }
            else if (txtRespuesta2.Text != pre2.respuesta)
            {
                errorProviderR1.SetError(txtRespuesta1, "");
                errorProviderR2.SetError(txtRespuesta2, "Respuesta inválida.");
                txtRespuesta2.Focus();
            }
            else if (txtRespuesta3.Text != pre3.respuesta)
            {
                errorProviderR2.SetError(txtRespuesta2, "");
                errorProviderR3.SetError(txtRespuesta3, "Respuesta inválida.");
                txtRespuesta3.Focus();
            }
            else
            {
                errorProviderR3.SetError(txtRespuesta3, "");
                Cambio_contraseña newpass = new Cambio_contraseña();
                this.Close();
                newpass.ShowDialog();

            }
        }

        private void txtRespuesta1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                this.txtPregunta2.Focus();
            }
        }

        private void txtRespuesta2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                this.txtPregunta3.Focus();
            }
        }

        private void txtRespuesta3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                Comprobacion();
            }
        }

        private void Recuperacion_preguntas_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)

            { xClick = e.X; yClick = e.Y; }

            else

            { this.Left = this.Left + (e.X - xClick); this.Top = this.Top + (e.Y - yClick); }
        }

        private void obtenerIDPreguntas(int id_usuario)
        {
            
            try
            {
                conexion.cerrarconexion();
                if (conexion.abrirconexion() == true)
                {
                    List<Clases.Preguntas> pregunta = new List<Clases.Preguntas>();
                    int n_pre = 0;
                    int i = 0;
                    
                    pregunta =Clases.Preguntas.ObtenerIDPreguntas(conexion.conexion, id_usuario);
                    conexion.cerrarconexion();

                    if (pregunta != null)
                    {
                        while (n_pre <3 && i < pregunta.Count)
                        {
                            if (pregunta[i].id_user1 == id_usuario)
                            {
                                if (i == 0)
                                {
                                    pre = pregunta[i];
                                    i++;
                                    n_pre++;
                                }else if (i == 1)
                                {
                                    pre2 = pregunta[i];
                                    i++;
                                    n_pre++;
                                }
                                else if (i == 2)
                                {
                                    pre3 = pregunta[i];
                                    i++;
                                    n_pre++;
                                }

                            }
                            else
                            {
                                i++;
                                n_pre++;
                            }
                        }
                    }else
                    {
                        MessageBox.Show("No existen preguntas de seguridad de este usuario.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    
                                            
                }

            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Ha ocurrido un error: " + ex, "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
           
        }

        private void ObtenerPreguntas()
        {
            try
            {
                conexion.cerrarconexion();
                if (conexion.abrirconexion() == true)
                {
                    pre.pregunta = Clases.Preguntas.ObtenerPregunta(conexion.conexion, pre.id_pregunta);
                    conexion.cerrarconexion();
                    if (conexion.abrirconexion() == true)
                    {
                        pre2.pregunta = Clases.Preguntas.ObtenerPregunta(conexion.conexion, pre2.id_pregunta);
                        conexion.cerrarconexion();
                        if (conexion.abrirconexion() == true)
                        {
                            pre3.pregunta = Clases.Preguntas.ObtenerPregunta(conexion.conexion, pre3.id_pregunta);
                            conexion.cerrarconexion();
                        }
                    }
                }

            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Ha ocurrido un error: " + ex, "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        
    }
}
