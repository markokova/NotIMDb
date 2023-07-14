using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NotIMDb.Api.Models.UserRest
{
    public class UserLoginRest
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string FirstName{ get; set; }
        public string LastName { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }
    }
}