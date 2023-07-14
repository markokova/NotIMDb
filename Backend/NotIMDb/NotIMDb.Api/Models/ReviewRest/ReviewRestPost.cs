using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NotIMDb.Api.Models.ReviewRest
{
    public class ReviewRestPost
    {
        public string Title { get; set; }

        public string Content { get; set; }

        public int Score { get; set; }

        public Guid MovieId { get; set; }

        public Guid CreatedByUserId { get; set; }

        public Guid UpdatedByUserId { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateUpdated { get; set; }
    }
}