using esperancephone.iOS;
using esperancephone.Views;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ShrinkingTextLabel), typeof(ShrinkingTextLabelRenderer))]

namespace esperancephone.iOS
{
    public class ShrinkingTextLabelRenderer : LabelRenderer
    {
        protected override void OnElementChanged(
            ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);

            var label = Control as UILabel;
            if (label != null)
            {
                label.AdjustsFontSizeToFitWidth = true;
                label.Lines = 1;
                label.BaselineAdjustment = UIBaselineAdjustment.AlignCenters;
                label.LineBreakMode = UILineBreakMode.Clip;
            }
        }
    }
}