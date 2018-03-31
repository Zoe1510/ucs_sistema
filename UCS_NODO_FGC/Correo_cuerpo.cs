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
using UCS_NODO_FGC.Clases;
using System.IO;
using System.Drawing.Imaging;
using System.Reflection;
using System.Diagnostics;
using System.Net.Mail;
using System.Net;

namespace UCS_NODO_FGC
{
    public partial class Correo_cuerpo : Form
    {
        Facilitadores fa = new Facilitadores();
        Facilitadores cofa = new Facilitadores();
        List<string> correosFa = new List<string>();
        Clientes cliente = new Clientes();
        public Correo_cuerpo()
        {
            InitializeComponent();
        }

        private void Correo_cuerpo_Load(object sender, EventArgs e)
        {
            //buscando correo de facilitador
            #region search mail
            MySqlDataReader correo = Conexion.ConsultarBD("select facilitadores_id_fa, ctf_id_cofa from cursos_tienen_fa where cursos_id_cursos='" + Datos_envio_correo.idcurso + "'");
            if (correo.Read())
            {
                fa.id_facilitador = Convert.ToInt32(correo["facilitadores_id_fa"]);

                MySqlDataReader leer = Conexion.ConsultarBD("select * from facilitadores where id_fa='" + fa.id_facilitador + "'");
                if (leer.Read())
                {
                    fa.correo_facilitador = Convert.ToString(leer["correo_fa"]);
                }
                leer.Close();

                
            }
            correo.Close();

            //buscando correo de cofacilitador
            MySqlDataReader correo2 = Conexion.ConsultarBD("select ctf_id_cofa from cursos_tienen_fa where cursos_id_cursos='" + Datos_envio_correo.idcurso + "'");
            if (correo2.Read())
            {
                cofa.id_facilitador = Convert.ToInt32(correo["ctf_id_cofa"]);
                if (cofa.id_facilitador != 0)
                {
                    MySqlDataReader leer2 = Conexion.ConsultarBD("select * from facilitadores where id_fa='" + cofa.id_facilitador + "'");
                    if (leer2.Read())
                    {
                        cofa.correo_facilitador = Convert.ToString(leer2["correo_fa"]);
                    }
                    leer2.Close();
                }
               
            }
            else
            {
                cofa.id_facilitador = 0;
            }
            #endregion

            #region search cliente
            MySqlDataReader idS = Conexion.ConsultarBD("select id_clientes1 from clientes_solicitan_cursos where id_curso1='" + Datos_envio_correo.idcurso+ "'");
            if (idS.Read())
            {
                cliente.id_cliente = Convert.ToInt32(idS["id_clientes1"]);
            }
            idS.Close();

            MySqlDataReader correoc = Conexion.ConsultarBD("select * from areas where id_cliente1='" + cliente.id_cliente + "' and nombre_area='"+Cursos.nombre_area+"'");
            if (correoc.Read())
            {
                cliente.correo_cliente = Convert.ToString(correoc["correo_contacto"]);//esto se usa en opcion 3
            }
            correoc.Close();
            #endregion
            txtDestinatarios.ReadOnly = true;
            if (Datos_envio_correo.opcion == 1)
            {
                txtDestinatarios.Text="Participantes inscritos";
            }else if (Datos_envio_correo.opcion == 2)
            {
                if (cofa.id_facilitador != 0)
                {
                    txtDestinatarios.Text = fa.correo_facilitador+" ; "+cofa.correo_facilitador;
                    correosFa.Add(fa.correo_facilitador);
                    correosFa.Add(cofa.correo_facilitador);
                }
                else
                {
                    txtDestinatarios.Text = fa.correo_facilitador;
                    correosFa.Add(fa.correo_facilitador);
                }
                
            }else if (Datos_envio_correo.opcion == 3)
            {
                txtDestinatarios.Text = cliente.correo_cliente;
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
            String mensaje = "Saludos cordiales. Se adjunta información pertinente al curso de formación: " + Datos_envio_correo.nombreformacion + ".";
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

        private void EnviarCorreoFacilitador( string asuntos, string msgPersonalizado)
        {
            //HAY QUE AÑADIR LAS RUTAS DE LOS ARCHIVOS SELECCIONADOS
            //consiste basicamente en obtener el pdf creado (desde la ruta ya establecida) y anexarlo a un formato de correo simple:
            List<string> destinatarios = new List<string>();
            String asunto = "";
            String mensaje = "";

            destinatarios.Add(fa.correo_facilitador);//CORREO DEL FA
            if(cofa.id_facilitador != 0)
            {
                destinatarios.Add(cofa.correo_facilitador);//CORREO DEL COFA
            }

            if (txtAsunto.Text == "")
            {
               asunto  = "Formación: " + Datos_envio_correo.nombreformacion + ".";
            }else
            {
                asunto = asuntos;
            }
           
            if(txtMensaje.Text == "")
            {
                //HAY QUE ARREGLAR EL MENSAJE PREDETERMINADO
                mensaje = "Saludos cordiales. XXXXXXXXXXXX al curso de formación: " + Datos_envio_correo.nombreformacion + ".";
            }
            else
            {
                mensaje = msgPersonalizado;
            }
             
            //String destintario = correo;
            String remitente = "soporteucs@gmail.com";
            MailMessage msg = new MailMessage();
            SmtpClient smtp = new SmtpClient();
            //
            // se asignan los destinatarios
            //
            foreach (string item in destinatarios)
            {
                msg.To.Add(new MailAddress(item));
            }



            //primero se añade el pdf creado con la informacion general
            msg.Attachments.Add(new Attachment(GetStreamFile(Nodos.ruta_PDF), Path.GetFileName(Nodos.ruta_PDF), "application/pdf"));
            
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
