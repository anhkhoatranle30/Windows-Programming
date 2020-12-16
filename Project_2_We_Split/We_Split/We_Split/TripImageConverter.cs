using System;
using System.Globalization;
using System.Windows.Data;

namespace We_Split
{
    internal class TripImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var memberidstring = value.ToString();
            return AppDomain.CurrentDomain.BaseDirectory + "Images\\" + "Trips" + "\\" + memberidstring + "\\thumbnail.jpg";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}