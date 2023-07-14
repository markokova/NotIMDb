using Microsoft.Win32;
using NotIMDb.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotIMDb.Model
{
    public class Movie : IMovie
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public int Runtime { get; set; }
        public string Image { get; set; }
        public List<Guid> ActorIds { get; set; }
        public List<Guid> GenreIds { get; set; }
        public bool IsActive { get; set; } = true;
        public decimal AverageScore { get; set; }
        public DateTime YearOfRelease { get; set; }
        public Guid CreatedByUserId { get; set; }
        public Guid UpdatedByUserId { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
    }
}
