using System;
using System.Diagnostics;
using System.Globalization;
using Xamarin.Forms;

namespace esperancephone.Converters
{
    public class BindingContextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Debug.WriteLine($"INFORMATION: BINDING CONTEXT: {value.GetType().Name}");

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}