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

        public string GetNumber(List<Keys> keys)
        {
            var response = string.Empty;

            foreach (var key in keys)
            {
                switch (key)
                {
                    case Keys.Key0:
                        response += "0";
                        break;
                    case Keys.Key1:
                        response += "1";
                        break;
                    case Keys.Key2:
                        response += "2";
                        break;
                    case Keys.Key3:
                        response += "3";
                        break;
                    case Keys.Key4:
                        response += "4";
                        break;
                    case Keys.Key5:
                        response += "5";
                        break;
                    case Keys.Key6:
                        response += "6";
                        break;
                    case Keys.Key7:
                        response += "7";
                        break;
                    case Keys.Key8:
                        response += "8";
                        break;
                    case Keys.Key9:
                        response += "9";
                        break;
                    case Keys.KeyStar:
                        response += "*";
                        break;
                    case Keys.KeyHash:
                        response += "#";
                        break;
                    case Keys.KeyPlus:
                        response += "+";
                        break;
                    case Keys.KeyCall:
                        throw new Exception("Keys.KeyCall is not a valid key input list item to derive a number from [esperancephone.Services.DiallerService].");
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            return response;
        }
    }
}