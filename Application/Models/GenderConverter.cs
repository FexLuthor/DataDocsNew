using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Windows.Data;


namespace WpfApp3.Models
{
    public class GenderConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            
            if (value is int gender)
            {
                switch (gender)
                {
                    case 0:
                        return "male";
                    case 1:
                        return "female";
                    case 2:
                        return "diverse";
                    default:
                        return "other";
                }
            }
            return "unknown";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // might be needed for put and post requests
            throw new NotImplementedException();
        }
    }
}