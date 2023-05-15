using ETicareAPI.Domain.Entities.IdentityEntities;
using ETicaretAPI.Application.Abstractions.Authentication;
using ETicaretAPI.Application.Abstractions.Tokens;
using ETicaretAPI.Application.DTOs;
using Google.Apis.Auth;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Google.Apis.Auth.GoogleJsonWebSignature;

namespace ETicaretAPI.Application.Features.Commands.Users.GoogleLoginUser
{
    public class GoogleLoginUserCommandHandler : IRequestHandler<GoogleLoginUserCommandRequest, GoogleLoginUserCommandResponse>
    {

       
        readonly IAuth _auth;

        public GoogleLoginUserCommandHandler(IAuth auth)
        {
            _auth = auth;
        }

        public async Task<GoogleLoginUserCommandResponse> Handle(GoogleLoginUserCommandRequest request, CancellationToken cancellationToken)
        {
            Token token = await  _auth.GoogleLoginFunc(request.IdToken);
            return new()
            {
                Token = token
            };
        }
    }
}
