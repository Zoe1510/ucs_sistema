using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Reflection;

namespace UCS_NODO_FGC.Clases
{
    public class Helper
    {
        public static Image ByteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            return Image.FromStream(ms);
        }

        public static byte[] ImageToByteArray(Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, ImageFormat.Jpeg);
            return ms.ToArray();
        }

        public static byte[] DocToByteArray(string doc)
        {
            
            byte[] documento = File.ReadAllBytes(doc);
            
            return documento;
        }
        
        //public static File ByteArrayToDoc(byte[] byteArray)
        //{
          
        //}

        public static Image ObtenerImagenNoDisponible()
        {
            Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            Stream file = assembly.GetManifestResourceStream("Helpers.Imagenes.NoDisponible.jpg");
            return Image.FromStream(file);
        }

    }
}
