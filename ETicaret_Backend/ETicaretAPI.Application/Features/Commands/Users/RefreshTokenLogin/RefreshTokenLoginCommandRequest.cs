﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Commands.Users.RefreshTokenLogin
{
    public class RefreshTokenLoginCommandRequest : IRequest<RrefreshTokenLoginCommandResponse>
    {
        public string refreshToken { get; set; }
    }
}
