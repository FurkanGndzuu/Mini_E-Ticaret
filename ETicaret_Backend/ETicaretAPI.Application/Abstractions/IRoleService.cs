using ETicaretAPI.Application.DTOs.Role;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Abstractions
{
    public interface IRoleService
    {
        Task CreateRole(string Name);
        Task<GetAllRoles> GetRoles(int Page , int Size);
    }
}
