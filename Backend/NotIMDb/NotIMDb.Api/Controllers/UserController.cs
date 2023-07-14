using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using NotIMDb.Api.App_Start;
using NotIMDb.Api.Models.UserRest;
using NotIMDb.Model;
using NotIMDb.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;

namespace NotIMDb.Api.Controllers
{
    [RoutePrefix("api/user")]

    public class UserController : ApiController
    {
        private readonly IUserService _UserService;

        public UserController(IUserService UserService)
        {
            _UserService = UserService;
        }
        [HttpPost]
        [Route("register")]

        public async Task<HttpResponseMessage> Register([FromBody] User request)
        {
            string result = await _UserService.RegisterAsync(request);

            if (result == "Registration successful!")
            {
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest, result);
        }

        [HttpPost]
        [Route("login")]

        public async Task<HttpResponseMessage> Login([FromBody] User request)
        {
            
            User user = await _UserService.ValidateUserAsync(request);

            if (user != null)
            {
                Role role = await _UserService.GetUserRoleAsync(user.RoleId);
                ClaimsIdentity identity = new ClaimsIdentity(OAuthDefaults.AuthenticationType);
                identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
                identity.AddClaim(new Claim("Email", user.Email));
                identity.AddClaim(new Claim(ClaimTypes.Role, role.Title));
                var ticket = new AuthenticationTicket(identity, new AuthenticationProperties());
                var accessToken = Startup.OAuthOptions.AccessTokenFormat.Protect(ticket);
                
                UserLoginRest userData = new UserLoginRest()
                {
                    Id = user.Id,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Role = role.Title,
                    Token = accessToken
                };
                return Request.CreateResponse(HttpStatusCode.OK, userData);
            }

            return Request.CreateResponse(HttpStatusCode.BadRequest, "Bad Credentials");
        }


        [HttpPost]
        [Route("authTest")]
        [Authorize(Roles = "User")]
        public async Task<HttpResponseMessage> AuthTest([FromBody] User request)
        {
            ClaimsIdentity identity = System.Web.HttpContext.Current.User.Identity as ClaimsIdentity;
            string userId = identity.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return Request.CreateResponse(HttpStatusCode.OK, userId + " - That's one pretty Guid" );
        }

        [HttpGet]
        [Route("getroles")]
        //[Authorize(Roles = "User, Administrator")]
        public async Task<HttpResponseMessage> GetRoles()
        {
            List<Role> roles = new List<Role>();
            roles = await _UserService.GetRoles();
            return roles.Count() > 0 ?  Request.CreateResponse(HttpStatusCode.OK, roles ) : Request.CreateResponse(HttpStatusCode.NotFound, "Not found");
        }


    }
}
