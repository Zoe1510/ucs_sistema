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

    public static class RelativeTimeExtensions
    {
        public static String RelativeTime(this TimeSpan ts)
        {
            const int second = 1;
            const int minute = 60 * second;
            const int hour = 60 * minute;
            const int day = 24 * hour;
            const int month = 30 * day;
            string tt = String.Format("{0:00}:{1:00}:{2:00}", ts.Hours, ts.Minutes, ts.Seconds);
            double delta = Math.Abs(ts.TotalSeconds);
            if (delta < 1 * minute)
            { //melhor se escrever só "Agora há pouco"
                return (ts.Seconds == 1 ? "un segundo" : ts.Seconds + " segundos");
            }
            if (delta < 2 * minute)
            {
                return ts.Minutes + " minuto";
            }
            if (delta < 45 * minute)
            {
                return ts.Minutes + " min. con " + ts.Seconds + " seg.";
            }
            if (delta < 90 * minute)
            {
                return ts.Hours + " hora con " + ts.Minutes + " min.";
            }
            if (delta < 24 * hour)
            {
                return ts.Hours + " horas con " + ts.Minutes + " min.";
            }
            if (delta < 48 * hour)
            {
                return tt + " día";
            }
            if (delta < 30 * day)
            {
                return tt + " días";
            }
            if (delta < 12 * month)
            {
                var months = Convert.ToInt32(Math.Floor((double)ts.Days / 30));
                return (months <= 1 ? "un mes" : months + " meses");
            }
            else
            {
                var years = Convert.ToInt32(Math.Floor((double)ts.Days / 365));
                return (years <= 1 ? "un año" : years + " años");
            }
        }

        public static String RelativeTimes(this TimeSpan ts)
        {
            const int second = 1;
            const int minute = 60 * second;
            const int hour = 60 * minute;
            const int day = 24 * hour;
            const int month = 30 * day;
            string tt = String.Format("{0:00}:{1:00}:{2:00}", ts.Hours, ts.Minutes, ts.Seconds);
            double delta = Math.Abs(ts.TotalSeconds);
            if (delta < 1 * minute)
            { //melhor se escrever só "Agora há pouco"
                return (ts.Seconds == 1 ? "un segundo" : ts.Seconds + " segundos");
            }
            if (delta < 2 * minute)
            {
                return ts.Minutes + " minuto";
            }
            if (delta < 45 * minute)
            {
                return ts.Minutes + " min. con " + ts.Seconds + " seg.";
            }
            if (delta < 90 * minute)
            {
                return ts.Hours + " hora con " + ts.Minutes + " min.";
            }
            if (delta < 24 * hour)
            {
                return ts.Hours + " horas con " + ts.Minutes + " min.";
            }
            if (delta < 48 * hour)
            {
                return tt + " día";
            }
            if (delta < 30 * day)
            {
                return tt + " días";
            }
            if (delta < 12 * month)
            {
                var months = Convert.ToInt32(Math.Floor((double)ts.Days / 30));
                return (months <= 1 ? "un mes" : months + " meses");
            }
            else
            {
                var years = Convert.ToInt32(Math.Floor((double)ts.Days / 365));
                return (years <= 1 ? "un año" : years + " años");
            }
        }
    }

}
