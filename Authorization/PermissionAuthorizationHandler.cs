using Microsoft.AspNetCore.Authorization;
using Employee_hub_new.Models;
using System.Security.Claims;

namespace Employee_hub_new.Authorization
{
    public class PermissionRequirement : IAuthorizationRequirement
    {
        public string Permission { get; }

        public PermissionRequirement(string permission)
        {
            Permission = permission;
        }
    }

    public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            var userRole = context.User.FindFirst(ClaimTypes.Role)?.Value;
            
            if (string.IsNullOrEmpty(userRole))
            {
                return Task.CompletedTask;
            }

            if (RoleConstants.RolePermissions.TryGetValue(userRole, out var permissions))
            {
                if (permissions.Contains(requirement.Permission))
                {
                    context.Succeed(requirement);
                }
            }

            return Task.CompletedTask;
        }
    }

    public static class Policies
    {
        public static AuthorizationPolicy BuildPolicy(string permission)
        {
            return new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .AddRequirements(new PermissionRequirement(permission))
                .Build();
        }
    }
} 