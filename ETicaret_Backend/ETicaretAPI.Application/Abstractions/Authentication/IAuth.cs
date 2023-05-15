using ETicaretAPI.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Abstractions.Authentication
{
    public interface IAuth : IExternalLogin , IInternalLogin
    {
        Task<Token> RefreshTokenLoginAsync(string refreshToken);
    }
}
