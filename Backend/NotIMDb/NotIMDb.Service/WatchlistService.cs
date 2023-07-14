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
    public class WatchlistService : IWatchlistService
    {
        private IWatchlistRepository Repository { get; set; }
        public WatchlistService(IWatchlistRepository repository)
        {
            Repository = repository;
        }

        public async Task<int> AddToWatchListAsync(Guid movieId, CurrentUser currentUser)
        {
            return await Repository.AddToWatchListAsync(movieId, currentUser);
        }

        public async Task<int> MarkAsWatchedAsync(Guid id, CurrentUser currentUser)
        {
            return await Repository.MarkAsWatchedAsync(id, currentUser);
        }

        public async Task<int> DeleteFromWatchListAsync(Guid id, CurrentUser currentUser)
        {
            return await Repository.DeleteFromWatchListAsync(id, currentUser);
        }
    }
}
