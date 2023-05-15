using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Queries.User.GetAllUsers
{
    public class GetAllUsersQueryResponse
    {
        public int TotalUserCount { get; set; }
        public object? Users { get; set; }
    }
}
