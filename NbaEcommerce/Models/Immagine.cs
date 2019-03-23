using System;
using System.Collections.Generic;

namespace NbaEcommerce.Models
{
    public partial class Immagine
    {
        public Guid Id { get; set; }
        public Guid IdProdotto { get; set; }
        public string ImageInfo { get; set; }
        public byte[] Data { get; set; }

        public Prodotto IdProdottoNavigation { get; set; }
    }
}
