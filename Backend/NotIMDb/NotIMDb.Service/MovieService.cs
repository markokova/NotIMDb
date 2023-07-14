using NotIMDb.Common;
using NotIMDb.Model;
using NotIMDb.Repository;
using NotIMDb.Repository.Common;
using NotIMDb.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace NotIMDb.Service
{
    public class MovieService  : IMovieService
    {
        private readonly IMovieRepository _movieRepository;
        public MovieService(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }
        public async Task<PagedList<MovieView>> GetAllMoviesAsync(Sorting sorting, Paging paging, MovieFiltering filtering, CurrentUser currentUser)
        {
            if((bool)filtering.ShouldFilterById)
            {
                filtering.UserId = currentUser.Id;
            }
            else
            {
                filtering.UserId = Guid.Empty;
            }
            if(filtering.GenreId == null)
            {
                filtering.GenreId = Guid.Empty;
            }
            return await _movieRepository.GetMoviesAsync(sorting, paging, filtering);
        }

        public async Task<MovieView> GetMovieByIdAsync(Guid id)
        {
            return await _movieRepository.GetMovieByIdAsync(id);
        }

        public async Task<bool> AddMovieAsync(Movie newMovie)
        {
            newMovie.Id = Guid.NewGuid();
            newMovie.DateCreated = DateTime.UtcNow;
            newMovie.DateUpdated = DateTime.UtcNow;
            newMovie.IsActive = true;

            return await _movieRepository.AddMovieAsync(newMovie);
        }

        public async Task<bool> UpdateMovieAsync(Guid id, Movie updateMovie)
        {
            updateMovie.DateUpdated = DateTime.UtcNow;
            return await _movieRepository.UpdateMovieAsync(id, updateMovie);
        }

        public async Task<bool> DeleteMovieAsync(Guid id)
        {
            return await _movieRepository.DeleteMovieAsync(id);
        }
    }
}
