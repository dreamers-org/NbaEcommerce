using System;
using System.Collections.Generic;
using System.ComponentModel;

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
        public Guid IdDispositivo { get; set; }

        public string Titolo { get; set; }

        public string Descrizione { get; set; }

        [DisplayName("Prezzo vendita")]
        public double PrezzoVendita { get; set; }

        [DisplayName("Prezzo vacquisto")]
        public double PrezzoAcquisto { get; set; }
        public bool Attivo { get; set; }
        public int Quantità { get; set; }
        public Categoria IdCategoriaNavigation { get; set; }
        public Marchio IdMarchioNavigation { get; set; }
        public Dispositivo IdDispositivoNavigation { get; set; }
        public ICollection<Immagine> Immagine { get; set; }
    }
}
