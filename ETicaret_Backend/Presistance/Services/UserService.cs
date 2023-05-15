using ETicareAPI.Domain.Entities.IdentityEntities;
using ETicaretAPI.Application.Abstractions.User;
using ETicaretAPI.Application.DTOs.User;
using ETicaretAPI.Application.Repositories.EndPoint;
using ETicaretAPI.Presistance.Repositories.EndPoint;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Presistance.Services
{
    public class UserService : IUserServices
    {
        readonly UserManager<AppUser> _userManager;
        readonly IEndPointReadRepository _endPointReadRepository;

        public UserService(UserManager<AppUser> userManager, IEndPointReadRepository endPointReadRepository)
        {
            _userManager = userManager;
            _endPointReadRepository = endPointReadRepository;
        }

        public async Task AssignRolesToUser(string UserId, string[] roles)
        {
            
            var user = await _userManager.FindByIdAsync(UserId);
            if(user != null)
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                await _userManager.RemoveFromRolesAsync(user, userRoles);

                await _userManager.AddToRolesAsync(user, roles);

            }
        }

        public async Task<GetAllUser> GetAllUsersAsync(int page , int size)
        {
          var users =   _userManager.Users;

            return new()
            {
                TotalUserCount = users.Count(),
                Users = users.Skip(page * size).Take(size).Select(x => new
                {
                    x.Id,
                    x.UserName,
                    x.Email,
                    x.TwoFactorEnabled
                }).ToList()
            };
        }

        public async Task<List<string>> GetRolesToUser(string UserIdOrName)
        {
           var user = await _userManager.FindByIdAsync(UserIdOrName);
            if (user is null)
                user = await _userManager.FindByNameAsync(UserIdOrName);

            if( user != null )
            {
               var userRoles = await _userManager.GetRolesAsync(user);

                List<string> roles = new();

                foreach (var role in userRoles)
                    roles.Add(role);
                return roles;
            }
            return null;
           
        }

        public async Task<bool> HasRolePermissionToEndpoint(string name, string code)
        {
            var roles = await GetRolesToUser(name);
            if (!roles.Any())
                return false;

           var endpoint =await _endPointReadRepository.Table.Include(x => x.Roles).FirstOrDefaultAsync(x => x.Code == code);

            if (endpoint == null)
                return false;

            var hasRole = false;
            var endpointRoles = endpoint.Roles.Select(r => r.Name);

            foreach(var userRole in roles)
                foreach (var endpointRole in endpointRoles)
                    if(userRole == endpointRole)
                        hasRole = true;
            return hasRole;
        }

        public async Task UpdateRefreshTokenAsync(string refreshToken, AppUser user, int RefreshTokenTime)
        {
            if (user != null)
            {
                user.refreshToken = refreshToken;
                user.refreshTokenEndDate = DateTime.UtcNow.AddSeconds(45);
                await _userManager.UpdateAsync(user);

            }
            else throw new Exception("USER NOT FOUND");
        }
    }
}
