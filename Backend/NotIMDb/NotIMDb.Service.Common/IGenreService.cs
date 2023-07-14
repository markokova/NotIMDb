using NotIMDb.Common;
using NotIMDb.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotIMDb.Service.Common
{
    public interface IGenreService
    {
        Task<PagedList<Genre>> GetAsync(Paging paging, Sorting sorting, GenreFiltering filtering);
        Task<bool> PostAsync(Genre genre);
        Task<bool> PutAsync(Guid id, Genre genre);
        Task<bool> DeleteAsync(Guid id);
    }
}
