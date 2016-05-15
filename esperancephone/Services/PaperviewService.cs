using System;
using esperancephone.Interfaces;
using esperancephone.Models;

namespace esperancephone.Services
{
    public class PaperviewService : IPaperviewService
    {
        public PaperviewModel CurrentPaperview { get; set; }
    }
}