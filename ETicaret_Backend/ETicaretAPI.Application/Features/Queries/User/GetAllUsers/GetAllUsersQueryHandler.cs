using ETicaretAPI.Application.Abstractions.User;
using ETicaretAPI.Application.DTOs.User;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Queries.User.GetAllUsers
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQueryRequest, GetAllUsersQueryResponse>
    {
        readonly IUserServices _userServices;

        public GetAllUsersQueryHandler(IUserServices userServices)
        {
            _userServices = userServices;
        }

        public async Task<GetAllUsersQueryResponse> Handle(GetAllUsersQueryRequest request, CancellationToken cancellationToken)
        {
          GetAllUser response = await _userServices.GetAllUsersAsync(request.page , request.size);
            return new()
            {
                TotalUserCount = response.TotalUserCount,
                Users = response.Users
            };
        }
    }
}
