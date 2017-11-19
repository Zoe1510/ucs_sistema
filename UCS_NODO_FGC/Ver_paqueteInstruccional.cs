using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace UCS_NODO_FGC
{
    public partial class Ver_paqueteInstruccional : Form
    {
        public Clases.conexion_bd conexion = new Clases.conexion_bd();
        public Clases.Paquete_instruccional p_inst = new Clases.Paquete_instruccional();

        public Ver_paqueteInstruccional()
        {
            InitializeComponent();
        }

        private void cerrarForm()
        {
            f1.Visible = false;
            f2.Visible = false;
            
            f4.Visible = false;
            f5.Visible = false;
            btnGuardarCambios.Enabled = false;
            btnModificarBitacora.Enabled = false;
            btnModificarContenido.Enabled = false;
           
            btnModificarManual.Enabled = false;
            btnModificarPresentacion.Enabled = false;
            btnVerBitacora.Enabled = false;
            btnVerContenido.Enabled = false;
            
            btnVerManual.Enabled = false;
            btnVerPresentacion.Enabled = false;
        }
        private void PQ()
        {
            //bitacora
            if (Clases.Paquete_instruccional._bitacora == null || Clases.Paquete_instruccional._bitacora == "")
            {
                txtRutaBitacora.Text = "No existe archivo.";
            }
            else
            {
                txtRutaBitacora.Text = Clases.Paquete_instruccional._bitacora;
                btnVerBitacora.Enabled = true;
            }

            //contenido
            if (Clases.Paquete_instruccional._contenido == null || Clases.Paquete_instruccional._contenido == "")
            {
                txtRutaContenido.Text = "No existe archivo.";
            }
            else
            {
                txtRutaContenido.Text = Clases.Paquete_instruccional._contenido;
                btnVerContenido.Enabled = true;
            }

           
            //Manual
            if (Clases.Paquete_instruccional._manual == null || Clases.Paquete_instruccional._manual == "")
            {
                txtRutaManual.Text = "No existe archivo.";
            }
            else
            {
                txtRutaManual.Text = Clases.Paquete_instruccional._manual;
                btnVerManual.Enabled = true;
            }

            //presentacion
            if (Clases.Paquete_instruccional._presentacion == null || Clases.Paquete_instruccional._presentacion == "")
            {
                txtRutaPresentacion.Text = "No existe archivo.";
            }
            else
            {
                txtRutaPresentacion.Text = Clases.Paquete_instruccional._presentacion;
                btnVerPresentacion.Enabled = true;
            }
        }
        private void MostrarPQ()
        {
            if (Clases.Paquete_instruccional.id_pin != 0)
            {
                PQ();
                

            }
        }
        private void Ver_paqueteInstruccional_Load(object sender, EventArgs e)
        {
            MostrarPQ();
        }

        private void btnVerBitacora_Click(object sender, EventArgs e)
        {
            Process.Start(txtRutaBitacora.Text);
        }

        private void btnVerContenido_Click(object sender, EventArgs e)
        {
            Process.Start(txtRutaContenido.Text);
        }

       
        private void btnVerManual_Click(object sender, EventArgs e)
        {
            Process.Start(txtRutaManual.Text);
        }

        private void btnVerPresentacion_Click(object sender, EventArgs e)
        {
            Process.Start(txtRutaPresentacion.Text);
        }

        private void btnModificarUtilizar_Click(object sender, EventArgs e)
        {
            if (Clases.Paquete_instruccional.tipo_curso == "Abierto")
            {
                btnModificarContenido.Enabled = true;
                btnModificarPresentacion.Enabled = true;
                f2.Visible = true;
                f5.Visible = true;

            }else if (Clases.Paquete_instruccional.tipo_curso == "FEE")
            {
                btnModificarPresentacion.Enabled = true;
                btnModificarContenido.Enabled = true;
                btnModificarBitacora.Enabled = true;
                f1.Visible = true;
                f2.Visible = true;
                f5.Visible = true;
            }
            else if (Clases.Paquete_instruccional.tipo_curso == "InCompany" || Clases.Paquete_instruccional.tipo_curso == "INCES")
            {
                btnModificarPresentacion.Enabled = true;
                btnModificarContenido.Enabled = true;
                btnModificarBitacora.Enabled = true;
                btnModificarManual.Enabled = true;
            }
        }

        private void btnModificarContenido_Click(object sender, EventArgs e)
        {
            OpenFileDialog od = new OpenFileDialog();
            od.Filter = "DOC files |*.doc; *.docx; *.docm; *.dotx; *.dotm";
            if (od.ShowDialog() == DialogResult.OK)
            {
                
                txtRutaContenido.Text = od.FileName;
                btnVerContenido.Enabled = true;
                btnGuardarCambios.Enabled = true;

            }
            else
            {
                if(txtRutaContenido.Text == "")
                {
                    txtRutaContenido.Text = "No existe archivo.";
                }
                
            }
        }

        private void btnModificarBitacora_Click(object sender, EventArgs e)
        {
            OpenFileDialog od = new OpenFileDialog();
            od.Filter = "PDF files |*.pdf";
            if (od.ShowDialog() == DialogResult.OK)
            {
                
                txtRutaBitacora.Text = od.FileName;
                btnVerBitacora.Enabled = true;
                btnGuardarCambios.Enabled = true;
            }
            else
            {
                if (txtRutaBitacora.Text == "")
                {
                    txtRutaBitacora.Text = "No existe archivo.";
                }

            }
        }

       

        private void btnModificarManual_Click(object sender, EventArgs e)
        {
            OpenFileDialog od = new OpenFileDialog();
            od.Filter = "PDF files |*.pdf";
            if (od.ShowDialog() == DialogResult.OK)
            {
               
                txtRutaManual.Text = od.FileName;
                btnVerManual.Enabled = true;
                btnGuardarCambios.Enabled = true;
            }
            else
            {
                if (btnVerManual.Text == "")
                {
                    btnVerManual.Text = "No existe archivo.";
                }

            }
        }

        private void btnModificarPresentacion_Click(object sender, EventArgs e)
        {
            OpenFileDialog od = new OpenFileDialog();
            od.Filter = "PPT files |*.ppt;*.pptx;*.pptm";
            if (od.ShowDialog() == DialogResult.OK)
            {
                
                txtRutaPresentacion.Text = od.FileName;
                btnVerPresentacion.Enabled = true;
                btnGuardarCambios.Enabled = true;
            }
            else
            {
                if (btnVerPresentacion.Text == "")
                {
                    btnVerPresentacion.Text = "No existe archivo.";
                }

            }
            
        }

        private void btnGuardarCambios_Click(object sender, EventArgs e)
        {
            try
            {
                if (Clases.Paquete_instruccional.tipo_curso == "Abierto")
                {
                    p_inst.id_pinstruccional = Clases.Paquete_instruccional.id_pin;
                    int Modc, ModP;
                    if (Clases.Paquete_instruccional._contenido != txtRutaContenido.Text)//hay cambio en el contenido
                    {
                        
                        
                        p_inst.contenido= txtRutaContenido.Text;
                        conexion.cerrarconexion();
                        if (conexion.abrirconexion() == true)
                        {
                            Modc = Clases.Paquete_instruccional.ModificarContenidoPQ(conexion.conexion, p_inst);
                            conexion.cerrarconexion();
                            if (Modc > 0)
                            {
                               MessageBox.Show("Actualizado correctamente.");
                            }
                        }

                        
                        
                    }
                    else//no hay cambio en el contenido
                    {
                        if (Clases.Paquete_instruccional._presentacion != txtRutaPresentacion.Text && txtRutaPresentacion.Text != "No existe archivo.")
                        {
                            p_inst.presentacion = txtRutaPresentacion.Text;
                            conexion.cerrarconexion();
                            if (conexion.abrirconexion() == true)
                            {
                                ModP = Clases.Paquete_instruccional.ModificarPresentacionPQ(conexion.conexion, p_inst);
                                conexion.cerrarconexion();
                                if (ModP > 0)
                                {
                                    MessageBox.Show("Actualizado correctamente.");
                                }
                            }
                        }else//no hay cambio en la presentacion
                        {
                            MessageBox.Show("No se han detectado cambios.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                }
                else if (Clases.Paquete_instruccional.tipo_curso == "FEE")
                {
                    
                }
                else if (Clases.Paquete_instruccional.tipo_curso == "InCompany")
                {
                   
                }else if(Clases.Paquete_instruccional.tipo_curso == "INCES")
                {

                }
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                conexion.cerrarconexion();

            }
        }

        private void btnUtilizar_Click(object sender, EventArgs e)
        {
            if (Clases.Paquete_instruccional.tipo_curso == "Abierto")
            {
                Clases.Paquete_instruccional._contenido = txtRutaContenido.Text;
                if (txtRutaPresentacion.Text != "No existe archivo.")
                {
                    Clases.Paquete_instruccional._presentacion = txtRutaPresentacion.Text;
                }
                cerrarForm();
                Clases.Paquete_instruccional.utilizado = true;
                this.Close();

            }
            else if (Clases.Paquete_instruccional.tipo_curso == "FEE")
            {

            }
            else if (Clases.Paquete_instruccional.tipo_curso == "InCompany")
            {

            }
            else if (Clases.Paquete_instruccional.tipo_curso == "INCES")
            {

            }
        }
    }
    
}
