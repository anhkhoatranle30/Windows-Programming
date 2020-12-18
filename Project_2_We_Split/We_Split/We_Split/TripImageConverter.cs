using System;
using System.Globalization;
using System.Windows.Data;

namespace We_Split
{
    internal class TripImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var tripidstring = value.ToString();
            var tripImages = new TripImagesDAOsqlserver().GetTripAvatar(int.Parse(tripidstring));
            return AppDomain.CurrentDomain.BaseDirectory + "Images\\Trips\\" + tripidstring + "\\" + tripImages;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}