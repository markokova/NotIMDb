using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NotIMDb.Api.Models.MovieRest
{
    public class MovieRestPut
    {
        public string Title { get; set; }
        public int Runtime { get; set; }
        public DateTime YearOfRelease { get; set; }
        public Guid UpdatedByUserId { get; set; }
    }
}