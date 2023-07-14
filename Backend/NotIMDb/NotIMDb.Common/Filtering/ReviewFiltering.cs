using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotIMDb.Common
{
    public class ReviewFiltering
    {
        public ReviewFiltering(int? score, string filterString, Guid? movieId)
        {
            Score = score;
            FilterString = filterString;
            MovieId = movieId;
        }
        public int? Score { get; set; }

        public string FilterString { get; set; }

        public Guid? MovieId { get; set; }
    }
}
