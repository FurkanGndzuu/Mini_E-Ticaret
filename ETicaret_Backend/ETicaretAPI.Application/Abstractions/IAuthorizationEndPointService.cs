using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Abstractions
{
    public interface IAuthorizationEndPointService
    {
        Task AssignRoleToEndPoints(string[] roles , string menu , string code , Type type);
        Task<List<string>> GetRoleToEndPoints(string code , string menu);
    }
}
