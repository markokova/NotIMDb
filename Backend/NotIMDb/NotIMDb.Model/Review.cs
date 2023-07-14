using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NotIMDb.Model
{
    public class Review
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public int Score { get; set; }

        public Guid MovieId { get; set; }

        public bool IsActive { get; set; }

        public Guid CreatedByUserId { get; set; }

        public Guid UpdatedByUserId { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateUpdated { get; set; }

        public Movie Movie { get; set; }

        public User User { get; set; }
    }
}
