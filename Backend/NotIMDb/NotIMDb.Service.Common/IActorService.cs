using NotIMDb.Common;
using NotIMDb.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotIMDb.Service.Common
{
    public interface IActorService
    {
        Task<PagedList<Actor>> GetAsync(Paging paging, Sorting sorting, ActorFiltering filtering);
        Task<Actor> GetAsync(Guid id);
        Task<bool> PostAsync(Actor genre);
        Task<bool> PutAsync(Guid id, Actor actor);
        Task<bool> DeleteAsync(Guid id);
    }
}
