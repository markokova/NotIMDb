using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotIMDb.Model
{
    public class MovieView
    {
        public Movie Movie { get; set; }

        public string Actors { get; set; }

        public string Genres { get; set; }

        public decimal AverageScore { get; set; }
    }
}
