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
using System.Runtime.InteropServices;

namespace UCS_NODO_FGC
{
    public partial class Preguntas_de_seguridad : Form
    {
        

        public Clases.Preguntas pre, pre2, pre3 = new Clases.Preguntas();
        public Clases.conexion_bd conexion = new Clases.conexion_bd();
        int id_pre1;
        int id_pre2;
        int id_pre3;
        string resp1, resp2, resp3;
        public int xClick = 0, yClick = 0;
        public Preguntas_de_seguridad()
        {
            InitializeComponent();
        }

        private void Preguntas_de_seguridad_Load(object sender, EventArgs e)
        {
            string pre = "¿";
            llenarcmbx1(pre);
            if(Clases.Usuarios.ActualizarPreguntas == 1)
            {
                btn_cerrar.Visible = true;
            }else if (Clases.Usuarios.ActualizarPreguntas == 0 || Clases.Recuperacion_contraseña.Opcion==3)
            {
                btn_cerrar.Visible = false;
            }
           
        }

        private void ObtenerIdRecuperacion()
        {
            int n_pre = 0;
            
            try
            {
                conexion.cerrarconexion();
                if (conexion.abrirconexion() == true)
                {
                    List<Clases.Preguntas> pregunta = new List<Clases.Preguntas>();
                    

                    pregunta = Clases.Preguntas.ObtenerIDPreguntas(conexion.conexion, Clases.Usuario_logeado.id_usuario);
                    conexion.cerrarconexion();

                    if (pregunta != null)
                    {
                        while (n_pre < pregunta.Count)
                        {
                            
                                if (n_pre == 0)
                                {
                                    pre = pregunta[0];
                                    n_pre++;
                                }
                                else if (n_pre == 1)
                                {
                                    pre2 = pregunta[1];
                                   
                                    n_pre++;
                                }
                                else if (n_pre == 2)
                                {
                                    pre3 = pregunta[2];
                                    
                                    n_pre++;
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
        private void guardar()
        {
            try
            {
                if (Clases.Usuarios.ActualizarPreguntas == 0 || Clases.Recuperacion_contraseña.cedula==1)
                {
                    conexion.cerrarconexion();
                    if ((cmbxPregunta1.SelectedIndex != -1) && (cmbxPregunta2.SelectedIndex != -1) && (cmbxPregunta3.SelectedIndex != -1))
                    {
                        if (cmbxPregunta1.SelectedIndex != cmbxPregunta2.SelectedIndex && cmbxPregunta1.SelectedIndex != cmbxPregunta3.SelectedIndex && cmbxPregunta3.SelectedIndex != cmbxPregunta2.SelectedIndex)
                        {
                            conexion.cerrarconexion();
                            if (conexion.abrirconexion() == true)
                            {
                                resp1 = txtRespuesta1.Text;
                                resp2 = txtRespuesta2.Text;
                                resp3 = txtRespuesta3.Text;
                                int id_user = Clases.Usuario_logeado.id_usuario;

                                int resultado1 = Clases.Preguntas.GuardarPreguntas(conexion.conexion, id_pre1, resp1, id_user, id_pre2, resp2, id_pre3, resp3);
                                conexion.cerrarconexion();
                                if (resultado1 > 0)
                                {
                                    MessageBox.Show("Guardado exitosamente.", "", MessageBoxButtons.OK);
                                    this.Close();
                                    Clases.Recuperacion_contraseña.cedula = 0;
                                    Clases.Recuperacion_contraseña.nombre = "";
                                    Clases.Recuperacion_contraseña.Opcion = 0;
                                }
                                else
                                {
                                    MessageBox.Show("Ha ocurrido un error al guardar las preguntas de seguridad.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }

                            }
                            else
                            {
                                MessageBox.Show("Ha ocurrido un error al conectarse a la base de datos.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }

                        }
                        else
                        {
                            MessageBox.Show("No puede seleccionar dos veces una misma pregunta.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }



                    }
                    else
                    {
                        MessageBox.Show("Debe proporcionar todas las preguntas.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
                else if (Clases.Usuarios.ActualizarPreguntas == 1)
                {
                    conexion.cerrarconexion();
                    if ((cmbxPregunta1.SelectedIndex != -1) && (cmbxPregunta2.SelectedIndex != -1) && (cmbxPregunta3.SelectedIndex != -1))
                    {
                        if (cmbxPregunta1.SelectedIndex != cmbxPregunta2.SelectedIndex && cmbxPregunta1.SelectedIndex != cmbxPregunta3.SelectedIndex && cmbxPregunta3.SelectedIndex != cmbxPregunta2.SelectedIndex)
                        {
                            ObtenerIdRecuperacion();
                            pre.respuesta = txtRespuesta1.Text;
                            pre2.respuesta = txtRespuesta2.Text;
                            pre3.respuesta = txtRespuesta3.Text;
                            pre.id_pregunta = id_pre1;
                            pre2.id_pregunta = id_pre2;
                            pre3.id_pregunta = id_pre3;
                            conexion.cerrarconexion();
                            if (conexion.abrirconexion() == true)
                            {
                                int P1 = Clases.Preguntas.ActualizarPreguntas(conexion.conexion, pre);
                                conexion.cerrarconexion();
                                if (conexion.abrirconexion() == true)
                                {
                                    int P2 = Clases.Preguntas.ActualizarPreguntas(conexion.conexion, pre2);
                                    conexion.cerrarconexion();
                                    if (conexion.abrirconexion() == true)
                                    {
                                        int P3 = Clases.Preguntas.ActualizarPreguntas(conexion.conexion, pre3);
                                        conexion.cerrarconexion();
                                        if(P1 >0 && P2>0 && P3 > 0)
                                        {
                                            MessageBox.Show("Actualizado exitosamente.", "", MessageBoxButtons.OK);
                                            this.Close();
                                        }
                                    }

                                }



                            }

                        }
                        else
                        {
                            MessageBox.Show("No puede seleccionar dos veces una misma pregunta.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Debe proporcionar todas las preguntas.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
               

            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnGuardarPDS_Click(object sender, EventArgs e)
        {
            guardar();
        }

        private void llenarcmbx1(string pre)
        {
            
            cmbxPregunta1.ValueMember = "id_pregunta";
            cmbxPregunta1.DisplayMember = "pregunta";
            cmbxPregunta1.DataSource = Clases.Paneles.LlenarComboboxPreguntas(pre);
            cmbxPregunta1.SelectedIndex = -1;
            cmbxPregunta1.Text = "Seleccione";
            llenarcmbx2(pre);
        }
        private void llenarcmbx2(string pre)
        {
            
            cmbxPregunta2.ValueMember = "id_pregunta";
            cmbxPregunta2.DisplayMember = "pregunta";
            cmbxPregunta2.DataSource = Clases.Paneles.LlenarComboboxPreguntas(pre);
            cmbxPregunta2.SelectedIndex = -1;
            cmbxPregunta2.Text = "Seleccione";

            llenarcmbx3(pre);
        }

        private void llenarcmbx3(string pre)
        {
            
            cmbxPregunta3.ValueMember = "id_pregunta";
            cmbxPregunta3.DisplayMember = "pregunta";
            cmbxPregunta3.DataSource = Clases.Paneles.LlenarComboboxPreguntas(pre);
            cmbxPregunta3.SelectedIndex = -1;
            cmbxPregunta3.Text = "Seleccione";
        }
        //private void txtRespuesta1_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    Clases.Paneles.sololetras(e);
        //}

        //private void txtRespuesta2_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    Clases.Paneles.sololetras(e);
        //}

        //private void txtRespuesta3_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    Clases.Paneles.sololetras(e);
        //}

        private void cmbxPregunta1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            id_pre1 = Convert.ToInt32(cmbxPregunta1.SelectedValue);
        }

        private void cmbxPregunta2_SelectionChangeCommitted(object sender, EventArgs e)
        {
            id_pre2= Convert.ToInt32(cmbxPregunta2.SelectedValue);
        }
        private void cmbxPregunta3_SelectionChangeCommitted(object sender, EventArgs e)
        {
            id_pre3= Convert.ToInt32(cmbxPregunta3.SelectedValue);
        }
       

        private void txtRespuesta1_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar == (int)Keys.Enter)
            {
                cmbxPregunta2.Focus();
            }
        }

        private void txtRespuesta2_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar == (int)Keys.Enter)
            {
                cmbxPregunta3.Focus();
            }
        }

        private void txtRespuesta3_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar == (int)Keys.Enter)
            {
                guardar();
            }
        }

        private void Preguntas_de_seguridad_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)

            { xClick = e.X; yClick = e.Y; }

            else

            { this.Left = this.Left + (e.X - xClick); this.Top = this.Top + (e.Y - yClick); }
        }

        private void txtRespuesta1_Validating(object sender, CancelEventArgs e)
        {
            if (txtRespuesta1.Text == "")
            {
                errorProviderR1.SetError(txtRespuesta1, "Debe proporcionar una respuesta válida.");
                txtRespuesta1.Focus();
            }
            else
            {
                errorProviderR1.SetError(txtRespuesta1, "");
            }
        }

        private void btn_cerrar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Desea cancelar la operación?", "",
       MessageBoxButtons.YesNo, MessageBoxIcon.Question)
       == DialogResult.Yes)
            {
                this.Close();
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
    }
}
