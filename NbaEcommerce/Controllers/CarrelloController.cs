using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NbaEcommerce.Models;

namespace NbaEcommerce.Controllers
{
    public class CarrelloController : Controller
    {
        private readonly NbaStoreContext _context;

        public CarrelloController(NbaStoreContext context)
        {
            _context = context;
        }

        // GET: Dispositivo
        public async Task<IActionResult> Index()
        {
            List<Prodotto> listaProdotti = new List<Prodotto>();


            //ottengo l'oggetto attualmente salvato in sessione.
            List<carrelloProdotto> listaIdProdotti = new List<carrelloProdotto>();

            

            if (HttpContext.Session.GetObject<List<carrelloProdotto>>(Utility.Utility._KeyCarrello) != null)
            {
                listaIdProdotti = HttpContext.Session.GetObject<List<carrelloProdotto>>(Utility.Utility._KeyCarrello);

                List<string> listaId = listaIdProdotti.Select(x => x.Id).ToList();
                listaProdotti = await _context.Prodotto.Include(p => p.IdCategoriaNavigation)
                                    .Include(p => p.IdMarchioNavigation)
                                    .Include(p => p.IdDispositivoNavigation)
                                    .Include(p => p.Immagine)
                                    .Where(x => listaId.Contains(x.Id.ToString())).ToListAsync();
            }

            return View(listaProdotti);
        }


        // GET: Dispositivo
        public IActionResult EliminaProdotto(Guid Id)
        {


            //ottengo l'oggetto attualmente salvato in sessione.
            List<carrelloProdotto> listaIdProdotti = new List<carrelloProdotto>();

            if (HttpContext.Session.GetObject<List<carrelloProdotto>>(Utility.Utility._KeyCarrello) != null)
            {
                listaIdProdotti = HttpContext.Session.GetObject<List<carrelloProdotto>>(Utility.Utility._KeyCarrello);

               var prodotto=  listaIdProdotti.Where(x => x.Id == Id.ToString()).First();
                listaIdProdotti.Remove(prodotto);

                HttpContext.Session.SetObject(Utility.Utility._KeyCarrello, listaIdProdotti);
            }

            return RedirectToAction("Index");
        }


        public IActionResult CreaOrdine()
        {

            //ottengo l'oggetto attualmente salvato in sessione.
            List<carrelloProdotto> listaIdProdotti = new List<carrelloProdotto>();

            if (HttpContext.Session.GetObject<List<carrelloProdotto>>(Utility.Utility._KeyCarrello) != null)
            {
                listaIdProdotti = HttpContext.Session.GetObject<List<carrelloProdotto>>(Utility.Utility._KeyCarrello);

                //creo l'ordine
                Guid idOrdine = new Guid();
                OrdineCliente ordineCliente = new OrdineCliente()
                {
                    Id = idOrdine,
                    IdCliente = new Guid("c413dc21-af58-4a5c-8ff4-bf43e4a4d782"),
                    DataInserimento = DateTime.Now,
                    DataModifica = DateTime.Now,
                    UtenteInserimento = HttpContext.User.Identity.Name,
                    UtenteModifica = HttpContext.User.Identity.Name,
                    Spedito = false,
                    SpeditoInParte = false,
                    DataConsegna = DateTime.Now.AddMonths(1),
                    Note = string.Empty,
                    Pagato = false
                };

                _context.OrdineCliente.Add(ordineCliente);

                foreach (var item in listaIdProdotti)
                {

                }

            }

            return RedirectToAction("Index");
        }


        public JsonResult IsCarrelloEmpty()
        {
            bool result = false;

            //ottengo l'oggetto attualmente salvato in sessione.
            List<carrelloProdotto> listaIdProdotti = new List<carrelloProdotto>();

            if (HttpContext.Session.GetObject<List<carrelloProdotto>>(Utility.Utility._KeyCarrello) != null)
            {
                listaIdProdotti = HttpContext.Session.GetObject<List<carrelloProdotto>>(Utility.Utility._KeyCarrello);

                if (listaIdProdotti != null && listaIdProdotti.Count > 0)
                {
                    result = true;
                }

            }

            return new JsonResult(result);
        }
    }
}
