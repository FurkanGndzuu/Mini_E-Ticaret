using ETicareAPI.Domain.Enums;
using ETicaretAPI.Application.Abstractions;
using ETicaretAPI.Application.CustomAttributes;
using ETicaretAPI.Application.DTOs.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Action = ETicaretAPI.Application.DTOs.Configuration.Action;

namespace ETicaretAPI.Infrastructure.Services
{
    public class ApplicationService : IApplicationService
    {
        public List<Menu> getAuthorizeDefinitionEndPoints(Type type)
        {
            Assembly? assembly = Assembly.GetAssembly(type);

            var controllers = assembly?.GetTypes().Where(t => t.IsAssignableTo(typeof(ControllerBase)));

            List<Menu> menus = new();

            if(controllers != null)
            {
                foreach(var controller in controllers)
                {
                    var actions = controller.GetMethods().Where(x => x.IsDefined(typeof(AuthorizeDefinitionAttribute)));

                    if(actions != null)
                    {
                        foreach(var action in actions)
                        {

                            var attributes = action != null ? action.GetCustomAttributes(true) : null;

                            if(attributes != null)
                            {

                                Menu? menu = null;

                                var AuthorizeDefinitionAttributes = attributes.FirstOrDefault(x => x.GetType() == typeof(AuthorizeDefinitionAttribute)) as AuthorizeDefinitionAttribute;

                                if (!menus.Any(m => m.Name == AuthorizeDefinitionAttributes.Menu))
                                {
                                    menu = new() { Name = AuthorizeDefinitionAttributes.Menu };
                                    menus.Add(menu);
                                }
                                else
                                    menu = menus.FirstOrDefault(m => m.Name == AuthorizeDefinitionAttributes.Menu);

                                Action _action = new()
                                {
                                    ActionType = Enum.GetName(typeof(ActionType), AuthorizeDefinitionAttributes.ActionType),
                                    Definition = AuthorizeDefinitionAttributes.Definition
                                };

                                var httpAttribute = attributes.FirstOrDefault(x => x.GetType().IsAssignableTo(typeof(HttpMethodAttribute))) as HttpMethodAttribute;

                                if (httpAttribute != null)
                                {
                                    _action.HttpType = httpAttribute.HttpMethods.FirstOrDefault();
                                }
                                else
                                    _action.HttpType = HttpMethods.Get;


                                _action.Code = $"{_action.HttpType}.{_action.ActionType}.{_action.Definition.Replace(" ", "")}";

                                menu.Actions.Add(_action);
                            }
                           
                        }
                    }
                }
            }

            return menus;

        }
    }
}
