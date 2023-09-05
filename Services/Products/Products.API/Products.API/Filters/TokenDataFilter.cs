using Microsoft.AspNetCore.Mvc.Filters;
using Products.Appilcation.Features.Products.Commands;
using Products.Appilcation.Features.Products.Commands.Interfaces;
using Products.Application.Features.Products.Commands.CreateProduct;
using System.Text;
using System.Text.Json;

namespace Products.API.Filters
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
                var userId = claims.FirstOrDefault(x => x.Type == "id")?.Value;
                var companyId = claims.FirstOrDefault(x => x.Type == "companyId")?.Value;

                if (context.HttpContext.Request.Method == "POST")
                {
                    var arguments = ((IProductCommand)context.ActionArguments.First().Value);
                    arguments.CompanyId = int.Parse(companyId);
                    arguments.CreatedBy = int.Parse(userId);
                    arguments.CreatedDate = DateTime.UtcNow;
                }
                else if (context.HttpContext.Request.Method == "PUT")
                {
                    var arguments = ((IUpdateProductCommand)context.ActionArguments.First().Value);
                    arguments.CompanyId = int.Parse(companyId);
                    arguments.LastModifiedBy = int.Parse(userId);
                    arguments.LastModifiedDate = DateTime.UtcNow;
                }
                else if (context.HttpContext.Request.Method == "PATCH")
                {
                    if(context.ActionArguments.First().Value is IEnumerable<IArchiveProductCommand>)
                    {
                        foreach(var archive in ((IEnumerable<IArchiveProductCommand>)context.ActionArguments.First().Value))
                        {
                            archive.CompanyId = int.Parse(companyId);
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
