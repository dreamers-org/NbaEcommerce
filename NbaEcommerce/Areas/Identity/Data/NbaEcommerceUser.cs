using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace NbaEcommerce.Areas.Identity.Data
{
    public class NbaEcommerceUser : IdentityUser
    {
        //Dati Generali
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo obbligatorio.")]
        [MaxLength(50, ErrorMessage = "Nome azienda troppo lungo")]
        public string NomeAzienda { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo obbligatorio.")]
        [MaxLength(500, ErrorMessage = "Nome troppo lungo")]
        public string PersonaRiferimento { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo obbligatorio.")]
        [RegularExpression(@"^[0|3]{1}[0-9]{5,10}$", ErrorMessage = "Inserire un numero di telefono valido")]
        public string Telefono { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo obbligatorio.")]
        [MaxLength(16, ErrorMessage = "Inserire un codice fiscale valido")]
        public string CodiceFiscale { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo obbligatorio.")]
        [MaxLength(11, ErrorMessage = "Inserire una partita iva valida")]
        public string PartitaIva { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo obbligatorio.")]
        [MaxLength(8000, ErrorMessage = "Testo troppo lungo.")]
        public string Note { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo obbligatorio.")]
        public string TipoAzienda { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo obbligatorio.")]
        [DefaultValue(0)]
        public bool PrivacyAccettata { get; set; }

        //Dati Sede legale
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo obbligatorio.")]
        [MaxLength(800, ErrorMessage = "Testo troppo lungo.")]
        public string IndirizzoSedeLegale { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo obbligatorio.")]
        [Range(0, 99999, ErrorMessage = "Inserire un cap valido")]
        public int CapSedeLegale { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo obbligatorio.")]
        [MaxLength(80, ErrorMessage = "Testo troppo lungo.")]
        public string CittàSedeLegale { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo obbligatorio.")]
        [MaxLength(2, ErrorMessage = "Inserire una provincia valida.")]
        public string ProvinciaSedeLegale { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo obbligatorio.")]
        [MaxLength(80, ErrorMessage = "Testo troppo lungo.")]
        public string NazioneSedeLegale { get; set; }

        //Dati Sede Operativa
        [MaxLength(800, ErrorMessage = "Testo troppo lungo.")]
        public string IndirizzoSedeOperativa { get; set; }
        [Range(0, 99999, ErrorMessage = "Inserire un cap valido")]
        public int? CapSedeOperativa { get; set; }
        [MaxLength(80, ErrorMessage = "Testo troppo lungo.")]
        public string CittàSedeOperativa { get; set; }
        [MaxLength(2, ErrorMessage = "Inserire una provincia valida.")]
        public string ProvinciaSedeOperativa { get; set; }
        [MaxLength(80, ErrorMessage = "Testo troppo lungo.")]
        public string NazioneSedeOperativa { get; set; }
    }
}
