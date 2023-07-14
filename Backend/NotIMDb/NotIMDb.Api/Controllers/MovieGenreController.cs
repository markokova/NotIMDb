using NotIMDb.Service.Common;
using NotIMDb.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using NotIMDb.Model;

namespace NotIMDb.Api.Controllers
{
    public class MovieGenreController : ApiController
    {
        private IMovieGenreService Service { get; }
        public MovieGenreController(IMovieGenreService service)
        {
            Service = service;
        }

        // POST api/<controller>
        public async Task<HttpResponseMessage> PostAsync([FromBody] MovieGenre movieGenre)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid input");
            }
            try
            {
                if (await Service.PostAsync(movieGenre))
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

        // DELETE api/<controller>/5
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