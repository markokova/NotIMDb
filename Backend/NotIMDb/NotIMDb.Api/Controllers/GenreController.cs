using NotIMDb.Api.Mappers;
using NotIMDb.Api.Models.ActorRest;
using NotIMDb.Api.Models.GenreRest;
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
    public class GenreController : ApiController
    {
        private IGenreService Service { get; }
        RestDomainGenreMapper Mapper = new RestDomainGenreMapper();
        public GenreController(IGenreService service)
        {
            Service = service;
        }
        // GET: api/Genre
        public async Task<HttpResponseMessage> GetAsync(int pageSize = 10, int pageNumber = 1, string sortBy = "Id", string sortOrder = "asc", string filterString = null)
        {
            try
            {
                Paging paging = new Paging(pageSize, pageNumber);
                Sorting sorting = new Sorting(sortBy, sortOrder);
                GenreFiltering filtering = new GenreFiltering(filterString);

                PagedList<Genre> genres = await Service.GetAsync(paging, sorting, filtering);

                GenresRest genresRest = Mapper.MapToRest(genres);

                if (genresRest == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }

                return Request.CreateResponse(HttpStatusCode.OK, genresRest);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex);
            }
        }
        // POST: api/Genre
        public async Task<HttpResponseMessage> Post([FromBody]GenreRest rest)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid input");
            }
            try
            {
                if (await Service.PostAsync(Mapper.MapFromRest(rest)))
                {
                    return Request.CreateErrorResponse(HttpStatusCode.OK, "Successful");
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

        // PUT: api/Genre/5
        public async Task<HttpResponseMessage> Put(Guid id, [FromBody]GenreRest rest)
        {            
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid input");
            }
            try
            {
                if (await Service.PutAsync(id, Mapper.MapFromRest(rest)))
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "Genre updated");
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

        // DELETE: api/Genre/5
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
