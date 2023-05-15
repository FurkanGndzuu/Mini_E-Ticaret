using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Commands.Users.CreateUser
{
    public class CreateUserCommandRequest : IRequest<CreateUserCommandResponse>
    {
        public string nameSurname { get; set; }
        public string userName  { get; set; }
        public string email { get; set; }
        public string password { get; set; }
    }
}
