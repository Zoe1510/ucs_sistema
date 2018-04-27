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
        int cantidad = 0;
        string ruta = "";
        Facilitadores fa = new Facilitadores();
        Facilitadores cofa = new Facilitadores();
        List<string> correosFa = new List<string>();
        Clientes cliente = new Clientes();
        List<string> lista_rutas = new List<string>();
        public Correo_cuerpo()
        {
            InitializeComponent();
            //buscando correo de facilitador
            #region search mail
            Datos_envio_correo.idcurso = Cursos.id_curso13;
            //MessageBox.Show(Datos_envio_correo.idcurso.ToString());
            MySqlDataReader correo = Conexion.ConsultarBD("select facilitadores_id_fa, ctf_id_cofa from cursos_tienen_fa where cursos_id_cursos='" + Datos_envio_correo.idcurso + "'");
            if (correo.Read())
            {
                fa.id_facilitador = Convert.ToInt32(correo["facilitadores_id_fa"]);
               // MessageBox.Show("id del fa: " + fa.id_facilitador.ToString());
            }
            correo.Close();
            MySqlDataReader leer = Conexion.ConsultarBD("select * from facilitadores where id_fa='" + fa.id_facilitador + "'");
            if (leer.Read())
            {
                fa.correo_facilitador = Convert.ToString(leer["correo_fa"]);
            }
            leer.Close();
            //buscando correo de cofacilitador
            MySqlDataReader correo2 = Conexion.ConsultarBD("select ctf_id_cofa from cursos_tienen_fa where cursos_id_cursos='" + Datos_envio_correo.idcurso + "'");
            if (correo2.Read())
            {
                cofa.id_facilitador = Convert.ToInt32(correo2["ctf_id_cofa"]);

            }
            else
            {
                cofa.id_facilitador = 0;
            }
            correo2.Close();
            if (cofa.id_facilitador != 0)
            {
                MySqlDataReader leer2 = Conexion.ConsultarBD("select * from facilitadores where id_fa='" + cofa.id_facilitador + "'");
                if (leer2.Read())
                {
                    cofa.correo_facilitador = Convert.ToString(leer2["correo_fa"]);
                }
                leer2.Close();
            }
            #endregion

            #region search cliente
            MySqlDataReader idS = Conexion.ConsultarBD("select id_cliente1 from clientes_solicitan_cursos where id_curso1='" + Datos_envio_correo.idcurso + "'");
            if (idS.Read())
            {
                cliente.id_cliente = Convert.ToInt32(idS["id_cliente1"]);
            }
            idS.Close();

            MySqlDataReader correoc = Conexion.ConsultarBD("select * from areas where id_cliente1='" + cliente.id_cliente + "' and nombre_area='" + Cursos.nombre_area + "'");
            if (correoc.Read())
            {
                cliente.correo_cliente = Convert.ToString(correoc["correo_contacto"]);//esto se usa en opcion 3
            }
            correoc.Close();
            #endregion
        }

        private void Correo_cuerpo_Load(object sender, EventArgs e)
        {
            btnEnviar.Enabled = true;
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
            catch 
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
        private void EnviarCorreoParticipante(string asuntos, string mensajes)
        {
            //consiste basicamente en obtener el pdf creado (desde la ruta ya establecida) y anexarlo a un formato de correo simple:

            String asunto = asuntos;
            String mensaje = mensajes;
            
            String remitente = "soporteucs@gmail.com";
            MailMessage msg = new MailMessage();
            SmtpClient smtp = new SmtpClient();

            foreach (string item in Datos_envio_correo.correos_participantes)
            {
                msg.To.Add(new MailAddress(item));
            }

            foreach (string ruta in lista_rutas)
            {
                msg.Attachments.Add(new Attachment(GetStreamFile(ruta), Path.GetFileName(ruta), "application/pdf"));
            }
            

            msg.From = new MailAddress(remitente, "Universidad Corporativa Sigo", System.Text.Encoding.UTF8);
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

            asunto = asuntos;          
            mensaje = msgPersonalizado;
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


            foreach(string ruta in lista_rutas)
            {
                msg.Attachments.Add(new Attachment(GetStreamFile(ruta), Path.GetFileName(ruta), "application/pdf"));
            }
            
            
            
            msg.From = new MailAddress(remitente, "Universidad Corporativa Sigo", System.Text.Encoding.UTF8);
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

        private void EnviarCorreoCliente(string asuntos, string msgPersonalizado)
        {
            String asunto = "";
            String mensaje = "";
            asunto = asuntos;
            mensaje = msgPersonalizado;
            //String destintario = correo;
            String remitente = "soporteucs@gmail.com";
            MailMessage msg = new MailMessage();
            SmtpClient smtp = new SmtpClient();
            //
            // se asignan los destinatarios
            //
             msg.To.Add(cliente.correo_cliente);

            foreach (string ruta in lista_rutas)
            {
                msg.Attachments.Add(new Attachment(GetStreamFile(ruta), Path.GetFileName(ruta), "application/pdf"));
            }
            msg.From = new MailAddress(remitente, "Universidad Corporativa Sigo", System.Text.Encoding.UTF8);
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

        private void btnRutaArchivo_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog od = new OpenFileDialog();
                od.Filter = "PDF files |*.pdf";
                if (od.ShowDialog() == DialogResult.OK)
                {
                    
                     ruta= od.FileName;
                    lista_rutas.Add(ruta);
                    cantidad += 1;
                    labelCantidad.Text =cantidad.ToString();
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                
            }
            
        }

        private void lblRemoverUltimo_Click(object sender, EventArgs e)
        {
            for(int i=0; i < lista_rutas.Count; i++)
            {
                if (lista_rutas[i] == ruta)
                {
                    lista_rutas.RemoveAt(i);
                    cantidad = cantidad-1;
                    labelCantidad.Text = cantidad.ToString();
                }
            }
        }        

        private void btnEnviar_Click(object sender, EventArgs e)
        {
            if (AccesoInternet())
            {
                if (txtAsunto.Text != "")
                {
                    if (txtMensaje.Text != "")
                    {
                        if (lista_rutas.Count > 0)
                        {
                            if (Datos_envio_correo.opcion == 1)//correo a los participantes
                            {
                                EnviarCorreoParticipante(txtAsunto.Text, txtMensaje.Text);
                            }
                            else if (Datos_envio_correo.opcion == 2) //correo al facilitador
                            {
                                //se recorre dentro, la lista de las rutas para añadir el archivo al correo y enviarlo
                                EnviarCorreoFacilitador(txtAsunto.Text, txtMensaje.Text);

                            }
                            else if (Datos_envio_correo.opcion == 3) //correo cliente
                            {
                                EnviarCorreoCliente(txtAsunto.Text, txtMensaje.Text);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Debe anexar al menos un archivo.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                    else
                    {
                        MessageBox.Show("El mensaje no puede mandarse vacío.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
                else
                {
                    MessageBox.Show("Debe proporcionar un asunto al correo.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
               
            }else
            {
                MessageBox.Show("No es posible enviar el correo en estos momentos (Verifique su conexión a internet).", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
