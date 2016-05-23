using System;
using System.Globalization;
using esperancephone.Models;
using Xamarin.Forms;

namespace esperancephone.Converters
{
    public class UserModeToBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var input = (UserMode) value;
            bool response = false;

            switch (input)
            {
                case UserMode.Basic:
                    response = false;
                    break;
                case UserMode.Advanced:
                    response = true;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return response;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? UserMode.Advanced : UserMode.Basic;
        }
    }
}