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
            List<string> listaIdProdotti = new List<string>();

            if (HttpContext.Session.GetObject<List<string>>(Utility.Utility._KeyCarrello) != null)
            {
                listaIdProdotti = HttpContext.Session.GetObject<List<string>>(Utility.Utility._KeyCarrello);

                listaProdotti = await _context.Prodotto.Include(p => p.IdCategoriaNavigation)
                                    .Include(p => p.IdMarchioNavigation)
                                    .Include(p => p.IdDispositivoNavigation)
                                    .Include(p => p.Immagine)
                                    .Where(x => listaIdProdotti.Contains(x.Id.ToString())).ToListAsync();
            }

            return View(listaProdotti);
        }


        // GET: Dispositivo
        public IActionResult EliminaProdotto(Guid Id)
        {
            List<Prodotto> listaProdotti = new List<Prodotto>();


            //ottengo l'oggetto attualmente salvato in sessione.
            List<string> listaIdProdotti = new List<string>();

            if (HttpContext.Session.GetObject<List<string>>(Utility.Utility._KeyCarrello) != null)
            {
                listaIdProdotti = HttpContext.Session.GetObject<List<string>>(Utility.Utility._KeyCarrello);

                listaIdProdotti.Remove(Id.ToString());

                HttpContext.Session.SetObject(Utility.Utility._KeyCarrello, listaIdProdotti);
            }

            return RedirectToAction("Index");
        }
    }
}
