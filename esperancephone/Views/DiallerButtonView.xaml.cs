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
    public partial class DiallerButtonView : ContentView
    {
        private string _key;

        public string Key
        {
            get { return _key; }
            protected set
            {
                _key = value;
                SetKeyCode(_key);
            }
        }

        private void SetKeyCode(string _key)
        {
            switch (_key)
            {
                case "0": KeyCode = Keys.Key0; break;
                case "1": KeyCode = Keys.Key1; break;
                case "2": KeyCode = Keys.Key2; break;
                case "3": KeyCode = Keys.Key3; break;
                case "4": KeyCode = Keys.Key4; break;
                case "5": KeyCode = Keys.Key5; break;
                case "6": KeyCode = Keys.Key6; break;
                case "7": KeyCode = Keys.Key7; break;
                case "8": KeyCode = Keys.Key8; break;
                case "9": KeyCode = Keys.Key9; break;
                case "*": KeyCode = Keys.KeyStar; break;
                case "#": KeyCode = Keys.KeyHash; break;
                case "+": KeyCode = Keys.KeyPlus; break;
                case "<call>": KeyCode = Keys.KeyCall; break;
                default: throw new ArgumentException($"A KeyCode has not been assigned for input: [{_key}].");
              }
        }

        public Keys KeyCode { get; set; }

        private double _supLabelFontSizeCache;
        private double _subLabelFontSizeCache;
        private double _centerLabelFontSizeCache;
        private double _fontAwesomeLabelFontSizeCache;

        private string _keyString;
        public string KeyString
        {
            get { return _keyString; }
            set { _keyString = value; this.SupLabel.Text = _keyString; SetKey(_keyString);
            }
        }

        private void SetKey(string key)
        {
            if (!string.IsNullOrEmpty(key))
            {
                if (key.Equals("\uf098")) key = "<call>";
                this.Key = key;
            }
        }

        public ICommand PressedCommand { get; set; }

        private string _subString;
        public string SubString
        {
            get { return _subString; }
            set { _subString = value; this.SubLabel.Text = _subString; }
        }

        private string _centerString;
        public string CenterString
        {
            get { return _centerString; }
            set { _centerString = value; this.CenterLabel.Text = _centerString; SetKey(_centerString);  }
        }

        private string _fontAwesomeString;
        public string FontAwesomeString
        {
            get { return _fontAwesomeString; }
            set { _fontAwesomeString = value; this.FontAwesomeLabel.Text = _fontAwesomeString; SetKey(_fontAwesomeString); }
        }


        public DiallerButtonView()
        {
            InitializeComponent();

            _supLabelFontSizeCache = SupLabel.FontSize;
            _subLabelFontSizeCache = SubLabel.FontSize;
            _centerLabelFontSizeCache = CenterLabel.FontSize;
            _fontAwesomeLabelFontSizeCache = FontAwesomeLabel.FontSize;

            this.GestureRecognizers.Add(new TapGestureRecognizer(KeyTapped));
        }

        private void KeyTapped(View s, object e)
        {
            Debug.WriteLine($"INFORMATION: Key tapped value = {((DiallerButtonView)s).Key}");
            Debug.WriteLine($"INFORMATION: Key tapped key assigned = {((DiallerButtonView)s).KeyCode}");

            this.PressedCommand?.Execute(((DiallerButtonView)s).KeyCode);
            TapAnimation();
        }

        private void TapAnimation()
        {
            this.Animate(
                name: "DiallerButtonTapAnimation",
                animation: new Xamarin.Forms.Animation((val) =>
                {
                    SupLabel.FontSize = _supLabelFontSizeCache - 4;
                    SubLabel.FontSize = _subLabelFontSizeCache - 4;
                    CenterLabel.FontSize = _centerLabelFontSizeCache - 4;
                    FontAwesomeLabel.FontSize = _fontAwesomeLabelFontSizeCache - 4;
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
                    SupLabel.FontSize = _supLabelFontSizeCache;
                    SubLabel.FontSize = _subLabelFontSizeCache;
                    CenterLabel.FontSize = _centerLabelFontSizeCache;
                    FontAwesomeLabel.FontSize = _fontAwesomeLabelFontSizeCache;
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
