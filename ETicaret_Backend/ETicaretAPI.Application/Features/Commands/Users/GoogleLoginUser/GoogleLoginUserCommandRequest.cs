﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Commands.Users.GoogleLoginUser
{
    public class GoogleLoginUserCommandRequest : IRequest<GoogleLoginUserCommandResponse>
    {

        public string Id { get; set; }
        public string IdToken { get; set; }
        public string Name { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhotoUrl { get; set; }
        public string Provider { get; set; }
    }
}

//provider: string;
//id: string;
//email: string;
//name: string;
//photoUrl: string;
//firstName: string;
//lastName: string;
//authToken: string;
//idToken: string;
//authorizationCode: string;
//response: any;