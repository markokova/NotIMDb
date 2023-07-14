using NotIMDb.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotIMDb.Repository.Common
{
    public interface IMovieGenreRepository
    {
        Task<bool> PostAsync(Guid guid, DateTime time, bool isActive, MovieGenre movieGenre);
        Task<bool> DeleteAsync(Guid id);
    }
}
