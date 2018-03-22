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
using System.Diagnostics;
using System.IO;

namespace UCS_NODO_FGC
{
    public partial class frmAyuda : Form
    {
        public frmAyuda()
        {
            InitializeComponent();
        }
        public void AddFormInPanel(object formHijo)
        {
            if (this.pnlDisplay.Controls.Count > 0)
                this.pnlDisplay.Controls.RemoveAt(0);
            Form fh = formHijo as Form;
            fh.TopLevel = false;
            fh.FormBorderStyle = FormBorderStyle.None;
            fh.Dock = DockStyle.None;
            this.pnlDisplay.Controls.Add(fh);
            this.pnlDisplay.Tag = fh;
            fh.Show();
            

        }
        private void frmAyuda_Load(object sender, EventArgs e)
        {
            this.Location = new Point(-5, 0);
            if (Clases.Usuario_logeado.cargo_usuario == "Lider")
            {
                btnBackUp.Enabled = true;
                btnRestore.Enabled = true;
                btnBackUp.Visible = true;
                btnRestore.Visible = true;
                btnCerrar.Location = new Point(0, 315);
            }
            else if (Clases.Usuario_logeado.cargo_usuario == "Coordinador")  //Si el usuario es coordinador
            {
                btnBackUp.Enabled = false;
                btnRestore.Enabled = false;
                btnBackUp.Visible =false;
                btnRestore.Visible = false;
                btnCerrar.Location = new Point(0, 181);
            }
            else if (Clases.Usuario_logeado.cargo_usuario == "Asistente")  //Si el usuario es asistente 
            {
                btnBackUp.Enabled = false;
                btnRestore.Enabled = false;
                btnBackUp.Visible = false;
                btnRestore.Visible = false;
                btnCerrar.Location = new Point(0, 181);
            }
        }

        private void Backup(String RutaGuardada)
        {
            //Los parametros de la base de datos para hacer el backup
            string constring = "server=localhost;user=root;pwd=root;database=ucs_bd;";

            // decidir el unicode y convertir fechas nulas en 0 para evitar errores (cosas de MySQL que no acepta como nulas)
            constring += "charset=latin1;convertzerodatetime=true;";


            //Se abre la conexion con el caracter unicode utf8 y los parametros de la base de datos
            using (MySqlConnection conn = new MySqlConnection(constring))
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {

                    using (MySqlBackup mb = new MySqlBackup(cmd))
                    {
                        cmd.Connection = conn;
                        conn.Open();

                        //Se exportan las tablas de la bd en el archivo creado por la ruta que selecciona el usuario

                        mb.ExportToFile(RutaGuardada);
                        conn.Close();
                        MessageBox.Show("Respaldo creado exitosamente.");
                    }
                }
            }
        }
        private void Restore(String ArchivoSeleccionado)
        {
            string constring = "server=localhost;user=root;pwd=root;database=ucs_bd;";

            // Important Additional Connection Options
            constring += "charset=latin1;convertzerodatetime=true;";



            using (MySqlConnection conn = new MySqlConnection(constring))
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    using (MySqlBackup mb = new MySqlBackup(cmd))
                    {
                        cmd.Connection = conn;
                        conn.Open();

                        mb.ImportFromFile(ArchivoSeleccionado);
                        conn.Close();
                        MessageBox.Show("Restaurado exitosamente.");
                    }
                }
            }

        }
        private void btnBackUp_Click(object sender, EventArgs e)
        {
            //Para decidir la ruta de guardado del archivo
            SaveFileDialog RutaDeGuardado = new SaveFileDialog();
            RutaDeGuardado.Filter = "Sql file|*.sql";
            RutaDeGuardado.Title = "Seleccione una ruta para hacer el respaldo";
            RutaDeGuardado.ShowDialog();

            //Que la ruta de guardado no sea vacia
            if (RutaDeGuardado.FileName != "")
            {


                System.IO.FileStream fs =
                   (System.IO.FileStream)RutaDeGuardado.OpenFile();

                fs.Close();

                //Se hace el backup y el archivo es el seleccionado en la ruta de guardado
                Backup(RutaDeGuardado.FileName);
            }
        }
        private void btnRestore_Click(object sender, EventArgs e)
        {
            OpenFileDialog SeleccionarArchivo = new OpenFileDialog();
            SeleccionarArchivo.Filter = "Sql file|*.sql";
            SeleccionarArchivo.Title = "Seleccione el archivo de restauración";
            SeleccionarArchivo.ShowDialog();
            if (SeleccionarArchivo.FileName != "")
            {


                System.IO.FileStream fs =
                   (System.IO.FileStream)SeleccionarArchivo.OpenFile();

                fs.Close();

                //Se hace el restore y el archivo es el seleccionado en la ruta de guardado
                Restore(SeleccionarArchivo.FileName);
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void btnAcercaDe_Click(object sender, EventArgs e)
        {
            if (pnlDisplay.Visible == false)
            {
                pnlDisplay.Visible = true;
            }else
            {
                pnlDisplay.Visible = false;
            }
        }

        private void btnManual_Click(object sender, EventArgs e)
        {
            if (Usuario_logeado.cargo_usuario == "Lider")
            {
                string ruta = @"C:\Users\ZM\Documents\Last_repo\ucs_sistema\UCS_NODO_FGC\Manuales\Manual de usuario lider.pdf";
                Process.Start(ruta);
            }
            else if (Clases.Usuario_logeado.cargo_usuario == "Coordinador")//Si el usuario es coordinador
            {
                string ruta = @"C:\Users\ZM\Documents\Last_repo\ucs_sistema\UCS_NODO_FGC\Manuales\";
                Process.Start(ruta);
            }
            else if (Clases.Usuario_logeado.cargo_usuario == "Asistente")
            {
                string ruta = @"C:\Users\ZM\Documents\Last_repo\ucs_sistema\UCS_NODO_FGC\Manuales\";
                Process.Start(ruta);
            }
        }
    }
}
