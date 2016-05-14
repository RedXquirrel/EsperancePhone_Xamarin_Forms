using System;
using esperancephone.iOS.Renderers;
using esperancephone.Models;
using esperancephone.Views;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(SelectionStyleViewCell), typeof(SelectionStyleViewCellRenderer))]

namespace esperancephone.iOS.Renderers
{
    /// <summary>
    /// Derived from https://developer.xamarin.com/guides/xamarin-forms/custom-renderer/viewcell/
    /// </summary>
    public class SelectionStyleViewCellRenderer : ViewCellRenderer
    {
        public override UIKit.UITableViewCell GetCell(Cell item, UIKit.UITableViewCell reusableCell, UIKit.UITableView tv)
        {
            var formsControl = (SelectionStyleViewCell) item;

            var cell = base.GetCell(item, reusableCell, tv);

            switch (formsControl.SelectionStyle)
            {
                case CellSelectionStyle.Default:
                    cell.SelectionStyle = UITableViewCellSelectionStyle.Default;
                    break;
                case CellSelectionStyle.None:
                    cell.SelectionStyle = UITableViewCellSelectionStyle.None;
                    break;
                case CellSelectionStyle.Blue:
                    cell.SelectionStyle = UITableViewCellSelectionStyle.Blue;
                    break;
                case CellSelectionStyle.Grey:
                    cell.SelectionStyle = UITableViewCellSelectionStyle.Gray;
                    break;
                default:
                    cell.SelectionStyle = UITableViewCellSelectionStyle.Default;
                    break;
            }
            
            return cell;
        }
    }
}