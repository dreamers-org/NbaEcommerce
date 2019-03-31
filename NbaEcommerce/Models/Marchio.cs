using System;
using System.Collections.Generic;

namespace NbaEcommerce.Models
{
    public partial class Marchio
    {
        public Marchio()
        {
            Prodotto = new HashSet<Prodotto>();
            Dispositivo = new HashSet<Dispositivo>();

        }

        public Guid Id { get; set; }
        public string Descrizione { get; set; }
        public bool Attivo { get; set; }

        public ICollection<Prodotto> Prodotto { get; set; }

        public ICollection<Dispositivo> Dispositivo { get; set; }
    }
}
