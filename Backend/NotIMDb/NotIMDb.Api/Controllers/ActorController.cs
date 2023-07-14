using NotIMDb.Api.Mappers;
using NotIMDb.Api.Models.ActorRest;
using NotIMDb.Api.Models.ReviewRest;
using NotIMDb.Common;
using NotIMDb.Model;
using NotIMDb.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace NotIMDb.Api.Controllers
{
    public class ActorController : ApiController
    {
        private IActorService Service { get;}

        RestDomainActorMapper Mapper = new RestDomainActorMapper();
        public ActorController(IActorService service)
        {
            Service = service;
        }

        // GET: api/Actor
        public async Task<HttpResponseMessage> GetAsync(int pageSize = 10, int pageNumber = 1, string sortBy = "Id", string sortOrder = "asc", string filterString = null)
        {
            try
            {
                Paging paging = new Paging(pageSize,pageNumber);
                Sorting sorting = new Sorting(sortBy,sortOrder);
                ActorFiltering filtering = new ActorFiltering(filterString);

                PagedList<Actor> actors = await Service.GetAsync(paging,sorting,filtering);

                ActorsRestGet actorsRest = Mapper.MapToRest(actors);

                if (actorsRest == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }

                return Request.CreateResponse(HttpStatusCode.OK, actorsRest);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        // GET: api/Actor/5
        public async Task<HttpResponseMessage> GetAsync(Guid id)
        {
            try
            {
                ActorRest actorRest = Mapper.MapToRest(await Service.GetAsync(id));

                return Request.CreateResponse(HttpStatusCode.OK, actorRest);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        // POST: api/Actor
        public async Task<HttpResponseMessage> PostAsync([FromBody] ActorRest rest)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid input");
            }
            try
            {
                if (await Service.PostAsync(Mapper.MapFromRest(rest)))
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "Successful");
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid input");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }

        }

        // PUT: api/Actor/5
        public async Task<HttpResponseMessage> Put(Guid id, [FromBody]ActorRest rest)
        { 
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid input");
            }
            try
            {
                if (await Service.PutAsync(id, Mapper.MapFromRest(rest)))
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "Actor updated");
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid request");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        // DELETE: api/Actor/5
        public async Task<HttpResponseMessage> Delete(Guid id)
        {
            try
            {
                if (await Service.DeleteAsync(id))
                {
                    return Request.CreateResponse(HttpStatusCode.Gone, "Succesfully deleted.");
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid request.");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
    }
}
