using ETicaretAPI.Application.Abstractions.User;
using ETicaretAPI.Application.CustomAttributes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Routing;
using System.Reflection;

namespace ETicaretAPI.API.Filters
{
    public class RolePermisionFilter : IAsyncActionFilter
    {
        readonly IUserServices _userService;

        public RolePermisionFilter(IUserServices userService)
        {
            _userService = userService;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var name = context.HttpContext.User.Identity.Name;

            if (name is not null && name != "Furkan")
            {
                var descriptor = context.ActionDescriptor as ControllerActionDescriptor;
                var attribute = descriptor?.MethodInfo.GetCustomAttribute(typeof(AuthorizeDefinitionAttribute)) as AuthorizeDefinitionAttribute;

                var httpAttribute = descriptor?.MethodInfo.GetCustomAttribute(typeof(HttpMethodAttribute)) as HttpMethodAttribute;

                var code = $"{(httpAttribute != null ? httpAttribute.HttpMethods.First() : HttpMethods.Get)}.{attribute.ActionType}.{attribute.Definition.Replace(" ", "")}";

                var hasRole = await _userService.HasRolePermissionToEndpoint(name, code);

                if (!hasRole)
                    context.Result = new UnauthorizedResult();
                else
                    await next();
            }
        }
    }
}
