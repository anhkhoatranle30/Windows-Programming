using Cake_Shop.DAO;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Cake_Shop
{
    class CakeImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var cakeID = (int)value;
            var cakeImage = CakeDAOSQLServer.GetByID(cakeID).Image;
            var result = AppDomain.CurrentDomain.BaseDirectory + "Images\\Cakes\\" + cakeID.ToString() + "\\" + cakeImage;
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
