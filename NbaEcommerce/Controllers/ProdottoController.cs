using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NbaEcommerce.Models;

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

            var nbaStoreContext = _context.ViewProdotto.Where(x => (string.IsNullOrEmpty(marchio) || x.Marchio.Contains(marchio)) & (string.IsNullOrEmpty(categoria) || x.Categoria.Contains(categoria)));
            return View(await nbaStoreContext.ToListAsync());
        }

        // GET: Prodotto
        public async Task<IActionResult> IndexCliente(string marchio, string categoria)
        {
            ViewData["marchio"] = marchio;
            ViewData["categoria"] = categoria;
            ViewData["marchi"] = _context.Marchio.ToListAsync();

            var nbaStoreContext = _context.ViewProdotto.Where(x => (string.IsNullOrEmpty(marchio) || x.Marchio.Contains(marchio)) & (string.IsNullOrEmpty(categoria) || x.Categoria.Contains(categoria)));
            return View(await nbaStoreContext.ToListAsync());
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
            return View();
        }

        // POST: Prodotto/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdMarchio,IdCategoria,Titolo,Descrizione,PrezzoVendita,PrezzoAcquisto,Attivo,Quantità")] Prodotto prodotto, List<IFormFile> immagini)
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
                        Immagine img = new Immagine { Id = Guid.NewGuid(), IdProdotto = prodotto.Id, Data = memoryStream.ToArray() };
                        _context.Immagine.Add(img);
                        await _context.SaveChangesAsync();
                    }
                }

                return RedirectToAction(nameof(Index));
            }

            ViewData["IdCategoria"] = new SelectList(_context.Categoria, "Id", "Descrizione", prodotto.IdCategoria);
            ViewData["IdMarchio"] = new SelectList(_context.Marchio, "Id", "Descrizione", prodotto.IdMarchio);

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
            return View(prodotto);
        }

        // POST: Prodotto/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,IdMarchio,IdCategoria,Titolo,Descrizione,PrezzoVendita,PrezzoAcquisto,Attivo,Quantità")] Prodotto prodotto)
        {
            if (id != prodotto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(prodotto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProdottoExists(prodotto.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCategoria"] = new SelectList(_context.Categoria, "Id", "Descrizione", prodotto.IdCategoria);
            ViewData["IdMarchio"] = new SelectList(_context.Marchio, "Id", "Descrizione", prodotto.IdMarchio);
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
    }
}
