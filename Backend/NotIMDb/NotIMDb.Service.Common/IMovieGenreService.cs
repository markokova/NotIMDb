using NotIMDb.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotIMDb.Service.Common
{
    public interface IMovieGenreService
    {
        Task<bool> PostAsync(MovieGenre movieGenre);
        Task<bool> DeleteAsync(Guid id);
    }
}
