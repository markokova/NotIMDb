using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotIMDb.Model.Common
{
    public interface IGenre
    {
        Guid Id { get; set; }
        string Title { get; set; }
        bool IsActive { get; set; }
        Guid CreatedByUserId { get; set; }
        Guid UpdatedByUserId { get; set; }
        DateTime DateCreated { get; set; }
        DateTime DateUpdated { get; set; }

    }
}
