using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspCoreApiDapperProject.Models
{
    public class GetParameter
    {
        const int maxpagesize = 50;
        public int pagenumber { get; set; }
        private int _pagesize = 10;
        public int PageSize
        {
            get
            {
                return _pagesize;
            }
            set
            {
                _pagesize = (value > maxpagesize) ? maxpagesize : value;
            }
        }

    }
}
