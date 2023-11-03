using System.Security.Claims;

namespace KeyShop {
    public static class ClaimsPrincipalExtensions {
        public static string GetUserId(this ClaimsPrincipal user) {
            return user.FindFirst(ClaimTypes.NameIdentifier).Value;
        }
    }
}
