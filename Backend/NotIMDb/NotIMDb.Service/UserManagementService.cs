using NotIMDb.Common;
using NotIMDb.Model;
using NotIMDb.Repository.Common;
using NotIMDb.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotIMDb.Service
{
    public class UserManagementService : IUserManagementService
    {
        private readonly IUserManagementRepository _userManagementRepository;

        public UserManagementService(IUserManagementRepository UserManagementRepository)
        {
            _userManagementRepository = UserManagementRepository;
        }
        public async Task<PaginatedResponse<User>> GetAllUsers(Sorting sorting, Paging paging, UserFiltering userFiltering)
        {
            PaginatedResponse<User> usersList = await _userManagementRepository.GetAllUsers(sorting, paging, userFiltering);
            return usersList;
        }

        public async Task<ResponseBaseModel<User>> GetById(Guid id)
        {
            ResponseBaseModel<User> user = await _userManagementRepository.GetById(id);
            return user;
        }
        public async Task<ResponseBaseModel<bool>> Edit(Guid id, User request, CurrentUser currentUser)
        {
            request.DateUpdated = DateTime.UtcNow;
            request.UpdatedByUserId = currentUser.Id;

            ResponseBaseModel<bool> response = await _userManagementRepository.Edit(id, request, currentUser);
            return response;
        }
    }
}
