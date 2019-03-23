using System;
using System.Collections.Generic;

namespace NbaEcommerce.Models
{
    public partial class Prodotto
    {
        public Prodotto()
        {
            Immagine = new HashSet<Immagine>();
        }

        public Guid Id { get; set; }
        public Guid IdMarchio { get; set; }
        public Guid IdCategoria { get; set; }
        public double PrezzoVendita { get; set; }
        public double PrezzoAcquisto { get; set; }
        public bool Attivo { get; set; }
        public int Quantità { get; set; }

        public Categoria IdCategoriaNavigation { get; set; }
        public Marchio IdMarchioNavigation { get; set; }
        public ICollection<Immagine> Immagine { get; set; }
    }
}
