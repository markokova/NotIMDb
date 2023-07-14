using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotIMDb.Model
{
    public class User
    {
		public Guid Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public DateTime? DateOfBirth { get; set; }
		public string Password { get; set; }
		public bool? IsActive { get; set; }
		public Guid? UpdatedByUserId { get; set; }
		public DateTime? DateCreated { get; set; }
		public DateTime? DateUpdated { get; set; }
		public Guid? RoleId { get; set; }
    }
}
