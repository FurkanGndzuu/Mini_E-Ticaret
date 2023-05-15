using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Commands.Users.CreateUser
{
    public class CreateUserCommandResponse
    {
        public bool succeded { get; set; }
        public string message { get; set; }
    }
}
