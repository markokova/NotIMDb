using Microsoft.Ajax.Utilities;
using NotIMDb.Api.Models.UserRest;
using NotIMDb.Common;
using NotIMDb.Model;
using NotIMDb.Service.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Routing.Constraints;
using System.Web.Management;
//using System.Web.Mvc;
using System.Web.Security;

namespace NotIMDb.Api.Controllers
{
    [RoutePrefix("api/usermanagement")]

    public class UserManagementController : ApiController
    {

        private readonly IUserService _userService;
        private readonly IUserManagementService _userManagementService;

        public UserManagementController(IUserManagementService UserManagementService, IUserService userService)
        {
            _userManagementService = UserManagementService;
            _userService = userService;
        }

        [Route("getall")]
        [HttpGet]
        public async Task<HttpResponseMessage> GetAllUsers(string email, string firstName, string lastName, string orderBy = "LastName", string sortOrder = "ASC", int pageSize = 10, int currentPage = 1)
        {
            Sorting sorting = new Sorting() { Orderby = orderBy, SortOrder = sortOrder };
            UserFiltering userFiltering = new UserFiltering() { Email = email, FirstName = firstName, LastName = lastName };
            Paging paging = new Paging() { PageSize = pageSize, CurrentPage = currentPage };

            PaginatedResponse<User> response = await _userManagementService.GetAllUsers(sorting, paging, userFiltering);

            if (response.Errors != null && response.Errors.Contains("USER_NOT_FOUND"))
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "User not found");
            }
            if (response.Success)
            {
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest, response);
        }

        [Route("getbyid")]
        [HttpGet]
        public async Task<HttpResponseMessage> GetById(Guid id)
        {
            ResponseBaseModel<User> response = await _userManagementService.GetById(id);

            if (response.Errors != null && response.Errors.Contains("USER_NOT_FOUND"))
            {
                Request.CreateResponse(HttpStatusCode.NotFound, "User not found");
            }
            Role userRole = await _userService.GetUserRoleAsync(response.Result.RoleId);
            ResponseBaseModel<User> updatedBy = new ResponseBaseModel<User>();

            if (response.Result.UpdatedByUserId != null)
            {
                updatedBy = await _userManagementService.GetById(response.Result.Id);
            }
            ResponseBaseModel<UserRestGet> result = new ResponseBaseModel<UserRestGet>()
            {
                Errors = response.Errors,
                Result = new UserRestGet()
                {
                    Id = response.Result.Id,
                    FirstName = response.Result.FirstName,
                    LastName = response.Result.LastName,
                    Email = response.Result.Email,
                    DateOfBirth = response.Result.DateOfBirth,
                    IsActive = response.Result.IsActive,
                    UpdatedByUser = updatedBy.Result != null ? updatedBy.Result.Email : "",
                    DateCreated = response.Result.DateCreated,
                    DateUpdated = response.Result.DateUpdated,
                    Role = userRole.Title

                }
            };
            return Request.CreateResponse(HttpStatusCode.OK, result);
            //return response.Errors != null && response.Errors.Contains("USER_NOT_FOUND") ? Request.CreateResponse(HttpStatusCode.NotFound, "User not found") : Request.CreateResponse(HttpStatusCode.OK, response);
        }

        [Route("edit")]
        [Authorize(Roles = "User, Administrator")]
        [HttpPut]
        public async Task<HttpResponseMessage> Edit(Guid id, [FromBody] User request)
        {
            ResponseBaseModel<bool> response = new ResponseBaseModel<bool>();
            CurrentUser currentUser;

            ClaimsIdentity identity = System.Web.HttpContext.Current.User.Identity as ClaimsIdentity;

            if (Guid.TryParse(identity.FindFirst(ClaimTypes.NameIdentifier)?.Value, out var newGuid))
            {
                currentUser = new CurrentUser() { Id = Guid.Parse(identity.FindFirst(ClaimTypes.NameIdentifier)?.Value), Role = identity.FindFirst(ClaimTypes.Role)?.Value };
                response = await _userManagementService.Edit(id, request, currentUser);
            }

            if (response.Errors != null && response.Errors.Contains("USER_NOT_FOUND"))
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "User not found");
            }
            return !response.Success ? Request.CreateResponse(HttpStatusCode.BadRequest, response) : Request.CreateResponse(HttpStatusCode.NoContent);
        }
    }
}
