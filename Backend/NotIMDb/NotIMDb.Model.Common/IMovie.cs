using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotIMDb.Model.Common
{
    public interface IMovie
    {
        Guid Id { get; set; }
        string Title { get; set; }
        int Runtime { get; set; }
        bool IsActive { get; set; }
        string Image { get; set; }
        DateTime YearOfRelease { get; set; }
        Guid CreatedByUserId { get; set; }
        Guid UpdatedByUserId { get; set; }
        DateTime DateCreated { get; set; }
        DateTime DateUpdated { get; set; }
    }
}
