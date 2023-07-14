using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotIMDb.Common
{
    public class Paging
    {
        public Paging()
        {
            
        }

        public Paging(int pageSize, int currentPage)
        {
            PageSize = pageSize;

            CurrentPage = currentPage;
        }
        public int PageSize { get; set; }

        public int CurrentPage { get; set; }

    }
}
