using NotIMDb.Common;
using NotIMDb.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotIMDb.Repository.Common
{
    public interface IMovieRepository
    {
        Task<PagedList<MovieView>> GetMoviesAsync(Sorting sorting, Paging paging, MovieFiltering filtering);
        Task<MovieView> GetMovieByIdAsync(Guid id);

        Task<bool> AddMovieAsync(Movie newMovie);
        Task<bool> UpdateMovieAsync(Guid id, Movie updateMovie);
        Task<bool> DeleteMovieAsync(Guid id);
    }
}
