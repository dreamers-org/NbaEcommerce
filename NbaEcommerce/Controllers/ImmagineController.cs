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
    public class ImmagineController : Controller
    {
        private readonly NbaStoreContext _context;

        public ImmagineController(NbaStoreContext context)
        {
            _context = context;
        }

        // GET: Immagine
        public async Task<IActionResult> Index()
        {
            var nbaStoreContext = _context.Immagine.Include(i => i.IdProdottoNavigation);
            return View(await nbaStoreContext.ToListAsync());
        }

        // GET: Immagine/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var immagine = await _context.Immagine
                .Include(i => i.IdProdottoNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (immagine == null)
            {
                return NotFound();
            }

            return View(immagine);
        }

        // GET: Immagine/Create
        public IActionResult Create()
        {
            ViewData["IdProdotto"] = new SelectList(_context.Prodotto, "Id", "Id");
            return View();
        }

        // POST: Immagine/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdProdotto,ImageInfo,Data")] Immagine immagine)
        {
            if (ModelState.IsValid)
            {
                immagine.Id = Guid.NewGuid();
                _context.Add(immagine);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdProdotto"] = new SelectList(_context.Prodotto, "Id", "Id", immagine.IdProdotto);
            return View(immagine);
        }

        // GET: Immagine/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var immagine = await _context.Immagine.FindAsync(id);
            if (immagine == null)
            {
                return NotFound();
            }
            ViewData["IdProdotto"] = new SelectList(_context.Prodotto, "Id", "Id", immagine.IdProdotto);
            return View(immagine);
        }

        // POST: Immagine/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,IdProdotto,ImageInfo,Data")] Immagine immagine)
        {
            if (id != immagine.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(immagine);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ImmagineExists(immagine.Id))
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
            ViewData["IdProdotto"] = new SelectList(_context.Prodotto, "Id", "Id", immagine.IdProdotto);
            return View(immagine);
        }

        // GET: Immagine/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var immagine = await _context.Immagine
                .Include(i => i.IdProdottoNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (immagine == null)
            {
                return NotFound();
            }

            return View(immagine);
        }

        // POST: Immagine/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var immagine = await _context.Immagine.FindAsync(id);
            _context.Immagine.Remove(immagine);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ImmagineExists(Guid id)
        {
            return _context.Immagine.Any(e => e.Id == id);
        }
    }
}
