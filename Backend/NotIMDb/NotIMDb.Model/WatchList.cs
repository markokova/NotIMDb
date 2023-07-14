using NotIMDb.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotIMDb.Model
{
    public class WatchList : IWatchList
    {
        public Guid Id { get; set; }

        public bool IsWatched { get; set; }

        public Guid MovieId { get; set; }

        public bool IsActive { get; set; }

        public Guid CreatedByUserId { get; set; }

        public Guid UpdatedByUserId { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateUpdated { get; set; }

        public Movie Movie { get; set; }
    }
}
