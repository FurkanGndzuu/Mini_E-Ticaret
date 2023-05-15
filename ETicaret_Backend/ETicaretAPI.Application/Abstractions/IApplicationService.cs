using ETicaretAPI.Application.DTOs.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Abstractions
{
    public interface IApplicationService
    {
        List<Menu> getAuthorizeDefinitionEndPoints(Type type);
    }
}
