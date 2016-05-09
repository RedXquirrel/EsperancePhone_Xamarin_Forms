using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using esperancephone.ViewModels;
using Xamarin.Forms;

namespace esperancephone.Extensions
{
    public static class DebuggingExtensions
    {
        public static void WriteLineInstanceAndInstanceId(this Page page)
        {
            Debug.WriteLine(
                $"INFORMATION: ViewModelType is {page.BindingContext.GetType()?.Name} and ViewModel Instance Id is {((StandardViewModel)page.BindingContext)?.ViewModelInstanceId}");
        }
    }
}
