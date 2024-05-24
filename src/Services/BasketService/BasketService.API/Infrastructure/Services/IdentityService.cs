using BasketService.API.Application.Services;
using System.Security.Claims;

namespace BasketService.API.Infrastructure.Services
{
    public class IdentityService(IHttpContextAccessor httpContextAccessor) : IIdentityService
    {
        public string GetUserName()
            => httpContextAccessor.HttpContext.User.FindFirst(x => x.Type == ClaimTypes.NameIdentifier).Value;

    }
}
