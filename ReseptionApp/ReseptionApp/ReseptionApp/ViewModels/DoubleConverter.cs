using System;
using System.Globalization;
using System.Windows.Data;

namespace ReseptionApp.ViewModels
{
    public class DoubleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
                return ((double)value).ToString("N1", CultureInfo.InvariantCulture);

            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
