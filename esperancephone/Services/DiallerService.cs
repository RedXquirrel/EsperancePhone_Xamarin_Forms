using System;
using System.Collections.Generic;
using System.Diagnostics;
using esperancephone.Interfaces;
using esperancephone.Models;

namespace esperancephone.Services
{
    public class DiallerService : IDiallerService
    {
        private readonly IList<Keys> _stackKeys = new List<Keys>();

        public void Press(Keys key)
        {
            Debug.WriteLine($"INFORMATION: DIALLERSERVICE: Key pressed is {key}");
            if (key != Keys.KeyCall)
            {
                _stackKeys.Add(key);
            }
            else
            {
                Call();
            }
        }

        public void PopKey()
        {
            if (_stackKeys.Count > 0)
            {
                _stackKeys.RemoveAt(_stackKeys.Count - 1);
            }
        }

        public void PopAllkeys()
        {
            _stackKeys.Clear();
        }

        public IList<Keys> GetStack()
        {
            return _stackKeys;
        }

        private static void Call()
        {

        }
    }
}