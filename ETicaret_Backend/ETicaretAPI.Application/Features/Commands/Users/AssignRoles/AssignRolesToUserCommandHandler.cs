using ETicaretAPI.Application.Abstractions.User;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Commands.Users.AssignRoles
{
    public class AssignRolesToUserCommandHandler : IRequestHandler<AssignRolesToUserCommandRequest, AssignRolesToUserCommandResponse>
    {
        readonly IUserServices _userServices;

        public AssignRolesToUserCommandHandler(IUserServices userServices)
        {
            _userServices = userServices;
        }

        public async Task<AssignRolesToUserCommandResponse> Handle(AssignRolesToUserCommandRequest request, CancellationToken cancellationToken)
        {
           await _userServices.AssignRolesToUser(request.Id , request.Roles);
           await _userServices.GetRolesToUser(request.Id);
            return new();
        }
    }
}
