using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotIMDb.Common
{
    public class MovieFiltering
    {
        public int? Runtime { get; set; }

        public Guid? UserId { get; set; }

        public string FilterString { get; set; }

        public Guid? GenreId { get; set; }
        public bool? ShouldFilterById { get; set; }
        public bool? IsWatched { get; set; }
    }
}
