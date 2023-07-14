using NotIMDb.Common;
using NotIMDb.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotIMDb.Service.Common
{
    public interface IMovieService
    {
        Task<PagedList<MovieView>> GetAllMoviesAsync(Sorting sorting, Paging paging, MovieFiltering filtering, CurrentUser currentUser);
        Task<MovieView> GetMovieByIdAsync(Guid id);
        Task<bool> AddMovieAsync(Movie newMovie);
        Task<bool> UpdateMovieAsync(Guid id, Movie updateMovie);
        Task<bool> DeleteMovieAsync(Guid id);
    }
}
