using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Commands.Users.AssignRoles
{
    public class AssignRolesToUserCommandRequest : IRequest<AssignRolesToUserCommandResponse>
    {
        public string Id { get; set; }
        public string[] Roles { get; set; }
    }
}
