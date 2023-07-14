using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NotIMDb.Api.Models.ReviewRest
{
    public class ReviewRestPut
    {
        public string Title { get; set; }

        public string Content { get; set; }

        public int Score { get; set; }        
    }
}