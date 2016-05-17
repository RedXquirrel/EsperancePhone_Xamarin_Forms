using System;

namespace esperancephone.Models
{
    public class CurrentPageCacheModel
    {
        public Type PageCache { get; set; }
        public Type ViewModelCache { get; set; }
        public object Parameters { get; set; }
    }
}