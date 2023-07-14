using NotIMDb.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NotIMDb.Api.Models.MovieRest
{
    public class MovieRestGet
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public int Runtime { get; set; }
        public string Image { get; set; }
        public DateTime YearOfRelease { get; set; }

        public string Actors { get; set; }

        public string Genres { get; set; }

        public decimal AverageScore { get; set; }
    }
}