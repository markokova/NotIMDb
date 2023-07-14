using NotIMDb.Common;
using NotIMDb.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotIMDb.Repository.Common
{
    public interface IActorRepository
    {
        Task<PagedList<Actor>> GetAsync(Paging paging, Sorting sorting, ActorFiltering filtering);
        Task<Actor> GetAsync(Guid id);
        Task<bool> PostAsync(Guid guid, DateTime time,bool isActive, Actor actor);
        Task<bool> PutAsync(Guid id, Actor actor, DateTime time);
        Task<bool> DeleteAsync(Guid id);
    }
}
