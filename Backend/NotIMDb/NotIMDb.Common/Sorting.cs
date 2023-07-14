using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotIMDb.Common
{
    public class Sorting
    {

        public Sorting()
        {

        }
        public Sorting(string orderBy, string sortOrder)
        {
            Orderby = orderBy;

            SortOrder = sortOrder;
        }
        public string Orderby { get; set; }

        public string SortOrder { get; set; }
    }
}
