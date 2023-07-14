using NotIMDb.Common;
using NotIMDb.Model;
using NotIMDb.Repository.Common;
using NotIMDb.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotIMDb.Service
{
    public class GenreService : IGenreService
    {
        private IGenreRepository Repository { get; }
        public GenreService(IGenreRepository repository)
        {
            Repository = repository;
        }
        public async Task<bool> DeleteAsync(Guid id)
        {
            return await Repository.DeleteAsync(id);
        }

        public async Task<PagedList<Genre>> GetAsync(Paging paging, Sorting sorting, GenreFiltering filtering)
        {
            return await Repository.GetAsync(paging, sorting, filtering);
        }

        public async Task<bool> PostAsync(Genre genre)
        {
            Guid guid = Guid.NewGuid();
            DateTime time = DateTime.Now;
            bool isActive = true;
            return await Repository.PostAsync(guid, time, isActive, genre);
        }

        public async Task<bool> PutAsync(Guid id, Genre genre)
        {
            DateTime time = DateTime.Now;
            Guid adminGuid = Guid.Parse("e2a804b7-964e-4f8a-a125-2eb492cc3108");
            return await Repository.PutAsync(id, genre, time, adminGuid);
        }
    }
}
