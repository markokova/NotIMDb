using NotIMDb.Common;
using NotIMDb.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotIMDb.Service.Common
{
    public interface IWatchlistService
    {

        Task<int> AddToWatchListAsync(Guid movieId, CurrentUser currentUser);

        Task<int> MarkAsWatchedAsync(Guid id, CurrentUser currentUser);

        Task<int> DeleteFromWatchListAsync(Guid id, CurrentUser currentUser);
    }
}
