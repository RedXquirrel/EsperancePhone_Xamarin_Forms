using System.Collections.Generic;
using esperancephone.Models;

namespace esperancephone.Interfaces
{
    public interface IDiallerService
    {
        void Press(Keys key);
        void PopKey();
        void PopAllkeys();
        IList<Keys> GetStack();
    }
}