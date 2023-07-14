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
    public class ActorService : IActorService
    {
        private IActorRepository Repository { get; }
        public ActorService(IActorRepository repository)
        {
            Repository = repository;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            return await Repository.DeleteAsync(id);
        }

        public async Task<PagedList<Actor>> GetAsync(Paging paging, Sorting sorting, ActorFiltering filtering)
        {
            return await Repository.GetAsync(paging, sorting, filtering);
        }

        public async Task<Actor> GetAsync(Guid id)
        {
            return await Repository.GetAsync(id);
        }

        public async Task<bool> PostAsync(Actor actor)
        {
            Guid guid = Guid.NewGuid();
            DateTime time = DateTime.Now;
            bool isActive = true;
            return await Repository.PostAsync(guid, time, isActive, actor);
        }

        public async Task<bool> PutAsync(Guid id, Actor actor)
        {
            DateTime time = DateTime.Now;
            return await Repository.PutAsync(id, actor, time);
        }
    }
}
