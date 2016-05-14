using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Com.Xamtastic.Patterns.SmallestMvvm;
using esperancephone.Models;

namespace esperancephone.ViewModels
{
    public class PersonaListItemViewModel : ViewModelBase
    {
        public PersonaListItemType TemplateSelectorType { get; set; }
        public object Data { get; set; }
    }
}
