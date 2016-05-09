using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace esperancephone.Views
{
    public class TappableGrid : Grid
    {
        public static readonly BindableProperty TappedCommandProperty = BindableProperty.Create(
            propertyName: "TappedCommand",
            returnType: typeof(ICommand),
            declaringType: typeof(TappableGrid),
            defaultValue: null,
            propertyChanged: TappedCommandChanged);

        private static void TappedCommandChanged(BindableObject bindable, object oldValue, object newValue)
        {
           
        }

        public ICommand TappedCommand
        {
            get { return (ICommand)GetValue(TappedCommandProperty); }
            set { SetValue(TappedCommandProperty, value); }
        }

        public TappableGrid()
        {
            this.GestureRecognizers.Add(new TapGestureRecognizer(Tapped));
        }

        private void Tapped(View arg1, object arg2)
        {
            TappedCommand?.Execute(null);
        }
    }
}
