using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NotIMDb.Api.Models.MovieRest
{
    public class MovieRestPost
    {
        public string Title { get; set; }
        public int Runtime { get; set; }
        public DateTime YearOfRelease { get; set; }
        public List<Guid> ActorIds { get; set; }
        public List<Guid> GenreIds { get; set; }
        public Guid CreatedByUserId { get; set; }
        public Guid UpdatedByUserId { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
    }
}