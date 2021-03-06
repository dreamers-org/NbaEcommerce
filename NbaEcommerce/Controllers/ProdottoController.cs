﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NbaEcommerce.Models;
//using NbaEcommerce.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace NbaEcommerce.Controllers
{
    public class ProdottoController : Controller
    {
        private readonly NbaStoreContext _context;

        public ProdottoController(NbaStoreContext context)
        {
            _context = context;
        }

        // GET: Prodotto
        public async Task<IActionResult> Index(string marchio, string categoria)
        {
            ViewData["marchio"] = marchio;
            ViewData["categoria"] = categoria;

            var nbaStoreContext = _context.Prodotto
                                    .Include(p => p.IdCategoriaNavigation)
                                    .Include(p => p.IdMarchioNavigation)
                                    .Include(p => p.IdDispositivoNavigation)
                                    .Where(x => (string.IsNullOrEmpty(marchio) || x.IdMarchioNavigation.Descrizione.Contains(marchio)) & (string.IsNullOrEmpty(categoria) || x.IdCategoriaNavigation.Descrizione.Contains(categoria)));
            return View(await nbaStoreContext.ToListAsync());
        }

        public async Task<IActionResult> IndexCliente()
        {
            ViewData["categorie"] = await _context.Categoria.ToListAsync();
            ViewData["marchi"] = await _context.Marchio.ToListAsync();
            ViewData["dispositivi"] = await _context.Dispositivo.ToListAsync();

            ViewData["marchioSelezionato"] = null;
            ViewData["categoriaSelezionata"] = null;
            ViewData["dispositivoSelezionato"] = null;

            List<Prodotto> listaProdotti = await _context.Prodotto.
                                    Include(p => p.IdCategoriaNavigation)
                                    .Include(p => p.IdMarchioNavigation)
                                    .Include(p => p.IdDispositivoNavigation)
                                    .Include(p => p.Immagine).ToListAsync();
            return View(listaProdotti);
        }


        [HttpPost]
        public async Task<IActionResult> IndexCliente(Guid? marchio, Guid? categoria, Guid? dispositivo)
        {
            ViewData["categorie"] = await _context.Categoria.ToListAsync();
            ViewData["marchi"] = await _context.Marchio.ToListAsync();
            ViewData["dispositivi"] = await _context.Dispositivo.ToListAsync();

            List<Prodotto> listaProdotti = await _context.Prodotto
                                    .Include(p => p.IdCategoriaNavigation)
                                    .Include(p => p.IdMarchioNavigation)
                                    .Include(p => p.IdDispositivoNavigation)
                                    .Include(p => p.Immagine)
                                    .Where(x => (marchio == null || x.IdMarchio == marchio) & (dispositivo == null || dispositivo == x.IdDispositivo) & (categoria == null || x.IdCategoria == categoria)).ToListAsync();

            ViewData["dispositivoSelezionato"] = dispositivo;
            ViewData["categoriaSelezionata"] = categoria;
            ViewData["marchioSelezionato"] = marchio;

            return View(listaProdotti);

            //String[] arrayMarchiSelezionati = new string[0];
            //String[] arrayCategorieSelezionati = new string[0];
            //String[] arrayDispositiviSelezionati = new string[0];

            //List<ViewProdottoViewModel> nbaStoreContext = new List<ViewProdottoViewModel>();

            //if (listaMarchi != null)
            //{
            //    arrayMarchiSelezionati = listaMarchi?.Split(";");
            //    ViewData["dispositivi"] = await _context.Dispositivo.Where(x => arrayMarchiSelezionati.Contains(x.IdMarchio.ToString())).ToListAsync();
            //}

            ////if (listaCategorie != null)
            ////{
            ////    arrayCategorieSelezionati = listaCategorie?.Split(";");

            ////}

            //if (listaDispositivi != null)
            //{
            //    arrayDispositiviSelezionati = listaDispositivi?.Split(";");
            //}

            //nbaStoreContext = await _context.ViewProdotto.Where(x => (arrayCategorieSelezionati.Length == 0 | arrayCategorieSelezionati.Contains(x.IdCategoria.ToString())) & (arrayMarchiSelezionati.Length == 0 | arrayMarchiSelezionati.Contains(x.IdMarchio.ToString()))).ToListAsync();



            //return View(nbaStoreContext);
        }


        // GET: Prodotto/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prodotto = await _context.Prodotto
                .Include(p => p.IdCategoriaNavigation)
                .Include(p => p.IdMarchioNavigation)
                .Include(p => p.IdDispositivoNavigation)
                .Include(p => p.Immagine)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (prodotto == null)
            {
                return NotFound();
            }

            return View(prodotto);
        }

        // GET: Prodotto/Create
        public IActionResult Create()
        {
            ViewData["IdCategoria"] = new SelectList(_context.Categoria, "Id", "Descrizione");
            ViewData["IdMarchio"] = new SelectList(_context.Marchio, "Id", "Descrizione");
            ViewData["IdDispositivo"] = new SelectList(_context.Dispositivo, "Id", "Descrizione");

            return View();
        }

        // POST: Prodotto/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdMarchio,IdCategoria,IdDispositivo,Titolo,Descrizione,PrezzoVendita,PrezzoAcquisto,Attivo,Quantità")] Prodotto prodotto, List<IFormFile> immagini)
        {
            if (ModelState.IsValid)
            {
                prodotto.Id = Guid.NewGuid();
                _context.Add(prodotto);
                await _context.SaveChangesAsync();

                // full path to file in temp location
                var filePath = Path.GetTempFileName();

                foreach (IFormFile formFile in immagini)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await formFile.CopyToAsync(memoryStream);
                        Immagine img = new Immagine { Id = Guid.NewGuid(), IdProdotto = prodotto.Id, ImageInfo = formFile.FileName, Data = memoryStream.ToArray() };
                        _context.Immagine.Add(img);
                        await _context.SaveChangesAsync();
                    }
                }

                return RedirectToAction(nameof(Index));
            }

            ViewData["IdCategoria"] = new SelectList(_context.Categoria, "Id", "Descrizione", prodotto.IdCategoria);
            ViewData["IdMarchio"] = new SelectList(_context.Marchio, "Id", "Descrizione", prodotto.IdMarchio);
            ViewData["IdDispositivo"] = new SelectList(_context.Dispositivo, "Id", "Descrizione", prodotto.IdDispositivo);

            return View(prodotto);
        }

        // GET: Prodotto/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prodotto = await _context.Prodotto.FindAsync(id);
            if (prodotto == null)
            {
                return NotFound();
            }
            ViewData["IdCategoria"] = new SelectList(_context.Categoria, "Id", "Descrizione", prodotto.IdCategoria);
            ViewData["IdMarchio"] = new SelectList(_context.Marchio, "Id", "Descrizione", prodotto.IdMarchio);
            ViewData["IdDispositivo"] = new SelectList(_context.Dispositivo, "Id", "Descrizione", prodotto.IdDispositivo);

            return View(prodotto);
        }

        // POST: Prodotto/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,IdMarchio,IdCategoria,IdDispositivo,Titolo,Descrizione,PrezzoVendita,PrezzoAcquisto,Attivo,Quantità")] Prodotto prodotto, List<IFormFile> immagini)
        {
            if (id != prodotto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //rimuovo tutte le immagini
                    List<Immagine> listaImmaginiAttuali = await _context.Immagine.Where(x => x.IdProdotto == prodotto.Id).ToListAsync();
                    foreach (var item in listaImmaginiAttuali)
                    {
                        _context.Immagine.Remove(item);
                        await _context.SaveChangesAsync();
                    }

                    //Modifico il prodotto
                    _context.Update(prodotto);
                    await _context.SaveChangesAsync();

                    //Inserisco le immagini
                    var filePath = Path.GetTempFileName();

                    foreach (IFormFile formFile in immagini)
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            await formFile.CopyToAsync(memoryStream);
                            Immagine img = new Immagine { Id = Guid.NewGuid(), IdProdotto = prodotto.Id, Data = memoryStream.ToArray() };
                            _context.Immagine.Add(img);
                            await _context.SaveChangesAsync();
                        }
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProdottoExists(prodotto.Id))
                    {
                        return NotFound();
                    }

                    else
                    {


                    }
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCategoria"] = new SelectList(_context.Categoria, "Id", "Descrizione", prodotto.IdCategoria);
            ViewData["IdMarchio"] = new SelectList(_context.Marchio, "Id", "Descrizione", prodotto.IdMarchio);
            ViewData["IdDispositivo"] = new SelectList(_context.Dispositivo, "Id", "Descrizione", prodotto.IdDispositivo);

            return View(prodotto);
        }

        // GET: Prodotto/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prodotto = await _context.Prodotto
                .Include(p => p.IdCategoriaNavigation)
                .Include(p => p.IdMarchioNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (prodotto == null)
            {
                return NotFound();
            }

            return View(prodotto);
        }

        // POST: Prodotto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var prodotto = await _context.Prodotto.FindAsync(id);
            _context.Prodotto.Remove(prodotto);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProdottoExists(Guid id)
        {
            return _context.Prodotto.Any(e => e.Id == id);
        }


        public IActionResult GetDispositiviByMarchio(string marchiSelezionati)
        {
            JsonResult dispositivi = null;

            if (!String.IsNullOrEmpty(marchiSelezionati))
            {
                String[] arrMarchi = marchiSelezionati.Split(";");

                var listaColori = _context.Dispositivo.Where(x => arrMarchi.Contains(x.IdMarchio.ToString())).Select(x => new { Id = x.Id, Descrizione = x.Descrizione }).ToList();

                dispositivi = Json(listaColori);
            }

            return dispositivi;
        }


        /// <summary>
        /// Aggiunge il prodotto nel carrello.
        /// </summary>
        /// <param name="IdProdotto"></param>
        /// <returns></returns>
        public async Task<IActionResult> Aggiungi(Guid IdProdotto)
        {
            //ottengo l'oggetto attualmente salvato in sessione.
            List<carrelloProdotto> listaIdProdotti = new List<carrelloProdotto>();

            if (HttpContext.Session.GetObject<List<carrelloProdotto>>(Utility.Utility._KeyCarrello) != null)
            {
                listaIdProdotti = HttpContext.Session.GetObject<List<carrelloProdotto>>(Utility.Utility._KeyCarrello);
            }

            //controllo se listaProdotti contiene idProdotto ( la lista non può essere vuota: GetObject() restituisce il default di T)
            if (!(listaIdProdotti.Where(x => x.Id == IdProdotto.ToString()).ToList().Count > 0))
            {
                //Aggiungo il prodotto alla lista
                listaIdProdotti.Add(new carrelloProdotto() { Id = IdProdotto.ToString(), Quantita = 1 });

                //aggiorno la sessione.
                HttpContext.Session.SetObject(Utility.Utility._KeyCarrello, listaIdProdotti);
            }

            return RedirectToAction(nameof(IndexCliente));
        }
    }
}
