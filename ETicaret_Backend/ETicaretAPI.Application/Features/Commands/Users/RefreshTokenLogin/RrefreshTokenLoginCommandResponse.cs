using ETicaretAPI.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Commands.Users.RefreshTokenLogin
{
    public class RrefreshTokenLoginCommandResponse
    {
        public Token token { get; set; }
    }
}
