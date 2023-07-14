using NotIMDb.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotIMDb.Service.Common
{
    public interface IUserService
    {
        Task<string> RegisterAsync(User user);

        Task<User> ValidateUserAsync(User user);

        Task<Role> GetUserRoleAsync(Guid? roleId);
        Task<List<Role>> GetRoles();
    }
}
