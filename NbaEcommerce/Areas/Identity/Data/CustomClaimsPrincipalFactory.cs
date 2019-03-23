using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NbaEcommerce.Areas.Identity.Data
{
    public class CustomClaimsPrincipalFactory : UserClaimsPrincipalFactory<NbaEcommerceUser>
    {
        UserManager<NbaEcommerceUser> _userManager;

        public CustomClaimsPrincipalFactory(UserManager<NbaEcommerceUser> userManager,
            IOptions<IdentityOptions> optionsAccessor) : base(userManager, optionsAccessor)
        { }

        public async override Task<ClaimsPrincipal> CreateAsync(NbaEcommerceUser user)
        {
            var principal = await base.CreateAsync(user);
            ((ClaimsIdentity)principal.Identity).AddClaims(new[] {
              new Claim("IsDeveloper", "true")
            });
            return principal;
        }
    }
}
