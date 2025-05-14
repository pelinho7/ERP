using Microsoft.AspNetCore.Authorization;

namespace Policies
{
    public class PermissionRequirement : IAuthorizationRequirement
    {
        public readonly int Module;
        public readonly string PermissionServiceUrl;
        public PermissionRequirement(int module, string permissionServiceUrl)
        {
            Module = module;
            PermissionServiceUrl = permissionServiceUrl;
        }
    }
}
