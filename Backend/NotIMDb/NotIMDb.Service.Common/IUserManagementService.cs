using NotIMDb.Common;
using NotIMDb.Model;
using System;
using System.Threading.Tasks;

namespace NotIMDb.Service.Common
{
    public interface IUserManagementService
    {
       Task<PaginatedResponse<User>> GetAllUsers(Sorting sorting, Paging paging, UserFiltering userFiltering);
       Task<ResponseBaseModel<User>> GetById(Guid id);
       Task<ResponseBaseModel<bool>> Edit(Guid id, User request, CurrentUser currentUser);
    }
}
