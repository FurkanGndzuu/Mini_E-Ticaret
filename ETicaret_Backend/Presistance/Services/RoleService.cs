using ETicareAPI.Domain.Entities.IdentityEntities;
using ETicaretAPI.Application.Abstractions;
using ETicaretAPI.Application.DTOs.Role;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Presistance.Services
{
    public class RoleService : IRoleService
    {
        readonly RoleManager<AppRole> _roleManager;

        public RoleService(RoleManager<AppRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task CreateRole(string Name)
        {
            await _roleManager.CreateAsync(new() { Id = Guid.NewGuid().ToString(), Name = Name });
        }

        public async Task<GetAllRoles> GetRoles(int Page , int Size)
        {
            var query = _roleManager.Roles;

        if(Page == -1 || Size == -1)
            {
                return new()
                {
                    TotalRoleCount = query.Count(),
                    Roles = query.Select(x => new { x.Id, x.Name }).ToList()
                };
            }
            return new()
            {
                TotalRoleCount = query.Count(),
                Roles = query.Skip(Page * Size).Take(Size).Select(x => new { x.Id, x.Name }).ToList()
            };
        }
    }
}
