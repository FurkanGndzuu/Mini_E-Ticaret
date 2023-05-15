using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.DTOs.Role
{
    public class GetAllRoles
    {
        public int TotalRoleCount { get; set; }
        public object Roles { get; set; }
    }
}
