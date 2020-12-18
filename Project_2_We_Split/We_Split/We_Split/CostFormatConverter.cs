using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace We_Split
{
    public class CostFormatConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string str = value.ToString();

            if (value == null)
            {
                Console.WriteLine("null");
            }

            double success = 0;
            double.TryParse(str, out success);

            Console.WriteLine(str);

            return String.Format(CultureInfo.InvariantCulture,
                                            "{0:0,0}", success);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}