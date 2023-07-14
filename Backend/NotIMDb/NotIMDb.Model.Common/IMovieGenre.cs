using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotIMDb.Model.Common
{
    public interface IMovieGenre
    {
        Guid Id { get; set; }
        Guid MovieId { get; set; }
        Guid GenreId { get; set; }
        bool IsActive { get; set; }
        Guid CreatedByUserId { get; set; }
        Guid UpdatedByUserId { get; set; }
        DateTime DateCreated { get; set; }
        DateTime DateUpdated { get; set; }
    }
}
