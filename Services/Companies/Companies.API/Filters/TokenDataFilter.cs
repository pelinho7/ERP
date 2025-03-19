using Companies.Appilcation.Features.Companies.Commands;
using Companies.Appilcation.Features.Companies.Commands.Interfaces;
using Microsoft.AspNetCore.Mvc.Filters;
//using Companies.Appilcation.Features.Companies.Commands;
//using Companies.Appilcation.Features.Companies.Commands.Interfaces;
//using Companies.Application.Features.Companies.Commands.CreateProduct;
using System.Text;
using System.Text.Json;

namespace Companies.API.Filters
{
    public class TokenDataFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (context.HttpContext.Request.Method == "POST"
                || context.HttpContext.Request.Method == "PUT"
                || context.HttpContext.Request.Method == "PATCH")
            {
                var claims = context.HttpContext.User.Claims;
                var userId = claims.FirstOrDefault(x => x.Type == System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
                
                if (context.HttpContext.Request.Method == "POST")
                {
                    var arguments = ((ICompanyCommand)context.ActionArguments.First().Value);
                    arguments.CreatedBy = int.Parse(userId);
                    arguments.CreatedDate = DateTime.UtcNow;
                }
                else if (context.HttpContext.Request.Method == "PUT")
                {
                    var arguments = ((IUpdateCompanyCommand)context.ActionArguments.First().Value);
                    arguments.LastModifiedBy = int.Parse(userId);
                    arguments.LastModifiedDate = DateTime.UtcNow;
                }
                else if (context.HttpContext.Request.Method == "PATCH")
                {
                    if (context.ActionArguments.First().Value is IEnumerable<IArchiveCompanyCommand>)
                    {
                        foreach (var archive in ((IEnumerable<IArchiveCompanyCommand>)context.ActionArguments.First().Value))
                        {
                            archive.LastModifiedBy = int.Parse(userId);
                            archive.LastModifiedDate = DateTime.UtcNow;
                        }
                    }
                }
            }

            await next();
        }
    }
}
