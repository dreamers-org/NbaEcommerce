using System;
using System.Collections.Generic;
using System.ComponentModel;
using NbaEcommerce.Models;

namespace NbaEcommerce.ViewModels
{
    public partial class ViewProdottoViewModel
    {
        public ViewProdottoViewModel()
        {
            Immagine = new HashSet<Immagine>();

        }

        public Guid Id { get; set; }
        public Guid IdMarchio { get; set; }
        public Guid IdCategoria { get; set; }
        public Guid IdDispositivo { get; set; }

        public string Titolo { get; set; }
        public string Dispositivo { get; set; }

        public string Descrizione { get; set; }

        [DisplayName("Prezzo vendita")]
        public double PrezzoVendita { get; set; }

        [DisplayName("Prezzo vacquisto")]
        public double PrezzoAcquisto { get; set; }
        public bool Attivo { get; set; }
        public int Quantità { get; set; }
        public string Marchio { get; set; }
        public string Categoria { get; set; }

        public ICollection<Immagine> Immagine { get; set; }

    }
}