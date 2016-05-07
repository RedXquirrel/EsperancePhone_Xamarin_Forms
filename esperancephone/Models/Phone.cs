using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using esperancephone.Interfaces;

namespace esperancephone.Models
{
    public class Phone : IPhone
    {
        public string Label { get; set; }
        public string Number { get; set; }
    }
}
