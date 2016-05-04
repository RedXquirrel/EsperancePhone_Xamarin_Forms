using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace esperancephone.Views
{
    public partial class Hamburger : ContentView
    {
        public static readonly BindableProperty IconCharacterProperty = BindableProperty.Create(
          propertyName: "IconCharacter",
          returnType: typeof(string),
          declaringType: typeof(Hamburger),
          defaultValue: default(string),
          propertyChanged: TextChanged);

        private static void TextChanged(BindableObject bindable, object oldValue, object newValue)
        {
            ((Hamburger)bindable).TextLabel.Text = ((Hamburger)bindable).IconCharacter;
        }

        public string IconCharacter
        {
            get { return (string)GetValue(IconCharacterProperty); }
            set { SetValue(IconCharacterProperty, value); }
        }

        public static readonly BindableProperty IconColorProperty = BindableProperty.Create(
          propertyName: "IconColor",
          returnType: typeof(Color),
          declaringType: typeof(Hamburger),
          defaultValue: Color.Transparent,
          propertyChanged: IconColorChanged);

        private static void IconColorChanged(BindableObject bindable, object oldValue, object newValue)
        {
            ((Hamburger)bindable).TextLabel.TextColor = ((Hamburger)bindable).IconColor;
        }

        public Color IconColor
        {
            get { return (Color)GetValue(IconColorProperty); }
            set { SetValue(IconColorProperty, value); }
        }

        public static readonly BindableProperty TappedCommandProperty = BindableProperty.Create(
          propertyName: "TappedCommand",
          returnType: typeof(ICommand),
          declaringType: typeof(Hamburger),
          defaultValue: null,
          propertyChanged: TappedCommandChanged);

        private static void TappedCommandChanged(BindableObject bindable, object oldValue, object newValue)
        {
            ((Hamburger)bindable).TextLabel.TextColor = ((Hamburger)bindable).IconColor;
        }

        public ICommand TappedCommand
        {
            get { return (ICommand)GetValue(TappedCommandProperty); }
            set { SetValue(TappedCommandProperty, value); }
        }

        public Hamburger()
        {
            InitializeComponent();

            this.IconCharacter = "\uf0c9";
            this.IconColor = Color.Black;

            _fontSizeCache = TextLabel.FontSize;

            var tapGestureRecogniser = new TapGestureRecognizer();
            tapGestureRecogniser.Tapped += TapGestureRecogniser_Tapped;

            TextLabel.GestureRecognizers.Add(tapGestureRecogniser);
        }

        private double _fontSizeCache;

        private async void TapGestureRecogniser_Tapped(object sender, EventArgs e)
        {
            TapAnimation();
        }

        private void TapAnimation()
        {
            this.Animate(
                name: "ButtonTapAnimation",
                animation: new Xamarin.Forms.Animation((val) =>
                {
                    TextLabel.FontSize = _fontSizeCache - 4;
                }),
                easing: Easing.CubicOut,
                length: 250,
                finished: (val, b) =>
                {
                    ResetAnimation();
                },
                repeat: () => { return false; }
                );
        }

        private void ResetAnimation()
        {
            this.Animate(
                name: "ButtonResetAnimation",
                animation: new Xamarin.Forms.Animation((val) =>
                {
                    TextLabel.FontSize = _fontSizeCache;
                }),
                easing: Easing.CubicIn,
                length: 250,
                finished: (val, b) =>
                {
                    TappedCommand?.Execute(null);
                },
                repeat: () => { return false; }
                );
        }
    }
}
