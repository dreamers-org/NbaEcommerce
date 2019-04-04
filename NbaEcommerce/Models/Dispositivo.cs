using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace NbaEcommerce.Models
{
    public partial class Dispositivo
    {

        public Dispositivo()
        {
            Prodotto = new HashSet<Prodotto>();
        }

        [Required]
        public Guid Id { get; set; }

        [Required]
        public string Descrizione { get; set; }

        [Required]
        public Guid IdMarchio { get; set; }

        public ICollection<Prodotto> Prodotto { get; set; }

        public Marchio IdMarchioNavigation { get; set; }
    }
}
