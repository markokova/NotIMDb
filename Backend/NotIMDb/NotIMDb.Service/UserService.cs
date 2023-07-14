using NotIMDb.Common;
using NotIMDb.Model;
using NotIMDb.Repository.Common;
using NotIMDb.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NotIMDb.Service
{
    public class UserService : IUserService 
    {
        private readonly IUserRepository _UserRepository;

        public UserService(IUserRepository UserRepository)
        {
            _UserRepository = UserRepository;
        }

        public async Task<string> RegisterAsync(User request)
        {
            request.Id = Guid.NewGuid();
            request.Password = PasswordHelper.HashPassword(request.Password);
            request.DateCreated = DateTime.UtcNow;
            string result = await _UserRepository.RegisterAsync(request);
            return result;
        }

        public async Task<User> ValidateUserAsync(User request) 
        {
            User result = await _UserRepository.ValidateUserAsync(request);
            return result;
        }

        public async Task<Role> GetUserRoleAsync(Guid? roleId)
        {
            Role result = await _UserRepository.GetUserRoleAsync(roleId);
            return result;
        }

        public async Task<List<Role>> GetRoles()
        {
            List<Role> result = await _UserRepository.GetRoles();
            return result;
        }

    }
}
