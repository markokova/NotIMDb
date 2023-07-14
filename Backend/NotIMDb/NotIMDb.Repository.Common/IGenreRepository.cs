using NotIMDb.Common;
using NotIMDb.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotIMDb.Repository.Common
{
    public interface IGenreRepository
    {
        Task<PagedList<Genre>> GetAsync(Paging paging, Sorting sorting, GenreFiltering filtering);
        Task<Genre> GetAsync(Guid id);
        Task<bool> PostAsync(Guid guid, DateTime time, bool isActive, Genre genre);
        Task<bool> PutAsync(Guid id, Genre genre, DateTime time, Guid adminId);
        Task<bool> DeleteAsync(Guid id);
    }
}
