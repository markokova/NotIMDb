using NotIMDb.Api.Mappers;
using NotIMDb.Api.Models.MovieRest;
using NotIMDb.Common;
using NotIMDb.Model;
using NotIMDb.Repository.Common;
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
    public class MovieController : ApiController
    {
        private readonly IMovieService _movieService;
        
        public MovieController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpGet]
        //[Authorize]
        //GET: api/Movie
        public async Task<HttpResponseMessage> GetAllMoviesAsync(string orderBy = "Runtime", string sortOrder = "DESC", int pageSize = 10, int currentPage = 1, int? runtime = null, Guid? userId = null, Guid? genreId = null, bool? shouldFilterByUserId = false, bool? isWatched = false)
        {
            Sorting sorting = new Sorting() { Orderby = orderBy, SortOrder = sortOrder};
            MovieFiltering movieFiltering = new MovieFiltering() { Runtime = runtime, UserId = userId, GenreId = genreId, ShouldFilterById=shouldFilterByUserId, IsWatched=isWatched};
            Paging paging = new Paging() { PageSize = pageSize, CurrentPage = currentPage };
            RestDomainMovieMapper restDomainMovieMapper = new RestDomainMovieMapper();
           
            CurrentUser currentUser = new CurrentUser();
            //currentUser.Id = GetIdentity();
            currentUser.Id = Guid.Parse("08d58761-429b-49a3-9bce-9b7ca1b3459a");

            PagedList<MovieView> allMovies = await _movieService.GetAllMoviesAsync(sorting, paging, movieFiltering, currentUser);           


            if(allMovies != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK,restDomainMovieMapper.MapToRest(allMovies));
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest, "There's something wrong with your request!");
        }

        [HttpGet]
        //[Authorize]
        //GET: api/Movie
        public async Task<HttpResponseMessage> GetMovieByIdAsync(Guid id)
        {
            RestDomainMovieMapper restDomainMovieMapper = new RestDomainMovieMapper();

            MovieView movieView = await _movieService.GetMovieByIdAsync(id);

            if (movieView != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, restDomainMovieMapper.MapToRest(movieView));
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest, "There's something wrong with your request!");
        }

        //POST: api/Movie
        [HttpPost]
        //[Authorize(Roles = "Administrator")]
        public async Task<HttpResponseMessage> CreateMovieAsync([FromBody] MovieRestPost newMovie)
        {
            RestDomainMovieMapper restDomainMovieMapper = new RestDomainMovieMapper();
            //Guid userId = GetIdentity();
                                            //zakomentirao sam stvari za Identity i hardkodirao usera kako bi mogao testirati frontend
            if (newMovie != null)
            {
                //newMovie.CreatedByUserId = userId;
                //newMovie.UpdatedByUserId = userId;

                newMovie.CreatedByUserId = Guid.Parse("4bab87d6-a735-4cbe-b785-0c0ddc4cddc0");
                newMovie.UpdatedByUserId = Guid.Parse("4bab87d6-a735-4cbe-b785-0c0ddc4cddc0");

                bool isAdded = await _movieService.AddMovieAsync(restDomainMovieMapper.MapFromRest(newMovie));

                if (isAdded)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "Movie successfully added!");
                }
                return Request.CreateResponse(HttpStatusCode.BadRequest, "There was an issue while adding your movie!");                
            }
            return Request.CreateResponse(HttpStatusCode.BadGateway, "There's an issue with your input.");
        }

        //PUT: api/Movie/5
        [HttpPut]
        //[Authorize(Roles = "Administrator")]
        public async Task<HttpResponseMessage> UpdateMovieAsync(Guid id, [FromBody] MovieRestPut updateMovie)
        {
            RestDomainMovieMapper restDomainMovieMapper = new RestDomainMovieMapper();
            Guid userId = GetIdentity();

            if (updateMovie != null)
            {   
                
                 updateMovie.UpdatedByUserId = userId;

                 bool isUpdated = await _movieService.UpdateMovieAsync(id, restDomainMovieMapper.MapFromRest(updateMovie));

                 if (isUpdated)
                 { 
                     return Request.CreateResponse(HttpStatusCode.OK, "Movie successfully edited!");
                 }
                 return Request.CreateResponse(HttpStatusCode.BadRequest, "There was an issue while updating your movie!");
                
            }
            return Request.CreateResponse(HttpStatusCode.BadGateway, "There's an issue with your input");
        }

        // DELETE: api/Movie/5
        [HttpDelete]
        //[Authorize(Roles = "Administrator")]
        public async Task<HttpResponseMessage> DeleteMovieAsync(Guid id)
        {
            bool isDeleted = await _movieService.DeleteMovieAsync(id);

            if(isDeleted) 
            {
                return Request.CreateResponse(HttpStatusCode.OK, "Movie deleted!");
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest, "There was an issue with deleting your movie!");
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
