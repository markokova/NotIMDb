using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NotIMDb.Model;

namespace NotIMDb.Model.Common
{
    public interface IWatchList
    {
         Guid Id { get; set; }

         bool IsWatched { get; set; }

         Guid MovieId { get; set; }

         bool IsActive { get; set; }

         Guid CreatedByUserId { get; set; }

         Guid UpdatedByUserId { get; set; }

         DateTime DateCreated { get; set; }

         DateTime DateUpdated { get; set; }

    }
}
