using System;
using System.Globalization;
using System.Windows.Data;

namespace Cake_Shop
{
    public class MoneyFormatConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return null;
            }

            string str = value.ToString();

            double success = 0;
            double.TryParse(str, out success);

            return String.Format(CultureInfo.InvariantCulture,
                                            "{0:0,0}", success);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}