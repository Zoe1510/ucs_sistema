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
    public partial class Ayuda : Form
    {
        
        public Ayuda()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
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


        private void button2_Click(object sender, EventArgs e)
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

                //Se hace el backup y el archivo es el seleccionado en la ruta de guardado
                Restore(SeleccionarArchivo.FileName);
            }
            
        }


        private void Backup(String RutaGuardada)
        {
            //Los parametros de la base de datos para hacer el backup
            string constring = "server=localhost;user=root;pwd=123456;database=ucs_bd;";

            // decidir el unicode y convertir fechas nulas en 0 para evitar errores (cosas de MySQL que no acepta como nulas)
            constring += "charset=utf8;convertzerodatetime=true;";

           
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
            string constring = "server=localhost;user=root;pwd=123456;database=ucs_bd;";

            // Important Additional Connection Options
            constring += "charset=utf8;convertzerodatetime=true;";

        

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

        private void Ayuda_Load(object sender, EventArgs e)
        {

        }
    }
}
