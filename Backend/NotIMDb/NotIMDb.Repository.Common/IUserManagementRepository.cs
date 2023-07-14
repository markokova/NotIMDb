using NotIMDb.Common;
using NotIMDb.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotIMDb.Repository.Common
{
    public interface IUserManagementRepository
    {
        Task<PaginatedResponse<User>> GetAllUsers(Sorting sorting, Paging paging, UserFiltering userFiltering);
        Task<ResponseBaseModel<User>> GetById(Guid id);
        Task<ResponseBaseModel<bool>> Edit(Guid id, User request, CurrentUser currentUser);
    }
}
