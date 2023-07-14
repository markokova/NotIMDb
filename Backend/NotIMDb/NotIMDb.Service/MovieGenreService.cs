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
    public class MovieGenreService : IMovieGenreService
    {
        private IMovieGenreRepository Repository { get; }
        public MovieGenreService(IMovieGenreRepository repository)
        {
            Repository = repository;
        }
        public Task<bool> DeleteAsync(Guid id)
        {
            return Repository.DeleteAsync(id);
        }

        public async Task<bool> PostAsync(MovieGenre movieGenre)
        {
            Guid guid = Guid.NewGuid();
            DateTime time = DateTime.Now;
            bool isActive = true;
            return await Repository.PostAsync(guid, time, isActive, movieGenre);
        }
    }
}
