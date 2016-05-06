using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using esperancephone.Models;
using Xamarin.Forms;

namespace esperancephone.Views
{
    public partial class DiallerView : RelativeLayout
    {
        public Page HostPage { get; set; }

        private static readonly List<DiallerButtonView> Keys = new List<DiallerButtonView>();

        private static double _margin;

        public static readonly BindableProperty KeyPressedCommandProperty = BindableProperty.Create(
          propertyName: "KeyPressedCommand",
          returnType: typeof(ICommand),
          declaringType: typeof(DiallerView),
          defaultValue: default(string),
          propertyChanged: KeyPressedCommandChanged);

        private static void KeyPressedCommandChanged(BindableObject bindable, object oldValue, object newValue)
        {
            foreach (var key in Keys)
            {
                key.PressedCommand = ((DiallerView) bindable).KeyPressedCommand;
            }
        }

        public ICommand KeyPressedCommand
        {
            get { return (ICommand)GetValue(KeyPressedCommandProperty); }
            set { SetValue(KeyPressedCommandProperty, value); }
        }

        public static readonly BindableProperty PopCommandProperty = BindableProperty.Create(
          propertyName: "PopCommand",
          returnType: typeof(ICommand),
          declaringType: typeof(DiallerView),
          defaultValue: default(string),
          propertyChanged: PopCommandChanged);

        private static void PopCommandChanged(BindableObject bindable, object oldValue, object newValue)
        {

        }

        public ICommand PopCommand
        {
            get { return (ICommand)GetValue(PopCommandProperty); }
            set { SetValue(PopCommandProperty, value); }
        }

        public static readonly BindableProperty KeyStackProperty = BindableProperty.Create(
          propertyName: "KeyStack",
          returnType: typeof(IList<Keys>),
          declaringType: typeof(DiallerView),
          defaultValue: null,
          defaultBindingMode: BindingMode.TwoWay,
          propertyChanged: KeyStackChanged);

        private static void KeyStackChanged(BindableObject bindable, object oldValue, object newValue)
        {
            ((DiallerView) bindable).KeyStackLabel.Text = string.Empty;

            foreach (var key in ((DiallerView)bindable).KeyStack)
            {
                ((DiallerView) bindable).KeyStackLabel.Text += GetDisplayToken(key);
            }
        }

        private static string GetDisplayToken(Keys key)
        {
            var response = string.Empty;
            switch(key)
            {
                case Models.Keys.Key0:
                    response = "0";
                    break;
                case Models.Keys.Key1:
                    response = "1";
                    break;
                case Models.Keys.Key2:
                    response = "2";
                    break;
                case Models.Keys.Key3:
                    response = "3";
                    break;
                case Models.Keys.Key4:
                    response = "4";
                    break;
                case Models.Keys.Key5:
                    response = "5";
                    break;
                case Models.Keys.Key6:
                    response = "6";
                    break;
                case Models.Keys.Key7:
                    response = "7";
                    break;
                case Models.Keys.Key8:
                    response = "8";
                    break;
                case Models.Keys.Key9:
                    response = "9";
                    break;
                case Models.Keys.KeyStar:
                    response = "*";
                    break;
                case Models.Keys.KeyHash:
                    response = "#";
                    break;
                case Models.Keys.KeyPlus:
                    response = "+";
                    break;
                case Models.Keys.KeyCall:
                    throw new Exception("Dialler cannot display the call key character <call>");
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(key), key, null);
            }

            return response;
        }

        public IList<Keys> KeyStack
        {
            get { return (IList<Keys>) GetValue(KeyStackProperty); }
            set { SetValue(KeyStackProperty, value); }
        }

        private Label _addContactLabel;
        private Label _popItemLabel;
        public DiallerView()
        {
            InitializeComponent();

            var sizeMedium = Device.GetNamedSize(NamedSize.Medium, typeof(Label));

            Grid addContactsGrid = new Grid() { Padding = 10 };
            Grid popItemsGrid = new Grid() { Padding = 10 };

            _addContactLabel = new Label() { Text = "\uf234", FontFamily = "FontAwesome", FontSize = sizeMedium, VerticalOptions = LayoutOptions.Center, TextColor = Color.White };
            _popItemLabel = new Label() { Text = "\uf053\uf00d", FontFamily = "FontAwesome", FontSize = sizeMedium, VerticalOptions = LayoutOptions.Center, TextColor = Color.White };

            addContactsGrid.GestureRecognizers.Add(new TapGestureRecognizer(AddContact));
            popItemsGrid.GestureRecognizers.Add(new TapGestureRecognizer(PopItem));

            addContactsGrid.Children.Add(_addContactLabel);
            popItemsGrid.Children.Add(_popItemLabel);

            this.DigitsPaneRelativeLayout.Children.Add(addContactsGrid);
            this.DigitsPaneRelativeLayout.Children.Add(popItemsGrid);

            Grid.SetColumn(addContactsGrid, 0);
            Grid.SetColumn(popItemsGrid, 2);

            this.Children.Add(DigitsPaneRelativeLayout, Constraint.RelativeToParent((parent) => 0), Constraint.RelativeToParent((parent) => 0), Constraint.RelativeToParent((parent) => parent.Width), Constraint.RelativeToParent((parent) => 50));

            this.Children.Add(OptionsPaneRelativeLayout, Constraint.RelativeToParent((parent) => 0), Constraint.RelativeToParent((parent) => parent.Height - 50), Constraint.RelativeToParent((parent) => parent.Width), Constraint.RelativeToParent((parent) => 50));

            this.Children.Add(DialPaneRelativeLayout, Constraint.RelativeToParent((parent) => 0), Constraint.RelativeToParent((parent) => 50), Constraint.RelativeToParent((parent) => parent.Width), Constraint.RelativeToParent((parent) => parent.Height - 100));

            _margin = 20;

            DialPaneRelativeLayout.Children.Add(DialPadRelativeLayout, Constraint.RelativeToParent((parent) => _margin), Constraint.RelativeToParent((parent) => _margin), Constraint.RelativeToParent((parent) => parent.Width - (2*_margin)), Constraint.RelativeToParent((parent) => parent.Height - _margin));

            var superStringDictionary = new Dictionary<int, string>();
            superStringDictionary.Add(0, "1");
            superStringDictionary.Add(1, "2");
            superStringDictionary.Add(2, "3");
            superStringDictionary.Add(3, "4");
            superStringDictionary.Add(4, "5");
            superStringDictionary.Add(5, "6");
            superStringDictionary.Add(6, "7");
            superStringDictionary.Add(7, "8");
            superStringDictionary.Add(8, "9");
            superStringDictionary.Add(9, string.Empty);
            superStringDictionary.Add(10, "0");
            superStringDictionary.Add(11, string.Empty);
            superStringDictionary.Add(13, string.Empty);

            var subStringDictionary = new Dictionary<int, string>();
            subStringDictionary.Add(0, string.Empty);
            subStringDictionary.Add(1, "ABC");
            subStringDictionary.Add(2, "DEF");
            subStringDictionary.Add(3, "GHI");
            subStringDictionary.Add(4, "JKL");
            subStringDictionary.Add(5, "MNO");
            subStringDictionary.Add(6, "PQRS");
            subStringDictionary.Add(7, "TUV");
            subStringDictionary.Add(8, "WXYZ");
            subStringDictionary.Add(9, string.Empty);
            subStringDictionary.Add(10, "+");
            subStringDictionary.Add(11, string.Empty);
            subStringDictionary.Add(13, string.Empty);

            var centerStringDictionary = new Dictionary<int, string>();
            centerStringDictionary.Add(0, string.Empty);
            centerStringDictionary.Add(1, string.Empty);
            centerStringDictionary.Add(2, string.Empty);
            centerStringDictionary.Add(3, string.Empty);
            centerStringDictionary.Add(4, string.Empty);
            centerStringDictionary.Add(5, string.Empty);
            centerStringDictionary.Add(6, string.Empty);
            centerStringDictionary.Add(7, string.Empty);
            centerStringDictionary.Add(8, string.Empty);
            centerStringDictionary.Add(9, "*");
            centerStringDictionary.Add(10, string.Empty);
            centerStringDictionary.Add(11, "#");
            centerStringDictionary.Add(13, string.Empty);

            var fontAwesomeDictionary = new Dictionary<int, string>();
            fontAwesomeDictionary.Add(0, string.Empty);
            fontAwesomeDictionary.Add(1, string.Empty);
            fontAwesomeDictionary.Add(2, string.Empty);
            fontAwesomeDictionary.Add(3, string.Empty);
            fontAwesomeDictionary.Add(4, string.Empty);
            fontAwesomeDictionary.Add(5, string.Empty);
            fontAwesomeDictionary.Add(6, string.Empty);
            fontAwesomeDictionary.Add(7, string.Empty);
            fontAwesomeDictionary.Add(8, string.Empty);
            fontAwesomeDictionary.Add(9, string.Empty);
            fontAwesomeDictionary.Add(10, string.Empty);
            fontAwesomeDictionary.Add(11, string.Empty);
            fontAwesomeDictionary.Add(13, "\uf098");

            for (var x = 0; x < 15; x++)
            {
                if (x != 12 && x != 14)
                {
                    int b = x;

                    var button = new DiallerButtonView()
                    {
                        KeyString = superStringDictionary[b], SubString = subStringDictionary[b], CenterString = centerStringDictionary[b], FontAwesomeString = fontAwesomeDictionary[b], PressedCommand = KeyPressedCommand
                    };

                    Keys.Add(button);

                    DialPadRelativeLayout.Children.Add(button, Constraint.RelativeToParent((parent) => (b%3)*(parent.Width/3)), // the remainder determines the column
                        Constraint.RelativeToParent((parent) => (b/3)*(parent.Height/5)), // the division (with remainder discarded) determines the row
                        Constraint.RelativeToParent((parent) => parent.Width/3), Constraint.RelativeToParent((parent) => parent.Height/5));
                }
            }
        }

        private async void AddContact(View arg1, object arg2)
        {
            TapLabelAnimation(_addContactLabel);

            var action = await HostPage.DisplayActionSheet("Creating contact with Especial Personance", "Cancel", null, "Create New Contact", "Add to Existing Contact");
            Debug.WriteLine("Action: " + action);
            await HostPage.DisplayAlert("Not Implemented", $"You selected {action}", "OK");
        }

        private void PopItem(View s, object e)
        {
            TapLabelAnimation(_popItemLabel);
            this.PopCommand?.Execute(null);
        }

        private double _tapLabelAnimationFontSizeCache;

        private void TapLabelAnimation(Label label)
        {
            _tapLabelAnimationFontSizeCache = label.FontSize;
            this.Animate(
                name: "TapLabelAnimation",
                animation: new Xamarin.Forms.Animation((val) =>
                {
                    label.FontSize = _tapLabelAnimationFontSizeCache - 4;
                }),
                easing: Easing.CubicOut,
                length: 250,
                finished: (val, b) =>
                {
                    ResetAnimation(label);
                },
                repeat: () => { return false; }
                );
        }

        private void ResetAnimation(Label label)
        {
            this.Animate(
                name: "TapLabelResetAnimation",
                animation: new Xamarin.Forms.Animation((val) =>
                {
                    label.FontSize = _tapLabelAnimationFontSizeCache;
                }),
                easing: Easing.CubicIn,
                length: 250,
                finished: (val, b) =>
                {
                    //TappedCommand?.Execute(null);
                },
                repeat: () => { return false; }
                );
        }
    }
}
