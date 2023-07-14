using Microsoft.Owin.Security.OAuth;
using NotIMDb.Model;
using NotIMDb.Repository;
using NotIMDb.Repository.Common;
using NotIMDb.Service;
using NotIMDb.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace NotIMDb.Api.AuthRepo
{
    public class AuthServerProvider : OAuthAuthorizationServerProvider
    {
        private readonly IUserRepository _UserRepository;

        public AuthServerProvider(IUserRepository UserRepository)
        {
            _UserRepository = UserRepository;
        }

        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            User request = new User() { Email = context.UserName, Password = context.Password };


            User response = await _UserRepository.ValidateUserAsync(request);

            if (response == null)
            {
                context.SetError("invalid_grant", "Provided username and password is incorrect");
                return;
            }
            Role userRole = await _UserRepository.GetUserRoleAsync(response.RoleId);
            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            identity.AddClaim(new Claim(ClaimTypes.Role, userRole.Title));
            identity.AddClaim(new Claim("Email", response.Email));
            context.Validated(identity);
        }
    }
}