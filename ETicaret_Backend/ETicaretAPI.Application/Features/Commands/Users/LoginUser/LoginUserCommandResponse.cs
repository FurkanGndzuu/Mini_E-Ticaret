using ETicaretAPI.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Commands.Users.LoginUser
{
    public class LoginUserCommandResponse
    {
    }

    public class SuccededLoginCommandResponse : LoginUserCommandResponse
    {
        public Token token { get; set; }
    }
    public class FailedLoginCommandResponse : LoginUserCommandResponse
    {
        public string message { get; set; }
    }
}
