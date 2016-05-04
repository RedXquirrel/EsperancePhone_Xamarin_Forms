using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace esperancephone.DataTemplates.CustomCells
{
    public partial class FunctionCell : ViewCell
    {
        public FunctionCell()
        {
            InitializeComponent();

            IconLabel.FontFamily = Device.OnPlatform(
                "fontawesome",
                null,
                null
                );
        }
    }
}
