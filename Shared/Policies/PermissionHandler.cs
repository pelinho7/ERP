using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Web;

namespace Policies
{
    public class PermissionHandler : AuthorizationHandler<PermissionRequirement>
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public PermissionHandler(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
        }

        protected async override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            var request = ((HttpContext)context.Resource).Request;
            var client = _httpClientFactory.CreateClient();
            if (request.Headers.TryGetValue("Authorization", out var authorizationHeader))
            {
                if (authorizationHeader.ToString().StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
                {
                    //get token from current request
                    var token = authorizationHeader.ToString().Substring("Bearer ".Length).Trim();

                    //set token to permission request
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                    var queryParams = new Dictionary<string, string>
                    {
                        { "module", requirement.Module.ToString() }
                    };

                    if (request.Method == HttpMethods.Get)
                    {
                        string? companyId = request.Query["companyId"];
                        queryParams.Add("companyId", companyId);
                    }

                    //create permission url with params
                    var uriBuilder = new UriBuilder(requirement.PermissionServiceUrl);
                    var query = HttpUtility.ParseQueryString(uriBuilder.Query);
                    foreach (var param in queryParams)
                    {
                        query[param.Key] = param.Value;
                    }
                    uriBuilder.Query = query.ToString();
                    string requestUri = uriBuilder.Uri.ToString();

                    var response = await client.GetAsync(requestUri);

                    if (response.IsSuccessStatusCode)
                    {
                        var responseContent = await response.Content.ReadAsStringAsync();
                        if (bool.TryParse(responseContent, out var result))
                        {
                            if (result)
                                context.Succeed(requirement);
                            else
                                context.Fail();
                        }
                        else
                        {
                            context.Fail();
                        }
                    }

                }
                else
                {
                    context.Fail();
                }
            }

            return;
        }
    }
}
