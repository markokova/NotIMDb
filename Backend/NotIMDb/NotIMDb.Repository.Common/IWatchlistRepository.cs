using NotIMDb.Common;
using NotIMDb.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotIMDb.Repository.Common
{
    public interface IWatchlistRepository
    {
        Task<int> AddToWatchListAsync(Guid movieId, CurrentUser currentUser);

        Task<int> MarkAsWatchedAsync(Guid id, CurrentUser current);

        Task<int> DeleteFromWatchListAsync(Guid id, CurrentUser current);
    }
}
