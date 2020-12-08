﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace We_Split
{
    class TripImageConverter : IValueConverter
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
