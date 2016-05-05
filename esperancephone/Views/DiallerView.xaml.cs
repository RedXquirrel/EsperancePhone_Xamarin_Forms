using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace esperancephone.Views
{
    public partial class DiallerView : RelativeLayout
    {
        private static double _margin;

        public DiallerView()
        {
            InitializeComponent();

            this.Children.Add(DigitsPaneRelativeLayout,
                Constraint.RelativeToParent((parent) => 0),
                Constraint.RelativeToParent((parent) => 0),
                Constraint.RelativeToParent((parent) => parent.Width),
                Constraint.RelativeToParent((parent) => 50)
                );

            this.Children.Add(OptionsPaneRelativeLayout,
                Constraint.RelativeToParent((parent) => 0),
                Constraint.RelativeToParent((parent) => parent.Height - 50),
                Constraint.RelativeToParent((parent) => parent.Width),
                Constraint.RelativeToParent((parent) => 50)
                );

            this.Children.Add(DialPaneRelativeLayout,
                Constraint.RelativeToParent((parent) => 0),
                Constraint.RelativeToParent((parent) => 50),
                Constraint.RelativeToParent((parent) => parent.Width),
                Constraint.RelativeToParent((parent) => parent.Height-100)
                );

            _margin = 20;

            DialPaneRelativeLayout.Children.Add(DialPadRelativeLayout,
                Constraint.RelativeToParent((parent) => _margin),
                Constraint.RelativeToParent((parent) => _margin),
                Constraint.RelativeToParent((parent) => parent.Width - (2*_margin)),
                Constraint.RelativeToParent((parent) => parent.Height - _margin)
                );

            var superStringDictionary = new Dictionary<int, string>();
            superStringDictionary.Add(0,"1");
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
            fontAwesomeDictionary.Add(13, "\uf095");

            for (var x = 0; x < 15; x++)
            {
                if (x != 12 && x != 14)
                {
                    int b = x;
                    DialPadRelativeLayout.Children.Add(
                        new DiallerButtonView()
                        {
                            KeyString = superStringDictionary[b],
                            SubString = subStringDictionary [b],
                            CenterString = centerStringDictionary[b],
                            FontAwesomeString = fontAwesomeDictionary[b]
                        },
                        Constraint.RelativeToParent((parent) => (b % 3) * (parent.Width / 3)),  // the remainder determines the column
                        Constraint.RelativeToParent((parent) => (b / 3) * (parent.Height / 5)), // the division (with remainder discarded) determines the row
                        Constraint.RelativeToParent((parent) => parent.Width / 3),
                        Constraint.RelativeToParent((parent) => parent.Height / 5)
                        );
                }
            }
        }
    }
}
