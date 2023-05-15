using ETicareAPI.Domain.Entities.IdentityEntities;
using ETicaretAPI.Application.Abstractions.Authentication;
using ETicaretAPI.Application.Abstractions.Tokens;
using ETicaretAPI.Application.DTOs;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace ETicaretAPI.Application.Features.Commands.Users.FacebookLogin
{
    public class FacebookLoginCommandHandler : IRequestHandler<FcaebookLoginCommandRequest, FacebookLoginCommandResponse>
    {
        
        readonly IAuth _auth;

        public FacebookLoginCommandHandler(IAuth auth)
        {
            _auth = auth;
        }

        public async Task<FacebookLoginCommandResponse> Handle(FcaebookLoginCommandRequest request, CancellationToken cancellationToken)
        {
            
          Token token = await  _auth.FacebookLoginFunc(request.authToken);
            return new()
            {
                Token = token
            };
        }
    }
}
