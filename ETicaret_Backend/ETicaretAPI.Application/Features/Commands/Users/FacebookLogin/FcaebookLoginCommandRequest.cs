using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Commands.Users.FacebookLogin
{
    public class FcaebookLoginCommandRequest : IRequest<FacebookLoginCommandResponse>
    {
        public string authToken { get; set; }
    }
}
