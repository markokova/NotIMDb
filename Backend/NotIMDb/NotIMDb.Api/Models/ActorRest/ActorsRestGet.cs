using NotIMDb.Api.Models.ReviewRest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NotIMDb.Api.Models.ActorRest
{
    public class ActorsRestGet
    {
        public List<ActorRest> actorRests { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
    }
}