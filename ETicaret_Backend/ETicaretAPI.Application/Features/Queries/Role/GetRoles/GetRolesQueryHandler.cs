using ETicaretAPI.Application.Abstractions;
using ETicaretAPI.Application.DTOs.Role;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Queries.Role.GetRoles
{
    public class GetRolesQueryHandler : IRequestHandler<GetRolesQueryRequest, GetRolesQueryResponse>
    {
        readonly IRoleService _roleService;

        public GetRolesQueryHandler(IRoleService roleService)
        {
            _roleService = roleService;
        }

        public async Task<GetRolesQueryResponse> Handle(GetRolesQueryRequest request, CancellationToken cancellationToken)
        {
            GetAllRoles response = await _roleService.GetRoles(request.Page, request.Size);

            return new()
            {
                Roles = response.Roles,
                TotalRoleCount = response.TotalRoleCount
            };
        }
    }
}
