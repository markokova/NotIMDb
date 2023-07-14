using NotIMDb.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotIMDb.Model
{
    public class ActorMovie : IActorMovie
    {
        public Guid Id { get; set; }
        public Guid ActorId { get; set; }
        public Guid MovieId { get; set; }
        public bool IsActive { get; set; } = true;
        public Guid CreatedByUserId { get; set; }
        public Guid UpdatedByUserId { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }

        //public Actor Actor { get; set; }
        //public Movie Movie { get; set; }
    }
}
