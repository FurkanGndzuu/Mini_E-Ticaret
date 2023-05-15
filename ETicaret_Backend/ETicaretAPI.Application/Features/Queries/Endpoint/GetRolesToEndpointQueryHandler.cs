using ETicaretAPI.Application.Abstractions;
using Google.Apis.Auth.OAuth2;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Queries.Endpoint
{
    public class GetRolesToEndpointQueryHandler : IRequestHandler<GetRolesToEndpointQueryRequest, GetRolesToEndpointQueryResponse>
    {
        readonly IAuthorizationEndPointService _endpointService;

        public GetRolesToEndpointQueryHandler(IAuthorizationEndPointService endpointService)
        {
            _endpointService = endpointService;
        }

        public async Task<GetRolesToEndpointQueryResponse> Handle(GetRolesToEndpointQueryRequest request, CancellationToken cancellationToken)
        {
          List<string>? list = await _endpointService.GetRoleToEndPoints(request.code, request.menu);
            return new()
            {
                list = list
            };
        }
    }
}
