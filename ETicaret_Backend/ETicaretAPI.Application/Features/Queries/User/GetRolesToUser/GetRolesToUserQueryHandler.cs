using ETicaretAPI.Application.Abstractions.User;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Queries.User.GetRolesToUser
{
    public class GetRolesToUserQueryHandler : IRequestHandler<GetRolesToUserQueryRequest, GetRolesToUserQueryResponse>
    {
        readonly IUserServices _userService;

        public GetRolesToUserQueryHandler(IUserServices userService)
        {
            _userService = userService;
        }

        public async Task<GetRolesToUserQueryResponse> Handle(GetRolesToUserQueryRequest request, CancellationToken cancellationToken)
        {
          List<string> list = await _userService.GetRolesToUser(request.UserId);
            return new()
            {
                list = list,
            };
        }
    }
}
