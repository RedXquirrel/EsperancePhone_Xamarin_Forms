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
            
            for (var x = 0; x < 15; x++)
            {
                if (x != 12 && x != 14)
                {
                    int b = x;
                    var grid = new Grid() { };
                    grid.Children.Add(new Label() { Text = b.ToString(), HorizontalOptions = LayoutOptions.Center, VerticalOptions = LayoutOptions.Center});
                    DialPadRelativeLayout.Children.Add(grid,
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
