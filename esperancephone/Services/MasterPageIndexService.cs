using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using esperancephone.DataSources;

namespace esperancephone.Services
{
    public class MasterPageIndexService
    {
        public static IList<MasterDetailItemGroupDataSource> Index { get; set; }

        static MasterPageIndexService()
        {
            Index = MasterDetailItemGroupDataSource.Groups;
        }
    }
}
