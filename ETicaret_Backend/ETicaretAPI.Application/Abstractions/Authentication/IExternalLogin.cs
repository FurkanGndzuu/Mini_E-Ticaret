using ETicaretAPI.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Abstractions.Authentication
{
    public interface IExternalLogin
    {
        Task<Token> FacebookLoginFunc(string authToken);
        Task<Token> GoogleLoginFunc(string IdToken);
    }
}
