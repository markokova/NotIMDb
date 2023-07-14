using NotIMDb.Common;
using NotIMDb.Model;
using NotIMDb.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;

namespace NotIMDb.Api.Controllers
{
    public class WatchlistController : ApiController
    {
        private IWatchlistService Service { get; set; }
        public WatchlistController(IWatchlistService service)
        {
            Service = service;
        }

        [HttpPost]
        //[Authorize(Roles = "User, Administrator")]
        public async Task<HttpResponseMessage> AddToWatchListAsync(Guid id)
        {
            CurrentUser currentUser = new CurrentUser();
            //currentUser.Id = GetIdentity();
            currentUser.Id = Guid.Parse("08d58761-429b-49a3-9bce-9b7ca1b3459a");

            int affectedRows = await Service.AddToWatchListAsync(id,currentUser);

            if(affectedRows == 0)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            return Request.CreateResponse(HttpStatusCode.OK);

        }

        [HttpPut]
        //[Authorize(Roles = "User, Administrator")]
        public async Task<HttpResponseMessage> MarkAsWatchedAsync(Guid id)
        {
            CurrentUser currentUser = new CurrentUser();
            //currentUser.Id = GetIdentity();
            currentUser.Id = Guid.Parse("08d58761-429b-49a3-9bce-9b7ca1b3459a");
            int affectedRows = await Service.MarkAsWatchedAsync(id, currentUser);

            if(affectedRows == 0)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, affectedRows);
            }
            return Request.CreateResponse(HttpStatusCode.OK, affectedRows);
        }

        [HttpDelete]
        //[Authorize(Roles = "User, Administrator")]
        public async Task<HttpResponseMessage> DeleteFromWatchListAsync(Guid id)
        {
            CurrentUser currentUser = new CurrentUser();
            //currentUser.Id = GetIdentity();
            currentUser.Id = Guid.Parse("08d58761-429b-49a3-9bce-9b7ca1b3459a");

            int affectedRows = await Service.DeleteFromWatchListAsync(id, currentUser);
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    
        private Guid GetIdentity()
        {
            ClaimsIdentity identity = System.Web.HttpContext.Current.User.Identity as ClaimsIdentity;
            string userIdString = identity.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            Guid userId = Guid.Empty;
            if (Guid.TryParse(userIdString, out Guid userGuid))
            {
                userId = userGuid;
            }
            return userId;
        }
    }
}
