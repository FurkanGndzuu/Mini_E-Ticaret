using ETicareAPI.Domain.Entities.IdentityEntities;
using ETicaretAPI.Application.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Abstractions.User
{
    public interface IUserServices
    {
        Task UpdateRefreshTokenAsync(string refreshToken, AppUser user, int RefreshTokenTime);

        Task<GetAllUser> GetAllUsersAsync(int page , int size);

        Task AssignRolesToUser(string UserId, string[] roles);
        Task<List<string>> GetRolesToUser(string UserId);

        Task<bool> HasRolePermissionToEndpoint(string name, string code);
    }
}
