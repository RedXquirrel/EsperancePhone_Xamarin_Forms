using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace esperancephone.Views
{
    public partial class DiallerButtonView : ContentView
    {
        private string _keyString;
        public string KeyString
        {
            get { return _keyString; }
            set { _keyString = value; this.SupLabel.Text = _keyString; }
        }

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
            set { _centerString = value; this.CenterLabel.Text = _centerString;  }
        }

        private string _fontAwesomeString;
        public string FontAwesomeString
        {
            get { return _fontAwesomeString; }
            set { _fontAwesomeString = value; this.FontAwesomeLabel.Text = _fontAwesomeString; }
        }


        public DiallerButtonView()
        {
            InitializeComponent();
        }
    }
}
