using System.Security.Claims;

namespace Sanatorium.Core.Users;

public static class UserExtensions
{
    public static string UserEmail(this ClaimsPrincipal claimsPrincipal) =>
        claimsPrincipal.Claims
            .FirstOrDefault(c => c.Type.Equals("emails"))
            ?.Value ?? string.Empty;

    public static bool IsAdmin(this ClaimsPrincipal claimsPrincipal) =>
        claimsPrincipal.Claims.FirstOrDefault(c => c.Type.Equals("extension_Roles"))?.Value
            .Contains(Role.Admin.ToString())??false;

    public static bool HasRole(this ClaimsPrincipal claimsPrincipal, Role role) =>
        claimsPrincipal.Claims.FirstOrDefault(c => c.Type.Equals("extension_Roles"))?.Value
            .Contains(role.ToString())??false;
}