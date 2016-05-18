using System;
using System.Globalization;
using esperancephone.Models;
using Xamarin.Forms;

namespace esperancephone.Converters
{
    public class BottomBarSelectionToIsVisibleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (string.IsNullOrEmpty(parameter.ToString())) return false;
            return ((BottomBarSelection)value).ToString().ToLower().Equals(parameter.ToString().ToLower()) ? true : false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}