using NotIMDb.Api.Models.UserRest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NotIMDb.Api.Models.ReviewRest
{
    public class ReviewRestGet
    {
        public string Title { get; set; }

        public string Content { get; set; }

        public int Score { get; set; }

        public string UserName { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateUpdated { get; set; }

        //public UserRestGet userRestGet { get; set; }
    }
}