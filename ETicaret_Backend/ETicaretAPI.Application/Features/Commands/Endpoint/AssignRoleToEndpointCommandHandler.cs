using ETicaretAPI.Application.Abstractions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Commands.Endpoint
{
    public class AssignRoleToEndpointCommandHandler : IRequestHandler<AssignRoleToEndpointCommandRequest, AssignRoleToEndpointCommandResponse>
    {
        readonly IAuthorizationEndPointService _authorizationEndPointService;

        public AssignRoleToEndpointCommandHandler(IAuthorizationEndPointService authorizationEndPointService)
        {
            _authorizationEndPointService = authorizationEndPointService;
        }

        public async Task<AssignRoleToEndpointCommandResponse> Handle(AssignRoleToEndpointCommandRequest request, CancellationToken cancellationToken)
        {
            await _authorizationEndPointService.AssignRoleToEndPoints(request.roles, request.menu, request.code, request.type);
            return new();
        }
    }
}
