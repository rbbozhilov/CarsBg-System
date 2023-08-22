using System.Security.Claims;

namespace CarsBg_System.Infrastructure
{
    public static class ClaimsPrincipalExtenssions
    {

        public static string GetId(this ClaimsPrincipal user)
            => user.FindFirst(ClaimTypes.NameIdentifier).Value;

    }
}
