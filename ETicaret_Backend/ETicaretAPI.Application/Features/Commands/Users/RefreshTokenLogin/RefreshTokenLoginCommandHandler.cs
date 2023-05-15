using ETicaretAPI.Application.Abstractions.Authentication;
using ETicaretAPI.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Commands.Users.RefreshTokenLogin
{
    public class RefreshTokenLoginCommandHandler : IRequestHandler<RefreshTokenLoginCommandRequest, RrefreshTokenLoginCommandResponse>
    {
        readonly IAuth _auth;

        public RefreshTokenLoginCommandHandler(IAuth auth)
        {
            _auth = auth;
        }

        public async Task<RrefreshTokenLoginCommandResponse> Handle(RefreshTokenLoginCommandRequest request, CancellationToken cancellationToken)
        {
          Token token =  await _auth.RefreshTokenLoginAsync(request.refreshToken);
            return new()
            {
                token = token,
            };
        }
    }
}
