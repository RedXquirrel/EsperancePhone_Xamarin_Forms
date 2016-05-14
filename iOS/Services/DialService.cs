using System;
using System.Text.RegularExpressions;
using esperancephone.Interfaces;
using Foundation;
using UIKit;

namespace esperancephone.iOS.Services
{
    public class DialService : IDialService
    {
        public bool Dial(string number)
        {
            number = number.Replace("(0)", "");
            number = Regex.Replace(number, @"\s+", "");
            return UIApplication.SharedApplication.OpenUrl(new NSUrl("tel:" + number));
        }
    }
}