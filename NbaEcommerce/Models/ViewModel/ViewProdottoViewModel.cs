using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace NbaEcommerce.ViewModels
{
    public partial class ViewProdottoViewModel
    {
        public ViewProdottoViewModel()
        {
        }

        public Guid Id { get; set; }
        public Guid IdMarchio { get; set; }
        public Guid IdCategoria { get; set; }
        public string Descrizione { get; set; }

        [DisplayName("Prezzo vendita")]
        public double PrezzoVendita { get; set; }

        [DisplayName("Prezzo vacquisto")]
        public double PrezzoAcquisto { get; set; }
        public bool Attivo { get; set; }
        public int Quantità { get; set; }
        public string Marchio { get; set; }
        public string Categoria { get; set; }
    }
}