using System;
using System.Collections.Generic;

namespace NbaEcommerce.Models
{
    public partial class OrdineCliente
    {
        public Guid Id { get; set; }
        public Guid IdCliente { get; set; }
        public DateTime DataConsegna { get; set; }
        public string Note { get; set; }
        public bool Pagato { get; set; }
        public DateTime DataInserimento { get; set; }
        public DateTime? DataModifica { get; set; }
        public string UtenteInserimento { get; set; }
        public string UtenteModifica { get; set; }
        public bool Spedito { get; set; }
        public bool SpeditoInParte { get; set; }

        public ICollection<RigaOrdineCliente> RigaOrdineCliente { get; set; }

    }
}
