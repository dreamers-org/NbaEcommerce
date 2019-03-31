using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace NbaEcommerce.Models
{
    public partial class Dispositivo
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string Descrizione { get; set; }

        [Required]
        public Guid IdMarchio { get; set; }
      
        public Marchio IdMarchioNavigation { get; set; }
    }
}
