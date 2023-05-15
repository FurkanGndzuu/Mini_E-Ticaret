using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Commands.Endpoint
{
    public class AssignRoleToEndpointCommandRequest : IRequest<AssignRoleToEndpointCommandResponse>
    {
        public string menu { get; set; }
        public string code { get; set; }
        public string[] roles { get; set; }
        public Type? type { get; set; }
    }
}
