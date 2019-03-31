using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Serilog;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NbaEcommerce.Areas.Identity.Data
{
    public class CustomClaimsPrincipalFactory : UserClaimsPrincipalFactory<NbaEcommerceUser>
    {
        public CustomClaimsPrincipalFactory(UserManager<NbaEcommerceUser> userManager, RoleManager<IdentityRole> roleManager,
            IOptions<IdentityOptions> optionsAccessor) : base(userManager, optionsAccessor)
        { }

        public async override Task<ClaimsPrincipal> CreateAsync(NbaEcommerceUser user)
        {
            try
            {
                var principal = await base.CreateAsync(user);

                if (!string.IsNullOrWhiteSpace(user.UserName))
                {
                    ((ClaimsIdentity)principal.Identity).AddClaims(new[] {
                        new Claim(ClaimTypes.Role, "Cliente"),
                          new Claim("NomeAzienda", user.NomeAzienda),
                          new Claim("PersonaRiferimento", user.PersonaRiferimento),
                          new Claim("Telefono", user.Telefono),
                          new Claim("CodiceFiscale", user.CodiceFiscale),
                          new Claim("PartitaIva", user.PartitaIva),
                          new Claim("Note", user.Note),
                          new Claim("TipoAzienda", user.TipoAzienda),
                          new Claim("PrivacyAccettata", user.PrivacyAccettata.ToString()),
                          new Claim("IndirizzoSedeLegale", user.IndirizzoSedeLegale),
                          new Claim("CapSedeLegale", user.CapSedeLegale.ToString()),
                          new Claim("CittàSedeLegale", user.CittàSedeLegale),
                          new Claim("ProvinciaSedeLegale", user.ProvinciaSedeLegale),
                          new Claim("NazioneSedeLegale", user.NazioneSedeLegale),
                          new Claim("IndirizzoSedeOperativa", user.IndirizzoSedeOperativa ?? user.IndirizzoSedeLegale),
                          new Claim("CapSedeOperativa", user.CapSedeOperativa == null ? user.CapSedeLegale.ToString() : user.CapSedeOperativa.ToString()),
                          new Claim("CittàSedeOperativa", user.CittàSedeOperativa ?? user.CittàSedeLegale),
                          new Claim("ProvinciaSedeOperativa", user.ProvinciaSedeOperativa ??  user.ProvinciaSedeLegale),
                          new Claim("NazioneSedeOperativa", user.NazioneSedeOperativa ?? user.NazioneSedeLegale)
                });
                }

                return principal;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                throw;
            }
        }
    }
}
