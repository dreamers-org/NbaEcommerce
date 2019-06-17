using System;
using System.Collections.Generic;

namespace NbaEcommerce.Models
{
    public partial class RigaOrdineCliente
    {
        public Guid Id { get; set; }
        public Guid IdOrdine { get; set; }

        public Guid IdProdotto { get; set; }

        public bool Spedito { get; set; }

        public int Quantita { get; set; }

        public DateTime DataInserimento { get; set; }
        public DateTime? DataModifica { get; set; }
        public string UtenteInserimento { get; set; }
        public string UtenteModifica { get; set; }

        public OrdineCliente IdOrdineNavigation { get; set; }
        public Prodotto IdProdottoNavigation { get; set; }
    }
}
