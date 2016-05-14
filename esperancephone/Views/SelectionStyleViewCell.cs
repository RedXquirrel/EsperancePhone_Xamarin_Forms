using System.Windows.Input;
using esperancephone.Models;
using Xamarin.Forms;

namespace esperancephone.Views
{
    public class SelectionStyleViewCell : ViewCell
    {
        public static readonly BindableProperty SelectionStyleProperty = BindableProperty.Create(
            propertyName: "SelectionStyle",
            returnType: typeof(CellSelectionStyle),
            declaringType: typeof(SelectionStyleViewCell),
            defaultValue: CellSelectionStyle.Default,
            propertyChanged: SelectionStyleChanged);

        private static void SelectionStyleChanged(BindableObject bindable, object oldValue, object newValue)
        {

        }

        public CellSelectionStyle SelectionStyle
        {
            get { return (CellSelectionStyle)GetValue(SelectionStyleProperty); }
            set { SetValue(SelectionStyleProperty, value); }
        }
    }
}