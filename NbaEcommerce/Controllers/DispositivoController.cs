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
    public class DispositivoController : Controller
    {
        private readonly NbaStoreContext _context;

        public DispositivoController(NbaStoreContext context)
        {
            _context = context;
        }

        // GET: Dispositivo
        public async Task<IActionResult> Index()
        {
            var nbaStoreContext = _context.Dispositivo.Include(d => d.IdMarchioNavigation);
            return View(await nbaStoreContext.ToListAsync());
        }

        // GET: Dispositivo/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dispositivo = await _context.Dispositivo
                .Include(d => d.IdMarchioNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dispositivo == null)
            {
                return NotFound();
            }

            return View(dispositivo);
        }

        // GET: Dispositivo/Create
        public IActionResult Create()
        {
            ViewData["IdMarchio"] = new SelectList(_context.Marchio, "Id", "Descrizione");
            return View();
        }

        // POST: Dispositivo/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Descrizione,IdMarchio")] Dispositivo dispositivo)
        {
            if (ModelState.IsValid)
            {
                dispositivo.Id = Guid.NewGuid();
                _context.Add(dispositivo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdMarchio"] = new SelectList(_context.Marchio, "Id", "Descrizione", dispositivo.IdMarchio);
            return View(dispositivo);
        }

        // GET: Dispositivo/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dispositivo = await _context.Dispositivo.FindAsync(id);
            if (dispositivo == null)
            {
                return NotFound();
            }
            ViewData["IdMarchio"] = new SelectList(_context.Marchio, "Id", "Descrizione", dispositivo.IdMarchio);
            return View(dispositivo);
        }

        // POST: Dispositivo/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Descrizione,IdMarchio")] Dispositivo dispositivo)
        {
            if (id != dispositivo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dispositivo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DispositivoExists(dispositivo.Id))
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
            ViewData["IdMarchio"] = new SelectList(_context.Marchio, "Id", "Descrizione", dispositivo.IdMarchio);
            return View(dispositivo);
        }

        // GET: Dispositivo/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dispositivo = await _context.Dispositivo
                .Include(d => d.IdMarchioNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dispositivo == null)
            {
                return NotFound();
            }

            return View(dispositivo);
        }

        // POST: Dispositivo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var dispositivo = await _context.Dispositivo.FindAsync(id);
            _context.Dispositivo.Remove(dispositivo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DispositivoExists(Guid id)
        {
            return _context.Dispositivo.Any(e => e.Id == id);
        }
    }
}
