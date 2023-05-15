using ETicareAPI.Domain.Entities.IdentityEntities;
using ETicaretAPI.Application.Abstractions;
using ETicaretAPI.Application.Repositories;
using ETicaretAPI.Application.Repositories.EndPoint;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Presistance.Services
{
    public class AuthorizationEndPointService : IAuthorizationEndPointService
    {
        readonly IMenuReadRepository _menuReadRepository;
        readonly IMenuWriteRepository _menuWriteRepository;
        readonly IEndPointReadRepository _endpointReadRepository;
        readonly IEndPointWriteRepository _endpointWriteRepository;
        readonly IApplicationService _applicationService;
        readonly RoleManager<AppRole> _roleManager;

        public AuthorizationEndPointService(IMenuReadRepository menuReadRepository,
            IMenuWriteRepository menuWriteRepository,
            IEndPointReadRepository endpointReadRepository,
            IEndPointWriteRepository endpointWriteRepository,
            IApplicationService applicationService,
            RoleManager<AppRole> roleManager)
        {
            _menuReadRepository = menuReadRepository;
            _menuWriteRepository = menuWriteRepository;
            _endpointReadRepository = endpointReadRepository;
            _endpointWriteRepository = endpointWriteRepository;
            _applicationService = applicationService;
            _roleManager = roleManager;
        }

        public async Task AssignRoleToEndPoints(string[] roles, string menu, string code, Type type)
        {
          var _menu = await _menuReadRepository.GetSingleAsync(x => x.Name == menu);
            if(_menu == null)
            {
                _menu = new()
                {
                    Name = menu,
                    Id = Guid.NewGuid()
                };

                await _menuWriteRepository.AddAsync(_menu);
               await  _menuWriteRepository.SaveAsync();
            }

          var endpoint =  await _endpointReadRepository.Table.Include(x => x.Menu).Include(x => x.Roles).FirstOrDefaultAsync(x => x.Code == code && x.Menu.Name == menu);

            if(endpoint == null)
            {
              var action = _applicationService.getAuthorizeDefinitionEndPoints(type).FirstOrDefault(m => m.Name == menu)?.Actions.FirstOrDefault(a => a.Code == code);
                endpoint = new()
                {
                    Id = Guid.NewGuid(),
                    ActionType = action?.ActionType,
                    Code = action?.Code,
                    Definition = action?.Definition,
                    HttpType = action?.HttpType,
                    Menu = _menu
                };
            await _endpointWriteRepository.AddAsync(endpoint);
            await _endpointWriteRepository.SaveAsync();
            }
            endpoint.Roles.Clear();
            var appRoles = await _roleManager.Roles.Where(r => roles.Contains(r.Name)).ToListAsync();

           
            foreach(var role in appRoles)
                endpoint.Roles.Add(role);

            await _endpointWriteRepository.SaveAsync();
        }

        public async Task<List<string>> GetRoleToEndPoints(string code, string menu)
        {
            var endpoint = await _endpointReadRepository.Table.Include(x => x.Roles).Include(x => x.Menu).FirstOrDefaultAsync(x => x.Code == code && x.Menu.Name == menu);
            if( endpoint != null)
                    return endpoint.Roles.Select(r => r.Name).ToList();
            return null;
        }
    }
}
