using ETicaretAPI.Application.Features.Commands.Users.LoginUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.DTOs
{
    public  class ResponseDtoForLoginProcess
    {
    }

    public class ForSuccessfullyResponse : ResponseDtoForLoginProcess
    {
        public Token token { get; set; }
    }
    public class ForFailedResponse : ResponseDtoForLoginProcess
    {
        public string message { get; set; }
    }
}
