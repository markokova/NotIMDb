using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotIMDb.Model.Common
{
    public interface IActor
    {
        Guid Id { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        string Bio { get; set; }
        string Image { get; set; }
        bool IsActive { get; set; }
        Guid CreatedByUserId { get; set; }
        Guid UpdatedByUserId { get; set; }
        DateTime DateCreated { get; set; }
        DateTime DateUpdated { get; set; }
    }
}
