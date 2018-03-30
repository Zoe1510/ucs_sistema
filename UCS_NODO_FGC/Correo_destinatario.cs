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
using System.Net.Mail;
using System.Net;
using System.IO;
using System.Diagnostics;

namespace UCS_NODO_FGC
{
    public partial class Correo_destinatario : Form
    {
        int mantener = 0;
        string correo, nodo;
        public Correo_destinatario()
        {
            InitializeComponent();
        }

        private void Correo_destinatario_Load(object sender, EventArgs e)
        {
            nodo = Nodos.nombre_nodo;
            if (Nodos.mantener == 0)
            {
                chkMantener.Checked = false;
                mantener = 0;
                if(Nodos.correo_ndoo=="NO HAY CORREO")
                {
                    txtCorreo.Clear();
                }else
                {
                    txtCorreo.Text = Nodos.correo_ndoo;
                }
            }else
            {

            }
        }
        private void txtCorreo_Validating(object sender, CancelEventArgs e)
        {
            if (Clases.Paneles.ComprobarFormatoEmail(txtCorreo.Text) == false)
            {
                errorProviderCorreo.SetError(txtCorreo, "Debe proporcionar un correo válido");
                txtCorreo.Focus();
            }
            else
            {
                errorProviderCorreo.SetError(txtCorreo, "");
                correo = txtCorreo.Text;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (chkMantener.Checked == true)
            {
                mantener = 1;
            }else
            {
                mantener = 0;
            }
        }

        private void btnEnviar_Click(object sender, EventArgs e)
        {
            //primero se guarda el destinatario

            //depende si hay o no uno en la base de datos, para insertar o modificar
            if(Nodos.correo_ndoo=="NO HAY CORREO")
            {
                GuardarDestinatario();
            }else
            {
                ActualizarDestinatario();
            }
            
            string asunt = "Datos de la formación: " + Nodos.formacion_nombre;
            //luego se envia el correo
            EnviarCorreo(correo, asunt, Nodos.ruta_PDF);
        }

        private void GuardarDestinatario()
        {
            if (txtCorreo.Text != "")
            {
                MySqlDataReader save = Conexion.ConsultarBD("insert into nodos (nombre_nodo, correo_nodo, mantener) values ('"+nodo+"', '"+correo+"', "+mantener+")");
                save.Close();
            }else
            {
                MessageBox.Show("Debe proporcionar un correo válido.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ActualizarDestinatario()
        {
            if (txtCorreo.Text != "")
            {
                MySqlDataReader save = Conexion.ConsultarBD("update nodos set correo_nodo='"+correo+"', mantener='"+mantener+"' where nombre_nodo='"+nodo+"'");
                save.Close();
            }
            else
            {
                MessageBox.Show("Debe proporcionar un correo válido.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #region envio correo
        private bool AccesoInternet()
        {

            try
            {
                System.Net.IPHostEntry host = System.Net.Dns.GetHostEntry("www.google.com");
                return true;

            }
            catch (Exception es)
            {

                return false;
            }

        }
        public Stream GetStreamFile(string filePath)
        {
            using (FileStream fileStream = File.OpenRead(filePath))
            {
                MemoryStream memStream = new MemoryStream();
                memStream.SetLength(fileStream.Length);
                fileStream.Read(memStream.GetBuffer(), 0, (int)fileStream.Length);

                return memStream;
            }
        }
        private void EnviarCorreo(string correo, string asuntos, string ruta)
        {
            //consiste basicamente en obtener el pdf creado (desde la ruta ya establecida) y anexarlo a un formato de correo simple:

            String asunto = asuntos;
            String mensaje = "Saludos cordiales. Se adjunta información pertinente al curso de formación: " + Nodos.formacion_nombre + ".";
            String destintario = correo;
            String remitente = "soporteucs@gmail.com";
            MailMessage msg = new MailMessage();
            SmtpClient smtp = new SmtpClient();
            msg.To.Add(destintario);

            msg.Attachments.Add(new Attachment(GetStreamFile(ruta), Path.GetFileName(ruta), "application/pdf"));

            msg.From = new MailAddress(remitente, "Nodo de Formación", System.Text.Encoding.UTF8);
            msg.Subject = asunto;
            msg.SubjectEncoding = System.Text.Encoding.UTF8;
            msg.Body = mensaje;
            msg.BodyEncoding = System.Text.Encoding.UTF8;
            msg.IsBodyHtml = false;

            smtp.Credentials = new System.Net.NetworkCredential(remitente, "ucs.29933526"); //entre comillas va el password de ese correo electronico
            smtp.Port = 587;
            smtp.Host = "smtp.gmail.com";
            smtp.EnableSsl = true;
            try
            {
                Task.Run(() =>
                {
                    smtp.Send(msg);
                    msg.Dispose();
                    MessageBox.Show("Correo enviado.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                );

                MessageBox.Show("Esta tarea puede tardar algunos minutos, por favor espere.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al enviar el correo electrónico: " + ex.Message + " Intentelo más tarde.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }
        #endregion
    }
}
