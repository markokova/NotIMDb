using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NotIMDb.Api.Models.ReviewRest
{
    public class ReviewsRestGet
    {
        public List<ReviewRestGet> reviewRests { get; set; }

        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
    }
}