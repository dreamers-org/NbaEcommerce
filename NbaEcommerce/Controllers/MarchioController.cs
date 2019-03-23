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
    public class MarchioController : Controller
    {
        private readonly NbaStoreContext _context;

        public MarchioController(NbaStoreContext context)
        {
            _context = context;
        }

        // GET: Marchio
        public async Task<IActionResult> Index()
        {
            return View(await _context.Marchio.ToListAsync());
        }

        // GET: Marchio/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var marchio = await _context.Marchio
                .FirstOrDefaultAsync(m => m.Id == id);
            if (marchio == null)
            {
                return NotFound();
            }

            return View(marchio);
        }

        // GET: Marchio/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Marchio/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Descrizione,Attivo")] Marchio marchio)
        {
            if (ModelState.IsValid)
            {
                marchio.Id = Guid.NewGuid();
                _context.Add(marchio);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(marchio);
        }

        // GET: Marchio/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var marchio = await _context.Marchio.FindAsync(id);
            if (marchio == null)
            {
                return NotFound();
            }
            return View(marchio);
        }

        // POST: Marchio/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Descrizione,Attivo")] Marchio marchio)
        {
            if (id != marchio.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(marchio);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MarchioExists(marchio.Id))
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
            return View(marchio);
        }

        // GET: Marchio/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var marchio = await _context.Marchio
                .FirstOrDefaultAsync(m => m.Id == id);
            if (marchio == null)
            {
                return NotFound();
            }

            return View(marchio);
        }

        // POST: Marchio/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var marchio = await _context.Marchio.FindAsync(id);
            _context.Marchio.Remove(marchio);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MarchioExists(Guid id)
        {
            return _context.Marchio.Any(e => e.Id == id);
        }
    }
}
