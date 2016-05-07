using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Input;
using esperancephone.Interfaces;
using esperancephone.Models;

namespace esperancephone.Services
{
    public class DiallerService : IDiallerService
    {
        private readonly IList<Keys> _stackKeys = new List<Keys>();

        public ICommand CallAction { get; set; }
        public IList<Keys> KeyStack { get { return _stackKeys; } }

        public Guid ServiceInstanceId { get; protected set; }

        public DiallerService()
        {
            this.ServiceInstanceId = Guid.NewGuid();
            Debug.WriteLine($"INFORMATION: DIALLERSERVICE instantiating with Id {ServiceInstanceId.ToString()}");
        }

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

        private void Call()
        {
            this.CallAction?.Execute(GetStack());
        }
    }
}