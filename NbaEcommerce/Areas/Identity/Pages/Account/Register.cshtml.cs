using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using NbaEcommerce.Areas.Identity.Data;

namespace NbaEcommerce.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<NbaEcommerceUser> _signInManager;
        private readonly UserManager<NbaEcommerceUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;

        public RegisterModel(
            UserManager<NbaEcommerceUser> userManager,
            SignInManager<NbaEcommerceUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public class InputModel
        {
            [Required(AllowEmptyStrings = false, ErrorMessage = "Campo obbligatorio.")]
            [MaxLength(50, ErrorMessage = "Nome azienda troppo lungo")]
            [Display(Name = "Nome azienda*")]
            public string NomeAzienda { get; set; }

            [Required(AllowEmptyStrings = false, ErrorMessage = "Campo obbligatorio.")]
            [MaxLength(500, ErrorMessage = "Nome troppo lungo")]
            [Display(Name = "Persona di riferimento*")]
            public string PersonaRiferimento { get; set; }

            [Required(AllowEmptyStrings = false, ErrorMessage = "Campo obbligatorio.")]
            [RegularExpression(@"^[0|3]{1}[0-9]{5,10}$", ErrorMessage = "Inserire un numero di telefono valido")]
            [Display(Name = "Numero di telefono*")]
            public string Telefono { get; set; }

            [Required(AllowEmptyStrings = false, ErrorMessage = "Campo obbligatorio.")]
            [RegularExpression(@"^[0|3]{1}[0-9]{5,10}$", ErrorMessage = "Inserire un numero di telefono valido")]
            [Display(Name = "Cellulare*")]
            public string Cellulare { get; set; }

            [Required(AllowEmptyStrings = false, ErrorMessage = "Campo obbligatorio.")]
            [MaxLength(16, ErrorMessage = "Inserire un codice fiscale valido")]
            [Display(Name = "Codice fiscale*")]
            public string CodiceFiscale { get; set; }

            [Required(AllowEmptyStrings = false, ErrorMessage = "Campo obbligatorio.")]
            [MaxLength(11, ErrorMessage = "Inserire una partita iva valida")]
            [Display(Name = "Partita iva*")]
            public string PartitaIva { get; set; }

            [MaxLength(8000, ErrorMessage = "Testo troppo lungo.")]
            [Display(Name = "Note")]
            public string Note { get; set; }

            [Display(Name = "Tipo azienda*")]
            [Required(AllowEmptyStrings = false, ErrorMessage = "Campo obbligatorio.")]
            public string TipoAzienda { get; set; }

            [Required(AllowEmptyStrings = false, ErrorMessage = "Campo obbligatorio.")]
            [DefaultValue(0)]
            [Display(Name = "Privacy*")]
            public bool PrivacyAccettata { get; set; }

            [Required(ErrorMessage = "Campo obbligatorio.")]
            [EmailAddress(ErrorMessage = "Inserire una mail valida.")]
            [Display(Name = "Email*")]
            public string Email { get; set; }

            [Required(ErrorMessage = "Campo obbligatorio.")]
            [StringLength(100, ErrorMessage = "La password deve essere lunga almeno 6 caratteri.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password*")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Conferma password*")]
            [Compare("Password", ErrorMessage = "Le due password non coincidono.")]
            public string ConfirmPassword { get; set; }

            //Dati Sede legale
            [Required(AllowEmptyStrings = false, ErrorMessage = "Campo obbligatorio.")]
            [MaxLength(800, ErrorMessage = "Testo troppo lungo.")]
            [Display(Name = "Indirizzo*")]
            public string IndirizzoSedeLegale { get; set; }

            [Required(AllowEmptyStrings = false, ErrorMessage = "Campo obbligatorio.")]
            [Range(0, 99999, ErrorMessage = "Inserire un cap valido")]
            [Display(Name = "Cap*")]
            public int CapSedeLegale { get; set; }

            [Required(AllowEmptyStrings = false, ErrorMessage = "Campo obbligatorio.")]
            [Display(Name = "Città*")]
            [MaxLength(80, ErrorMessage = "Testo troppo lungo.")]
            public string CittàSedeLegale { get; set; }

            [Required(AllowEmptyStrings = false, ErrorMessage = "Campo obbligatorio.")]
            [MaxLength(2, ErrorMessage = "Inserire una provincia valida.")]
            [Display(Name = "Provincia*")]
            public string ProvinciaSedeLegale { get; set; }

            [Required(AllowEmptyStrings = false, ErrorMessage = "Campo obbligatorio.")]
            [MaxLength(80, ErrorMessage = "Testo troppo lungo.")]
            [Display(Name = "Nazione*")]
            public string NazioneSedeLegale { get; set; }

            //Dati Sede Operativa
            [MaxLength(800, ErrorMessage = "Testo troppo lungo.")]
            [Display(Name = "Indirizzo")]
            public string IndirizzoSedeOperativa { get; set; }

            [Range(0, 99999, ErrorMessage = "Inserire un cap valido")]
            [Display(Name = "Cap.")]
            public int CapSedeOperativa { get; set; }

            [MaxLength(80, ErrorMessage = "Testo troppo lungo.")]
            [Display(Name = "Città")]
            public string CittàSedeOperativa { get; set; }

            [MaxLength(2, ErrorMessage = "Inserire una provincia valida.")]
            [Display(Name = "Provincia")]
            public string ProvinciaSedeOperativa { get; set; }

            [MaxLength(80, ErrorMessage = "Testo troppo lungo.")]
            [Display(Name = "Nazione")]
            public string NazioneSedeOperativa { get; set; }
        }

        public void OnGet(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            if (ModelState.IsValid)
            {
                var user = new NbaEcommerceUser
                {
                    UserName = Input.Email,
                    Email = Input.Email
                };
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { userId = user.Id, code = code },
                        protocol: Request.Scheme);
                    var claim = new Claim("Test", "ok");
                    await _userManager.AddClaimAsync(user, claim);

                    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                    $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return LocalRedirect(returnUrl);
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
