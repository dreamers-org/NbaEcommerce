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
    public class RicercaController : Controller
    {
        private readonly NbaStoreContext _context;

        public RicercaController(NbaStoreContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> RicercaPerModello()
        {
            var nbaStoreContext = _context.Marchio;
            return View(await nbaStoreContext.ToListAsync());
        }
    }
}
