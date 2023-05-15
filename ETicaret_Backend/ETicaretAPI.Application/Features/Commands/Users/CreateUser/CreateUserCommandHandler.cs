using ETicareAPI.Domain.Entities.IdentityEntities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Commands.Users.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommandRequest, CreateUserCommandResponse>
    {
        readonly UserManager<AppUser> _userManager;

        


        public CreateUserCommandHandler(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<CreateUserCommandResponse> Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
        {
          IdentityResult result =  await _userManager.CreateAsync(new AppUser()
            {
                UserName = request.userName,
                Id = Guid.NewGuid().ToString(),
                Email = request.email,
                nameSurname = request.nameSurname,

            },request.password);

            if (result.Succeeded)
            {
                return new()
                {
                    message = "User Registration Successfully Created",
                    succeded = true
                };
            }
            else
            {
                return new()
                {
                    message = "Failed to Create User Record",
                    succeded = false
                };
            }

           
        }
    }
}
