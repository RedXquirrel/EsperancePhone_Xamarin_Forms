using System;
using System.Collections.Generic;
using System.Windows.Input;
using esperancephone.Models;

namespace esperancephone.Interfaces
{
    public interface IDiallerService
    {
        Guid ServiceInstanceId { get; }
        void Press(Keys key);
        void PopKey();
        void PopAllkeys();
        IList<Keys> GetStack();
        IList<Keys> KeyStack { get; }
        ICommand CallAction { get; set; }
        string GetNumber(List<Keys> keys);
    }
}