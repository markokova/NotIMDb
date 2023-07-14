using NotIMDb.Api.Models.ActorRest;
using NotIMDb.Api.Models.ReviewRest;
using NotIMDb.Common;
using NotIMDb.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NotIMDb.Api.Mappers
{
    public class RestDomainActorMapper
    {
        public ActorsRestGet MapToRest(PagedList<Actor> actors)
        {
            ActorsRestGet actorsRest = new ActorsRestGet();
            actorsRest.actorRests = new List<ActorRest>();

            if (actors != null)
            {
                foreach (Actor actor in actors)
                {
                    ActorRest actorRest = new ActorRest();
                    actorRest.FirstName = actor.FirstName;
                    actorRest.LastName = actor.LastName;
                    actorRest.Bio = actor.Bio;
                    actorRest.Image= actor.Image;
                    actorsRest.actorRests.Add(actorRest);
                }
                actorsRest.CurrentPage = actors.CurrentPage;
                actorsRest.PageSize = actors.PageSize;
                actorsRest.TotalPages = actors.TotalCount;
                actorsRest.TotalCount = actors.TotalCount;
            }

            return actorsRest;
        }

        public ActorRest MapToRest(Actor actor)
        {
            ActorRest rest = new ActorRest();
            rest.FirstName= actor.FirstName;
            rest.LastName= actor.LastName;
            rest.Bio= actor.Bio;
            rest.Image= actor.Image;
            return rest;
        }

        public Actor MapFromRest(ActorRest rest)
        {
            Actor actor = new Actor();
            actor.FirstName = rest.FirstName;
            actor.LastName = rest.LastName;
            actor.Bio= rest.Bio;
            actor.Image= rest.Image;
            return actor;
        }
    }
}