using System;
using System.Globalization;
using System.Windows.Data;

namespace PJK.WPF.PRISM.PM2020.Module.Mana
{ 
    public class WindowOpenCloseConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((bool)value == true)
                return "Open";
            else
                return "Closed";

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return false;
        }
    }
}
