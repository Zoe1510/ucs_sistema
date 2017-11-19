using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UCS_NODO_FGC.Clases
{
    public static class HelperFunctions
    {
        public static String GetValueComboxBox(ComboBox combobox)
        {
            return combobox.GetItemText(combobox.SelectedItem);
        }
    }
}
