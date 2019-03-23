using System;
using System.Collections.Generic;

namespace NbaEcommerce.Models
{
    public partial class Marchio
    {
        public Marchio()
        {
            Prodotto = new HashSet<Prodotto>();
        }

        public Guid Id { get; set; }
        public string Descrizione { get; set; }
        public bool Attivo { get; set; }

        public ICollection<Prodotto> Prodotto { get; set; }
    }
}
